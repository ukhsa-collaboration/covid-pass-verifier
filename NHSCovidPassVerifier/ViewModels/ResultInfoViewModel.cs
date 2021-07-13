using NHSCovidPassVerifier.Enums;
using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NHSCovidPassVerifier.ViewModels
{
    public class ResultInfoViewModel : BaseViewModel
    {
        private string _titleText { get; set; }
        private string _paragraphText { get; set; }
        public string _paragraphText2 { get; set; }
        private bool _isVaccination { get; set; } = false;
        private bool _isRecovery { get; set; } = false;
        private bool _isTestResult { get; set; } = false;

        public bool IsVaccination
        {
            get => _isVaccination;
            private set
            {
                _isVaccination = value;
                RaisePropertyChanged(() => IsVaccination);
            }
        }
        public bool IsRecovery
        {
            get => _isRecovery;
            private set
            {
                _isRecovery = value;
                RaisePropertyChanged(() => IsRecovery);
            }
        }
        public bool IsTestResult
        {
            get => _isTestResult;
            private set
            {
                _isTestResult = value;
                RaisePropertyChanged(() => IsTestResult);
            }
        }

        public string TitleText
        {
            get => _titleText;
            private set
            {
                _titleText = value;
                RaisePropertyChanged(() => TitleText);
            }
        }

        public string ParagraphText
        {
            get => _paragraphText;
            private set
            {
                _paragraphText = value;
                RaisePropertyChanged(() => ParagraphText);
            }
        }
        public string ParagraphText2
        {
            get => _paragraphText2;
            private set
            {
                _paragraphText2 = value;
                RaisePropertyChanged(() => ParagraphText2);
            }
        }

        public ResultInfoViewModel()
        {
        }

        private void SetPageText(InfoResultType InfoType)
        {
            switch (InfoType)
            {
                case InfoResultType.Vaccination:
                    TitleText = "INFO_VACCINATION_TITLE".Translate();
                    ParagraphText = "INFO_VACCINATION_TEXT".Translate();
                    ParagraphText2 = "INFO_VACCINATION_TEXT_2".Translate();
                    IsVaccination = true;
                    break;
                case InfoResultType.Recovery:
                    TitleText = "INFO_RECOVERY_TITLE".Translate();
                    ParagraphText = "INFO_RECOVERY_TEXT".Translate();
                    ParagraphText2 = "INFO_RECOVERY_TEXT_2".Translate();
                    IsRecovery = true;
                    break;
                case InfoResultType.TestResult:
                    TitleText = "INFO_TEST_TITLE".Translate();
                    ParagraphText = "INFO_TEST_TEXT".Translate();
                    IsTestResult = true;
                    break;
                default:
                    break;
            }
        }

        public void ResetPageData()
        {
            IsVaccination = false;
            IsRecovery = false;
            IsTestResult = false;
        }

        public override Task InitializeAsync(object navigationData)
        {
            var InfoType = (InfoResultType)navigationData;
            ResetPageData();
            SetPageText(InfoType);
            return base.InitializeAsync(navigationData);
        }
    }
}
