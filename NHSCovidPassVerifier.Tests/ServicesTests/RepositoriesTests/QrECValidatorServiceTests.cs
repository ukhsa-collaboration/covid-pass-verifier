using NHSCovidPassVerifier.Models.Dtos;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Services.Repositories;
using NHSCovidPassVerifier.Tests.TestData;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services;

namespace NHSCovidPassVerifier.Tests.RepositoryTests
{
    [TestFixture]
    class QrECValidatorServiceTests
    {
        private readonly Mock<ISecureStorageService<JwkDto>> _mockSecureStorage;
        private readonly Mock<ILoggingService> _mockLoggingService; 
        private readonly IQrValidatorService _qrValidatorService;
        private readonly ISettingsService settingsService;

        public QrECValidatorServiceTests()
        {
            _mockSecureStorage = new Mock<ISecureStorageService<JwkDto>>();
            _mockLoggingService = new Mock<ILoggingService>(); 
            settingsService = new SettingsService();

            _qrValidatorService = new QrECValidatorService(_mockSecureStorage.Object, 
                settingsService, _mockLoggingService.Object);
        }

        [Test]
        public async Task GetECKeyShouldReturnDefaultWithNoJWK()
        {
            var jwkDto = new JwkDto();

            _mockSecureStorage.Setup(x => x.GetSecureStorageAsync(It.IsAny<string>())).ReturnsAsync(jwkDto);

            var actual = await _qrValidatorService.GetKeyVaultKey(string.Empty);
            IEnumerable<SubjectPublicKeyInfoDto> expected = default;

            Assert.AreEqual(expected, actual);
        }
    }
}
