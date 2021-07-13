using System;
using Newtonsoft.Json;

namespace NHSCovidPassVerifier.Models.International.Items
{
    /// <summary>
    /// Dto for a test_entry in the EU DCC schema.
    /// </summary>
    public class InternationalCertificateTestResult
    {
        [JsonProperty("tg")]
        public string DiseaseTargeted { get; set; }
        
        [JsonProperty("tt")]
        public string TestType { get; set; }

        [JsonProperty("nm")]
        public string TestName { get; set; }

        [JsonProperty("ma")]
        public string TestManufacturer { get; set; }

        [JsonProperty("sc")]
        public DateTime? SampleCollectedTime { get; set; }

        [JsonProperty("tr")]
        public string TestResult { get; set; }

        [JsonProperty("tc")]
        public string TestCentre { get; set; }

        [JsonProperty("co")]
        public string CountryOfTest { get; set; }

        [JsonProperty("is")]
        public string CertificateIssuer { get; set; }

        [JsonProperty("ci")]
        public string CertificateId { get; set; }
    }
}