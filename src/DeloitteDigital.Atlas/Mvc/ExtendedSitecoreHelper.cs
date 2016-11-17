using System.Web.Mvc;

namespace DeloitteDigital.Atlas.Mvc
{
    public class ExtendedSitecoreHelper
    {
        protected HtmlHelper HtmlHelper { get; set; }

        public ExtendedSitecoreHelper(HtmlHelper htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }


    }
}
