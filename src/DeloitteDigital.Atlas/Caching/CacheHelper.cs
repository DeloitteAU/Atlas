using System;
using Microsoft.Practices.ServiceLocation;

namespace DeloitteDigital.Atlas.Caching
{
    public static class CacheHelper
    {
        /// <summary>
        /// Ensure we are only on web sitecore database
        /// </summary>
        /// <remarks>
        /// We only support caching against the web database
        /// core, master and others which may happen from a sitecore event handler or indexing
        /// are just too difficult to track
        /// </remarks>
        /// <returns></returns>
        public static bool EnsureWebDatabase()
        {
            if (Sitecore.Context.Database == null) return false;
            if (Sitecore.Context.Database.Name.Equals("web", StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }

        public static void ClearCache(string siteName, string databaseName)
        {
            //var cache = ServiceLocator.Current.GetInstance<ICacheService>();
            var cache = (ICacheService) Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(ICacheService));
            cache.ClearCache(siteName, databaseName);
        }

        public static void ClearItemsWithPublishDependancy(string siteName, string databaseName)
        {
            //var cache = ServiceLocator.Current.GetInstance<ICacheService>();
            var cache = (ICacheService)Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(ICacheService));
            cache.ClearItemsWithPublishDependency(siteName, databaseName);
        }
    }
}
