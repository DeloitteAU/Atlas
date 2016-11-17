using DeloitteDigital.Atlas.Mapping.ChildrenMapping;

namespace DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta
{
    public interface IChildMappingPropertyMeta : IPropertyMeta
    {
        string Selector { get; set; }
        ChildrenMapType ChildMapType { get; set; }
    }
   
}