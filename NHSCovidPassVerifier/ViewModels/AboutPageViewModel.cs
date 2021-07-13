using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels.Base;
using NHSCovidPassVerifier.Views;
using I18NPortable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NHSCovidPassVerifier.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        private readonly ISecureStorageService<TermsAndConditionsAgreed> _secureStorageService;

        public string AboutPageTitle { get; set; }
        public string RepoLinkTitle { get; set; }
        public string RepoLinkURL { get; set; }
        public string AboutFAQ { get; set; }
        public string AcceptButtonText { get; set; }
        public string DeclineButtonText { get; set; }
        public string VoluntaryTitle { get; set; }
        public string VoluntaryText { get; set; }
        public string AboutHowThisAppWorksTitle2 { get; set; }
        public string HowToUseScannerTitle { get; set; }
        public string AboutPageParagraph1 { get; set; }
        public string AboutPageSubtitle2 { get; set; }
        public string AboutDataText { get; set; }
        public string AboutPageSubtitle3 { get; set; }
        public string AboutPageSubtitle4 { get; set; }
        public string AboutPageSubtitle5 { get; set; }
        public string AboutAppName { get; set; }
        public string AboutAppRelease { get; set; }
        public string AboutAppVersion { get; set; }
        public string AboutAppNameValue { get; set; }
        public string AboutAppReleaseValue { get; set; }
        public string AboutAppVersionValue { get; set; }
        public string AboutHowThisAppWorksTitle { get; set; }
        public string AboutHowThisAppWorksParagraph { get; set; }
        public string AboutInternationalTitle { get; set; }
        public string AboutInternationalParagraph { get; set; }
        public string AboutDomesticTitle { get; set; }
        public string AboutDomesticParagraph { get; set; }
        public bool ShowButtons { get; set; } = true;
        public ICommand AgreeCommand =>
            new AsyncCommand(async () => await ExecuteOnceAsync(AcceptTermsAndConditions));
        public override ICommand BackCommand => BackWithResultCommand;

        public string AboutHowThisAppWorksText { get; set; }
        public string AboutHowThisAppWorksText2 { get; set; }
        public string RecordedTitle { get; set; }
        public string RecordedText { get; set; }
        public string RecordedText2 { get; set; }
        public string RejectTitle { get; set; }
        public string RejectText { get; set; }
        public string ErasedTitle { get; set; }
        public string ErasedText { get; set; }
        public string PolociesTitle { get; set; }
        public string PoiciesText { get; set; }
        public string PersonalDataTitle { get; set; }
        public string PersonalDataText { get; set; }
        public string ConsentTitle { get; set; }
        public string AppName { get; set; }
        public string AppPromo { get; set; }
        public string AppDescriptionTitle { get; private set; }
        public string PrivacyLink { get; private set; }
        public string TANDCLink { get; private set; }
        public string AccessibilityLink { get; private set; }
        public string FAQLink { get; private set; }
        public string AppPermissionsTitle { get; private set; }
        public string AppDescription { get; set; }
        public string AppPermissionsText { get; private set; }

        public AboutPageViewModel(ISecureStorageService<TermsAndConditionsAgreed> secureStorageService)
        {
            _secureStorageService = secureStorageService;
            InitText();
        }

        private void InitText()
        {
            AboutPageTitle = "ABOUT_TITLE".Translate();
            HowToUseScannerTitle = "ABOUT_SUBTITLE_1".Translate();
            AboutPageParagraph1 = "ABOUT_SUBTITLE_1_TEXT_1".Translate();
            AboutPageSubtitle2 = "ABOUT_SUBTITLE_2".Translate();
            AboutDataText = "ABOUT_DATA_TEXT".Translate();
            AboutPageSubtitle3 = "ABOUT_SUBTITLE_3".Translate();
            AboutPageSubtitle4 = "ABOUT_SUBTITLE_4".Translate();
            AboutPageSubtitle5 = "ABOUT_SUBTITLE_5".Translate();
            AboutAppVersion = "ABOUT_APP_VERSION".Translate();
            AboutAppVersionValue = _settingsService.GetVersionNumber();
            AboutHowThisAppWorksTitle = "ABOUT_HOW_THIS_APP_WORKS".Translate();
            AboutHowThisAppWorksParagraph = "ABOUT_HOW_THIS_APP_WORKS_PARAGRAPH".Translate();
            AboutInternationalTitle = "ABOUT_SUBTITLE_INTERNATIONAL".Translate();
            AboutInternationalParagraph = "ABOUT_INTERNATIONAL_PARAGRAPH".Translate();
            AboutDomesticTitle = "ABOUT_SUBTITLE_DOMESTIC".Translate();
            AboutDomesticParagraph = "ABOUT_DOMESTIC_PARAGRAPH".Translate();
            RepoLinkTitle = "ABOUT_GITHUB_REPO".Translate();
            RepoLinkURL = "ABOUT_GITHUB_URL".Translate();
            AboutFAQ = "ABOUT_FAQ".Translate();
            AcceptButtonText = "TERMS_AND_CONDITIONS_ACCEPT_TEXT".Translate();
            DeclineButtonText = "TERMS_AND_CONDITIONS_DECLINE_TEXT".Translate();
            VoluntaryTitle = "ABOUT_VOLUNTARY_TITLE".Translate();
            VoluntaryText = "ABOUT_VOLUNTARY_TEXT".Translate();
            AboutHowThisAppWorksTitle2 = "ABOUT_HOW_APP_WORKS_TITLE".Translate();
            AboutHowThisAppWorksText = "ABOUT_HOW_APP_WORKS_TEXT1".Translate();
            RecordedTitle = "ABOUT_RECORDED_TITLE".Translate();
            RecordedText = "ABOUT_RECORDED_TEXT1".Translate();
            RejectTitle = "ABOUT_REJECT_TITLE".Translate();
            RejectText = "ABOUT_REJECT_TEXT".Translate();
            ErasedTitle = "ABOUT_ERASED_TITLE".Translate();
            ErasedText = "ABOUT_ERASED_TEXT".Translate();
            PolociesTitle = "ABOUT_POLICIES_TITILE".Translate();
            PoiciesText = "ABOUT_POLICIES_TEXT".Translate();
            PersonalDataTitle = "ABOUT_PERSONAL_DATA_TITLE".Translate();
            PersonalDataText = "ABOUT_PERSONAL_DATA_TEXT".Translate();
            ConsentTitle = "ABOUT_CONSENT".Translate();
            AppDescription = "APP_DESCRIPTION".Translate();
            AppName = "APP_NAME".Translate();
            AppPromo = "APP_PROMO".Translate();
            AppDescriptionTitle = "APP_DESCRIPTION_TITLE".Translate();
            PrivacyLink = "ABOUT_PRIVACY".Translate();
            TANDCLink = "ABOUT_TANDC".Translate();
            AccessibilityLink = "ABOUT_ACCESSIBILITY".Translate();
            FAQLink = "ABOUT_FAQ".Translate();
            AppPermissionsTitle = "ABOUT_APP_PERMISSIONS_TITLE".Translate();
            AppPermissionsText = "ABOUT_APP_PERMISSIONS_TEXT".Translate();
        }

        public override Task InitializeAsync(object navigationData)
        {
            ShowButtons = (bool)navigationData;
            RaisePropertyChanged(() => ShowButtons);
            return base.InitializeAsync(navigationData);
        }

        private async Task AcceptTermsAndConditions()
        {
            await _secureStorageService.SetSecureStorageAsync(_settingsService.TermsAndConditionsAgreed,
                new TermsAndConditionsAgreed { IsTermsAndConditionsAgreed = true });
            await CheckScannerPermission();
        }

        private async Task CheckScannerPermission()
        {
            if (await PermissionUtils.CheckAndRequestCameraPermission())
            {
                await _navigationService.ReplaceTopPage(new QRScannerPage());
            }
            else
            {
                await ShowError("PERMISSIONS_MISSING_TITLE".Translate(), "PERMISSIONS_MISSING_DESCRIPTION_SCANNER".Translate());
            }
        }
    }
}
