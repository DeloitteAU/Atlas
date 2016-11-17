using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class DateFieldRenderingStringFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof (IDateFieldRenderingString).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var dateFieldRenderingProperty = propertyMeta as DateFieldRenderingStringPropertyMeta<TModel>;
            if (dateFieldRenderingProperty == null)
            {
                return;
            }

            dateFieldRenderingProperty.AssignValueToModelProperty(model, new DateFieldRenderingString(item, dateFieldRenderingProperty.MappingName));
        }
    }
}
