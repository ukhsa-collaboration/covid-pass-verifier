using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DropDownMenu : Frame
    {
        public DropDownMenu()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<QRScannerViewModel>();
        }
    }
}