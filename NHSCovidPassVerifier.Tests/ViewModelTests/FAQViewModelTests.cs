using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    public class FAQViewModelTests
    {
        private readonly FAQViewModel _viewModel;
        public FAQViewModelTests()
        {
            _viewModel = new FAQViewModel();
        }
        
        [Test]
        public void TextIsInitializedCorrectly()
        {
            Assert.AreEqual("FAQ_COMMON_QUESTIONS_TITLE".Translate(), _viewModel.commonQuestionsTitle);
            Assert.AreEqual("FAQ_INVALIDQR_QUESTION".Translate(), _viewModel.InvalidQRCodeQuestion);
            Assert.AreEqual("FAQ_INVALIDQR_ANSWER".Translate(), _viewModel.InvalidQRCodeAnswer);
            Assert.AreEqual("FAQ_INTERNET_QUESTION".Translate(), _viewModel.InternetQuestion);
            Assert.AreEqual("FAQ_INTERNET_ANSWER".Translate(), _viewModel.InternetAnswer);
            Assert.AreEqual("FAQ_INTERNATIONAL_QUESTION".Translate(), _viewModel.InternationalQuestion);
            Assert.AreEqual("FAQ_INTERNATIONAL_ANSWER".Translate(), _viewModel.InternationalAnswer);
            Assert.AreEqual("FAQ_MINIMUM_REQUIREMENT_QUESTION".Translate(), _viewModel.MinimumRequirementsQuestion);
            Assert.AreEqual("FAQ_MINIMUM_REQUIREMENT_ANSWER".Translate(), _viewModel.MinimumRequirementsAnswer);
        }
    }
}
