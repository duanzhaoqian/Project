using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Caching;

namespace ServiceStack.RCache
{
    public class AddCache
    {
        private readonly static int CacheMode = 1;// Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CacheMode"]);
        public static void Add(string CacheName, object CacheValue, int Minute)
        {
            //LogCacheKey(CacheName);
            if (CacheName.Length > 50)
            {
                CacheName = KYJ.ServiceStack.Tool.GetMd5(CacheName);
            }
            switch (CacheMode)
            {
                case 0://不使用缓存

                    return;
                case 1://.net缓存
                    System.Web.HttpContext.Current.Cache.Insert(CacheName, CacheValue, null, DateTime.Now.AddMinutes(Minute), System.Web.Caching.Cache.NoSlidingExpiration);
                    break;
                case 2://memorycache(还未实现)
                    return;
                case 3://winform或控制台服务器缓存
                    DeskAppCache.Insert(CacheName, CacheValue, null, DateTime.Now.AddMinutes(Minute), System.Web.Caching.Cache.NoSlidingExpiration);
                    break;
            }
        }
        public static Object Get(string cacheName)
        {
            if (cacheName.Length > 50)
            {
                cacheName = KYJ.ServiceStack.Tool.GetMd5(cacheName);
            }
            switch (CacheMode)
            {

                case 0://不使用缓存
                    return null;
                case 1://.net缓存
                    return (Object)HttpContext.Current.Cache[cacheName];
                case 2://memorycache(还未实现)
                    return null; ;
                case 3://winform或控制台服务器缓存
                    return (Object)DeskAppCache[cacheName];

            }
            return null;
        }
        #region 桌面程序Cache专用
        private static HttpRuntime _httpRuntime;
        private static Cache DeskAppCache
        {
            get
            {
                EnsureHttpRuntime();
                return HttpRuntime.Cache;
            }
        }
        private static void EnsureHttpRuntime()
        {
            if (null == _httpRuntime)
            {
                try
                {
                    Monitor.Enter(typeof(AddCache));
                    if (null == _httpRuntime)
                    {
                        // Create an Http Content to give us access to the cache.
                        _httpRuntime = new HttpRuntime();
                    }
                }
                finally
                {
                    Monitor.Exit(typeof(AddCache));
                }
            }
        }
        #endregion

    }
}
