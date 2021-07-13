using System;

namespace NHSCovidPassVerifier.Services.ErrorHandlers
{
    public interface IAppErrorHandler
    {
        void HandleError(Exception ex);
    }
}