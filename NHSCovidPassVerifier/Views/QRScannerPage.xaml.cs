using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace NHSCovidPassVerifier.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScannerPage : ContentPage
    {
        private readonly ISettingsService _settingsService = IoCContainer.Resolve<ISettingsService>();

        public QRScannerPage()
        {
            InitializeComponent();
            BindingContext = IoCContainer.Resolve<QRScannerViewModel>();

            _scanView.OnScanResult += OnScanResult;
        }

        private void OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () => await ((QRScannerViewModel) BindingContext).HandleScanResult(result));
        }

        protected override async void OnAppearing()
        {
            MessagingCenter.Send(this, _settingsService.PreventLandscape);

            if (_scanView == null)
                await CreateScanner();
            else _scanView.IsAnalyzing = true;

            ((QRScannerViewModel)BindingContext).IsHelpMenuExpanded = false;

            base.OnAppearing();
        }

        private async Task CreateScanner()
        {
            if (_scanView != null) return;

            _scanView = new ZXingScannerView();
            _scanView.OnScanResult += OnScanResult;
            _scanView.Options = ((QRScannerViewModel)BindingContext).ScanningOptions;

            await Device.InvokeOnMainThreadAsync(() =>
            {
                if (_scanView == null) return;
                ScanContainer.Children.Add(_scanView);
                _scanView.IsScanning = true;
                _scanView.IsAnalyzing = true;
            });
        }

        protected override void OnDisappearing()
        {
            if (_scanView != null)
                _scanView.IsAnalyzing = false;
            base.OnDisappearing();
            MessagingCenter.Send(this, _settingsService.AllowLandScapePortrait);
            
        }

        public void DestroyScannerView()
        {
            if (_scanView == null) return;

            _scanView.IsScanning = false;
            _scanView.IsAnalyzing = false;
            _scanView.OnScanResult -= OnScanResult;
            ScanContainer.Children.Remove(_scanView);
            _scanView = null;
        }
    }
}