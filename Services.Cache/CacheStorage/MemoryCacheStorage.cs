using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Threading;

namespace Services.Cache.CacheStorage
{
    public class MemoryCacheStorage : ICacheStorage
    {
        private static MemoryCache cache = MemoryCache.Default;
        private readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        public void Remove(string key)
        {
            cacheLock.EnterWriteLock();
            try
            {
                cache.Remove(key);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public T Retrieve<T>(string key)
        {
            cacheLock.EnterReadLock();

            try
            {
                T val = (T)cache.Get(key);
                if (val == null)
                    return default(T);
                else
                    return val;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
            return default(T);
        }

        public void Store(string key, object data, TimeSpan timespan)
        {
            cacheLock.EnterWriteLock();
            try
            {
                if (cache.Contains(key))
                {
                    cache.Remove(key);
                }
                cache.Add(key, data, DateTime.Now.Add(timespan));
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
    }
}
