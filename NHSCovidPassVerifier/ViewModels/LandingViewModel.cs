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
        public string LandingsPageImageSource { get; }
        public string OpenScannerText { get; set; }

        public ICommand NextPageCommand => new AsyncCommand(async () => await ExecuteOnceAsync(NextPage));

        public LandingViewModel(ISecureStorageService<TermsAndConditionsAgreed> secureStorageService)
        {
            _secureStorageService = secureStorageService;
            
            LandingsPageImageSource = "nhs_logo_white.png";
            InitText();
        }

        private void InitText()
        {
            OpenScannerText = "LANDING_PAGE_QR_CODE".Translate();
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
    }
}
