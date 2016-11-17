using System;

namespace DeloitteDigital.Atlas.Caching
{
    public interface ICacheService
    {
        /// <summary>
        /// Create or get an item from the cache.
        /// </summary>
        /// <remarks>
        /// The item is added with the defaults of invalidate on publish or three hours passes.
        /// Only Master or Web sitecore contexts are supported
        /// </remarks>
        T CreateOrGet<T>(string key, Func<T> resolver);

        /// <summary>
        /// Create or get an item from the cache.
        /// </summary>
        /// <typeparam name="T">the item type</typeparam>
        /// <param name="key">the cache item key</param>
        /// <param name="duration">duration item is to be cached for</param>
        /// <param name="resolver">function to resolve the cache item value</param>
        /// <param name="invalidateOnPublish">true to remove from cache on a sitecore publish event</param>
        /// <returns></returns>
        T CreateOrGet<T>(string key, TimeSpan duration, Func<T> resolver, bool invalidateOnPublish = false);

        void Remove(string key);

        void ClearCache();

        void ClearCache(string siteName, string databaseName);

        void ClearItemsWithPublishDependency(string siteName, string databaseName);
    }
}
