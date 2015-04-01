using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 14:14:23
    /// 描述：出售商品实体类
    /// </summary>
    public class SaleGoods
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

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
        /// 出售商品标题
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        /// <summary>
        /// 新旧程度
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// 出售价格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// GUID唯一标识
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 显示状态：0 未知，1上架，2下架
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审核状态：0未知，1待审核，2 通过，3未通过
        /// </summary>
        public int State { get; set; }

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

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Contact { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ContactPhone { get; set; }

        /// <summary>
        /// 是否不讲价(0讲价1,不讲价)
        /// </summary>
        public bool IsBargain { get; set; }

        /// <summary>
        /// 标签（格式：标签Id, 标签Id, 标签Id）
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Tag { get; set; }

        /// <summary>
        /// 下架原因
        /// </summary>
        public string ShelfReason { get; set; }
    }
}
