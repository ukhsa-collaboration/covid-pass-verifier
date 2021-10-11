using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NHSCovidPassVerifier.Tests.TestData;
using NHSCovidPassVerifier.ViewModels;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using Moq;
using NUnit.Framework;
using NHSCovidPassVerifier.Services.Mocks;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    public class ScannerResultViewModelTests
    {
        private readonly ScannerResultViewModel _viewModel;
        private readonly Mock<IQrDecoderService> _qrDecoderService;
        private readonly DomesticCertificate testCert;
        private readonly DomesticCertificate expiredTestCert;

        public ScannerResultViewModelTests()
        {
            _qrDecoderService = new Mock<IQrDecoderService>();
            testCert = CertificateData.GetValidDomesticCertificate();
            expiredTestCert = CertificateData.GetExpiredDomesticCertificate();
            IoCContainer.RegisterSingleton<ISettingsService, MockSettingsService>();
            IoCContainer.RegisterSingleton<ICommonSettingsService, MockCommonSettingsService>();
            _viewModel = new ScannerResultViewModel(_qrDecoderService.Object);
        }
        [SetUp]
        public void Setup()
        {
            _qrDecoderService.Setup(x => x.GetDecodedCertificate()).Returns(testCert);
        }


        [Test]
        public void TestSuccessTextIsCorrect()
        {
            _viewModel.InitializeAsync(true);
            Assert.AreEqual("VALID_HELP_TEXT".Translate(), _viewModel.ValidHelpText);
            Assert.AreEqual("NHS_COVID_STATUS".Translate(), _viewModel.CovidStatus);
            Assert.AreEqual("QR_PAUSE_TEXT".Translate(), _viewModel.PauseText);
            Assert.AreEqual("EXPIRED_TEXT".Translate(), _viewModel.ExpiredText);
        }

        [Test]
        public void CertificateValuesSetCorrectlyForValidPassport()
        {
            _viewModel.InitializeAsync(true);
            Assert.AreEqual(testCert.FullName, _viewModel.Name);
            Assert.AreEqual("Valid", _viewModel.ValidStatusText);
            Assert.AreEqual("VALID UNTIL", _viewModel.ExpiresText);
            Assert.AreEqual(testCert.Expiry.ToLocalTime().ToString("dd MMM yyy"), _viewModel.ExpiresDate);
            Assert.AreEqual(DateUtils.FormatTimeToMidnightMidday(testCert.Expiry.ToLocalTime()), _viewModel.ExpiresTime);
            Assert.AreEqual(false, _viewModel.ExpiredPassport);
            Assert.AreEqual(true, _viewModel.ValidPassport);
        }
        [Test]
        public void TestInvalidCertificate()
        {
            _viewModel.InitializeAsync(false);
            Assert.AreEqual("INVALID_QR".Translate(), _viewModel.InvalidCode);
            Assert.AreEqual(true, _viewModel.InvalidPassport);
        }
        [Test]
        public void TestExpiredCertificate()
        {
            _qrDecoderService.Setup(x => x.GetDecodedCertificate()).Returns(expiredTestCert);
            _viewModel.InitializeAsync(true);
            Assert.AreEqual(true, _viewModel.ExpiredPassport);
        }

        [Test]
        public void CheckTimerIsSet()
        {
            Assert.IsNotNull(_viewModel.TimerProgress);
        }
    }
}
