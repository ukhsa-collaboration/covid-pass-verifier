using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Services.ErrorHandlers;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NHSCovidPassVerifier.ViewModels.Base
{
    public abstract class BaseViewModel : BaseBindableObject
    {
        private bool _isLoading;
        private bool _isHelpMenuExpanded = false;
        protected static readonly INavigationService _navigationService;
        protected static readonly ISettingsService _settingsService;
        protected static readonly AppErrorHandler _appErrorHandler;
        protected static readonly IInternationalEnabledService _internationalEnabledService;

        public virtual ICommand BackCommand => new AsyncCommand(async () => await ExecuteOnceAsync(() =>_navigationService.PopPage(false)), errorHandler: _appErrorHandler);
        protected virtual ICommand BackWithResultCommand => new AsyncCommand(async () => await ExecuteOnceAsync(() => _navigationService.PopPageWithResult(false, true)), errorHandler: _appErrorHandler);
        public virtual ICommand PrivacyPolicyCommand => new AsyncCommand(async () => await ExecuteOnceAsync(PushPrivacyPolicyPage), errorHandler: _appErrorHandler);
        public virtual ICommand TermsAndConditionsCommand => new AsyncCommand(async () => await ExecuteOnceAsync(PushTermsAndConditionsPage), errorHandler: _appErrorHandler);
        public virtual ICommand AccessibilityCommand => new AsyncCommand(async () => await ExecuteOnceAsync(PushAccessibilityPage), errorHandler: _appErrorHandler);
        public virtual ICommand AboutCommand => new AsyncCommand(async () => await ExecuteOnceAsync(PushAboutPage), errorHandler: _appErrorHandler);
        public virtual ICommand FAQCommand => new AsyncCommand(async () => await ExecuteOnceAsync(PushFAQPage), errorHandler: _appErrorHandler);
        public ICommand HelpCommand => new AsyncCommand(async () => IsHelpMenuExpanded = !IsHelpMenuExpanded);

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
        public bool IsHelpMenuExpanded
        {
            get => _isHelpMenuExpanded;
            set
            {
                _isHelpMenuExpanded = value;
                RaisePropertyChanged(() => IsHelpMenuExpanded);
            }
        }
        public bool InternationalToggleEnabled
        {
            get => _internationalEnabledService.GetInternationalEnabled();
            set => ToggleInternational(value);
        }

        static BaseViewModel()
        {
            _navigationService = IoCContainer.Resolve<INavigationService>();
            _settingsService = IoCContainer.Resolve<ISettingsService>();
            _internationalEnabledService = IoCContainer.Resolve<IInternationalEnabledService>();
            _appErrorHandler = new AppErrorHandler();
        }

        public async Task ToggleInternational(bool newToggleValue)
        {
            if (newToggleValue == _internationalEnabledService.GetInternationalEnabled()) return;

            if (!newToggleValue)
            {
                await InnerToggleInternational(false);
            }
            else
            {
                await _navigationService.PushPage(new ApprovePage(), false);
            }
        }

        protected async Task InnerToggleInternational(bool newToggleValue)
        {
            await _internationalEnabledService.SetInternationalEnabled(newToggleValue);
            RaisePropertyChanged(() => InternationalToggleEnabled);
        }

        protected void SetIsLoading(bool value)
        {
            IsLoading = value;
        }

        public virtual Task ExecuteOnReturn(object data)
        {
            return Task.FromResult(false);
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        protected async Task ShowError(string title = null, string description = null, string buttonText = null, Func<Task> callback = null)
        {
            await _navigationService.GoToErrorPage(title, description, buttonText, callback);
        }

        protected async Task ExecuteOnceAsync(Func<Task> awaitableTask)
        {
            if (IsLoading) return;
            SetIsLoading(true);

            try
            {
                await awaitableTask();
            }
            catch (Exception ex)
            {
                _appErrorHandler.HandleError(ex);
            }
            finally
            {
                SetIsLoading(false);
            }
        }

        protected async Task PushTermsAndConditionsPage()
        {
            await _navigationService.PushPage(new TermsAndConditionsPage(), false, false);     
        }
        protected async Task PushAccessibilityPage()
        {
            await _navigationService.PushPage(new AccessibilityPage(), false);
        }
        protected async Task PushPrivacyPolicyPage()
        {
            await _navigationService.PushPage(new PrivacyPolicyPage(), false);
        }
        protected async Task PushAboutPage()
        {
            await _navigationService.PushPage(new AboutPage(), false, false);
        }

        protected async Task PushFAQPage()
        {
            await _navigationService.PushPage(new FAQPage(), false);
        }
    }
}
