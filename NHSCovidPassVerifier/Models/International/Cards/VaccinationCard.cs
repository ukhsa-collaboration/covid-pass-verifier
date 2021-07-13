using System;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Models.International.Items;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using I18NPortable;

namespace NHSCovidPassVerifier.Models.International.Cards
{
    public class VaccinationCard : IEquatable<VaccinationCard>, IInternationalResultCard
    {
        private readonly DateTime? _dateOfVaccination;

        public string VaccineName { get; }
        public string DoseText { get; }
        public string DoseNumber { get; }
        public string VaccineProduct { get; }
        public string BatchNumber { get; }
        public string CertificateId { get; }
        public string DiseaseTargeted { get; }
        public string Vaccine { get; }
        public string CountryOfVaccination { get; }
        public string CertificateIssuer { get; }
        public string DateOfVaccinationText { get; }
        public string Manufacturer { get; }
        public string ProductCode { get; }

        public VaccinationCard(InternationalCertificateVaccination v)
        {
            var certificateMappingService = IoCContainer.Resolve<ICertificateMappingService>();

            _dateOfVaccination = v.DateOfVaccination;

            VaccineName = certificateMappingService.GetReadableVaccineName(v.ProductCode).SetDashIfNoValue();
            DoseText =
                $"{"INTERNATIONAL_SCANNER_RESULT_DOSE_TEXT".Translate()} {v.DoseNumber} {"INTERNATIONAL_SCANNER_RESULT_OF_TEXT".Translate()} {v.TotalNumberOfDose}";
            DoseNumber = v.DoseNumber.ToString().SetDashIfNoValue();
            VaccineProduct = certificateMappingService.GetVaccineName(v.ProductCode).SetDashIfNoValue();
            BatchNumber = v.BatchNumber.SetDashIfNoValue();
            CertificateId = v.CertificateId.SetDashIfNoValue();
            ProductCode = v.ProductCode.SetDashIfNoValue();
            DiseaseTargeted = certificateMappingService.GetDiseaseTargeted(v.DiseaseTargeted).SetDashIfNoValue();
            Vaccine = certificateMappingService.GetVaccineType(v.VaccineTypeCode).SetDashIfNoValue();
            CountryOfVaccination = v.Country.SetDashIfNoValue();
            Manufacturer = certificateMappingService.GetTestManufacturer(v.Manufacturer).SetDashIfNoValue();
            CertificateIssuer = v.CertificateIssuer.SetDashIfNoValue();
            DateOfVaccinationText = "INTERNATIONAL_SCANNER_RESULT_DATE_OF_VACCINATION".Translate(
                _dateOfVaccination.HasValue
                    ? _dateOfVaccination.Value.FormatDate()
                    : string.Empty);
        }

        public DateTime? GetSortByDate()
        {
            return _dateOfVaccination;
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

        public bool Equals(VaccinationCard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return VaccineName == other.VaccineName
                   && DoseText == other.DoseText
                   && DoseNumber == other.DoseNumber
                   && VaccineProduct == other.VaccineProduct
                   && BatchNumber == other.BatchNumber
                   && CertificateId == other.CertificateId
                   && DiseaseTargeted == other.DiseaseTargeted
                   && Vaccine == other.Vaccine
                   && CountryOfVaccination == other.CountryOfVaccination
                   && CertificateIssuer == other.CertificateIssuer
                   && DateOfVaccinationText == other.DateOfVaccinationText
                   && Manufacturer == other.Manufacturer
                   && ProductCode == other.ProductCode;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((VaccinationCard)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = VaccineName != null ? VaccineName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (DoseText != null ? DoseText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DoseNumber != null ? DoseNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (VaccineProduct != null ? VaccineProduct.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BatchNumber != null ? BatchNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CertificateId != null ? CertificateId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DiseaseTargeted != null ? DiseaseTargeted.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Vaccine != null ? Vaccine.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CountryOfVaccination != null ? CountryOfVaccination.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CertificateIssuer != null ? CertificateIssuer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DateOfVaccinationText != null ? DateOfVaccinationText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Manufacturer != null ? Manufacturer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProductCode != null ? ProductCode.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}