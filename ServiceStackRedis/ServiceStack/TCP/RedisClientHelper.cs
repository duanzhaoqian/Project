using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ServiceStack.Redis;

namespace ServiceStack.TCP
{
    public class RedisClientHelper
    {
        private static Dictionary<string, RedisClient> poolDir = new Dictionary<string, RedisClient>();
        /// <summary>
        /// 获取连接池
        /// </summary>
        //private static Dictionary<string, RedisClient> redisPool
        //{
        //    get
        //    {
        //        if (poolDir.Count <= 0)
        //        {
        //            foreach (RedisHostEntity hostItem in GetRedisHostList())
        //            {

        //                if (poolDir.ContainsKey(hostItem.Key))
        //                {
        //                    continue;
        //                }
        //                try
        //                {

        //                    poolDir.Add(hostItem.Key, new RedisClient(hostItem.Host, Convert.ToInt32(hostItem.Port)));
        //                }
        //                catch (Exception ex)
        //                {
        //                }
        //            }
        //        }
        //        return poolDir;
        //    }
        //}
        private static object MyLock = new object();
        private static List<RedisHostEntity> redisHostList = null;
        public static List<RedisHostEntity> GetRedisHostList()
        {
            try
            {
                lock (MyLock)
                {
                    if (redisHostList == null)
                    {
                        redisHostList = new Tool().LoadTCPRedisConfigureToRedis();
                    }
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
        public RedisClient Distribution(Enum FunctionTypeEnum, int CityId)
        {
            CityEnum cityEnum = GetRedisTypeByCityId(CityId);
            if (FunctionTypeEnum == null)
            {
                throw new Exception("FunctionTypeEnum 不能为空");
            }
            string key = "RedisServerDefault";
            string City = cityEnum == null ? string.Empty : cityEnum.ToString();
            string Type = FunctionTypeEnum.ToString();

            if (Type == "Test")
            {
                key = "Test";
            }
            else if (Type.ToLower().Contains("NEWSANDINFORMATION".ToLower()))
            {
                key = "RedisServerDefault";
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
            //    Console.WriteLine("key" + key);
                var hostItem = GetRedisHostList().Find(t => t.Name == key);
                //string poolkey = hostItem.Host.Replace(":", "_");
                //if (redisPool.ContainsKey(hostItem.Key))
                //{
                //    RedisClient redisHost = null;
                //    bool isHost = redisPool.TryGetValue(hostItem.Key, out redisHost);
                //    if (isHost)
                //    {
                //        return redisHost;
                //    }
                //}
                File.AppendAllText("RedisHost.txt", hostItem.Host + "->>" + hostItem.Key + "->>" + hostItem.Name + "->>" + hostItem.Port + "->>" + DateTime.Now + Environment.NewLine);
                return new RedisClient(hostItem.Host, Convert.ToInt32(hostItem.Port));
                //redisPool.Add(hostItem.Key, host);
                //return host;
                //return new RedisHost(hostItem.Host, Convert.ToInt32(hostItem.Connections));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 城市分发
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public static CityEnum GetRedisTypeByCityId(int CityId)
        {
            switch (CityId)
            {
                case 253:
                    return CityEnum.BeiJing;
                case 239:
                    return CityEnum.ShangHai;
                case 344:
                    return CityEnum.GuangZhou;
                case 355:
                    return CityEnum.ShenZhen;
                case 205:
                    return CityEnum.TianJin;
                case 263:
                    return CityEnum.QingDao;
                case 307:
                    return CityEnum.HangZhou;
                case 155:
                    return CityEnum.ChengDu;
                case 243:
                    return CityEnum.NanJing;
                case 296:
                    return CityEnum.Dalian;
                default:
                    return CityEnum.Other;
            }
        }
    }
}
