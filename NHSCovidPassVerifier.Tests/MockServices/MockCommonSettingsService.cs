using System;
using NHSCovidPassVerifier.Services.Interfaces;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Crmf;

namespace NHSCovidPassVerifier.Tests.MockServices
{
    public class MockCommonSettingsService : ICommonSettingsService
    {
        private readonly string TestKey = "testKey";
        public IDictionary<string, string> VaccineManufacturers => GetDictionary<string, string>(TestKey);

        public IDictionary<string, string> VaccineTypes => GetDictionary<string, string>(TestKey);

        public IDictionary<string, string> DiseasesTargeted => GetDictionary<string, string>(TestKey);

        public IDictionary<string, string> VaccineNames => GetDictionary<string, string>(TestKey);

        public IDictionary<string, string> ReadableVaccineNames => GetDictionary<string, string>(TestKey);

        public IDictionary<string, string> TestTypes => GetDictionary<string, string>(TestKey);

        public IDictionary<string, string> TestResults => GetDictionary<string, string>(TestKey);
        public IDictionary<string, string> TestManufacturers =>
            GetDictionary<string, string>(nameof(TestKey));

        private IDictionary<TKey, TValue> GetDictionary<TKey, TValue>(string key)
        {
            JToken value = JObject.Parse(
                "{ \"testKey\": \"testValue\"}"
            );
            if (value == null)
            {
                throw new InvalidOperationException($"Key '{key}' does not exist in common settings file.");
            }
            return value.ToObject<IDictionary<TKey, TValue>>();
        }
    }
}
