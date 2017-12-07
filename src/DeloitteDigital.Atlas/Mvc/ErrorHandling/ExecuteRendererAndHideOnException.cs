using System;
using DeloitteDigital.Atlas.Logging;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mvc.ErrorHandling
{
    /// <summary>
    /// Custom renderer that catches exception in ViewRenderings at render time and hides the rendering. 
    /// NOTE: This does not extend to controller renderings. 
    /// Loosly based on https://github.com/GuitarRich/sitecore-exception-handler
    /// <seealso cref="IHideOnError"/>
    /// <seealso cref="ICustomErrorView"/>
    /// </summary>
    public class ExecuteRendererAndHideOnException : ExecuteRenderer
    {
        // TODO: this should come from a configuration service
        public virtual string ErrorViewFile => "~/Modules/Shared/Errors/FailedViewRendering.cshtml";

        public override void Process(RenderRenderingArgs args)
        {
            try
            {
                base.Process(args);
            }
            catch (Exception ex)
            {
                // don't handle the exception if the rendering is not a view rendering
                var rendererAsViewRenderer = args.Rendering.Renderer as ViewRenderer;
                if (rendererAsViewRenderer == null)
                    throw;

                // don't handle the exception if the rendering has not opted in to the error handling
                if (!(rendererAsViewRenderer.Model is IHideOnError))
                    throw;

                args.Cacheable = false;

                try
                {
                    var logService = (ILogService) Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(ILogService));
                    logService.Error($"Error rendering {args.Rendering.Renderer}", ex, this);
                }
                catch (Exception) { } // the ILogService interface has not been configured

                var rendererWithCustomErrorView = rendererAsViewRenderer.Model as ICustomErrorView;

                RenderError(rendererWithCustomErrorView == null ? ErrorViewFile : rendererWithCustomErrorView.ErrorViewFile,
                    GetErrorRenderingModel(args.Rendering.Renderer.ToString(), ex), args);
            }
        }

        public virtual ErrorRenderingModel GetErrorRenderingModel(string failedRenderingName, Exception exception)
        {
            return new ErrorRenderingModel(failedRenderingName, exception);
        }

        public virtual void RenderError(string viewName, ErrorRenderingModel errorRenderingModel, RenderRenderingArgs args)
        {
            var vr = new ViewRenderer { ViewPath = viewName, Model = errorRenderingModel };
            vr.Render(args.Writer);
        }

    }
}
