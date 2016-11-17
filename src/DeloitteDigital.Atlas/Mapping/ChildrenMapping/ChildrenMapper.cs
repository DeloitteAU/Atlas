using System;
using System.Collections;
using System.Collections.Generic;
using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.ChildrenMapping
{
    public class ChildrenMapper : IMapper
    {
        public void HandleMapping<TModel>(TModel model, Item item, IPropertyMeta propertyMeta, ICache cache, IItemMapper itemMapper)
        {
            var childMapPropertyMeta = propertyMeta as IChildMappingPropertyMeta;
            if (childMapPropertyMeta == null || model.GetType().GetProperty(propertyMeta.PropertyName).Equals(null))
            {
                return;
            }

            var property = model.GetType().GetProperty(propertyMeta.PropertyName);
            var listType = property.PropertyType.GetGenericArguments()[0];

            var genericList = typeof(List<>).MakeGenericType(listType);
            var collection = Activator.CreateInstance(genericList) as IList;
            if (collection == null)
            {
                return;
            }

            var parentItem = GetParentItemForChildCollection(item, childMapPropertyMeta);
            if (parentItem != null)
            {
                foreach (Item childItem in parentItem.Children)
                {
                    var childObject = Activator.CreateInstance(listType) as dynamic;
                    itemMapper.Map(childObject, childItem);
                    collection.Add(childObject);
                }
            }
            property.SetValue(model, collection);
        }

        private static Item GetParentItemForChildCollection(Item item, IChildMappingPropertyMeta childMapPropertyMeta)
        {
            Item parentItem = null;
            switch (childMapPropertyMeta.ChildMapType)
            {
                case ChildrenMapType.Field:
                    parentItem = global::Sitecore.Context.Database.GetItem(item[childMapPropertyMeta.Selector]);
                    break;
                case ChildrenMapType.Id:
                case ChildrenMapType.Path:
                    parentItem = global::Sitecore.Context.Database.GetItem(childMapPropertyMeta.Selector);
                    break;
                case ChildrenMapType.Query:
                    parentItem = item.Axes.SelectSingleItem(childMapPropertyMeta.Selector);
                    break;
                case ChildrenMapType.Direct:
                    parentItem = item;
                    break;
            }
            return parentItem;
        }

        
    }
}
