using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using DeloitteDigital.Atlas.Refactoring;

namespace DeloitteDigital.Atlas.Extensions
{
    [LegacyCode]
    public static class UrlExtensions
    {
        /// <summary>
        /// Gets the writeable querystring collection.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns></returns>
        public static NameValueCollection GetWriteableQuerystringCollection(this HttpRequest httpRequest)
        {
            return httpRequest.QueryString.AsWriteable();
        }

        /// <summary>
        /// Ases the writeable.
        /// </summary>
        /// <param name="nameValueCollection">The name value collection.</param>
        /// <returns></returns>
        public static NameValueCollection AsWriteable(this NameValueCollection nameValueCollection)
        {
            if (nameValueCollection != null && nameValueCollection.Count > 0)
                return new NameValueCollection(nameValueCollection.Count, nameValueCollection);

            return new NameValueCollection();
        }

        /// <summary>
        /// Gets the full URL.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static Uri GetFullUrl(this HttpRequest context)
        {
            string fullPath = string.Format("{0}://{1}{2}", context.Url.Scheme, context.Url.Authority, context.RawUrl);

            var uri = new Uri(fullPath);
            return uri;
        }

        /// <summary>
        /// Gets the path without query.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static string GetPathWithoutQuery(this Uri uri)
        {
            return uri.GetLeftPart(UriPartial.Path);
        }

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns></returns>
        public static string BuildUrl(this Uri uri, NameValueCollection queryStringParams)
        {
            if (uri != null)
            {
                if (queryStringParams != null && queryStringParams.Count > 0)
                {
                    string queryString = queryStringParams.ToQueryString(null, true);
                    return uri.GetPathWithoutQuery() + "?" + queryString;
                }
                
                return uri.GetPathWithoutQuery();
            }

            return string.Empty;
        }

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns></returns>
        public static string BuildUrl(this HttpRequest httpContext, NameValueCollection queryStringParams)
        {
            var uri = httpContext.GetFullUrl();
            return uri.BuildUrl(queryStringParams);
        }

        /// <summary>
        /// Constructs a NameValueCollection into a query string.
        /// </summary>
        /// <remarks>Consider this method to be the opposite of "System.Web.HttpUtility.ParseQueryString"</remarks>
        /// <param name="parameters">The NameValueCollection</param>
        /// <param name="delimiter">The String to delimit the key/value pairs</param>
        /// <param name="omitEmpty"></param>
        /// <returns>A key/value structured query string, delimited by the specified String</returns>
        public static string ToQueryString(this NameValueCollection parameters, string delimiter, bool omitEmpty)
        {
            if (string.IsNullOrEmpty(delimiter))
                delimiter = "&";

            const char equals = '=';
            if (parameters != null && parameters.Count > 0)
            {
                var items = new List<string>();
                for (int i = 0; i < parameters.Count; i++)
                {
                    foreach (string value in parameters.GetValues(i))
                    {
                        bool addValue = !(omitEmpty) || !string.IsNullOrEmpty(value);
                        if (addValue)
                            items.Add(string.Concat(parameters.GetKey(i), equals, HttpUtility.UrlEncode(value)));
                    }
                }
                return string.Join(delimiter, items.ToArray());
            }

            return string.Empty;
        }
    }
}
