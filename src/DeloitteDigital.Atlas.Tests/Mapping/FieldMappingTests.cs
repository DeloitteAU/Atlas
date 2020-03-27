using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping;
using DeloitteDigital.Atlas.Tests.FakeDB;
using Shouldly;
using Xunit;

namespace DeloitteDigital.Atlas.Tests.Mapping
{
    public class FieldMappingTests
    {
        [Fact]
        public void FieldMappingShouldMapTitle()
        {
            using (var db = new DspFakeDb())
            {
                var mapper = new ItemMapper(new WebCache());
                var sampleItem = mapper.Map<SampleItem>(db.GetHomeItem());

                sampleItem.ShouldNotBeNull();
                sampleItem.Title.ShouldBe("Sitecore Experience Platform");
            }

        }
    }
}
