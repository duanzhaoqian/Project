using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TXCommons.DBUtility;
using TXCommons.Mongo;

namespace MongoProcess
{
    public class Mongo_HouseState_Dal
    {
        internal List<Mongo_HouseStateModel_Wrapper> GetHouseStateModels(int startId, int endId)
        {
            List<Mongo_HouseStateModel_Wrapper> list = new List<Mongo_HouseStateModel_Wrapper>();
            string sql = string.Format(@"SELECT 
      [HouseId]
      ,[Rtype]
      ,[Createtime]
      ,[Remark]
      ,[AdminName]
  FROM [dbo].[HouseState]
where id >={0} and id <={1}
", startId, endId);

            DataTable dataTable = KYJHouseReadDB.GetTable(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                Mongo_HouseStateModel_Wrapper mongoHouseStateModelWrapper = new Mongo_HouseStateModel_Wrapper();
                mongoHouseStateModelWrapper.Id = Convert.ToInt32(row["HouseId"]);
                mongoHouseStateModelWrapper.MongoHouseStateModel.Createtime = Convert.ToDateTime(row["Createtime"]);
                mongoHouseStateModelWrapper.MongoHouseStateModel.Rtype = row["Rtype"].ToString();
                mongoHouseStateModelWrapper.MongoHouseStateModel.Remark = row["Remark"].ToString();
                mongoHouseStateModelWrapper.MongoHouseStateModel.AdminName = row["AdminName"].ToString();
                list.Add(mongoHouseStateModelWrapper);
            }

            return list;
        }

        internal List<Mongo_HouseStateModel_Wrapper> GetHouseStateModels(List<int> listidsNew)
        {
            List<Mongo_HouseStateModel_Wrapper> list = new List<Mongo_HouseStateModel_Wrapper>();
            string sql = string.Format(@"SELECT 
      [HouseId]
      ,[Rtype]
      ,[Createtime]
      ,[Remark]
      ,[AdminName]
  FROM [dbo].[HouseState]
where id in ({0})
", string.Join(",", listidsNew));

            DataTable dataTable = KYJHouseReadDB.GetTable(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                Mongo_HouseStateModel_Wrapper mongoHouseStateModelWrapper = new Mongo_HouseStateModel_Wrapper();
                mongoHouseStateModelWrapper.Id = Convert.ToInt32(row["HouseId"]);
                mongoHouseStateModelWrapper.MongoHouseStateModel.Createtime = Convert.ToDateTime(row["Createtime"]);
                mongoHouseStateModelWrapper.MongoHouseStateModel.Rtype = row["Rtype"].ToString();
                mongoHouseStateModelWrapper.MongoHouseStateModel.Remark = row["Remark"].ToString();
                mongoHouseStateModelWrapper.MongoHouseStateModel.AdminName = row["AdminName"].ToString();
                list.Add(mongoHouseStateModelWrapper);
            }
            return list;
        }
    }
}
