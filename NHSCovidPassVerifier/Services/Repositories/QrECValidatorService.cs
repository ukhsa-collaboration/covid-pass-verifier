using NHSCovidPassVerifier.Models.Dtos;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Cose;
using NHSCovidPassVerifier.Services.ErrorHandlers;

namespace NHSCovidPassVerifier.Services.Repositories
{
    public class QrECValidatorService : IQrValidatorService
    {
        private readonly ISecureStorageService<JwkDto> _secureStorage;
        private readonly ILoggingService _loggingService;
        private readonly ISettingsService _settingsService;

        public QrECValidatorService(ISecureStorageService<JwkDto> secureStorage, ISettingsService settingsService, ILoggingService loggingService)
        {
            _secureStorage = secureStorage;
            _loggingService = loggingService;
            _settingsService = settingsService;
        }

        public async Task<bool> ValidateQrCodeForDomesticCertificate(byte[] digest, byte[] signature, string header)
        {
            try
            {
                var ecKey = (await GetKeyVaultKey(header)).FirstOrDefault();
                if (ecKey == default)
                {
                    return false;
                }

                var key = PublicKeyFactory.CreateKey(Convert.FromBase64String(ecKey.PublicKey));

                var verifier = SignerUtilities.GetSigner("SHA256withPLAIN-ECDSA");
                verifier.Init(false, key);
                verifier.BlockUpdate(digest, 0, digest.Length);
                return verifier.VerifySignature(signature);
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(QrECValidatorService)}] ValidateQrCodeForCertificate");
                return false;
            }
        }

        public async Task<bool> ValidateQrCodeForInternationalCertificate(CoseSign1Object coseSign1Object)
        {
            try
            {
                var kidBase64 = Convert.ToBase64String(coseSign1Object.GetKeyIdentifier());

                var publicKeys = (await GetKeyVaultKey(kidBase64)).ToList();


                if (!publicKeys.Any())
                {
                    throw new InvalidDataException($"no public key corespondent to provided key identifier found. key identifier: {kidBase64}");
                }

                foreach (var publicKey in publicKeys)
                {
                    try
                    {
                        var pk = Convert.FromBase64String(publicKey.PublicKey);
                        coseSign1Object.VerifySignature(pk);
                        return true;
                    }
                    catch (SignatureVerificationException e)
                    {
                        _loggingService.LogException(LogSeverity.ERROR, e, $"[{nameof(SignatureVerificationException)}] : {e.Message}");
                    }
                }

                throw new Exception("Signature verification failed for all attempted keys");
            }
            catch (SignatureVerificationException e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(QrECValidatorService)}] ValidateQrCodeForInternationalCertificate");
                return false;
            }
        }

        public async Task<IEnumerable<SubjectPublicKeyInfoDto>> GetKeyVaultKey(string id = null)
        {
            try
            {
                var jwkList = await _secureStorage.GetSecureStorageAsync(_settingsService.Jwk);

                if (jwkList?.Jwk != default)
                {
                    var ecKeys = JsonConvert
                        .DeserializeObject<IEnumerable<SubjectPublicKeyInfoDto>>(jwkList.Jwk);

                    return id == null ? ecKeys : ecKeys?.Where(x => 
                        x.Kid.Replace(" ", string.Empty) == id);
                }
            }

            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(QrECValidatorService)}] GetKeyVaultKey");
            }

            return default;
        }
    }
}
