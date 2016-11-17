using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping;
using DeloitteDigital.Atlas.Tests.FakeDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloitteDigital.Atlas.Tests.Mapping
{
    [TestClass]
    public class FieldMappingTests
    {
        [TestMethod]
        public void FieldMappingShouldMapTitle()
        {
            using (var db = new DspFakeDb())
            {
                var mapper = new ItemMapper(new WebCache());
                var sampleItem = mapper.Map<SampleItem>(db.GetHomeItem());

                Assert.IsNotNull(sampleItem);
                Assert.AreEqual("Sitecore Experience Platform", sampleItem.Title);
            }

        }
    }
}
