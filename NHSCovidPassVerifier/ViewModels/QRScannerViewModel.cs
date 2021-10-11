using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels.Base;
using NHSCovidPassVerifier.Views;
using I18NPortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ZXing;
using ZXing.Mobile;

namespace NHSCovidPassVerifier.ViewModels
{

    public class QRScannerViewModel : BaseViewModel
    {
        private readonly IQrDecoderService _qrDecoderService;
        private readonly ILoggingService _loggingService;
        private readonly IJwkService _jwkService;
        private readonly IInternationalEnabledService _internationalEnabledService;
        private readonly ICommonSettingsService _commonsettingsService;

        private ICertificate _certificate;

        public string CovidStatus { get; set; }
        public string AboutPageText { get; set; }
        public string ToggleVerifyText { get; set; }
        public string ToggleInternationalText { get; set; }
        public string Toggle2DBarcodeText { get; set; }

        public override ICommand BackCommand => new AsyncCommand(async () => await ExecuteOnceAsync(GoBackAndDestroyScanner), errorHandler: _appErrorHandler);
        public override ICommand AboutCommand => new AsyncCommand(async () => await ExecuteOnceAsync(GoToAboutPageDestroyScanner), errorHandler: _appErrorHandler);

        private static readonly int DelayBetweenContinousScans = 200;
        private static readonly int DelayBetweenAnalyzingFrames = 200;
        private static int MinResolutionHeightThreshold = 720;

        public MobileBarcodeScanningOptions ScanningOptions => new MobileBarcodeScanningOptions
        {
            DelayBetweenContinuousScans = DelayBetweenContinousScans,
            DelayBetweenAnalyzingFrames = DelayBetweenAnalyzingFrames,
            PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
            CameraResolutionSelector = SelectLowestResolution,
            TryInverted = true,
            TryHarder = true,
            InitialDelayBeforeAnalyzingFrames = 0
        };

        public QRScannerViewModel(IQrDecoderService qrDecoderService,
            ILoggingService loggingService,
            IJwkService jwkService,
            IInternationalEnabledService internationalEnabledService,
            ICommonSettingsService commonsettingsService)
        {
            _qrDecoderService = qrDecoderService;
            _loggingService = loggingService;
            _jwkService = jwkService;
            _internationalEnabledService = internationalEnabledService;
            _commonsettingsService = commonsettingsService;


            InitText();
        }

        private void InitText()
        {
            CovidStatus = "NHS_COVID_STATUS".Translate();
            AboutPageText = "DROP_DOWN_MENU_ABOUT_PAGE".Translate();

            ToggleVerifyText = "DROP_DOWN_MENU_TOGGLE_VERIFY".Translate();
            ToggleInternationalText = "DROP_DOWN_MENU_TOGGLE_INTERNATIONAL".Translate();
            Toggle2DBarcodeText = "DROP_DOWN_MENU_TOGGLE_2D_BARCODE".Translate();
        }

        /// <summary>
        /// Prevents camera preview distortion. Selects the lowest resolution within the tolerance of device aspect ratio.
        /// Lowest resolution is selected as the lower the resolution, the faster QR detection should be.
        /// </summary>
        /// <param name="availableResolutions">
        /// API generated list of available camera resolutions for the scanner view.
        /// </param>
        /// <returns>
        /// Lowest resolution within tolerance.
        /// </returns>
        private CameraResolution SelectLowestResolution(List<CameraResolution> availableResolutions)
        {
            CameraResolution result = null;
            double aspectTolerance = 0.1;
            var targetRatio = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait
                ? DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Width
                : DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Height;
            var targetHeight = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait
                ? DeviceDisplay.MainDisplayInfo.Height
                : DeviceDisplay.MainDisplayInfo.Width;
            var minDiff = double.MaxValue;

            availableResolutions
                .Where(r => Math.Abs(((double)r.Width / r.Height) - targetRatio) < aspectTolerance)
                .ForEach(
                    res =>
                    {
                        if (Math.Abs(res.Height - targetHeight) < minDiff &&
                            res.Height >= MinResolutionHeightThreshold)
                        {
                            minDiff = Math.Abs(res.Height - targetHeight);
                            result = res;
                        }
                    });

            return result;
        }

