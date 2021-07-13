namespace NHSCovidPassVerifier.Enums
{
    public enum NhsPageState
    {
        Success = 200,
        CertificateNotFound = 204,
        Unauthorized = 401, 
        NotFound = 404,
        MethodNotAllowd = 405,
        RequestTimeout = 408,
        InternalServerError = 500,
        ServiceNotAvailable = 503
    }

}
