using NHSCovidPassVerifier.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models;
using System.IO;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IJwkRepository
    {
        /// <summary>
        /// Attempts to get the Jwk token from the apps jwkurl
        /// </summary>
        /// <returns>An ApiResponse with the jwk contents as a Stream</returns>
        Task<ApiResponse<Stream>> GetJwk();
    }
}
