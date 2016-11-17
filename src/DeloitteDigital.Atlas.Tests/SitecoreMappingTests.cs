using System.Linq;
using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Mapping.FieldMapping;
using DeloitteDigital.Atlas.Mapping.Meta;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloitteDigital.Atlas.Tests
{
    [TestClass]
    public class SitecoreMappingTests
    {
        [TestMethod]
        public void PropertyMetaBuilderShouldCorrectNumberOfMappedProperties()
        {
            var propertyMetaBuilder = new PropertyMetaBuilder();
            var myModelPropertyMetaMap = propertyMetaBuilder.BuildPropertyMetaMap<MyModel>();
            Assert.AreEqual(1, myModelPropertyMetaMap.Count());
        }

        public class MyModel
        {
            [FieldMap("My Sc Field")]
            public IFieldRenderingString MyProperty { get; set; }
        }
    }
}
