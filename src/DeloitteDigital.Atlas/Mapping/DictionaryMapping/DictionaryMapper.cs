using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.DictionaryMapping
{
    public class DictionaryMapper : IMapper
    {
        public void HandleMapping<TModel>(TModel model, Item item, IPropertyMeta propertyMeta, ICache cache, IItemMapper itemMapper)
        {
            var dictionaryMappings = MappingUtil.GetDictionaryMappers(cache);
            if (dictionaryMappings.ContainsKey(propertyMeta.PropertyKey))
            {
                dictionaryMappings[propertyMeta.PropertyKey].SetDictionaryFieldMapping(model, propertyMeta, item);
            }
        }
    }
}
