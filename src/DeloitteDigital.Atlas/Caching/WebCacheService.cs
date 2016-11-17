using System;
using System.Collections;
using DeloitteDigital.Atlas.Logging;

namespace DeloitteDigital.Atlas.Caching
{
    public class WebCacheService : ICacheService
    {
        private readonly ILogService logService;
        private readonly ICache cache;
        private const string CachePrefix = "AtlasCache";

        public WebCacheService(ILogService logService, ICache cache)
        {
            this.logService = logService;
            this.cache = cache;
        }

        /// <summary>
        /// Cache the item for 3 hours or until a publish event invalidates the cache
        /// </summary>
        public T CreateOrGet<T>(string key, Func<T> resolver)
        {
            return CreateOrGet(key, TimeSpan.FromHours(3), resolver, true);
        }

        public T CreateOrGet<T>(string key, TimeSpan duration, Func<T> resolver, bool invalidateOnPublish = false)
        {
            if (!CacheHelper.EnsureWebDatabase())
            {
                // we only support caching against the master or web database
                // core and unknown which may happen from a sitecore event handler or indexing
                // are just too difficult

                return resolver();
            }

            // prefix items this class manages to reduce side effects
            var prefixedKey = CreateKey(key);
                      
            var result = cache.Get(prefixedKey);

            if (result != null)
            {
                var item = result as CacheItem;
                if (item != null)
                {
                    logService.Debug(string.Format("DeloitteDigital.Atlas.Caching.WebCacheService.CreateOrGet - found item with key '{0}' in cache.", prefixedKey), this);
                    return (T)item.Data;
                }
            }

            result = resolver();

            if (result == null)
            {
                logService.Debug(string.Format("DeloitteDigital.Atlas.Caching.WebCacheService.CreateOrGet - resolution of item with key '{0}' was null. Nothing was cached.", prefixedKey), this);
                return default(T);
            }

            AddToCache(prefixedKey, key, result, duration, invalidateOnPublish);

            // cache miss
            logService.Debug(string.Format("DeloitteDigital.Atlas.Caching.WebCacheService.CreateOrGet - added item with key '{0}' in cache.", prefixedKey), this);
            return (T)result;
        }

        public void Remove(string key)
        {
            var prefixedKey = CreateKey(key);
            cache.Remove(prefixedKey);
        }

        public void ClearCache()
        {
            var prefix = CachePrefix;
            cache.RemoveAll(prefix);
        }

        public void ClearCache(string siteName, string databaseName)
        {
            var prefix = string.Format("{0}.{1}.{2}", CachePrefix, siteName ?? "", databaseName ?? "");
            cache.RemoveAll(prefix);
        }

        public void ClearItemsWithPublishDependency(string siteName, string databaseName)
        {         
            var prefix = string.Format("{0}.{1}.{2}", CachePrefix, siteName ?? "", databaseName ?? "");

            foreach (DictionaryEntry entry in cache.GetAll(prefix))
            {                
                var cacheItem = entry.Value as CacheItem;

                if (cacheItem == null) continue;

                if (cacheItem.InvalidateOnPublish)
                {
                    cache.Remove(entry.Key.ToString());
                }
            }
        }

        private static string CreateKey(string key)
        {
            var siteName = global::Sitecore.Context.Site == null ? "" : global::Sitecore.Context.Site.Name;
            var databaseName = global::Sitecore.Context.Database == null ? "" : global::Sitecore.Context.Database.Name;

            return string.Format("{0}.{1}.{2}.{3}", CachePrefix, siteName, databaseName, key);
        }

        /// <summary>
        /// Add the item to cache.  
        /// </summary>
        private void AddToCache<T>(string key, string orignalKey, T result, TimeSpan duration, bool invalidateOnPublish)
        {
            var cacheItem = new CacheItem(key, orignalKey, result, invalidateOnPublish);
            cache.Set(key, cacheItem, DateTime.Now.Add(duration));
        }
    }
}
