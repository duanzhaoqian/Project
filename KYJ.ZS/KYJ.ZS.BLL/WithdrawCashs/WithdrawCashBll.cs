using System;
using System.Collections.Generic;
using KYJ.ZS.DAL.WithdrawCashs;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.WithdrawCashs;

namespace KYJ.ZS.BLL.WithdrawCashs
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：操作商户提现(数据库表M_WithdrawCashs)，包括查询、添加、修改
    /// </summary>
    public class WithdrawCashBll
    {
        private WithdrawCashDal dal = new WithdrawCashDal();

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="withdrawCash">商户提现</param>
        /// <returns></returns>
        public bool Create(WithdrawCash withdrawCash)
        {
            return dal.Create(withdrawCash) > 0;
        }

        /// <summary>
        /// 修改提现信息
        /// </summary>
        /// <param name="withdrawCashId">提现表ID</param>
        /// <param name="state">提现状态：0 未知，1 待审批，2 审批通过待打款，3 审批未通过，4 打款成功，5 打款失败</param>
        /// <param name="updateTime">更新时间</param>
        /// <returns></returns>
        public bool Update(int withdrawCashId, int state, DateTime updateTime)
        {
            return dal.Update(withdrawCashId, state, updateTime) > 0;
        }

        /// <summary>
        /// 获取提现信息
        /// </summary>
        /// <param name="withdrawCashId">提现记录ID</param>
        /// <returns></returns>
        public WithdrawCash GetWithdrawCash(int withdrawCashId)
        {
            return dal.GetWithdrawCash(withdrawCashId);
        }

        /// <summary>
        /// 获取提现记录
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="areaEnum">提现区域Enum</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<WithdrawCash> GetWithdrawCashList(int? merchantId, WithdrawCashAreaEnum areaEnum, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetWithdrawCashList(merchantId, areaEnum, null, null, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 获取商户提现记录
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="areaEnum">提现区域Enum</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<WithdrawCash> GetWithdrawCashList(int? merchantId, WithdrawCashAreaEnum areaEnum, DateTime? startDate, DateTime? endDate, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetWithdrawCashList(merchantId, areaEnum, startDate, endDate, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <param name="reason">驳回理由</param>
        /// <returns></returns>
        public bool Reject(int id, string reason)
        {
            return dal.Reject(id, reason) > 0;
        }

        /// <summary>
        /// 确认提现
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <returns></returns>
        public bool ConfirmWithdrawal(int id)
        {
            return dal.ConfirmWithdrawal(id) > 0;
        }
    }
}
