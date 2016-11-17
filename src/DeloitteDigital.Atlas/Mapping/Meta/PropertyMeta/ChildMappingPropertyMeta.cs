using DeloitteDigital.Atlas.Mapping.ChildrenMapping;

namespace DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta
{
    public class ChildMappingPropertyMeta<TModel> : IChildMappingPropertyMeta
    {
        public string PropertyName { get; set; }
        public string MappingName { get; set; }
        public IMapper Mapper { get; set; }
        public string Selector { get; set; }
        public ChildrenMapType ChildMapType { get; set; }

        public string PropertyKey => this.GetType().ToString();
    }
}
