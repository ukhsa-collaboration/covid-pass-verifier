using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    public class ApprovePageViewModelTests
    {
        private readonly ApprovePageViewModel _viewModel;

        public ApprovePageViewModelTests()
        {
            IoCContainer.RegisterSingleton<ISettingsService, MockSettingsService>();
            var mockLoggingService = new Mock<ILoggingService>().Object;
            _viewModel = new ApprovePageViewModel(mockLoggingService);
        }

        [Test]
        public void TextIsCorrect()
        {
            Assert.AreEqual(_viewModel.ApprovePageTitle, "APPROVE_TITLE".Translate());
            Assert.AreEqual(_viewModel.ApproveToCText, "APPROVE_TEXT".Translate());
            Assert.AreEqual(_viewModel.ToCLinkText, "APPROVE_TERMS_AND_CONDITIONS_TEXT".Translate());
            Assert.AreEqual(_viewModel.ApproveToCText_2, "APPROVE_TEXT_2".Translate());
            Assert.AreEqual(_viewModel.AcceptButtonText, "TERMS_AND_CONDITIONS_ACCEPT_TEXT".Translate());

        }

    }
}

