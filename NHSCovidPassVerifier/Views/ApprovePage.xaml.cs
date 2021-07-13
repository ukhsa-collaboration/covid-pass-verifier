using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApprovePage : ContentPage
    {
        public ApprovePage()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<ApprovePageViewModel>();
        }
    }
}