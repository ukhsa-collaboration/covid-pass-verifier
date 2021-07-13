using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace NHSCovidPassVerifier.Services
{
    public class AppCenterService : IAppCenterService
    {
        public void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(name, properties);
        }

        public void TrackError(
            Exception exception,
            IDictionary<string, string> properties = null,
            params ErrorAttachmentLog[] attachments)
        {
            Crashes.TrackError(exception, properties, attachments);
        }
    }
}
