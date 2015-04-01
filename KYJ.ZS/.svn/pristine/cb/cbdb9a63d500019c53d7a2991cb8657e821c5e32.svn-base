using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.DB
{
    using System.ComponentModel;

    /// <summary>
    /// 对数据库的一些包装
    /// </summary>
    public class ConvertDB
    {  /// <summary>
        /// 作者：douhaichao
        /// 时间：2010-12-20
        /// 功能：在DataSet 提取 DataTable
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static DataTable GetTable(DataSet ds)
        {
            DataTable dt = new DataTable();
            if (!Auxiliary.CheckDs(ds))
                return dt;
            if (ds.Tables.Count <= 0)
                return dt;
            if (ds.Tables[0] == null)
                return dt;
            if (ds.Tables[0].Rows.Count <= 0)
                return dt;
            if (ds.Tables[0].Rows[0] == null)
                return dt;
            return ds.Tables[0];
        }


        /// <summary>
        /// 作者：douhaichao
        /// 时间：2010-12-21
        /// 功能：在DataSet 提取 DataRow
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static DataRow GetRow(DataSet ds)
        {
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            dt = GetTable(ds);
            if (!Auxiliary.CheckDt(dt))
                return dr;
            if (dt.Rows.Count <= 0 || dt.Rows[0] == null)
                return dr;
            dr = dt.Rows[0];
            return dr;

        }

        /// <summary>
        /// 作者：douhaichao
        /// 时间：2010-12-20
        /// 功能：根据Object获取string
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static string GetString(Object ob)
        {
            string str = string.Empty;
            if (ob == null)
                return str;
            if (String.IsNullOrEmpty(Convert.ToString(ob)))
                return str;
            if (String.IsNullOrEmpty(Convert.ToString(ob).Trim()))
                return str;
            str = Convert.ToString(ob).Trim();
            return str;
        }

        /// <summary>
        /// 作者：douhaichao
        /// 时间：2010-12-20
        /// 功能：根据Object获取Int
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int GetInt(Object ob)
        {
            return Auxiliary.ToInt32(ob, 0);
        }
        public static DataSet GetSet(DataTable dt)
        {
            DataSet set = new DataSet();
            set.Tables.Add(dt);
            return set;
        }
        //作者：孟国栋
        //时间：2014-05-19
        //描述：将Datatable中的数据转换为字符串集合
        /// <summary>
        /// 从DataTable中获得字符串集合
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<string> GetStringList(DataTable table)
        {
            List<string> list = new List<string>();
            string tempStr = string.Empty;
            if (table != null)
            {
                foreach (DataRow Rows in table.Rows)
                {
                    tempStr = Rows[0].ToString();
                    list.Add(tempStr);
                }
            }
            return list;
        }
    }


    /// <summary>
    /// 数据集合转换为实体集合工具类
    /// 时间：2014年4月16日
    /// 作者：马谦
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class DataHelper<T> where T : new()
    {
        /// <summary>
        /// 从DataRow中获取实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T GetEntity(DataRow row)
        {
            T t = new T();
            if (row == null)
            {
                throw new Exception("参数DataRow不能为null");
            }
            else
            {
                Type type = typeof(T);
                PropertyInfo[] propertyInfos = type.GetProperties();
                foreach (PropertyInfo info in propertyInfos)
                {
                    if (row.Table.Columns.Contains(info.Name))
                    {
                        if (row[info.Name] != null && row[info.Name].ToString().Length > 0)
                        {
                            try
                            {
                                info.SetValue(t, ChangeType(row[info.Name], info.PropertyType), null);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// 扩展ChangeType方法，解决不能转换null类型问题
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value != null)
                {
                    var nullableConverter = new NullableConverter(conversionType);
                    conversionType = nullableConverter.UnderlyingType;
                }
                else
                {
                    return null;
                }
            }

            return Convert.ChangeType(value, conversionType);
        }
        /// <summary>
        /// 从DataTable中获取实体集合
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> GetEntityList(DataTable table)
        {
            List<T> list = new List<T>();
            if (table == null)
            {
                throw new Exception("DataTable参数不能为null");
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    list.Add(GetEntity(row));
                }
            }
            return list;
        }
      
    }
}

