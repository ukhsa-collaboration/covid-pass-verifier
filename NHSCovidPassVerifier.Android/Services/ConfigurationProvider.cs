using Android.App;
using System.Linq;
using System.IO;
using System.Reflection;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Droid
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly  string environmentVariable;

        public ConfigurationProvider()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(true);

            var config = attributes.OfType<AssemblyConfigurationAttribute>().FirstOrDefault();

            environmentVariable = config?.Configuration;
        }

        public Stream GetConfiguration()
        {
            return Application.Context.Assets?.Open($"appsettings.json");
        }

        public Stream GetCommonConfiguration()
        {
            return Application.Context.Assets?.Open("commonConfiguration.json");
        }

        public string GetEnvironment()
        {
            return environmentVariable;
        }
    }
}