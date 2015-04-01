using System;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：商家账户
    /// </summary>
    public class Account
    {

        public int Id { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 账户金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 上次提现金额
        /// </summary>
        public decimal BackPrice { get; set; }

        /// <summary>
        /// 提现次数
        /// </summary>
        public int DrawNum { get; set; }

        /// <summary>
        /// 提现总金额
        /// </summary>
        public decimal DrawTotalPrice { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
