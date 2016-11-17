using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using Sitecore.Data;

namespace DeloitteDigital.Atlas.Extensions
{
    public static class StringExtensions
    {
        public static bool IsShortIDFormat(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            Guid temp;
            return Guid.TryParseExact(value, "N", out temp);
        }

        /// <summary>
        /// Parse a string as a ShortID format string into a Sitecore ID
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A Sitecore ID</returns>
        /// <exception cref="ArgumentException"></exception>
        public static ID ParseShortID(this string value)
        {
            if (!value.IsShortIDFormat()) throw new ArgumentException("value is not in 32 digit ShortID format");

            Guid temp;
            Guid.TryParseExact(value, "N", out temp);
            return new ID(temp);
        }

        /// <summary>
        /// Parse a string as a Sitecore ID
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A Sitecore ID</returns>
        /// <exception cref="ArgumentException"></exception>
        public static ID ParseId(this string value)
        {
            ID temp;
            if (ID.TryParse(value, out temp)) return temp;

            throw new ArgumentException("value is not in a valid ID format");
        }

        public static bool TryParseShortID(this string value, out ID result)
        {
            result = ID.Null;

            if (!IsShortIDFormat(value)) return false;

            result = value.ParseShortID();

            return true;
        }



        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        /// <summary>
        /// Determines whether the specified string has value.
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>
        ///   <c>true</c> if the specified string has value; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(this string str)
        {
            return (!string.IsNullOrEmpty(str) && str != "");
        }

        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            bool retValue;
            bool.TryParse(str, out retValue);
            return retValue;
        }

        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="length">The length.</param>
        /// <param name="addString">The add string.</param>
        /// <returns></returns>
        public static string GetSummary(this string str, int length, string addString)
        {
            // rid of characters which will break stuff
            str = str.Replace("\n", "");
            str = str.Replace("\r", "");
            str = str.Replace("\t", "");

            // clean up any leading or whitespace.
            str = RemoveTags(str.Trim());

            if (str.Length > length)
            {
                // if string is null or empty return empty string.
                if (string.IsNullOrEmpty(str)) return "";

                // otherwise generate the summary.
                str = Regex.Match(str, @"^.{1," + length + @"}\b(?<!\s)").Value;
            }

            // throw the append string on no matter what... looks better being consistant.
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(addString))
                str = str.TrimEnd('.') + addString;

            return str;
        }

        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="length">The length.</param>
        /// <param name="addString">The add string.</param>
        /// <param name="forceAppendString">if set to <c>true</c> [force append string].</param>
        /// <returns></returns>
        public static string GetSummary(this string str, int length, string addString, bool forceAppendString)
        {
            string summary = str.GetSummary(length, addString);

            // throw the append string on no matter what... looks better being consistant.
            if (!string.IsNullOrEmpty(summary) && !string.IsNullOrEmpty(addString) && forceAppendString)
                summary = summary.TrimEnd('.') + addString;
            else if (!string.IsNullOrEmpty(summary) && summary.Length < str.Length)
                summary = summary.TrimEnd('.') + addString;

            return summary;
        }

        private static readonly Regex RemoveWhitespaceRegex = new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RemoveTagsRegex = new Regex("<[^>]*>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static string RemoveTags(this string value)
        {
            return string.IsNullOrEmpty(value)
                ? ""
                : RemoveTagsRegex.Replace(value, "");
        }

        /// <summary>
        ///     Strip Html tags from a string.  You should probably call HtmlEncode now...
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StripHtmlTags(this string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            // not very performant, but we *believe* it to be the most secure option available to us
            var doc = new HtmlDocument();

            doc.LoadHtml(value);

            // then call RemoveTags to remove html comments
            return doc.DocumentNode.InnerText.RemoveTags();
        }

        public static string RemoveWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? ""
                : RemoveWhitespaceRegex.Replace(value, " ").Trim();
        }

        public static string HtmlEncode(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        public static string HtmlDecode(this string value)
        {
            return HttpUtility.HtmlDecode(value);
        }

        public static string UrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        public static string UrlDecode(this string value)
        {
            return HttpUtility.UrlDecode(value);
        }

        public static string HtmlAttributeEncode(this string value)
        {
            return !string.IsNullOrEmpty(value) ? HttpUtility.HtmlAttributeEncode(value) : string.Empty;
        }
    }
}

