using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dal
{
    public abstract class DalBase
    {
        /// <summary>
        /// SqlBulkCopy批量插入数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="connectionString"></param>
        protected virtual void InsertData(DataTable dt, string connectionString)
        {
            SQLHelp.SQLHelp.InsertData(dt, connectionString);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="deleteSql"></param>
        /// <param name="connectionString"></param>
        protected virtual void DeleteData(string deleteSql, string connectionString)
        {
            SQLHelp.SQLHelp.ExcuteNoQuerySql(deleteSql, connectionString);
        }
        /// <summary>
        /// 先删除，再添加数据
        /// </summary>
        /// <param name="updateSql"></param>
        /// <param name="dt"></param>
        /// <param name="connectionString"></param>
        protected virtual void UpdateData(string updateSql, DataTable dt, string connectionString)
        {
            SQLHelp.SQLHelp.UpdateData(updateSql, dt, connectionString);
        }
        /// <summary>
        /// 批量查询数据
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        protected virtual DataTable SelectData(string selectSql, string connectionString)
        {
            return SQLHelp.SQLHelp.SelectData(selectSql, connectionString);
        }
        /// <summary>
        /// 拼接Sql更新数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        protected virtual void UpdateData(DataTable dt, string connectionString)
        {
             SQLHelp.SQLHelp.UpdateData(dt, connectionString);
        }
    }
}
