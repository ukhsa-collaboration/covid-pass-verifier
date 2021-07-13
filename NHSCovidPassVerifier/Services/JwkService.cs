using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Dtos;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Services
{
    public class JwkService : IJwkService
    {
        private readonly IJwkRepository _jwkRepository;
        private readonly ILoggingService _loggingService;
        private readonly ISecureStorageService<JwkDto> _secureStorage;
        private readonly ISettingsService _settingsService;


        public JwkService(IJwkRepository jwkRepository, ILoggingService loggingService, ISecureStorageService<JwkDto> secureStorage,
            ISettingsService settingsService)
        {
            _jwkRepository = jwkRepository;
            _loggingService = loggingService;
            _secureStorage = secureStorage;
            _settingsService = settingsService;
        }

        public async Task<NhsAppResult<JwkDto>> GetJwk()
        {
            var result = new NhsAppResult<JwkDto>();
            try
            {
                var response = await _jwkRepository.GetJwk();

                result.SetState((int)response.StatusCode);
                result.Data = new JwkDto
                {
                    Jwk = StreamToJwk(response.Data),
                    TimeReceived = DateTime.UtcNow
                };

                if (result.State != NhsPageState.Success)
                    _loggingService.LogApiError<Stream>(LogSeverity.ERROR, response, $"Error[{nameof(JwkService)}] GetJwk");

            }
            catch (Exception e)
            {
                result.SetState(500);
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(JwkService)}] GetJwk");
            }
            return result;
        }

        public async Task<bool> JwkListNeedsUpdating()
        {
            try
            {
                var jwkDto = await _secureStorage.GetSecureStorageAsync(_settingsService.Jwk);

                if (jwkDto == default)
                    return true;

                return jwkDto.TimeReceived.AddHours(_settingsService.JwkValidForHours) < DateTime.UtcNow;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(JwkService)}] JwkListNeedsUpdating");
                return true;
            }
        }

        public async Task<bool> JwkListPresent()
        {
            try
            {
                var jwkDto = await _secureStorage.GetSecureStorageAsync(_settingsService.Jwk);
                return jwkDto.Jwk != null;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(JwkService)}] JwkListPresent");
                return false;
            }
        }

        public async Task<bool> UpdateJwkList()
        {
            var result = await UpdateJwkListOnline();
            return result ? result : await UpdateJwkListLocally();
        }

        private async Task<bool> UpdateJwkListOnline()
        {
            try
            {
                var response = await GetJwk();

                if (!response.State.Equals(NhsPageState.Success))
                {
                    return false;
                }

                await _secureStorage.Clear(_settingsService.Jwk);
                await _secureStorage.SetSecureStorageAsync(_settingsService.Jwk, response.Data);

                _loggingService.LogMessage(LogSeverity.INFO, "PKI Updated");

                return true;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(JwkService)}] UpdateJWKList");
                return false;
            }
        }

        private async Task<bool> UpdateJwkListLocally()
        {
            try
            {
                var data = new JwkDto
                {
                    Jwk = _settingsService.PublicKeys.ToString(Formatting.None),
                    TimeReceived = DateTime.UtcNow
                };

                await _secureStorage.Clear(_settingsService.Jwk);
                await _secureStorage.SetSecureStorageAsync(_settingsService.Jwk, data);

                _loggingService.LogMessage(LogSeverity.INFO, "PKI Updated Locally");

                return true;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(JwkService)}] UpdateJWKListLocally");
                return false;
            }
        }

        private string StreamToJwk(Stream stream)
        {
            string jwk;
            if (stream == null)
                return string.Empty;
            using var reader = new StreamReader(stream);
            jwk = reader.ReadToEnd();
            return jwk;
        }
        private bool IsValidEcKey(string jwk)
        {
            try
            {
                var parsed = JArray.Parse(jwk);
                if (parsed.First?["id"] != null
                    && parsed.First["x"] != null
                    && parsed.First["y"] != null)
                {
                    return true;
                }

                _loggingService.LogMessage(LogSeverity.ERROR, $"Invalid Jwk found from {_settingsService.GetJwkUrl}");
                return false;
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(JwkService)}] IsValidEcKey");
                return false;
            }
        }
    }
}
