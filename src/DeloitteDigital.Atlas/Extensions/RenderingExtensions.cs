using System;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Extensions
{
    public static class RenderingExtensions
    {

        /// <summary>Get Sitecore Parameter value</summary>
        /// <param name="rendering">The Rendering.</param>
        /// <param name="name">The Parameter name.</param>
        /// <param name="defaultValue">The default value if the parameter is missing.</param>
        /// <returns>The Parameter value <see cref="string"/>.</returns>
        public static string GetParameter(this Rendering rendering, string name, string defaultValue = null)
        {

            if (rendering == null)
            {

                return defaultValue;
            }

            return rendering.Parameters[name] ?? defaultValue;
        }

        /// <summary>Get Sitecore Parameter value</summary>
        /// <param name="rendering">The Rendering.</param>
        /// <param name="name">The Parameter name.</param>
        /// <param name="defaultValue">The default value if the parameter is missing.</param>
        /// <returns>The Parameter value <see cref="string"/>.</returns>
        public static bool GetParameter(this Rendering rendering, string name, bool defaultValue)
        {

            if (rendering == null)
            {

                return defaultValue;
            }

            var value = rendering.GetParameter(name);
            return value == null ? defaultValue : (value.Equals("true", StringComparison.InvariantCultureIgnoreCase) || value == "1");
        }

        /// <summary>Get Sitecore Parameter value</summary>
        /// <param name="rendering">The Rendering.</param>
        /// <param name="name">The Parameter name.</param>
        /// <param name="defaultValue">The default value if the parameter is missing.</param>
        /// <returns>The Parameter value <see cref="string"/>.</returns>
        public static int GetParameter(this Rendering rendering, string name, int defaultValue)
        {

            if (rendering == null)
            {

                return defaultValue;
            }

            var value = rendering.GetParameter(name);
            if (value == null)
            {

                return defaultValue;
            }

            int result;
            int.TryParse(value, out result);
            return result;
        }

    }

}
