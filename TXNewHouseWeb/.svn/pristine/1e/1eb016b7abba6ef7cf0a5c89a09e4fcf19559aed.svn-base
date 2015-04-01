
using TXOrm;
using TXModel.Commons;
using TXModel.Web;
namespace TXBll.HouseData
{
    /// <summary>
    /// 房源 开发商管理平台
    /// </summary>
    public partial class HouseBll
    {
        private readonly TXDal.HouseData.HouseDal dal = new TXDal.HouseData.HouseDal();

        #region 房源分页列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/4 10:40:57
        /// 功能描述：房源分页列表
        /// </summary>
        /// <param name="developerId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<HouseAndBuilding> GetHouseList(Paging<HouseAndBuilding> paging, int developerId, int pid, int bid, int uid, int fid, int sid, bool IsRelease, int order)
        {
            return dal.GetHouseList(paging, developerId, pid, bid, uid, fid, sid, IsRelease, order);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/4 10:40:57
        /// 功能描述：房源分页列表(已经删除房源)
        /// </summary>
        /// <param name="developerId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<HouseAndBuilding> GetHouseList(Paging<HouseAndBuilding> paging, int developerId, int pid, int bid, int uid, int fid, int sid)
        {
            return dal.GetHouseList(paging, developerId, pid, bid, uid, fid, sid);
        }
        #endregion

        #region 更新房源信息
        /// <summary>
        /// 更新房源信息
        /// </summary>
        /// <param name="entity">房源实体</param>
        /// <returns>返回：0 失败 1 成功 2 正则参加活动</returns>
        public int Update(House entity)
        {
            return dal.Update(entity);
        }
        #endregion


        #region 根据多个Id 批量修改房源状态

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/15 15:36:51
        /// 功能描述：根据多个Id 批量修改房源状态
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int UpdateHouse_IsRelease(string Ids, int IsRelease, int DeveloperId)
        {
            return dal.UpdateHouse_IsRelease(Ids, IsRelease, DeveloperId);
        }
        #endregion

        #region 根据多个Id 批量修改房源销售状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/15 15:36:51
        /// 功能描述：根据多个Id 批量修改房源发布状态
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int UpdateHouse_SalesStatus(string Ids, int SalesStatus, int DeveloperId)
        {
            return dal.UpdateHouse_SalesStatus(Ids, SalesStatus, DeveloperId);
        }

        #endregion

        #region 根据房源ID 获取房源CityId 可查询已经删除的房源
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/17 13:14:46
        /// 功能描述：根据房源ID 获取房源CityId 可查询已经删除的房源
        /// </summary>
        /// <param name="HouseId"></param>
        /// <returns></returns>
        public int GetCityIDByHouseID(int HouseId)
        {
            return dal.GetCityIDByHouseID(HouseId);
        }
        #endregion

        #region 删除单个房源

        ///作者：谢江
        ///时间：2014/1/20 16:59:55
        ///功能描述：删除单个房源
        /// /// <returns>返回：0 失败 1 成功 2 正则参加活动</returns>
        public int DelHouseByID(int houseId)
        {
            return dal.DelHouseByID(houseId);
        }
        #endregion
    }
}