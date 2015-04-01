using System;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：商家账户日志
    /// </summary>
    public class AccountLog
    {

        public int Id { get; set; }
        /// <summary>
        /// 自增标示
        /// </summary>
        public int Tag { get; set; }
        
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 日志类型：0 未知，1 充值，2 提现，3 租金
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 状态：0 未知，1 出账，2 入账
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 日志说明
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 日志创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
