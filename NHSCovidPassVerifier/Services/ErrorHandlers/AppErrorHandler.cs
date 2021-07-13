using System;
using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;

namespace NHSCovidPassVerifier.Services.ErrorHandlers
{
    public class AppErrorHandler : IAppErrorHandler
    {
        public void HandleError(Exception ex)
        {
            var loggingService = IoCContainer.Resolve<ILoggingService>();
            loggingService.LogException(LogSeverity.ERROR, ex, $"Application Error: {ex.Message}");
        }
    }
}