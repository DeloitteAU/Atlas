using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.DictionaryMapping
{
    public interface IDictionaryMapping : ITypeMapper
    {
        void SetDictionaryFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item);
    }
}
