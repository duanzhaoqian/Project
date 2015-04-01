using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Common;

namespace KYJ.ZS.BLL.Geographies
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-05-08
    /// Desc:地理位置信息
    /// </summary>
    public class GeographyBll
    {
        private KYJ.ZS.DAL.Geographies.GeographyDal _dal = null;

        public GeographyBll()
        {
            if (_dal == null)
                _dal = new DAL.Geographies.GeographyDal();
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-08
        /// Desc:获取所有国家数据
        /// </summary>
        /// <returns>GeographyItemEntity数据实体枚举列表</returns>
        public List<GeographyItemEntity> GetCountries()
        {
            return _dal.GetCountries();
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-08
        /// Desc:根据国家ID获取所有省份
        /// </summary>
        /// <param name="countryID">国家ID</param>
        /// <returns>GeographyItemEntity数据实体枚举列表</returns>
        public List<GeographyItemEntity> GetProvinces(int countryID = 1)
        {
            return _dal.GetProvinces(countryID);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-08
        /// Desc:根据省份ID获取所有城市
        /// </summary>
        /// <param name="ProvinceID">省份Id</param>
        /// <returns>城市Id和名称列表</returns>
        public List<GeographyItemEntity> GetCities(int provinceID)
        {
            return _dal.GetCities(provinceID);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-08
        /// Desc:获取指定城市下的所有区域
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns>区域Id和名称列表</returns>
        public List<GeographyItemEntity> GetDistricts(int cityId)
        {
            return _dal.GetDistricts(cityId);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-08
        /// Desc:获取指定区域下的所有商圈
        /// </summary>
        /// <param name="districtId">商圈ID</param>
        /// <returns>商圈Id和名称列表</returns>
        public List<GeographyItemEntity> GetBussineses(int districtId)
        {

            return _dal.GetBussineses(districtId);
        }
    }
}
