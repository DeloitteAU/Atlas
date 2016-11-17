using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping;
using DeloitteDigital.Atlas.Tests.FakeDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.Data;

namespace DeloitteDigital.Atlas.Tests.Mapping
{
    [TestClass]
    public class ItemPropertyMappingTests
    {
        [TestMethod]
        public void ItemPropertyMappingShouldMapItemProperties()
        {
            using (var db = new DspFakeDb())
            {
                var mapper = new ItemMapper(new WebCache());
                var sampleItem = mapper.Map<SampleItem>(db.GetHomeItem());

                // assert item property mappings
                Assert.IsNotNull(sampleItem);
                Assert.AreEqual(new ID("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"), sampleItem.ItemId);
                Assert.AreEqual("Home", sampleItem.ItemName);
                Assert.AreEqual("/sitecore/content/Home", sampleItem.ItemPath);
                Assert.AreEqual("/en/sitecore/content/Home.aspx", sampleItem.ItemUrl);
                Assert.AreEqual(new ID("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}"), sampleItem.TemplateId);
                Assert.AreEqual("Sample Item", sampleItem.TemplateName);
            }
        }
    }
}
