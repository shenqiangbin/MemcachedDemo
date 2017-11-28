using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Services.Cache.CacheStorage
{
    public class HttpContextCache : ICacheStorage
    {

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public T Retrieve<T>(string key)
        {
            T itemStored = (T)HttpContext.Current.Cache.Get(key);
            if (itemStored == null)
                itemStored = default(T);

            return itemStored;
        }

        public void Store(string key, object data, TimeSpan timespan)
        {
            HttpContext.Current.Cache.Insert(key, data, null, DateTime.UtcNow, timespan);
        }
    }
}
