using Services.Cache.CacheStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Cache.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ICacheStorage cacheStorage = new HttpContextCache();
            cacheStorage.Store("abc", "abc", new TimeSpan(0, 1, 0));
            
            Console.ReadKey();
        }
    }
}
