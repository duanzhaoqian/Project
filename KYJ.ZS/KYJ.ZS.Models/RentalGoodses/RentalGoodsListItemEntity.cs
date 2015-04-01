using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-4-30
    /// 列表单个商品对象
    /// </summary>
    public class RentalGoodsListItemEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 市场价格
        /// </summary>
        public Double Price { get; set; }

        /// <summary>
        /// 月租金
        /// </summary>
        public Double MonthPrice { get; set; }

        /// <summary>
        /// 类型：租，售
        /// </summary>
        public int GoodsType { get; set; }
        /// <summary>
        /// 图片URL
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public string CategoryId { get; set; }
    }
}
