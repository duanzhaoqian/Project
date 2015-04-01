using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using KYJ.ZS.Commons;

namespace TXGoodsDal.DB
{
    public class ToolHelper
    {
        public string constr1;
        public ToolHelper(string constr)
        {
            constr1 = constr;
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
            SqlConnection cn = new SqlConnection(constr1);
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
            SqlConnection cn = new SqlConnection(constr1);
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
            SqlConnection myCn = new SqlConnection(constr1);

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

        #region 存储过程
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(constr1))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }
        #endregion

        #region 存储过程分页

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataTable GetPages(int index, string where_, string OrderByColumn, string ColumnList, string TableList, int PageSize, bool IsAccount, out int TotalRecord, out int TotalPage)
        {
            DataSet set = new DataSet();
            SqlParameter[] parm = { 
                                  new SqlParameter("@OrderByColumn",SqlDbType.NVarChar,500),
                                  new SqlParameter("@ColumnList",SqlDbType.NVarChar,500),
                                  new SqlParameter("@TableList",SqlDbType.NVarChar,1000),
                                  new SqlParameter("@Condition",SqlDbType.NVarChar,1000),
                                  new SqlParameter("@PageSize",SqlDbType.Int,4),
                                  new SqlParameter("@CurrentPage",SqlDbType.Int,4),
                                  new SqlParameter("@IsAccount",SqlDbType.Bit,1),
                                  new SqlParameter("@TotalRecord",SqlDbType.Int),
                                  new SqlParameter("@TotalPage",SqlDbType.Int)
                                  };
            parm[0].Value = OrderByColumn;
            parm[1].Value = ColumnList;
            parm[2].Value = TableList;
            parm[3].Value = where_;
            parm[4].Value = PageSize;
            parm[5].Value = index;
            parm[6].Value = IsAccount;
            parm[7].Direction = ParameterDirection.Output;
            parm[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteDataset(constr1, set, CommandType.StoredProcedure, "Sp_CustomPage2005_V1_2", parm);
            TotalRecord = 0;
            TotalPage = 0;
            if (Convert.ToBoolean(Auxiliary.CheckDbObject(parm[7].Value, 0)) && Convert.ToBoolean(Auxiliary.CheckDbObject(parm[8].Value, 1)))
            {
                TotalRecord = Convert.ToInt32(parm[7].Value);
                TotalPage = Convert.ToInt32(parm[8].Value);
            }
            return ConvertDB.GetTable(set);
        }
        #endregion

    }

}
