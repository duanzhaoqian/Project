using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;
using TXModel.NHouseActivies.Bargain;
namespace TXDal.NHouseActivies.Bargain
{
    public partial class Bargain_DeveloperDal
    {
        # region 获取砍价活动用户列表
        /// <summary>
        /// 获取砍价活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="houseId">房源Id</param>
        /// <returns></returns>
        public List<CT_UserList> GetActivitieBargainUser(int houseId)
        {
            List<CT_UserList> list = new List<CT_UserList>();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = (from  bid in newhouseDb.BidPrices
                                 where  bid.IsDel == false && bid.HouseId == houseId
                                 group bid by bid.BidUserId into biduser
                                 select new
                                 {
                                     Id = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().Id,
                                     HousId = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().HouseId,
                                     Status = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().Status,
                                     LoginName = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserName,
                                     Name = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserName,
                                     IDCard = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().IDCard,
                                     Mobile = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserMobile,
                                     QQ = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserQQ,
                                     Email = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserMail,
                                     BidCount = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidCount,
                                     LastBidPrice = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserPrice,
                                     LastBidTime = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().CreateTime,
                                 });

                    list = query.ToList().ConvertAll(
                  it => new CT_UserList
                  {
                      Id = it.Id,
                      HouseId=it.HousId,
                      Status=it.Status,
                      LoginName=it.LoginName,
                      RealName = it.Name,
                      IDCard=it.IDCard,
                      Mobile = it.Mobile,
                      QQ = it.QQ,
                      Email = it.Email,
                      BidCount = it.BidCount,
                      LastBidPrice = it.LastBidPrice,
                      LastBidTime = it.LastBidTime.ToString("yyyy-MM-dd HH:mm")
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
