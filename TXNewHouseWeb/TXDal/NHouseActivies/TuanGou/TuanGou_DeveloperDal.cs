using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXOrm;
using TXModel.NHouseActivies.TuanGou;
using System.Data.Common;
using System.Data.SqlClient;
namespace TXDal.NHouseActivies.TuanGou
{
    public partial class TuanGou_DeveloperDal
    {
        # region 获取阶梯团购活动用户列表
        /// <summary>
        /// 获取阶梯团购活动用户列表
        /// aythor:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        public List<CT_UserList> GetActivitieTuanGouUser(int activityId)
        {
            List<CT_UserList> list = new List<CT_UserList>();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = (from user in newhouseDb.BidPrices
                                 where user.IsDel == false && user.ActivitiesId == activityId
                                 select new
                                 {
                                     LoginName = user.BidUserName,
                                     Name = user.BidRealName,
                                     IDCard = user.IDCard,
                                     Mobile = user.BidUserMobile,
                                     QQ = user.BidUserQQ,
                                     Email = user.BidUserMail,
                                     PremisName = (from h in newhouseDb.Houses join p in newhouseDb.Premises on h.PremisesId equals p.Id where h.Id == user.HouseId select p.Name).FirstOrDefault(),
                                     BuildingName = (from h in newhouseDb.Houses join b in newhouseDb.Buildings on h.BuildingId equals b.Id where h.Id == user.HouseId select b.Name + b.NameType).FirstOrDefault(),
                                     Unit = (from h in newhouseDb.Houses join u in newhouseDb.Units on h.BuildingId equals u.Num where h.Id == user.HouseId && u.BuildingId == h.BuildingId select u.Name).FirstOrDefault(),
                                     Floor = (from h in newhouseDb.Houses where h.Id == user.HouseId select h.Floor).FirstOrDefault(),
                                     HouseName = (from h in newhouseDb.Houses where h.Id == user.HouseId select h.Name).FirstOrDefault(),
                                     CreateTime = user.CreateTime
                                 });

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
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("activityId:{0}", activityId), e);
                //throw;
            }
            return list;
        }
        #endregion

        #region 创建团购活动

        public int CreateTuanGou(int CityId, int DeveloperId, string ActivName, int ActivType, decimal ActivBond, DateTime ActivStart, DateTime ActivEnd, String[] Arr, string[] ids)
        {
            int result = 0;
            DbTransaction tran = null;
            using (kyj_NewHouseDBEntities db = new kyj_NewHouseDBEntities())
            {
                db.Connection.Open();
                tran = db.Connection.BeginTransaction();

                #region SQL语句

                StringBuilder sql = new StringBuilder();

                #region 表Activities
                sql.Append("declare @aidentity int ;");
                sql.Append(@"INSERT INTO [Activities]
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
                                   ,[IsDel]
                                  )
                             VALUES
                                   (@DeveloperId
                                    ,@Name
                                    ,@Type
                                    ,0
                                    ,@HouseCount
                                    ,0
                                    ,0
                                    ,0
                                    ,@BeginTime
                                    ,@EndTime
                                    ,@Bond
                                    ,@ActivitieState
                                    ,getdate()
                                    ,getdate()
                                    ,0);");

                sql.Append("set @aidentity = @@identity; ");

                #endregion

                #region 表ActivitiesHouse
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
                                   select 
                                   @aidentity,{0},@DeveloperId,PremisesId,BuildingId,UnitId,Id,0 as Discount ,getdate(),getdate() ,0 as IsDel   from House where Id in({1});", CityId, string.Join(",", ids));

                #endregion

                #region 表Social

                for (int i = 0; i < Arr.Length; i++)
                {
                    string[] disCondi = Arr[i].Split(',');
                    sql.AppendFormat(@"INSERT INTO [Social]
                                    ([ActivitiesId]
                                    ,[UserCount]
                                    ,[Discount]
                                    ,[CreateTime]
                                    ,[UpdateTime]
                                    ,[IsDel])
                                VALUES
                                    (@aidentity
                                    ,{0}
                                    ,{1}
                                    ,getdate()
                                    ,getdate()
                                    ,0);", disCondi[0], disCondi[1]);
                }
                #endregion

                #region 参数
                var dbparams = new DbParameter[] { 
                    new SqlParameter("DeveloperId",DeveloperId) , 
                    new SqlParameter("Name",ActivName) , 
                    new SqlParameter("Type",ActivType) , 
                    new SqlParameter("BeginTime",ActivStart),
                    new SqlParameter("EndTime",ActivEnd),
                    new SqlParameter("Bond",ActivBond),
                    new SqlParameter("HouseCount",ids.Length),
                    new SqlParameter("ActivitieState",ActivStart>=DateTime.Now?1:0)
                 };
                #endregion

                #endregion

                db.ExecuteStoreCommand(sql.ToString(), dbparams);
                tran.Commit();
                result = 1;
            }
            return result;
        }



        #endregion
    }
}
