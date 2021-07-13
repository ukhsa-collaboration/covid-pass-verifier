using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models.Interfaces;

namespace NHSCovidPassVerifier.Models
{
    public class DomesticCertificatePayload : ICertificatePayload
    {
        public DomesticCertificatePayload(string payloadContent)
        {
            PayloadContent = payloadContent;
        }
        public string PayloadContent { get; }
        
        public CertificateType GetCertificateType()
        {
            return CertificateType.Domestic;
        }
    }
}