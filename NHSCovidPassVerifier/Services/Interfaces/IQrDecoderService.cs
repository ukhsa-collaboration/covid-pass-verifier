using System.Threading.Tasks;
using NHSCovidPassVerifier.Models.Cose;
using NHSCovidPassVerifier.Models.Interfaces;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IQrDecoderService
    {
        /// <summary>
        /// Checks the validity of the qr code.
        /// </summary>
        /// <param name="qrCode">the raw contents of the qr code.</param>
        /// <returns>true if the code is valid, otherwise false.</returns>
        Task<bool> ValidateQrCode(string qrCode);

        /// <summary>
        /// Generates a Domestic of international qr code object, depending on the qr code.
        /// </summary>
        /// <returns>A Certificate which implements the ICertificate Interface. </returns>
        ICertificate GenerateCertificateFromQrCode();

        /// <summary>
        /// Retrieves a Generated Certificate.
        /// </summary>
        /// <returns>A Certificate which implements the ICertificate Interface.</returns>
        ICertificate GetDecodedCertificate();


        CoseSign1Object DecodeToCose(string base45String);
    }
}
