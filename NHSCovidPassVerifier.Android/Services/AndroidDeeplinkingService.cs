using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content;
using NHSCovidPassVerifier.Services.Interfaces;
using Plugin.CurrentActivity;

namespace NHSCovidPassVerifier.Droid.Services
{
    public class AndroidDeeplinkingService : IDeeplinkingService
    {
        public Task GoToAppSettings()
        {
            try
            {
                var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
                intent.AddFlags(ActivityFlags.NewTask);
                string package_name = "uk.gov.dhsc.healthrecord";
                var uri = Android.Net.Uri.FromParts("package", package_name, null);
                intent.SetData(uri);
                CrossCurrentActivity.Current.AppContext.StartActivity(intent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error attempting to deep-link to application settings: {ex}");
            }

            return Task.CompletedTask;
        }
    }
}