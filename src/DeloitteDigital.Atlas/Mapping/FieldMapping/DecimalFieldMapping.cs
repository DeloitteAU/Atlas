using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class DecimalFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof (decimal).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var decimalProperty = propertyMeta as DecimalPropertyMeta<TModel>;
            if (decimalProperty == null)
            {
                return;
            }

            decimalProperty.AssignValueToModelProperty(model, decimal.Parse(item[decimalProperty.MappingName]));
        }
    }
}
