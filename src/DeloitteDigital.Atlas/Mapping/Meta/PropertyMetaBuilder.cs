using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;

namespace DeloitteDigital.Atlas.Mapping.Meta
{
    public class PropertyMetaBuilder
    {
        public IEnumerable<IPropertyMeta> BuildPropertyMetaMap<TModel>()
        {
            var modelType = typeof(TModel);
            var propertyMap = new List<IPropertyMeta>();
            var propertyMetaCollection = BuildMappingDictionary(modelType);

            foreach (var property in modelType.GetProperties())
            {
                var attributes = property.GetCustomAttributes(true);
                propertyMap.AddRange(
                    attributes.Where(x => x is IMappingAttribute)
                        .Cast<IMappingAttribute>()
                        .Select(attribute => attribute.CreatePropertyMeta<TModel>(property, propertyMetaCollection))
                        .Where(propertyMeta => propertyMeta != default(IPropertyMeta)));
            }

            return propertyMap;
        }

        private static IDictionary<string, Type> BuildMappingDictionary(Type modelType)
        {
            var mappingDictionary = new Dictionary<string, Type>();
            var mappingClasses = GetAllPropertyMetaClasses();

            foreach (var mappingClass in mappingClasses)
            {
                var genericType = mappingClass.MakeGenericType(modelType);
                var instance = Activator.CreateInstance(genericType) as IPropertyMeta;
                if (instance != null && !mappingDictionary.ContainsKey(instance.PropertyKey))
                {
                    mappingDictionary.Add(instance.PropertyKey, instance.GetType());
                }
            }

            return mappingDictionary;
        }

        private static IEnumerable<Type> GetAllPropertyMetaClasses()
        {
            var mappingInterface = typeof(IPropertyMeta);
            var mappingClasses =
                Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name)
                    .GetTypes()
                    .Where(t => mappingInterface.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            return mappingClasses;
        }
    }
}
