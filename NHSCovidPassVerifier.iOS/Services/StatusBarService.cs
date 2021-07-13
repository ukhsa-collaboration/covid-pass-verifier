using System;
using NHSCovidPassVerifier.iOS;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof (StatusBarService))]
namespace NHSCovidPassVerifier.iOS
{
    public class StatusBarService : IStatusBarService
    {

        private readonly int _statusBarTag = 1099; 

        public StatusBarService()
        {
        }

        public void RemoveStatusBar()
        {
            var subViews = UIApplication.SharedApplication.KeyWindow.Subviews;

            foreach (UIView v in subViews)
            {
                if (v.Tag.Equals(_statusBarTag))
                    v.RemoveFromSuperview();
            }

            UIApplication.SharedApplication.KeyWindow.RootViewController?.SetNeedsStatusBarAppearanceUpdate();
        }

        public void SetDefaultStatusBar()
        {
            SetStatusBarColor(NhsColour.NhsBlue.Color());
        }

        public void SetStatusBarColor(Color color)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                statusBar.Tag = _statusBarTag;
                statusBar.TranslatesAutoresizingMaskIntoConstraints = false;
                UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                statusBar.BackgroundColor = color.ToUIColor();

                statusBar.LeadingAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.LeadingAnchor).Active = true;
                statusBar.TopAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.TopAnchor).Active = true;
                statusBar.TrailingAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.TrailingAnchor).Active = true;
                statusBar.HeightAnchor.ConstraintEqualTo(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame.Height).Active = true;
            }
            else
            {
                UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    statusBar.BackgroundColor = color.ToUIColor();
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
                }
                statusBar.Tag = _statusBarTag;
            }
        }
    }
}
