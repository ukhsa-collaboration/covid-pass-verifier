using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models.Cose;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Models.International;
using Newtonsoft.Json;
using PeterO.Cbor;

namespace NHSCovidPassVerifier.Services.Repositories
{
    public class QrDecoderService : IQrDecoderService
    {
        private ICertificate _decodedCertificate;
        private ICertificatePayload _certificatePayload;
        
        private readonly IQrValidatorService _qrValidatorService;
        private readonly ILoggingService _loggingService;
        private readonly IBase45Service _base45Service;
        private readonly IZLibService _zLibService;

        private readonly IDictionary<CertificateType, Func<string, Task<bool>>> _validateQrCodeMap;
        private readonly IDictionary<CertificateType, Func<ICertificate>> _generateCertificateMap;

        public QrDecoderService(IQrValidatorService qrValidatorService, ILoggingService loggingService, 
            IBase45Service base45Service, IZLibService zLibService)
        {
            _qrValidatorService = qrValidatorService;
            _loggingService = loggingService;
            _base45Service = base45Service;
            _zLibService = zLibService;
            
            _validateQrCodeMap = new Dictionary<CertificateType, Func<string, Task<bool>>>
            {
                [CertificateType.Domestic] = ValidateQrCodeForDomesticCertificate,
                [CertificateType.International] = ValidateQrCodeForInternationalCertificate
            };

            _generateCertificateMap = new Dictionary<CertificateType, Func<ICertificate>>
            {
                [CertificateType.Domestic] = GenerateDomesticCertificateFromQrCode,
                [CertificateType.International] = GenerateInternationalCertificateFromQrCode
            };
        }

        public Task<bool> ValidateQrCode(string qrCode)
        {
            _certificatePayload = default;
            var resultType = CertificateTypeExtension.GetCertificateType(qrCode.Substring(0, 3));
            
            return _validateQrCodeMap[resultType].Invoke(qrCode);
        }

        public ICertificate GenerateCertificateFromQrCode()
        {
            var resultType = _certificatePayload?.GetCertificateType() ?? CertificateType.Domestic;
            return _generateCertificateMap[resultType].Invoke();
        }

        public ICertificate GetDecodedCertificate()
        {
            return _decodedCertificate;
        }

        private async Task<bool> ValidateQrCodeForDomesticCertificate(string qrCode)
        {
            try
            {
                var tokenParts = qrCode.Split('.');
                if (tokenParts.Length != 3)
                {
                    return false;
                }
                var header = tokenParts[0];
                var payload = tokenParts[1];
                var signature = tokenParts[2];
                var byteData = Encoding.Unicode.GetBytes($"{header}.{payload}");

                var sigBytes = Base64UrlEncoder.DecodeBytes(signature);

                var validSignature = await _qrValidatorService.ValidateQrCodeForDomesticCertificate(byteData, sigBytes, header);
                if (validSignature)
                {
                    _certificatePayload = new DomesticCertificatePayload(Base64UrlEncoder.Decode(payload));
                }

                return validSignature;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(QrDecoderService)}] ValidateQrCodeForDomesticCertificate");
                return false;
            }
        }

        private DomesticCertificate GenerateDomesticCertificateFromQrCode()
        {
            try
            {
                if (_certificatePayload == null || string.IsNullOrEmpty(((DomesticCertificatePayload) _certificatePayload).PayloadContent))
                    return default;

                var input = ((DomesticCertificatePayload) _certificatePayload).PayloadContent.Substring(1);

                var numMatch = Regex.Match(input, "[0-9]", RegexOptions.IgnoreCase);

                var nameMatch = Regex.Match(input, "[a-z]", RegexOptions.IgnoreCase);

                if (!numMatch.Success || !nameMatch.Success)
                    return default;

                var charLocation = input.IndexOf(nameMatch.Value, StringComparison.Ordinal);

                var numLocation = input.IndexOf(numMatch.Value, StringComparison.Ordinal);

                if (numLocation != 0 || charLocation <= 0) return default;

                var name = input.Substring(charLocation);

                var expiry = input.Substring(numLocation, charLocation);

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(expiry) ||
                    !DateTime.TryParseExact(expiry, "yyMMddHHmm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var validExpiryDate))
                    return default;

                var certificate = new DomesticCertificate
                {
                    FullName = name,
                    Expiry = validExpiryDate
                };

                _decodedCertificate = certificate;
                return certificate;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(QrDecoderService)}] GenerateDomesticCertificateFromQrCode");
            }

            return default;
        }

        private async Task<bool> ValidateQrCodeForInternationalCertificate(string qrCode)
        {
            try
            {
                var coseSign1Object = DecodeToCose(qrCode.Substring(4));
                var validSignature =
                    await _qrValidatorService.ValidateQrCodeForInternationalCertificate(coseSign1Object);

                if (!validSignature) return false;

                var json = coseSign1Object.GetJson();
                _certificatePayload = JsonConvert.DeserializeObject<InternationalCertificatePayload>(json);
                return true;
            }
            catch (CBORException cborException)
            {
                _loggingService.LogException(LogSeverity.ERROR, cborException,
                    $"INVALID QR CODE [{nameof(CBORException)}] : {cborException.Message}");

                return false;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e,
                    $"Error[{nameof(QrDecoderService)}] ValidateQrCodeForInternationalCertificate");
                return false;
            }
        }

        private InternationalCertificate GenerateInternationalCertificateFromQrCode()
        {
            var internationalCertificate = new InternationalCertificate();
            try
            {
                if (_certificatePayload == null)
                {
                    return default;
                }

                internationalCertificate.DecodedModel = (InternationalCertificatePayload) _certificatePayload;
                _decodedCertificate = internationalCertificate;
                return internationalCertificate;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(QrDecoderService)}] GenerateInternationalCertificateFromQrCode");
                return internationalCertificate;
            }
        }

        public CoseSign1Object DecodeToCose(string base45String)
        {
            var compressedBytesFromBase45Token = _base45Service.Base45Decode(base45String);
            var decompressedSignedCoseBytes = _zLibService.DecompressData(compressedBytesFromBase45Token);
            var cborMessageFromCose = CoseSign1Object.Decode(decompressedSignedCoseBytes);
            
            return cborMessageFromCose;
        }
    }
}

