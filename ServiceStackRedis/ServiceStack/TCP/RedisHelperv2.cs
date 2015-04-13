using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.TCP
{
    public class RedisHelperv2
    {
        private static Redis.RedisClient GetClient(Enum FunctionTypeEnum, int CityId)
        {
            ServiceStack.Redis.RedisClient client = new RedisClientHelper().Distribution(FunctionTypeEnum, CityId);
            client.SendTimeout = 5000;
            return client;
        }
        #region get set
        /// <summary>
        /// set
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="info">值</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市id</param>
        /// <param name="dataType">所属类型String|Json|Protobuf，如果是对象可以传Json或Protobuf</param>
        /// <param name="expireMinute">生命周期，分钟</param>
        /// <returns>是否成功</returns>
        public static bool SetValue<T>(string key, T info, Enum FunctionTypeEnum, int CityId, double expireMinute = 0)
        {
            bool state = false;
            try
            {
                //RedisKey.GetType<T>(key, dataType).Set(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), info, expireMinute);
                using (Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    if (expireMinute > 0)
                        state = client.Set<T>(key, info, DateTime.Now.AddMinutes(expireMinute));
                    else
                        state = client.Set<T>(key, info);
                }
            }
            catch (Exception ex)
            {
            }
            return state;
        }


        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市id</param>
        /// <param name="dataType">所属类型String|Json|Protobuf，如果是对象可以传Json或Protobuf</param>
        /// <returns>数据集</returns>
        public static T GetValue<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            //return RedisKey.GetType<T>(key, DataType.Json).Get<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
            T t = default(T);
            using (Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
            {
                t = client.Get<T>(key);
            }
            return t;
        }

        public static IDictionary<string, T> GetValues<T>(List<string> keys, Enum FunctionTypeEnum, int CityId)
        {
            IDictionary<string, T> t = null;
            using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
            {
                string[] arrkeys = keys.ToArray();
                t = client.GetAll<T>(arrkeys);
            }
            return t;
            //return RedisKey.GetType<T>(arrkeys, DataType.Json).Get<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), keys.Count);
        }
        #endregion

        #region list

        /// <summary>
        /// 添加单条数据 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="info">值</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市id</param>
        /// <param name="dataType">所属类型String|Json|Protobuf，如果是对象可以传Json或Protobuf</param>
        /// <param name="expireMinute">生命周期，分钟</param>
        /// <returns>是否成功</returns>
        public static bool AddItemToList<T>(string key, T info, Enum FunctionTypeEnum, int CityId, double expireMinute = 0)
        {
            bool state = false;
            int currentCount = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        //RedisKey.GetType<T>(key, dataType).LstAdd(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), info, expireMinute);
                        //return true;
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            typedclient.AddItemToList(typedclient.Lists[key], info);
                            if (expireMinute > 0)
                            {
                                typedclient.ExpireEntryAt(key, DateTime.Now.AddMinutes(expireMinute));
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        currentCount++;
                    }
                }
                state = true;
            }
            catch (Exception ex)
            {
            }
            return state;
        }

        /// <summary>
        /// 获取list的所有值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>数据集</returns>
        public static IList<T> GetAllItemsFromList<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            //return RedisKey.GetType<T>(key, DataType.Json).LstRange<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
            IList<T> imglist = null;
            int currentCount = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            imglist = typedclient.GetAllItemsFromList(typedclient.Lists[key]);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return imglist;
        }

        /// <summary>
        /// 移除list项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="info">值</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        public static bool RemoveItemFromList<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            //try
            //{
            //RedisKey.GetType<T>(key, DataType.Json).LstLREM<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), info);
            //return true;

            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            bool state = false;
            int currentCount = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            typedclient.RemoveItemFromList(typedclient.Lists[key], info);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
                state = true;
            }
            catch (Exception ex)
            {
            }
            return state;
        }
        #endregion

        #region SortedSet

        /// <summary>
        /// 有序set
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="info">值</param>
        /// <param name="key">键</param>
        /// <param name="scores">权重</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="expireMinute">生命周期：分钟</param>
        /// <returns>是否成功</returns>
        public static bool AddItemToSortedSet<T>(T info, string key, double scores, Enum FunctionTypeEnum, int CityId, double expireMinute = 0)
        {
            //try
            //{
            //    RedisKey.GetType<T>(key, dataType).SetZADD<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), info, scores, expireMinute);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            bool state = false;
            int currentCount = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            typedclient.AddItemToSortedSet(typedclient.SortedSets[key], info, scores);
                            if (expireMinute > 0)
                            {
                                typedclient.ExpireEntryAt(key, DateTime.Now.AddMinutes(expireMinute));
                            }
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
                state = true;
            }
            catch (Exception ex)
            {
            }
            return state;
        }

        /// <summary>
        /// 移除set项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="info">值</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>是否成功</returns>
        public static bool RemoveItemFromSortedSet<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            //try
            //{
            //    RedisKey.GetType<T>(key, DataType.Json).SetZREM<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), info);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            bool result = false;
            int currentCount = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            result = typedclient.RemoveItemFromSortedSet(typedclient.SortedSets[key], info);
                            string sss = ServiceStack.Text.JsonSerializer.SerializeToString<T>(info);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 获取set集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>数据集</returns>
        public static IList<T> GetAllItemsFromSortedSet<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            //return RedisKey.GetType<T>(key, DataType.Json).SetZRANGE<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
            var imglist = new List<T>();
            int currentCount = 0;
            try
            {
                using (Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            imglist = typedclient.GetAllItemsFromSortedSet(typedclient.SortedSets[key]);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return imglist;

        }

        /// <summary>
        /// 获取set集合及权重
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">类型</param>
        /// <param name="CityId">城市id</param>
        /// <returns>数据集</returns>
        public static IDictionary<T, double> GetAllWithScoresFromSortedSet<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            //return RedisKey.GetType<T>(key, DataType.Json).SetZRANGEWITHSCORE<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
            IDictionary<T, double> imglist = null;
            int currentCount = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            imglist = typedclient.GetAllWithScoresFromSortedSet(typedclient.SortedSets[key]);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return imglist;
        }

        /// <summary>
        /// 读取set的权重值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="info">值</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>权重值</returns>
        public static double GetItemScoreInSortedSet<T>(string key, T info, Enum FunctionTypeEnum, int CityId)
        {
            //return RedisKey.GetType<T>(key, DataType.Json).SetZSCORE<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), info);
            double score = 0;
            int currentCount = 0;
            try
            {
                //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            score = typedclient.GetItemScoreInSortedSet(typedclient.SortedSets[key], info);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return score;
        }

        /// <summary>
        /// 获取set的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>数量</returns>
        public static int GetSortedSetCount(string key, Enum FunctionTypeEnum, int CityId)
        {
            // return key.RedisString().SetCOUNT(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
            int result = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    result = client.GetSortedSetCount(key);
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 获取set集合从权重高-低
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">类型</param>
        /// <param name="CityId">城市id</param>
        /// <returns>数据集</returns>
        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByHighestScore<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            //return RedisKey.GetType<T>(key, DataType.Json).SetZREVRANGE<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
            IDictionary<T, double> imglist = null;
            int currentCount = 0;
            try
            {
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            imglist = typedclient.GetRangeWithScoresFromSortedSetByHighestScore(typedclient.SortedSets[key], 1000, 0, 0, 1);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return imglist;
        }


        /// <summary>
        /// 获取set集合从权重低-高
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">类型</param>
        /// <param name="CityId">城市id</param>
        /// <returns>数据集</returns>
        public static IDictionary<T, double> GetRangeWithScoresFromSortedSetByLowestScore<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            //return RedisKey.GetType<T>(key, DataType.Json).SetZRANGEWITHSCORE<T>(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
            IDictionary<T, double> imglist = null;
            int currentCount = 0;
            try
            {
                //Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId);
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    while (currentCount < client.RetryCount + 1)
                    {
                        try
                        {
                            ServiceStack.Redis.Generic.IRedisTypedClient<T> typedclient = client.GetTypedClient<T>();
                            imglist = typedclient.GetRangeWithScoresFromSortedSetByLowestScore(typedclient.SortedSets[key], 0, 1000, 0, 1);
                            break;
                        }
                        catch (Exception ex)
                        {

                        }
                        currentCount++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return imglist;
        }
        #endregion


        #region Sequence

        /// <summary>
        /// 获取自增列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="FunctionTypeEnum"></param>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public static int GetNextSequence(string key, Enum FunctionTypeEnum, int CityId)
        {
            //return key.RedisString().INCR(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId), expireMinute);
            int result = 0;
            using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
            {
                result = client.Incr(key);
            }
            return result;
        }

        #endregion


        #region 删除

        /// <summary>
        /// 删除单个键
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>是否成功</returns>
        public static bool Remove(string key, Enum FunctionTypeEnum, int CityId)
        {
            bool state = false;
            try
            {
                //key.RedisString().Delete(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
                //return true;
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    state = client.Remove(key);
                }
            }
            catch (Exception ex)
            {
            }
            return state;
        }

        /// <summary>
        /// 删除多个键
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="FunctionTypeEnum">redis类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>是否成功</returns>
        public static bool RemoveAll(string[] keys, Enum FunctionTypeEnum, int CityId)
        {
            bool state = false;
            try
            {
                //keys.RedisString().Delete(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    client.RemoveAll(keys);
                    state = true;
                }
            }
            catch (Exception ex)
            {
            }
            return state;
        }
        #endregion


        #region keys


        /// <summary>
        /// 获取相关键
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="FunctionTypeEnum">类型</param>
        /// <param name="CityId">城市ID</param>
        /// <returns>键集合</returns>
        public static IList<string> Keys(string key, Enum FunctionTypeEnum, int CityId)
        {
            IList<string> list = new List<string>();
            try
            {
                //return key.RedisString().Keys(FunctionTypeEnum, RedisKey.GetRedisTypeByCityId(CityId));
                using (ServiceStack.Redis.RedisClient client = GetClient(FunctionTypeEnum, CityId))
                {
                    byte[][] b = client.Keys(key);
                    for (int i = 0; i < b.Length; i++)
                    {
                        byte[] bone = b[i];
                        string str = System.Text.Encoding.Default.GetString(bone);
                        list.Add(str);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        #endregion

    }
}
