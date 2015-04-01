using System;

namespace TXBll.Order
{
   public partial class OrderBll
    {
        /// <summary>
        /// 生成充值编号
        /// Author:汪敏
        /// 时间:2013-12-5
        /// </summary>
        /// <returns></returns>
        public string GetRechargeNo(string MaxId)
        {
            return "1501" + DateTime.Now.ToString("yyMMdd") + MaxId.PadLeft(6, '0');
        }
    }
}
