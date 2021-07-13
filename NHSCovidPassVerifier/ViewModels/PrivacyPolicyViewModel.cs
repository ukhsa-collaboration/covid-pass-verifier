using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;

namespace NHSCovidPassVerifier.ViewModels
{
    public class PrivacyPolicyViewModel : BaseViewModel
    {
        public string PrivacyPolicyText2 { get; set; }
        public string PrivacyPolicyTitle { get; set; }

        public string PrivacyPolicySubtitle3 { get; set; }
        public string PrivacyPolicyText3 { get; set; }
        public string PrivacyPolicySubtitle4 { get; set; }
        public string PrivacyPolicyText4 { get; set; }
        public string PrivacyPolicySubtitle5 { get; set; }
        public string PrivacyPolicyText5 { get; set; }
        public string PrivacyPolicySubtitle6 { get; set; }
        public string PrivacyPolicyText6 { get; set; }
        public string PrivacyPolicySubtitle7 { get; set; }
        public string PrivacyPolicyText7 { get; set; }
        public string PrivacyPolicySubtitle7_1 { get; private set; }
        public string PrivacyPolicyText7_1 { get; private set; }
        public string PrivacyPolicySubtitle9 { get; set; }
        public string PrivacyPolicyText9 { get; set; }


        public PrivacyPolicyViewModel()
        {
            InitText();
        }

        

        private void InitText()
        {
            PrivacyPolicyTitle = "PRIVACY_TITLE".Translate();
            PrivacyPolicyText2 = "PRIVACY_TEXT_2".Translate();

            PrivacyPolicySubtitle3 = "PRIVACY_SUBTITLE_3".Translate();
            PrivacyPolicyText3 = "PRIVACY_TEXT_3".Translate();

            PrivacyPolicySubtitle4 = "PRIVACY_SUBTITLE_4".Translate();
            PrivacyPolicyText4 = "PRIVACY_TEXT_4".Translate();

            PrivacyPolicySubtitle5 = "PRIVACY_SUBTITLE_5".Translate();
            PrivacyPolicyText5 = "PRIVACY_TEXT_5".Translate();

            PrivacyPolicySubtitle6 = "PRIVACY_SUBTITLE_6".Translate();
            PrivacyPolicyText6 = "PRIVACY_TEXT_6".Translate();

            PrivacyPolicySubtitle7 = "PRIVACY_SUBTITLE_7".Translate();
            PrivacyPolicyText7 = "PRIVACY_TEXT_7".Translate();

            PrivacyPolicySubtitle7_1 = "PRIVACY_SUBTITLE_7_1".Translate();
            PrivacyPolicyText7_1 = "PRIVACY_TEXT_7_1".Translate();

            PrivacyPolicySubtitle9 = "PRIVACY_SUBTITLE_9".Translate();
            PrivacyPolicyText9 = "PRIVACY_TEXT_9".Translate();
        }
    }
}
