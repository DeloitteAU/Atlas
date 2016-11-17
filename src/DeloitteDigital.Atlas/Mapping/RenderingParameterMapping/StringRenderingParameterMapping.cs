using DeloitteDigital.Atlas.Extensions;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mapping.RenderingParameterMapping
{
    public class StringRenderingParameterMapping : IRenderingParameterMapping
    {
        public string MappingTypeKey => typeof (string).ToString();

        public void SetRenderingParameterMappingMapping<TModel>(TModel model, IPropertyMeta propertyMeta, RenderingContext renderingContext)
        {
            var stringProperty = propertyMeta as StringPropertyMeta<TModel>;
            if (stringProperty == null)
            {
                return;
            }

            stringProperty.AssignValueToModelProperty(model, renderingContext.Rendering.GetParameter(propertyMeta.MappingName));
        }
    }
}
