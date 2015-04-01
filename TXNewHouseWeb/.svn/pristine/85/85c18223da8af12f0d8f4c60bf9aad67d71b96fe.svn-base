using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXOrm;
using TXModel.NHouseActivies.Arranging;
using System.Data.Common;
namespace TXDal.NHouseActivies.Arranging
{
    public partial class Arranging_DeveloperDal
    {
        # region 获取排号购房活动用户列表
        /// <summary>
        /// 获取排号购房活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        public List<CT_UserList> GetActivitieArrangingUser(int activityId)
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
                                     ActivitiesId = user.ActivitiesId,
                                     LoginName = user.BidUserName,
                                     Name = user.BidRealName,
                                     Email = user.BidUserMail,
                                     IDCard = user.IDCard,
                                     Mobile = user.BidUserMobile,
                                     QQ = user.BidUserQQ,
                                     Num = user.BidUserNumber,
                                     CreateTime = user.CreateTime
                                 });

                    list = query.OrderByDescending(it => it.CreateTime).ToList().ConvertAll(
                  it => new CT_UserList
                  {
                      LoginName = it.LoginName,
                      RealName = it.Name,
                      IDCard = it.IDCard,
                      Mobile = it.Mobile,
                      Email = it.Email,
                      QQ = it.QQ,
                      Num = it.Num,
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

        public int CreateArranging(int CityId, int DeveloperId, string ActivName, int ActivType, decimal ActivBond, DateTime ActivStart, DateTime ActivEnd, int Count, string[] ids)
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

                sql.Append("INSERT INTO [Activities] ");
                sql.Append("([DeveloperId],[Name],[Type],[UserCount],[HouseCount],[BidPrice],[AddPrice],");
                sql.Append("[MaxPrice],[BeginTime],[EndTime],[Bond],[ActivitieState],[UpdateTime],[CreateTime],[IsDel]) ");
                sql.AppendFormat("VALUES({0},'{1}',{2},{3},{4},0,0,0,'{5}','{6}',{7},{8},getdate(),getdate(),0)", DeveloperId, ActivName, ActivType, Count, ids.Length, ActivStart, ActivEnd, ActivBond, ActivStart >= DateTime.Now ? 1 : 0);

                #endregion

                #region 表ActivitiesHouse

                sql.AppendFormat("INSERT INTO [ActivitiesHouse] ");
                sql.AppendFormat("([ActivitiesId],[CityId],[DeveloperId],[PremisesId],[BuildingId],[UnitId],[HouseId],[Discount],[UpdateTime],[CreateTime],[IsDel])");
                sql.AppendFormat("select @@identity,{0}, DeveloperId,PremisesId,BuildingId,UnitId,Id,0,getdate(),getdate(),0 from House where House.Id in ({1})", CityId, string.Join(",", ids));

                #endregion

                #endregion

                db.ExecuteStoreCommand(sql.ToString());
                tran.Commit();
                result = 1;
            }
            return result;
        }
    }
}
