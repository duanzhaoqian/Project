using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dal;

namespace Bll
{
    public class S_LongHouse_TempService
    {
        private S_LongHouseTemp_Dal sLongHouseTempDal = new S_LongHouseTemp_Dal();

        public DataTable SelectData()
        {
            return sLongHouseTempDal.SelectData();
        }
    }
}
