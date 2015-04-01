using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AdvertConfigure
{
    public class SearchServiceDal
    {
        string ConnectionString = "";
        public SearchServiceDal(string Connstring)
        {
            ConnectionString = Connstring;
        }
       

        public void  AddCityData(DataRow row)
        {
            string sql = " INSERT INTO AdvertConfigure " +
           " ([AreaId] " +
            "  ,[AreaPY]" +
          "  ,[AreaType] " +
           " ,[Pid] " +
           " ,[ShowCount_q] " +
         "   ,[AddCount_q] " +
          "  ,[AddPrice_q] " +
         "   ,[ShowCount_c] " +
         "   ,[AddCount_c] " +
         "   ,[AddPrice_c] " +
         "   ,[ShowCount_b] " +
         "   ,[AddCount_b] " +
         "   ,[AddPrice_b] " +
         "   ,[CreateTime] " +
         "   ,[UpdateTime] " +
         "   ,[AdminId] " +
         "   ,[IsDel]) " +
    "   values  ( "+
   row["id"].ToString() + ", "+
    "'" + TXCommons.Han2Ping.Convert(row["Name"].ToString().Replace("市", "")) + "', " +
   "   1, " +
    row["pid"].ToString() + ", " +
     "   '10-10-10-10-10', " +
  "    '10-10-10-10-10', " +
   "   '2-2-2-2-2', " +

   "    '25-25-25-25-25', " +
   "   '25-25-25-25-25', " +
   "   '1-1-1-1-1', " +

    "   '40-40-40-40-40', " +
   "   '240-240-240-240-240', " +
   "   '1-1-1-1-1', " +

   "   getdate(), " +
   "   getdate(), " +
   "   0, " +
   "   0 " +
  "    )";

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
            Console.WriteLine("city:"+ row["id"].ToString());
        }


        public void AddDistrictData(DataRow row)
        {
            string sql = " INSERT INTO AdvertConfigure " +
           " ([AreaId] " +
              "  ,[AreaPY]" +
          "  ,[AreaType] " +
           " ,[Pid] " +
           " ,[ShowCount_q] " +
         "   ,[AddCount_q] " +
          "  ,[AddPrice_q] " +
         "   ,[ShowCount_c] " +
         "   ,[AddCount_c] " +
         "   ,[AddPrice_c] " +
         "   ,[ShowCount_b] " +
         "   ,[AddCount_b] " +
         "   ,[AddPrice_b] " +
         "   ,[CreateTime] " +
         "   ,[UpdateTime] " +
         "   ,[AdminId] " +
         "   ,[IsDel]) " +

   "   values  ( " +
   row["id"].ToString() + ", " +
    "'" + TXCommons.Han2Ping.Convert(row["Name"].ToString()) + "', " +
   "   2, " +
     row["cityid"].ToString() + ", " +

     "   '10-10-10-10-10', " +
  "    '10-10-10-10-10', " +
   "   '2-2-2-2-2', " +

   "    '25-25-25-25-25', " +
   "   '25-25-25-25-25', " +
   "   '1-1-1-1-1', " +

    "   '40-40-40-40-40', " +
   "   '240-240-240-240-240', " +
   "   '1-1-1-1-1', " +

   "   getdate(), " +
   "   getdate(), " +
   "   0, " +
   "   0 " +
  "    )";

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
            Console.WriteLine("district:" + row["id"].ToString());
        }



        public void AddBusinessData(DataRow row)
        {
            string sql = " INSERT INTO AdvertConfigure " +
           " ([AreaId] " +
              "  ,[AreaPY]" +
          "  ,[AreaType] " +
           " ,[Pid] " +
           " ,[ShowCount_q] " +
         "   ,[AddCount_q] " +
          "  ,[AddPrice_q] " +
         "   ,[ShowCount_c] " +
         "   ,[AddCount_c] " +
         "   ,[AddPrice_c] " +
         "   ,[ShowCount_b] " +
         "   ,[AddCount_b] " +
         "   ,[AddPrice_b] " +
         "   ,[CreateTime] " +
         "   ,[UpdateTime] " +
         "   ,[AdminId] " +
         "   ,[IsDel]) " +

    "   values  ( " +
   row["id"].ToString() + ", " +
    "'"+TXCommons.Han2Ping.Convert(row["Name"].ToString()) + "', " +
   "   3, " +
  
     row["Did"].ToString() + ", " +
   "   '10-10-10-10-10', " +
  "    '10-10-10-10-10', " +
   "   '2-2-2-2-2', " +

   "    '25-25-25-25-25', " +
   "   '25-25-25-25-25', " +
   "   '1-1-1-1-1', " +

    "   '40-40-40-40-40', " +
   "   '240-240-240-240-240', " +
   "   '1-1-1-1-1', " +

   "   getdate(), " +
   "   getdate(), " +
   "   0, " +
   "   0 " +
  "    )";

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
            Console.WriteLine("business:" + row["id"].ToString());
        }


        public DataTable GetArea()
        {
            string sql = " select * from area where pid>1" ;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql).Tables[0];
        }

        public DataTable GetDistrict()
        {
            string sql = " select *  from District";

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql).Tables[0];
        }

        public DataTable GetBusiness()
        {
            string sql = " select * from Business" ;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql).Tables[0];
        }
    }

   
}
