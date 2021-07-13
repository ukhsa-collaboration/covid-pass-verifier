using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    public class TermsAndConditionsViewModelTests
    {
        private readonly TermsAndConditionsViewModel _viewModel;

        public TermsAndConditionsViewModelTests()
        {
            _viewModel = new TermsAndConditionsViewModel();
        }

        [Test]
        public void TextIsCorrect()
        {
            Assert.AreEqual(_viewModel.AcceptButtonText, "TERMS_AND_CONDITIONS_ACCEPT_TEXT".Translate());
            Assert.AreEqual(_viewModel.DeclineButtonText, "TERMS_AND_CONDITIONS_DECLINE_TEXT".Translate());
            Assert.AreEqual(_viewModel.TermsTitle, "TERMS_AND_CONDITIONS_TITLE".Translate());
            Assert.AreEqual(_viewModel.TermsAgreeText, "TERMS_AND_CONDITIONS_AGREE".Translate());
            Assert.AreEqual(_viewModel.TermsText1, "TERMS_AND_CONDITIONS_1".Translate());
            Assert.AreEqual(_viewModel.TermsText1_1, "TERMS_AND_CONDITIONS_1_1".Translate());
            Assert.AreEqual(_viewModel.TermsText1_2, "TERMS_AND_CONDITIONS_1_2".Translate());
            Assert.AreEqual(_viewModel.TermsText2, "TERMS_AND_CONDITIONS_2".Translate());
            Assert.AreEqual(_viewModel.TermsText2_1, "TERMS_AND_CONDITIONS_2_1".Translate());
            Assert.AreEqual(_viewModel.TermsText2_2, "TERMS_AND_CONDITIONS_2_2".Translate());
            Assert.AreEqual(_viewModel.TermsText2_3, "TERMS_AND_CONDITIONS_2_3".Translate());
            Assert.AreEqual(_viewModel.TermsText3, "TERMS_AND_CONDITIONS_3".Translate()); 
            Assert.AreEqual(_viewModel.TermsText3_1, "TERMS_AND_CONDITIONS_3_1".Translate());
            Assert.AreEqual(_viewModel.TermsText3_2, "TERMS_AND_CONDITIONS_3_2".Translate());
            Assert.AreEqual(_viewModel.TermsText4, "TERMS_AND_CONDITIONS_4".Translate());
            Assert.AreEqual(_viewModel.TermsText4_1, "TERMS_AND_CONDITIONS_4_1".Translate()); 
            Assert.AreEqual(_viewModel.TermsText4_2, "TERMS_AND_CONDITIONS_4_2".Translate()); 
            Assert.AreEqual(_viewModel.TermsText5, "TERMS_AND_CONDITIONS_5".Translate());
            Assert.AreEqual(_viewModel.TermsText5_1, "TERMS_AND_CONDITIONS_5_1".Translate());
            Assert.AreEqual(_viewModel.TermsText5_2, "TERMS_AND_CONDITIONS_5_2".Translate());
            Assert.AreEqual(_viewModel.TermsText5_2_1, "TERMS_AND_CONDITIONS_5_2_1".Translate());
            Assert.AreEqual(_viewModel.TermsText5_2_2, "TERMS_AND_CONDITIONS_5_2_2".Translate());
            Assert.AreEqual(_viewModel.TermsText5_2_3, "TERMS_AND_CONDITIONS_5_2_3".Translate());
            Assert.AreEqual(_viewModel.TermsText5_2_4, "TERMS_AND_CONDITIONS_5_2_4".Translate());
            Assert.AreEqual(_viewModel.TermsText5_2_5, "TERMS_AND_CONDITIONS_5_2_5".Translate());
            Assert.AreEqual(_viewModel.TermsText5_3, "TERMS_AND_CONDITIONS_5_3".Translate());
            Assert.AreEqual(_viewModel.TermsText6, "TERMS_AND_CONDITIONS_6".Translate());
            Assert.AreEqual(_viewModel.TermsText6_1, "TERMS_AND_CONDITIONS_6_1".Translate());
            Assert.AreEqual(_viewModel.TermsText7, "TERMS_AND_CONDITIONS_7".Translate());
            Assert.AreEqual(_viewModel.TermsText7_1, "TERMS_AND_CONDITIONS_7_1".Translate());
            Assert.AreEqual(_viewModel.TermsText8, "TERMS_AND_CONDITIONS_8".Translate());
            Assert.AreEqual(_viewModel.TermsText8_1, "TERMS_AND_CONDITIONS_8_1".Translate());
            Assert.AreEqual(_viewModel.TermsText8_2, "TERMS_AND_CONDITIONS_8_2".Translate());
            Assert.AreEqual(_viewModel.TermsText8_3, "TERMS_AND_CONDITIONS_8_3".Translate());
            Assert.AreEqual(_viewModel.TermsText8_4, "TERMS_AND_CONDITIONS_8_4".Translate());
            Assert.AreEqual(_viewModel.TermsTextLastUpdated, "TERMS_AND_CONDITIONS_LAST_UPDATED".Translate());
        }
    }
}