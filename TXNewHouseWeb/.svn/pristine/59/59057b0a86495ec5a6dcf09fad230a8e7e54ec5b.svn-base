using System;
using System.Collections.Generic;
using TXCommons.Admins;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXBll.NHouseActivies.Discount
{
    /// <summary>
    /// 折扣 (网站管理平台)
    /// </summary>
    public partial class DiscountBll
    {
        /// <summary>
        /// 分页搜索显示折扣列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Discount> GetPageList_BySearch_Admin(PVS_NH_Discount search, int pageIndex, int pageSize)
        {
            var list = _discountDal.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].BeginTimeString = string.Format("{0:yyyy-MM-dd}", list[i].BeginTime);
                    list[i].EndTimeString = string.Format("{0:yyyy-MM-dd}", list[i].EndTime);
                    list[i].DiscountString = (list[i].MinDiscount == list[i].MaxDiscount
                        ? Convert.ToString(list[i].MinDiscount)
                        : string.Format("{0}～{1}", list[i].MinDiscount, list[i].MaxDiscount));

                    if (list[i].ActivitieState != 2) // 活动状态值为“2:已结束”可能是后台管理人员提前结束该活动时设置，此时不以时间范围判断活动状态
                    {
                        list[i].ActivitieStateString = AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(list[i].BeginTime, list[i].EndTime, DateTime.Now);
                    }
                    else
                    {
                        list[i].ActivitieStateString = AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(list[i].ActivitieState);
                    }
                    list[i].RestTimeString = GetRestTime(list[i].EndTime - DateTime.Now);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private string GetRestTime(TimeSpan timeSpan)
        {
            var str = string.Empty;
            if (0 < timeSpan.Days)
            {
                str += timeSpan.Days + "天";
            }
            if (0 < timeSpan.Hours)
            {
                str += timeSpan.Hours + "小时";
            }
            if (0 < timeSpan.Minutes)
            {
                str += timeSpan.Minutes + "分";
            }
            if (string.IsNullOrEmpty(str))
            {
                str = "0";
            }
            return str;
        }

        /// <summary>
        /// 分页搜索显示折扣列表总记录
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Admin(PVS_NH_Discount search)
        {
            return _discountDal.GetTotalCount_BySearch_Admin(search);
        }

        /// <summary>
        /// 根据编号删除活动
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <returns></returns>
        public ESqlResult DelDiscountById(int id)
        {
            return _discountDal.DelDiscountById(id);
        }

        #region 修改限时折扣房源列表

        /// <summary>
        /// 获取修改限时折扣房源列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Discount_Edit1> GetHousesInDiscountEdit1(PVS_NH_Discount_Houses_Edit1 search, int pageIndex, int pageSize)
        {
            var list = _discountDal.GetHousesInDiscountEdit1(search, pageIndex, pageSize);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].BuildingName = list[i].BuildingName + list[i].BuildingNameType;
                    list[i].FloorName = (list[i].Floor > 0 ? string.Format("{0}层", list[i].Floor) : string.Format("地下{0}层", Math.Abs(list[i].Floor)));
                    list[i].RoomTypeString = string.Format("{0}室{1}厅{2}卫{3}厨", list[i].Room, list[i].Hall, list[i].Toilet, list[i].Kitchen);
                    list[i].BuildingAreaString = string.Format("{0:N0}平方米", list[i].BuildingArea);
                    list[i].SinglePriceString = string.Format("{0:N0}元/平方米", list[i].SinglePrice);
                    list[i].TotalPriceString = string.Format("{0:N0}万元/套", list[i].TotalPrice);
                    list[i].SalesStatusString = AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(list[i].SalesStatus);
                    list[i].CreateTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", list[i].CreateTime);

                }
            }
            return list;
        }

        /// <summary>
        /// 获取修改限时折扣房源列表总记录数
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_GetHousesInDiscountEdit1(PVS_NH_Discount_Houses_Edit1 search)
        {
            return _discountDal.GetTotalCount_BySearch_GetHousesInDiscountEdit1(search);
        }

        /// <summary>
        /// 获取参与活动的房源列表
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<ActivitiesHouse> GetHousesJoinDiscount(int activityId)
        {
            return _discountDal.GetHousesJoinDiscount(activityId);
        }

        /// <summary>
        /// 获取选取参加活动的房源信息
        /// </summary>
        /// <param name="activitiesId">活动编号</param>
        /// <param name="houseIds">房源编号集合(逗号分隔)</param>
        /// <returns></returns>
        public List<PVM_NH_Discount_Edit2> GetHousesDiscountInDiscountEdit(string activitiesId, string houseIds)
        {
            var list = _discountDal.GetHousesDiscountInDiscountEdit(activitiesId, houseIds);

            if (null != list
                && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].BuildingName = list[i].BuildingName + list[i].BuildingNameType;
                    list[i].FloorName = (list[i].Floor > 0 ? string.Format("{0}层", list[i].Floor) : string.Format("地下{0}层", Math.Abs(list[i].Floor)));
                    list[i].RoomTypeString = string.Format("{0}室{1}厅{2}卫{3}厨", list[i].Room, list[i].Hall, list[i].Toilet, list[i].Kitchen);
                    list[i].BuildingAreaString = string.Format("{0:N0}", list[i].BuildingArea);
                    list[i].TotalPriceString = string.Format("{0:N0}", list[i].TotalPrice);
                    list[i].DiscountString = list[i].Discount == -1 ? "" : Convert.ToString(list[i].Discount);
                    // list[i].AfterDiscountPriceString = (list[i].Discount == -1 ? string.Format("{0:N2}", list[i].TotalPrice) : string.Format("{0:N2}", (Convert.ToDecimal(list[i].Discount/10.0)*list[i].TotalPrice)));
                }
            }

            return list;
        }

        /// <summary>
        /// 更新折扣活动
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public bool UpdateDiscount_Admin(PVE_NH_Discount discount)
        {
            var count = _discountDal.UpdateDiscount_Admin_UpdateActivities(discount);
            if (0 < count)
            {
                count = _discountDal.UpdateDiscount_Admin_UpdateActivityHouse(discount);
                return 0 < count;
            }
            return false;
        }

        #endregion

        #region 详情

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PVE_NH_Discount_Detail GetDiscountDetail(int id)
        {
            var model = _discountDal.GetDiscountDetail(id);
            if (null == model)
            {
                return null;
            }

            model.Houses = _discountDal.GetDiscountDetail_Houses(id);

            return model;
        }

        #endregion

        #region 用户报名列表

        /// <summary>
        /// 用户报名列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildingId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<PVM_NH_Discount_UserReport> GetDiscountUserReports(int id, int buildingId, int unitId)
        {
            var list = _discountDal.GetDiscountUserReports(id, buildingId, unitId);

            if (null != list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].BidTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", list[i].BidTime);
                    list[i].HouseInfo = list[i].PremiseName + " " + list[i].BuildingName + " " + list[i].UnitName + " " + (0 < list[i].Floor ? list[i].Floor + "层" : "地下" + list[i].Floor + "层") + " " + list[i].HouseName;
                }
            }
            return list;
        }

        #endregion
    }
}