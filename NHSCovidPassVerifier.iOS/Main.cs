using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.Models.Logging;
using NHSCovidPassVerifier.Services.Interfaces;
using System;
using UIKit;

namespace NHSCovidPassVerifier.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception e)
            {
                var loggingService = IoCContainer.Resolve<ILoggingService>();
                loggingService.LogException(LogSeverity.ERROR, e);
                throw e;
            }

        }
    }
}
