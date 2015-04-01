using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;
using TXModel.NHouseActivies.Biding;
namespace TXDal.NHouseActivies.Biding
{
    /// <summary>
    /// 竞价活动管理(开发商后台)
    /// </summary>
    public partial class BidingDal
    {
        # region 获取竞价活动用户列表
        /// <summary>
        /// 获取竞价活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="houseId">房源Id</param>
        /// <returns></returns>
        public List<CT_UserList> GetActivitieBidingUser(int houseId)
        {
            List<CT_UserList> list = new List<CT_UserList>();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {

                    var query = (from bid in newhouseDb.BidPrices
                                 where bid.IsDel == false && bid.HouseId == houseId
                                 group bid by bid.BidUserId into biduser
                                 select new
                                 {
                                     Id = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().Id,
                                     HouseId = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().HouseId,
                                     Status = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().Status,
                                     LoginName = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserName,
                                     Name = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidRealName,
                                     IDCard = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().IDCard,
                                     Mobile = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserMobile,
                                     QQ = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserQQ,
                                     Email = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserMail,
                                     BidCount = biduser.Count(),
                                     LastBidPrice = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().BidUserPrice,
                                     LastBidTime = biduser.OrderByDescending(q => q.CreateTime).FirstOrDefault().CreateTime,
                                 });
                    list = query.ToList().ConvertAll(
                  it => new CT_UserList
                  {
                      Id = it.Id,
                      HouseId = it.HouseId,
                      Status = it.Status,
                      LoginName = it.LoginName,
                      RealName = it.Name,
                      IDCard = it.IDCard,
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



        # region 公布活动结果
        /// <summary>
        /// 公布活动结果
        /// author:汪敏
        /// </summary>
        /// <param name="houseId">房源Id</param>
        /// <param name="houseId">出价表Id</param>
        /// <returns></returns>
        public int AnnounceActivitieResult(int houseId, int bidPriceId)
        {
            int result = 0;
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = (from house in newhouseDb.Houses where house.Id == houseId select house).FirstOrDefault();
                    query.SalesStatus = 3;

                    //要成交的 出价 和 订单
                    var bidPrice = (from bid in newhouseDb.BidPrices where bid.Id == bidPriceId select bid).FirstOrDefault();
                    var order = (from o in newhouseDb.Orders where o.HouseId == houseId && o.ActivitiesId == bidPrice.ActivitiesId && o.BidUserId == bidPrice.BidUserId select o).FirstOrDefault();

                    //修改活动中 已中标或已成交的记录
                    //出价记录状态为 未成交
                    //订单状态为 未成交
                    var listBid = newhouseDb.BidPrices.Where(b => b.ActivitiesId == bidPrice.ActivitiesId && b.HouseId == houseId);
                    foreach (var item in listBid)
                    {
                        item.Status = 0;
                    }

                    var listOrder = newhouseDb.Orders.Where(o => o.ActivitiesId == bidPrice.ActivitiesId && o.HouseId == houseId);
                    foreach (var item in listOrder)
                    {
                        item.Status = 0;
                    }

                    //修改要成交的 记录和订单 状态
                    bidPrice.Status = 2;
                    order.Status = 2;

                    newhouseDb.SaveChanges();
                    result = 1;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("houseId:{0},bidPriceId:{1}", houseId, bidPriceId), e);
                //throw;
            }
            return result;
        }
        #endregion


    }
}