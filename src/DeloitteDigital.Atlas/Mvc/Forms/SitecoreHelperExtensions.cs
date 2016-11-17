using System.Web.Mvc;
using Sitecore.Mvc.Helpers;

namespace DeloitteDigital.Atlas.Mvc.Forms
{
    public static class SitecoreHelperExtensions
    {
        public static MvcHtmlString RenderingToken(this SitecoreHelper helper)
        {
            if (helper.CurrentRendering == null) return null;

            var tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes["type"] = "hidden";
            tagBuilder.Attributes["name"] = Constants.Uid;
            tagBuilder.Attributes["value"] = helper.CurrentRendering.UniqueId.ToString();

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
