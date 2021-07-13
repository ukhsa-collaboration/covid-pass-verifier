using System;
using Newtonsoft.Json;

namespace NHSCovidPassVerifier.Models.International.Items
{
    /// <summary>
    /// Dto for a vaccination_entry in EU DCC schema.
    /// "lot" does not exist in the schema and has been added.
    /// </summary>
    public class InternationalCertificateVaccination
    {
        [JsonProperty("tg")]
        public string DiseaseTargeted { get; set; }

        [JsonProperty("vp")]
        public string VaccineTypeCode { get; set; }

        [JsonProperty("mp")]
        public string ProductCode { get; set; }

        [JsonProperty("ma")]
        public string Manufacturer { get; set; }

        [JsonProperty("dn")]
        public int DoseNumber { get; set; }
        
        [JsonProperty("sd")]
        public int TotalNumberOfDose { get; set; }
        
        [JsonProperty("dt")]
        public DateTime? DateOfVaccination { get; set; }
        
        [JsonProperty("co")]
        public string Country { get; set; }
        
        [JsonProperty("is")]
        public string CertificateIssuer { get; set; }

        [JsonProperty("ci")]
        public string CertificateId { get; set; }

        [JsonProperty("lot")]
        public string BatchNumber { get; set; }
    }
}