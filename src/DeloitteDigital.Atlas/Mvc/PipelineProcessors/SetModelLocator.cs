using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines;

namespace DeloitteDigital.Atlas.Mvc.PipelineProcessors
{
    public class SetModelLocator
    {
        public virtual void Process(PipelineArgs args)
        {
            MvcSettings.RegisterObject<ModelLocator>(() => new AdvancedModelLocator());
        }
    }
}
