using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class DoubleFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof (double).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var doubleProperty = propertyMeta as DoublePropertyMeta<TModel>;
            if (doubleProperty == null)
            {
                return;
            }

            doubleProperty.AssignValueToModelProperty(model, double.Parse(item[doubleProperty.MappingName]));
        }
    }
}
