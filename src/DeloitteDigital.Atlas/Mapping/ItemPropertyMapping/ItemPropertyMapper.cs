using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.ItemPropertyMapping
{
    public class ItemPropertyMapper : IMapper
    {
        public void HandleMapping<TModel>(TModel model, Item item, IPropertyMeta propertyMeta, ICache cache, IItemMapper itemMapper)
        {
            var itemPropertyMappings = MappingUtil.GetItemPropertyMappers(cache);
            if (itemPropertyMappings.ContainsKey(propertyMeta.PropertyKey))
            {
                itemPropertyMappings[propertyMeta.PropertyKey].SetItemPropertyMapping(model, propertyMeta, item);
            }
        }
    }
}
