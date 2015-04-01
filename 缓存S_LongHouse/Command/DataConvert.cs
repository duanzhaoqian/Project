using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Command
{
    public class DataConvert
    {
        public static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> list = new List<T>();
            DataColumnCollection dataColumnCollection = dt.Columns;
            DataRowCollection dataRowCollection = dt.Rows;
            foreach (DataRow dataRow in dataRowCollection)
            {
                Type type = typeof(T);
                PropertyInfo[] propertyInfos = type.GetProperties();
                T t = new T();
                foreach (PropertyInfo info in propertyInfos)
                {
                    object obj = dataRow[info.Name];
                    if (obj==DBNull.Value)
                    {
                        obj = null;
                    }
                    info.SetValue(t, obj, null);
                }
                list.Add(t);
            }
            return list;
        }

        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dataTable = new DataTable();
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo info in propertyInfos)
            {
                dataTable.Columns.Add(info.Name);
            }
            foreach (T item in list)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (PropertyInfo info in propertyInfos)
                {
                    object obj = info.GetValue(item, null);
                    if (info.PropertyType == typeof(DateTime) && Convert.ToDateTime(obj) == default(DateTime))
                    {
                        obj = null;
                    }

                    dataRow[info.Name] = obj;
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
    }
}
