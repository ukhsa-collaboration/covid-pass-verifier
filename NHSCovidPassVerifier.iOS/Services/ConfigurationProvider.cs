using System.IO;
using System.Linq;
using System.Reflection;
using NHSCovidPassVerifier.Services.Interfaces;
using Foundation;

namespace NHSCovidPassVerifier.iOS
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly string environmentVariable;

        public ConfigurationProvider()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(true);

            var config = attributes.OfType<AssemblyConfigurationAttribute>().FirstOrDefault();

            environmentVariable = config?.Configuration;
        }

        public Stream GetConfiguration()
        {
            var path = NSBundle.MainBundle.PathForResource($"appsettings.json", null);
            return File.OpenRead(path);
        }

        public Stream GetCommonConfiguration()
        {
            var path = NSBundle.MainBundle.PathForResource("commonConfiguration.json", null);
            return File.OpenRead(path);
        }

        public string GetEnvironment()
        {
            return environmentVariable;
        }
    }
}