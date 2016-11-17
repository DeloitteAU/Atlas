using System;
using System.Collections.Generic;
using System.Reflection;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class FieldMapAttribute : Attribute, IMappingAttribute
    {
        public string SitecoreFieldName { get; set; }

        public FieldType FieldType { get; set; }

        public FieldMapAttribute() : this(FieldType.Single) { }

        public FieldMapAttribute(FieldType fieldType) : this(string.Empty, fieldType) { }

        public FieldMapAttribute(string sitecoreFieldName, FieldType fieldType = FieldType.Single)
        {
            SitecoreFieldName = sitecoreFieldName;
            FieldType = fieldType;
        }

        public IPropertyMeta CreatePropertyMeta<TModel>(PropertyInfo property, IDictionary<string, Type> propertyMetaDictionary)
        {
            var propertyMeta = InstantiatePropertyMeta<TModel>(property, propertyMetaDictionary);
            if (propertyMeta == null)
            {
                return default(IPropertyMeta);
            }

            propertyMeta.PropertyName = property.Name;

            var mappingName = !string.IsNullOrEmpty(SitecoreFieldName) ? SitecoreFieldName : property.Name;
            propertyMeta.MappingName = mappingName;
            if (FieldType == FieldType.Multi)
            {
                propertyMeta.Mapper = new ListFieldMapper();

            }
            else
            {
                propertyMeta.Mapper = new FieldMapper();
            }
            return propertyMeta;
        }

        private static IPropertyMeta InstantiatePropertyMeta<TModel>(PropertyInfo property, IDictionary<string, Type> propertyMetaDictionary)
        {
            return propertyMetaDictionary.ContainsKey(property.PropertyType.ToString())
                ? Activator.CreateInstance(propertyMetaDictionary[property.PropertyType.ToString()]) as IPropertyMeta
                : new DynamicPropertyMeta<TModel>();
        }
    }
}
