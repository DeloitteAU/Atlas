using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class FieldRenderingStringFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof (IFieldRenderingString).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var fieldRenderingStringProperty = propertyMeta as FieldRenderingStringPropertyMeta<TModel>;
            if (fieldRenderingStringProperty == null)
            {
                return;
            }

            fieldRenderingStringProperty.AssignValueToModelProperty(model, new FieldRenderingString(item, fieldRenderingStringProperty.MappingName));

        }
    }
}
