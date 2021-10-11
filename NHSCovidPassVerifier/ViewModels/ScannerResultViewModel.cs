using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;
using System;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
namespace NHSCovidPassVerifier.ViewModels
{
    public class ScannerResultViewModel : BaseViewModel
    {
        private IQrDecoderService _qrDecoderService;
        private readonly Timer _timer;
        private double _timerDecimal = 0;
        private int timerDurationMs;
        private const int timerIntervalMs = 10;

        public string Name { get; set; }
        public string ValidStatusText { get; set; }
        public string ExpiresDate { get; set; }
        public string ExpiresTime { get; set; }
        public string ExpiresText { get; set; }
        public double TimerProgress
        {
            get => _timerDecimal;
            set
            {
                _timerDecimal = value;
                RaisePropertyChanged(() => TimerProgress);
            }
        }
        public bool ValidPassport { get; set; }
        public bool ExpiredPassport { get; set; }
        public string ValidHelpText { get; set; }
        public string CovidStatus { get; set; }
        public string PauseText { get; set; }
        public string InvalidCode { get; set; }
        public bool InvalidPassport { get; set; }
        public string ExpiredText { get; set; }
        public bool ScanSuccess { get; set; }

        public ScannerResultViewModel(IQrDecoderService qrDecoderService)
        {
            _qrDecoderService = qrDecoderService;

            timerDurationMs = _settingsService.ScannerResultShownDuration;

            TimerProgress = 1.0;
            _timer = new Timer();
            _timer.Elapsed += OnTimedEvent;
            _timer.Interval = timerIntervalMs;
            _timer.Enabled = true;

            InitText();

        }

        public void SetDomesticCertificate(DomesticCertificate certificate)
        {
            ValidHelpText = "VALID_HELP_TEXT".Translate();
            CovidStatus = "NHS_COVID_STATUS".Translate();
            ExpiredText = "EXPIRED_TEXT".Translate();
            Name = certificate.FullName;
            ValidStatusText = certificate.CertificateState == CertificateState.Valid ? "Valid" : "";
            ExpiresText = certificate.CertificateState == CertificateState.Invalid ? "EXPIRED" : "VALID UNTIL";
            ExpiresDate = certificate.Expiry.ToLocalTime().ToString("dd MMM yyy");
            ExpiresTime = DateUtils.FormatTimeToMidnightMidday(certificate.Expiry.ToLocalTime());
            ExpiredPassport = certificate.CertificateState == CertificateState.Invalid ? true : false;
            ValidPassport = certificate.CertificateState == CertificateState.Valid ? true : false;
            RaisePropertyChanged(() => ValidHelpText);
            RaisePropertyChanged(() => CovidStatus);
            RaisePropertyChanged(() => ExpiredText);
            RaisePropertyChanged(() => Name);
            RaisePropertyChanged(() => ValidStatusText);
            RaisePropertyChanged(() => ExpiresText);
            RaisePropertyChanged(() => ExpiresDate);
            RaisePropertyChanged(() => ExpiresTime);
            RaisePropertyChanged(() => ExpiredPassport);
            RaisePropertyChanged(() => ValidPassport);
        }

        public void SetInternationalCertificate(InternationalCertificate certificate)
        {
            ValidHelpText = "VALID_HELP_TEXT".Translate();
            CovidStatus = "NHS_COVID_STATUS".Translate();
            ExpiredText = "EXPIRED_TEXT".Translate();

            var subject = certificate.DecodedModel.hcert.euHcertV1Schema.InternationalCertificateSubject;
            Name = subject.GivenName + " " + subject.FamilyName;

            var certificateExpirationDate = certificate.DecodedModel.exp.ConvertEpochToDate();

            ExpiresDate = certificateExpirationDate?.ToLocalTime().FormatDate();
            ValidStatusText = "Valid";
            ValidPassport = true;
            RaisePropertyChanged(() => ValidHelpText);
            RaisePropertyChanged(() => CovidStatus);
            RaisePropertyChanged(() => ExpiredText);
            RaisePropertyChanged(() => ExpiresDate);
            RaisePropertyChanged(() => Name);
            RaisePropertyChanged(() => ValidStatusText);
            RaisePropertyChanged(() => ValidPassport);
        }

        public void InitText()
        {
            PauseText = "QR_PAUSE_TEXT".Translate();
        }

        public void SetInvalidText()
        {
            InvalidCode = "INVALID_QR".Translate();
            InvalidPassport = true;
            RaisePropertyChanged(() => InvalidPassport);
            RaisePropertyChanged(() => InvalidCode);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (TimerProgress <= 0 && _timer.Enabled)
            {
                _timer.Enabled = false;
                Device.BeginInvokeOnMainThread(async () => await _navigationService.PopPage());

            }
            TimerProgress -= (double)timerIntervalMs / timerDurationMs;
        }

        public void PauseTimer()
        {
            _timer.Enabled = false;
        }

        public void ResumeTimer()
        {
            _timer.Enabled = true;
        }

        public override Task InitializeAsync(object navigationData)
        {
            ScanSuccess = (bool)navigationData;
            if (ScanSuccess)
            {
                var certificate = _qrDecoderService.GetDecodedCertificate();
                if (certificate.GetCertificateType() == CertificateType.Domestic)
                {
                    SetDomesticCertificate((DomesticCertificate)certificate);
                }
                else
                {
                    SetInternationalCertificate((InternationalCertificate)certificate);
                }

            }
            else
                SetInvalidText();

            return base.InitializeAsync(navigationData);
        }


    }
}
