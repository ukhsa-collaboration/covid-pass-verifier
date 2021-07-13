using NHSCovidPassVerifier.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Tests.MockServices
{
    public class MockSettingsService : ISettingsService
    {
        public MockSettingsService()
        {
            UseMockServices = true; 
        }

        public bool UseMockServices { get; set; }

        public string GetJwkUrl => "";

        public string AndroidAppCenterKey => "";

        public string iOSAppCenterKey => "";

        public string AllowLandScapePortrait => nameof(AllowLandScapePortrait);

        public string PreventLandscape => nameof(PreventLandscape);

        public int DefaultTimeout => 15;

        public int ScannerResultShownDuration => 3000;

        public int PressedToReleasedCloseInterval => 100;

        public int JwkValidForHours => 6;

        public bool IsScreenShotAllowed => true;

        public string Jwk => "";
        public JArray PublicKeys { get; }

        public string TermsAndConditionsAgreed => nameof(TermsAndConditionsAgreed);

        public string InternationalEnabled => nameof(InternationalEnabled);

        public Task ClearAllSettings()
        {
            return Task.CompletedTask;
        }
        public string GetVersionNumber()
        {
            return "1.2.0";
        }
    }
}
