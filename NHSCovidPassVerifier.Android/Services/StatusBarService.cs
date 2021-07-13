using Android.OS;
using NHSCovidPassVerifier.Droid;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(StatusBarService))]
namespace NHSCovidPassVerifier.Droid
{
    public class StatusBarService : IStatusBarService
    {
        public StatusBarService()
        {
        }

        public void RemoveStatusBar()
        {
        }

        public void SetDefaultStatusBar()
        {
            SetStatusBarColor(NhsColour.NhsBlue.Color());
        }

        public void SetStatusBarColor(Color color)
        {
            if (CrossCurrentActivity.Current?.Activity?.Window != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(color.ToAndroid());
                }
            }
        }
    }
}