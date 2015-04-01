using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;
using TXModel.NHouseActivies.SecKill;
namespace TXDal.NHouseActivies.SecKill
{
    public partial class SecKill_DeveloperDal
    {
        # region 获取秒杀活动用户列表
        /// <summary>
        /// 获取秒杀活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="houseId">房源Id</param>
        /// <returns></returns>
        public List<CT_UserList> GetActivitieSecKillUser(int houseId)
        {
            List<CT_UserList> list = new List<CT_UserList>();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = (from bid in newhouseDb.BidPrices
                                 where bid.IsDel == false && bid.HouseId == houseId 
                                 select new
                                 {
                                     Id = bid.Id,
                                     LoginName=bid.BidUserName,
                                     Name = bid.BidRealName,
                                     IDCard = bid.IDCard,
                                     Mobile = bid.BidUserMobile,
                                     QQ = bid.BidUserQQ,
                                     Email = bid.BidUserMail,
                                     CreateTime = bid.CreateTime,
                                 });

                    list = query.OrderByDescending(it => it.CreateTime).ToList().ConvertAll(
                  it => new CT_UserList
                  {
                      Id = it.Id,
                      LoginName=it.LoginName,
                      RealName = it.Name,
                      IDCard = it.IDCard,
                      Mobile = it.Mobile,
                      QQ = it.QQ,
                      Email = it.Email,
                      BidTime = it.CreateTime.ToString("yyyy-MM-dd HH:mm")
                  });
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("houseId:{0}", houseId), e);
                //throw;
            }
            return list;
        }
        #endregion
    }
}
