using System.Collections.Generic;
using System.Text;
using TXCommons.Admins;
using TXModel.AdminPVM;
using TXOrm;

namespace TXBll.HouseData
{
    /// <summary>
    /// 楼盘 管理平台
    /// </summary>
    public partial class BuildingBll
    {
        /// <summary>
        /// 根据搜索条件获取楼盘列表信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Building> GetPageList_BySearch_Admin(PVS_NH_Building search, int pageIndex, int pageSize)
        {
            var list = _buildingDal.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var tmp = list[i];
                    tmp.BuildingTypeString = AdminTypes.Db_NewHouse.Tb_Premises.Fc_ById.For_BuildingType(tmp.BuildingType);
                    tmp.RenovationString = AdminTypes.Db_NewHouse.Tb_Building.Fc_ById.For_Renovation(tmp.Renovation);
                    tmp.PropertyTypeString = AdminTypes.Db_NewHouse.Tb_Building.Fc_ById.For_PropertyTypes(tmp.PropertyType);
                    tmp.StateString = AdminTypes.Db_NewHouse.Tb_Building.Fc_ById.For_State(tmp.State);
                }

                var ids = GetBuildingIdsByList(list);
                var pairs = new HouseBll().GetHouseCountByBuildingId(ids);
                if (null != pairs)
                {
                    FillHouseCountForBuildingList(list, pairs);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据列表获取楼栋编号集合
        /// author: liyuzhao
        /// </summary>
        /// <param name="buildings">列表</param>
        /// <returns></returns>
        private string GetBuildingIdsByList(IEnumerable<PVM_NH_Building> buildings)
        {
            var ids = new StringBuilder();
            foreach (var b in buildings)
            {
                ids.AppendFormat("{0},", b.Id);
            }

            return ids.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 使用房源数量列表数据填充楼栋列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="buildings"></param>
        /// <param name="houseCounts"></param>
        private void FillHouseCountForBuildingList(List<PVM_NH_Building> buildings, List<PVS_NH_ForHouseCount> houseCounts)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                var b = buildings[i];
                for (int j = 0; j < houseCounts.Count; j++)
                {
                    var count = houseCounts[j];
                    if (b.Id == count.BuildingId)
                    {
                        b.HouseCount = count.HouseCount;
                    }
                }
            }
        }

        /// <summary>
        /// 根据搜索条件获取楼盘列表信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Admin(PVS_NH_Building search)
        {
            return _buildingDal.GetTotalCount_BySearch_Admin(search);
        }

        /// <summary>
        /// 添加新楼栋
        /// author: liyuzhao
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int AddNewBuilding(PVE_NH_Building building)
        {
            return _buildingDal.AddNewBuilding(building);
        }

        /// <summary>
        /// 更新楼栋信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int UpdateNewBuilding(PVE_NH_Building building)
        {
            return _buildingDal.UpdateNewBuilding(building);
        }

        /// <summary>
        /// 更新楼栋信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="building"></param>
        /// <param name="updateHouseState">更新房源状态目标值</param>
        /// <returns></returns>
        public int UpdateNewBuildingAndUpdateHouseState(PVE_NH_Building building, int updateHouseState)
        {
            return _buildingDal.UpdateNewBuildingAndUpdateHouseState(building, updateHouseState);
        }

        /// <summary>
        /// 删除楼栋
        /// author: liyuzhao
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public int DelNewBuilding(int buildingId)
        {
            return _buildingDal.DelNewBuilding(buildingId);
        }

        /// <summary>
        /// 根据楼盘编号获取楼栋编号
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<Building> GetBuildingListByPremisesId(int premisesId)
        {
            return _buildingDal.GetBuildingListByPremisesId(premisesId);
        }

        /// <summary>
        /// 根据楼栋编号获取单元列表
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<Unit> GetUnitsByBuildingId(int buildingId)
        {
            return _buildingDal.GetUnitsByBuildingId(buildingId);
        }

        /// <summary>
        /// 根基编号和名称查询在同一楼盘下同名楼栋数量
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int GetBuildingNameCountByIdAndName(PVE_NH_Building building)
        {
            return _buildingDal.GetBuildingNameCountByIdAndName(building);
        }

        /// <summary>
        /// 根基编号和名称查询在同一楼盘下同名楼栋数量(更新时使用)
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int GetBuildingNameCountByIdAndName_Update(PVE_NH_Building building)
        {
            return _buildingDal.GetBuildingNameCountByIdAndName_Update(building);
        }
    }
}