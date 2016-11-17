using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public interface IFieldMapping : ITypeMapper
    {
        void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item);
    }
}