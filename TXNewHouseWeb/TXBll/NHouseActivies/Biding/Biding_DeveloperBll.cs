using System.Collections.Generic;
using TXDal.NHouseActivies.Biding;
namespace TXBll.NHouseActivies.Biding
{
    /// <summary>
    /// 竞价活动管理(开发商后台)
    /// </summary>
    public partial class BidingBll
    {
        BidingDal _dal = new BidingDal();
        # region 获取竞价活动用户列表
        /// <summary>
        /// 获取竞价活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="houseId">房源Id</param>
        /// <returns></returns>
        public List<TXModel.NHouseActivies.Biding.CT_UserList> GetActivitieBidingUser(int houseId)
        {
            return _dal.GetActivitieBidingUser(houseId);
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
            return _bidingDal.AnnounceActivitieResult(houseId, bidPriceId);
        }
        #endregion

    }
}