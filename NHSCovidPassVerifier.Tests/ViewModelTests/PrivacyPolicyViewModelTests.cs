using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    public class PrivacyPolicyViewModelTests
    {
        private readonly PrivacyPolicyViewModel _viewModel;

        public PrivacyPolicyViewModelTests()
        {
            _viewModel = new PrivacyPolicyViewModel();
        }

        [Test]
        public void TextIsCorrect()
        {
            Assert.AreEqual(_viewModel.PrivacyPolicyTitle, "PRIVACY_TITLE".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText2, "PRIVACY_TEXT_2".Translate());
        
            Assert.AreEqual(_viewModel.PrivacyPolicySubtitle3, "PRIVACY_SUBTITLE_3".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText3, "PRIVACY_TEXT_3".Translate());

            Assert.AreEqual(_viewModel.PrivacyPolicySubtitle4, "PRIVACY_SUBTITLE_4".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText4, "PRIVACY_TEXT_4".Translate());

            Assert.AreEqual(_viewModel.PrivacyPolicySubtitle5, "PRIVACY_SUBTITLE_5".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText5, "PRIVACY_TEXT_5".Translate());

            Assert.AreEqual(_viewModel.PrivacyPolicySubtitle6, "PRIVACY_SUBTITLE_6".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText6, "PRIVACY_TEXT_6".Translate());

            Assert.AreEqual(_viewModel.PrivacyPolicySubtitle7, "PRIVACY_SUBTITLE_7".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText7, "PRIVACY_TEXT_7".Translate());

            Assert.AreEqual(_viewModel.PrivacyPolicySubtitle7_1, "PRIVACY_SUBTITLE_7_1".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText7_1, "PRIVACY_TEXT_7_1".Translate());

            Assert.AreEqual(_viewModel.PrivacyPolicySubtitle9, "PRIVACY_SUBTITLE_9".Translate());
            Assert.AreEqual(_viewModel.PrivacyPolicyText9, "PRIVACY_TEXT_9".Translate());

        }
    }
}

