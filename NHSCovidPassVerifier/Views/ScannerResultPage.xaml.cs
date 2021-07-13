using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerResultPage : ContentPage
    {
        private readonly Timer _timer;

        private readonly IStatusBarService _statusBarService = IoCContainer.Resolve<IStatusBarService>();

        private readonly ISettingsService _settingsService = IoCContainer.Resolve<ISettingsService>();

        public ScannerResultPage()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<ScannerResultViewModel>();

            this._timer = new Timer();
            _timer.Interval = _settingsService.PressedToReleasedCloseInterval;
        }

        private void Button_Pressed(object sender, System.EventArgs e)
        {
            _timer.Enabled = true;
            (BindingContext as ScannerResultViewModel)?.PauseTimer();

        }

        private void Button_Released(object sender, System.EventArgs e)
        {
            (BindingContext as ScannerResultViewModel)?.ResumeTimer();
            _timer.Enabled = false;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
        }

        protected override void OnAppearing()
        {
            _statusBarService.SetDefaultStatusBar(); 
            base.OnAppearing();
            _timer.Elapsed += OnTimedEvent;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as ScannerResultViewModel)?.PauseTimer();
            _timer.Elapsed -= OnTimedEvent;
        }
    }
}