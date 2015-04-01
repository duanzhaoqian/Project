using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Payments
{
    /// <summary>
    /// Author:zhuzh
    /// Date  :2014-05-19
    /// Desc  :支付信息
    /// </summary>
    public class PaymentEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 订单商品编号
        /// </summary>
        public int OrderGoodesID { get; set; }
        /// <summary>
        /// 订单所属用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }
        /// <summary>
        /// 订单支付总价格
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 产生滞纳金天数
        /// </summary>
        public int LatefeesDay { get; set; }
        /// <summary>
        /// 商品滞纳金
        /// </summary>
        public decimal Latefees { get; set; }
        /// <summary>
        /// 订单总押金
        /// </summary>
        public decimal TotalDeposit { get; set; }
        /// <summary>
        /// 赔损金额
        /// </summary>
        public decimal Loseprice { get; set; }
        /// <summary>
        /// 商品租期数
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthPrice { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal FreightCost { get; set; }
        /// <summary>
        /// 所选银行
        /// </summary>
        public BankType Mode { get; set; }
        /// <summary>
        /// 回调地址
        /// </summary>
        public string CallBackUrl { get; set; }

    }
    /// <summary>
    /// Author:zhuzh
    /// Date  :2014-05-19
    /// Desc  :支付银行类型
    /// </summary>
    public enum BankType
    {
        /// <summary>
        /// 中国工商银行
        /// </summary>
        ICBC,
        /// <summary>
        /// 中国建设银行
        /// </summary>
        CCB,
        /// <summary>
        /// 中国农业银行
        /// </summary>
        ABC,
        /// <summary>
        /// 招商银行
        /// </summary>
        CMB,
        /// <summary>
        /// 中国银行
        /// </summary>
        BOC,
        /// <summary>
        /// 浦发银行
        /// </summary>
        SPDB,
        /// <summary>
        /// 交通银行
        /// </summary>
        BCOM,
        /// <summary>
        /// 广发银行
        /// </summary>
        GDB,
        /// <summary>
        /// 中国民生银行
        /// </summary>
        CMBC,
        /// <summary>
        /// 兴业银行
        /// </summary>
        CIB,
        /// <summary>
        /// 中国光大银行
        /// </summary>
        CEB,
        /// <summary>
        /// 中信银行
        /// </summary>
        CITIC,
        /// <summary>
        /// 中国邮政储蓄银行
        /// </summary>
        PSBC,
        /// <summary>
        /// 平安银行
        /// </summary>
        PAB,
        /// <summary>
        /// 上海银行
        /// </summary>
        SHB,
        /// <summary>
        /// 深圳发展银行
        /// </summary>
        SDB,
        /// <summary>
        /// 中国银联
        /// </summary>
        UPOP,
        /// <summary>
        /// 汇丰银行
        /// </summary>
        HSB,
        /// <summary>
        /// 华夏银行
        /// </summary>
        HXB,
        /// <summary>
        /// 广州银行
        /// </summary>
        GZCB,
        /// <summary>
        /// 南京银行
        /// </summary>
        NJCB,
        /// <summary>
        /// 渤海银行
        /// </summary>
        CBHB,
        /// <summary>
        /// 杭州银行
        /// </summary>
        HZB,
        /// <summary>
        /// 东亚银行
        /// </summary>
        BEA,
        /// <summary>
        /// 上海农商银行
        /// </summary>
        SRCB,
        /// <summary>
        /// 浙商银行
        /// </summary>
        CZB,
        /// <summary>
        /// 中国邮政
        /// </summary>
        POST,
        /// <summary>
        /// 北京农商银行
        /// </summary>
        BJRCB,
        /// <summary>
        /// 北京银行
        /// </summary>
        BOB,
        /// <summary>
        /// 宁波银行
        /// </summary>
        NBCB,
        /// <summary>
        /// 块钱
        /// </summary>
        KQ
    }
}
