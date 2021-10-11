using System.Collections.Generic;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Services
{
    public class CertificateMappingService : ICertificateMappingService
    {
        private readonly ICommonSettingsService _commonSettingsService;

        private readonly IDictionary<string, string> _vaccineManufacturers;
        private readonly IDictionary<string, string> _vaccineTypes;
        private readonly IDictionary<string, string> _diseasesTargeted;
        private readonly IDictionary<string, string> _vaccineNames;
        private readonly IDictionary<string, string> _readableVaccineNames;
        private readonly IDictionary<string, string> _testTypes;
        private readonly IDictionary<string, string> _testResults;
        private readonly IDictionary<string, string> _testManufacturers;

        public CertificateMappingService(ICommonSettingsService commonSettingsService)
        {
            _commonSettingsService = commonSettingsService;

            _vaccineManufacturers = _commonSettingsService.VaccineManufacturers;
            _vaccineTypes = _commonSettingsService.VaccineTypes;
            _diseasesTargeted = _commonSettingsService.DiseasesTargeted;
            _vaccineNames = _commonSettingsService.VaccineNames;
            _readableVaccineNames = _commonSettingsService.ReadableVaccineNames;
            _testTypes = _commonSettingsService.TestTypes;
            _testResults = _commonSettingsService.TestResults;
            
        }
        
        public string GetManufacturer(string key)
        {
            return _vaccineManufacturers.TryGetValue(key ?? string.Empty, out var value) ? value : key;
        }

        public string GetVaccineType(string key)
        {
            return _vaccineTypes.TryGetValue(key ?? string.Empty, out var value) ? value : key;
        }

        public string GetDiseaseTargeted(string key)
        {
            return _diseasesTargeted.TryGetValue(key ?? string.Empty, out var value) ? value : key;
        }

        public string GetVaccineName(string productCode)
        {
            return _vaccineNames.TryGetValue(productCode ?? string.Empty, out var value) ? value : productCode;
        }

        public string GetReadableVaccineName(string productCode)
        {
            return _readableVaccineNames.TryGetValue(productCode ?? string.Empty, out var value)
                ? value
                : GetVaccineName(productCode);
        }

        public string GetTestType(string key)
        {
            return _testTypes.TryGetValue(key ?? string.Empty, out var value) ? value : key;
        }

        public string GetTestResult(string key)
        {
            return _testResults.TryGetValue(key ?? string.Empty, out var value) ? value : key;
        }

        public string GetTestManufacturer(string key)
        {
            return _testManufacturers.TryGetValue(key ?? string.Empty, out var value) ? value : key;
        }
    }
}