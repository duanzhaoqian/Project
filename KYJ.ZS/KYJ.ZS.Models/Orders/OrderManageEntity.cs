using System;

namespace KYJ.ZS.Models.Orders
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-23
    /// Desc：订单管理列表Entity
    /// </summary>
    public class OrderManageEntity
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
        /// 时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsId { get; set; }

        /// <summary>
        /// 商品GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 颜色名称
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string NormName { get; set; }

        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthPrice { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int GoodsNum { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        public string UserNikeName { get; set; }

        /// <summary>
        /// 订单总价格
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 订单运费
        /// </summary>
        public decimal FreightCost { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int State { get; set; }


        public string NomrAndColor
        {
            get
            {
                string str = "";
                if (!string.IsNullOrEmpty(NormName))
                {
                    str += NormName;
                }
                if (!string.IsNullOrEmpty(NormName) && !string.IsNullOrEmpty(ColorName))
                {
                    str += " | "+ColorName;
                }
                if (string.IsNullOrEmpty(NormName) && !string.IsNullOrEmpty(ColorName))
                {
                    str += ColorName;
                }
                return str;
            }
        }


    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-23
    /// Desc：订单区域Enum
    /// </summary>
    public enum OrderAreaEnum
    {
        /// <summary>
        /// 全部订单
        /// </summary>
        All,

        /// <summary>
        /// 未付款区
        /// </summary>
        Unpaid,

        /// <summary>
        /// 发货区
        /// </summary>
        Ship,

        /// <summary>
        /// 租用区
        /// </summary>
        Hire,

        /// <summary>
        /// 退租及退换货区
        /// </summary>
        Return,

        /// <summary>
        /// 结算区
        /// </summary>
        Settlement,

        /// <summary>
        /// 成功订单
        /// </summary>
        Success,

        /// <summary>
        /// 关闭订单
        /// </summary>
        Closed
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-23
    /// Desc：订单状态Enum
    /// </summary>
    public enum OrderStateEnum
    {
        /// <summary>
        /// 待付款
        /// </summary>
        DaiFuKuan = 1,

        /// <summary>
        /// 付款失败
        /// </summary>
        FuKuanShiBai = 2,

        /// <summary>
        /// 待发货
        /// </summary>
        DaiFaHuo = 3,

        /// <summary>
        /// 待收货
        /// </summary>
        DaiShouHuo = 4,

        /// <summary>
        /// 已收货
        /// </summary>
        YiShouHuo = 5,

        /// <summary>
        /// 待确认起租
        /// </summary>
        DaiQiZu = 6,

        /// <summary>
        /// 租用中
        /// </summary>
        ZuYongZhong = 7,

        /// <summary>
        /// 待续租
        /// </summary>
        DaiXuZu = 8,

        /// <summary>
        /// 申请退租
        /// </summary>
        ShenQingTuiZu = 9,

        /// <summary>
        /// 退租验收
        /// </summary>
        TuiZuYanShou = 10,

        /// <summary>
        /// 退租结算
        /// </summary>
        TuiZuJieSuan = 11,

        /// <summary>
        /// 申请退货
        /// </summary>
        ShenQingTuiHuo = 12,

        /// <summary>
        /// 退货验收
        /// </summary>
        TuiHuoYanShou = 13,

        /// <summary>
        /// 退货结算
        /// </summary>
        TuiHuoJieSuan = 14,

        /// <summary>
        /// 申请换货
        /// </summary>
        ShenQingHuanHuo = 15,

        /// <summary>
        /// 换货验收
        /// </summary>
        HuanHuoYanShou = 16,

        /// <summary>
        /// 退租成功
        /// </summary>
        TuiZuChengGong = 17,

        /// <summary>
        /// 代售成功
        /// </summary>
        DaiShouChengGong = 18,

        /// <summary>
        /// 订单关闭
        /// </summary>
        DingDanGuanBi = 19,

        /// <summary>
        /// 退货关闭
        /// </summary>
        TuiHuoGuanBi = 20,

        /// <summary>
        /// 代售结算
        /// </summary>
        DaiShouJieSuan = 21,

        /// <summary>
        /// 退租用户结算待确认
        /// </summary>
        TuiZuDaiQueRen = 22,

        /// <summary>
        /// 退租用户结算待支付
        /// </summary>
        TuiZuDaiZhiFu = 23,

        /// <summary>
        /// 退货用户结算待确认
        /// </summary>
        TuiHuoDaiQueRen = 24,

        /// <summary>
        /// 退货用户结算待支付
        /// </summary>
        TuiHuoDaiZhiFu = 25,

        /// <summary>
        /// 换货用户结算待支付
        /// </summary>
        HuanHuoDaiZhiFu = 26
    }
}
