using System;
using Newtonsoft.Json;

namespace NHSCovidPassVerifier.Models.International.Items
{
    public class InternationalCertificateRecovery
    {
        [JsonProperty("tg")]
        public string DiseaseTargeted { get; set; }
        
        [JsonProperty("fr")]
        public DateTime? DateOfFirstPositiveResult { get; set; }
        
        [JsonProperty("co")]
        public string CountryOfTest { get; set; }
        
        [JsonProperty("is")]
        public string CertificateIssuer { get; set; }
        
        [JsonProperty("df")]
        public DateTime? DateValidFrom { get; set; }
        
        [JsonProperty("du")]
        public DateTime? DateValidUntil { get; set; }
        
        [JsonProperty("ci")]
        public string CertificateId { get; set; }
    }
}