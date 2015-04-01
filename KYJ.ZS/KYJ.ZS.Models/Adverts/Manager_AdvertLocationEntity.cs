using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Adverts
{
    public class Manager_AdvertLocationEntity
    {
        /// <summary>
        /// 广告位置ID
        /// </summary>
        public int AdvertId { get; set; }
        /// <summary>
        /// 广告类型ID
        /// </summary>
        public int AdvertTypeId { get; set; }
        /// <summary>
        /// 广告类型名称
        /// </summary>
        public string AdvertTypeName { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 分类名字
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 位置名称
        /// </summary>
        public string LocationName { get; set; }
    }

    /// <summary>
    /// 广告位置
    /// </summary>
    public class AdvertLocations
    {
        /// <summary>
        /// 广告类型ID
        /// </summary>
        public int AdvertTypeId { get; set; }
        /// <summary>
        /// 广告分类ID
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 广告位置ID
        /// </summary>
        public int GeographyCode { get; set; }
        /// <summary>
        /// 广告位置名称
        /// </summary>
        public string GeographyName { get; set; }
        /// <summary>
        /// 最大图片数量（排序时用到）
        /// </summary>
        public int AdvertLocationNum { get; set; }
    }
    /// <summary>
    /// 广告尺寸
    /// </summary>
    public class AdvertSize
    {
        /// <summary>
        /// Width
        /// </summary>
        public int AdvertWidth { get; set; }
        /// <summary>
        /// Height
        /// </summary>
        public int AdvertHeight { get; set; }
    }
}
