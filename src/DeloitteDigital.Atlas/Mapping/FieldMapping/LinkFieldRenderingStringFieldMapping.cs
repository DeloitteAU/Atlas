using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class LinkFieldRenderingStringFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof (ILinkFieldRenderingString).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var linkFieldProperty = propertyMeta as LinkFieldRenderingStringPropertyMeta<TModel>;
            if (linkFieldProperty == null)
            {
                return;
            }

            linkFieldProperty.AssignValueToModelProperty(model, new LinkFieldRenderingString(item, linkFieldProperty.MappingName));
        }
    }
}
