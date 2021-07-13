using System;
using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;
using System.Threading.Tasks;
using System.Windows.Input;
using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.ViewModels
{
    public sealed class ErrorViewModel : BaseViewModel
    {
        private string _title;
        private string _description;
        private string _buttonText;
        private ICommand _buttonCommand;
        public override ICommand BackCommand => new AsyncCommand(async () => await ExecuteOnceAsync(OpenLandingPage), errorHandler: _appErrorHandler);

        public ICommand OpenSettingsCommand => new AsyncCommand(async () =>
        {
            await ExecuteOnceAsync(async () =>
            {
                await IoCContainer.Resolve<IDeeplinkingService>().GoToAppSettings();
            });
        });

        public string Title
        {
            get => _title;
            private set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string Description
        {
            get => _description;
            private set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            } 
        }

        public string ButtonText
        {
            get => _buttonText;
            private set
            {
                _buttonText = value;
                RaisePropertyChanged(() => ButtonText);
            } 
        }

        public ICommand ButtonCommand
        {
            get => _buttonCommand;
            set
            {
                _buttonCommand = value;
                RaisePropertyChanged(() => ButtonCommand);
            }
        }

        public ErrorViewModel()
        {
            Title = "GENERIC_ERROR_TITLE".Translate();
            Description = "GENERIC_ERROR_DESCRIPTION".Translate();
            ButtonText = "GO_TO_SETTINGS".Translate();
            ButtonCommand = OpenSettingsCommand;
        }
        public async Task OpenLandingPage()
        {
            _navigationService.OpenLandingPage();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is object[] data)
            {
                if (!string.IsNullOrWhiteSpace(data[0] as string)) Title = data[0] as string;
                if (!string.IsNullOrWhiteSpace(data[1] as string)) Description = data[1] as string;
                if (!string.IsNullOrWhiteSpace(data[2] as string)) ButtonText = data[2] as string;
                if (data[3] is Func<Task> callback) ButtonCommand = new AsyncCommand(callback);
            }

            await base.InitializeAsync(navigationData);
        }
    }
}
