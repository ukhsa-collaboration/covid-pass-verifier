using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IRestClient
    {
        /// <summary>
        /// Gets a file stream asynchronously.
        /// </summary>
        /// <param name="url">url to get file from</param>
        /// <returns>An Apiresponse with the file stream.</returns>
        Task<ApiResponse<Stream>> GetFileAsStreamAsync(string url);
    }
}