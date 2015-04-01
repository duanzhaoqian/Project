using System.Collections.Generic;
using TXDal.FinancialData;
using TXModel.AdminPVM;

namespace TXBll.FinancialData
{
    /// <summary>
    /// 提现记录
    /// </summary>
    public class WithdrawCashBll
    {
        private readonly WithdrawCashDal _withdrawCashDal = new WithdrawCashDal();

        /// <summary>
        /// 获取提现列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_WithdrawCash> GetPageList_BySearch_WithdrawCash(PVS_NH_WithdrawCash search, int pageIndex, int pageSize)
        {
            var list = _withdrawCashDal.GetPageList_BySearch_WithdrawCash(search, pageIndex, pageSize);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].CreateTimeString = string.Format("{0:yyy-MM-dd HH:mm:ss}", list[i].CreateTime);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取提现记录总数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_WithdrawCash(PVS_NH_WithdrawCash search)
        {
            return _withdrawCashDal.GetTotalCount_BySearch_WithdrawCash(search);
        }

        /// <summary>
        /// 修改提现状态
        /// 李   刚
        /// </summary>
        /// <param name="id">提现编号</param>
        /// <param name="status">提现状态</param>
        /// <param name="adminId">操作员Id</param>
        /// <param name="adminName">操作员姓名</param>
        /// <param name="developerId">提现用户Id</param>
        /// <param name="price">提现金额</param>
        /// <param name="currentStatus">当前提现记录的状态</param>
        /// <returns></returns>
        public int UpdateWithdrawCashStatus(int id, int status, int adminId, string adminName, int developerId, decimal price, int currentStatus)
        {
            int result = 0;
            //审批通过 或 打款失败
            if (status == 1 || status == 4)
            {
                result = _withdrawCashDal.UpdateStatus(id, status, adminId, adminName);
            }
                //打款成功
            else if (status == 3)
            {
                result = _withdrawCashDal.UpdateSuccess(id, status, adminId, adminName, developerId, price);
            }
                //审批未通过
            else if (status == 2)
            {
                result = _withdrawCashDal.UpdateFail(id, status, adminId, adminName, developerId, price);
            }
            return result;
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<PVM_NH_WithdrawCash> GetWithdrawCash_Export(PVS_NH_WithdrawCash search)
        {
            return _withdrawCashDal.GetWithdrawCash_Export(search);
        }

        /// <summary>
        /// 根据编号获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetEntity_ById(int id)
        {
            return _withdrawCashDal.GetEntity_ById(id);
        }
    }
}