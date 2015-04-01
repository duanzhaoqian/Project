using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Orders
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-06-24
    /// </summary>
    public class Services_OrderPPEntity
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        ///商品Id
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int OrderGoodsNum { get; set; }
        /// <summary>
        /// 商品价格扩展Id
        /// </summary>
        public int GoodsPriceId { get; set; }
    }
}
