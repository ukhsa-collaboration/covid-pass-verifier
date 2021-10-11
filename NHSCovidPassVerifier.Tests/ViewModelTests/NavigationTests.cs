using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Tests.MockServices;
using NHSCovidPassVerifier.ViewModels;
using NHSCovidPassVerifier.Views;
using I18NPortable;
using Moq;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    public class NavigationTests
    {
        private readonly MockNavigationService _mockNavigationService;

        public NavigationTests()
        {
            _mockNavigationService = new MockNavigationService();
        }

        [Test]
        public void PopPageTest()
        {
            var PopPageResult = _mockNavigationService.PopPage();
            Assert.IsNotNull(PopPageResult);
        }

        [Test]
        public void PopPageTestTrue()
        {
            var PopPageResult = _mockNavigationService.PopPage(true);
            Assert.IsNotNull(PopPageResult);
        }

        [Test]
        public void PopPageWithResultTest()
        {
            var PopPageWithResult = _mockNavigationService.PopPageWithResult();
            Assert.IsNotNull(PopPageWithResult);
        }

        [Test]
        public void ReplaceTopPageTest()
        {
            var AboutPageReturn = new AboutPage();

            var ReplaceTopPageResult =  _mockNavigationService.ReplaceTopPage(AboutPageReturn);
            Assert.IsNotNull(ReplaceTopPageResult);
        }

        [Test]
        public void PushModalPageTest()
        {
            var AboutPageReturn = new AboutPage();

            var PushModalPageResult = _mockNavigationService.PushModal(AboutPageReturn);
            Assert.IsNotNull(PushModalPageResult);
        }

        [Test]
        public void PushPageTest()
        {
            var AboutPageReturn = new AboutPage();

            var PushPageResult =  _mockNavigationService.PushPage(AboutPageReturn);
            Assert.IsNotNull(PushPageResult);
        }

    }
}
