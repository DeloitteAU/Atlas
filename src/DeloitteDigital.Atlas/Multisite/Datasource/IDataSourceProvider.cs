using System.Collections.Generic;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Multisite.Datasource
{
    public interface IDatasourceProvider
    {
        IEnumerable<Item> GetDatasources(string name, Item contextItem);

        bool CanAct(string datasourceLocationValue);
    }
}
