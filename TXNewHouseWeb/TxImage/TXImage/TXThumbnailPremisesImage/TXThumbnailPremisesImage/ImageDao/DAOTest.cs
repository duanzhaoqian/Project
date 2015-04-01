using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ImageTestDao.Base.Impl;

namespace ImageTestDao
{

    public class DaoTest : BaseDao
    {

        public List<Tuple<int,int,string>> SelectHouseIds()
        {
            const string sql = @"Select Id,CityId,InnerCode from S_LongHouse WHERE isdel=0 AND IsSVip=1 AND UserType=1 AND State=1";
            DataTable dt = ExecuteQuery(sql);
            return (from DataRow row in dt.Rows select Tuple.Create(Convert.ToInt32(row["ID"]), Convert.ToInt32(row["CityId"]), row["InnerCode"] as string)).ToList();
        }
    }
}
