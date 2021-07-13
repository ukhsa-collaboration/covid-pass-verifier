using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    public class LandingViewModelTests
    {
        private readonly LandingViewModel _viewModel;
        private Mock<ISecureStorageService<TermsAndConditionsAgreed>> _secureStorageService;

        private const string ExpectedImageSource = "nhs_logo_white.png";

        public LandingViewModelTests()
        {
            _secureStorageService = new Mock<ISecureStorageService<TermsAndConditionsAgreed>>();
            _viewModel = new LandingViewModel(_secureStorageService.Object);
        }

        [Test]
        public void ImageIsInitialised()
        {
            Assert.AreEqual(ExpectedImageSource, _viewModel.LandingsPageImageSource);
        }

        [Test]
        public void TextIsCorrect()
        {
            Assert.AreEqual(_viewModel.OpenScannerText, "LANDING_PAGE_QR_CODE".Translate());
        }

        [Test]
        public void CommandIsInitialised()
        {
            Assert.IsNotNull(_viewModel.NextPageCommand);
        }
    }
}
