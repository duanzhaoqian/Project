using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXDal.NHouseActivies.APrice;
using TXOrm;
using TXModel.NHouseActivies.Discount;
using System.Data.Common;
using System.Data.SqlClient;
namespace TXDal.NHouseActivies.Discount
{
    public partial class Discount_DeveloperDal
    {
        # region 获取限时折扣活动用户列表
        /// <summary>
        /// 获取限时折扣活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋 Id</param>
        /// <param name="unitId">单元Id</param>
        /// <returns></returns>
        public List<CT_UserList> GetActivitieDiscountsUser(int activityId, int premisesId, int buildingId, int unitId)
        {
            List<CT_UserList> list = new List<CT_UserList>();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {

                    var BidPrices = newhouseDb.BidPrices.Where(b => b.IsDel == false && b.ActivitiesId == activityId);
                    var ActivitiesHouses =newhouseDb.ActivitiesHouses.Where(ah => ah.IsDel == false && ah.ActivitiesId == activityId);

                    var query = (from user in BidPrices
                                 join house in ActivitiesHouses
                                 on user.HouseId equals house.HouseId
                                 select new
                                 {
                                     PremisesId = house.PremisesId,
                                     BuildingId = house.BuildingId,
                                     UnitId = house.UnitId,
                                     LoginName = user.BidUserName,
                                     Name = user.BidRealName,
                                     IDCard = user.IDCard,
                                     QQ = user.BidUserQQ,
                                     Email = user.BidUserMail,
                                     Mobile = user.BidUserMobile,
                                     PremisName = (from h in newhouseDb.Houses join p in newhouseDb.Premises on h.PremisesId equals p.Id where h.Id == user.HouseId select p.Name).FirstOrDefault(),
                                     BuildingName = (from h in newhouseDb.Houses join b in newhouseDb.Buildings on h.BuildingId equals b.Id where h.Id == user.HouseId select b.Name + b.NameType).FirstOrDefault(),
                                     Unit = (from h in newhouseDb.Houses join u in newhouseDb.Units on h.BuildingId equals u.Num where h.Id == user.HouseId && u.BuildingId == h.BuildingId select u.Name).FirstOrDefault(),
                                     Floor = (from h in newhouseDb.Houses where h.Id == user.HouseId select h.Floor).FirstOrDefault(),
                                     HouseName = (from h in newhouseDb.Houses where h.Id == user.HouseId select h.Name).FirstOrDefault(),
                                     CreateTime = user.CreateTime
                                 });
                    if (query.Count() > 0)
                    {
                        if (premisesId > 0)
                        {
                            query = query.Where(p => p.PremisesId == premisesId);
                        }
                        if (buildingId > 0)
                        {
                            query = query.Where(p => p.BuildingId == buildingId);
                        }
                        if (unitId > 0)
                        {
                            query = query.Where(p => p.UnitId == unitId);
                        }
                        list = query.OrderByDescending(it => it.CreateTime).ToList().ConvertAll(
                      it => new CT_UserList
                      {
                          LoginName = it.LoginName,
                          RealName = it.Name,
                          IDCard = it.IDCard,
                          Mobile = it.Mobile,
                          QQ = it.QQ,
                          Email = it.Email,
                          HouseName = it.PremisName + " " + it.BuildingName + " " + it.Unit + it.Floor.ToString() + it.HouseName,
                          CreateTime = it.CreateTime.ToString("yyyy-MM-dd HH:mm")
                      });
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("activityId:{0},premisesId:{1},buildingId:{2},unitId:{3}", activityId, premisesId, buildingId, unitId), e);
                //throw;
            }
            return list;
        }
        #endregion


        public List<object> GetFloor(int PremisesId, int BuildingId, int UnitId)
        {
            using (var db = new kyj_NewHouseDBEntities())
            {
                try
                {
                    var query = db.Houses.Where(it => !it.IsDel && it.PremisesId == PremisesId && it.BuildingId == BuildingId && it.UnitId == UnitId)
                                         .Select(it => new { Id = it.Floor }).Distinct().ToList<object>();
                    return query;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public int CreateActivity(string[] activity, string[] houses)
        {
            int result = 0;
            DbTransaction tran = null;
            try
            {
                using (kyj_NewHouseDBEntities db = new kyj_NewHouseDBEntities())
                {
                    db.Connection.Open();
                    tran = db.Connection.BeginTransaction();
                    DateTime starTime = Convert.ToDateTime(activity[2]);
                    DateTime endTime = Convert.ToDateTime(activity[3]).AddHours(23).AddMinutes(59).AddSeconds(59);

                    #region SQL语句
                    StringBuilder sql = new StringBuilder();
                    sql.AppendFormat(@"INSERT INTO [kyj_NewHouseDB].[dbo].[Activities]
                                       ([DeveloperId]
                                       ,[Name]
                                       ,[Type]
                                       ,[UserCount]
                                       ,[HouseCount]
                                       ,[BidPrice]
                                       ,[AddPrice]
                                       ,[MaxPrice]
                                       ,[BeginTime]
                                       ,[EndTime]
                                       ,[Bond]
                                       ,[ActivitieState]
                                       ,[UpdateTime]
                                       ,[CreateTime]
                                       ,[IsDel])
                                 VALUES
                                       ( {0}
                                       ,'{1}'
                                       , 2
                                       ,  0
                                       ,{2} 
                                       ,  0
                                       , 0
                                       , 0
                                       ,'{3}'
                                       ,  '{4}'
                                       , {5}
                                       , {6}
                                       , getdate()
                                       ,getdate()
                                       ,0);
                                       declare @ActivitiesId int; select @ActivitiesId=@@IDENTITY;", activity[4], activity[0], houses.Length, starTime, endTime, activity[1], starTime >= DateTime.Now.Date ? 1 : 0);
                    for (int i = 0; i < houses.Length; i++)
                    {
                        string[] house = houses[i].Split('&');
                        sql.AppendFormat(@"INSERT INTO [ActivitiesHouse]
                                        ([ActivitiesId]
                                        ,[CityId]
                                        ,[DeveloperId]
                                        ,[PremisesId]
                                        ,[BuildingId]
                                        ,[UnitId]
                                        ,[HouseId]
                                        ,[Discount]
                                        ,[UpdateTime]
                                        ,[CreateTime]
                                        ,[IsDel])
                                    VALUES
                                        (@ActivitiesId
                                        ,{0}
                                        ,{1}
                                        ,{2}
                                        ,{3}
                                        ,{4}
                                        ,{5}
                                        ,{6}
                                        ,getdate()
                                         ,getdate()
                                        ,0);", activity[6], activity[4], activity[5], house[1], house[2], house[0], house[3]);
                    }
                    #endregion

                    db.ExecuteStoreCommand(sql.ToString());
                    tran.Commit();
                    result = 1;
                }
            }
            catch (SqlException ex)
            {
                if (tran != null) tran.Rollback();
            }
            return result;
        }

    }
}
