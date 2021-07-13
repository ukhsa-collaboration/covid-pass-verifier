using System;

namespace NHSCovidPassVerifier.Models.Dtos
{
    public class JwkDto
    {
        public string Jwk { get; set; }

        public DateTime TimeReceived { get; set; }
    }
}
