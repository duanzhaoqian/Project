using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Orders
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-24
    /// Desc：订单（发货管理）
    /// </summary>
    public class OrderEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string DeliveryTel { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string DeliveryCode { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string DeliveryRealName { get; set; }

        /// <summary>
        /// 快递/物流公司名称
        /// </summary>
        public string ExpressCompany { get; set; }

        /// <summary>
        /// 快递/物流单号
        /// </summary>
        public string ExpressNum { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsId { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 商品GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 颜色名称
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string NormName { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int GoodsNum { get; set; }

        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthPrice { get; set; }

        /// <summary>
        /// 标识数据来源表（1、近三个月的订单；2、三个月以前的订单）
        /// </summary>
        //public int Type { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-24
    /// Desc：订单搜索条件
    /// </summary>
    public class OrderSearchEntity
    {
        /// <summary>
        /// 商品所属用户/商户ID
        /// </summary>
        public int OperId { get; set; }

        /// <summary>
        /// 是否发货（true：发货订单   false：未发货订单）
        /// </summary>
        public bool IsSend { get; set; }

        /// <summary>
        /// 收件人名称
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endDate { get; set; }
    }
}
