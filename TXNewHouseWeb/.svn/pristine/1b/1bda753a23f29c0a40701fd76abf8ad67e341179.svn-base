using System;
using System.Data;
using System.Data.SqlClient;

namespace ImageTestDao.Base.Impl
{
    public abstract class BaseDao
    {
        private readonly IDbConnectionManager _dbConnectionManager = DbConnectionManager.Instance;

        protected IDbConnectionManager ConnectionManager
        {
            get { return _dbConnectionManager; }
        }

        protected SqlConnection GetConnection()
        {
            if (_dbConnectionManager.HasConnection())
            {
                return _dbConnectionManager.ContextConnection;
            }
            return _dbConnectionManager.GetConnection();
        }

        protected SqlCommand GetCommand(string commandText)
        {
            return GetCommand(commandText, CommandType.Text);
        }

        protected SqlCommand GetCommand(string commandText, CommandType type)
        {
            var conn = GetConnection();
            var comm = conn.CreateCommand();
            if (_dbConnectionManager.HasOpenTransaction())
            {
                comm.Transaction = _dbConnectionManager.ContextTransaction;
            }
            comm.CommandText = commandText;
            comm.CommandType = type;
            if (comm.Connection.State == ConnectionState.Closed)
                comm.Connection.Open();
            return comm;
        }

        /// <summary>
        /// 执行指定的SQL语句。
        /// </summary>
        /// <param name="commandText">要执行SQL语句</param> 
        /// /// <returns>返回执行SQL语句后受到影响的记录</returns>
        protected int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, null);
        }

        /// <summary>
        /// 执行指定的SQL语句。
        /// </summary>
        /// <param name="commandText">要执行SQL语句</param>        
        /// <param name="parameters">指定参数</param>
        /// <returns>返回执行SQL语句后受到影响的记录</returns>
        protected int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, parameters);
        }

        /// <summary>
        /// 执行指定的SQL语句或存储过程。
        /// </summary>
        /// <param name="commandText">要执行SQL语句</param>
        /// <param name="commandType">指定要执行的SQL语句类型</param>
        /// <param name="parameters">指定参数</param>
        /// <returns>返回执行SQL语句后受到影响的记录</returns>
        protected int ExecuteNonQuery(string commandText, CommandType commandType,
            params SqlParameter[] parameters)
        {
            var comm = GetCommand(commandText, commandType);
            if (parameters != null && parameters.Length > 0)
            {
                comm.Parameters.AddRange(parameters);
            }
            try
            {
                return comm.ExecuteNonQuery();
            }
            catch 
            {
                throw;
            }
            finally
            {
                Close(comm);
            }
        }

        /// <summary>
        /// 执行指定的SQL语句。
        /// </summary>
        /// <param name="commandText">要执行SQL语句</param>
        /// <param name="needTransaction">是否在事务中执行</param>
        /// <param name="parameters">指定参数</param>
        /// <returns>返回执行SQL语句后受到影响的记录</returns>
        protected int ExecuteNonQuery(string commandText, bool needTransaction,
            params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, needTransaction, parameters);
        }

        /// <summary>
        /// 执行指定SQL语句或存储过程
        /// </summary>
        /// <param name="commandText">要执行的SQL语句或存储过程的名字</param>
        /// <param name="commandType">指定要执行的SQL语句类型</param>
        /// <param name="needTransaction">是否需要在事务中执行</param>
        /// <param name="parameters">指定参数</param>
        /// <returns>返回执行语句后受到影响的记录数量</returns>
        protected int ExecuteNonQuery(string commandText, CommandType commandType,
            bool needTransaction, params SqlParameter[] parameters)
        {
            if (needTransaction)
            {
                using (var conn = ConnectionManager)
                {
                    try
                    {
                        conn.BeginTransaction();
                        int affectedRows = ExecuteNonQuery(commandText, commandType, parameters);
                        conn.Commit();
                        return affectedRows;
                    }
                    catch
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
            return ExecuteNonQuery(commandText, commandType, parameters);
        }

        protected object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            var comm = GetCommand(commandText);

            if (parameters != null && parameters.Length > 0)
            {
                comm.Parameters.AddRange(parameters);
            }
            try
            {
                return comm.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                Close(comm);
            }
        }

        protected object ExecuteScalar(string commandText, bool needTransaction, params SqlParameter[] parameters)
        {
            if (needTransaction)
            {
                using (var conn = ConnectionManager)
                {
                    try
                    {
                        conn.BeginTransaction();
                        var obj = ExecuteScalar(commandText, parameters);
                        conn.Commit();
                        return obj;
                    }
                    catch
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
            return ExecuteScalar(commandText, parameters);
        }

        //执行不带参数的语句并返回DataTable
        protected DataTable ExecuteQuery(String commandText)
        {
            var table = new DataTable();
            var comm = GetCommand(commandText);
            try
            {
                using (var reader = comm.ExecuteReader(CommandBehavior.SingleResult))
                {
                    table.Load(reader, LoadOption.OverwriteChanges);
                }
                return table;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                Close(comm);
            }
        }

        protected DataTable ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            return ExecuteQuery(commandText, CommandType.Text, parameters);
        }

        protected DataTable ExecuteQuery(string commandText, bool needTransaction, params SqlParameter[] parameters)
        {
            return ExecuteQuery(commandText, CommandType.Text, true, parameters);
        }

        protected DataTable ExecuteQuery(string commandText, CommandType commandType, bool needTransaction, params SqlParameter[] parameters)
        {
            if (needTransaction)
            {
                using (var conn = ConnectionManager)
                {
                    try
                    {
                        conn.BeginTransaction();
                        var table = ExecuteQuery(commandText, commandType, parameters);
                        conn.Commit();
                        return table;
                    }
                    catch
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
            return ExecuteQuery(commandText, commandType, parameters);
        }

        protected DataTable ExecuteQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var table = new DataTable();
            var comm = GetCommand(commandText, commandType);

            if (parameters != null && parameters.Length > 0)
            {
                comm.Parameters.AddRange(parameters);
            }
            try
            {
                using (var reader = comm.ExecuteReader(CommandBehavior.SingleResult))
                {
                    table.Load(reader, LoadOption.OverwriteChanges);
                }
                return table;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                Close(comm);
            }
        }

        protected DataSet ExecuteQueryDataSet(string commandText, params SqlParameter[] parameters)
        {
            return ExecuteQueryDataSet(commandText, CommandType.Text, parameters);
        }

        protected DataSet ExecuteQueryDataSet(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var comm = GetCommand(commandText, commandType);
            if (parameters != null && parameters.Length > 0)
            {
                comm.Parameters.AddRange(parameters);
            }
            try
            {
                var adapter = new SqlDataAdapter(comm);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                Close(comm);
            }
        }

        protected void Close(SqlCommand comm)
        {
            if (comm == null) return;
            comm.Dispose();
            if (comm.Transaction == null)
            {
                _dbConnectionManager.Dispose();
            }
        }

    }
}
