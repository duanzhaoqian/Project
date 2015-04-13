using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.TCP
{
    public class InitHelper
    {
        /*
        private static Dictionary<string, RedisHost> poolDir = new Dictionary<string, RedisHost>();
        /// <summary>
        /// 获取连接池
        /// </summary>
        private static Dictionary<string, RedisHost> redisPool
        {
            get
            {
                if (poolDir.Count <= 0)
                {
                    foreach (RedisHostEntity hostItem in GetRedisHostList())
                    {
                        string poolkey = hostItem.Host.Replace(":", "_");
                        if (poolDir.ContainsKey(poolkey))
                        {
                            continue;
                        }
                        try
                        {
                            poolDir.Add(poolkey, new RedisHost(hostItem.Host, Convert.ToInt32(hostItem.Connections)));
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                return poolDir;
            }
        }
         * */
        private static List<RedisHostEntity> redisHostList = null;
        public static List<RedisHostEntity> GetRedisHostList()
        {
            try
            {
                if (redisHostList == null)
                {
                    redisHostList = new Tool().LoadTCPRedisConfigureToRedis();
                }
                return redisHostList;
            }
            catch (Exception ex)
            {
                return new List<RedisHostEntity>();
            }
        }

        /// <summary>
        /// 分发
        /// </summary>
        public RedisHost Distribution(Enum FunctionTypeEnum, Enum CityEnum)
        {
            if (FunctionTypeEnum == null)
            {
                throw new Exception("FunctionTypeEnum 不能为空");
            }
            string key = "RedisServerDefault";
            string City = CityEnum == null ? string.Empty : CityEnum.ToString();
            string Type = FunctionTypeEnum.ToString();

            if (Type=="Test")
            {
                key = "Test";
            }
            else if (Type.Contains("NewHouse"))
            {
                key = "NewHouse";
            }
            else if (Type.Contains("KYD") || Type.Contains("BaseData"))
            {
                key = "KuaiYouDai";
            }
            else if (Type == "HouseImage")
            {
                key = "HouseImage";
            }
            else if (Type == "UserHeadImage")
            {
                key = "UserHeadImage";
            }
            else if (City == "Other" || Type == "BidPrice" || Type == "WebDBData" || Type == "HouseDBData")
            {
                //如果是other要存到出价服务器中
                key = "Other";
            }
            else if (Type == "ViewCount")
            {
                key = "ViewCount";
            }
            try
            {
                var hostItem = GetRedisHostList().Find(t => t.Name == key);
                //string poolkey = hostItem.Host.Replace(":", "_");
                //if (redisPool.ContainsKey(poolkey))
                //{
                //    RedisHost redisHost = null;
                //    bool isHost = redisPool.TryGetValue(poolkey, out redisHost);
                //    if (isHost)
                //    {
                //        return redisHost;
                //    }
                //}
                //RedisHost host = new RedisHost(hostItem.Host, Convert.ToInt32(hostItem.Connections));
                //redisPool.Add(poolkey, host);
                //return host;
                return new RedisHost(hostItem.Host, Convert.ToInt32(hostItem.Connections));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
