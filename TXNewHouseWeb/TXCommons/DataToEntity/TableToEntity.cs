using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data;

namespace TXCommons.DataToEntity
{
    public static class TableToEntity
    {
        public static List<T> GetTableEntity<T>(DataTable dt) where T : new()
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<T> listform = new List<T>();
                    T sf = new T();
                    //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        sf = new T();
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            //获取属性值需要指定属性名
                            PropertyInfo ProInfo = sf.GetType().GetProperty(dt.Columns[k].ColumnName);
                            if (ProInfo != null)
                                ProInfo.SetValue(sf, Convert.ToString(dr[k]), null);
                        }
                        listform.Add(sf);
                    }

                    return listform;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
