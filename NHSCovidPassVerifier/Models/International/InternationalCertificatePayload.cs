using System;
using NHSCovidPassVerifier.Controls.Converters;
using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models.Interfaces;
using Newtonsoft.Json;

namespace NHSCovidPassVerifier.Models.International
{
    public class InternationalCertificatePayload : ICertificatePayload
    {
        [JsonProperty("1")]
        public string iss { get; set; }

        [JsonProperty("6")]
        public int iat { get; set; }

        [JsonProperty("4")]
        public int exp { get; set; }

        [JsonProperty("-260")]
        public HCertModel hcert { get; set; }

        public CertificateType GetCertificateType()
        {
            return CertificateType.International;
        }
    }
    
    public class HCertModel
    {
        [JsonProperty("1")]
        public EuHcertV1Schema euHcertV1Schema;

    }
}