using System.Linq;
using System.Net.Http;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.iOS.Services;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Views;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using ObjCRuntime;
using UIKit;
using Xamarin.Essentials;

namespace NHSCovidPassVerifier.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags(new string[] { "CollectionView_Experimental", "Brush_Experimental", "Shapes_Experimental", "RadioButton_Experimental", "Expander_Experimental" });

            global::Xamarin.Forms.Forms.Init();

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            RegisterClientHandler();
            RegisterIosServices();

            LoadApplication(new App());

            DeviceDisplay.MainDisplayInfoChanged += HandleOrientationChanges; 
            return base.FinishedLaunching(app, options);
        }

        private void RegisterClientHandler()
        {
            RestClient.HttpClientHandler = new NSUrlSessionHandler();
        }

        private void RegisterIosServices()
        {
            IoCContainer.RegisterSingleton<IConfigurationProvider, ConfigurationProvider>();
            IoCContainer.RegisterSingleton<IStatusBarService, StatusBarService>();
            IoCContainer.RegisterInterface<IDeeplinkingService, IosDeeplinkingService>();
        }

        private void HandleOrientationChanges(object sender, DisplayInfoChangedEventArgs e)
        {
            var orientation = e.DisplayInfo.Orientation;
            var statusBarService = IoCContainer.Resolve<IStatusBarService>();

            if (orientation.Equals(DisplayOrientation.Landscape))
                statusBarService.RemoveStatusBar();
            else if (orientation.Equals(DisplayOrientation.Portrait))
                statusBarService.SetDefaultStatusBar();
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            return UIInterfaceOrientationMask.Portrait;
        }

        public override void WillTerminate(UIApplication uiApplication)
        {
            base.WillTerminate(uiApplication);
            DeviceDisplay.MainDisplayInfoChanged -= HandleOrientationChanges;
        }

    }
}
