using System.Collections.Generic;
using TXModel.User;
using TXDal.User;
using TXModel.Commons;
namespace TXBll.User
{
    public partial class MyNewHousesBll
    {
        CT_BidDal _dal = new CT_BidDal();
        /// <summary>
        /// 获取我参与的竞购
        /// author:汪敏
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="type">活动类型</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<CT_Bid> GetMyBidList(int userId, int type, Paging<TXModel.User.CT_Bid> paging)
        {
            return _dal.GetMyBidList(userId, type, paging);
        }
        /// <summary>
        /// 获取我参与的网络摇号
        /// author:汪敏
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<CT_Yaohao> GetMyYaohaoList(int UserId, int pageindex, int pagesize, out int totalcount)
        {
            return _dal.GetMyYaohaoList(UserId, pageindex, pagesize, out totalcount);
        }
        /// <summary>
        /// 获取我对某个房源的出价记录
        /// author:汪敏
        /// </summary>
        /// <param name="houseid">房源Id</param>
        /// <param name="userid">用户Id</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<TXOrm.BidPrice> GetMyBidRecord(int houseid, int userid, Paging<TXOrm.BidPrice> paging)
        {
            return _dal.GetMyBidRecord(houseid, userid, paging);
        }
        /// <summary>
        /// 获取用户收藏的楼盘
        /// author:汪敏
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<CT_FavoritePrem> GetMyPremisesFavorite(string pids, Paging<CT_FavoritePrem> paging)
        {
            return _dal.GetMyPremisesFavorite(pids, paging);
        }
        /// <summary>
        /// 获取楼盘预览房源信息
        /// author:汪敏
        /// </summary>
        /// <param name="PremisId">楼盘Id</param>
        /// <returns></returns>
        public List<CT_Bid> GetPreviewHouseByPremisId(int PremisId)
        {
            return _dal.GetPreviewHouseByPremisId(PremisId);
        }
        /// <summary>
        /// 取消收藏
        /// author:汪敏
        /// </summary>
        /// <param name="pid">楼盘id</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public int Delete(int pid, int uid)
        {
            return _dal.Delete(pid, uid);
        }
    }
}
