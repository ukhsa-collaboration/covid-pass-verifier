using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<AboutPageViewModel>();
        }
    }
}