using System;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class BoolFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof(bool).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var boolProperty = propertyMeta as BoolPropertyMeta<TModel>;
            if (boolProperty == null)
            {
                return;
            }

            boolProperty.AssignValueToModelProperty(model, item[boolProperty.MappingName].Equals("1", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
