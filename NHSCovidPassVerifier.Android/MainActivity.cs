using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Droid.Services;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels.Base;
using NHSCovidPassVerifier.Views;
using FFImageLoading.Forms.Platform;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.CurrentActivity;
using Xamarin.Android.Net;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

[assembly: UsesFeature("android.hardware.camera", Required = true)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = true)]
namespace NHSCovidPassVerifier.Droid
{
    [Activity(
        Label = "NHS COVID Pass Verifier",
        Icon = "@mipmap/icon",
        RoundIcon = "@mipmap/icon_round",
        Theme = "@style/MainTheme",
        NoHistory = false,
        LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            global::Xamarin.Forms.Forms.SetFlags(new string[] { "Brush_Experimental", "Shapes_Experimental", "RadioButton_Experimental", "Expander_Experimental" });
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CrossCurrentActivity.Current.Activity = this;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 94, 184));
            }

            base.OnCreate(savedInstanceState);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            AndroidEnvironment.UnhandledExceptionRaiser += OnUnhandledAndroidException;

            Forms.Init(this, savedInstanceState);

            PreventLinkerFromStrippingCommonLocalizationReferences();
            RegisterClientHandler();
            RegisterAndroidServices();

            LoadApplication(new App());
            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            var settingsService = IoCContainer.Resolve<ISettingsService>();
            if (!settingsService.IsScreenShotAllowed)
                this.Window.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);

        }

        private void RegisterClientHandler()
        {
            RestClient.HttpClientHandler = new AndroidClientHandler();
        }

        private void RegisterAndroidServices()
        {
            IoCContainer.RegisterSingleton<IConfigurationProvider, ConfigurationProvider>();
            IoCContainer.RegisterSingleton<IStatusBarService, StatusBarService>();
            IoCContainer.RegisterInterface<IDeeplinkingService, AndroidDeeplinkingService>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            var settingsService = IoCContainer.Resolve<ISettingsService>();

            AndroidEnvironment.UnhandledExceptionRaiser -= OnUnhandledAndroidException;
            base.OnDestroy();
        }

        private void OnUnhandledAndroidException(object sender, RaiseThrowableEventArgs e)
        {
            if (e?.Exception != null)
            {
                var loggingService = IoCContainer.Resolve<ILoggingService>();

                string message = $"{nameof(MainActivity)}.{nameof(OnUnhandledAndroidException)}: "
                    + (!e.Handled
                    ? "Native unhandled crash"
                    : "Native unhandled exception - not crashing");
                LogSeverity logLevel = e.Handled
                    ? LogSeverity.WARNING
                    : LogSeverity.ERROR;
                loggingService.LogException(logLevel, e.Exception, message);
            }
        }

        public override void OnBackPressed()
        {
            var navService = IoCContainer.Resolve<INavigationService>();
            var currentPage = navService.FindCurrentPage();
            if (currentPage.GetType() == typeof(LandingPage))
            {
                base.OnBackPressed();
            }
            else if (currentPage.BindingContext is BaseViewModel viewModel)
            {
                viewModel.BackCommand.Execute(null);
            }
            else
            {
                navService.PopPage();
            }
        }
        private static void PreventLinkerFromStrippingCommonLocalizationReferences()
        {
            _ = new System.Globalization.GregorianCalendar();
            _ = new System.Globalization.PersianCalendar();
            _ = new System.Globalization.UmAlQuraCalendar();
            _ = new System.Globalization.ThaiBuddhistCalendar();
        }
    }
}