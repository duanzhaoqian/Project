using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    public class ConvertToEntity
    {
        public static T GetEntityFromDataRow<T>(DataRow row) where T:new ()
        {
            T entity = default(T);
            if (entity == null) entity= Activator.CreateInstance<T>();
            foreach (PropertyInfo info in entity.GetType().GetProperties())
            {
                if (info.CanWrite && row.Table.Columns.Contains(info.Name))
                {
                    object obj = (row[info.Name] == DBNull.Value) ? null : row[info.Name];
                    info.SetValue(entity, obj, null);
                }
                //foreach (DataColumn column in row.Table.Columns)
                //{
                //    if (info.CanWrite&&column.ColumnName.ToLower() == info.Name.ToLower())
                //    {
                //        object obj = (row[info.Name] == DBNull.Value) ? null : row[info.Name];
                //        info.SetValue(entity,obj,null);
                //        break;
                //    }
                //}
            }
            return entity;
        }
        public static List<T> GetEntityListFromTable<T>(DataTable dt) where T : new()
        {
            List<T> listEntity = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                listEntity.Add(GetEntityFromDataRow<T>(row));
            }
            return listEntity;
        }
        #region DataRow     从数据库中安全获取数据，即当数据库中的数据为NULL时，保证读取不发生异常。

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为整数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static int GetIntValue(DataRow row, string colname)
        {
            int r;
            return int.TryParse(row[colname].ToString(), out r) ? r : Int32.MinValue;
        }
        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为长整数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回0</returns>
        public static long GetLongValue(DataRow row, string colname)
        {
            long r;
            return long.TryParse(row[colname].ToString(), out r) ? r : long.MinValue;
        }
        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为精度类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回 Decimal.MinValue</returns>
        public static decimal GetDecimalValue(DataRow row, string colname)
        {
            Decimal r;
            return Decimal.TryParse(row[colname].ToString(), out r) ? r : Decimal.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为布尔类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Int32.MinValue</returns>
        public static bool GetBoolenValue(DataRow row, string colname)
        {
            bool r;
            bool.TryParse(row[colname].ToString(), out r);
            return r;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为浮点数类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.Double.MinValue</returns>
        public static double GetDoubleValue(DataRow row, string colname)
        {
            Double r;
            return Double.TryParse(row[colname].ToString(), out r) ? r : Double.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为时间类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.DateTime.MinValue;</returns>
        public static DateTime GetDateTimeValue(DataRow row, string colname)
        {
            DateTime r;
            return DateTime.TryParse(row[colname].ToString(), out r) ? r : DateTime.MinValue;
        }

        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为字符串类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回System.String.Empty</returns>
        public static string GetValue(DataRow row, string colname)
        {
            return row[colname].ToString();
        }
        /// <summary>
        /// 从一个DataRow中，安全得到列colname中的值：值为 T 类型
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="colname">列名</param>
        /// <returns>如果值存在，返回；否则，返回 T 类型的默认值</returns>
        public static T GetValue<T>(DataRow row, string colname) where T : IConvertible
        {
            if (row[colname] != DBNull.Value)
                return (T)(Convert.ChangeType(row[colname], typeof(T)));
            return default(T);
        }

        #endregion DataRow
    }
}
