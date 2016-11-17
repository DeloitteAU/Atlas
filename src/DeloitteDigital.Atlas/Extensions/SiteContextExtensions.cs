using System;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Sites;
using Constants = DD.Atlas.Constants;

namespace DeloitteDigital.Atlas.Extensions
{
    public static class SiteContextExtensions
    {
        public static Item GetStartItem(this SiteContext siteContext)
        {        
            var database = Context.Database;
            return database.GetItem(siteContext.StartPath);
        }

        /// <summary>
        /// Note: this works for the master and web indexes only
        /// </summary>
        public static string GetIndexName(this SiteContext site)
        {
            return site.Database.Name.Equals(Constants.Database.MasterDatabaseName, StringComparison.OrdinalIgnoreCase)
                ? Constants.Index.MasterIndexName
                : Constants.Index.WebIndexName;
        }
    }
}
