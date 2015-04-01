using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.Web
{
    public class OrderStatus
    {
        #region 订台状态文字说明
        /// <summary>
        /// 订台状态文字说明
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetStateExplain(int state)
        {
            string[] arrState = new string[] { "未知", "待付款", "付款失败", "待发货", "待收货", "已收货", "待确认起租", "租用中", "待续租", "申请退租", "退租验收", "退租结算", "申请退货", "退货验收", "退货结算", "申请换货", "换货验收", "退租成功", "代售成功", "订单关闭", "退货关闭", "代售结算", "退租用户结算待确认","退租用户结算待支付","退货用户结算待确认","退货用户结算待支付","换货用户结算待支付" };
            return arrState[state];
        }

        #endregion
        /// <summary>
        /// 订单时间的区别
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetStateTime(int state)
        {
            string[] arrState = new string[] { "未知", "预租", "预租", "支付", "发货", "收货", "发送起租协议", "起租", "待续租", "申请退租", "同意退租", "验收", "申请退货", "同意退货", "验收", "申请换货", "同意换货", "成功", "成功", "关闭", "关闭", "代售结束", "确定赔损金额", "确定赔损金额", "确定赔损金额", "买家支付赔损金额" };
            return arrState[state];
        }
        #region 订单显示时间
        /// <summary>
        /// 订单显示时间---用户后台
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetOrderShowTime(int state)
        {
            string[] arrState = new string[] { "未知", "下单时间", "下单时间", "支付时间", "发货时间", "收货时间", "起租待确认时间", "起租时间", "待续租", "申请退租时间", "同意退租时间", "用户结算时间", "申请退货时间", "同意退货时间", "用户结算时间", "申请换货时间", "同意换货时间", "成功时间", "成功时间", "关闭时间", "关闭时间", "结算", "验收时间", "验收时间", "验收时间", "验收时间", "验收时间" };
           // string[] arrState = new string[] { "未知", "下单", "下单", "支付", "发货", "收货", "发送起租协议", "起租", "待续租", "申请退租", "同意退租", "退租结算", "申请退货", "同意退货", "退货结算", "申请换货", "同意换货", "成功", "成功", "关闭", "关闭", "结算", "退租用户结算待确认", "退租用户结算待支付", "退货用户结算待确认", "退货用户结算待支付", "换货用户结算待支付" };
            return arrState[state];
        }
        #endregion
    }
    /// <summary>
    /// 星座
    /// </summary>
    public class Constellation
    {
        public static string GetConstellation(int id)
        {
            string[] arrState = new string[] { "未知", "魔蝎座", "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座" };
            return arrState[id];
        }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-28
    /// Desc：提现状态
    /// </summary>
    public class WithdrawCashStates
    {
        /// <summary>
        /// 提现状态文字说明
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetStateTitle(int state)
        {
            string[] arrState = new string[] { "未知", "待审核", "审批通过待打款", "已驳回", "已提现", "打款失败" };
            return arrState[state];
        }
    }
}
