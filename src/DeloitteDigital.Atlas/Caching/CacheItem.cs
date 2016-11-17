using System;

namespace DeloitteDigital.Atlas.Caching
{
    public class CacheItem
    {
        public CacheItem(string key, string originalKey, object data, bool invalidateOnPublish)
        {
            Key = key;
            OriginalKey = originalKey;
            Data = data;
            InvalidateOnPublish = invalidateOnPublish;
            CreatedDateTime = DateTime.Now;
        }

        public bool InvalidateOnPublish { get; private set; }
        public string Key { get; private set; }
        public string OriginalKey { get; private set; }
        public object Data { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
    }
}
