using NHSCovidPassVerifier.Models;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NHSCovidPassVerifier.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly IConsoleService _consoleService;
        private readonly IAppCenterService _appCenterService;

        public LoggingService(IConsoleService consoleService, IAppCenterService appCenterService)
        {
            _consoleService = consoleService;
            _appCenterService = appCenterService;
        }

        public void LogApiError<T>(LogSeverity severity, ApiResponse<T> apiResponse, string additionalInfo = null)
        {
            var dict = GetAdditionalInfo(severity, apiResponse, additionalInfo);
            _appCenterService.TrackEvent("API Error", dict);
            PrettyPrintDictToConsole(dict, severity, apiResponse.Exception);
        }

        public void LogException(LogSeverity severity, Exception e, string additionalInfo = null)
        {
            var dict = GetAdditionalInfo(severity, null, additionalInfo);
            _appCenterService.TrackError(e, dict);
            PrettyPrintDictToConsole(dict, severity, e);
        }

        public void LogMessage(LogSeverity severity, string message, Dictionary<string, string> customProperties = null)
        {
            var dict = GetAdditionalInfo(severity, null, null, customProperties);
            _appCenterService.TrackEvent(message, dict);
            PrettyPrintDictToConsole(dict, severity);
        }

        void PrettyPrintDictToConsole(IDictionary<string, string> logObj, LogSeverity severity, Exception e = null)
        {
            _consoleService.PrintToDebugConsole($"Logged {severity.ToString()}:");
            _consoleService.PrintToDebugConsole("{");

            if (logObj != null)
                foreach (var kvp in logObj)
                    _consoleService.PrintToDebugConsole($"   {kvp.Key}: {kvp.Value}:");

            _consoleService.PrintToDebugConsole("}");

            if (e == null) return;
            _consoleService.PrintToDebugConsole($"With exception [{e?.GetType()?.Name}]: {e?.Message}");
            _consoleService.PrintToDebugConsole($"{e?.StackTrace}");
        }


        /// <summary>
        /// Create mandatory additional information dictionary 
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="apiResponse"></param>
        /// <param name="additionalInfo"></param>
        /// <returns></returns>
        private IDictionary<string, string> GetAdditionalInfo(LogSeverity severity, ApiResponse apiResponse = null,
            string additionalInfo = null, Dictionary<string, string> customProperties = null)
        {
            var dict = new Dictionary<string, string>
            {
                //Mandatory information 
                { "Severity", severity.ToString() }
            };

            if (apiResponse != null)
            {
                var endPoint = apiResponse.Endpoint;
                var errorCode = apiResponse.StatusCode > 0 ? apiResponse.StatusCode.ToString() : "";
                var errorMessage = (new string[] { "200", "201" }).Contains(errorCode) ? "" : apiResponse.ResponseText;

                dict.Add("API", "/" + endPoint);
                dict.Add("ApiErrorCode", errorCode);
                dict.Add("ApiErrorMessage", errorMessage);
            }

            if (additionalInfo != null)
                dict.Add("AdditionalInfo", additionalInfo);

            if (customProperties != null)
            {
                foreach (var p in customProperties)
                {
                    dict.Add(p.Key, p.Value);
                }
            }

            return dict;
        }

    }
}
