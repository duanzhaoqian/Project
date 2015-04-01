using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Command;

namespace Dal
{
    public class S_LongHouse_Dal : DalBase
    {
        private string connectionString = Config.S_LongHouse_Cache_ConnectionString;

        public void InsertData(DataTable dataTable)
        {
            this.InsertData(dataTable, connectionString);
        }

        public void UpdateData(DataTable dataTable)
        {
            this.UpdateData(dataTable, connectionString);
        }

        public void RelDeleteData(List<int> listIds)
        {
            string sql = string.Format("delete from S_LongHouse where id in ({0})", string.Join(",", listIds));
            this.DeleteData(sql, connectionString);
        }

        public void LogcallyDeleteData(List<int> listDeleteIds)
        {
            string sql = string.Format("update S_LongHouse set isdel=1 where id in ({0})",
                string.Join(",", listDeleteIds));
            this.DeleteData(sql,connectionString);
        }
    }
}
