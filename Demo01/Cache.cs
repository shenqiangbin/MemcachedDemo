﻿using Memcached.ClientLibrary;
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

            //各服务器之间负载均衡的设置比例
            pool.SetWeights(new int[] { 1 });
            //初始化时创建连接数
            pool.InitConnections = 3;
            //最小连接数
            pool.MinConnections = 3;
            //最大连接数
            pool.MaxConnections = 5;
            //连接的最大空闲时间，下面设置为6个小时（单位ms），超过这个设置时间，连接会被释放掉
            pool.MaxIdle = 1000 * 60 * 60 * 6;
            //socket连接的超时时间，下面设置表示不超时（单位ms），即一直保持链接状态
            pool.SocketConnectTimeout = 0;
            //通讯的超市时间，下面设置为3秒（单位ms），.Net版本没有实现
            pool.SocketTimeout = 1000 * 3;
            //维护线程的间隔激活时间，下面设置为30秒（单位s），设置为0时表示不启用维护线程
            pool.MaintenanceSleep = 30;
            //设置SocktIO池的故障标志
            pool.Failover = true;
            //是否对TCP/IP通讯使用nalgle算法，.net版本没有实现
            pool.Nagle = false;
            //socket单次任务的最大时间（单位ms），超过这个时间socket会被强行中端掉，当前任务失败。
            pool.MaxBusy = 1000 * 10;

            pool.Initialize();
        }
    }
}
