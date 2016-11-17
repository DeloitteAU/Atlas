using System.Linq;
using Sitecore.Data;

namespace DeloitteDigital.Atlas
{
    public class DataSourceParser
    {
        public DataSource Parse(string rawDataSource)
        {
            var dataSourceItemId = default(ID);
            var database = Sitecore.Context.Database;

            if (ID.TryParse(rawDataSource, out dataSourceItemId))
            // case 1: data source value is an item ID
            {
                var dataSourceItem = database.GetItem(dataSourceItemId);

                if (dataSourceItem != null)
                {
                    return new DataSource
                    {
                        Item = dataSourceItem,
                        Items = new[] { dataSourceItem }
                    };
                }
            }
            else if (rawDataSource.ToLower().StartsWith("query:"))
            // case 2: data source value is a sitecore query that starts with "query:"
            {
                var results = database.SelectItems(rawDataSource.Substring("query:".Length));

                if (results.Length > 0)
                {
                    return new DataSource
                    {
                        Item = results.First(),
                        Items = results
                    };
                }
            }

            return DataSource.Empty;
        }
    }
}
