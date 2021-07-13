using System.Collections.Generic;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Tests.MockServices
{
    public class MockSecureStorageService<T> : ISecureStorageService<T>
    {
        private readonly IDictionary<string, T> _mockSecureStorage = new Dictionary<string, T>();
        
        public Task<T> GetSecureStorageAsync(string key)
        {
            return Task.FromResult(_mockSecureStorage.TryGetValue(key, out var value) ? value : default);
        }

        public Task SetSecureStorageAsync(string key, T value)
        {
            _mockSecureStorage[key] = value;
            return Task.CompletedTask;
        }

        public Task<bool> Clear(string key)
        {
            return Task.FromResult(_mockSecureStorage.Remove(key));
        }
    }
}