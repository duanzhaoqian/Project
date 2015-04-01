using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-28
    /// 描述：广告实体类
    /// </summary>
    public class Advert
    {
        /// <summary>
        /// 广告ID
        /// </summary>
        public int AdvertId { get; set; }
        /// <summary>
        /// 广告类型ID
        /// </summary>
        public int AdvertTypeId { get; set; }
        /// <summary>
        /// 广告位置ID
        /// </summary>
        public int AdvertLocationId { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 广告唯一标识
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 广告说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string MerchantName { get; set; }
        /// <summary>
        /// 广告价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 广告地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 状态(0-未知，1-上架，2-下架)
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// 广告核审状态（0未知1未审核2审核中（初审）3审核中（审核未通过）4审核中（复审）5审核通过（待上线，已上线，已下线））
        /// </summary>
        public int AduitState { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 核审时间
        /// </summary>
        public DateTime AduitTime { get; set; }
        /// <summary>
        /// 核审说明
        /// </summary>
        public string AduitRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int Isdel { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; }

    }
    /// <summary>
    /// 广告排序搜索
    /// </summary>
    public class AdvertsSearchEntity
    {
        /// <summary>
        /// 名称查找
        /// </summary>
        public string NameSearch { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 状态（1-未核审2-核审中3-未上线4-已上线5-已下线）
        /// </summary>
        public int State { get; set; }
    }

    /// <summary>
    /// 广告（页面）类型
    /// </summary>
    public class AdvertTypes
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public int AdvertTypeID { get; set; }
        /// <summary>
        /// 广告类型名称
        /// </summary>
        public string  AdvertTypeName { get; set; }
        /// <summary>
        /// 是否是分类广告
        /// </summary>
        public bool AdvertTypeIsCategory { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int AdvertTypeSort { get; set; }
    }
}
