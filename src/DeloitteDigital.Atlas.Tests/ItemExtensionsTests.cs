using DeloitteDigital.Atlas.Extensions;
using DeloitteDigital.Atlas.Tests.FakeDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeloitteDigital.Atlas.Tests
{
    [TestClass]
    public class ItemExtensionsTests
    {
        [TestMethod]
        public void GetFieldsValueShouldReturnCorrectValue()
        {
            using (var db = new DspFakeDb())
            {
                var home = db.GetHomeItem();

                Assert.IsTrue(home.GetFieldValue("Title").Equals("Sitecore Experience Platform"));                
            }            
        }
    }
}
