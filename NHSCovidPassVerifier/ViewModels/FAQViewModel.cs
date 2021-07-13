using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHSCovidPassVerifier.ViewModels
{
    public class FAQViewModel : BaseViewModel
    {
        public string commonQuestionsTitle { get; set; }
        public string InvalidQRCodeQuestion { get; private set; }
        public string InvalidQRCodeAnswer { get; set; }
        public string InternetQuestion { get; private set; }
        public string InternetAnswer { get; private set; }
        public string InternationalQuestion { get; private set; }
        public string InternationalAnswer { get; private set; }
        public string MinimumRequirementsQuestion { get; private set; }
        public string MinimumRequirementsAnswer { get; private set; }

        public FAQViewModel()
        {
            InitText();
        }

        private void InitText()
        {
            commonQuestionsTitle = "FAQ_COMMON_QUESTIONS_TITLE".Translate();
            InvalidQRCodeQuestion = "FAQ_INVALIDQR_QUESTION".Translate();
            InvalidQRCodeAnswer = "FAQ_INVALIDQR_ANSWER".Translate();
            InternetQuestion = "FAQ_INTERNET_QUESTION".Translate();
            InternetAnswer = "FAQ_INTERNET_ANSWER".Translate();
            InternationalQuestion = "FAQ_INTERNATIONAL_QUESTION".Translate();
            InternationalAnswer = "FAQ_INTERNATIONAL_ANSWER".Translate();
            MinimumRequirementsQuestion = "FAQ_MINIMUM_REQUIREMENT_QUESTION".Translate();
            MinimumRequirementsAnswer = "FAQ_MINIMUM_REQUIREMENT_ANSWER".Translate();
        }
    }
}
