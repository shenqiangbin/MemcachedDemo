using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enyim.Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            //MemcachedClient mc = new MemcachedClient();

            //ServerStats stats = mc.Stats();
            //mc.Store(StoreMode.Set, "d1", "admin");
            //mc.Store(StoreMode.Set, "d2", true);

            //User user = new User() { Id = 100, Accout = "admin100", Password = "000000", Name = "管理员100" };

            //mc.Store(StoreMode.Set, $"account_{user.Id}", user);

            //User obj = mc.Get<User>($"account_{user.Id}");

            //Console.WriteLine(obj.ToString());

            //string d1 = mc.Get<string>("d1");
            //bool flag = mc.Get<bool>("d2");

            //Console.WriteLine(d1);
            //Console.WriteLine(flag);

            /*********************************阶段二********************************/

            UserService userService = new UserService();
            //todo:清理缓存写成bat文件
            //todo:一个value最好是一个table
            User user = userService.GetById(3);
            Console.WriteLine(user.ToString());

            user = userService.GetById(3);

            Console.ReadKey();
        }
    }

    public static class Cache
    {
        public static MemcachedClient Client = new MemcachedClient();
    }

    public class UserService
    {
        private UserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }

        public User GetById(int id)
        {
            string key = $"account_{id}";
            User user = Cache.Client.Get<User>(key);
            if (user != null)
            {
                return user;
            }
            else
            {
                user = _repository.GetById(id);
                Cache.Client.Store(StoreMode.Set, key, user);
                return user;
            }
        }
    }

    public class UserRepository
    {
        private List<User> _db;

        public UserRepository()
        {
            _db = new List<User>();

            _db.AddRange(new[]
            {
                new User { Id = 1, Accout = "admin1", Password = "000000", Name = "管理员1" },
                new User { Id = 2, Accout = "admin2", Password = "000000", Name = "管理员2" },
                new User { Id = 3, Accout = "admin3", Password = "000000", Name = "管理员3" },
            });
        }

        public void Add()
        {

        }

        public void Remove()
        {

        }

        public void Save()
        {

        }

        public IEnumerable<User> Get()
        {
            return _db;
        }

        public User GetById(int id)
        {
            return _db.FirstOrDefault(m => m.Id == id);
        }
    }

    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Accout { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"id:{this.Id} account:{this.Accout} password:{this.Password} name:{this.Name}";
        }
    }
}
