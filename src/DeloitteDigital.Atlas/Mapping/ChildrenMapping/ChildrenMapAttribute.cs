using System;
using System.Collections.Generic;
using System.Reflection;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;

namespace DeloitteDigital.Atlas.Mapping.ChildrenMapping
{
    public class ChildrenMapAttribute : Attribute, IMappingAttribute
    {
        public string Selector { get; set; }
        public ChildrenMapType MapType { get; set; }

        public ChildrenMapAttribute(ChildrenMapType mapType)
            : this(string.Empty, mapType)
        {
        }

        public ChildrenMapAttribute(string selector, ChildrenMapType mapType)
        {
            Selector = selector;
            MapType = mapType;
        }

        public IPropertyMeta CreatePropertyMeta<TModel>(PropertyInfo property, IDictionary<string, Type> propertyMetaDictionary)
        {
            var childMapMeta = new ChildMappingPropertyMeta<TModel>
            {
                PropertyName = property.Name,
                MappingName = property.Name,
                Selector = Selector,
                ChildMapType = MapType,
                Mapper = new ChildrenMapper()
            };

            return childMapMeta;
        }
    }
}
