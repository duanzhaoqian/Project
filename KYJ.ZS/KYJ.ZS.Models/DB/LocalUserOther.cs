using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author:zhuzh
    /// Time:2014-4-17
    /// Desc:用户关联数据实体
    /// </summary>
    public class LocalUserOther
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 居住省份ID
        /// </summary>
        public int LiveProvinceId { get; set; }
        /// <summary>
        /// 居住省份名称
        /// </summary>
        public string LiveProvinceName { get; set; }
        /// <summary>
        /// 居住城市ID
        /// </summary>
        public int LiveCityId { get; set; }
        /// <summary>
        /// 居住城市名称
        /// </summary>
        public string LiveCityName { get; set; }
        /// <summary>
        /// 居住城市区域ID
        /// </summary>
        public int LiveDistrictId { get; set; }
        /// <summary>
        /// 居住城市区域名称
        /// </summary>
        public string LiveDistrictName { get; set; }
        /// <summary>
        /// 家乡省份ID
        /// </summary>
        public int HomeProvinceId { get; set; }
        /// <summary>
        /// 家乡省份名称
        /// </summary>
        public string HomeProvinceName { get; set; }
        /// <summary>
        /// 家乡城市ID
        /// </summary>
        public int HomeCityId { get; set; }
        /// <summary>
        /// 家乡城市名称
        /// </summary>
        public string HomeCityName { get; set; }
        /// <summary>
        /// 家乡城市区域ID
        /// </summary>
        public int HomeDistrictId { get; set; }
        /// <summary>
        /// 家乡城市区域名称
        /// </summary>
        public string HomeDistrictName { get; set; }

        public string College { get; set; }
    }
}
