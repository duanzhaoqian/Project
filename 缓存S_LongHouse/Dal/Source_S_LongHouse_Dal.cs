using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Command;

namespace Dal
{
    public class Source_S_LongHouse_Dal : DalBase
    {

        private string connectionString = Config.SourceLongHouse_ConnectionString;

        public DataTable GetDataTable(DateTime beginTime, DateTime endTime)
        {
            string selectSql =
                string.Format("select * from s_longhouse where updateTime between '{0}' and '{1}'",
                    beginTime.ToString(), endTime.ToString());
            return this.SelectData(selectSql, connectionString);
        }
    }
}
