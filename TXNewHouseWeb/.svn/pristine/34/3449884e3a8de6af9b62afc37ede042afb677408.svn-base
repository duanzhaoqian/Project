using System.Collections.Generic;
using System.Text;
using TXModel.AdminPVM;
using TXModel.Geography;
using TXOrm;
using System;


namespace TXBll.HouseData
{
    /// <summary>
    /// 楼盘BLL类-管理后台
    /// </summary>
    public partial class PremisesBll
    {

        #region 楼盘信息列表

        /// <summary>
        /// 楼盘信息列表
        /// Author:wangzhikun
        /// </summary>
        /// <param name="premises">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_Premises> GetPremisesListByPages(PVS_NH_Premises premises, int pageIndex, int pageSize)
        {
            var list = _premisesDal.GetPremisesListByPages(premises, pageIndex, pageSize, false);
            if (list != null && 0 < list.Count)
            {
                foreach (var item in list)
                {
                    item.PropertyTypeString = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Premises.Fc_ById.For_PropertyTypes(item.PropertyType);
                    item.SalesStatusString = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Premises.Fc_ById.For_SalesStatus(item.SalesStatus);
                }
            }
            return list;

        }

        #endregion

        #region 楼盘信息总记录数

        /// <summary>
        /// 楼盘信息总记录数
        /// Author:wangzhikun
        /// </summary>
        /// <param name="premises">实体</param>
        /// <returns></returns>
        public int GetPremisesListByPageCounts(PVS_NH_Premises premises)
        {
            return _premisesDal.GetPremisesListByPageCounts(premises, false);
        }

        #endregion
        #region 没有关联开发商的楼盘
        /// <summary>
        /// 没有关联开发商楼盘信息列表
        /// Author:wangzhikun
        /// </summary>
        /// <param name="premises">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_Premises> GetPremisesListByPagesNodevel(PVS_NH_Premises premises, int pageIndex, int pageSize)
        {
            var list = _premisesDal.GetPremisesListByPages(premises, pageIndex, pageSize, true);
            if (list != null && 0 < list.Count)
            {
                foreach (var item in list)
                {
                    item.PropertyTypeString = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Premises.Fc_ById.For_PropertyTypes(item.PropertyType);
                    item.SalesStatusString = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Premises.Fc_ById.For_SalesStatus(item.SalesStatus);
                }
            }
            return list;

        }
        /// <summary>
        /// 没有关联开发商楼盘信息总记录数
        /// Author:wangzhikun
        /// </summary>
        /// <param name="premises">实体</param>
        /// <returns></returns>
        public int GetPremisesListByPageNodevelCounts(PVS_NH_Premises premises)
        {
            return _premisesDal.GetPremisesListByPageCounts(premises, true);
        }
        #endregion


        #region 获取所有楼盘列表(下拉列表)

        /// <summary>
        /// 获取所有楼盘列表(下拉列表)
        /// author:liyuzhao
        /// </summary>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPremisesForSelectItems()
        {
            return _premisesDal.GetPremisesForSelectItems();
        }

        /// <summary>
        /// 根据省、市、开发商编号获取所有楼盘列表(下拉列表)
        /// author:liyuzhao
        /// </summary>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPremisesByProvinceOrCityOrDeveloperIdForSelectItems(int provinceId, int cityId, int developerId)
        {
            return _premisesDal.GetPremisesByProvinceOrCityOrDeveloperIdForSelectItems(provinceId, cityId, developerId);
        }

        #endregion

        #region 获取实体

        /// <summary>
        /// 获取实体
        /// author: wangzhikun
        /// </summary>
        /// <param name="id">楼盘Id</param>
        /// <returns></returns>
        public PVM_NH_Premises GetPVM_PremisesById(int id)
        {
            var model = _premisesDal.GetPVM_PremisesById(id);
            if (null != model)
            {
                model.SalePermitList = new PermitPresaleBll().GetPermitPresalesByPremisesId(id);
            }
            return model;
        }

        #endregion

        #region 根据城市Id获取楼盘名称

        /// <summary>
        ///  根据城市Id获取楼盘名称
        /// </summary>
        /// <param name="provinceId">省份Id</param> 
        /// <param name="cityId">城市Id</param> 
        /// <returns></returns>
        public List<string> GetPremiseNameById(int provinceId, int cityId)
        {
            return _premisesDal.GetPremiseNameById(provinceId, cityId);
        }

        #endregion

        #region 获取指定城市的基本信息
        /// <summary>
        /// 获取指定城市的基本信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public Area Z_GetAreaByCityID(int cityId)
        {
            return _premisesDal.Z_GetAreaByCityID(cityId);
        }
        #endregion


        #region 获取指定城市的地铁线列表

        /// <summary>
        /// 获取指定城市的地铁线 列表
        /// </summary>
        /// <param name="cityId">城市Id</param>
        /// <param name="tid">父id</param>
        /// <returns></returns>
        public List<Z_GeographyItem> Z_GetTrafficsByCityIDAndTId(int cityId, int tid)
        {
            return _premisesDal.Z_GetTrafficsByCityIDAndTId(cityId, tid);
        }

