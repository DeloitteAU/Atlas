using System;
using System.ComponentModel;
using DeloitteDigital.Atlas.Refactoring;

namespace DeloitteDigital.Atlas.Extensions
{
    [LegacyCode]
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum val)
        {
            if (val == null)
            {
                return string.Empty;
            }

            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}