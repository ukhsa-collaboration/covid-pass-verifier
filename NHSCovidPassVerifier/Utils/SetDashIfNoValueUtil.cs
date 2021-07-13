using I18NPortable;
using System;
using System.Globalization;

namespace NHSCovidPassVerifier.Utils
{
    public static class SetDashIfNoValueUtil
    {

        public static string SetDashIfNoValue(this string str)
        {
            return string.IsNullOrEmpty(str) ? "INTERNATIONAL_SCANNER_RESULT_BLANK_FIELD".Translate() : str;
        }

        public static string SetDashIfNoValue(this DateTime? val)
        {
            return val.HasValue ? val.Value.FormatDate() : "INTERNATIONAL_SCANNER_RESULT_BLANK_FIELD".Translate();
        }


    }
}