        #endregion

        #region 页面获取指定地铁线路下面的每一个站

        /// <summary>
        /// 页面获取指定地铁线路下面的每一个站
        /// </summary>
        /// <param name="tid">地铁线ID</param>
        /// <returns></returns>
        public List<Z_GeographyItem> Z_GetTrafficsByID(int tid)
        {
            return _premisesDal.Z_GetTrafficsByID(tid);
        }

        #endregion

        #region 根据省市，关键字获取楼盘列表

        /// 根据城市或者关键字获取JSON格式楼盘
        /// </summary>
        /// <returns></returns>
        public string GetJsonPremises_ByCityIDORKeywords(int cityID, string keywords)
        {
            var list = _premisesDal.GetPremises_ByCityIDOrKeywords(cityID, keywords);

            if (null == list || 0 == list.Count)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append("{\"id\":\"" + list[i].ID + "\"");
                builder.Append(",\"name\":\"" + list[i].Name + "\"}");
                if (i < list.Count - 1)
                {
                    builder.Append(",");
                }
            }
            builder.Append("]");
            return builder.ToString();
        }

        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="sandTables"></param>
        /// <returns></returns>
        public int AddNew(PVE_NH_Premises2 premises, List<SandTableLabel> sandTables)
        {
            var subways = new PremisesBll().GetSubwaysByIds(premises.TId);
            return _premisesDal.AddNew(premises, subways, sandTables);
        }

        /// <summary>
        /// 根据楼盘信息获取同地域同名数量
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public int GetNameCountByPremises(PVE_NH_Premises2 premises)
        {
            return _premisesDal.GetNameCountByPremises(premises);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="subways"></param>
        /// <param name="sandTables"></param>
        /// <returns></returns>
        public int UpdatePremises(PVE_NH_Premises2 premises, List<SandTableLabel> sandTables)
        {
            var subways = new PremisesBll().GetSubwaysByIds(premises.TId);
            return _premisesDal.UpdatePremises(premises, subways, sandTables);
        }

        public int DelPremise(int pid)
        {
            return _premisesDal.DelPremises(pid);
        }

        /// <summary>
        /// 更新（状态变更）
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="sandTables"></param>
        /// <param name="buildingState"></param>
        /// <param name="houseState"></param>
        /// <returns></returns>
        public int UpdatePremises_StateChanged(PVE_NH_Premises2 premises, List<SandTableLabel> sandTables, int buildingState, int houseState)
        {
            var subways = new PremisesBll().GetSubwaysByIds(premises.TId);
            return _premisesDal.UpdatePremises_StateChanged(premises, subways, sandTables, buildingState, houseState);
        }

        /// <summary>
        /// 根据Id集获取地铁
        /// </summary>
        /// <param name="ids">逗号分隔</param>
        /// <returns></returns>
        public List<Traffic> GetSubwaysByIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return null;
            }

            return _premisesDal.GetSubwaysByIds(ids);
        }

        /// <summary>
        /// 根据楼盘编号获取预售许可证列表
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<PVS_NH_Premises_SalePermit> GetSalePermitByPremisesId(int premisesId)
        {
            return _premisesDal.GetSalePermitByPremisesId(premisesId);
        }

        /// <summary>
        /// 根据楼盘编号获取地铁信息列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<PremisesSubway> GetSubwaysByPremisesId(int premisesId)
        {
            return _premisesDal.GetSubwaysByPremisesId(premisesId);
        }

        /// <summary>
        /// 根据楼盘编号获取地铁信息列表(JSON格式)
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public string GetSubwaysJsonStringByPremisesId(int premisesId)
        {
            var list = _premisesDal.GetSubwaysByPremisesId(premisesId);

            if (null == list || 0 == list.Count)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append("{\"id\":\"" + list[i].Tid + "\"");
                builder.Append(",\"name\":\"" + list[i].TName + "\"}");
                if (i < list.Count - 1)
                {
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }

        /// <summary>


        /// <summary>
        /// 根据图片关联Id获取楼盘信息(使用BuildingId承载CityId)
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public List<House> GetHousesByRId(int rid)
        {
            return _premisesDal.GetHousesByRId(rid);
        }

        /// <summary>
        /// 在编辑楼盘信息时清空当前楼盘下所有楼栋的沙盘图标记信息
        /// </summary>
        /// <param name="premisesId"></param>
        public void UpdateBuildingPNumForUpdateSandBox(int premisesId)
        {
            _premisesDal.UpdateBuildingPNumForUpdateSandBox(premisesId);
        }

        /// <summary>
        /// 根据活动编号获取楼盘信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public Premis GetEntity_ByActvityId(int activityId)
        {
            return _premisesDal.GetEntity_ByActvityId(activityId);
        }
        /// <summary>
        /// 关联没有开放商的楼盘
        /// </summary>
        /// <param name="develid"></param>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public int UpdatePremisesDevelID(int develid, string premisesid)
        {
            return _premisesDal.UpdatePremisesDevelID(develid, premisesid);
        }
    }
}