using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OverSeaPremisesSearchModel
    {
        private string id;
        /// <summary>
        /// 楼盘ID
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string countryId;
        /// <summary>
        /// 国家ID
        /// </summary>
        public string CountryId
        {
            get { return countryId; }
            set { countryId = value; }
        }

        private string countryName;
        /// <summary>
        /// 国家名称
        /// </summary>
        public string CountryName
        {
            get { return countryName; }
            set { countryName = value; }
        }

        private string countryStateId;
        /// <summary>
        /// 州ID
        /// </summary>
        public string CountryStateId
        {
            get { return countryStateId; }
            set { countryStateId = value; }
        }

        private string countryStateName;
        /// <summary>
        /// 州名称
        /// </summary>
        public string CountryStateName
        {
            get { return countryStateName; }
            set { countryStateName = value; }
        }

        private string cityId;
        /// <summary>
        /// 城市ID
        /// </summary>
        public string CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }

        private string cityName;
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        private string cityNameEn;
        /// <summary>
        /// 城市名称En
        /// </summary>
        public string CityNameEn
        {
            get { return cityNameEn; }
            set { cityNameEn = value; }
        }

        private string name;
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string premisesAddress;
        /// <summary>
        /// 楼盘地址
        /// </summary>
        public string PremisesAddress
        {
            get { return premisesAddress; }
            set { premisesAddress = value; }
        }

        private string propertyType;
        /// <summary>
        /// 物业类型
        /// </summary>
        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public string PageIndex
        {
            get
            {
                if (string.IsNullOrEmpty(_pageIndex))
                {
                    PageIndex = "1";
                }
                return _pageIndex;
            }
            set { _pageIndex = value; }
        }
        /// <summary>
        /// 每页条数
        /// </summary>
        public string PageSize
        {
            get
            {
                if (string.IsNullOrEmpty(_pageSize))
                {
                    PageSize = "10";
                }
                return _pageSize;
            }
            set { _pageSize = value; }
        }

        private string _pageIndex;
        private string _pageSize;
        /// <summary>
        /// 价格范围起始
        /// </summary>
        public string PriceBegin { get; set; }
        /// <summary>
        /// 价格范围结束
        /// </summary>
        public string PriceEnd { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortKey { get; set; }
    }
}
