using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Dtos;
using NHSCovidPassVerifier.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.International;
using Xamarin.Essentials;

namespace NHSCovidPassVerifier.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly JObject _settings;

        public SettingsService()
        {
            UseMockServices = true;
        }

        public SettingsService(IConfigurationProvider configurationProvider)
        {
            using var reader = new StreamReader(configurationProvider.GetConfiguration());

            var json = reader.ReadToEnd();

            _settings = JObject.Parse(json);

            UseMockServices = false;
        }

        public bool UseMockServices { get; set; }

        public string GetJwkUrl => GetSetting<string>(nameof(GetJwkUrl));

        public string AndroidAppCenterKey => GetSetting<string>(nameof(AndroidAppCenterKey));

        public string iOSAppCenterKey => GetSetting<string>(nameof(iOSAppCenterKey));

        public int DefaultTimeout => GetSetting<int>(nameof(DefaultTimeout));

        public string AllowLandScapePortrait => nameof(AllowLandScapePortrait);
        public string PreventLandscape => nameof(PreventLandscape);

        public int ScannerResultShownDuration => GetSetting<int>(nameof(ScannerResultShownDuration));

        public int PressedToReleasedCloseInterval => GetSetting<int>(nameof(PressedToReleasedCloseInterval));

        public int JwkValidForHours => GetSetting<int>(nameof(JwkValidForHours));

        public bool IsScreenShotAllowed => GetSetting<bool>(nameof(IsScreenShotAllowed));

        public string Jwk => nameof(Jwk);

        public JArray PublicKeys => GetSetting<JArray>(nameof(PublicKeys));

        public string TermsAndConditionsAgreed => nameof(TermsAndConditionsAgreed);

        public string InternationalEnabled => nameof(InternationalEnabled);

        private T GetSetting<T>(string key)
        {
            var value = _settings.SelectToken(key);

            if (value == null)
            {
                throw new InvalidOperationException($"Key '{key}' does not exist in current settings file.");
            }

            return value.Value<T>();
        }

        public async Task ClearAllSettings()
        {
            var termsAndConditionsTask = IoCContainer.Resolve<ISecureStorageService<TermsAndConditionsAgreed>>().Clear(TermsAndConditionsAgreed);
            var internationalTask = IoCContainer.Resolve<ISecureStorageService<InternationalEnabled>>().Clear(InternationalEnabled);
            var jwkTask = IoCContainer.Resolve<ISecureStorageService<JwkDto>>().Clear(Jwk);
            await Task.WhenAll(termsAndConditionsTask, jwkTask, internationalTask);
        }

        public string GetVersionNumber()
        {
            return $"{AppInfo.VersionString} ({AppInfo.BuildString})";
        }
    }
}