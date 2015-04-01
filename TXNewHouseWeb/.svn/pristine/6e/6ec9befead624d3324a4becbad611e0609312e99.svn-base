using TXDal.NHouseActivies;
using System.Collections.Generic;
using TXModel.Commons;
using TXModel.NHouseActivies.Discount;
using TXModel;
using TXModel.NHouseActivies.Common;
using System;
using TXOrm;

namespace TXBll.NHouseActivies
{
    /// <summary>
    /// 活动公用类
    /// </summary>
    public class ActiviesUtilsBll
    {
        private readonly ActiviesUtilsDal _activiesUtilsDal = new ActiviesUtilsDal();

        /// <summary>
        /// 根据楼盘编号获取正在参与活动的房源数量
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">楼盘编号</param>
        /// <returns>如果执行错误返回： -1</returns>
        public int GetLivingHouseCountByPremisesId(int id)
        {
            return _activiesUtilsDal.GetLivingHouseCountByPremisesId(id);
        }

        /// <summary>
        /// 根据楼栋编号获取正在参与活动的房源数量
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">楼栋编号</param>
        /// <returns>如果执行错误返回： -1</returns>
        public int GetLivingHouseCountByBuildingId(int id)
        {
            return _activiesUtilsDal.GetLivingHouseCountByBuildingId(id);
        }


        /// <summary>
        /// 查询楼盘
        /// author: 李刚
        /// </summary>
        /// <param name="developerId">楼盘Id</param>
        /// <returns></returns>
        public List<PremiseModel> GetPremise(int developerId, int salesStatus)
        {
            return _activiesUtilsDal.GetPremise(developerId, salesStatus);
        }
        public List<PremiseModel> GetPremise(int developerId)
        {
            return _activiesUtilsDal.GetPremise(developerId);
        }
        /// <summary>
        /// 查询楼栋
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <returns></returns>
        public List<BuildingModel> GetBuilding(int premisesId)
        {
            return _activiesUtilsDal.GetBuilding(premisesId);
        }

        /// <summary>
        /// 查新单元
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <returns></returns>
        public List<UnitModel> GetUnit(int premisesId, int buildingId)
        {
            return _activiesUtilsDal.GetUnit(premisesId, buildingId);
        }
        /// <summary>
        /// 查询楼层
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <returns></returns>
        public List<int> GetFloor(int premisesId, int buildingId)
        {
            return _activiesUtilsDal.GetFloor(premisesId, buildingId);
        }
        /// <summary>
        /// 查询楼层
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <returns></returns>
        public List<int> GetFloor(int premisesId, int buildingId, int unitId)
        {
            return _activiesUtilsDal.GetFloor(premisesId, buildingId, unitId);
        }
        /// <summary>
        /// 查询房源
        /// author: 李刚
        /// </summary>
        /// <param name="developerId">开发商ID</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <param name="floor">楼层Id</param>
        /// <param name="status">销售状态</param>
        /// <param name="paging">分页类</param>
        /// <returns></returns>
        public Paging<HouseInfoModel> GetHouse(int developerId, int premisesId, int buildingId, int unitId, int floor, int status, Paging<HouseInfoModel> paging)
        {
            return _activiesUtilsDal.GetHouse(developerId,premisesId,buildingId,unitId, floor,status, paging);
        }
        /// <summary>
        /// 查询活动
        /// author: 李刚
        /// </summary>
        /// <param name="paging">分页类</param>
        /// <param name="type">活动类型</param>
        /// <returns></returns>
        public Paging<CT_Activity> GetActivityPageList(Paging<CT_Activity> paging, int type)
        {
            return _activiesUtilsDal.GetActivityPageList(paging, type);
        }

        /// <summary>
        /// 查询活动
        /// author: 李刚
        /// </summary>
        /// <param name="paging">分页类</param>
        /// <param name="type">活动类型</param>
        /// <returns></returns>
        public Paging<CT_Activity> GetActivityPageList2(Paging<CT_Activity> paging, int type)
        {
            return _activiesUtilsDal.GetActivityPageList2(paging, type);
        }

        /// <summary>
        /// 查询活动房源列表
        /// 李刚
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <param name="floor">楼层</param>
        /// <param name="activState">状态</param>
        /// <param name="type">活动类型</param>
        /// <param name="paging">分页</param>
        /// <returns></returns>
        public Paging<ActivityHouse> GetActivityHouseList(int developerId, int premisesId, int buildingId, int unitId, int floor, int activState, int type, Paging<ActivityHouse> paging)
        {
            return _activiesUtilsDal.GetActivityHouseList(developerId, premisesId, buildingId, unitId, floor, activState, type, paging);
        }
        /// <summary>
        /// 创建活动
        /// 李刚
        /// </summary>
        /// <param name="activName">活动名称</param>
        /// <param name="bidPrice">起拍价格</param>
        /// <param name="addPrice">加价幅度</param>
        /// <param name="maxPrice">最大幅度</param>
        /// <param name="bond">活动保证金金额</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="ids">房源Id集合，以逗号分隔</param>
        public int CreateActivity(int cityId, int developerId, int type, DateTime? bStart, DateTime? bEnd, string activName, decimal bidPrice, decimal addPrice, decimal maxPrice, decimal bond, DateTime start, DateTime end, String ids, int activState)
        {
            return _activiesUtilsDal.CreateActivity(cityId,developerId, type, bStart, bEnd, activName, bidPrice, addPrice, maxPrice, bond, start, end, ids, activState);
        }

        /// <summary>
        /// 根据活动编号获取活动房源信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<ActivitiesHouse> GetActivitiesHousesByActivityId(int activityId)
        {
            return _activiesUtilsDal.GetActivitiesHousesByActivityId(activityId);
        }

        /// <summary>
        /// 获得单个活动详情
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public TXOrm.Activity GetActivityById(int activityId)
        {
            return _activiesUtilsDal.GetActivityById(activityId);
        }

        /// <summary>
        /// 根据活动编号获取活动房源信息
        /// author: maboxun
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<CT_ActivitiesHouse> GetActivitiesHousesById(int activityId)
        {
            return _activiesUtilsDal.GetActivitiesHousesById(activityId);
        }

        /// <summary>
        /// 获取阶梯团购折扣
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<TXOrm.Social> GetSocialByActivityId(int activityId)
        {
            return _activiesUtilsDal.GetSocialByActivityId(activityId);
        }
    }
}