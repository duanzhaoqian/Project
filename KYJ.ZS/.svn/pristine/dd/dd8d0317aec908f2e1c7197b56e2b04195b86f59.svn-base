using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RentalGoodses
{
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/28 13:17:42
    /// 描述：
    /// </summary>
    public class RentalGoodsEntity
    {
        public RentalGoods RentalGoods { get; set; }
        public RentalOther RentalOther { get; set; }

        public List<GoodsAttribute> GoodsAttribute { get; set; }
        public List<GoodsColor> GoodsColor { get; set; }
        public List<GoodsNorm> GoodsNorm { get; set; }
        public List<GoodsPrice> GoodsPrice { get; set; }
    }

    /// <summary>
    /// Author:ningjd
    /// Date:2014-05-22
    /// Desc:出租商品管理列表Entity
    /// </summary>
    public class RentalGoodsIndexEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商家编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthPrice { get; set; }

        /// <summary>
        /// 上架状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 下架原因
        /// </summary>
        public string ShelfReason { get; set; }
    }

    /// <summary>
    /// Author:ningjd
    /// Date:2014-05-27
    /// Desc:出租商品区域Enum
    /// </summary>
    public enum RentalGoodsAreaEnum
    {
        /// <summary>
        /// 管理列表
        /// </summary>
        Manage=2,

        /// <summary>
        /// 待审核
        /// </summary>
        Pending=1,

        /// <summary>
        /// 违规商品
        /// </summary>
        ViolationGoods=3
    }
}
