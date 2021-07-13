using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Services.Repositories
{
    public class JwkRepository : IJwkRepository
    {
        private readonly ISettingsService _settingsService;
        private IRestClient _restClient;

        public JwkRepository(ISettingsService settingsService, IRestClient restClient)
        {
            _settingsService = settingsService;
            _restClient = restClient;
        }

        public async Task<ApiResponse<Stream>> GetJwk()
        {
            return await _restClient.GetFileAsStreamAsync(_settingsService.GetJwkUrl);
        }
    }
}
