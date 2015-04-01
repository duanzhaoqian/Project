/*
作者：窦海超
功能：连接数据库（从微动作粘过来的）
时间：2011-12-7
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Commons.DB
{
    public class AccessData
    {
        public string ConnectionSql;
        public AccessData(string constr)
        {
            ConnectionSql = constr;
        }
      
        #region 返回DataTable对象
        /// <summary>
        /// 传入SQL语句，返回DataTable对象
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql, SqlParam param)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return new DataTable();
            }
            SqlConnection cn = new SqlConnection(ConnectionSql);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (param != null)
                    foreach (SqlParameter p in param.Paramters)
                        cmd.Parameters.Add(p);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message + sql);
            }
            finally
            {
                cn.Close();
            }
        }
        public DataTable GetTable(string sql)
        {
            return GetTable(sql, null);
        }
        #endregion

        #region 执行SQL语句。
        /// <summary>
        /// 执行SQL语句。
        /// </summary>
        /// <param name="sql"></param>
        public int SqlExecute(string sql)
        {
            return SqlExecute(sql, null);
        }
        public int SqlExecute(string sql, SqlParam param)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return 0;
            }
            SqlConnection cn = new SqlConnection(ConnectionSql);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                if (param != null)
                    foreach (SqlParameter p in param.Paramters)
                        cmd.Parameters.Add(p);
                cmd.CommandTimeout = 300;
                return cmd.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message + sql);
            }
            finally
            {
                cn.Close();
            }
        }
        #endregion

        #region "取出需要查询的第一行第一列"
        /// <summary>
        /// 取出需要查询的第一行第一列
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public string GetFirst(string sql, SqlParam param)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return "";
            }
            SqlConnection myCn = new SqlConnection(ConnectionSql);
            string wt = "";
            try
            {
                myCn.Open();
                SqlCommand com = new SqlCommand(sql, myCn);
                if (param != null)
                    foreach (SqlParameter p in param.Paramters)
                        com.Parameters.Add(p);
                object rs = com.ExecuteScalar();
                if (rs != null)
                {
                    wt = rs.ToString();
                    myCn.Close();
                    return wt;
                }
                return wt;

            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message + sql);
            }
            finally
            {
                myCn.Close();
            }
        }
        public string GetFirst(string sql)
        {
            return GetFirst(sql, null);
        }
        #endregion
    }
}
