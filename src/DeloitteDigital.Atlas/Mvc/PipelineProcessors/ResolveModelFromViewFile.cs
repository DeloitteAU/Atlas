using System.Web.Compilation;
using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mvc.PipelineProcessors
{
    public class ResolveModelFromViewFile : GetModelProcessor
    {
        protected virtual object GetFromViewPath(Rendering rendering, GetModelArgs args)
        {
            var path = rendering.Renderer is ViewRenderer 
                ? ((ViewRenderer)rendering.Renderer).ViewPath 
                : rendering.ToString().Replace("View: ", string.Empty);

            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            // don't act on SPEAK or other Sitecore views
            if (path.Contains("sitecore/shell"))
            {
                return null; 
            }

            // Retrieve the compiled view
            var compiledViewType = BuildManager.GetCompiledType(path);
            var baseType = compiledViewType.BaseType;

            // Check to see if the view has been found and that it is a generic type
            if (baseType == null || !baseType.IsGenericType)
            {
                return null;
            }

            var modelType = baseType.GetGenericArguments()[0];

            // Check to see if no model has been set
            if (modelType == typeof(object))
            {
                return null;
            }

            var fullyQualifiedName = modelType.FullName + ", " + modelType.Assembly.GetName().Name;

            return args.ModelLocator.GetModel(fullyQualifiedName, true);
        }

        public override void Process(GetModelArgs args)
        {
            if (args.Result == null)
            {
                args.Result = GetFromViewPath(args.Rendering, args);
            }
        }
    }
}
