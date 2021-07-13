using System;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ServicesTests
{
    [TestFixture]
    public class InternationalEnabledServiceTests
    {
        private IInternationalEnabledService _internationalEnabledService;

        private ISecureStorageService<InternationalEnabled> _secureStorageService;
        private readonly Random _random = new Random();

        [SetUp]
        public void SetUp()
        {
            _secureStorageService = new MockSecureStorageService<InternationalEnabled>();

            _internationalEnabledService =
                new InternationalEnabledService(_secureStorageService, new MockSettingsService());
        }

        [Test]
        public void GetWithoutSetReturnsFalse()
        {
            // when
            var actual = _internationalEnabledService.GetInternationalEnabled();
            // then
            Assert.IsFalse(actual);
        }
        
        [Test]
        public async Task GetAfterSetToFalseReturnsFalse()
        {
            // given
            await _internationalEnabledService.SetInternationalEnabled(false);
            // when
            var actual = _internationalEnabledService.GetInternationalEnabled();
            // then
            Assert.IsFalse(actual);
        }
        
        [Test]
        public async Task GetAfterSetToTrueReturnsTrue()
        {
            // given
            await _internationalEnabledService.SetInternationalEnabled(true);
            // when
            var actual = _internationalEnabledService.GetInternationalEnabled();
            // then
            Assert.IsTrue(actual);
        }
        
        [Test]
        public async Task GetAfterRandomSetThenSetToFalseReturnsFalse()
        {
            // given
            await _internationalEnabledService.SetInternationalEnabled(_random.NextDouble() > 0.5);
            await _internationalEnabledService.SetInternationalEnabled(false);
            // when
            var actual = _internationalEnabledService.GetInternationalEnabled();
            // then
            Assert.IsFalse(actual);
        }
        
        [Test]
        public async Task GetAfterRandomSetThenSetToTrueReturnsTrue()
        {
            // given
            await _internationalEnabledService.SetInternationalEnabled(_random.NextDouble() > 0.5);
            await _internationalEnabledService.SetInternationalEnabled(true);
            // when
            var actual = _internationalEnabledService.GetInternationalEnabled();
            // then
            Assert.IsTrue(actual);
        }
    }
}