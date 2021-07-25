using System.Threading.Tasks;
using System.Windows.Input;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels.Base;
using NHSCovidPassVerifier.Views;
using I18NPortable;

namespace NHSCovidPassVerifier.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        private readonly ISecureStorageService<TermsAndConditionsAgreed> _secureStorageService;
        private readonly IJwkService _jwks;

        public string LandingsPageImageSource { get; }
        public string OpenScannerText { get; set; }

        public string ReferenceDataStatusText { get; set; }
        public bool ReferenceDataUpdateRequired { get; set; }

        public ICommand NextPageCommand => new AsyncCommand(async () => await ExecuteOnceAsync(NextPage));

        public ICommand UpdateReferenceDataCommand => new AsyncCommand(
            async () => await ExecuteOnceAsync(UpdateReferenceData),
            () => ReferenceDataUpdateRequired
        );

        public LandingViewModel(ISecureStorageService<TermsAndConditionsAgreed> secureStorageService, IJwkService jwks)
        {
            _secureStorageService = secureStorageService;
            _jwks = jwks;
            
            LandingsPageImageSource = "nhs_logo_white.png";
            
            InitText();
            RefreshReferenceDataStatusText();
        
        }

        private void InitText()
        {
            OpenScannerText = "LANDING_PAGE_QR_CODE".Translate();
            ReferenceDataStatusText = "DATA_UPDATE_LOADING".Translate();
        }

        private async Task NextPage()
        {
            if (await CheckTermsAndConditions())
            {
                await CheckScannerPermission();
            }
            else
            {
                await PushAboutPageWithButtons();
            }
        }

        private async Task<bool> CheckTermsAndConditions()
        {
            var isAgreed = await _secureStorageService.GetSecureStorageAsync(_settingsService.TermsAndConditionsAgreed);
            return isAgreed?.IsTermsAndConditionsAgreed ?? false;
        }

        private async Task CheckScannerPermission()
        {
            if (await PermissionUtils.CheckAndRequestCameraPermission())
            {
                await PushScannerPage();
            }
            else
            {
                await ShowError("PERMISSIONS_MISSING_TITLE".Translate(), "PERMISSIONS_MISSING_DESCRIPTION_SCANNER".Translate());
            }
        }

        private async Task PushScannerPage()
        {
            await _navigationService.PushPage(new QRScannerPage(),false,true);
        }

        private async Task PushAboutPageWithButtons()
        {
            await _navigationService.PushPage(new AboutPage(),false,true);
        }
    
        /// <summary>
        /// Refreshes the 'Data Last Updated' value displayed
        /// </summary>
        /// <returns></returns>
        private async Task RefreshReferenceDataStatusText()
        {
            ReferenceDataStatusText = "DATA_UPDATE_LOADING".Translate();
            RaisePropertyChanged(() => ReferenceDataStatusText);

            bool updateRequired = false;

            bool jwkDataPresent = await _jwks.JwkListPresent();
            
            if (!jwkDataPresent)
            {
                updateRequired = true;
            } 
            else
            {
                updateRequired = await _jwks.JwkListNeedsUpdating();
            }

            // Push into status prop
            if (updateRequired)
            {
                ReferenceDataStatusText = "DATA_UPDATE_REQUIRED".Translate();
            } else
            {
                ReferenceDataStatusText = "DATA_UPDATE_NOT_REQUIRED".Translate();
            }
            RaisePropertyChanged(() => ReferenceDataStatusText);

            // Set the property backing CanExecute of the Update command
            ReferenceDataUpdateRequired = updateRequired;
            RaisePropertyChanged(() => ReferenceDataUpdateRequired);
        }

        private async Task UpdateReferenceData()
        {
            ReferenceDataStatusText = "DATA_UPDATE_RUNNING".Translate();
            RaisePropertyChanged(() => ReferenceDataStatusText);

            bool updateSuccess = await _jwks.UpdateJwkList();
            if (updateSuccess)
            {
                ReferenceDataUpdateRequired = false;
                ReferenceDataStatusText = "DATA_UPDATE_NOT_REQUIRED".Translate();
            } 
            else
            {
                ReferenceDataUpdateRequired = true;
                ReferenceDataStatusText = "DATA_UPDATE_REQUIRED".Translate();
            }

            RaisePropertyChanged(() => ReferenceDataStatusText);
            RaisePropertyChanged(() => ReferenceDataUpdateRequired);
        }

    }
}
