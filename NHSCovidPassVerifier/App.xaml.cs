using System.Threading.Tasks;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NHSCovidPassVerifier
{
    public partial class App : Application
    {
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;

        public App()
        {

            System.Diagnostics.Debug.Print("Initializing app.");
            InitializeComponent();

            _settingsService = IoCContainer.Resolve<ISettingsService>();

            _navigationService = IoCContainer.Resolve<INavigationService>();

            ConfigureApp();

            UpdateScannerEcKeys();
        }

        protected override void OnStart()
        {
            if (!string.IsNullOrEmpty(_settingsService.AndroidAppCenterKey)
                && !string.IsNullOrEmpty(_settingsService.iOSAppCenterKey))
            {
                AppCenter.Start($"android={_settingsService.AndroidAppCenterKey};ios={_settingsService.iOSAppCenterKey}", typeof(Analytics), typeof(Crashes));
            }
            if (VersionTracking.IsFirstLaunchEver)
            {
                _settingsService.ClearAllSettings();
            }
            _navigationService.OpenLandingPage();

            base.OnStart();
        }

        private void ConfigureApp()
        {
            LocalesService.Initialize();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }

        private async Task UpdateScannerEcKeys()
        {
           await IoCContainer.Resolve<IJwkService>().UpdateJwkList();
        }
    }
}
