using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Command;

namespace SQLHelp
{
    public class SQLHelp
    {
        /// <summary>
        /// SqlBulkCopy超时时间
        /// </summary>
        public static int SqlBulkCopyTimeOut
        {
            get
            {
                if (_sqlBulkCopyTimeOut == 0)
                {
                    SqlBulkCopyTimeOut = Convert.ToInt32(Config.SqlBulkCopyTimeOut);
                }
                return _sqlBulkCopyTimeOut;
            }
            set { _sqlBulkCopyTimeOut = value; }
        }
        /// <summary>
        /// SqlBulkCopy每批次条数
        /// </summary>
        public static int SqlBulkCopyBatchSize
        {
            get
            {
                if (_sqlBulkCopyBatchSize == 0)
                {
                    SqlBulkCopyBatchSize = Convert.ToInt32(Config.SqlBulkCopyBatchSize);
                }
                return _sqlBulkCopyBatchSize;
            }
            set { _sqlBulkCopyBatchSize = value; }
        }
        /// <summary>
        /// SqlDataAdapter超时时间
        /// </summary>
        public static int SqlDataAdapterTimeOut
        {
            get
            {
                if (_sqlDataAdapterTimeOut == 0)
                {
                    SqlDataAdapterTimeOut = Convert.ToInt32(Config.SqlDataAdapterTimeOut);
                }
                return _sqlDataAdapterTimeOut;
            }
            set { _sqlDataAdapterTimeOut = value; }
        }

        private static int _sqlDataAdapterTimeOut;
        private static int _sqlBulkCopyBatchSize;
        private static int _sqlBulkCopyTimeOut;
        /// <summary>
        /// SqlBulkCopy批量插入数据
        /// </summary>
        /// <param name="dt">要插入的DataTable</param>
        /// <param name="connectionString">连接字符串</param>
        public static void InsertData(DataTable dt, string connectionString)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    //设置目标表名
                    sqlBulkCopy.DestinationTableName = dt.TableName;
                    //设置超时时间
                    sqlBulkCopy.BulkCopyTimeout = SqlBulkCopyTimeOut;
                    //设置批次条数
                    sqlBulkCopy.BatchSize = SqlBulkCopyBatchSize;
                    //设置对应列名
                    DataColumnCollection dataColumnCollection = dt.Columns;
                    //sqlBulkCopy.ColumnMappings.Clear();
                    foreach (DataColumn column in dataColumnCollection)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
        }
        /// <summary>
        /// 批量更新数据，先删除再添加，使用了事务
        /// </summary>
        /// <param name="sql">执行删除的语句</param>
        /// <param name="dt">要插入的DataTable</param>
        /// <param name="connectionString">连接字符串</param>
        public static void UpdateData(string sql, DataTable dt, string connectionString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction))
                {
                    //设置目标表名
                    sqlBulkCopy.DestinationTableName = dt.TableName;
                    //设置超时时间
                    sqlBulkCopy.BulkCopyTimeout = SqlBulkCopyTimeOut;
                    //设置批次条数
                    sqlBulkCopy.BatchSize = SqlBulkCopyBatchSize;
                    //设置对应列名
                    DataColumnCollection dataColumnCollection = dt.Columns;
                    foreach (DataColumn column in dataColumnCollection)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }
                    try
                    {
                        DelDataByIds(sql, sqlConnection);
                        sqlBulkCopy.WriteToServer(dt);
                        sqlTransaction.Commit();
                    }
                    catch
                    {
                        sqlTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connectionString"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static int ExcuteNoQuerySql(string sql, string connectionString, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = sql;
                    if (sqlParameters != null)
                    {
                        sqlCommand.Parameters.AddRange(sqlParameters);
                    }
                    sqlConnection.Open();
                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 拼接Sql字符串，执行更新操作
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static void UpdateData(DataTable dataTable, string connectionString)
        {
            List<string> listSql = new List<string>();
            List<SqlParameter> listParameters = new List<SqlParameter>();
            List<string> listColumns = new List<string>();
            List<string> listColumnNames = new List<string>();
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                listColumnNames.Add(dataTable.Columns[i].ColumnName);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    listColumns.Add(dataTable.Columns[j].ColumnName + "=@" + dataTable.Columns[j].ColumnName + i);

                }
                listSql.Add("update " + dataTable.TableName + " set " + string.Join(",", listColumns) + " where " + dataTable.Columns[0].ColumnName + "=@" + dataTable.Columns[0].ColumnName + i);
                listColumns.Clear();
                foreach (string columnName in listColumnNames)
                {
                    listParameters.Add(new SqlParameter("@" + columnName + i, dataTable.Rows[i][columnName]));
                }
            }
            int size = 10;
            int pagecount = (int)Math.Ceiling(listSql.Count * 1.0 / size);
            for (int i = 0; i < pagecount; i++)
            {
                List<string> sql = listSql.Skip(i * size).Take(size).ToList();
                List<SqlParameter> parameters =
                    listParameters.Skip(i * size * listColumnNames.Count).Take(size * listColumnNames.Count).ToList();
                ExcuteNoQuerySql(string.Join(";", sql), connectionString, parameters.ToArray());
            }

        }
        private static int DelDataByIds(string sql, SqlConnection sqlConnection)
        {
            using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandText = sql;
                return sqlCommand.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">执行的sql语句</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>删除条数</returns>
        public static int ExcuteNoQuerySql(string sql, string connection)
        {
            return ExcuteNoQuerySql(sql, connection, null);
        }
        /// <summary>
        /// 批量查询数据
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="connection">连接字符串</param>
        /// <returns>查询到的DataTable</returns>
        public static DataTable SelectData(string sql, string connection)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandTimeout = SqlDataAdapterTimeOut;
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }
    }
}
