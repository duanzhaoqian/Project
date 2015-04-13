using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using System.Web;

namespace KYJ.ServiceStack
{
    public class Redis
    {
        /// <summary>
        /// 自增标识
        /// </summary>
        /// <typeparam name="T">自增长的类型</typeparam>
        /// <param name="CityEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static int GetNextSequence<T>(Enum FunctionTypeEnum, Enum CityEnum)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            StringBuilder sb = new StringBuilder(LuaPostUrl).Append("?opt=incr&key=seq:" + typeof(T).Name);
            int temp = Tool.ToInt32(Tool.GetHtml(sb.ToString()));
            if (temp == 0)
            {
                Random ro = new Random();
                return ro.Next(100000000, 900000000);
            }
            return temp;
           
        }

        /// <summary>
        /// 自增标识
        /// </summary>
        /// <returns></returns>
        public static double GetNextSequence(Enum FunctionTypeEnum, Enum CityEnum, string key)
        {
            return GetNextSequence(FunctionTypeEnum, CityEnum, key, null);
        }
        public static double GetNextSequence(Enum FunctionTypeEnum, Enum CityEnum, string key, DateTime? expireDateTime)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            //string incrkey = "hid:";
            //if (FunctionTypeEnum.ToString()=="NewHouseViewCount")
            //{
            //    incrkey = "nhid:";
            //}
            StringBuilder sb = new StringBuilder(LuaPostUrl).Append("?opt=incr&key=").Append(key);
            double temp = Tool.ToDouble(Tool.GetHtml(sb.ToString()));
            if (temp == 0)
            {
                Random ro = new Random();
                return ro.Next(100000000, 900000000);
            }
            SaveExpireDateTime(key, expireDateTime, FunctionTypeEnum, CityEnum);
            return temp;
        }
        /// <summary>
        /// 自增标识IncrBy
        /// </summary>
        /// <returns></returns>
        public static double GetNextSequence(Enum FunctionTypeEnum, Enum CityEnum, string key, double value)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            StringBuilder sb = new StringBuilder(LuaPostUrl).Append("?opt=incrby&key=").Append(key).Append("&val=").Append(value);
            double temp = Tool.ToDouble(Tool.GetHtml(sb.ToString()));
            if (temp == 0)
            {
                Random ro = new Random();
                return ro.Next(100000000, 900000000);
            }
            return temp;
        }
        public static double GetOneViewCountValue(Enum FunctionTypeEnum, Enum CityEnum, string key)
        {
            try
            {
                string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
                StringBuilder sb = new StringBuilder(LuaPostUrl).Append("?opt=get&key=");
                sb.Append(key.Trim());
                string html = Tool.GetHtml(sb.ToString());
                if (string.IsNullOrEmpty(html) || html.Contains("{}"))
                {
                    return 0.0;
                }
                html = html.Replace("null,", string.Empty);
                return Tool.ToDouble(html);

            }
            catch //(Exception ex)
            {
                return 0.0;
            }
        }

        public static T GetValues<T>(Enum FunctionTypeEnum, Enum CityEnum, string keys)
        {
            try
            {
                string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
                string[] arr = keys.Split(',');
                StringBuilder sbStr = new StringBuilder();
                foreach (string item in arr)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }
                    //sbStr.Append("hid:").Append(item).Append(",");
                    sbStr.Append(item).Append(",");
                }
                //LuaPostUrl
                StringBuilder sb = new StringBuilder().Append("opt=mget&key=");
                sb.Append(sbStr.ToString().TrimEnd(','));
                string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
                if (string.IsNullOrEmpty(html) || html.Contains("{}"))
                {
                    return default(T);
                }
                //html = html.Replace("null,", string.Empty);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(html);

            }
            catch //(Exception ex)
            {
                return default(T);
            }
        }

        #region list

        /// <summary>
        /// 根据键存储Redis值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <param name="expireDateTime"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        public static bool AddItemToList<T>(string key, T info, DateTime? expireDateTime, Enum FunctionTypeEnum, Enum CityEnum)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            string html = RedisCommon.GetLPUSHData<T>(key, info, LuaPostUrl);
            if (Tool.ToInt32(html, 0) > 0)
            {
                SaveExpireDateTime(key, expireDateTime, FunctionTypeEnum, CityEnum);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据键存储Redis值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static T GetAllItemsFromList<T>(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            try
            {
                string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
                StringBuilder sb = new StringBuilder().Append("opt=lrange&n=1&key=");
                sb.Append(key);
                string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
                if (string.IsNullOrEmpty(html) || html == "{}")
                {
                    return default(T);
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Tool.GetObjectToJson<T>(html));
            }
            catch// (Exception ex)
            {
                return default(T);
            }
        }


        /// <summary>
        /// 根据键删除Redis值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        public static bool Remove(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            StringBuilder sb = new StringBuilder().Append("opt=del&key=");
            sb.Append(key);
            //sb.Append("%0D%0A");
            string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
            return Tool.ToInt32(html) == 1 ? true : false;
        }

        /// <summary>
        /// 删除List键的某一项值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        public static bool RemoveItemFromList<T>(string key, T info, Enum FunctionTypeEnum, Enum CityEnum)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            string result = RedisCommon.GetLREMData<T>(key, info, LuaPostUrl);
            if (Tool.ToInt32(result) != 0)
            {
                return true;
            }
            return false;
        }
        #endregion


        #region set
        /// <summary>
        /// 根据键存储Redis值为SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <param name="expireDateTime"></param>
        /// <param name="RedisType">ServiceStack.FunctionTypeEnum枚举</param>
        public static bool AddItemToSortedSet<T>(string key, T info, double scores, DateTime? expireDateTime, Enum FunctionTypeEnum, Enum CityEnum)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            string html = RedisCommon.GetZADDData<T>(key, info, scores, LuaPostUrl);
            if (Tool.ToInt32(html, 0) > 0)
            {
                SaveExpireDateTime(key, expireDateTime, FunctionTypeEnum, CityEnum);

                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expireDateTime"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.FunctionType枚举</param>
        private static void SaveExpireDateTime(string key, DateTime? expireDateTime, Enum FunctionTypeEnum, Enum CityEnum)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            if (expireDateTime == null)
            { return; }
            StringBuilder sb = new StringBuilder();
            TimeSpan span = (TimeSpan)(expireDateTime - DateTime.Now);
            string time = span.TotalSeconds.ToString();
            time = time.Split('.')[0];
            sb.Append("opt=cmd&n=1&cmd=");
            sb.Append("EXPIRE ").Append(key).Append(" ").Append(time);
            sb.Append("%0D%0A");
            string htmlexpireDateTime = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
        }



        /// <summary>
        /// 根据键获取Redis值为SortedSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static T GetAllItemsFromSortedSet<T>(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            try
            {
                T t = default(T);
                StringBuilder sb = new StringBuilder().Append("opt=zrange&n=1&key=");
                sb.Append(key);

                string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
                string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
                if (string.IsNullOrEmpty(html) || html.Contains("{}"))
                {
                    return t;
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Tool.GetObjectToJson<T>(html));
            }
            catch// (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 根据键、值删除某项数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        public static bool RemoveItemFromSortedSet<T>(string key, T info, Enum FunctionTypeEnum, Enum CityEnum)
        {
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            string result = RedisCommon.GetZREMData<T>(key, info, LuaPostUrl);
            if (Tool.ToInt32(result) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据键获取值和权重
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static IDictionary<T, double> GetAllWithScoresFromSortedSet<T>(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("opt=zrangebyscore&key=");
            sb.Append(key);

            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
            return RedisCommon.GetWeightObject<T>(html);
        }

        /// <summary>
        /// 获取该键 值里的权重
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static double GetItemScoreInSortedSet<T>(string key, T info, Enum FunctionTypeEnum, Enum CityEnum)
        {

            List<string> list = Tool.GetJsonToObject<T>(info);
            foreach (string item in list)
            {
                string itemVal = Tool.SetReplaceValue(item);
                StringBuilder sb = new StringBuilder().Append("opt=zscore&key=");
                sb.Append(key).Append("&val=").Append(itemVal);

                string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);

                string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
                if (string.IsNullOrEmpty(html) || html == "null")
                {
                    return 0;
                }
                return Convert.ToDouble(html.Replace("\"", string.Empty));
            }
            return 0;
        }
        /// <summary>
        /// 根据KEY获取当前KEY存储值的数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static int GetSortedSetCount(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("opt=zcount&key=");
            sb.Append(key);

            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            return Tool.ToInt32(Tool.GetPostHtml(LuaPostUrl, sb.ToString()));
        }
        /// <summary>
        /// 递增排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByHighestScore<T>(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            //ZRANGE salary 0 -1 WITHSCORES        # 递增排列
            StringBuilder sb = new StringBuilder();
            sb.Append("opt=rangewithhighestscores&key=");
            sb.Append(key);
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
            return RedisCommon.GetWeightObject<T>(html);
        }
        /// <summary>
        /// 递减排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByLowestScore<T>(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            // ZREVRANGE salary 0 -1 WITHSCORES     # 递减排列
            StringBuilder sb = new StringBuilder();
            sb.Append("opt=rangewithlowestscores&key=");
            sb.Append(key);
            string LuaPostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            string html = Tool.GetPostHtml(LuaPostUrl, sb.ToString());
            return RedisCommon.GetWeightObject<T>(html);
        }

        #endregion

        #region GetSet
        /// <summary>
        /// SET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <param name="expireDateTime"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static bool Set<T>(string key, T info, DateTime? expireDateTime, Enum FunctionTypeEnum, Enum CityEnum)
        {

            string PostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
            StringBuilder sb = new StringBuilder().Append("opt=set&key=");
            string content = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            content = Tool.SetReplaceValue(content);
            sb.Append(key);
            sb.Append("&val=").Append(content);
            string html = Tool.GetPostHtml(PostUrl, sb.ToString());
            if (html.Contains("OK") || html.Contains("ok"))
            {
                SaveExpireDateTime(key, expireDateTime, FunctionTypeEnum, CityEnum);
                return true;
            }
            return false;
        }


        /// <summary>
        /// GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static T Get<T>(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            try
            {
                //string RedisServerKey = RedisType.ToString() + FunctionType.ToString();
                string PostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
                StringBuilder sb = new StringBuilder().Append("opt=get&key=");
                sb.Append(key);
                string html = Tool.GetPostHtml(PostUrl, sb.ToString()).Replace("\"", "");
                if (string.IsNullOrEmpty(html) || html == "error")
                {
                    return default(T);
                }
                html = Tool.GetReplaceValue(html);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(html);
            }
            catch// (Exception ex)
            {
                return default(T);
            }
        }


        /// <summary>
        /// GET Keys
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum">ServiceStack.RedisType枚举</param>
        /// <returns></returns>
        public static T GetKeys<T>(string key, Enum FunctionTypeEnum, Enum CityEnum)
        {
            try
            {
                //string RedisServerKey = RedisType.ToString() + FunctionType.ToString();
                string PostUrl = Tool.GetRedisPostUrl(FunctionTypeEnum, CityEnum);
                StringBuilder sb = new StringBuilder().Append("opt=keys&key=");
                sb.Append(key);
                string html = Tool.GetPostHtml(PostUrl, sb.ToString()).Replace("\"", "");
                if (string.IsNullOrEmpty(html) || html == "error")
                {
                    return default(T);
                }
                html = Tool.GetReplaceValue(html);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(html);
            }
            catch// (Exception ex)
            {
                return default(T);
            }
        }
        #endregion
    }
}
