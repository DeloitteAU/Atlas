using DeloitteDigital.Atlas.Extensions;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mapping.RenderingParameterMapping
{
    public class BoolRenderingParameterMapping : IRenderingParameterMapping
    {
        public string MappingTypeKey => typeof (bool).ToString();

        public void SetRenderingParameterMappingMapping<TModel>(TModel model, IPropertyMeta propertyMeta, RenderingContext renderingContext)
        {
            var boolProperty = propertyMeta as BoolPropertyMeta<TModel>;
            if (boolProperty == null)
            {
                return;
            }

            boolProperty.AssignValueToModelProperty(model, renderingContext.Rendering.GetParameter(propertyMeta.MappingName, false));
        }
    }
}
