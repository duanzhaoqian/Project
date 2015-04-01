using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.OrderProcess
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
            string[] arrState = new string[] { "未知", "待付款", "付款失败", "待发货", "待收货", "待确认起租", "租用中", "待续租", "退租", "退租验收", "退租结算", "退货", "退货验收", "退货结算", "成功订单", "关闭订单" };
            return arrState[state];
        }
        #endregion
    }
}
