using System;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.ItemPropertyMapping
{
    class GuidItemPropertyMapping : IItemPropertyMapping
    {
        public string MappingTypeKey => typeof(Guid).ToString();

        public void SetItemPropertyMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var guidProperty = propertyMeta as GuidPropertyMeta<TModel>;
            if (guidProperty == null)
            {
                return;
            }

            ItemPropertyMappingType mappingType;
            if (!Enum.TryParse(guidProperty.MappingName, true, out mappingType)) return;

            switch (mappingType)
            {
                case ItemPropertyMappingType.ItemId:
                    guidProperty.AssignValueToModelProperty(model, item.ID.Guid);
                    break;
                case ItemPropertyMappingType.TemplateId:
                    guidProperty.AssignValueToModelProperty(model, item.TemplateID.Guid);
                    break;
            }
        }
    }
}
