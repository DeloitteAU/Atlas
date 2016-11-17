using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace DeloitteDigital.Atlas.Caching
{
    public class WebCache : ICache
    {
        private Cache Cache
        {
            get { return HttpContext.Current != null ? HttpContext.Current.Cache : HttpRuntime.Cache; }
        }

        public object Get(string key)
        {
            return this.Cache.Get(key);
        }

        public IEnumerable<object> GetAll(string keyPrefix)
        {
            var retVal = new List<object>();

            foreach (DictionaryEntry entry in Cache)
            {
                var key = entry.Key.ToString();
                if (!key.StartsWith(keyPrefix, StringComparison.OrdinalIgnoreCase)) continue;
                retVal.Add(entry);
            }

            return retVal;
        }

        public void Set(string key, object value)
        {
            this.Cache.Insert(key, value);

        }

        public void Set(string key, object value, DateTime expirationTime)
        {
            this.Cache.Insert(key, value, null, expirationTime, Cache.NoSlidingExpiration);
        }

        public object Remove(string key)
        {
            return this.Cache.Remove(key);
        }

        public void RemoveAll(string keyPrefix)
        {
            foreach (DictionaryEntry entry in Cache)
            {
                var key = entry.Key.ToString();
                if (!key.StartsWith(keyPrefix, StringComparison.OrdinalIgnoreCase)) continue;
                Cache.Remove(key);
            }
        }
    }
}
