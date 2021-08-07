using System.Collections.Generic;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.TestData;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ServicesTests
{
    [TestFixture]
    public class CertificateMappingServiceTests
    {
        #region Setup
        private readonly CertificateMappingService _certificateMappingService;

        private readonly Mock<ICommonSettingsService> _commonSettingsServiceMock;

        private static readonly IEnumerable<string[]> VaccineManufacturerValidTestCases = new List<string[]>
        {
            new[] {"ORG-100001699", "AstraZeneca AB"},
            new[] {"ORG-100030215", "Biontech Manufacturing GmbH"},
            new[] {"ORG-100001417", "Janssen-Cilag International"},
            new[] {"ORG-100031184", "Moderna Biotech Spain S.L."},
            new[] {"ORG-100006270", "Curevac AG"},
            new[] {"ORG-100013793", "CanSino Biologics"},
            new[] {"ORG-100020693", "China Sinopharm International Corp. - Beijing location"},
            new[] {"ORG-100010771", "Sinopharm Weiqida Europe Pharmaceutical s.r.o. - Prague location"},
            new[] {"ORG-100024420", "Sinopharm Zhijun (Shenzhen) Pharmaceutical Co. Ltd. - Shenzhen location"},
            new[] {"ORG-100032020", "Novavax CZ AS"},
            new[] {"Gamaleya-Research-Institute", "Gamaleya Research Institute"},
            new[] {"Vector-Institute", "Vector Institute"},
            new[] {"Sinovac-Biotech", "Sinovac Biotech"},
            new[] {"Bharat-Biotech", "Bharat Biotech"},
            new[] {"ORG-100002068", "Valneva Sweden AB"},
        };

        private static readonly IEnumerable<string[]> DiseaseTargetedValidTestCases = new List<string[]>
        {
            new[] {"840539006", "COVID-19"},
        };
        
        private static readonly IEnumerable<string[]> VaccineNameValidTestCases = new List<string[]>
        {
            new[] {"EU/1/20/1528", "Comirnaty"},
            new[] {"EU/1/20/1507", "COVID-19 Vaccine Moderna"},
            new[] {"EU/1/21/1529", "Vaxzevria"},
            new[] {"EU/1/20/1525", "COVID-19 Vaccine Janssen"},
            new[] {"CVnCoV", "CVnCoV"},
            new[] {"NVX-CoV2373", "NVX-CoV2373"},
            new[] {"Sputnik-V", "Sputnik-V"},
            new[] {"Convidecia", "Convidecia"},
            new[] {"EpiVacCorona", "EpiVacCorona"},
            new[] {"BBIBP-CorV", "BBIBP-CorV"},
            new[] {"Inactivated-SARS-CoV-2-Vero-Cell", "Inactivated SARS-CoV-2 (Vero Cell)"},
            new[] {"CoronaVac", "CoronaVac"},
            new[] {"Covaxin", "Covaxin (also known as BBV152 A, B, C)"},
        };

        private static readonly IEnumerable<string[]> ReadableVaccineNameValidTestCases = new List<string[]>
        {
            new[] {"EU/1/20/1528", "Pfizer/BioNTech COVID-19 vaccine"},
            new[] {"EU/1/20/1507", "COVID-19 Vaccine Moderna"},
            new[] {"EU/1/21/1529", "COVID-19 Vaccine AstraZeneca"},
            new[] {"EU/1/20/1525", "COVID-19 Vaccine Janssen"},
            new[] {"CVnCoV", "CVnCoV"},
            new[] {"NVX-CoV2373", "NVX-CoV2373"},
            new[] {"Sputnik-V", "Sputnik-V"},
            new[] {"Convidecia", "Convidecia"},
            new[] {"EpiVacCorona", "EpiVacCorona"},
            new[] {"BBIBP-CorV", "BBIBP-CorV"},
            new[] {"Inactivated-SARS-CoV-2-Vero-Cell", "Inactivated SARS-CoV-2 (Vero Cell)"},
            new[] {"CoronaVac", "CoronaVac"},
            new[] {"Covaxin", "Covaxin (also known as BBV152 A, B, C)"},
        };
        
        private static readonly IEnumerable<string[]> TestTypeValidTestCases = new List<string[]>
        {
            new[] {"LP6464-4", "Nucleic acid amplification with probe detection"},
            new[] {"LP217198-3", "Rapid immunoassay"},
        };
        
        private static readonly IEnumerable<string[]> TestResultValidTestCases = new List<string[]>
        {
            new[] {"260415000", "Not detected"},
            new[] {"260373001", "Detected"},
        };

        private static readonly IEnumerable<string[]> TestManufacturerValidTestCases = new List<string[]>
        {
            new[] {"1833", "AAZ-LMB, COVID-VIRO"},
            new[] {"1232", "Abbott Rapid Diagnostics, Panbio COVID-19 Ag Rapid Test"}
        };

        private static readonly IEnumerable<string> UnmappedTestCases = new List<string>
        {
            "",
            "a",
            "0",
            "abc",
            "123",
            "test case",
            "null",
            "!",
            null,
        };

        public CertificateMappingServiceTests()
        {
            _commonSettingsServiceMock = CreateMockCommonSettingsService();
            
            _certificateMappingService = new CertificateMappingService(_commonSettingsServiceMock.Object);
        }

        private Mock<ICommonSettingsService> CreateMockCommonSettingsService()
        {
            var mock = new Mock<ICommonSettingsService>();
            mock.Setup(x => x.VaccineManufacturers)
                .Returns(CertificateMappingData.GetVaccineManufacturers());
            mock.Setup(x => x.VaccineTypes)
                .Returns(CertificateMappingData.GetVaccineTypes());
            mock.Setup(x => x.DiseasesTargeted)
                .Returns(CertificateMappingData.GetDiseasesTargeted());
            mock.Setup(x => x.VaccineNames)
                .Returns(CertificateMappingData.GetVaccineNames());
            mock.Setup(x => x.ReadableVaccineNames)
                .Returns(CertificateMappingData.GetReadableVaccineNames());
            mock.Setup(x => x.TestTypes)
                .Returns(CertificateMappingData.GetTestTypes());
            mock.Setup(x => x.TestResults)
                .Returns(CertificateMappingData.GetTestResults());
            mock.Setup(x => x.TestManufacturers)
                .Returns(CertificateMappingData.GetTestManufacturers());

            return mock;
        }
        #endregion

        #region VaccineManufacturerTests
        [TestCaseSource(nameof(VaccineManufacturerValidTestCases))]
        public void VaccineManufacturerInMappingReturnsCorrectlyMappedString(string s, string expected)
        {
            var actual = _certificateMappingService.GetVaccineManufacturer(s);
            Assert.AreEqual(expected, actual);
        }
        
        [TestCaseSource(nameof(UnmappedTestCases))]
        public void VaccineManufacturerNotInMappingReturnsSameString(string s)
        {
            var actual = _certificateMappingService.GetVaccineManufacturer(s);
            Assert.AreEqual(s, actual);
        }
        #endregion
        
        #region DiseaseTargetedTests
        [TestCaseSource(nameof(DiseaseTargetedValidTestCases))]
        public void DiseaseTargetedInMappingReturnsCorrectlyMappedString(string s, string expected)
        {
            var actual = _certificateMappingService.GetDiseaseTargeted(s);
            Assert.AreEqual(expected, actual);
        }
        
        [TestCaseSource(nameof(UnmappedTestCases))]
        public void DiseaseTargetedNotInMappingReturnsSameString(string s)
        {
            var actual = _certificateMappingService.GetDiseaseTargeted(s);
            Assert.AreEqual(s, actual);
        }
        #endregion
        
        #region VaccineNameTests
        [TestCaseSource(nameof(VaccineNameValidTestCases))]
        public void VaccineNameInMappingReturnsCorrectlyMappedString(string s, string expected)
        {
            var actual = _certificateMappingService.GetVaccineName(s);
            Assert.AreEqual(expected, actual);
        }
        
        [TestCaseSource(nameof(UnmappedTestCases))]
        public void VaccineNameNotInMappingReturnsSameString(string s)
        {
            var actual = _certificateMappingService.GetVaccineName(s);
            Assert.AreEqual(s, actual);
        }
        #endregion
        
        #region ReadableVaccineNameTests
        [TestCaseSource(nameof(ReadableVaccineNameValidTestCases))]
        public void ReadableVaccineNameInMappingReturnsCorrectlyMappedString(string s, string expected)
        {
            var actual = _certificateMappingService.GetReadableVaccineName(s);
            Assert.AreEqual(expected, actual);
        }
        
        [TestCaseSource(nameof(UnmappedTestCases))]
        public void ReadableVaccineNameNotInMappingReturnsSameString(string s)
        {
            var actual = _certificateMappingService.GetReadableVaccineName(s);
            Assert.AreEqual(s, actual);
        }
        #endregion
        
        #region TestTypeTests
        [TestCaseSource(nameof(TestTypeValidTestCases))]
        public void TestTypeInMappingReturnsCorrectlyMappedString(string s, string expected)
        {
            var actual = _certificateMappingService.GetTestType(s);
            Assert.AreEqual(expected, actual);
        }
        
        [TestCaseSource(nameof(UnmappedTestCases))]
        public void TestTypeNotInMappingReturnsSameString(string s)
        {
            var actual = _certificateMappingService.GetTestType(s);
            Assert.AreEqual(s, actual);
        }
        #endregion
        
        #region TestResultsTests
        [TestCaseSource(nameof(TestResultValidTestCases))]
        public void TestResultInMappingReturnsCorrectlyMappedString(string s, string expected)
        {
            var actual = _certificateMappingService.GetTestResult(s);
            Assert.AreEqual(expected, actual);
        }
        
        [TestCaseSource(nameof(UnmappedTestCases))]
        public void TestResultNotInMappingReturnsSameString(string s)
        {
            var actual = _certificateMappingService.GetTestResult(s);
            Assert.AreEqual(s, actual);
        }
        #endregion

        #region TestManufacturersTests

        [TestCaseSource(nameof(TestManufacturerValidTestCases))]
        public void TestManufacturersReturnsCorrectlyMappedString(string s, string expected)
        {
            var actual = _certificateMappingService.GetTestManufacturer(s);
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(UnmappedTestCases))]
        public void TestManufacturersNotInMappingReturnsSameString(string s)
        {
            var actual = _certificateMappingService.GetTestManufacturer(s);
            Assert.AreEqual(s, actual);
        }

        #endregion
    }
}