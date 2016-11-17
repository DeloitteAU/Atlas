using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mapping.RenderingParameterMapping
{
    public class RenderingParameterMapper : IMapper
    {
        public void HandleMapping<TModel>(
            TModel model,
            Item item,
            IPropertyMeta propertyMeta,
            ICache cache,
            IItemMapper itemMapper)
        {
            var renderingParameterMappings = MappingUtil.GetRenderingParameterMappers(cache);
            var renderingContext = RenderingContext.Current;
            if (renderingContext != null)
            {
                if (renderingParameterMappings.ContainsKey(propertyMeta.PropertyKey))
                {
                    renderingParameterMappings[propertyMeta.PropertyKey].SetRenderingParameterMappingMapping(
                        model,
                        propertyMeta,
                        renderingContext);
                }
            }
        }
    }
}
