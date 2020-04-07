using Shouldly;
using Xunit;

namespace DeloitteDigital.Atlas.Tests.FakeDB
{
    public class DeserialisationTests
    {
        [Fact]
        public void DeserialisationShouldReturnSitecoreItem()
        {
            using (var db = new DspFakeDb())
            {
                var home = db.GetHomeItem();

                // ensure the de-serialisation worked
                home.ShouldNotBeNull();
                home.Name.ShouldBe("Home");
                home.Fields["Title"].ToString().ShouldBe("Sitecore Experience Platform");
            }
        }

    }
}
