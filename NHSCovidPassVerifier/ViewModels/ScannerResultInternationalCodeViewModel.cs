using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NHSCovidPassVerifier.Models.Interfaces;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Models.International.Cards;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Utils;
using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;

namespace NHSCovidPassVerifier.ViewModels
{
    public class ScannerResultInternationalCodeViewModel : BaseViewModel
    {
        public override ICommand BackCommand => BackWithResultCommand;
        
        // Page Text
        public string Name { get; set; }
        public string DateOfBirthText { get; set; }
        public string CertificateExpirationText { get; set; }
        
        // Shared text
        public string TitleText { get; set; }
        public string DiseaseTargetedText { get; set; }
        public string CertificateIssuerText { get; set; }
        public string CertificateIdText { get; set; }
        public string ManufacturerText { get; set; }
        public string CountryOfTestText { get; set; }

        // Vaccination Card text
        public string VaccineProductText { get; set; }
        public string VaccineText { get; set; }
        public string CountryVaccinatedText { get; set; }
        public string ProductCodeText { get; set; }
        public string DoseLabelText { get; set; }
        
        // Recovery Card text
        public string RecoveryText { get; set; }
        public string FirstPositiveResultText { get; set; }
        public string DateValidUntilText { get; set; }


        // Test Result text
        public string TestText { get; set; }
        public string TestTypeText { get; set; }
        public string TestNameText { get; set; }
        public string SampleCollectedDateText { get; set; }
        public string TestResultTest { get; set; }
        public string TestCentre { get; set; }

        private ObservableCollection<IInternationalResultCard> _qrCodeResults;
        public ObservableCollection<IInternationalResultCard> QrCodeResults 
        {
            get => _qrCodeResults;
            set 
            { 
                _qrCodeResults = value;
                RaisePropertyChanged(() => QrCodeResults);
            } 
        }

        public bool JwkNeedsUpdating { get; set; }
        public bool JwkPresent { get; set; }

        private readonly IJwkService _jwkService;
        private readonly IQrDecoderService _qrDecoderService;

        public ScannerResultInternationalCodeViewModel(IJwkService jwkService, IQrDecoderService qrDecoderService)
        {
            _jwkService = jwkService;
            _qrDecoderService = qrDecoderService;

            InitText();
        }

        public void InitText()
        {
            TitleText = "INTERNATIONAL_SCANNER_RESULT_TITLE".Translate();
            DiseaseTargetedText = "INTERNATIONAL_SCANNER_RESULT_DISEASE_TARGETED_TEXT".Translate();
            CertificateIssuerText = "INTERNATIONAL_SCANNER_RESULT_CERTIFICATE_ISSUER".Translate();
            CertificateIdText = "INTERNATIONAL_SCANNER_RESULT_CERTIFICATE_ID_TEXT".Translate();
            CountryOfTestText = "INTERNATIONAL_SCANNER_RESULT_COUNTRY_OF_TEST".Translate();

            VaccineProductText = "INTERNATIONAL_SCANNER_RESULT_VACCINE_PRODUCT_TEXT".Translate();
            VaccineText = "INTERNATIONAL_SCANNER_RESULT_VACCINE_TEXT".Translate();
            CountryVaccinatedText = "INTERNATIONAL_SCANNER_RESULT_COUNTRY_OF_VACCINATION_TEXT".Translate();
            ManufacturerText = "INTERNATIONAL_SCANNER_RESULT_MANUFACTURER_TEXT".Translate();
            ProductCodeText = "INTERNATIONAL_SCANNER_RESULT_PRODUCT_CODE_TEXT".Translate();
            DoseLabelText = "INTERNATIONAL_SCANNER_RESULT_DOSE_TEXT".Translate();

            RecoveryText = "INTERNATIONAL_SCANNER_RESULT_RECOVERY".Translate();
            FirstPositiveResultText = "INTERNATIONAL_SCANNER_RESULT_FIRST_POSITIVE_RESULT".Translate();
            DateValidUntilText = "INTERNATIONAL_SCANNER_RESULT_DATE_VALID_UNTIL".Translate();

            TestText = "INTERNATIONAL_SCANNER_RESULT_RESULT_TEST".Translate();
            TestTypeText = "INTERNATIONAL_SCANNER_RESULT_RESULT_TEST_TYPE".Translate();
            TestNameText = "INTERNATIONAL_SCANNER_RESULT_RESULT_TEST_NAME".Translate();
            SampleCollectedDateText = "INTERNATIONAL_SCANNER_RESULT_RESULT_SAMPLE_COLLECTED_DATE".Translate();
            TestResultTest = "INTERNATIONAL_SCANNER_RESULT_RESULT_TEST_RESULT".Translate();
            TestCentre = "INTERNATIONAL_SCANNER_RESULT_RESULT_TEST_CENTRE".Translate();
        }

        public void SetPageData(InternationalCertificate certificate)
        {
            var subject = certificate.DecodedModel.hcert.euHcertV1Schema.InternationalCertificateSubject;
            Name = subject.GivenName + " " + subject.FamilyName;
            
            var dateOfBirth = certificate.DecodedModel.hcert.euHcertV1Schema.DateOfBirth;
            DateOfBirthText = "INTERNATIONAL_SCANNER_RESULT_DATE_OF_BIRTH".Translate(dateOfBirth?.ToLocalTime().FormatDate());

            var certificateExpirationDate = certificate.DecodedModel.exp.ConvertEpochToDate();

            CertificateExpirationText = "INTERNATIONAL_SCANNER_RESULT_CERTIFICATE_EXPIRATION"
                .Translate(certificateExpirationDate == null 
                    ? "INTERNATIONAL_SCANNER_RESULT_BLANK_FIELD".Translate() 
                    : certificateExpirationDate?.ToLocalTime().FormatDate());

            var vaccinationList = (IEnumerable<IInternationalResultCard>) certificate.DecodedModel.hcert.euHcertV1Schema
                .Vaccinations?
                .ConvertAll(x => (IInternationalResultCard) new VaccinationCard(x));

            var recoveryList = (IEnumerable<IInternationalResultCard>) certificate.DecodedModel.hcert.euHcertV1Schema
                .Recovery?
                .ConvertAll(x => new RecoveryCard(x));

            var testList = (IEnumerable<IInternationalResultCard>) certificate.DecodedModel.hcert.euHcertV1Schema
                .TestResults?
                .ConvertAll(x => new TestResultCard(x));

            QrCodeResults = (vaccinationList ?? Enumerable.Empty<IInternationalResultCard>())
                .Union(recoveryList ?? Enumerable.Empty<IInternationalResultCard>())
                .Union(testList ?? Enumerable.Empty<IInternationalResultCard>())
                .ToObservableCollection();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            JwkNeedsUpdating = await _jwkService.JwkListNeedsUpdating();
            JwkPresent = await _jwkService.JwkListPresent();
            SetPageData((InternationalCertificate) _qrDecoderService.GetDecodedCertificate());
            IsHelpMenuExpanded = false;
            
            await base.InitializeAsync(navigationData);
        }

        public override async Task ExecuteOnReturn(object data)
        {
            IsHelpMenuExpanded = false;
            await base.ExecuteOnReturn(data);
        }

    }
}
