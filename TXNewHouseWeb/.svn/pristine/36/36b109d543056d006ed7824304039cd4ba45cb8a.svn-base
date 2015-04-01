
using System.Collections.Generic;
using TXModel.AdminPVM;
using TXOrm;

namespace TXBll.HouseData
{
    /// <summary>
    /// 房源 网站管理平台
    /// </summary>
    public partial class HouseBll
    {
        #region 根据搜索条件获取房源表信息
        /// <summary>
        /// 根据搜索条件获取房源表信息
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_House> GetPageList_BySearch_Admin(PVS_NH_House search, int pageIndex, int pageSize)
        {
            var list = _houseDal.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            foreach (PVM_NH_House m in list)
            {
                m.SalesStatusStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(m.SalesStatus);
                m.PriceTypeStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_PriceType(m.PriceType);
            }
            return list;
        }
        #endregion

        #region 根据搜索条件获取房源表信息记录数
        /// <summary>
        /// 根据搜索条件获取房源表信息记录数
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_House search)
        {
            return _houseDal.GetPageCount_BySearch_Admin(search);
        }
        #endregion

        #region 更新房源信息

        /// <summary>
        /// 更新房源信息
        /// </summary>
        /// <param name="entity">房源实体</param>
        /// <returns>返回：影响行数</returns>
        public int UpdateNHouse_Admin(PVM_NH_House entity)
        {
            return _houseDal.UpdateNHouse_Admin(entity);
        }
        #endregion

        #region 根据楼栋编号集获取房源数量

        /// <summary>
        /// 根据楼栋编号集获取房源数量
        /// author: liyuzhao
        /// </summary>
        /// <param name="buildingIds">楼栋编号集(逗号分隔)</param>
        /// <returns></returns>
        public List<PVS_NH_ForHouseCount> GetHouseCountByBuildingId(string buildingIds)
        {
            if (string.IsNullOrEmpty(buildingIds))
            {
                return null;
            }

            return _houseDal.GetHouseCountByBuildingId(buildingIds);
        }

        #endregion

        #region 删除多个房源
        /// <summary>
        /// 删除多个房源
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        public int DeleteHouseByIds(string ids)
        {
            return _houseDal.DeleteHouseByIds(ids);
        }
        #endregion

        #region 更新多个房源销售状态

        /// <summary>
        /// 更新多个房源销售状态
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <param name="state">销售状态</param>
        /// <returns></returns>
        public int UpdateHouseSalesStatusByIds(string ids, int state)
        {
            return _houseDal.UpdateHouseSalesStatusByIds(ids, state);
        }
        #endregion

        #region 添加房源

        /// <summary>
        /// 添加房源
        /// </summary>
        /// <param name="model">房源实体</param>
        /// <returns>返回：新添加房源编号</returns>
        public int AddHouse_Admin(House model)
        {
            return _houseDal.AddHouse_Admin(model);
        }
        #endregion

        #region 检测房源集不可更新的房源

        /// <summary>
        /// 检测房源集不可更新的房源
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        public List<HouseIdAndName> CheckNotUpdateHouses(string ids)
        {
            return _houseDal.CheckNotUpdateHouses(ids);
        }
        #endregion

        #region 根据房源id获取城市id

        /// <summary>
        /// 根据房源id获取城市id
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public int GetCityIdByHouseId(int hid)
        {
            return _houseDal.GetCityIdByHouseId(hid);
        }
        #endregion

        #region 检查房源名称重复

        /// <summary>
        /// 检查房源名称重复
        /// author: liyuzhao
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public int GetHouseNameCountByIdAndName(House house)
        {
            return _houseDal.GetHouseNameCountByIdAndName(house);
        }

        /// <summary>
        /// 检查房源名称重复(更新信息)
        /// author: liyuzhao
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public int GetHouseNameCountByIdAndName_Update(House house)
        {
            return _houseDal.GetHouseNameCountByIdAndName_Update(house);
        }

        #endregion

        #region 房源详情

        /// <summary>
        /// 房源详情
        /// author: liyuzhao
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public PVE_NH_House_Detail GetHouseDetailByHouseId(int houseId)
        {
            var list = _houseDal.GetHouseDetailByHouseId(houseId);
            if (null != list)
            {
                list.FloorName = (0 < list.Floor ? string.Format("{0}F", list.Floor) : string.Format("B{0}", list.Floor));
                list.HouseTypeStructure = string.Format("{0}室{1}厅{2}卫{3}厨", list.Room, list.Hall, list.Toilet, list.Kitchen);
                list.BuildingTypeName = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_BuildingType(list.BuildingType);
                list.OrientationName = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_Orientation(list.Orientation);
                list.PriceTypeName = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_PriceType(list.PriceType);
                list.TotalPriceString = string.Format("{0:N0}", list.TotalPrice);
                list.SinglePriceString = string.Format("{0:N0}", list.SinglePrice);
                list.DownPaymentString = string.Format("{0:N0}", list.DownPayment);
                list.SalesStatusName = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(list.SalesStatus);
                list.BuildingAreaString = string.Format("{0:N0}", list.BuildingArea);
                list.AreaString = string.Format("{0:N0}", list.Area);
            }
            return list;
        }

        #endregion

        /// <summary>
        /// 根据删除的户型图编号重置房源表->户型图关联
        /// </summary>
        /// <param name="id"></param>
        public void ResetHouseRIdByDelPicId(int id)
        {
            _houseDal.ResetHouseRIdByDelPicId(id);
        }

        /// <summary>
        /// 根据楼栋编号获取房源编号列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<int> GetHouseIdListByBuildingId(int id)
        {
            return _houseDal.GetHouseIdListByBuildingId(id);
        }
    }
}