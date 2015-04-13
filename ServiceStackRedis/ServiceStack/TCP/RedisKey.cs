using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.TCP
{
    public class RedisKey
    {
        public RedisKey(string datakey, DataType dtype)
        {
            DataKey = datakey;
            DataType = dtype;
            mIsSingleKey = true;
        }

        private bool mIsSingleKey = true;

        public RedisKey(IEnumerable<string> datakey, DataType dtype)
        {
            DataKey = datakey;
            DataType = dtype;
            mIsSingleKey = false;
        }
        protected object DataKey
        {
            get;
            set;
        }

        private string[] GetKeys
        {
            get
            {
                return ((IEnumerable<string>)DataKey).ToArray();
            }
        }

        public void Delete(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            if (mIsSingleKey)
                db.Delete((string)DataKey);
            else
                db.Delete(GetKeys);
        }
        public int INCR(Enum Func, CityEnum City, double expireMinute = 0, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.INCR((string)DataKey, expireMinute);
        }
        public IList<string> Keys(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.Keys((string)DataKey);
        }

        protected DataType DataType
        {
            get;
            private set;
        }

        public T Get<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.Get<T>((string)DataKey, DataType);
        }

        public void Set(Enum Func, CityEnum City, object value, double expireTime = 0, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.Set((string)DataKey, value, DataType, expireTime);
        }

        public void SetValues(Enum Func, CityEnum City, object[] values, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            List<Field> kvs = new List<Field>();
            string[] keys = GetKeys;
            int count = keys.Length > values.Length ? values.Length : keys.Length;
            for (int i = 0; i < count; i++)
                kvs.Add(new Field { Value = values[i], Name = keys[i] });
            db.Set(kvs.ToArray(), DataType);
        }

        public IList<object> Get<T, T1>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.Get(new Type[] { typeof(T), typeof(T1) }, GetKeys, DataType);

        }

        public IList<object> Get<T, T1, T2>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.Get(new Type[] { typeof(T), typeof(T1), typeof(T2) }, GetKeys, DataType);
        }

        public IList<object> Get<T, T1, T2, T3>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.Get(new Type[] { typeof(T), typeof(T1), typeof(T2), typeof(T3) }, GetKeys, DataType);
        }

        public IList<object> Get<T, T1, T2, T3, T4>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.Get(new Type[] { typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, GetKeys, DataType);
        }


        public IList<object> Get<T>(Enum Func, CityEnum City, int GetCount, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            Type[] t = new Type[GetCount];
            for (int i = 0; i < GetCount; i++)
            {
                t[i] = typeof(T);
            }
            return db.Get(t, GetKeys, DataType);
        }



        #region lst
        public int LstCount(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.ListLength((string)DataKey);
        }

        public T LstPop<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.ListPop<T>((string)DataKey, DataType);
        }

        public T LstRemove<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.ListRemove<T>((string)DataKey, DataType);
        }

        public void LstAdd(Enum Func, CityEnum City, object value, double expireMinute = 0, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.ListAdd((string)DataKey, value, DataType, expireMinute);
        }

        public void LstPush(Enum Func, CityEnum City, object value, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.ListPush((string)DataKey, value, DataType);
        }

        public IList<T> LstRange<T>(Enum Func, CityEnum City, int start, int end, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.ListRange<T>((string)DataKey, start, end, DataType);
        }

        public IList<T> LstRange<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.ListRange<T>((string)DataKey, DataType);
        }

        public T LstGetItem<T>(Enum Func, CityEnum City, int index, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.GetListItem<T>((string)DataKey, index, DataType);
        }

        public void LstSetItem(Enum Func, CityEnum City, int index, object value, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.SetListItem((string)DataKey, index, value, DataType);
        }

        public void LstLREM<T>(Enum Func, CityEnum City, T value, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.ListLREM<T>((string)DataKey, value, -1, DataType);
        }
        #endregion

        #region sortedset

        public void SetZADD<T>(Enum Func, CityEnum City, T value, double Score, double expireMinute = 0, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.SetZADD<T>((string)DataKey, Score, value, DataType, expireMinute);
        }

        public void SetZREM<T>(Enum Func, CityEnum City, T value, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.SetZREM<T>((string)DataKey, value, DataType);
        }
        public IList<T> SetZRANGE<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.SetZRANGE<T>((string)DataKey, DataType);
        }
        public IDictionary<T, double> SetZRANGEWITHSCORE<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.SetZRANGEWITHSCORE<T>((string)DataKey, DataType);
        }
        public double SetZSCORE<T>(Enum Func, CityEnum City, T value, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.SetZSCORE<T>((string)DataKey, value, DataType);
        }
        public int SetCOUNT(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.SetCOUNT((string)DataKey, DataType);
        }
        public IDictionary<T, double> SetZREVRANGE<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.SetZREVRANGE<T>((string)DataKey, DataType);
        }
        #endregion

        #region field
        public IList<object> GetFields<T>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))}, DataType).FieldValueToList();
        }

        public IList<object> GetFields<T, T1>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T)),new NameType(typeof(T1))}, DataType).FieldValueToList();
        }

        public IList<object> GetFields<T, T1, T2>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))
            ,new NameType(typeof(T1))
            ,new NameType(typeof(T2))}, DataType).FieldValueToList();
        }

        public IList<object> GetFields<T, T1, T2, T3>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))
            ,new NameType(typeof(T1))
            ,new NameType(typeof(T2))
            ,new NameType(typeof(T3))}, DataType).FieldValueToList();
        }

        public IList<object> GetFields<T, T1, T2, T3, T4>(Enum Func, CityEnum City, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))
            ,new NameType(typeof(T1))
            ,new NameType(typeof(T2))
            ,new NameType(typeof(T3)),
            new NameType(typeof(T4))}, DataType).FieldValueToList();
        }

        public void SetField(string field, object value, RedisClient db = null)
        {
            SetField(new Field { Name = field, Value = value }, db);
        }

        public void SetField(Field field, RedisClient db = null)
        {
            SetField(new Field[] { field }, db);
        }

        public void SetField(params object[] value)
        {
            SetField(null, value);
        }
        public void SetField(RedisClient db, object[] value)
        {

            List<Field> fields = new List<Field>();
            foreach (object item in value)
            {
                fields.Add(new Field { Name = item.GetType().Name, Value = item });
            }
            SetField(fields.ToArray(), db);
        }

        public void SetField(IList<object> value, RedisClient db = null)
        {
            SetField(value.ToArray(), db);
        }

        public void SetField(Enum Func, CityEnum City, Field[] fields, RedisClient db = null)
        {
            db = db == null ? new RedisClient(Func, City) : db;
            db.SetFields((string)DataKey, fields, DataType);
        }
        #endregion

        #region 数据分发

        private static List<string> typeList
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("string");
                list.Add("int16");
                list.Add("int32");
                list.Add("int64");
                list.Add("boolean");
                list.Add("byte");
                list.Add("sbyte");
                list.Add("char");
                list.Add("decimal");
                list.Add("double");
                list.Add("float");
                list.Add("long");
                list.Add("ulong");
                list.Add("short");
                list.Add("ushort");
                return list;
            }
        }
        /// <summary>
        /// 类型处理
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static RedisKey GetType<T>(string key, DataType dataType = DataType.String)
        {
            string typeStr = typeof(T).Name.ToString().ToLower();
            if (typeList.Contains(typeStr) || dataType == DataType.String)
            {
                return key.RedisString();
            }
            if (dataType == DataType.Json)
            {
                return key.RedisJson();
            }
            return key.RedisProtobuf();
        }
        /// <summary>
        /// 类型处理
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static RedisKey GetType<T>(string[] keys, DataType dataType = DataType.String)
        {
            string typeStr = typeof(T).Name.ToString().ToLower();
            if (typeList.Contains(typeStr) || dataType == DataType.String)
            {
                return keys.RedisString();
            }
            if (dataType == DataType.Json)
            {
                return keys.RedisJson();
            }
            return keys.RedisProtobuf();
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
        #endregion

    }
}
