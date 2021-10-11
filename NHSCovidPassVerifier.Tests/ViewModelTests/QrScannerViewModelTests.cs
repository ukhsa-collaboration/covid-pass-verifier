using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    class QrScannerViewModelTests
    {
        private readonly QRScannerViewModel _viewModel;

        private readonly IQrDecoderService _qrDecoderService;
        private readonly ILoggingService _loggingService;
        private readonly IJwkService _jwkService;
        private readonly IInternationalEnabledService _internationalEnabledService;
        private readonly ICommonSettingsService _commonSettingsService;

        public QrScannerViewModelTests()
        {
            _qrDecoderService = new Mock<IQrDecoderService>().Object;
            _loggingService = new Mock<ILoggingService>().Object;
            _jwkService = new Mock<IJwkService>().Object;
            _internationalEnabledService = new Mock<IInternationalEnabledService>().Object;
            _commonSettingsService = new Mock<ICommonSettingsService>().Object;

            _viewModel = new QRScannerViewModel(_qrDecoderService, _loggingService, _jwkService, _internationalEnabledService, _commonSettingsService);
        }

        [Test]
        public void TestTextIsCorrect()
        {
            Assert.AreEqual("NHS_COVID_STATUS".Translate(), this._viewModel.CovidStatus);
        }

        [Test]
        public void TestCommandsHaveBeenInitialised()
        {
            Assert.IsNotNull(this._viewModel.BackCommand);
        }
    }
}
