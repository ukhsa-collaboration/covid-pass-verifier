using System.Collections.Generic;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Cose;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IQrValidatorService
    {
        /// <summary>
        /// Checks the validity of a Domestic qr code.
        /// </summary>
        /// <param name="digest"></param>
        /// <param name="signature"></param>
        /// <param name="keyId">key for finding the correct public key.</param>
        /// <returns></returns>
        public Task<bool> ValidateQrCodeForDomesticCertificate(byte[] digest, byte[] signature, string keyId);

        /// <summary>
        ///  Checks the validity of a International qr code.
        /// </summary>
        /// <param name="coseSign1Object"></param>
        /// <returns></returns>
        public Task<bool> ValidateQrCodeForInternationalCertificate(CoseSign1Object coseSign1Object);

        /// <summary>
        /// Gets an IEnumerable of public keys for checking qr codes.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<SubjectPublicKeyInfoDto>> GetKeyVaultKey(string id = null);
    }
}
