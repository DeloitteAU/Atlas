using System;
using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class FieldMapper : IMapper
    {
        public void HandleMapping<TModel>(TModel model, Item item, IPropertyMeta propertyMeta, ICache cache, IItemMapper itemMapper)
        {
            var itemFieldMappings = MappingUtil.GetFieldMappers(cache);
            if (item == null || item.Fields[propertyMeta.MappingName] == null)
            {
                return;
            }

            if (itemFieldMappings.ContainsKey(propertyMeta.PropertyKey))
            {
                itemFieldMappings[propertyMeta.PropertyKey].SetModelFieldMapping(model, propertyMeta, item);
            }
            else
            {
                HandleLinkedObjectMapping(model, item, propertyMeta, itemMapper);
            }
        }

        private void HandleLinkedObjectMapping<TModel>(TModel model, Item item, IPropertyMeta propertyMeta, IItemMapper itemMapper)
        {
            Guid guid;
            if (!Guid.TryParse(item[propertyMeta.MappingName], out guid))
            {
                return;
            }

            var linkedItem = global::Sitecore.Context.Database.GetItem(new ID(guid));
            if (linkedItem == null)
            {
                return;
            }

            var property = model.GetType().GetProperty(propertyMeta.PropertyName);
            var linkedObject = Activator.CreateInstance(property.PropertyType) as dynamic;
            itemMapper.Map(linkedObject, linkedItem);
            property.SetValue(model, linkedObject);
        }
    }
}
