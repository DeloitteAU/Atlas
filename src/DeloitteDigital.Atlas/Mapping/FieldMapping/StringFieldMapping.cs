using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class StringFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof(string).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var stringProperty = propertyMeta as StringPropertyMeta<TModel>;
            if (stringProperty == null)
            {
                return;
            }

            stringProperty.AssignValueToModelProperty(model, Translate.Text(item[stringProperty.MappingName]));
        }
    }
}
