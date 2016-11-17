using System;
using System.Collections;
using System.Collections.Generic;
using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class ListFieldMapper : IMapper
    {
        public void HandleMapping<TModel>(TModel model, Item item, IPropertyMeta propertyMeta, ICache cache, IItemMapper itemMapper)
        {
            var listField = (MultilistField)item.Fields[propertyMeta.MappingName];

            if (listField == null) return;

            var listItems = listField.GetItems();

            var property = model.GetType().GetProperty(propertyMeta.PropertyName);

            var listType = property.PropertyType.GetGenericArguments()[0];

            var genericList = typeof(List<>).MakeGenericType(listType);
            var collection = Activator.CreateInstance(genericList) as IList;
            if (collection == null)
            {
                return;
            }

            foreach (var linkedItem in listItems)
            {
                var childObject = Activator.CreateInstance(listType) as dynamic;
                itemMapper.Map(childObject, linkedItem);
                collection.Add(childObject);
            }
            property.SetValue(model, collection);
        }
    }
}
