using System.Collections.Generic;
using System.Linq;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NHSCovidPassVerifier.Tests.TestData;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ServicesTests
{
    [TestFixture]
    public class CommonSettingsServiceTests
    {
        #region Setup
        private readonly CommonSettingsService _commonSettingsService;

        private readonly IConfigurationProvider _mockConfigurationProvider;

        private static readonly IDictionary<string, string> ExpectedVaccineManufacturers =
            CertificateMappingData.GetVaccineManufacturers();

        private static readonly IDictionary<string, string> ExpectedVaccineDiseasesTargeted =
            CertificateMappingData.GetDiseasesTargeted();

        private static readonly IDictionary<string, string> ExpectedVaccineNames =
            CertificateMappingData.GetVaccineNames();

        private static readonly IDictionary<string, string> ExpectedReadableVaccineNames =
            CertificateMappingData.GetReadableVaccineNames();

        private static readonly IDictionary<string, string> ExpectedTestTypes =
            CertificateMappingData.GetTestTypes();

        private static readonly IDictionary<string, string> ExpectedTestResults =
            CertificateMappingData.GetTestResults();

        public CommonSettingsServiceTests()
        {
            _mockConfigurationProvider = new MockConfigurationProvider();

            _commonSettingsService = new CommonSettingsService(_mockConfigurationProvider);
        }
        #endregion

        #region Helpers

        private static bool AreEqual<TK, TV>(IDictionary<TK, TV> o1, IDictionary<TK, TV> o2)
        {
            return o1.Count == o2.Count && !o1.Except(o2).Any();
        }

        #endregion

        [Test]
        public void VaccineManufacturersMappingIsAsExpected()
        {
            var actual = _commonSettingsService.VaccineManufacturers;
            var expected = ExpectedVaccineManufacturers;
            Assert.IsTrue(AreEqual(expected, actual));
        }

        [Test]
        public void DiseasesTargetedMappingIsAsExpected()
        {
            var actual = _commonSettingsService.DiseasesTargeted;
            var expected = ExpectedVaccineDiseasesTargeted;
            Assert.IsTrue(AreEqual(expected, actual));
        }

        [Test]
        public void VaccineNamesMappingIsAsExpected()
        {
            var actual = _commonSettingsService.VaccineNames;
            var expected = ExpectedVaccineNames;
            Assert.IsTrue(AreEqual(expected, actual));
        }

        [Test]
        public void ReadableVaccineNamesMappingIsAsExpected()
        {
            var actual = _commonSettingsService.ReadableVaccineNames;
            var expected = ExpectedReadableVaccineNames;
            Assert.IsTrue(AreEqual(expected, actual));
        }

        [Test]
        public void TestTypesMappingIsAsExpected()
        {
            var actual = _commonSettingsService.TestTypes;
            var expected = ExpectedTestTypes;
            Assert.IsTrue(AreEqual(expected, actual));
        }

        [Test]
        public void TestResultsMappingIsAsExpected()
        {
            var actual = _commonSettingsService.TestResults;
            var expected = ExpectedTestResults;
            Assert.IsTrue(AreEqual(expected, actual));
        }
    }
}