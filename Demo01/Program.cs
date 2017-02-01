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

            //Cache.Client.Delete("01");
            //Cache.Client.Set("01", "hello");

            Console.WriteLine(s.ToString());

            List<User> users = new List<User>
            {
                new User { Id = 1,Name = "admin1" },
                new User { Id = 2,Name = "admin2" },
            };

            Cache.Client.Set("users", users);

            //get the users

            var usersObj = Cache.Client.Get("users");


            Console.ReadKey();
        }
    }

    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
