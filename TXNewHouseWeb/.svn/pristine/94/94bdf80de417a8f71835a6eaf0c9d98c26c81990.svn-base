using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace ImageTestDao.Base.Impl
{
    public sealed class DbConnectionManager : IDbConnectionManager
    {
        static readonly DbConnectionManager ConnectionManager = new DbConnectionManager();

        private static string _connString; //= DefaultConnString;
        const string DefaultConnString = "";//@"Data Source=192.168.3.156;Initial Catalog=kyj_HouseDB_5_4;User ID=sa;Password=110;";


        static DbConnectionManager()
        {
            var connSetting = ConfigurationManager.ConnectionStrings["kyj_NewHouseDBEntities"]; //@"Data Source=192.168.3.156;Initial Catalog=kyj_HouseDB_5_4;User ID=sa;Password=110;";
            if (connSetting == null)
            {
                return;
            }
            _connString = connSetting.ToString();
        }




        /// <summary>
        /// 初始化数据连接。本方法应在调用类库的宿主程序里调用。
        /// </summary>
        /// <param name="connString"></param>
        public static void Init(string connString)
        {
            _connString = string.IsNullOrEmpty(connString) ? DefaultConnString : connString;
        }

        public static DbConnectionManager Instance
        {
            get
            {
                return ConnectionManager;
            }
        }

        public SqlConnection GetConnection()
        {
            if (!HasConnection())
            {
                ContextConnection = new SqlConnection(_connString);
                if (ContextConnection.State == ConnectionState.Closed)
                {
                    ContextConnection.Open();
                }
            }

            return ContextConnection;
        }

        public void BeginTransaction()
        {
            if (ContextTransaction != null) return;
            ContextTransaction = GetConnection().BeginTransaction();
        }

        public void Commit()
        {
            if (HasOpenTransaction())
            {
                ContextTransaction.Commit();
                ContextTransaction.Dispose();
            }
            ContextTransaction = null;
        }

        public bool HasOpenTransaction()
        {
            return ContextTransaction != null;
        }

        public void Rollback()
        {
            if (HasOpenTransaction())
            {
                ContextTransaction.Rollback();
                ContextTransaction.Dispose();
            }
            ContextTransaction = null;
        }

        public bool HasConnection()
        {
            return ContextConnection != null;
        }

        public SqlTransaction ContextTransaction
        {
            get
            {
                if (IsInWebContext())
                {
                    return (SqlTransaction)HttpContext.Current.Items[TransactionKey];
                }
                return (SqlTransaction)CallContext.GetData(TransactionKey);
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[TransactionKey] = value;
                }
                else
                {
                    CallContext.SetData(TransactionKey, value);
                }
            }
        }

        public SqlConnection ContextConnection
        {
            get
            {
                if (IsInWebContext())
                {
                    return (SqlConnection)HttpContext.Current.Items[ConnectionKey];
                }
                return (SqlConnection)CallContext.GetData(ConnectionKey);
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[ConnectionKey] = value;
                }
                else
                {
                    CallContext.SetData(ConnectionKey, value);
                }
            }
        }

        private static bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }

        public void Close()
        {
            if (ContextConnection == null) return;
            ContextConnection.Dispose();
            ContextConnection = null;
        }

        private const string TransactionKey = "CONTEXT_TRANSACTION";
        private const string ConnectionKey = "CONTEXT_CONNECTION";

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        #endregion
    }
}
