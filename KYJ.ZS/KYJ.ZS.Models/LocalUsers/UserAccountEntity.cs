using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.LocalUsers
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-5-15
    /// Desc:个人帐户实体类
    /// </summary>
    public class UserAccountEntity
    {
        /// <summary>
        /// pk
        /// </summary>
        public int AId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账户金额
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 上次提现价格
        /// </summary>
        public decimal BackPrice { get; set; }
        /// <summary>
        /// 提现次数
        /// </summary>
        public int DrawMoneyTimes { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 充值总金额
        /// </summary>
        public decimal RechargeTotalPrice { get; set; }
        /// <summary>
        /// 投资总金额
        /// </summary>
        public decimal InvesteTotalPrice { get; set; }
        /// <summary>
        /// 投资总收入
        /// </summary>
        public decimal InvesteIncomeTotalPrice { get; set; }
        /// <summary>
        /// 已提现总金额
        /// </summary>
        public decimal WithdrawCashTotalPrice { get; set; }
    }
}
