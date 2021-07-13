using NHSCovidPassVerifier.Enums;

namespace NHSCovidPassVerifier.Models.Interfaces
{
    public interface ICertificate
    {
        public CertificateType GetCertificateType();
    }
}