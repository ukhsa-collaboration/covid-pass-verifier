using System.IO;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets the apps configuration file
        /// </summary>
        /// <returns>a Stream of the configuration files contents.</returns>
        public Stream GetConfiguration();

        /// <summary>
        /// Gets the common configuration file, which contains mappings for the certificates.
        /// </summary>
        /// <returns>a Stream of the configuration files contents.</returns>
        public Stream GetCommonConfiguration();

        /// <summary>
        /// Gets the environment the app was built for.
        /// </summary>
        /// <returns>a String representing which environment the app was built for.</returns>
        public string GetEnvironment();
    }
}
