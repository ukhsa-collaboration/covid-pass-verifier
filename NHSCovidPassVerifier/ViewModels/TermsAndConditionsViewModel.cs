using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels.Base;
using NHSCovidPassVerifier.Views;
using I18NPortable;
using System.Threading.Tasks;
using System.Windows.Input;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.ViewModels
{
    public class TermsAndConditionsViewModel : BaseViewModel
    {
        public string AcceptButtonText { get; set; }
        public string DeclineButtonText { get; set; }
        public object TermsTitle { get; set; }
        public object TermsText1_1 { get; set; }
        public object TermsText1_2 { get; set; }
        public bool ShowButtons { get; set; } = true;
        public string TermsAgreeText { get; set; }
        public string TermsText1 { get; set; }
        public string TermsText2 { get; set; }
        public string TermsText2_1 { get; set; }
        public string TermsText2_2 { get; set; }
        public string TermsText2_3 { get; set; }
        public string TermsText2_4 { get; set; }
        public string TermsText3 { get; set; }
        public string TermsText3_1 { get; set; }
        public string TermsText3_1_P2 { get; set; }
        public string TermsText3_2 { get; set; }
        public string TermsText3_3 { get; set; }
        public string TermsText3_4 { get; set; }
        public string TermsText3_5 { get; set; }
        public string TermsText3_6 { get; set; }
        public string TermsText4 { get; set; }
        public string TermsText4_1 { get; set; }
        public string TermsText4_2 { get; set; }
        public string TermsText5 { get; set; }
        public string TermsText5_1 { get; set; }
        public string TermsText5_2 { get; set; }
        public string TermsText5_2_1 { get; set; }
        public string TermsText5_2_2 { get; set; }
        public string TermsText5_2_3 { get; set; }
        public string TermsText5_2_4 { get; set; }
        public string TermsPurposeOfServiceText { get; set; }
        public string TermsPurposeOfEmployeeText { get; set; }
        public string TermsPurposeOfFreeText { get; set; }
        public string TermsText7_1 { get; set; }
        public string TermsText7_1_Email { get; set; }
        public string TermsText8 { get; set; }
        public string TermsText8_1 { get; set; }
        public string TermsText8_2 { get; set; }
        public string TermsText8_3 { get; set; }
        public string TermsText8_4 { get; set; }
        public string TermsTextLastUpdated { get; set; }
        public string TermsText5_2_5 { get; set; }
        public string TermsText5_3 { get; set; }
        public string TermsText6 { get; set; }
        public string TermsText6_1 { get; set; }
        public string TermsText7 { get; set; }

        public TermsAndConditionsViewModel()
        {
            InitText();
        }

        private void InitText()
        {
            AcceptButtonText = "TERMS_AND_CONDITIONS_ACCEPT_TEXT".Translate();
            DeclineButtonText = "TERMS_AND_CONDITIONS_DECLINE_TEXT".Translate();
            TermsTitle = "TERMS_AND_CONDITIONS_TITLE".Translate();
            TermsAgreeText = "TERMS_AND_CONDITIONS_AGREE".Translate();
            TermsText1 = "TERMS_AND_CONDITIONS_1".Translate();
            TermsText1_1 = "TERMS_AND_CONDITIONS_1_1".Translate();
            TermsText1_2 = "TERMS_AND_CONDITIONS_1_2".Translate();
            TermsText2 = "TERMS_AND_CONDITIONS_2".Translate();
            TermsText2_1 = "TERMS_AND_CONDITIONS_2_1".Translate();
            TermsText2_2 = "TERMS_AND_CONDITIONS_2_2".Translate();
            TermsText2_3 = "TERMS_AND_CONDITIONS_2_3".Translate();
            TermsText2_4 = "TERMS_AND_CONDITIONS_2_4".Translate();
            TermsText3 = "TERMS_AND_CONDITIONS_3".Translate();
            TermsText3_1 = "TERMS_AND_CONDITIONS_3_1".Translate();
            TermsText3_1_P2 = "TERMS_AND_CONDITIONS_3_1_PART2".Translate();
            TermsText3_2 = "TERMS_AND_CONDITIONS_3_2".Translate();
            TermsText3_3 = "TERMS_AND_CONDITIONS_3_3".Translate();
            TermsText3_4 = "TERMS_AND_CONDITIONS_3_4".Translate();
            TermsText3_5 = "TERMS_AND_CONDITIONS_3_5".Translate();
            TermsText3_6 = "TERMS_AND_CONDITIONS_3_6".Translate();
            TermsText4 = "TERMS_AND_CONDITIONS_4".Translate();
            TermsText4_1 = "TERMS_AND_CONDITIONS_4_1".Translate();
            TermsText4_2 = "TERMS_AND_CONDITIONS_4_2".Translate();
            TermsText5 = "TERMS_AND_CONDITIONS_5".Translate();
            TermsText5_1 = "TERMS_AND_CONDITIONS_5_1".Translate();
            TermsText5_2 = "TERMS_AND_CONDITIONS_5_2".Translate();
            TermsText5_2_1 = "TERMS_AND_CONDITIONS_5_2_1".Translate();
            TermsText5_2_2 = "TERMS_AND_CONDITIONS_5_2_2".Translate();
            TermsText5_2_3 = "TERMS_AND_CONDITIONS_5_2_3".Translate();
            TermsText5_2_4 = "TERMS_AND_CONDITIONS_5_2_4".Translate();
            TermsText5_2_5 = "TERMS_AND_CONDITIONS_5_2_5".Translate();
            TermsText5_3 = "TERMS_AND_CONDITIONS_5_3".Translate();
            TermsText6 = "TERMS_AND_CONDITIONS_6".Translate();
            TermsText6_1 = "TERMS_AND_CONDITIONS_6_1".Translate();
            TermsText7 = "TERMS_AND_CONDITIONS_7".Translate();
            TermsText7_1 = "TERMS_AND_CONDITIONS_7_1".Translate();
            TermsText7_1_Email = "TERMS_AND_CONDITIONS_7_1_EMAIL".Translate();
            TermsText8 = "TERMS_AND_CONDITIONS_8".Translate();
            TermsText8_1 = "TERMS_AND_CONDITIONS_8_1".Translate();
            TermsText8_2 = "TERMS_AND_CONDITIONS_8_2".Translate();
            TermsText8_3 = "TERMS_AND_CONDITIONS_8_3".Translate();
            TermsText8_4 = "TERMS_AND_CONDITIONS_8_4".Translate();
            TermsTextLastUpdated = "TERMS_AND_CONDITIONS_LAST_UPDATED".Translate();
        }

        public override Task InitializeAsync(object navigationData)
        {
            ShowButtons = (bool)navigationData;
            RaisePropertyChanged(() => ShowButtons);
            return base.InitializeAsync(navigationData);
        }
    }
}
