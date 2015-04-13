using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDal
{
    public class HouseDal
    {

        private string _sqlConnection;

        private string baseSql =
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "baseHouseSql.txt"));
        public HouseDal(string sqlConnection)
        {

            _sqlConnection = sqlConnection;
        }

        public DataTable GetDataTableByWhere(string where)
        {
            string sql = baseSql +" "+ where;
            return GetDataTableBySql(sql);
        }
        public DataTable GetDataTableBySql(string sql)
        {
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, _sqlConnection))
            {
                sqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetHouseModels(int beginid, int endid)
        {
            string sql = string.Format(baseSql + @" where  S_LongHouseBase.Id >= {0} AND dbo.S_LongHouseBase.Id<={1} ", beginid, endid);

            DataTable dataTable = GetDataTableBySql(sql);
            return dataTable;
        }
        public DataTable GetHouseModels(List<int> listids)
        {
            string sql = string.Format(baseSql + @" where  S_LongHouseBase.Id in ({0})", string.Join(",", listids));

            DataTable dataTable = GetDataTableBySql(sql);
            return dataTable;
        }
    }
}
