using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface ISecureStorageService<TValue>
    {
        /// <summary>
        /// Retrieves a value from SecureStorage.
        /// </summary>
        /// <param name="key">Key for finding a specific type of value in SecureStorage</param>
        /// <returns>The value stored with the given key</returns>
        Task<TValue> GetSecureStorageAsync(string key);

        /// <summary>
        /// Stores a value into SecureStorage.
        /// </summary>
        /// <param name="key">A key for finding the stored value</param>
        /// <param name="value">The value to store.</param>
        /// <returns>The Task status</returns>
        Task SetSecureStorageAsync(string key, TValue value);
        
        /// <summary>
        /// Deletes a value from SecureStorage.
        /// </summary>
        /// <param name="key">The key for the value to be deleted.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        Task<bool> Clear(string key);
    }
}