        public async Task HandleScanResult(Result result)
        {
            if (await IsResultOpen())
                return;

            var isValid = await _qrDecoderService.ValidateQrCode(result.Text);

            if (!isValid && await HasJwkUpdated())
                isValid = await _qrDecoderService.ValidateQrCode(result.Text);

            if (isValid)
            {
                await OnScanSuccess();
                return;
            }

            _loggingService.LogMessage(Models.Logging.LogSeverity.INFO, QrCodeScannerStates.InvalidScan.ToDescriptionString());

            await PushPage(new ScannerResultPage(), false, false);

        }

        private async Task OnScanSuccess()
        {
            _certificate = _qrDecoderService.GenerateCertificateFromQrCode();
            var certificateType = _certificate.GetCertificateType();
            var internationalEnabled = _internationalEnabledService.GetInternationalEnabled();
            var isEnglandVaccination = false;
            var isCorrectNumberOfDoses = false;

            switch (_certificate)
            {
                case DomesticCertificate domesticCertificate:
                    _loggingService.LogMessage(Models.Logging.LogSeverity.INFO,
                        domesticCertificate.CertificateState == CertificateState.Invalid
                            ? QrCodeScannerStates.InvalidDomesticCertificate.ToDescriptionString()
                            : QrCodeScannerStates.ValidDomesticCertificate.ToDescriptionString());
                    break;
                case InternationalCertificate internationalCertificate:
                    var issuer = string.IsNullOrEmpty(internationalCertificate.DecodedModel.iss)
                        ? "N/A"
                        : internationalCertificate.DecodedModel.iss;
                    var customProperties = new Dictionary<string, string> { { "Issuer", issuer } };
                    _loggingService.LogMessage(Models.Logging.LogSeverity.INFO,
                        internationalEnabled
                            ? QrCodeScannerStates.ValidInternationalCertificate.ToDescriptionString()
                            : QrCodeScannerStates.DisabledInternationalCertificate.ToDescriptionString(),
                        customProperties);
                    var certificateVaccination = internationalCertificate.DecodedModel.hcert.euHcertV1Schema.Vaccinations?.FirstOrDefault();
                    if (certificateVaccination != null)
                    {
                        isEnglandVaccination = _commonsettingsService.EnglishCertificateIssuers.Contains(certificateVaccination.CertificateIssuer);
                        isCorrectNumberOfDoses = _commonsettingsService.InternationalMinimumDoses.TryGetValue(certificateVaccination.ProductCode, out var minimumDoses)
                                                 && certificateVaccination.DoseNumber >= minimumDoses;
                    }
                    break;
            }


            if (certificateType == CertificateType.Domestic)
            {
                await PushPage(new ScannerResultPage(), false, true);
            }
            else if (internationalEnabled)
            {
                await PushPage(new ScannerResultInternationalCodePage(), false, true);
            }
            else if (!internationalEnabled && certificateType == CertificateType.International && !isEnglandVaccination && isCorrectNumberOfDoses)
            {
                await PushPage(new ScannerResultPage(), false, true);
            }
            else
            {
                await PushPage(new ScannerResultPage(), false, false);
            }
        }

        private async Task PushPage(Page page, bool animated, bool data)
        {
            if (await IsResultOpen()) return;
            IsHelpMenuExpanded = false;
            await _navigationService.PushPage(page, animated, data);
        }

        /// <returns>
        /// A bool that is true if the JWK List has been updated
        /// </returns>
        public async Task<bool> HasJwkUpdated()
        {
            return await _jwkService.JwkListNeedsUpdating() && await _jwkService.UpdateJwkList();
        }


        public override async Task InitializeAsync(object navigationData)
        {
            if (await _jwkService.JwkListNeedsUpdating())
                await _jwkService.UpdateJwkList();
            IsHelpMenuExpanded = false;
            await base.InitializeAsync(navigationData);
        }

        public override async Task ExecuteOnReturn(object data)
        {
            RaisePropertyChanged(() => InternationalToggleEnabled);
            IsHelpMenuExpanded = false;
            await base.ExecuteOnReturn(data);
        }

        private async Task<bool> IsResultOpen()
        {
            var page = _navigationService.FindCurrentPage();
            return page is ScannerResultInternationalCodePage
                || page is ScannerResultPage;
        }

        private async Task GoBackAndDestroyScanner()
        {
            if (_navigationService.FindCurrentPage() is QRScannerPage qr)
                qr.DestroyScannerView();

            await _navigationService.PopPage(false);
        }
        private async Task GoToAboutPageDestroyScanner()
        {
            if (_navigationService.FindCurrentPage() is QRScannerPage qr)
                qr.DestroyScannerView();

            await PushAboutPage();
        }
    }
}
