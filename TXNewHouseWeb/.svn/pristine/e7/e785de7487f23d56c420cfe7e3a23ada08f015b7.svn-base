using System;
using System.Data.SqlClient;

namespace ImageTestDao.Base
{
    public interface IDbConnectionManager : IDisposable
    {
        /// <summary>
        /// 获得一个SQL Server数据库的连接。
        /// </summary>
        /// <returns>一个SqlConnection对象。</returns>
        SqlConnection GetConnection();

        /// <summary>
        /// 开启事务。
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务。
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务。
        /// </summary>
        void Rollback();

        /// <summary>
        /// 关闭连接。
        /// </summary>
        void Close();

        bool HasOpenTransaction();

        bool HasConnection();
        
        SqlTransaction ContextTransaction { get; set; }
        
        SqlConnection ContextConnection { get; set; }
    }
}
