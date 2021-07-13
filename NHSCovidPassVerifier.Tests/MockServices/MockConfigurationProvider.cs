using System.IO;
using System.Reflection;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Tests.MockServices
{
    public class MockConfigurationProvider : IConfigurationProvider
    {
        public MockConfigurationProvider() { }
        
        public Stream GetConfiguration()
        {
            throw new System.NotImplementedException();
        }

        public Stream GetCommonConfiguration()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("NHSCovidPassVerifier.Tests.Resources.commonConfiguration.json");
        }

        public string GetEnvironment()
        {
            throw new System.NotImplementedException();
        }
    }
}