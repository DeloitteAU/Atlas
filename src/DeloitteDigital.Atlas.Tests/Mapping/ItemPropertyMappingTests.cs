using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping;
using DeloitteDigital.Atlas.Tests.FakeDB;
using Sitecore.Data;
using Shouldly;
using Xunit;

namespace DeloitteDigital.Atlas.Tests.Mapping
{
    public class ItemPropertyMappingTests
    {
        [Fact]
        public void ItemPropertyMappingShouldMapItemProperties()
        {
            using (var db = new DspFakeDb())
            {
                var mapper = new ItemMapper(new WebCache());
                var sampleItem = mapper.Map<SampleItem>(db.GetHomeItem());

                // assert item property mappings
                sampleItem.ShouldNotBeNull();
                sampleItem.ItemId.ShouldBe(new ID("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"));
                sampleItem.ItemName.ShouldBe("Home");
                sampleItem.ItemPath.ShouldBe("/sitecore/content/Home");
                sampleItem.ItemUrl.ShouldBe("/en/sitecore/content/Home.aspx");
                sampleItem.TemplateId.ShouldBe(new ID("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}"));
                sampleItem.TemplateName.ShouldBe("Sample Item");
            }
        }
    }
}
