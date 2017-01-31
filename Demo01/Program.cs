using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            Cache.Client.Add("01", "Hello");
            var s = Cache.Client.Get("01");
            Console.WriteLine(s.ToString());

            Console.ReadKey();
        }
    }
}
