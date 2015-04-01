using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Command;

namespace Dal
{
    public class S_LongHouseCache_Dal : DalBase
    {
        private string connectionString = Config.S_LongHouse_Cache_ConnectionString;

        public void UpdateData(DataTable dt)
        {
            this.UpdateData(dt, connectionString);
        }
        /// <summary>
        /// S_LongHouseCache表，插入前先清空
        /// </summary>
        /// <param name="dt"></param>
        public void InsertData(DataTable dt)
        {
            string truncateTableSql = "truncate table s_longhouseCache";
            this.DeleteData(truncateTableSql, connectionString);
            this.InsertData(dt, connectionString);
        }
    }
}
