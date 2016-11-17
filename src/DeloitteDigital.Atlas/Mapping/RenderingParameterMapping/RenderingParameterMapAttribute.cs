using System;
using System.Collections.Generic;
using System.Reflection;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;

namespace DeloitteDigital.Atlas.Mapping.RenderingParameterMapping
{
    public class RenderingParameterMapAttribute : Attribute, IMappingAttribute
    {
        public RenderingParameterMapAttribute()
        {
        }

        public RenderingParameterMapAttribute(string renderingParameterName)
        {
            this.RenderingParameterName = renderingParameterName;
        }

        public string RenderingParameterName { get; set; }

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
            propertyMeta.MappingName = RenderingParameterName ?? property.Name;
            propertyMeta.Mapper = new RenderingParameterMapper();
            return propertyMeta;
        }
    }
}
