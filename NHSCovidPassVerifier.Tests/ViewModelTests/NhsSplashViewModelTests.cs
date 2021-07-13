using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using NUnit.Framework;

namespace NHSCovidPassVerifier.Tests.ViewModelTests
{
    [TestFixture]
    public class NhsSplashViewModelTests
    {
        private readonly NhsSplashViewModel _viewModel;

        public NhsSplashViewModelTests()
        {
            _viewModel = new NhsSplashViewModel();
        }

        [Test]
        public void TestText()
        {
            Assert.AreEqual(this._viewModel.LoadingText, "NHS_SPLASH_TEXT".Translate());
        }
    }
}
