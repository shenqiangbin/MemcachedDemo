using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    public class Cache
    {
        public static MemcachedClient Client;

        static Cache()
        {
            SockIOPool pool = SockIOPool.GetInstance();
            InitPool(pool);

            Client = new MemcachedClient();
            Client.EnableCompression = false;
        }

        static void InitPool(SockIOPool pool)
        {
            string[] services = { "127.0.0.1:11211" };
            pool.SetServers(services);

            pool.Initialize();
        }
    }
}
