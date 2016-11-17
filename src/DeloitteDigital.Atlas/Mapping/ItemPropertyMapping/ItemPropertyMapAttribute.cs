using System;
using System.Collections.Generic;
using System.Reflection;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;

namespace DeloitteDigital.Atlas.Mapping.ItemPropertyMapping
{
    public class ItemPropertyMapAttribute : Attribute, IMappingAttribute
    {
        public ItemPropertyMappingType ItemPropertyMappingType { get; set; }

        public ItemPropertyMapAttribute(ItemPropertyMappingType itemPropertyMappingType)
        {
            ItemPropertyMappingType = itemPropertyMappingType;
        }

        public IPropertyMeta CreatePropertyMeta<TModel>(PropertyInfo property, IDictionary<string, Type> propertyMetaDictionary)
        {
            var propertyMeta = propertyMetaDictionary.ContainsKey(property.PropertyType.ToString())
                ? Activator.CreateInstance(propertyMetaDictionary[property.PropertyType.ToString()]) as IPropertyMeta
                : new DynamicPropertyMeta<TModel>();

            if (propertyMeta == null)
            {
                return default(IPropertyMeta);
            }

            propertyMeta.PropertyName = property.Name;
            propertyMeta.MappingName = this.ItemPropertyMappingType.ToString();
            propertyMeta.Mapper = new ItemPropertyMapper();

            return propertyMeta;            
        }
    }
}
