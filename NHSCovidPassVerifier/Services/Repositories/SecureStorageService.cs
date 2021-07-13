using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NHSCovidPassVerifier.Services.Repositories
{
    public class SecureStorageService<TValue> : ISecureStorageService<TValue>
    {
        private readonly ILoggingService _loggingService;

        public SecureStorageService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async Task<TValue> GetSecureStorageAsync(string key)
        {
            try
            {
                var store = await SecureStorage.GetAsync(key);

                return store != default ? JsonConvert.DeserializeObject<TValue>(store) : default(TValue);
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(SecureStorage)}] Error Getting Key {key} in {nameof(GetSecureStorageAsync)}");

                throw;
            }
        }

        public async Task SetSecureStorageAsync(string key, TValue value)
        {
            try
            {
                await SecureStorage.SetAsync(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception e)
            {
                _loggingService.LogException(LogSeverity.ERROR, e, $"Error[{nameof(SecureStorage)}] Error Setting Key {key} in {nameof(SetSecureStorageAsync)}");

                throw;
            }
        }
        
        public async Task<bool> Clear(string key)
        {
            return await Task.FromResult<bool>(SecureStorage.Remove(key));
        }

    }
}