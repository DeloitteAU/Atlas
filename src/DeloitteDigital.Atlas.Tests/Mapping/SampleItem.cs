using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Mapping.FieldMapping;
using DeloitteDigital.Atlas.Mapping.ItemPropertyMapping;
using Sitecore.Data;

namespace DeloitteDigital.Atlas.Tests.Mapping
{
    class SampleItem
    {
        [ItemPropertyMap(ItemPropertyMappingType.ItemId)]
        public ID ItemId { get; set; }

        [ItemPropertyMap(ItemPropertyMappingType.ItemName)]
        public string ItemName { get; set; }

        [ItemPropertyMap(ItemPropertyMappingType.ItemPath)]
        public string ItemPath { get; set; }

        [ItemPropertyMap(ItemPropertyMappingType.ItemUrl)]
        public string ItemUrl { get; set; }

        [ItemPropertyMap(ItemPropertyMappingType.TemplateId)]
        public ID TemplateId { get; set; }

        [ItemPropertyMap(ItemPropertyMappingType.TemplateName)]
        public string TemplateName { get; set; }

        [ItemPropertyMap(ItemPropertyMappingType.ParentId)]
        public ID ParentId { get; set; }

        [FieldMap]
        public string Title { get; set; }

        [FieldMap]
        public IFieldRenderingString Text { get; set; }
    }
}
