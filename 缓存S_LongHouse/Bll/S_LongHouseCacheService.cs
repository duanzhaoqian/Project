using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Command;
using Dal;

namespace Bll
{
    public class S_LongHouseCacheService
    {
        Dal.S_LongHouseCache_Dal sLongHouseCacheDal = new S_LongHouseCache_Dal();


        public void InsertData(DataTable dt)
        {
            dt.TableName = Config.S_LongHouseCache_TableName;
            sLongHouseCacheDal.InsertData(dt);
        }
    }
}
