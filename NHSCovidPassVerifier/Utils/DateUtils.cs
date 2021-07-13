using I18NPortable;
using System;
using System.Globalization;

namespace NHSCovidPassVerifier.Utils
{
    public static class DateUtils
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static string FormatDateTime(this DateTime date)
        {
            return date.ToString("d MMM yyyy 'at' HH\\:mm", CultureInfo.InvariantCulture);
        }
        public static string FormatDate(this DateTime date)
        {
            return date.ToString("d MMMM yyyy");
        }
        public static DateTime? ConvertEpochToDate(this int date)
        {
            if(date == 0) return null;
            return epoch.AddSeconds(date);
        }

        public static string FormatTimeToMidnightMidday(DateTime time) => time.Hour switch
            {
                0 when time.Minute == 0 => "MIDNIGHT_TEXT".Translate(),
                12 when time.Minute == 0 => "MIDDAY_TEXT".Translate(),
                _ => time.ToString("h\\.mm") + (time.Hour > 12 ? "pm" : "am")
            };
        }
}
