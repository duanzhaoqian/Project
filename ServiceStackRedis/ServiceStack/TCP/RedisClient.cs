using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.TCP
{
    public class RedisClient
    {
        private RedisHost redisHost = null;

        //RedisClient() { mDefaultDB = new RedisClient(FunctionTypeEnum.WebDBData, CityEnum.BeiJing); }

        public RedisClient(Enum FunctionTypeEnum, CityEnum CityEnum)
        {
            redisHost = new InitHelper().Distribution(FunctionTypeEnum, CityEnum);
        }

        //public RedisClient GetClient(Enum FunctionTypeEnum, CityEnum CityEnum, RedisClient db)
        //{
        //    if (db == null)
        //        return new RedisClient(FunctionTypeEnum, CityEnum);
        //    return db;
        //}
        //private static RedisClient mDefaultDB;

        //public static RedisClient DefaultDB
        //{
        //    get
        //    {
        //        return new RedisClient(FunctionTypeEnum.WebDBData, CityEnum.BeiJing);
        //    }
        //}

        private int mReadHostIndex = 0;

        private int mWriteHostIndex = 0;

        public RedisHost.ClientItem GetClient()
        {
            RedisHost.ClientItem client;
            if (redisHost.Available)
            {
                redisHost.Push(redisHost.CreateClient());
                client = redisHost.Pop();
                SelectDB(client.Client);
                return client;
            }
            else
            {
                redisHost.Detect();
            }
            mWriteHostIndex++;
            throw new Exception("write host not Available!");
        }
        /*
        public RedisHost.ClientItem GetReader()
        {
            RedisHost.ClientItem client;
            if (redisHost.Available)
            {
                //client = redisHost.Pop();
                //SelectDB(client.Client);
                //return client;
                redisHost.Push(redisHost.CreateClient());
                client = redisHost.Pop();
                SelectDB(client.Client);
                return client;
            }
            else
            {
                redisHost.Detect();
            }
            throw new Exception("read host not Available!");
        }
        */
        private void SelectDB(TcpClient client)
        {
            if (client.DB != DB)
            {
                client.DB = DB;
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_SELECT);
                    cmd.Add(DB.ToString());
                    using (Result result = TcpClient.Send(cmd, client))
                    {
                    }
                }
            }
        }

        public int DB
        {
            get;
            set;
        }
        public int INCR(string key, double expireMinute = 0)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_INCR);
                    cmd.Add(key);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        ExpireTimeMethod(key, expireMinute, c.Client);
                        return Convert.ToInt32(result.ResultData.ToString().Replace("\r\n", "").ToString());
                    }
                }
            }
        }
        public int Delete(params string[] keys)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_DEL);
                    foreach (string key in keys)
                    {
                        cmd.Add(key);
                    }
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        return int.Parse(result.ResultData.ToString());
                    }
                }
            }
        }

        public void Delete(IList<string> keys)
        {
            Delete(keys.ToArray());
        }

        public IList<string> Keys(string match)
        {
            IList<string> r = null;
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_KEYS);

                    cmd.Add(match);

                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        r = new List<string>(result.ResultDataBlock.Count);
                        foreach (ArraySegment<byte> i in result.ResultDataBlock)
                        {
                            r.Add(i.GetString());
                        }
                    }
                }
            }
            return r;
        }

        public int ListLength(string key)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_LLEN);
                    cmd.Add(key);

                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        return int.Parse(result.ResultData.ToString());
                    }
                }
            }
        }

        public T ListPop<T>(string key, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_LPOP);
                    cmd.Add(key);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        return (T)FromRedis(result.ResultDataBlock[0], dtype, typeof(T));
                    }
                }
            }
        }

        public T ListRemove<T>(string key, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_RPOP);
                    cmd.Add(key);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        return (T)FromRedis(result.ResultDataBlock[0], dtype, typeof(T));
                    }
                }
            }
        }

        public void ListPush(string key, object value, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_LPUSH);
                    cmd.Add(key);
                    ToRedis(value, dtype, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {

                    }
                }
            }
        }

        public void SetListItem(string key, int index, object value, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_LSET);
                    cmd.Add(key);
                    cmd.Add(index.ToString());
                    ToRedis(value, dtype, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {

                    }
                }
            }
        }

        public void ListAdd(string key, object value, DataType dtype, double expireMinute = 0)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_RPUSH);
                    cmd.Add(key);
                    ToRedis(value, dtype, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        ExpireTimeMethod(key, expireMinute, c.Client);
                    }
                }
            }
        }

        public IList<T> ListRange<T>(string key, int start, int end, DataType dtype)
        {
            IList<T> lst = null;
            using (RedisHost.ClientItem c = GetClient())
            {
                lst = new List<T>();
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_LRANGE);
                    cmd.Add(key);
                    cmd.Add(start.ToString());
                    cmd.Add(end.ToString());
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        foreach (ArraySegment<byte> item in result.ResultDataBlock)
                        {
                            lst.Add((T)FromRedis(item, dtype, typeof(T)));
                        }
                    }
                }
                return lst;
            }
        }

        public bool ListLREM<T>(string key, T value, int count, DataType dtype)
        {
            IList<T> lst = null;
            using (RedisHost.ClientItem c = GetClient())
            {
                lst = new List<T>();
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_LREM);
                    cmd.Add(key);
                    cmd.Add(count.ToString());
                    ToRedis(value, dtype, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                    }
                }
                return true;
            }
        }


        public IList<T> ListRange<T>(string key, DataType dtype)
        {
            return ListRange<T>(key, 0, -1, dtype);
        }

        public T GetListItem<T>(string key, int index, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_LINDEX);
                    cmd.Add(key);
                    cmd.Add(index.ToString());
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        return (T)FromRedis(result.ResultDataBlock[0], dtype, typeof(T));
                    }
                }
            }
        }

        public void SetZADD<T>(string key, double score, T value, DataType dtype, double expireMinute = 0)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_ZADD);
                    cmd.Add(key);
                    cmd.Add(score.ToString());
                    ToRedis(value, dtype, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        ExpireTimeMethod(key, expireMinute, c.Client);
                    }
                }
            }
        }


        public bool SetZREM<T>(string key, T value, DataType dtype)
        {
            IList<T> lst = null;
            using (RedisHost.ClientItem c = GetClient())
            {
                lst = new List<T>();
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_ZREM);
                    cmd.Add(key);
                    cmd.Add("0");
                    cmd.Add("-1");
                    cmd.Add("WITHSCORES");
                    ToRedis(value, dtype, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                    }
                }
                return true;
            }
        }


        public IList<T> SetZRANGE<T>(string key, DataType dtype)
        {
            IList<T> lst = null;
            using (RedisHost.ClientItem c = GetClient())
            {
                lst = new List<T>();
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_ZRANGE);
                    cmd.Add(key);
                    cmd.Add("0");
                    cmd.Add("-1");
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        IList<ArraySegment<byte>> listvalue = result.ResultDataBlock;
                        if (listvalue == null)
                        {
                            return default(IList<T>);
                        }
                        foreach (ArraySegment<byte> item in result.ResultDataBlock)
                        {
                            lst.Add((T)FromRedis(item, dtype, typeof(T)));
                        }
                    }
                }
                return lst;
            }
        }

        public IDictionary<T, double> SetZRANGEWITHSCORE<T>(string key, DataType dtype)
        {
            IDictionary<T, double> lst = new Dictionary<T, double>(); ;
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_ZRANGE);
                    cmd.Add(key);
                    cmd.Add("0");
                    cmd.Add("-1");
                    cmd.Add("WITHSCORES");
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        for (int i = 0, t = result.ResultDataBlock.Count; i < t; i++)
                        {
                            ArraySegment<byte> mbyte = result.ResultDataBlock[i];
                            T entity = (T)FromRedis(mbyte, dtype, typeof(T));
                            i++;
                            ArraySegment<byte> score = result.ResultDataBlock[i];
                            string doublescore = (string)FromRedis(score, DataType.String, typeof(string));
                            lst.Add(entity, Convert.ToDouble(doublescore));
                        }
                    }
                }
                return lst;
            }
        }

        public double SetZSCORE<T>(string key, T value, DataType dtype)
        {
            double score = 0;
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_ZSCORE);
                    cmd.Add(key);
                    ToRedis(value, dtype, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        foreach (ArraySegment<byte> item in result.ResultDataBlock)
                        {
                            return Convert.ToDouble((string)FromRedis(item, dtype, typeof(string)));
                        }
                    }
                }
                return score;
            }
        }
        public int SetCOUNT(string key, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_ZCOUNT);
                    cmd.Add(key);
                    cmd.Add("-1000000");
                    cmd.Add("1000000");
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        return Convert.ToInt32(result.ResultData.ToString().Replace("\r\n", ""));
                    }
                }
            }
        }
        public IDictionary<T, double> SetZREVRANGE<T>(string key, DataType dtype)
        {
            IDictionary<T, double> lst = new Dictionary<T, double>(); ;
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_ZREVRANGE);
                    cmd.Add(key);
                    cmd.Add("0");
                    cmd.Add("-1");
                    cmd.Add("WITHSCORES");
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        for (int i = 0, t = result.ResultDataBlock.Count; i < t; i++)
                        {
                            ArraySegment<byte> mbyte = result.ResultDataBlock[i];
                            T entity = (T)FromRedis(mbyte, dtype, typeof(T));
                            i++;
                            ArraySegment<byte> score = result.ResultDataBlock[i];
                            string doublescore = (string)FromRedis(score, DataType.String, typeof(string));
                            lst.Add(entity, Convert.ToDouble(doublescore));
                        }
                    }
                }
                return lst;
            }
        }
        public IList<Field> GetFields(string key, params string[] fields)
        {
            List<NameType> nts = new List<NameType>();
            foreach (string field in fields)
            {
                nts.Add(new NameType(typeof(String), field, 0));
            }
            return GetFields(key, nts.ToArray(), DataType.String);
        }

        public IList<Field> GetFields(string key, NameType[] fields, DataType type)
        {
            List<Field> result = GetResultSpace<Field>(fields.Length);
            object value;
            NameType item;
            using (RedisHost.ClientItem c = GetClient())
            {
                List<NameType> inputs = new List<NameType>();
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_HMGET);
                    cmd.Add(key);
                    for (int i = 0; i < fields.Length; i++)
                    {
                        item = fields[i];
                        inputs.Add(item);
                        item.Index = i;
                        cmd.Add(item.Name);
                    }
                    using (Result R = TcpClient.Send(cmd, c.Client))
                    {
                        for (int i = 0; i < inputs.Count; i++)
                        {
                            item = inputs[i];

                            value = FromRedis(R.ResultDataBlock[i], type, item.Type);
                            result[item.Index] = new Field { Name = item.Name, Value = value };
                        }
                    }
                }
            }
            return result;
        }

        //public IList<object> Get<T, T1>(string key, string key1, DataType type)
        //{
        //    return Get(new Type[] { typeof(T), typeof(T1) }, new string[] { key, key1 }, type);
        //}

        //public IList<object> Get<T, T1, T2>(string key, string key1, string key2, DataType type)
        //{
        //    return Get(new Type[] { typeof(T), typeof(T1), typeof(T2) }, new string[] { key, key1, key2 }, type);
        //}

        //public IList<object> Get<T, T1, T2, T3>(string key, string key1, string key2, string key3, DataType type)
        //{
        //    return Get(new Type[] { typeof(T), typeof(T1), typeof(T2), typeof(T3) }, new string[] { key, key1, key2, key3 }, type);
        //}

        //public IList<object> Get<T, T1, T2, T3, T4>(string key, string key1, string key2, string key3, string key4, DataType type)
        //{
        //    return Get(new Type[] { typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, new string[] { key, key1, key2, key3, key4 }, type);
        //}

        public IList<object> Get(Type[] types, string[] keys, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                List<object> result = GetResultSpace<object>(keys.Length);
                List<NameType> _types = new List<NameType>();
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_MGET);
                    for (int i = 0; i < keys.Length; i++)
                    {
                        string key = keys[i];
                        cmd.Add(key);
                        _types.Add(new NameType(types[i], keys[i], i));
                    }
                    using (Result r = TcpClient.Send(cmd, c.Client))
                    {
                        for (int i = 0; i < _types.Count; i++)
                        {
                            object item = FromRedis(r.ResultDataBlock[i], dtype, _types[i].Type);
                            result[_types[i].Index] = item;
                        }
                    }
                }
                return result;
            }
        }

        private List<T> GetResultSpace<T>(int count)
        {
            List<T> result = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                result.Add(default(T));
            }
            return result;
        }

        public string Get(string key)
        {
            return Get<string>(key, DataType.String);
        }

        public T Get<T>(string key, DataType type)
        {
            T value = default(T);
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_GET);
                    cmd.Add(key);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        if (result.ResultDataBlock.Count > 0)
                        {
                            value = (T)FromRedis(result.ResultDataBlock[0], type, typeof(T));
                            return value;
                        }
                    }

                }

                return default(T);
            }
        }

        public void SetFields(string key, IEnumerable<Field> items, DataType type)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_HMSET);
                    cmd.Add(key);
                    foreach (Field item in items)
                    {
                        cmd.Add(item.Name);
                        ToRedis(item.Value, type, cmd);
                    }
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {

                    }
                }
            }
        }

        public void Set(Field[] KValues, DataType dtype)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_MSET);
                    foreach (Field item in KValues)
                    {
                        cmd.Add(item.Name);
                        ToRedis(item.Value, dtype, cmd);
                    }
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                    }
                }
            }
        }

        public void Set(string key, object value, DataType type, double expireTimeMinuse = 0)
        {
            using (RedisHost.ClientItem c = GetClient())
            {
                using (Command cmd = new Command())
                {
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_SET);
                    cmd.Add(key);
                    ToRedis(value, type, cmd);
                    using (Result result = TcpClient.Send(cmd, c.Client))
                    {
                        ExpireTimeMethod(key, expireTimeMinuse, c.Client);
                    }
                }
            }


        }

        private void ExpireTimeMethod(string key, double expireTimeMinuse, TcpClient client)
        {
            if (expireTimeMinuse > 0)
            {
                //using (RedisHost.ClientItem c = GetWriter())
                //{
                using (Command cmd = new Command())
                {
                    string time = (expireTimeMinuse * 60).ToString();
                    cmd.Add(CONST_VALURES.REDIS_COMMAND_EXPIRE);
                    cmd.Add(key);
                    cmd.Add(time);
                    using (Result result = TcpClient.Send(cmd, client))
                    {
                    }
                }
                //}
            }
        }

        private void ToRedis(object value, DataType type, Command cmd)
        {
            if (type == DataType.String)
            {
                cmd.Add((string)value);
            }
            else if (type == DataType.Json)
            {
                cmd.AddJson(value);
            }
            else
            {
                cmd.AddProtobuf(value);
            }
        }

        private object FromRedis(ArraySegment<byte> data, DataType type, Type otype)
        {
            if (type == DataType.String)
            {
                return data.GetString();
            }
            else if (type == DataType.Json)
            {
                return data.GetJson(otype);
            }
            else
            {
                return data.GetProtobuf(otype);
            }
        }
    }
}
