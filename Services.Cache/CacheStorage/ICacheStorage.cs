using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Cache.CacheStorage
{
    public interface ICacheStorage
    {
        void Remove(string key);
        void Store(string key, object data, TimeSpan timespan);
        T Retrieve<T>(string key);
    }
}
