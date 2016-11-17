using System;
using DeloitteDigital.Atlas.Extensions;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.ItemPropertyMapping
{
    class StringItemPropertyMapping : IItemPropertyMapping
    {
        public string MappingTypeKey => typeof(string).ToString();

        public void SetItemPropertyMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var stringProperty = propertyMeta as StringPropertyMeta<TModel>;
            if (stringProperty == null)
            {
                return;
            }

            ItemPropertyMappingType mappingType;
            if (!Enum.TryParse(stringProperty.MappingName, true, out mappingType)) return;
            
            switch (mappingType)
            {
                case ItemPropertyMappingType.ItemId:
                    stringProperty.AssignValueToModelProperty(model, item.ID.ToString());
                    break;
                case ItemPropertyMappingType.ItemUrl:
                    stringProperty.AssignValueToModelProperty(model, item.GetItemUrl());
                    break;
                case ItemPropertyMappingType.TemplateId:
                    stringProperty.AssignValueToModelProperty(model, item.TemplateID.ToString());
                    break;
                case ItemPropertyMappingType.TemplateName:
                    stringProperty.AssignValueToModelProperty(model, item.TemplateName);
                    break;
                case ItemPropertyMappingType.ItemName:
                    stringProperty.AssignValueToModelProperty(model, item.Name);
                    break;
                case ItemPropertyMappingType.ItemPath:
                    stringProperty.AssignValueToModelProperty(model, item.Paths.Path);
                    break;
            }
        }
    }
}
