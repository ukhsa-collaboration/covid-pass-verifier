using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Logging;
using System;
using System.Collections.Generic;

namespace NHSCovidPassVerifier.Services.Interfaces
{
    public interface ILoggingService
    {
        void LogException(LogSeverity severity, Exception e, string additionalInfo = null);
        void LogApiError<T>(LogSeverity severity, ApiResponse<T> apiResponse, string additionalInfo = null);
        void LogMessage(LogSeverity severity, string message, Dictionary<string, string> customProperties = null);
    }
}
