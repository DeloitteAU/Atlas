using System;
using System.Web;

namespace DeloitteDigital.Atlas.Extensions
{
    /// <summary>
    /// HTTP CONTEXT EXTENSIONS
    /// </summary>
    public static class PerHttpRequestFactory
    {
        /// <summary>
        /// Create an instance of T only once per http request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static T CreatePerRequest<T>(this HttpContext context, string key, Func<T> function)
        {
            if (context.Items.Contains(key)) return (T)context.Items[key];

            var result = function();
            // real friends don't cache nulls
            if (result == null) return result;

            context.Items[key] = result;

            return (T)context.Items[key];
        }

        /// <summary>
        /// Create an instance of T only once per http request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static T CreatePerRequest<T>(this HttpContextBase context, string key, Func<T> function)
        {
            if (context.Items.Contains(key)) return (T)context.Items[key];

            var result = function();
            // real friends don't cache nulls
            if (result == null) return result;

            context.Items[key] = result;

            return (T)context.Items[key];
        }
    }
}
