using System;
using System.Reflection;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mvc.Forms
{
    public class ValidRenderingTokenAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var rendering = RenderingContext.CurrentOrNull;
            if (rendering == null) return false;

            Guid postedId;
            return Guid.TryParse(controllerContext.HttpContext.Request.Form[Constants.Uid], out postedId) && postedId.Equals(rendering.Rendering.UniqueId);
        }
    }
}
