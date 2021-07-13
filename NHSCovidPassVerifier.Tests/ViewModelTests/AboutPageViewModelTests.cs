using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    public class AboutPageViewModelTests
    {
        private readonly AboutPageViewModel _viewModel;
        private readonly ISettingsService _settingsService;
        private Mock<ISecureStorageService<TermsAndConditionsAgreed>> _secureStorageService;

        public AboutPageViewModelTests()
        {
            IoCContainer.RegisterSingleton<ISettingsService, MockSettingsService>();
            _secureStorageService = new Mock<ISecureStorageService<TermsAndConditionsAgreed>>();
            _settingsService = IoCContainer.Resolve<ISettingsService>();
            _viewModel = new AboutPageViewModel(_secureStorageService.Object);
        }

        [Test]
        public void TextIsInitializedCorrectly()
        {
            Assert.AreEqual(_viewModel.AboutPageTitle, "ABOUT_TITLE".Translate());
            Assert.AreEqual(_viewModel.HowToUseScannerTitle, "ABOUT_SUBTITLE_1".Translate());
            Assert.AreEqual(_viewModel.AboutPageParagraph1, "ABOUT_SUBTITLE_1_TEXT_1".Translate());
            Assert.AreEqual(_viewModel.AboutPageSubtitle2, "ABOUT_SUBTITLE_2".Translate());
            Assert.AreEqual(_viewModel.AboutDataText, "ABOUT_DATA_TEXT".Translate());
            Assert.AreEqual(_viewModel.AboutPageSubtitle3, "ABOUT_SUBTITLE_3".Translate());
            Assert.AreEqual(_viewModel.AboutPageSubtitle4, "ABOUT_SUBTITLE_4".Translate());
            Assert.AreEqual(_viewModel.AboutPageSubtitle5, "ABOUT_SUBTITLE_5".Translate());
            Assert.AreEqual(_viewModel.AboutAppVersion, "ABOUT_APP_VERSION".Translate());
            Assert.AreEqual(_viewModel.AboutAppVersionValue, _settingsService.GetVersionNumber());
            Assert.AreEqual(_viewModel.AboutHowThisAppWorksTitle, "ABOUT_HOW_THIS_APP_WORKS".Translate());
            Assert.AreEqual(_viewModel.AboutHowThisAppWorksParagraph, "ABOUT_HOW_THIS_APP_WORKS_PARAGRAPH".Translate());
            Assert.AreEqual(_viewModel.AboutInternationalTitle, "ABOUT_SUBTITLE_INTERNATIONAL".Translate());
            Assert.AreEqual(_viewModel.AboutInternationalParagraph, "ABOUT_INTERNATIONAL_PARAGRAPH".Translate());
            Assert.AreEqual(_viewModel.AboutDomesticTitle, "ABOUT_SUBTITLE_DOMESTIC".Translate());
            Assert.AreEqual(_viewModel.AboutDomesticParagraph, "ABOUT_DOMESTIC_PARAGRAPH".Translate());
        }

        [Test]
        public void CommandIsInitialised()
        {
            Assert.IsNotNull(_viewModel.AgreeCommand);
        }
    }
}
