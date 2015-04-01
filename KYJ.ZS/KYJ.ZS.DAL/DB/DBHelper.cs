using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KYJ.ZS.Models.Pages;

namespace KYJ.ZS.DAL.DB
{
    /// <summary>
    /// 查询数据库
    /// </summary>
    public partial class KYJ_ZushouRDB
    {
        private static ToolHelper bd = new ToolHelper(System.Configuration.ConfigurationManager.ConnectionStrings["kyj.zushourdb"].ToString());

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
    /// <summary>
    /// 作者：maq
    /// 时间:2014年4月21日
    /// 描述:对GetPages方法进行封装
    /// </summary>1
    public partial class KYJ_ZushouRDB
    {
        public static PageData<T> GetPages<T>(PagePmsDal pagePmsDal) where T : new()
        {
            int pageCount;
            int recordCount;
            using (
                DataTable table = KYJ_ZushouRDB.GetPages(pagePmsDal.PageIndex, pagePmsDal.StrWhere, pagePmsDal.SortColnum,
                   pagePmsDal.ColList,pagePmsDal.TableList, pagePmsDal.PageSize, true,
                    out recordCount, out pageCount))
            {
                PageData<T> pageData = new PageData<T>(pageCount, recordCount, pagePmsDal.PageIndex, pagePmsDal.PageSize);
                pageData.DataList = DataHelper<T>.GetEntityList(table);
                return pageData;
            }
        }
    }


    /// <summary>
    /// 修改、添加数据库
    /// </summary>
    public class KYJ_ZushouWDB
    {
        private static ToolHelper bd = new ToolHelper(System.Configuration.ConfigurationManager.ConnectionStrings["kyj.zushouwdb"].ToString());

        public static DataTable GetTable(string sql)
        {
            return bd.GetTable(sql);
        }
        public static DataTable GetTable(string sql, SqlParam param)
        {
            return bd.GetTable(sql, param);
        }
        #region 作者：邓福伟 时间：2014-06-23
        public static DataSet GetDataSet(string sql)
        {
            return bd.GetDataSet(sql);
        }
        public static DataSet GetDataSet(string sql, SqlParam param)
        {
            return bd.GetDataSet(sql, param);
        }
        #endregion
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
        public static int SqlExecuteRuturnId(string sql)
        {
            return bd.SqlExecuteRuturnId(sql);
        }
        public static int SqlExecuteRuturnId(string sql, SqlParam param)
        {
            return bd.SqlExecuteRuturnId(sql, param);
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
        /// <returns></returns>
        public static PageInfo GetPages(int index, string where_, string OrderByColumn, string ColumnList, string TableList, int PageSize, bool IsAccounte)
        {
            int TotalRecord = 0;
            int TotalPage = 0;
            var _table = bd.GetPages(index, where_, OrderByColumn, ColumnList, TableList, PageSize, IsAccounte, out TotalRecord, out TotalPage);
            var pageInfo = new PageInfo(TotalRecord, index, _table, TotalPage);
            return pageInfo;
        }
    }



    /// <summary>
    /// 查询数据库(管理平台)
    /// </summary>
    public partial class KYJ_ZSPlatformRDB
    {
        private static ToolHelper bd = new ToolHelper(System.Configuration.ConfigurationManager.ConnectionStrings["kyj.zsplatformrdb"].ToString());

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
    /// <summary>
    /// 作者：maq
    /// 时间:2014年4月21日
    /// 描述:对GetPages方法进行封装
    /// </summary>1
    public partial class KYJ_ZSPlatformRDB
    {
        public static PageData<T> GetPages<T>(PagePmsDal pagePmsDal) where T : new()
        {
            int pageCount;
            int recordCount;
            using (
                DataTable table = KYJ_ZSPlatformRDB.GetPages(pagePmsDal.PageIndex, pagePmsDal.StrWhere, pagePmsDal.SortColnum,
                   pagePmsDal.ColList, pagePmsDal.TableList, pagePmsDal.PageSize, true,
                    out recordCount, out pageCount))
            {
                PageData<T> pageData = new PageData<T>(pageCount, recordCount, pagePmsDal.PageIndex, pagePmsDal.PageSize);
                pageData.DataList = DataHelper<T>.GetEntityList(table);
                return pageData;
            }
        }
    }

    /// <summary>
    /// 修改、添加数据库(管理平台)
    /// </summary>
    public class KYJ_ZSPlatformWDB
    {
        private static ToolHelper bd = new ToolHelper(System.Configuration.ConfigurationManager.ConnectionStrings["kyj.zsplatformwdb"].ToString());

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
        public static int SqlExecuteRuturnId(string sql)
        {
            return bd.SqlExecuteRuturnId(sql);
        }
        public static int SqlExecuteRuturnId(string sql, SqlParam param)
        {
            return bd.SqlExecuteRuturnId(sql, param);
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
        /// <returns></returns>
        public static PageInfo GetPages(int index, string where_, string OrderByColumn, string ColumnList, string TableList, int PageSize, bool IsAccounte)
        {
            int TotalRecord = 0;
            int TotalPage = 0;
            var _table = bd.GetPages(index, where_, OrderByColumn, ColumnList, TableList, PageSize, IsAccounte, out TotalRecord, out TotalPage);
            var pageInfo = new PageInfo(TotalRecord, index, _table, TotalPage);
            return pageInfo;
        }
    }
}
