using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Services.Interfaces;
using Foundation;
using UIKit;

namespace NHSCovidPassVerifier.iOS.Services
{
    public class IosDeeplinkingService : IDeeplinkingService
    {
        public async Task GoToAppSettings()
        {
            try
            {
                await UIApplication.SharedApplication.OpenUrlAsync(new NSUrl(UIApplication.OpenSettingsUrlString),
                    new UIApplicationOpenUrlOptions());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error attempting to deep-link to application settings: {ex}");
            }
        }
    }
}