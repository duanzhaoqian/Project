using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.SaleGoodses
{
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/25 15:55:56
    /// 描述：
    /// </summary>
    public class SaleGoodsEntity
    {
        public SaleGoods SaleGoods { get; set; }
        public SaleOther SaleOther { get; set; }
    }

    /// <summary>
    /// Author:ningjd
    /// Date:2014-05-23
    /// Desc:出售商品管理列表Entity
    /// </summary>
    public class SaleGoodsIndexEntity
    {
        /// <summary>
        /// 商品ID(编号)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户账户
        /// </summary>
        public string UserLoginName { get; set; }

        /// <summary>
        /// 信息名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 出售价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 下架原因
        /// </summary>
        public string ShelfReason { get; set; }
    }

    /// <summary>
    /// Author:ningjd
    /// Date:2014-05-29
    /// Desc:闲置商品区域Enum
    /// </summary>
    public enum SaleGoodsAreaEnum
    {
        /// <summary>
        /// 管理信息
        /// </summary>
        Manage = 2,

        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 1,

        /// <summary>
        /// 违规信息
        /// </summary>
        ViolationGoods = 3
    }
}
