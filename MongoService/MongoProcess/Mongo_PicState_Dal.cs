using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TXCommons.DBUtility;

namespace MongoProcess
{
    public class Mongo_PicState_Dal
    {
        public List<Mongo_PicStateModel_Wrapper> GetPicStateModels(List<int> listidsNew)
        {
            List<Mongo_PicStateModel_Wrapper> list = new List<Mongo_PicStateModel_Wrapper>();
            string sql = string.Format(@"SELECT 
      [HouseId]
      ,[State]
      ,[CreateTime]
      ,[Remerk]
      ,[AdminName]
  FROM [dbo].[PicState]
where id in ({0})
", string.Join(",", listidsNew));

            DataTable dataTable = KYJHouseReadDB.GetTable(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                Mongo_PicStateModel_Wrapper mongoHouseStateModelWrapper = new Mongo_PicStateModel_Wrapper();
                mongoHouseStateModelWrapper.Id = Convert.ToInt32(row["HouseId"]);
                mongoHouseStateModelWrapper.MongoPicStateModel.CreateTime = Convert.ToDateTime(row["CreateTime"]);
                mongoHouseStateModelWrapper.MongoPicStateModel.State = row["State"].ToString();
                mongoHouseStateModelWrapper.MongoPicStateModel.Remerk = row["Remerk"].ToString();
                mongoHouseStateModelWrapper.MongoPicStateModel.AdminName = row["AdminName"].ToString();
                list.Add(mongoHouseStateModelWrapper);
            }

            return list;

        }

        public List<Mongo_PicStateModel_Wrapper> GetPicStateModels(int startId, int endId)
        {

            List<Mongo_PicStateModel_Wrapper> list = new List<Mongo_PicStateModel_Wrapper>();
            string sql = string.Format(@"SELECT 
      [HouseId]
      ,[State]
      ,[CreateTime]
      ,[Remerk]
      ,[AdminName]
  FROM [dbo].[PicState]
where id >={0} and id <={1}
", startId, endId);

            DataTable dataTable = KYJHouseReadDB.GetTable(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                Mongo_PicStateModel_Wrapper mongoHouseStateModelWrapper = new Mongo_PicStateModel_Wrapper();
                mongoHouseStateModelWrapper.Id = Convert.ToInt32(row["HouseId"]);
                mongoHouseStateModelWrapper.MongoPicStateModel.CreateTime = Convert.ToDateTime(row["CreateTime"]);
                mongoHouseStateModelWrapper.MongoPicStateModel.State = row["State"].ToString();
                mongoHouseStateModelWrapper.MongoPicStateModel.Remerk = row["Remerk"].ToString();
                mongoHouseStateModelWrapper.MongoPicStateModel.AdminName = row["AdminName"].ToString();
                list.Add(mongoHouseStateModelWrapper);
            }

            return list;
        }
    }
}
