using System;
using System.Collections.Generic;
using NHSCovidPassVerifier.Models.International.Items;
using Newtonsoft.Json;

namespace NHSCovidPassVerifier.Models.International
{
    public class EuHcertV1Schema
    {
        [JsonProperty("v")]
        public List<InternationalCertificateVaccination> Vaccinations { get; set; }

        [JsonProperty("r")]
        public List<InternationalCertificateRecovery> Recovery { get; set; }
        
        [JsonProperty("t")]
        public List<InternationalCertificateTestResult> TestResults { get; set; }

        [JsonProperty("dob")]
        public DateTime? DateOfBirth { get; set; }
        [JsonProperty("nam")]
        public InternationalCertificateSubject InternationalCertificateSubject { get; set; }
        [JsonProperty("ver")]
        public string Version { get; set; }

    }

}