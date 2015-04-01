using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-4-17
    /// Desc:收货地址实体类
    /// </summary>
    public class DeliveryAddress
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 省份ID
        /// </summary>
        [Required]
        public int ProvinceId { get; set; }
        /// <summary>
        /// 省份名称
        /// </summary>
        [Required]
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        [Required]
        public int CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        [Required]
        public string CityName { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        [Required]
        public int DistrictId { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [Required]
        public string DistrictName { get; set; }
        /// <summary>
        /// 收货详细地址
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [Required]
        [RegularExpression(@"[0-9]{6}")]
        public string Code { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string RealName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [Required]
        [StringLength(50)]
        [RegularExpression(@"((13[0-9]|15[012356789]|18[0-9]|14[57])[0-9]{8})|((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))")]
        public string Tel { get; set; }
        /// <summary>
        /// 备用联系电话
        /// </summary>
        [StringLength(50)]
        [RegularExpression(@"((13[0-9]|15[012356789]|18[0-9]|14[57])[0-9]{8})|((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))")]
        public string ResTel { get; set; }
        /// <summary>
        /// 是否是默认收货地址
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
