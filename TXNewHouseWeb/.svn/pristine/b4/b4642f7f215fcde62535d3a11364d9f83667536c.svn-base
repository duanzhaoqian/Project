using System;
using System.Collections.Generic;
using TXDal.NHouseActivies.Arranging;
namespace TXBll.NHouseActivies.Arranging
{
    public partial class Arranging_DeveloperBll
    {
        Arranging_DeveloperDal _dal = new Arranging_DeveloperDal();
        # region 获取排号购房活动用户列表(汪敏)
        /// <summary>
        /// 获取排号购房活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        public List<TXModel.NHouseActivies.Arranging.CT_UserList> GetActivitieArrangingUser(int activityId)
        {
            return _dal.GetActivitieArrangingUser(activityId);
        }
        #endregion

        #region 排号购房
        public int CreateArranging(int CityId, int DeveloperId, string ActivName, int ActivType, decimal ActivBond, DateTime ActivStart, DateTime ActivEnd, int Count, string[] ids)
        {
            return _dal.CreateArranging(CityId, DeveloperId, ActivName, ActivType, ActivBond, ActivStart, ActivEnd, Count, ids);
        }
        #endregion
    }
}
