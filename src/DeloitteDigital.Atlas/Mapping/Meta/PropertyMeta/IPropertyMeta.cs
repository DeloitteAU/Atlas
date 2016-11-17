namespace DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta
{
    public interface IPropertyMeta
    {
        string PropertyName { get; set; }
        string PropertyKey { get; }
        string MappingName { get; set; }
        IMapper Mapper { get; set; }
    }
}