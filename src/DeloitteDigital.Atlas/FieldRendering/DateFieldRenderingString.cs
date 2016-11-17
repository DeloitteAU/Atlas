using System;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.FieldRendering
{
    public class DateFieldRenderingString : FieldRenderingString, IDateFieldRenderingString
    {
        public DateFieldRenderingString(Item item, string fieldName) : base(item, fieldName) { }
        
        public DateTime GetDateTime()
        {
            return DateUtil.IsoDateToDateTime(RawValue());
        }

        public HtmlString GetDateTime(string formatString)
        {
            if (Context.PageMode.IsExperienceEditorEditing)
            {
                return new HtmlString(Sitecore.Web.UI.WebControls.FieldRenderer.Render(
                  Item,
                  FieldName,
                  "format=" + formatString));
            }

            var dateTime = GetDateTime();
            return new HtmlString(dateTime.ToString(formatString));
        }
    }
}
