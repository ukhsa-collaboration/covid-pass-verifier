using System;
using System.Collections.Generic;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Models.International.Items;

namespace NHSCovidPassVerifier.Tests.TestData
{
    public static class CertificateData
    {
        public static DateTime refDate = DateTime.UtcNow;
        public static DomesticCertificate GetValidDomesticCertificate()
        {
            return new DomesticCertificate
            {
                FullName = "testy valid",
                Expiry = refDate.AddDays(3),
            };
        }
        public static DomesticCertificate GetExpiredDomesticCertificate()
        {
            return new DomesticCertificate
            {
                FullName = "testy valid",
                Expiry = refDate.AddDays(-3),
            };
        }

        public static InternationalCertificate GetValidInternationalCertificate()
        {
            var testVaccination1 = new InternationalCertificateVaccination()
            {
                CertificateId = "1",
                ProductCode = "1",
                Manufacturer = "Factory",
                DoseNumber = 1,
                TotalNumberOfDose = 2,
                DateOfVaccination = refDate.AddDays(-30),
                Country = "England",
                CertificateIssuer = "Whipps Cross",
                VaccineTypeCode = "AZ"    
            };
            var testSubject = new InternationalCertificateSubject()
            {
                GivenName = "Testy",
                FamilyName = "Valid"
            };

            var internationalSchema = new EuHcertV1Schema()
            {
                DateOfBirth = refDate.AddDays(-1900),
                InternationalCertificateSubject = testSubject,
                Vaccinations = new List<InternationalCertificateVaccination> { testVaccination1 }

            };

            return new InternationalCertificate()
            {
                DecodedModel = new InternationalCertificatePayload() { exp = 1620650094, hcert = new HCertModel() { euHcertV1Schema = internationalSchema } }
            };

        }
    }
}
