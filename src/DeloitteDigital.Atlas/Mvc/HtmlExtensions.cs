using System;
using System.Web.Mvc;
using DeloitteDigital.Atlas.FieldRendering;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mvc
{
    public static class HtmlExtensions
    {
        public static MvcEditFrame BeginEditFrame<T>(this HtmlHelper<T> helper, Item item, string buttons)
        {
            return item == null ? null : BeginEditFrame(helper, item.Paths.FullPath, buttons);
        }

        public static MvcEditFrame BeginEditFrame<T>(this HtmlHelper<T> helper, string dataSource, string buttons)
        {
            var frame = new MvcEditFrame(helper.ViewContext.Writer, dataSource, buttons);
            return frame;
        }        

        public static IDisposable BeginLink(this HtmlHelper htmlHelper, ILinkFieldRenderingString linkField, string alternateTag = null, bool skipInExperienceEditor = false)
        {
            if (skipInExperienceEditor && global::Sitecore.Context.PageMode.IsExperienceEditor)
                return new EmptyMvcLink();
            return new MvcLink(htmlHelper.ViewContext, linkField, alternateTag);
        }
    }
}
