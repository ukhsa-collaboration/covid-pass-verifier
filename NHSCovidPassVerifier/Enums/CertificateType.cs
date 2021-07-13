using System.Collections.Generic;

namespace NHSCovidPassVerifier.Enums
{
    public enum CertificateType
    {
        International,
        Domestic
    }
    
    public static class CertificateTypeExtension
    {
        private static readonly Dictionary<string, CertificateType> TokenTypeDictionary = new Dictionary<string, CertificateType>
        {
            {"HC1", CertificateType.International}
        };

        public static CertificateType GetCertificateType(string prefix)
        {
            return TokenTypeDictionary.TryGetValue(prefix, out var result) ? result : CertificateType.Domestic;
        }
    }
}