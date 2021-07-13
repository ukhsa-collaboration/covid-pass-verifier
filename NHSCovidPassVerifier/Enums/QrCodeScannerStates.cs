using System.ComponentModel;

namespace NHSCovidPassVerifier.Enums
{
    public enum QrCodeScannerStates
    {
        [Description("Valid Domestic QR Code")]
        ValidDomesticCertificate, 
        

        [Description("Expired Domestic QR Code")]
        InvalidDomesticCertificate,
        
        [Description("Valid International QR Code")]
        ValidInternationalCertificate,
        
        [Description("Disabled International QR Code")]
        DisabledInternationalCertificate,

        [Description("QR Code Not Recognised")]
        InvalidScan
    }
}
