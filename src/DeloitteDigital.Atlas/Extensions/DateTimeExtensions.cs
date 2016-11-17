using System;
using System.Diagnostics;
using System.Globalization;
using DeloitteDigital.Atlas.Refactoring;

namespace DeloitteDigital.Atlas.Extensions
{
    [LegacyCode]
    public static class DateTimeExtensions
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        /// <summary>
        /// Get the relative time for a date based on current date time e.g Over 2 minutes ago
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string RelativeDate(this DateTime date)
        {
            var timespan = DateTime.Now.Subtract(date);
            if (timespan.Days > 1)
                return date.ToString(CultureInfo.InvariantCulture);
            if (timespan.Hours > 1)
                return string.Format("Over {0} hours ago.", timespan.Hours);
            if (timespan.Minutes > 1)
                return string.Format("{0} minutes ago.", timespan.Minutes);
            return string.Format("{0} seconds ago.", timespan.Seconds);
        }

        /// <summary>
        /// Converts to unspecified date.
        /// date value will be converted to local server time first, and then will be converted to unspecified date time
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime ConvertToUnspecifiedDate(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Utc)
            {
                var tempDate = date.ToLocalTime();
                return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);
            }
            if (date.Kind == DateTimeKind.Unspecified)
            {
                return date;
            }
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }

}
