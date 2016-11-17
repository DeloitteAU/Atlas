using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace DeloitteDigital.Atlas.Mapping.DictionaryMapping
{
    public class StringDictionaryMapping : IDictionaryMapping
    {
        public string MappingTypeKey => typeof (string).ToString();

        public void SetDictionaryFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var propertyMetaDictionaryString = propertyMeta as StringPropertyMeta<TModel>;
            if (propertyMetaDictionaryString == null)
            {
                return;
            }

            propertyMetaDictionaryString.AssignValueToModelProperty(model, Translate.Text(propertyMetaDictionaryString.MappingName));
        }
    }
}