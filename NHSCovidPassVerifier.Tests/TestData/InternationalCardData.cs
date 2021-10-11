using System;
using System.Collections.Generic;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Models.International.Cards;
using NHSCovidPassVerifier.Models.International.Items;

namespace NHSCovidPassVerifier.Tests.TestData
{
    public static class InternationalCardData
    {
        public static DateTime refDate = DateTime.UtcNow;


        public static TestResultCard GetTestResultCard()
        {

            var testCertificate = new InternationalCertificateTestResult()
            {
                TestName = "PCR"


            };

            return new TestResultCard(testCertificate);

        }

        public static TestResultCard GetEmptyTestResultCard()
        {

            var testCertificate = new InternationalCertificateTestResult()
            {
                TestName = "PCR"

            };

            return new TestResultCard(testCertificate);

        }

        public static VaccinationCard GetVaccinationCard()
        {

            var testVaccinationCertificate = new InternationalCertificateVaccination
            {
                DiseaseTargeted = "COVID-19"

            };

            return new VaccinationCard(testVaccinationCertificate);

        }

        public static RecoveryCard GetRecoveryCard()
        {

            var testRecoveryCertificate = new InternationalCertificateRecovery
            {
                DiseaseTargeted = "COVID-19"

            };

            return new RecoveryCard(testRecoveryCertificate);

        }

    }

}
