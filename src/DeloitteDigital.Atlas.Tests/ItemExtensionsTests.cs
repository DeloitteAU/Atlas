using DeloitteDigital.Atlas.Extensions;
using DeloitteDigital.Atlas.Tests.FakeDB;
using Shouldly;
using Xunit;

namespace DeloitteDigital.Atlas.Tests
{
    public class ItemExtensionsTests
    {
        [Fact]
        public void GetFieldsValueShouldReturnCorrectValue()
        {
            using (var db = new DspFakeDb())
            {
                var home = db.GetHomeItem();

                home.GetFieldValue("Title").ShouldBe("Sitecore Experience Platform");
            }            
        }
    }
}
