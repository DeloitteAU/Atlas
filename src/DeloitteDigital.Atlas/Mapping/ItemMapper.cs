using System;
using System.Collections.Generic;
using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping
{
    public class ItemMapper : IItemMapper
    {
        private readonly ICache cache;
        private static readonly object DictionarySyncRoot = new object();
        private static readonly object MetaSyncRoot = new object();
        private const string MetaDataDictionaryCacheKey = "DeloitteDigital.Atlas.Mapping.ItemMapper.MetadataDictionary";

        public ItemMapper() : this(new WebCache())
        {

        }

        public ItemMapper(ICache cache)
        {
            this.cache = cache;
        }

        public TModel Map<TModel>(TModel model, Item item)
        {
            // Step 1: check if meta data dictionary exist, and build meta data dictionary if not exist
            var metadataDictionary = EnsureMetaDictionaryExists();

            // Step 2: check if meta data for TModel type exist, and build meta data for TModel type if not exist
            var modelMeta = EnsureModelDictionaryEntryExists<TModel>(metadataDictionary);

            // Step 3: use cached compiled expression to retrieve property from meta cache
            MapModelProperties(model, item, modelMeta);

            return model;
        }
        
        public TModel Map<TModel>(Item from)
        {
            var model = Activator.CreateInstance<TModel>();
            return new ItemMapper().Map<TModel>(model, from);
        }

        private void MapModelProperties<TModel>(TModel model, Item item, ModelMeta modelMeta)
        {
            foreach (var propertyMetaInterface in modelMeta.PropertyMap)
            {
                propertyMetaInterface.Mapper.HandleMapping(model, item, propertyMetaInterface, cache, this);
            }
        }

        private ModelMeta EnsureModelDictionaryEntryExists<TModel>(IDictionary<string, ModelMeta> metadataDictionary)
        {
            var modelType = typeof(TModel);
            var modelMetaKey = modelType.AssemblyQualifiedName;
            var modelMeta = default(ModelMeta);

            if (metadataDictionary.ContainsKey(modelMetaKey))
            {
                modelMeta = metadataDictionary[modelMetaKey];
            }
            else
            {
                lock (MetaSyncRoot)
                {
                    if (!metadataDictionary.ContainsKey(modelMetaKey))
                    {
                        modelMeta = new ModelMeta();
                        var propertyMetaBuilder = new PropertyMetaBuilder();
                        modelMeta.PropertyMap = propertyMetaBuilder.BuildPropertyMetaMap<TModel>();
                        metadataDictionary.Add(modelMetaKey, modelMeta);
                    }
                }
            }
            return modelMeta;
        }

        private IDictionary<string, ModelMeta> EnsureMetaDictionaryExists()
        {
            var metadataDictionary = cache.Get(MetaDataDictionaryCacheKey) as IDictionary<string, ModelMeta>;
            if (metadataDictionary != null)
            {
                return metadataDictionary;
            }

            lock (DictionarySyncRoot)
            {
                if (metadataDictionary == null)
                {
                    metadataDictionary = new Dictionary<string, ModelMeta>();
                    cache.Set(MetaDataDictionaryCacheKey, metadataDictionary);
                }
            }
            return metadataDictionary;
        }
    }
}
