using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NhsSplashPage : ContentPage
    {
        public NhsSplashPage()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<NhsSplashViewModel>();
        }
    }
}