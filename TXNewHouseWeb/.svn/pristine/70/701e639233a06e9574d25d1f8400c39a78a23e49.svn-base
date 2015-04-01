using System;
using System.Collections.Generic;
using TXDal.NHouseActivies.TuanGou;
namespace TXBll.NHouseActivies.TuanGou
{
    public partial class TuanGou_DeveloperBll
    {
        TuanGou_DeveloperDal _dal = new TuanGou_DeveloperDal();
        # region 获取阶梯团购活动用户列表(汪敏)
        /// <summary>
        /// 获取阶梯团购活动用户列表
        /// aythor:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        public List<TXModel.NHouseActivies.TuanGou.CT_UserList> GetActivitieTuanGouUser(int activityId)
        {
            return _dal.GetActivitieTuanGouUser(activityId);
        }
        #endregion

        public int CreateTuanGou(int CityId, int DeveloperId, string ActivName, int ActivType, decimal ActivBond, DateTime ActivStart, DateTime ActivEnd, String[] Arr, string[] ids)
        {
            return _dal.CreateTuanGou(CityId, DeveloperId, ActivName, ActivType, ActivBond, ActivStart, ActivEnd, Arr, ids);
        }
    }
}
