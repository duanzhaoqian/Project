using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ServiceStack;

namespace KYJ.ZS.Commons.Common
{
    internal class RedisHelper
    {
        //private static object lockobject = new object();
        //private static ServiceStack.Redis.RedisClient client;

        public static string IMGPATH_BASE;
        public static string USER_IMGPATH_BASE;
        public static string IMGWEB_BASE;
        public static string USER_IMGWEB_BASE;
        public static string WaterMarkPic;
        public static string VILLAGE_BASE;
        public static string VILLAGE_WEB_BASE;

        static RedisHelper()
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
            }
        }

        public static void Init()
        {
            try
            {
                string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                xml.Load(s.Substring(0, s.LastIndexOf(@"\")) + "\\RedisConfigure.xml");
                XmlAttributeCollection child = xml.ChildNodes[0].Attributes;
                IMGPATH_BASE = child["IMGPATH_BASE"].Value;
                USER_IMGPATH_BASE = child["USER_IMGPATH_BASE"].Value;
                IMGWEB_BASE = child["IMGWEB_BASE"].Value;
                USER_IMGWEB_BASE = child["USER_IMGWEB_BASE"].Value;

                VILLAGE_BASE = child["VILLAGE_BASE"].Value;
                VILLAGE_WEB_BASE = child["VILLAGE_WEB_BASE"].Value;

                WaterMarkPic = IMGPATH_BASE + "watermark.png";

            }
            catch (Exception ex)
            {
            }

        }

        public static bool AddItemToList<T>(T info, string key, DateTime? expireDateTime, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.AddItemToList<T>(key, info, expireDateTime, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetAllItemsFromList<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetAllItemsFromList<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetValues<T>(Enum FunctionTypeEnum, int CityId, string keys)
        {
            return KYJ.ServiceStack.Redis.GetValues<T>(FunctionTypeEnum, GetRedisTypeByCityId(CityId), keys);
        }

        public static double GetNextSequence(Enum FunctionTypeEnum, int CityId, string key, DateTime expireDateTime)
        {
            return KYJ.ServiceStack.Redis.GetNextSequence(FunctionTypeEnum, GetRedisTypeByCityId(CityId), key, expireDateTime);
        }

        public static double GetNextSequence(Enum FunctionTypeEnum, int CityId, string key)
        {
            return KYJ.ServiceStack.Redis.GetNextSequence(FunctionTypeEnum, GetRedisTypeByCityId(CityId), key);
        }

        public static bool RemoveAllFromList(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.Remove(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static bool RemoveItemFromList<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
           return KYJ.ServiceStack.Redis.RemoveItemFromList<T>(key, info, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static void AddItemToSortedSet<T>(T info, string key, double scores, DateTime? expireDateTime, Enum FunctionTypeEnum, int CityId)
        {
            KYJ.ServiceStack.Redis.AddItemToSortedSet<T>(key, info, scores, expireDateTime, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static bool RemoveItemFromSortedSet<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.RemoveItemFromSortedSet<T>(key, info, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetAllItemsFromSortedSet<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static IDictionary<T, double> GetAllWithScoresFromSortedSet<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetAllWithScoresFromSortedSet<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }


        public static double GetItemScoreInSortedSet<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetItemScoreInSortedSet<T>(key, info, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static int GetSortedSetCount<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetSortedSetCount(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static int GetNextSequence<T>(Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetNextSequence<T>(FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByHighestScore<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByHighestScore<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByLowestScore<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByLowestScore<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static bool SetValue<T>(string key, T info, DateTime? expireDateTime, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.Set<T>(key, info, expireDateTime, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }

        public static T GetValue<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return KYJ.ServiceStack.Redis.Get<T>(key, FunctionTypeEnum, GetRedisTypeByCityId(CityId));
        }
        public static Enum GetRedisTypeByCityId(int CityId)
        {
            if (CityId == 0)
            {
                return null;
            }
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
                    return null;
            }
        }
    }
}
