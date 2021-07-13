using System;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Models.International.Items;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using I18NPortable;

namespace NHSCovidPassVerifier.Models.International.Cards
{
    public class TestResultCard : IInternationalResultCard, IEquatable<TestResultCard>
    {
        private readonly DateTime? _sampleCollectedTime;
        private readonly DateTime? _resultProducedTime;

        public string DiseaseTargeted { get; }
        public string TestType { get; }
        public string TestName { get; }
        public string TestManufacturer { get; }
        public string SampleCollectedTimeText { get; }
        public string TestResult { get; }
        public string TestCentre { get; }
        public string CountryOfTest { get; }
        public string CertificateIssuer { get; }
        public string CertificateId { get; }
        public string ResultProducedTimeText { get; }

        public TestResultCard(InternationalCertificateTestResult t)
        {
            var certificateMappingService = IoCContainer.Resolve<ICertificateMappingService>();

            _sampleCollectedTime = t.SampleCollectedTime;

            DiseaseTargeted = certificateMappingService.GetDiseaseTargeted(t.DiseaseTargeted).SetDashIfNoValue();
            TestType = certificateMappingService.GetTestType(t.TestType).SetDashIfNoValue();
            TestName = t.TestName.SetDashIfNoValue();
            TestManufacturer = certificateMappingService.GetTestManufacturer(t.TestManufacturer).SetDashIfNoValue();
            SampleCollectedTimeText = _sampleCollectedTime.SetDashIfNoValue();
            TestResult = certificateMappingService.GetTestResult(t.TestResult).SetDashIfNoValue();
            TestCentre = t.TestCentre.SetDashIfNoValue();
            CountryOfTest = t.CountryOfTest.SetDashIfNoValue();
            CertificateIssuer = t.CertificateIssuer.SetDashIfNoValue();
            CertificateId = t.CertificateId.SetDashIfNoValue();
            ResultProducedTimeText = "INTERNATIONAL_SCANNER_RESULT_RESULT_PRODUCED_DATE".Translate(
                _resultProducedTime.HasValue
                    ? _resultProducedTime.Value.FormatDate()
                    : string.Empty);
        }

        public DateTime? GetSortByDate()
        {
            return _resultProducedTime;
        }

        public int CompareTo(IInternationalResultCard other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Nullable.Compare(GetSortByDate(), other.GetSortByDate());
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(this, obj)) return 0;
            if (ReferenceEquals(null, obj)) return 1;
            return obj is VaccinationCard other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(VaccinationCard)}");
        }

        public bool Equals(TestResultCard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DiseaseTargeted == other.DiseaseTargeted
                   && TestType == other.TestType
                   && TestName == other.TestName
                   && TestManufacturer == other.TestManufacturer
                   && SampleCollectedTimeText == other.SampleCollectedTimeText
                   && ResultProducedTimeText == other.ResultProducedTimeText
                   && TestResult == other.TestResult
                   && TestCentre == other.TestCentre
                   && CountryOfTest == other.CountryOfTest
                   && CertificateIssuer == other.CertificateIssuer
                   && CertificateId == other.CertificateId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TestResultCard)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DiseaseTargeted != null ? DiseaseTargeted.GetHashCode() : 0;
                hashCode = hashCode * 397 ^ (TestType != null ? TestType.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (TestName != null ? TestName.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (TestManufacturer != null ? TestManufacturer.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (SampleCollectedTimeText != null ? SampleCollectedTimeText.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (ResultProducedTimeText != null ? ResultProducedTimeText.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (TestResult != null ? TestResult.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (TestCentre != null ? TestCentre.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (CountryOfTest != null ? CountryOfTest.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (CertificateIssuer != null ? CertificateIssuer.GetHashCode() : 0);
                hashCode = hashCode * 397 ^ (CertificateId != null ? CertificateId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}