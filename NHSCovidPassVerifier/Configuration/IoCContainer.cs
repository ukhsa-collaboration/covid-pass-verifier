using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Dtos;
using NHSCovidPassVerifier.Models.International;
using NHSCovidPassVerifier.Services;
using NHSCovidPassVerifier.Services.Interfaces;
using NHSCovidPassVerifier.Services.Repositories;
using NHSCovidPassVerifier.ViewModels;
using I18NPortable;
using TinyIoC;

namespace NHSCovidPassVerifier.Configuration
{
    public static class IoCContainer
    {
        private static TinyIoCContainer _container;

        static IoCContainer()
        {
            _container = new TinyIoCContainer();
            RegisterServices();
            RegisterViewModels();
        }

        public static void RegisterViewModels()
        {
            _container.Register<LandingViewModel>().AsSingleton();
            _container.Register<NhsSplashViewModel>().AsSingleton();
            _container.Register<QRScannerViewModel>().AsSingleton();
            _container.Register<TermsAndConditionsViewModel>().AsSingleton();
            _container.Register<AccessiblityViewModel>().AsSingleton();
            _container.Register<PrivacyPolicyViewModel>().AsSingleton();
            _container.Register<AboutPageViewModel>().AsSingleton();
            _container.Register<ScannerResultViewModel>();
            _container.Register<ScannerResultInternationalCodeViewModel>();
            _container.Register<ErrorViewModel>();
        }

        private static void RegisterServices()
        {
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<ICommonSettingsService, CommonSettingsService>();
            _container.Register<IQrDecoderService, QrDecoderService>();
            _container.Register<ISecureStorageService<JwkDto>, SecureStorageService<JwkDto>>();
            _container.Register<ISecureStorageService<TermsAndConditionsAgreed>, SecureStorageService<TermsAndConditionsAgreed>>();
            _container.Register<ISecureStorageService<InternationalEnabled>, SecureStorageService<InternationalEnabled>>();
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IQrValidatorService, QrECValidatorService>();
            _container.Register<IJwkRepository, JwkRepository>();
            _container.Register<ILoggingService, LoggingService>().AsSingleton();
            _container.Register<IRestClient, RestClient>();
            _container.Register<IJwkService, JwkService>();
            _container.Register<IConsoleService, ConsoleService>().AsSingleton();
            _container.Register<IAppCenterService, AppCenterService>().AsSingleton();
            _container.Register<IBase45Service, Base45Service>().AsSingleton();
            _container.Register<IZLibService, ZLibService>().AsSingleton();
            _container.Register<ICertificateMappingService, CertificateMappingService>();
            _container.Register<ILocaleReader, CachedLocaleReader>().AsSingleton();
            _container.Register<IInternationalEnabledService, InternationalEnabledService>().AsSingleton();
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static void RegisterInterface<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}