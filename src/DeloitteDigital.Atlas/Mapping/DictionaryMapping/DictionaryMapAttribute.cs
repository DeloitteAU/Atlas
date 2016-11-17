using System;
using System.Collections.Generic;
using System.Reflection;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;

namespace DeloitteDigital.Atlas.Mapping.DictionaryMapping
{
    public class DictionaryMapAttribute : Attribute, IMappingAttribute
    {
        public string DictionaryEntryKey { get; set; }

        public DictionaryMapAttribute(string dictionaryEntryKey)
        {
            DictionaryEntryKey = dictionaryEntryKey;
        }

        public IPropertyMeta CreatePropertyMeta<TModel>(PropertyInfo property, IDictionary<string, Type> propertyMetaDictionary)
        {
            if (!propertyMetaDictionary.ContainsKey(property.PropertyType.ToString()))
            {
                return default(IPropertyMeta);
            }

            var propertyMeta = Activator.CreateInstance(propertyMetaDictionary[property.PropertyType.ToString()]) as IPropertyMeta;
            if (propertyMeta == null)
            {
                return default(IPropertyMeta);
            }

            propertyMeta.PropertyName = property.Name;
            propertyMeta.MappingName = DictionaryEntryKey;
            propertyMeta.Mapper = new DictionaryMapper();
            return propertyMeta;
        }
    }
}
