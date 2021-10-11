using System.Collections.Generic;
using System.Linq;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Services.Mocks
{
    public class MockCommonSettingsService : ICommonSettingsService
    {
        public IDictionary<string, string> VaccineManufacturers { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> VaccineTypes { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> DiseasesTargeted { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> VaccineNames { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> ReadableVaccineNames { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> TestTypes { get; } = new Dictionary<string, string>();
        public IDictionary<string, string> TestResults { get; } = new Dictionary<string, string>();
        public IEnumerable<string> EnglishCertificateIssuers { get; } = Enumerable.Empty<string>();
        public IDictionary<string, int> InternationalMinimumDoses { get; } = new Dictionary<string, int>();
    }
}