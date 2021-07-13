using System;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.TestData;
using Moq;
using NUnit.Framework;
using System.IO;
using NHSCovidPassVerifier.Models.Dtos;
using System.Text;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.Tests.ServicesTests
{
    public class JwkServiceTests
    {
        #region Setup
        private readonly IJwkService _jwkService;
        private readonly Mock<IJwkRepository> _jwkRepository;
        private readonly Mock<ILoggingService> _loggingService;
        private readonly Mock<ISecureStorageService<JwkDto>> _secureStorage;
        private readonly Mock<ISettingsService> _settingsService;

        public JwkServiceTests()
        {
            _jwkRepository = new Mock<IJwkRepository>();
            _loggingService = new Mock<ILoggingService>();
            _secureStorage = new Mock<ISecureStorageService<JwkDto>>();
            _settingsService = new Mock<ISettingsService>();

            _jwkService = new JwkService(_jwkRepository.Object, _loggingService.Object, _secureStorage.Object, _settingsService.Object);
        }

        [SetUp]
        public void Setup()
        {
            _settingsService.Setup(x => x.Jwk).Returns(nameof(_settingsService.Object.Jwk));
            _secureStorage.Setup(x => x.SetSecureStorageAsync(_settingsService.Object.Jwk, It.IsAny<JwkDto>()));
        }
        #endregion

        #region GetJwkTests
        [Test]
        public void TestGetJwkSuccess()
        {
            //given
            var expectedKey = TestJwkData.GetValidJwkString();
            _jwkRepository.Setup(x => x.GetJwk()).Returns(TestJwkData.GetJwkStreamWithStatusCode());

            //when 
            var response = _jwkRepository.Object.GetJwk().Result;
            var actualKey = _jwkService.GetJwk().Result.Data.Jwk;

            //then 
            Assert.AreEqual(expectedKey, actualKey);
            _loggingService.Verify(x => x.LogApiError<Stream>(It.IsAny<LogSeverity>(), response, It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void JwkDataIsEmptyStringIfJwkResponseDataIsNull()
        {
            var expectedKey = string.Empty;
            _jwkRepository
                .Setup(x => x.GetJwk())
                .Returns(TestJwkData.GetJwkStreamWithStatusCodeAndData((MemoryStream)null));

            var response = _jwkRepository.Object.GetJwk().Result;
            var actualKey = _jwkService.GetJwk().Result.Data.Jwk;

            Assert.AreEqual(expectedKey, actualKey);
            _loggingService.Verify(x => x.LogApiError(LogSeverity.ERROR, response, It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void TestGetJwkReturnsError()
        {
            //given
            var expectedStatusCode = 500;
            _jwkRepository.Setup(x => x.GetJwk()).Returns(TestJwkData.GetJwkStreamWithStatusCode(expectedStatusCode));

            //when 
            var response = _jwkRepository.Object.GetJwk().Result;
            var result = _jwkService.GetJwk().Result;

            //then 
            Assert.AreEqual(expectedStatusCode, (int)result.State);
            _loggingService.Verify(x => x.LogApiError<Stream>(It.IsAny<LogSeverity>(), response, It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void LogErrorWhenRepositoryThrowsException()
        {
            var expectedStatusCode = 500;
            var expectedException = new Exception();
            _jwkRepository.Setup(x => x.GetJwk()).Throws(expectedException);

            var response = _jwkService.GetJwk().Result;

            Assert.AreEqual(expectedStatusCode, (int)response.State);
            _loggingService.Verify(x => x.LogException(LogSeverity.ERROR, expectedException, It.IsAny<string>()), Times.AtLeastOnce);
        }

        #endregion

        #region UpdateJWKListTests

        [TestCase(0)]
        [TestCase(204)]
        [TestCase(404)]
        [TestCase(500)]
        public void ShouldNotUpdateJwkListIfUnsuccessfulStatusCode(int statusCode)
        {
            //given
            var testData = TestJwkData.GetValidJwk().ToString();
            var mockApiResponse = TestJwkData.GetJwkStreamWithStatusCodeAndData(testData, statusCode);
            _jwkRepository.Setup(x => x.GetJwk()).Returns(mockApiResponse);
            var expectedStorage = mockApiResponse.Result.Data;
            //when
            var actual = _jwkService.UpdateJwkList().Result;
            //then
            _secureStorage.Verify(x => x.SetSecureStorageAsync(_settingsService.Object.Jwk, null), Times.Never);
            Assert.False(actual);

        }
        #endregion

        #region JwkListDoesNotNeedUpdatingTest
        [Test]
        public async Task JwkDoesNotNeedUpdating()
        {
            var jwkDto = TestJwkData.GetValidJwk();
            var jwkValidFor = 6;

            _secureStorage.Setup(x => x.GetSecureStorageAsync(_settingsService.Object.Jwk)).ReturnsAsync(jwkDto);
            _settingsService.Setup(x => x.JwkValidForHours).Returns(jwkValidFor);

            var actual = await _jwkService.JwkListNeedsUpdating();

            Assert.False(actual);
        }

        [TestCase(0)]
        [TestCase(24)]
        [TestCase(8760)]
        [TestCase(876000)]
        public async Task JwkNeedsUpdating(int hours)
        {
            var jwkDto = TestJwkData.GetValidJwk();
            hours =_settingsService.Object.JwkValidForHours + hours;
            jwkDto.TimeReceived = DateTime.UtcNow.AddHours(-hours);
            _secureStorage.Setup(x => x.GetSecureStorageAsync(_settingsService.Object.Jwk)).ReturnsAsync(jwkDto);

            var actual = await _jwkService.JwkListNeedsUpdating();

            Assert.True(actual);
        }
        #endregion

        #region TestJwkListPresent
        [Test]
        public async Task JwkListShouldBePresent()
        {
            var jwkDto = TestJwkData.GetValidJwk();
            _secureStorage.Setup(x => x.GetSecureStorageAsync(_settingsService.Object.Jwk)).ReturnsAsync(jwkDto);

            var actual = await _jwkService.JwkListPresent();

            Assert.True(actual);
        }
        [Test]
        public async Task JwkListShouldNotBePresent()
        {
            var jwkDto = new JwkDto();
            _secureStorage.Setup(x => x.GetSecureStorageAsync(_settingsService.Object.Jwk)).ReturnsAsync(jwkDto);

            var actual = await _jwkService.JwkListPresent();

            Assert.False(actual);
        }
        #endregion
    }
}
