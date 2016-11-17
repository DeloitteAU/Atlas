using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.DictionaryMapping;
using DeloitteDigital.Atlas.Mapping.FieldMapping;
using DeloitteDigital.Atlas.Mapping.ItemPropertyMapping;
using DeloitteDigital.Atlas.Mapping.RenderingParameterMapping;

namespace DeloitteDigital.Atlas.Mapping
{
    public static class MappingUtil
    {
        private const string DictionaryMappingCacheKey = "DeloitteDigital.Atlas.Mapping.ItemMapper.DictionaryMapping";
        private const string RenderingParameterMappingSyncRootCacheKey = "DeloitteDigital.Atlas.Mapping.ItemMapper.RenderingParameterMapping";
        private const string ItemPropertyMappingSyncRootCacheKey = "DeloitteDigital.Atlas.Mapping.ItemMapper.ItemPropertyMapping";
        private const string FieldMappingDictionaryCacheKey = "DeloitteDigital.Atlas.Mapping.ItemMapper.ItemFieldMappingDictionary";
        private static readonly object DictionaryMappingSyncRoot = new object();
        private static readonly object RenderingParameterMappingSyncRoot = new object();
        private static readonly object ItemPropertyMappingSyncRoot = new object();
        private static readonly object FieldMappingsSyncRoot = new object();

        public static IDictionary<string, IFieldMapping> GetFieldMappers(ICache _cache)
        {
            return GetTypeMappingDictionary<IFieldMapping>(_cache, FieldMappingDictionaryCacheKey, FieldMappingsSyncRoot);
        }

        public static IDictionary<string, IDictionaryMapping> GetDictionaryMappers(ICache _cache)
        {
            return GetTypeMappingDictionary<IDictionaryMapping>(_cache, DictionaryMappingCacheKey, DictionaryMappingSyncRoot);
        }

        public static IDictionary<string, IRenderingParameterMapping> GetRenderingParameterMappers(ICache _cache)
        {
            return GetTypeMappingDictionary<IRenderingParameterMapping>(_cache, RenderingParameterMappingSyncRootCacheKey, RenderingParameterMappingSyncRoot);
        }

        public static IDictionary<string, IItemPropertyMapping> GetItemPropertyMappers(ICache _cache)
        {
            return GetTypeMappingDictionary<IItemPropertyMapping>(_cache, ItemPropertyMappingSyncRootCacheKey, ItemPropertyMappingSyncRoot);
        }

        private static IDictionary<string, TTypeMapper> GetTypeMappingDictionary<TTypeMapper>(ICache _cache, string cacheKey, object syncRoot)
             where TTypeMapper : class, ITypeMapper
        {
            var mappingDictionary = _cache.Get(cacheKey) as IDictionary<string, TTypeMapper>;
            if (mappingDictionary != null)
            {
                return mappingDictionary;
            }

            lock (syncRoot)
            {
                if (mappingDictionary == null)
                {
                    mappingDictionary = BuildTypeMappingDictionary<TTypeMapper>();
                    _cache.Set(cacheKey, mappingDictionary);
                }
            }
            return mappingDictionary;
        }

        private static IDictionary<string, TTypeMapper> BuildTypeMappingDictionary<TTypeMapper>()
            where TTypeMapper : class, ITypeMapper
        {
            var mappingDictionary = new Dictionary<string, TTypeMapper>();
            var mappingClasses = GetClassesImplementingInterface<TTypeMapper>();

            foreach (var mappingClass in mappingClasses)
            {
                var instance = Activator.CreateInstance(mappingClass) as TTypeMapper;
                if (instance != null && !mappingDictionary.ContainsKey(instance.MappingTypeKey))
                {
                    mappingDictionary.Add(instance.MappingTypeKey, instance);
                }
            }

            return mappingDictionary;
        }

        private static IEnumerable<Type> GetClassesImplementingInterface<TTypeMapper>() where TTypeMapper : class, ITypeMapper
        {
            var mappingInterface = typeof(TTypeMapper);
            var mappingClasses = Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name)
                                         .GetTypes()
                                         .Where(t => mappingInterface.IsAssignableFrom(t) && t.IsClass);
            return mappingClasses;
        }
    }
}
