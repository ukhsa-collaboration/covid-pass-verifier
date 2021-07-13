using System;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using Foundation;
using HIPSTO.iOS.Platform.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(IOSPageRenderer))]
namespace HIPSTO.iOS.Platform.Renderers
{
    public class IOSPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            IoCContainer.Resolve<IStatusBarService>().SetDefaultStatusBar();
        }
    }
}
