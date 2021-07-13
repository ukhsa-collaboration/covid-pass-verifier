using NHSCovidPassVerifier.ViewModels.Base;
using Xamarin.Forms;
using I18NPortable;

namespace NHSCovidPassVerifier.ViewModels
{
    public class NhsSplashViewModel : BaseViewModel
    {
        public string LoadingText { get; set; }

        public NhsSplashViewModel()
        {
            InitText();
        }

        private void InitText()
        {
            LoadingText = "NHS_SPLASH_TEXT".Translate();
        }
    }
}
