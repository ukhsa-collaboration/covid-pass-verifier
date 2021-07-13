namespace NHSCovidPassVerifier.Models.International
{
    public class InternationalQrCode
    {
        public string Name { get; set; }
        public string BatchNumber { get; set; }
        public string Issuer { get; set; }
        public string Id { get; set; }
        public string DiseaseTarget { get; set; }
        public string CountryOfVaccination { get; set; }
        public string AdministeringCentre { get; set; }

    }
}
