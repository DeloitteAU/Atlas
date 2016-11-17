using System.Collections.Generic;
using DeloitteDigital.Atlas.Mapping.Meta.PropertyMeta;

namespace DeloitteDigital.Atlas.Mapping.Meta
{
    internal class ModelMeta
    {
        public IEnumerable<IPropertyMeta> PropertyMap { get; set; }
    }
}
