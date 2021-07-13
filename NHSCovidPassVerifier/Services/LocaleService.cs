using System.Reflection;
using NHSCovidPassVerifier.Configuration;
using I18NPortable;

namespace NHSCovidPassVerifier.Services
{
    //See nuget https://github.com/xleon/I18N-Portable
    public static class LocalesService
    {
        public static void Initialize()
        {
            if (I18N.Current?.Locale == null)
            {
                I18N.Current
                .SetNotFoundSymbol("$") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                .SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
                .AddLocaleReader(IoCContainer.Resolve<ILocaleReader>(), ".json") //Use the json parser
                .Init(typeof(LocalesService).GetTypeInfo().Assembly);
            }

            SetInternationalization();
        }

        public static void SetInternationalization()
        {
            I18N.Current.Locale = "en";
        }
    }
}
