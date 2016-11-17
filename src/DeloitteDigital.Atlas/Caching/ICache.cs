using System;
using System.Collections.Generic;

namespace DeloitteDigital.Atlas.Caching
{
    public interface ICache
    {
        object Get(string key);

        IEnumerable<object> GetAll(string keyPrefix);

        void Set(string key, object value);

        void Set(string key, object value, DateTime expirationTime);

        object Remove(string key);

        void RemoveAll(string keyPrefix);
    }
}
