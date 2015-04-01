using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月23日15:43:33
    /// 描述：订单状态与显示区域的对应
    /// </summary>
    public class OrderStateEnumOper
    {
        private  Dictionary<OrderStateAreaType, int[]> _dic = new Dictionary<OrderStateAreaType, int[]>();
        private  string [] _ints=new string[]
        {
            "未知","待付款","付款失败","待发货","待收货","已收货","待确认起租","租用中","待续租","申请退租","退租验收","退租结算"
            ,"申请退货","退货验收","退货结算","申请换货","换货验收","退租成功","代售成功","订单关闭","退货关闭","代售结算","退租用户结算待确认",
            "退租用户结算待支付","退货用户结算待确认","退货用户结算待支付","换货用户结算待支付"
        };

        private string[] _strings = new string[] { "未知", "下单", "下单", "支付", "发货", "收货", "发送起租协议", "起租", "待续租", "申请退租", "同意退租", "退租结算", "申请退货", "同意退货", "退货结算", "申请换货", "同意换货", "成功", "成功", "关闭", "关闭", "结算", "退租用户结算待确认", "退租用户结算待支付", "退货用户结算待确认", "退货用户结算待支付", "换货用户结算待支付" };

        //private string [] _strings=new string[]
        //{
        //   "未知",//      1
        //   "预租",//      2
        //   "预租",//      3
        //   "支付",//      4 
        //   "发货",//      5
        //   "收货",//      6
        //   "发送起租协议",//7
        //   "起租",//      8
        //   "待续租", //    9
        //   "申请退租",//    10
        //   "同意退租", //   11
        //   "验收",       //     12
        //   "申请退货", //       13
        //   "同意退货", //       14
        //   "验收", //         15
        //   "申请换货",//        16
        //   "同意换货", //       17
        //   "成功", //             18
        //   "成功", //         19
        //   "关闭",//          20
        //   "关闭", //         21
        //   "代售结束",//        22
        //   "确定赔损金额",//      23
        //   "确定赔损金额", //     24
        //   "确定赔损金额", //     25
        //   "买家支付赔损金额" //    26
        //};


        public OrderStateEnumOper()
        {
            //全部订单
            _dic.Add(OrderStateAreaType.All, new[] { -1 });
            _dic.Add(OrderStateAreaType.Closed, new[] { 19, 20 });
            _dic.Add(OrderStateAreaType.RentGoods, new[] { 6, 7, 8 });
            _dic.Add(OrderStateAreaType.ReturnAndBackGoods, new[] { 9, 10, 12, 13, 15, 16 });
            _dic.Add(OrderStateAreaType.SendGoods, new[] { 3, 4, 5 });
            _dic.Add(OrderStateAreaType.Settlement, new[] { 11, 14, 21, 22, 23, 24, 25, 26 });
            _dic.Add(OrderStateAreaType.Successed, new[] { 17, 18 });
            _dic.Add(OrderStateAreaType.WaitPay, new[] { 1, 2 });
        }
        /// <summary>
        /// 获取一个订单区域对应的订单集合
        /// </summary>
        /// <returns></returns>
        public int[] GetOrderStates(OrderStateAreaType orderStateAreaType)
        {
            return _dic[orderStateAreaType];
        }

        /// <summary>
        /// 获取一个订单区域对应的订单集合
        /// </summary>
        /// <param name="orderStateAreaType"></param>
        /// <returns></returns>
        public string GetOrderState(OrderStateAreaType orderStateAreaType)
        {
            return string.Join(",", _dic[orderStateAreaType]);
        }
        /// <summary>
        /// 根据订单状态得到订单所在区域
        /// </summary>
        /// <returns></returns>
        public OrderStateAreaType GetAreaType(OrderState orderState)
        {
            return GetAreaType((int)orderState);
        }
        /// <summary>
        /// 根据订单状态得到订单所在区域
        /// </summary>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public OrderStateAreaType GetAreaType(int orderState)
        {
            foreach (var kv in _dic)
            {
                if (kv.Value.Contains(orderState))
                {
                    return kv.Key;
                }
            }
            return OrderStateAreaType.All;
        }
        /// <summary>
        /// 获取订单状态描述
        /// </summary>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public string GetOrderStateDes(int orderState)
        {
            return _ints[orderState];
        }
        /// <summary>
        /// 获取订单状态的描述
        /// </summary>
        /// <returns></returns>
        public string GetOrderStateDes(OrderState state)
        {
            return _ints[(int)state];
        }
        /// <summary>
        /// 获取订单状态的时间描述
        /// </summary>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public string GetOrderStateTimeDes(int orderState)
        {
            return _strings[orderState];
        }
        /// <summary>
        /// 获取订单状态的时间描述
        /// </summary>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public string GetOrderStateTimeDes(OrderState orderState)
        {
            return GetOrderStateTimeDes((int) orderState);
        }

    }


    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月23日15:51:49
    /// 描述：订单显示区域枚举
    /// </summary>
    public enum OrderStateAreaType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 1,
        /// <summary>
        /// 等待付款区
        /// </summary>
        WaitPay = 2,
        /// <summary>
        /// 发货区
        /// </summary>
        SendGoods = 3,
        /// <summary>
        /// 租用区
        /// </summary>
        RentGoods = 4,
        /// <summary>
        /// 退租及退换货区
        /// </summary>
        ReturnAndBackGoods = 5,
        /// <summary>
        /// 结算区
        /// </summary>
        Settlement = 6,
        /// <summary>
        /// 成功区
        /// </summary>
        Successed = 7,
        /// <summary>
        /// 关闭单区
        /// </summary>
        Closed = 8
    }

    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月26日09:37:00
    /// 描述：租售订单状态
    /// </summary>
    public enum OrderState
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = -1,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 代付款
        /// </summary>
        WaitPay = 1,
        /// <summary>
        /// 付款失败
        /// </summary>
        PayFailed = 2,
        /// <summary>
        /// 待发货
        /// </summary>
        WaitSendGoods = 3,
        /// <summary>
        /// 待收货
        /// </summary>
        WaitReceive = 4,
        /// <summary>
        /// 已收货
        /// </summary>
        Received = 5,
        /// <summary>
        /// 待确认起租
        /// </summary>
        WaitRent = 6,
        /// <summary>
        /// 租用中
        /// </summary>
        Renting = 7,
        /// <summary>
        /// 待续租
        /// </summary>
        WaitRenewal = 8,
        /// <summary>
        /// 申请退租
        /// </summary>
        ApplyBackRent = 9,
        /// <summary>
        /// 退租验收
        /// </summary>
        BackRentCheck = 10,
        /// <summary>
        /// 退租结算
        /// </summary>
        BackRentSettlement = 11,
        /// <summary>
        /// 申请退货
        /// </summary>
        ApplyBackGoods = 12,
        /// <summary>
        /// 退货验收
        /// </summary>
        BackGoodsCheck = 13,
        /// <summary>
        /// 退货结算
        /// </summary>
        BackGoodsSettlement = 14,
        /// <summary>
        /// 申请换货
        /// </summary>
        ApplyExchangeGoods = 15,
        /// <summary>
        /// 换货验收
        /// </summary>
        ExchangeGoodsCheck = 16,
        /// <summary>
        /// 退租成功
        /// </summary>
        BackRentSucceed = 17,
        /// <summary>
        /// 代售成功
        /// </summary>
        SaleSucceed = 18,
        /// <summary>
        /// 订单关闭
        /// </summary>
        OrderClosed = 19,
        /// <summary>
        /// 退货关闭
        /// </summary>
        BackGoodsClosed = 20,
        /// <summary>
        /// 代售结算
        /// </summary>
        SaleSettlement = 21,
        /// <summary>
        /// 退租用户结算待确认
        /// </summary>
        BackRentWaitConfirm = 22,
        /// <summary>
        /// 退租用户结算待支付
        /// </summary>
        BackRentWaitPay = 23,
        /// <summary>
        /// 退货用户结算待确认
        /// </summary>
        BackGoodsWaitConfirm = 24,
        /// <summary>
        /// 退货用户结算待支付
        /// </summary>
        BackGoodsWaitPay = 25,
        /// <summary>
        /// 换货用户结算待支付
        /// </summary>
        ExchaneGoodsWaitPay = 26

    }
}
