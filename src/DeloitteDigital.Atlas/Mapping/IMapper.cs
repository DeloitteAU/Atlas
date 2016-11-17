using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping
{
    public interface IMapper
    {
        void HandleMapping<TModel>(TModel model, Item item, IPropertyMeta propertyMeta, ICache cache, IItemMapper itemMapper);
    }
}
