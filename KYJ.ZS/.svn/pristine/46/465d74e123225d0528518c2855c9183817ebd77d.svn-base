using System;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：商户提现
    /// </summary>
    public class WithdrawCash
    {

        public int Id { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 提现单号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 提现状态：0 未知，1 待审批，2 审批通过待打款，3 审批未通过，4 打款成功，5 打款失败
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 驳回理由
        /// </summary>
        public string Rejectreason { get; set; }

        /// <summary>
        /// 提现方式：0 未知，1 银行卡，2 支付宝
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 商户银行帐号
        /// </summary>
        public string BandNumber { get; set; }

        /// <summary>
        /// 商户银行开户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商户开户行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 备注(银行账户)
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
