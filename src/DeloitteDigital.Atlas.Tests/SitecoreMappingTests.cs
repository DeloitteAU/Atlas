using System.Linq;
using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Mapping.FieldMapping;
using DeloitteDigital.Atlas.Mapping.Meta;
using Shouldly;
using Xunit;

namespace DeloitteDigital.Atlas.Tests
{
    public class SitecoreMappingTests
    {
        [Fact]
        public void PropertyMetaBuilderShouldCorrectNumberOfMappedProperties()
        {
            var propertyMetaBuilder = new PropertyMetaBuilder();
            var myModelPropertyMetaMap = propertyMetaBuilder.BuildPropertyMetaMap<MyModel>();
            myModelPropertyMetaMap.Count().ShouldBe(1);
        }

        public class MyModel
        {
            [FieldMap("My Sc Field")]
            public IFieldRenderingString MyProperty { get; set; }
        }
    }
}
