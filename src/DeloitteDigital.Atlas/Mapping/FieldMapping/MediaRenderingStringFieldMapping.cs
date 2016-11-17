using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mapping.FieldMapping
{
    public class MediaRenderingStringFieldMapping : IFieldMapping
    {
        public string MappingTypeKey => typeof (IMediaRenderingString).ToString();

        public void SetModelFieldMapping<TModel>(TModel model, IPropertyMeta propertyMeta, Item item)
        {
            var mediaFieldProperty = propertyMeta as MediaRenderingStringPropertyMeta<TModel>;
            if (mediaFieldProperty == null)
            {
                return;
            }

            mediaFieldProperty.AssignValueToModelProperty(model, new MediaRenderingString(item, mediaFieldProperty.MappingName));
        }
    }
}
