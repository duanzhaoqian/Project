using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-4-17
    /// Desc:订单实体类
    public class Order
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 商品所属用户/商户ID
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 订单商品类型  0未知 1租 2售
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string Number { get; set; }
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
        /// 收货地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 备用联系方式
        /// </summary>
        public string ResTel { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 快递/物流公司名称
        /// </summary>
        public string ExpressCompany { get; set; }
        /// <summary>
        /// 快递/物流单号
        /// </summary>
        public string ExpressNum { get; set; }
        /// <summary>
        /// 订单运费
        /// </summary>
        public decimal FreightCost { get; set; }
        /// <summary>
        /// 支付方式  0未知 1线上 2线下
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 订单状态 0未知 1待付款 2付款失败 3待发货 4待收货 5已收货 6待确认起租 7租用中 8待续租 9申请退租 10退租验收 11退租结算 12申请退货 13退货验收 14退货结算 15申请换货 16换货验收 17退租成功 18代售成功 19订单关闭 20退货关闭 21代售结算 22等待用户支付换货赔损金额 23等待用户支付退货赔偿金额 24等待用户支付退租赔偿金额 25用户已支付赔损金额
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 订单总价格
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 订单总押金
        /// </summary>
        public decimal TotalDeposit { get; set; }
        /// <summary>
        /// 每月总价格（售：每月总价格=总价格）
        /// </summary>
        public decimal MonthPrice { get; set; }
        /// <summary>
        /// 送货方式 0未知 1不限 2只工作日 3只双休日
        /// </summary>
        public int ShipPingMethod { get; set; }
        /// <summary>
        /// 是否电话确认
        /// </summary>
        public bool IsPhoneConfirm { get; set; }
        /// <summary>
        /// 发票方式 0未知  1普通发票 2增值税发票
        /// </summary>
        public int InvoiceMode { get; set; }
        /// <summary>
        /// 发票抬头 0未知 1个人 2单位
        /// </summary>
        public int InvoiceRise { get; set; }
        /// <summary>
        /// 发票抬头内容(如单位名称)
        /// </summary>
        public string InvoiceTitle { get; set; }
        /// <summary>
        /// 发票内容 0未知 1明细 2办公用品 3电脑配件 4耗材
        /// </summary>
        public int InvoiceType { get; set; }
        /// <summary>
        /// 是否使用优惠券
        /// </summary>
        public bool IsDiscoupon { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public decimal DisPrice { get; set; }
        /// <summary>
        /// 是否评论
        /// </summary>
        public bool IsComment { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

    }
}
