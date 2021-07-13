using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;
using System.Threading.Tasks;
using System.Windows.Input;
using NHSCovidPassVerifier.Models.Logging;

namespace NHSCovidPassVerifier.ViewModels
{
    public class ApprovePageViewModel : BaseViewModel
    {
        private readonly ILoggingService _loggingService;
        
        public string ApprovePageTitle { get; set; }
        public string ApproveToCText { get; set; }
        public string ToCLinkText { get; set; }
        public string ApproveToCText_2 { get; set; }
        public string AcceptButtonText { get; set; }
       
        public ICommand AgreeCommand => new AsyncCommand(async () => await ExecuteOnceAsync(EnableInternational));
        public override ICommand BackCommand => BackWithResultCommand;

        public ApprovePageViewModel(ILoggingService loggingService)
        {
            _loggingService = loggingService;
            
            InitText();
        }

        private void InitText()
        {
            ApprovePageTitle = "APPROVE_TITLE".Translate();
            ApproveToCText = "APPROVE_TEXT".Translate();
            ToCLinkText = "APPROVE_TERMS_AND_CONDITIONS_TEXT".Translate();
            ApproveToCText_2 = "APPROVE_TEXT_2".Translate();
            AcceptButtonText = "TERMS_AND_CONDITIONS_ACCEPT_TEXT".Translate();
        }

        private async Task EnableInternational()
        {
            await InnerToggleInternational(true);
            _loggingService.LogMessage(LogSeverity.INFO, "International Enabled");
            await _navigationService.PopPage();
        }
    }
}
