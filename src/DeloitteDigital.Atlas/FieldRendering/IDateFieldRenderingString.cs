using System;
using System.Web;

namespace DeloitteDigital.Atlas.FieldRendering
{
    public interface IDateFieldRenderingString : IFieldRenderingString
    {
        DateTime GetDateTime();
        HtmlString GetDateTime(string formatString);
    }
}