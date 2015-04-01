using System.Collections.Generic;
using TXDal.NHouseActivies.Discount;
namespace TXBll.NHouseActivies.Discount
{
    public partial class Discount_DeveloperBll
    {
        Discount_DeveloperDal _dal = new Discount_DeveloperDal();
        # region 获取限时折扣活动用户列表(汪敏)
        /// <summary>
        /// 获取限时折扣活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋 Id</param>
        /// <param name="unitId">单元Id</param>
        /// <returns></returns>
        public List<TXModel.NHouseActivies.Discount.CT_UserList> GetActivitieDiscountsUser(int activityId, int premisesId, int buildingId, int unitId)
        {
            return _dal.GetActivitieDiscountsUser(activityId, premisesId, buildingId, unitId);
        }

        #endregion

        #region 限时折扣

        public List<object> GetFloor(int PremisesId, int BuildingId, int UnitId)
        {
            return _dal.GetFloor(PremisesId, BuildingId, UnitId);
        }
        public int CreateActivity(string[] activity, string[] houses)
        {
            return _dal.CreateActivity(activity, houses);
        }
        #endregion

    }
}
