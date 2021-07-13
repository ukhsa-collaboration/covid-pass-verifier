using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IJwkService
    {
        /// <summary>
        /// Attempts to get the jwk from the apps jwkurl.
        /// </summary>
        /// <returns>A NhsAppResult with the JwkDto or empty if it fails.</returns>
        Task<NhsAppResult<JwkDto>> GetJwk();

        /// <summary>
        /// Attempts to update the Jwk from the jwkurl, the jwk is saved to SecureStorage.
        /// </summary>
        /// <returns>boolean true if jwk was updated, false if it failed. </returns>
        Task<bool> UpdateJwkList();

        /// <summary>
        /// Checks if the jwk needs to be updated.
        /// </summary>
        /// <returns>true if jwk is older than the jwkvalidfor config setting, otherwise false.</returns>
        Task<bool> JwkListNeedsUpdating();

        /// <summary>
        /// Check if there is a jwk object in SecureStorage.
        /// </summary>
        /// <returns>true if jwkList is present, otherwise false.</returns>
        Task<bool> JwkListPresent();
    }
}
