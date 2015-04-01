using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Orders
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-05-06
    /// 描述：订单生成参数
    /// </summary>
    public class Web_OrderParameterEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品颜色Id
        /// </summary>
        public int GoodsColorId { get; set; }
        /// <summary>
        /// 商品规格Id
        /// </summary>
        public int GoodsNormId { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int GoodsNum { get; set; }
        /// <summary>
        /// 商品租期
        /// </summary>
        public int GoodsMonth { get; set; }
        /// <summary>
        /// 收货地址Id
        /// </summary>
        public int DeliveryId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 发票类型
        /// </summary>
        public int ShippingMethod { get; set; }
        /// <summary>
        ///  是否电话确认
        /// </summary>
        public bool IsPhoneConfirm { get; set; }

        /// <summary>
        /// 商品价格扩展
        /// </summary>
        public string GoodsOtherPrices { get; set; }
    }
}
