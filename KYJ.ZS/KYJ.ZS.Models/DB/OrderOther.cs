using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-5-27
    /// 订单扩展表
    /// </summary>
    public class OrderOther
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PaidTime { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime DeliveryTime { get; set; } 
        /// <summary>
        /// 收获时间
        /// </summary>
       public DateTime HarvestTime { get; set; }
        /// <summary>
        /// 发送起租协议时间
        /// </summary>
       public DateTime SendsGreementTime { get; set; }
        /// <summary>
        /// 起租时间
        /// </summary>
        public DateTime AccrueTime { get; set; }
        /// <summary>
        /// 待续租时间
        /// </summary>
        public DateTime RenewTime { get; set; }
        /// <summary>
        /// 同意退租时间
        /// </summary>
        public DateTime SurrenderTime { get; set; }
        /// <summary>
        /// 返回订单状态
        /// </summary>
        public int OrderState { get; set; }
        /// <summary>
        /// 赔损金额
        /// </summary>
        public decimal LosePrice { get; set; }
        /// <summary>
        /// 驳回理由
        /// </summary>
        public string Rejectreason { get; set; }




    }
}
