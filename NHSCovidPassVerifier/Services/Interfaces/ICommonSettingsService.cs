using System.Collections.Generic;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface ICommonSettingsService
    {
        IDictionary<string, string> VaccineManufacturers { get; }
        IDictionary<string, string> VaccineTypes { get; }
        IDictionary<string, string> DiseasesTargeted { get; }
        IDictionary<string, string> VaccineNames { get; }
        IDictionary<string, string> ReadableVaccineNames { get; }
        IDictionary<string, string> TestTypes { get; }
        IDictionary<string, string> TestResults { get; }
        IEnumerable<string> EnglishCertificateIssuers { get; }
        IDictionary<string, int> InternationalMinimumDoses { get; }
    }
}