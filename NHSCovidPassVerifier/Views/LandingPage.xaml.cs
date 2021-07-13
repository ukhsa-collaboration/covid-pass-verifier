using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        private readonly IStatusBarService _statusBarService = IoCContainer.Resolve<IStatusBarService>();

        public LandingPage()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<LandingViewModel>();
        }
    }
}