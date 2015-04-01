using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dal;

namespace Bll
{
    public class Source_S_LongHouseService
    {
        private Source_S_LongHouse_Dal sLongHouseBaseDal = new Source_S_LongHouse_Dal();

        public List<T> GetList<T>(DateTime beginTime, DateTime endTime) where T : new()
        {
            DataTable dataTable = sLongHouseBaseDal.GetDataTable(beginTime, endTime);
            List<T> list = Command.DataConvert.DataTableToList<T>(dataTable);
            return list;
        }

        public DataTable GetDataTable(DateTime beginTime, DateTime endTime)
        {
            DataTable dataTable = sLongHouseBaseDal.GetDataTable(beginTime, endTime);
            return dataTable;
        }
    }
}
