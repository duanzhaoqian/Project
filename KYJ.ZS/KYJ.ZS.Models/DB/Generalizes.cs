using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-28
    /// 描述：商品排序管理
    /// </summary>
   public class Generalize
    {
       /// <summary>
       /// 商品推广ID
       /// </summary>
        public int GeneralizeId { get; set; }
       /// <summary>
       /// 推广类型ID
       /// </summary>
        public int GeneralizeTypeId { get; set; }
       /// <summary>
       /// 推广位置ID
       /// </summary>
        public int GeneralizeLocationId { get; set; }
       /// <summary>
       /// 分类ID
       /// </summary>
        public int CategoryId { get; set; }
       /// <summary>
       /// 推广名称
       /// </summary>
        public string Name { get; set; }
       /// <summary>
       /// 推广说明
       /// </summary>
        public string Remark { get; set; }
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
        /// 推广核审状态（0未知1未审核2审核中（初审）3审核中（审核未通过）4审核中（复审）5审核通过（待上线，已上线，已下线））
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
   /// 推广排序搜索
   /// </summary>
   public class GeneralizeSearchEntity
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
       public int State { get;set;}
   }
    /// <summary>
    /// 排序状态
    /// </summary>
    public enum GeneralizeSate
    {
        /// <summary>
        /// 未核审
        /// </summary>
        NoAduit = 1,
        /// <summary>
        /// 核审中
        /// </summary>
        Aduit = 2,
        /// <summary>
        /// 待上线
        /// </summary>
        NoLine = 3,
        /// <summary>
        /// 已上线
        /// </summary>
        OnLine = 4,
        /// <summary>
        /// 已下线
        /// </summary>
        DownLine = 5

    }
    /// <summary>
    /// 推广（页面）类型
    /// </summary>
    public class GeneralizeTypes
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public int GeneralizeTypeID { get; set; }
        /// <summary>
        /// 推广类型名称
        /// </summary>
        public string GeneralizeTypeName { get; set; }
        /// <summary>
        /// 是否是分类推广
        /// </summary>
        public bool GeneralizeIsCategory { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int GeneralizeTypeSort { get; set; }
    }
}
