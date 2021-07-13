using System.Collections.Generic;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface ICommonSettingsService
    {
        /// <summary>
        /// Dictionary containing Vaccine Manufacturers
        /// </summary>
        public IDictionary<string, string> VaccineManufacturers { get; }

        /// <summary>
        /// Dictionary containing Vaccine Types
        /// </summary>
        public IDictionary<string, string> VaccineTypes { get; }

        /// <summary>
        /// Dictionary containing Diseases Targeted
        /// </summary>
        public IDictionary<string, string> DiseasesTargeted { get; }

        /// <summary>
        /// Dictionary containing Vaccine Names
        /// </summary>
        public IDictionary<string, string> VaccineNames { get; }

        /// <summary>
        /// Dictionary containing Readable Vaccine Names
        /// </summary>
        public IDictionary<string, string> ReadableVaccineNames { get; }

        /// <summary>
        /// Dictionary containing Test Types
        /// </summary>
        public IDictionary<string, string> TestTypes { get; }

        /// <summary>
        /// Dictionary containing Test Results
        /// </summary>
        public IDictionary<string, string> TestResults { get; }

        public IDictionary<string, string> TestManufacturers { get; }
    }
}