using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mapping.RenderingParameterMapping
{
    public interface IRenderingParameterMapping : ITypeMapper
    {
        void SetRenderingParameterMappingMapping<TModel>(TModel model, IPropertyMeta propertyMeta,RenderingContext renderingContext);
    }
}