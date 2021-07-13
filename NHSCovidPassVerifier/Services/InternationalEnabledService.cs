using System.Threading.Tasks;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Services
{
    public class InternationalEnabledService : IInternationalEnabledService
    {
        private bool _internationalEnabled;

        private readonly ISecureStorageService<InternationalEnabled> _secureStorageService;
        private readonly ISettingsService _settingsService;

        public InternationalEnabledService(ISecureStorageService<InternationalEnabled> secureStorageService, 
            ISettingsService settingsService)
        {
            _secureStorageService = secureStorageService;
            _settingsService = settingsService;
            UpdateInternationalEnabled();
        }

        private async Task UpdateInternationalEnabled()
        {
            _internationalEnabled =
                (await _secureStorageService.GetSecureStorageAsync(_settingsService.InternationalEnabled))
                ?.IsInternationalEnabled ?? false;
        }

        public bool GetInternationalEnabled()
        {
            return _internationalEnabled;
        }

        public async Task SetInternationalEnabled(bool newValue)
        {
            _internationalEnabled = newValue;
            await _secureStorageService.SetSecureStorageAsync(_settingsService.InternationalEnabled,
                new InternationalEnabled { IsInternationalEnabled = newValue });
        }
    }
}