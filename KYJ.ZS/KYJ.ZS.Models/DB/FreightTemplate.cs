using System;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-17
    /// Desc：运费模板
    /// </summary>
    public class FreightTemplate
    {

        public int Id { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 运费模版标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否包邮
        /// </summary>
        public bool IsFreeship { get; set; }

        /// <summary>
        /// 计价方式：0 未知，1 按件数，2 按重量，3 按体积
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 是否支持快递
        /// </summary>
        public bool IsExpress { get; set; }

        /// <summary>
        /// 是否支持EMS
        /// </summary>
        public bool IsEms { get; set; }

        /// <summary>
        /// 是否支持平邮
        /// </summary>
        public bool IsSnailmail { get; set; }

        /// <summary>
        /// 是否支持物流
        /// </summary>
        public bool IsLogistics { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>re
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
