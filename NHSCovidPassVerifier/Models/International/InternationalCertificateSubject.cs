using Newtonsoft.Json;

namespace NHSCovidPassVerifier.Models.International
{
    public class InternationalCertificateSubject
    {
        [JsonProperty("gn")]
        public string GivenName { get; set; }
        [JsonProperty("gnt")]
        public string GivenNameTransliterated { get; set; }
        [JsonProperty("fn")]
        public string FamilyName { get; set; }
        [JsonProperty("fnt")]
        public string FamilyNameTransliterated { get; set; }
    }
}