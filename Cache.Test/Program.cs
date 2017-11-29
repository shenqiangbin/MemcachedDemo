using MySql.Data.MySqlClient;
using MySqlDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Cache.Test
{
    class Program
    {
        private static readonly object _newCodeLocker = new object();
        static void Main(string[] args)
        {
            Parallel.For(0, 10, i =>
            {
                lock (_newCodeLocker)
                {
                    var code = GetNewCode("user");
                    Console.WriteLine(code);
                }
            });
            //目前是12，取10个值后，应为22
            //在没有lock的时候，取的值是不对的，只有lock了，取的值才是对的。

            //TestSync();
            //for (int i = 0; i < 10; i++)
            //{
            //    var code = GetNewCode("user");
            //    Console.WriteLine(code);
            //}

            Console.ReadKey();
        }

        //验证Cache的时间过期
        static void TestTime()
        {
            MemoryCache cache = MemoryCache.Default;
            cache.Add("key1", "{name:abc,age:2}", DateTime.Now.AddSeconds(5));

            foreach (var item in cache)
            {
                Console.WriteLine(item);
            }

            for (int i = 0; i < 10; i++)
            {
                GetVal(cache);
                Thread.Sleep(new TimeSpan(0, 0, 1));
            }
        }

        static void GetVal(MemoryCache cache)
        {
            object val = cache.Get("key1");
            Console.WriteLine(val);

        }



        static int totalNum = 10;

        static void TestSync()
        {
            Parallel.For(0, 10, i => Withdraw(1));
            Console.WriteLine("finally-totalNum" + totalNum);
        }

        static void Withdraw(int number)
        {
            Console.WriteLine(string.Format("{0}:{1}", totalNum, number));
            while (totalNum >= number)
            {
                Thread.Sleep(new TimeSpan(0, 0, 1));
                totalNum -= number;
                Console.WriteLine("totalNum" + totalNum);
            }
        }

        static string GetNewCode(string tableName)
        {
            MySqlRepository res = new MySqlRepository();

            using (var conn = res.GetConn())
            {
                string sql = $"select id from dbcode where tablename = '{tableName}'";
                int numVal = conn.ExecuteScalar<int>(sql);
                if (numVal == 0)
                {
                    int num = 1;
                    string insertSql = $"insert into dbcode(tablename,id) values('{tableName}','{num}')";
                    int result = conn.Execute(insertSql);
                    if (result > 0)
                        return num.ToString();
                    else
                        throw new Exception("新增code失败");
                }
                else
                {
                    numVal = numVal + 1;
                    string updateSql = $"update dbcode set id = {numVal} where tablename = '{tableName}'";
                    int result = conn.Execute(updateSql);
                    if (result > 0)
                        return numVal.ToString();
                    else
                        throw new Exception("更新code失败");
                }
            }
        }
    }
}
