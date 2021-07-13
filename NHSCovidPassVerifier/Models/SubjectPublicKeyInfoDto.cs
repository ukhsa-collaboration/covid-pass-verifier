using Newtonsoft.Json;

namespace NHSCovidPassVerifier.Models
{
    public class SubjectPublicKeyInfoDto
    {
        [JsonProperty("kid")]
        public string Kid { get; set; }
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }
    }
}
