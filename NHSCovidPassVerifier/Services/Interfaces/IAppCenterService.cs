using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace NHSCovidPassVerifier.Services
{
    public interface IAppCenterService
    {
        void TrackEvent(string name, IDictionary<string, string> properties = null);

        void TrackError(Exception exception, IDictionary<string, string> properties = null, params ErrorAttachmentLog[] attachments);
    }
}