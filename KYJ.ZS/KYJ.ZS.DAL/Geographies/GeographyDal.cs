using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Common;

namespace KYJ.ZS.DAL.Geographies
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-05-08
    /// Desc:地理位置信息
    /// </summary>
    public class GeographyDal
    {
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-08
        /// Desc:获取所有国家数据
        /// </summary>
        /// <returns>GeographyItemEntity数据实体枚举列表</returns>
        public List<GeographyItemEntity> GetCountries()
        {
            try
            {
                BaseDataWebService.BaseDataWebServiceSoapClient _web = new BaseDataWebService.BaseDataWebServiceSoapClient();

                var countries = _web.SearchCountryList();

                return countries != null ? countries.ConvertAll(it => new GeographyItemEntity() { GeographyCode = it.Id, GeographyName = it.Name }) : null;
            }
            catch (Exception)
            {
                throw;
            }
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
            try
            {
                BaseDataWebService.BaseDataWebServiceSoapClient _web = new BaseDataWebService.BaseDataWebServiceSoapClient();

                var provinces = _web.SearchProvinceList(countryID);

                return provinces != null ? provinces.ConvertAll(it => new GeographyItemEntity() { GeographyCode = it.Id, GeographyName = it.Name }) : null;
            }
            catch (Exception)
            {
                throw;
            }
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
            try
            {
                BaseDataWebService.BaseDataWebServiceSoapClient _web = new BaseDataWebService.BaseDataWebServiceSoapClient();

                var cities = _web.SearchCityList(provinceID);

                return cities != null ? cities.ConvertAll(it => new GeographyItemEntity() { GeographyCode = it.Id, GeographyName = it.Name }) : null;
            }
            catch (Exception)
            {
                throw;
            }
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
            try
            {
                BaseDataWebService.BaseDataWebServiceSoapClient _web = new BaseDataWebService.BaseDataWebServiceSoapClient();

                var districts = _web.SearchDistrictList(cityId);

                return districts != null ? districts.ConvertAll(it => new GeographyItemEntity() { GeographyCode = it.Id, GeographyName = it.Name }) : null;
            }
            catch (Exception)
            {
                throw;
            }
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
            try
            {
                BaseDataWebService.BaseDataWebServiceSoapClient _web = new BaseDataWebService.BaseDataWebServiceSoapClient();

                var businesses = _web.SearchBusinessList(districtId);

                return businesses != null ? businesses.ConvertAll(it => new GeographyItemEntity() { GeographyCode = it.Id, GeographyName = it.Name }) : null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
