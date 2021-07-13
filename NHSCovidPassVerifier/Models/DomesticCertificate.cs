using NHSCovidPassVerifier.Enums;
using System;
using NHSCovidPassVerifier.Models.Interfaces;

namespace NHSCovidPassVerifier.Models
{
    public class DomesticCertificate : ICertificate
    {
        public string FullName { get; set; }

        public DateTime Expiry { get; set; }

        public CertificateState CertificateState => GetStatus();
        
        public CertificateType GetCertificateType()
        {
            return CertificateType.Domestic;
        }

        private CertificateState GetStatus()
        {
            return Expiry >= DateTime.UtcNow ? CertificateState.Valid : CertificateState.Invalid;
        }
    }
}
