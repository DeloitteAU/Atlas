using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloitteDigital.Atlas.Tests.FakeDB
{
    [TestClass]
    public class DeserialisationTests
    {
        [TestMethod]
        public void DeserialisationShouldReturnSitecoreItem()
        {
            using (var db = new DspFakeDb())
            {
                var home = db.GetHomeItem();

                // ensure the de-serialisation worked 
                Assert.IsNotNull(home);
                Assert.AreEqual("Home", home.Name);
                Assert.AreEqual("Sitecore Experience Platform", home.Fields["Title"].ToString());
            }
        }

    }
}
