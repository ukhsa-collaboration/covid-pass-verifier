using System;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Models.International.Items;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using I18NPortable;

namespace NHSCovidPassVerifier.Models.International.Cards
{
    public class RecoveryCard : IInternationalResultCard, IEquatable<RecoveryCard>
    {
        private readonly DateTime? _dateOfFirstPositiveResult;
        private readonly DateTime? _dateValidFrom;
        private readonly DateTime? _dateValidUntil;

        public string DiseaseTargeted { get; }
        public string DateOfFirstPositiveResult { get; }
        public string CountryOfTest { get; }
        public string CertificateIssuer { get; }
        public string DateValidFromText { get; }
        public string DateValidUntil { get; }
        public string CertificateId { get; }

        public RecoveryCard(InternationalCertificateRecovery r)
        {
            var certificateMappingService = IoCContainer.Resolve<ICertificateMappingService>();

            _dateOfFirstPositiveResult = r.DateOfFirstPositiveResult;
            _dateValidFrom = r.DateValidFrom;
            _dateValidUntil = r.DateValidUntil;

            DiseaseTargeted = certificateMappingService.GetDiseaseTargeted(r.DiseaseTargeted).SetDashIfNoValue();
            DateOfFirstPositiveResult = _dateOfFirstPositiveResult.SetDashIfNoValue();
            CountryOfTest = r.CountryOfTest.SetDashIfNoValue();
            CertificateIssuer = r.CertificateIssuer.SetDashIfNoValue();
            DateValidUntil = _dateValidUntil.SetDashIfNoValue();
            CertificateId = r.CertificateId.SetDashIfNoValue();
            DateValidFromText = "INTERNATIONAL_SCANNER_RESULT_DATE_VALID_FROM".Translate(
                _dateValidFrom.HasValue
                    ? _dateValidFrom.Value.FormatDate()
                    : string.Empty);
        }

        public DateTime? GetSortByDate()
        {
            return _dateValidFrom;
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

        public bool Equals(RecoveryCard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DiseaseTargeted == other.DiseaseTargeted
                   && DateOfFirstPositiveResult == other.DateOfFirstPositiveResult
                   && CountryOfTest == other.CountryOfTest
                   && CertificateIssuer == other.CertificateIssuer
                   && DateValidFromText == other.DateValidFromText
                   && DateValidUntil == other.DateValidUntil
                   && CertificateId == other.CertificateId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RecoveryCard)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DiseaseTargeted != null ? DiseaseTargeted.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (DateOfFirstPositiveResult != null ? DateOfFirstPositiveResult.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CountryOfTest != null ? CountryOfTest.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CertificateIssuer != null ? CertificateIssuer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DateValidFromText != null ? DateValidFromText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DateValidUntil != null ? DateValidUntil.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CertificateId != null ? CertificateId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}