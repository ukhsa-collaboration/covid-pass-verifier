using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models.Interfaces;

namespace NHSCovidPassVerifier.Models.International
{
    public class InternationalCertificate : ICertificate
    {
        public InternationalCertificatePayload DecodedModel { get; set; }
        public CertificateType GetCertificateType()
        {
            return CertificateType.International;
        }
    }
}