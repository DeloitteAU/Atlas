using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Sitecore.FakeDb.Serialization;

namespace DeloitteDigital.Atlas.Tests.FakeDB
{
    class DspFakeDb : Db
    {
        public DspFakeDb()
        {
            Add(new DsDbTemplate("/sitecore/templates/Sample/Sample Item"));
            Add(new DsDbItem("/sitecore/content/Home"));
        }

        public Item GetHomeItem()
        {
            return GetItem("/sitecore/content/Home", "en");
        }

    }
}
