using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DeloitteDigital.Atlas.Extensions
{
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        ///     Get a NameValueCollection that represents the HttpRequest.QueryString
        /// </summary>
        /// <remarks>
        ///     QueryString is implemented with HttpValueCollection which is a .NET internal class.
        ///     It does not allow you to mutate the query string.
        /// </remarks>
        public static NameValueCollection SetKey(this NameValueCollection queryString, string key, object value)
        {
            // add or update the key
            queryString[key] = value.ToString();

            return queryString;
        }

        public static NameValueCollection RemoveKey(this NameValueCollection queryString, string key)
        {
            var dictionary = ToDictionary(queryString);

            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }

            return ToNameValueCollection(dictionary);
        }

        public static NameValueCollection ToNameValueCollection(this Dictionary<string, string> dictionary)
        {
            var nvc = new NameValueCollection(dictionary.Count);

            foreach (var item in dictionary)
            {
                nvc.Add(item.Key, item.Value);
            }

            return nvc;
        }

        public static Dictionary<string, string> ToDictionary(this NameValueCollection queryString)
        {
            return queryString
                .AllKeys
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToDictionary(key => key, key => queryString[key]);
        }

        /// <summary>
        ///     Return the querystring as a string of name value pairs, e.g. &amp;name=value&amp;name=value etc.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="includeEmptyValues"></param>
        /// <returns></returns>
        public static string ToQueryString(this NameValueCollection queryString, bool includeEmptyValues = false)
        {
            // Calling ToString on NameValueCollection returns "System.Collections.Specialized.NameValueCollection"
            // Calling ToString on the Request.QueryString (which appears to be of type NameValueCollection)
            // returns the key value pairs as a delimited string.
            // There is something weird about Request.QueryString.
            // As our instances are "real" NameValueCollection we can replicate the query string behaviour and join the values up nicely.
            // OK - Request.QueryString is really HttpValueCollection which is an internal class to the framework
            var d = ToDictionary(queryString);
            return d.Aggregate("", (keyString, kv) =>
            {
                if (!includeEmptyValues && string.IsNullOrWhiteSpace(kv.Value)) return keyString;

                return string.Format("{0}&{1}={2}",
                    keyString,
                    kv.Key,
                    kv.Value.UrlEncode());
            }).TrimStart('&');
        }

        public static bool GetBoolean(this NameValueCollection nvc, string key, bool defaultValue)
        {
            var boolean = GetBoolean(nvc, key);
            return boolean ?? defaultValue;
        }

        public static bool? GetBoolean(this NameValueCollection nvc, string key)
        {
            bool value;
            var stringValue = nvc[key];
            if (bool.TryParse(stringValue, out value))
                return value;

            if (string.IsNullOrEmpty(stringValue))
                return null;

            stringValue = stringValue.ToLowerInvariant();
            switch (stringValue)
            {
                case "1":
                case "t":
                    return true;
                case "0":
                case "f":
                    return false;
                default:
                    return null;
            }
        }

    }
}
