using System;

namespace DeloitteDigital.Atlas.Caching
{
    public static class CacheHelper
    {
        /// <summary>
        /// Ensure we are only on web sitecore database
        /// </summary>
        /// <remarks>
        /// We only support caching against the web database. Other databases like core or master may have 
        /// interactiosn triggered from a Sitecore event handler or indexing which are too difficult to track.
        /// </remarks>
        /// <returns>True if running against the web database, otherwise false.</returns>
        public static bool EnsureWebDatabase()
        {
            if (Sitecore.Context.Database == null)
                return false;

            return Sitecore.Context.Database.Name.Equals("web", StringComparison.OrdinalIgnoreCase);
        }

        public static void ClearCache(string siteName, string databaseName)
        {
            var cache = (ICacheService) Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(ICacheService));
            cache.ClearCache(siteName, databaseName);
        }

        public static void ClearItemsWithPublishDependancy(string siteName, string databaseName)
        {
            var cache = (ICacheService)Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(ICacheService));
            cache.ClearItemsWithPublishDependency(siteName, databaseName);
        }
    }
}
