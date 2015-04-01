using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TXGoodsDal.DB
{
    /// <summary>
    /// 查询数据库
    /// </summary>
    public partial class KYJ_ZushouRDB
    {
        private static ToolHelper bd = new ToolHelper(System.Configuration.ConfigurationManager.AppSettings["kyj.zushourdb"].ToString());

        public static DataTable GetTable(string sql)
        {
            return bd.GetTable(sql);
        }
        public static DataTable GetTable(string sql, SqlParam param)
        {
            return bd.GetTable(sql, param);
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
        /// <summary>
        /// 存储过程分页
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="where_">条件</param>
        /// <param name="OrderByColumn">排序条件</param>
        /// <param name="ColumnList">列名</param>
        /// <param name="TableList">表名</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="IsAccount">是否返回总条数、总页数</param>
        /// <param name="TotalRecord">总条数</param>
        /// <param name="TotalPage">总页数</param>
        /// <returns></returns>
        public static DataTable GetPages(int index, string where_, string OrderByColumn, string ColumnList, string TableList, int PageSize, bool IsAccount, out int TotalRecord, out int TotalPage)
        {
            TotalRecord = 0;
            TotalPage = 0;
            return bd.GetPages(index, where_, OrderByColumn, ColumnList, TableList, PageSize, IsAccount, out TotalRecord, out TotalPage);
        }


    }
}
