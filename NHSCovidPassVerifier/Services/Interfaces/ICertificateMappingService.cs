namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface ICertificateMappingService
    {
        /// <summary>
        /// Get associated vaccine manufacturer name (contained in common configuration) from key parameter
        /// </summary>
        /// <param name="key">Manufacturer key defined by EU standard</param>
        /// <returns>Manufacturer name or key if no associated name is found</returns>
        public string GetManufacturer(string key);

        /// <summary>
        /// Get associated vaccine type name (contained in common configuration) from key parameter
        /// </summary>
        /// <param name="key">Vaccine type key defined by EU standard</param>
        /// <returns>Vaccine type name or key if no associated name is found</returns>
        public string GetVaccineType(string key);

        /// <summary>
        /// Get associated Disease Targerted name (contained in common configuration) from key parameter
        /// </summary>
        /// <param name="key">Disease Targerted key defined by EU standard</param>
        /// <returns>Disease Targerted name or key if no associated name is found</returns>
        public string GetDiseaseTargeted(string key);

        /// <summary>
        /// Get associated Vaccine name (contained in common configuration) from productCode parameter
        /// </summary>
        /// <param name="key">Product code for vaccine defined by EU standard</param>
        /// <returns>Vaccine name or key if no associated name is found</returns>
        public string GetVaccineName(string productCode);

        /// <summary>
        /// Get readable vacccine name (contained in common configuration) from key parameter
        /// </summary>
        /// <param name="key">Product code for vaccine defined by EU standard</param>
        /// <returns>Readable vacccine name or key if no associated name is found</returns>
        public string GetReadableVaccineName(string productCode);

        /// <summary>
        /// Get test type name (contained in common configuration) from key parameter
        /// </summary>
        /// <param name="key">Test type code for vaccine defined by EU standard</param>
        /// <returns>Test type name or key if no associated name is found</returns>
        public string GetTestType(string key);

        /// <summary>
        /// Get associated test result name (contained in common configuration) from key parameter
        /// </summary>
        /// <param name="key">Test result name key defined by EU standard</param>
        /// <returns>Test result name or key if no associated name is found</returns>
        public string GetTestResult(string key);

        /// <summary>
        /// Get associated test manufacturer name (contained in common configuration) from key parameter
        /// </summary>
        /// <param name="key">Manufacturer key defined by EU standard</param>
        /// <returns>Manufacturer name or key if no associated name is found</returns>
        public string GetTestManufacturer(string key);
    }
}