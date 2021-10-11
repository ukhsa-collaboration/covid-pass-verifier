using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using Moq;
using NUnit.Framework;
using NHSCovidPassVerifier.Models.International;
using System.Collections.ObjectModel;
using NHSCovidPassVerifier.Models.International.Cards;
using NHSCovidPassVerifier.Tests.TestData;
using NHSCovidPassVerifier.Tests.MockServices;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using NHSCovidPassVerifier.Services.Mocks;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    class ScannerResultInternationalCodeViewModelTests
    {
        private readonly ScannerResultInternationalCodeViewModel _viewModel;
        private readonly Mock<IQrDecoderService> _qrDecoderService;
        private readonly Mock<IJwkService> _jwkService;
        private readonly InternationalCertificate testCert;

        public ScannerResultInternationalCodeViewModelTests()
        {
            IoCContainer.RegisterInterface<ICommonSettingsService, MockCommonSettingsService>();
            _jwkService = new Mock<IJwkService>();
            _qrDecoderService = new Mock<IQrDecoderService>();
            IoCContainer.RegisterSingleton<ISettingsService, MockSettingsService>();
            _viewModel = new ScannerResultInternationalCodeViewModel(_jwkService.Object, _qrDecoderService.Object);
            testCert = CertificateData.GetValidInternationalCertificate();
        }

        [Test]
        public void TestInitialisedTextIsCorrect()
        {
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_TITLE".Translate(), _viewModel.TitleText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_VACCINE_PRODUCT_TEXT".Translate(), _viewModel.VaccineProductText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_CERTIFICATE_ID_TEXT".Translate(), _viewModel.CertificateIdText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_DISEASE_TARGETED_TEXT".Translate(), _viewModel.DiseaseTargetedText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_VACCINE_TEXT".Translate(), _viewModel.VaccineText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_COUNTRY_OF_VACCINATION_TEXT".Translate(), _viewModel.CountryVaccinatedText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_CERTIFICATE_ISSUER".Translate(), _viewModel.CertificateIssuerText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_MANUFACTURER_TEXT".Translate(), _viewModel.ManufacturerText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_PRODUCT_CODE_TEXT".Translate(), _viewModel.ProductCodeText);
            Assert.AreEqual("INTERNATIONAL_SCANNER_RESULT_DOSE_TEXT".Translate(), _viewModel.DoseLabelText);
        }


        [Test]
        public void TestPageDataSet()
        {
            _viewModel.SetPageData(testCert);
            var subject = testCert.DecodedModel.hcert.euHcertV1Schema.InternationalCertificateSubject;
            var expectedName = subject.GivenName + " " + subject.FamilyName;
            var dateOfBirth = testCert.DecodedModel.hcert.euHcertV1Schema.DateOfBirth;
            var expectedDateOfBirthText = "INTERNATIONAL_SCANNER_RESULT_DATE_OF_BIRTH".Translate(dateOfBirth?.FormatDate());
            var certificateExpirationDate = testCert.DecodedModel.exp.ConvertEpochToDate();
            var expectedCertificateExpirationText = "INTERNATIONAL_SCANNER_RESULT_CERTIFICATE_EXPIRATION".Translate(certificateExpirationDate?.FormatDate());
            var vaccinations = testCert.DecodedModel.hcert.euHcertV1Schema.Vaccinations;
            var expectedQrCodeResults = new ObservableCollection<VaccinationCard>(
                vaccinations.ConvertAll(x => new VaccinationCard(x))
            );
            Assert.AreEqual(expectedName, _viewModel.Name);
            Assert.AreEqual(expectedDateOfBirthText, _viewModel.DateOfBirthText);
            Assert.AreEqual(expectedCertificateExpirationText, _viewModel.CertificateExpirationText);
            Assert.AreEqual(expectedQrCodeResults,_viewModel.QrCodeResults);
        }

    }
}
