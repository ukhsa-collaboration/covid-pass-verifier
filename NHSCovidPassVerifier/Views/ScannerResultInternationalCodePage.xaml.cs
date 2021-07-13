using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerResultInternationalCodePage : ContentPage
    {
        private readonly IStatusBarService _statusBarService = IoCContainer.Resolve<IStatusBarService>();
        public ScannerResultInternationalCodePage()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<ScannerResultInternationalCodeViewModel>();
        }
    }
}