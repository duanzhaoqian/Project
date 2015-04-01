using System;
using System.Collections.Generic;
using TXCommons.Admins;
using TXModel.AdminPVM;
using TXModel.NHouseActivies.Common;
using TXOrm;

namespace TXBll.NHouseActivies.SecKill
{
    /// <summary>
    /// 秒杀(网站管理平台)
    /// </summary>
    public partial class SecKillBll
    {
        /// <summary>
        /// 根据省编号、市编号、开发商名称获取开发商列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="provinceId">省编号</param>
        /// <param name="cityId">市编号</param>
        /// <param name="cmpname">开发商名称</param>
        /// <returns></returns>
        public List<Developer_Identity> GetList_ByProvinceIdCityIdCompanyName(int provinceId, int cityId, string cmpname)
        {
            return _secKillDal.GetList_ByProvinceIdCityIdCompanyName(provinceId, cityId, cmpname);
        }

        /// <summary>
        /// 根据省编号、市编号、开发商名称获取开发商列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="provinceId">省编号</param>
        /// <param name="cityId">市编号</param>
        /// <param name="name">开发商/代理商名称</param>
        /// <returns></returns>
        public List<PVS_NH_DeveloperAgentName> GetList_ByProvinceIdCityIdDeveloperAgent(int provinceId, int cityId, string name)
        {
            return _secKillDal.GetList_ByProvinceIdCityIdDeveloperAgent(provinceId, cityId, name);
        }

        /// <summary>
        /// 根据条件获取秒杀活动信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_SecKill> GetPageList_BySearch_Admin(PVS_NH_SecKill search, int pageIndex, int pageSize)
        {
            var list = _secKillDal.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            if (null != list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].BuildingName = list[i].BuildingName + list[i].BuildingNameType;
                    list[i].FloorString = (0 < list[i].Floor ? string.Format("{0}F", list[i].Floor) : string.Format("B{0}", Math.Abs(list[i].Floor)));
                    list[i].TotalPriceString = string.Format("{0:0}", list[i].TotalPrice);
                    list[i].BidPriceString = string.Format("{0:0}", list[i].BidPrice);
                    list[i].ActivityTime = string.Format("{0:yyyy-MM-dd HH:mm}---{1:yyyy-MM-dd HH:mm}", list[i].BeginTime, list[i].EndTime);
                    list[i].RestTimeString = new AdminComs().GetRestTime(list[i].EndTime, (DateTime.Now < list[i].BeginTime ? list[i].BeginTime : DateTime.Now));
                    // list[i].ActivitieStateString = AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(list[i].BeginTime, list[i].EndTime, DateTime.Now);
                    list[i].ActivitieStateString = AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(list[i].ActivitieState);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据条件获取秒杀活动信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Admin(PVS_NH_SecKill search)
        {
            return _secKillDal.GetTotalCount_BySearch_Admin(search);
        }

        /// <summary>
        /// 根据活动编号获取房源信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<PVE_NH_SecKill_HouseInfo> GetHouseInfoByActivityId(int activityId)
        {
            var list = _secKillDal.GetHouseInfoByActivityId(activityId);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FloorString = (0 < list[i].Floor ? Convert.ToString(list[i].Floor) : string.Format("地下{0}", Math.Abs(list[i].Floor)));
                }
            }
            return list;
        }

        /// <summary>
        /// 根据活动、房源编号获取报名信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <param name="houseId">房源信息</param>
        /// <returns></returns>
        public List<PVM_NH_SecKill_UserReport> GetUserReportsByActivityId(int activityId, int houseId)
        {
            var list = _secKillDal.GetUserReportsByActivityId(activityId, houseId);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Id = (i + 1);
                    list[i].BidTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", list[i].BidTime);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据活动编号删除活动相关信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public bool DelByActivityId(int activityId)
        {
            var result = _secKillDal.DelByActivityId(activityId);

            if ("1".Equals(result.Code))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取搜索记录
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_SecKill_Edit_Houses> GetPageList_BySearch_EditHouses_Admin(PVS_NH_SecKill_Edit search, int pageIndex = 0, int pageSize = 0)
        {
            var list = _secKillDal.GetPageList_BySearch_EditHouses_Admin(search, pageIndex, pageSize);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FloorString = (0 < list[i].Floor ? string.Format("{0}层", list[i].Floor) : string.Format("地下{0}层", list[i].Floor));
                    list[i].BuildingName += list[i].BuildingNameType;
                    list[i].RoomTypeString = string.Format("{0}室{1}厅{2}卫{3}厨", list[i].Room, list[i].Hall, list[i].Toilet, list[i].Kitchen);
                    list[i].BuildingAreaString = string.Format("{0:0}", list[i].BuildingArea);
                    list[i].SinglePriceString = string.Format("{0:0}", list[i].SinglePrice);
                    list[i].TotalPriceString = string.Format("{0:0}", list[i].TotalPrice);
                    list[i].SalesStatusString = AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(list[i].SalesStatus);
                    list[i].CreateTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", list[i].CreateTime);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取总记录数
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_EditHouses_Admin(PVS_NH_SecKill_Edit search)
        {
            return _secKillDal.GetTotalCount_BySearch_EditHouses_Admin(search);
        }

        /// <summary>
        /// 保存活动信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public bool SaveActivityInfo(PVE_NH_SecKill_Edit activity)
        {
            var result = _secKillDal.SaveActivityInfo(activity);
            if ("1".Equals(result.Code))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取活动房源信息（HouseId, PremisesId）
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ActivityHouse> GetActivityHouseInfoList(int activityId)
        {
            return _secKillDal.GetActivityHouseInfoList(activityId);
        }

        /// <summary>
        /// 获取指定活动的楼盘信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ActivityHouse> GetActivityPremisesInfoList(int activityId)
        {
            return _secKillDal.GetActivityPremisesInfoList(activityId);
        }
    }
}