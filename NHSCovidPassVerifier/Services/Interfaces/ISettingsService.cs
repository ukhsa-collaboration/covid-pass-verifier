using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface ISettingsService
    {
        public bool UseMockServices { get; set; }

        public string GetJwkUrl { get; }

        public string AndroidAppCenterKey { get; }

        public string iOSAppCenterKey { get; }

        public string AllowLandScapePortrait { get; }

        public string PreventLandscape { get; }

        public int DefaultTimeout { get; }

        public int ScannerResultShownDuration { get; }

        public int PressedToReleasedCloseInterval { get; }

        public int JwkValidForHours { get; }

        public bool IsScreenShotAllowed { get; }

        public string Jwk { get; }

        public JArray PublicKeys { get; }

        public string TermsAndConditionsAgreed { get; }
        
        public string InternationalEnabled { get; }

        public Task ClearAllSettings();

        public string GetVersionNumber();
    }
}