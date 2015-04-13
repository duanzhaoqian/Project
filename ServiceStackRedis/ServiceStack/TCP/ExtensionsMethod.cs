using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.TCP
{
    public static class BeetleRedisExtensionsMethod
    {


        public static string GetString(this ArraySegment<byte> data)
        {
            return utils.GetString(data);
        }

        public static T GetJson<T>(this ArraySegment<byte> data)
        {
            return (T)utils.GetJson(data, typeof(T));
        }

        public static T GetProtobuf<T>(this ArraySegment<byte> data)
        {
            return (T)utils.GetProtobuf(data, typeof(T));
        }

        public static object GetJson(this ArraySegment<byte> data, Type type)
        {
            return utils.GetJson(data, type);
        }

        public static object GetProtobuf(this ArraySegment<byte> data, Type type)
        {
            return utils.GetProtobuf(data, type);
        }
        public static IList<object> FieldValueToList(this IEnumerable<Field> items)
        {
            List<object> result = new List<object>();
            foreach (Field item in items)
            {
                result.Add(item.Value);
            }
            return result;
        }
    }

    public static class BeetleRedisGetExtensionsMethod
    {
        public static RedisKey RedisString(this IEnumerable<string> key)
        {
            return new RedisKey(key, DataType.String);
        }
        public static RedisKey RedisJson(this IEnumerable<string> key)
        {
            return new RedisKey(key, DataType.Json);
        }

        public static RedisKey RedisProtobuf(this IEnumerable<string> key)
        {
            return new RedisKey(key, DataType.Protobuf);
        }
        public static RedisKey RedisString(this string key)
        {
            return new RedisKey(key, DataType.String);
        }
        public static RedisKey RedisJson(this string key)
        {
            return new RedisKey(key, DataType.Json);
        }

        public static RedisKey RedisProtobuf(this string key)
        {
            return new RedisKey(key, DataType.Protobuf);
        }
    }
}
