using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas
{
    public class DataSource
    {
        public Item Item { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public bool HasItems
        {
            get { return Item != null || Items.Count() > 0; }
        }

        public static DataSource Empty
        {
            get
            {
                return new DataSource
                {
                    Item = null,
                    Items = Enumerable.Empty<Item>()
                };
            }
        }
    }
}
