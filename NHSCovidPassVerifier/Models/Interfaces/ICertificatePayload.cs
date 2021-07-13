using NHSCovidPassVerifier.Enums;

namespace NHSCovidPassVerifier.Models.Interfaces
{
    public interface ICertificatePayload
    {
        public CertificateType GetCertificateType();
    }
}