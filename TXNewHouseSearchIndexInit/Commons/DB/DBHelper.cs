using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Commons.DB
{
    public class NewHouseDB
    {
        private static AccessData bd = new AccessData(System.Configuration.ConfigurationManager.AppSettings["NewHouseDB"].ToString());
        public static DataTable GetTable(string sql)
        {
            return bd.GetTable(sql);
        }
        public static DataTable GetTable(string sql, SqlParam param)
        {
            return bd.GetTable(sql, param);
        }
        public static DataTable GetTable(string sql, SqlParam param, string constr)
        {
            AccessData bd1 = new AccessData(constr);
            return bd1.GetTable(sql);
        }
        public static string GetFirst(string sql)
        {
            return bd.GetFirst(sql);
        }
        public static string GetFirst(string sql, SqlParam param)
        {
            return bd.GetFirst(sql, param);
        }
        public static int SqlExecute(string sql)
        {
            return bd.SqlExecute(sql);
        }
        public static int SqlExecute(string sql, SqlParam param)
        {
            return bd.SqlExecute(sql, param);
        }
    }
    public class WebDB
    {
        private static AccessData bd = new AccessData(System.Configuration.ConfigurationManager.AppSettings["WebDB"].ToString());
        public static DataTable GetTable(string sql)
        {
            return bd.GetTable(sql);
        }
        public static DataTable GetTable(string sql, SqlParam param)
        {
            return bd.GetTable(sql, param);
        }
        public static DataTable GetTable(string sql, SqlParam param, string constr)
        {
            AccessData bd1 = new AccessData(constr);
            return bd1.GetTable(sql);
        }
        public static string GetFirst(string sql)
        {
            return bd.GetFirst(sql);
        }
        public static string GetFirst(string sql, SqlParam param)
        {
            return bd.GetFirst(sql, param);
        }
        public static int SqlExecute(string sql)
        {
            return bd.SqlExecute(sql);
        }
        public static int SqlExecute(string sql, SqlParam param)
        {
            return bd.SqlExecute(sql, param);
        }
    }
}
