using System;
using System.Collections.Generic;
using System.IO;
using NHSCovidPassVerifier.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace NHSCovidPassVerifier.Services
{
    public class CommonSettingsService : ICommonSettingsService
    {
        private readonly JObject _commonSettings;

        public CommonSettingsService() { }
        
        public CommonSettingsService(IConfigurationProvider configurationProvider)
        {
            using var reader = new StreamReader(configurationProvider.GetCommonConfiguration());

            var json = reader.ReadToEnd();

            _commonSettings = JObject.Parse(json);
        }
        
        public IDictionary<string, string> VaccineManufacturers => GetDictionary<string, string>(nameof(VaccineManufacturers));
        public IDictionary<string, string> VaccineTypes => GetDictionary<string, string>(nameof(VaccineTypes));
        public IDictionary<string, string> DiseasesTargeted => GetDictionary<string, string>(nameof(DiseasesTargeted));
        public IDictionary<string, string> VaccineNames => GetDictionary<string, string>(nameof(VaccineNames));
        public IDictionary<string, string> ReadableVaccineNames => GetDictionary<string, string>(nameof(ReadableVaccineNames));
        public IDictionary<string, string> TestTypes => GetDictionary<string, string>(nameof(TestTypes));
        public IDictionary<string, string> TestResults => GetDictionary<string, string>(nameof(TestResults));

        public IDictionary<string, string> TestManufacturers =>
            GetDictionary<string, string>(nameof(TestManufacturers));

        private IDictionary<TKey, TValue> GetDictionary<TKey, TValue>(string key)
        {
            var value = _commonSettings.SelectToken(key);
            
            if (value == null)
            {
                throw new InvalidOperationException($"Key '{key}' does not exist in common settings file.");
            }
            
            return value.ToObject<IDictionary<TKey, TValue>>();
        }
    }
}