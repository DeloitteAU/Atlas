using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class IntFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof (int).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var intProperty = propertyMeta as IntPropertyMeta<TModel>;
            if (intProperty == null)
            {
                return;
            }

            intProperty.AssignValueToModelProperty(model, int.Parse(item[intProperty.MappingName]));
        }
    }
}