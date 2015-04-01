using System.Collections.Generic;
using TXDal.WebSite;
using TXModel.Geography;

namespace TXBll.WebSite
{
    public class GeographyBll
    {
        private readonly GeographyDal _geographyDal = new GeographyDal();

        /// <summary>
        /// 获取所有省份
        /// </summary>
        /// <returns>省份Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetProvinces()
        {
            return _geographyDal.Z_GetProvinces();
        }

        /// <summary>
        ///  根据省份ID获取所有城市
        /// </summary>
        /// <param name="provinceID">省份Id</param>
        /// <returns>城市Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetCities(int provinceID)
        {
            return _geographyDal.Z_GetCities(provinceID);
        }

        /// <summary>
        /// 获取指定城市下的所有区域
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public List<Z_GeographyItem> Z_GetDistricts(int cityId)
        {
            return _geographyDal.Z_GetDistricts(cityId);
        }

        /// <summary>
        /// 获取指定区域下的所有商圈
        /// </summary>
        /// <param name="districtId">区域ID</param>
        /// <returns></returns>
        public List<Z_GeographyItem> Z_GetBussineses(int districtId)
        {
            return _geographyDal.Z_GetBussineses(districtId);
        }

        /// <summary>
        /// 根据城市Id查询线路列表信息
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public List<Z_GeographyItem> GetSubWayLinesByCityID(int cityId)
        {
            return _geographyDal.GetSubWayLinesByCityID(cityId);
        }

        /// <summary>
        /// 根据楼盘ID获取预售许可证列表
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPermitPresalesByPremisesId(int id)
        {
            return _geographyDal.GetPermitPresalesByPremisesId(id);
        }

        #region 级联数据

        /// <summary>
        ///  获取所有楼盘
        /// </summary>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetPremises()
        {
            return _geographyDal.Z_GetPremises();
        }

        #region 根据城市Id获取楼盘
        /// <summary>
        ///  根据城市Id获取楼盘
        /// </summary>
        /// <param name="cityId">楼城市Id</param> 
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetPremisesById(int cityId)
        {
            return _geographyDal.Z_GetPremisesById(cityId);
        }
        #endregion

        /// <summary>
        ///  根据楼盘ID获取楼栋
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetBuildings(int id)
        {
            return _geographyDal.Z_GetBuildings(id);
        }

        /// <summary>
        ///  根据楼盘ID获取楼栋 剔除不可发布房源的楼栋（3 建设中 4规划中）
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetBuildings_Find(int id)
        {
            return _geographyDal.Z_GetBuildings_Find(id);
        }

        /// <summary>
        ///  根据楼栋ID获取单元
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetUnits(int id)
        {
            return _geographyDal.Z_GetUnits(id);
        }

        /// <summary>
        ///  根据楼栋ID获取楼层列表
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetBuildingFloors(int id)
        {
            return _geographyDal.Z_GetBuildingFloors(id);
        }

        #endregion
    }
}