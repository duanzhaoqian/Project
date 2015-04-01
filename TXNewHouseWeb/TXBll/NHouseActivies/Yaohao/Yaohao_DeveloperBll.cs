using System.Collections.Generic;
using TXDal.NHouseActivies.Yaohao;
using TXModel.Commons;
namespace TXBll.NHouseActivies.Yaohao
{
    /// <summary>
    /// 摇号 (开发商平台)
    /// </summary>

    public partial class YaohaoBll
    {
        Yaohao_DeveloperDal dal = new Yaohao_DeveloperDal();
        /// <summary>
        /// 摇号活动列表
        /// author:汪敏
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="paging">分页类</param>
        /// <returns></returns>
        public Paging<TXModel.NHouseActivies.YaoHao.CT_YaoHao> GetYaohaoList(int developerId, Paging<TXModel.NHouseActivies.YaoHao.CT_YaoHao> paging)//摇号活动列表
        {
            return dal.GetYaohaoList(developerId, paging);
        }
        /// <summary>
        /// 申请摇号活动
        /// author:汪敏
        /// </summary>
        /// <returns>申请失败返回:0</returns>
        public int Add(TXOrm.ActivitiesYaoHaoApply activity)
        {
            return dal.Add(activity);
        }
        /// <summary>
        /// (楼盘)楼栋下是否有房源参与了摇号
        /// author:汪敏
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <returns></returns>
        public int CheckYaohao(int developerId, int premisesId, int buildingId)//(楼盘)楼栋下是否有房源参与了摇号
        {
            return dal.CheckYaohao(developerId, premisesId, buildingId);
        }
        /// <summary>
        /// 摇号活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        public List<TXOrm.BidPrice> GetYaohaoUserList(int activityId)//摇号活动用户列表
        {
            return dal.GetYaohaoUserList(activityId);
        }
    }
}