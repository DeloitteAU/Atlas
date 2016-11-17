using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.ItemPropertyMapping
{
    public interface IItemPropertyMapping : ITypeMapper
    {
        void SetItemPropertyMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item);
    }
}
