using DeloitteDigital.Atlas.Extensions;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mapping.RenderingParameterMapping
{
    public class IntRenderingParameterMapping : IRenderingParameterMapping
    {
        public string MappingTypeKey => typeof (int).ToString();

        public void SetRenderingParameterMappingMapping<TModel>(TModel model, IPropertyMeta propertyMeta, RenderingContext renderingContext)
        {
            var intProperty = propertyMeta as IntPropertyMeta<TModel>;
            if (intProperty == null)
            {
                return;
            }

            intProperty.AssignValueToModelProperty(model, renderingContext.Rendering.GetParameter(propertyMeta.MappingName, 0));
        }
    }
}
