using System;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.ItemPropertyMapping
{
    class IdItemPropertyMapping : IItemPropertyMapping
    {
        public string MappingTypeKey => typeof(ID).ToString();

        public void SetItemPropertyMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var idProperty = propertyMeta as IdPropertyMeta<TModel>;
            if (idProperty == null)
            {
                return;
            }

            ItemPropertyMappingType mappingType;
            if (!Enum.TryParse(idProperty.MappingName, true, out mappingType)) return;

            switch (mappingType)
            {
                case ItemPropertyMappingType.ItemId:
                    idProperty.AssignValueToModelProperty(model, item.ID);
                    break;
                case ItemPropertyMappingType.TemplateId:
                    idProperty.AssignValueToModelProperty(model, item.TemplateID);
                    break;
            }
        }
    }
}
