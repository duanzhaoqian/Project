using System.Collections.Generic;
using KYJ.ZS.DAL.BankAccounts;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.BankAccounts;
using KYJ.ZS.Models.View;
using KYJ.ZS.Commons;

namespace KYJ.ZS.BLL.BankAccounts
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：操作商家银行账户(数据库表M_BankAccounts)，包括查询、添加、修改
    /// </summary>
    public class BankAccountBll
    {
        private BankAccountDal dal = new BankAccountDal();

        #region 添加银行账户 +bool Create(BankAccount bankAccount)
        /// <summary>
        /// 添加银行账户
        /// </summary>
        /// <param name="bankAccount">银行账户</param>
        /// <returns></returns>
        public bool Create(BankAccount bankAccount)
        {
            return dal.Create(bankAccount) > 0;
        }
        #endregion

        #region 修改银行账户 +bool Update(BankAccount bankAccount)
        /// <summary>
        /// 修改银行账户
        /// </summary>
        /// <param name="bankAccount">银行账户</param>
        /// <returns></returns>
        public bool Update(BankAccount bankAccount)
        {
            return dal.Update(bankAccount) > 0;
        }
        #endregion

        #region 获取银行账户 +BankAccount GetBankAccount(int bankAccountId)
        /// <summary>
        /// 获取银行账户
        /// </summary>
        /// <param name="bankAccountId">银行账户ID</param>
        /// <returns></returns>
        public BankAccount GetBankAccount(int bankAccountId)
        {
            return dal.GetBankAccount(bankAccountId);
        }
        #endregion

        #region 获取商户银行账号列表 +IList<BankAccount> GetBankAccountList(int merchantId)
        /// <summary>
        /// 获取商户银行账号列表
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public IList<BankAccount> GetBankAccountList(int merchantId)
        {
            return dal.GetBankAccountList(merchantId);
        }
        #endregion

        #region 删除银行账户 +bool Delete(int bankId, out string message)
        /// <summary>
        /// 删除银行账户
        /// </summary>
        /// <param name="bankId">银行账户ID</param>
        /// <returns></returns>
        public bool Delete(int bankId, out string message)
        {
            if (!(dal.Delete(bankId) > 0))
            {
                message = "删除失败！";
                return false;
            }
            else
            {
                message = "删除成功！";
                return true;
            }

        }
        #endregion

        #region 删除商户所有银行账户 +bool DelByMerchantID(int merchantId)
        /// <summary>
        /// 删除商户所有银行账户
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public bool DelByMerchantID(int merchantId)
        {
            return dal.DelByMerchantID(merchantId) > 0;
        }
        #endregion

        #region 获取商户银行账号列表以及账号金额 +List<BankAccountEntity> GetBankList(int merchantId)
        /// <summary>
        /// Author：李晓波
        /// Time：2014-04-28
        /// Desc：获取商户银行账号列表以及账号金额
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        /// </summary>
        public List<BankAccountEntity> GetBankList(int merchantId)
        {
            return dal.GetBankList(merchantId);

        }
        #endregion

        #region 获取商户账户余额 +string GetAccountPrice(int id)
        /// <summary>
        /// 获取商户账户余额
        /// </summary>
        /// <param name="id">商户id</param>
        /// <returns></returns>
        public string GetAccountPrice(int id)
        {
            return dal.GetAccountPrice(id);
        }
        #endregion

        #region 商户提现 +bool ApplyFor(int id, BankAccountEntity mdlAccountEntity, decimal withdraw, decimal withdrawbalance, string orderNumber, out string message)
        /// <summary>
        /// Author：李晓波
        /// Time：2014-04-28
        /// Desc：商户提现 
        /// <para>20140528新添加字段：</para>
        /// <para>orderNumber:提现单号</para>
        /// <para>withdrawbalance:提现后的账户余额</para>
        /// </summary>   
        /// <param name="withdraw">提现金额</param>
        /// <returns></returns>
        public bool ApplyFor(int id, BankAccountEntity mdlAccountEntity, decimal withdraw, decimal withdrawbalance, string orderNumber, out string message)
        {
            bool status = GetAccountStatus(id);
            if (!status)//如果账户没有冻结
            {
                if (string.IsNullOrEmpty(withdraw.ToString()))
                {
                    message = "必须输入提现金额！";
                    return false;
                }
                else
                {

                    decimal AccountPrice = Auxiliary.ToDecimal(dal.GetAccountPrice(id));
                    if (withdraw < 1000)
                    {
                        message = "每次提现金额不得小于1000元！";
                        return false;
                    }
                    else if (withdraw > AccountPrice)
                    {
                        message = "提现金额不能大于可用余额！";
                        return false;
                    }
                    else
                    {
                        int result = dal.ApplyFor(id, mdlAccountEntity, withdraw, withdrawbalance, orderNumber);
                        if (result > 0)
                        {
                            message = "提现成功，管理员正在审批！";
                            return true;
                        }
                        else
                        {
                            message = "提现操作失败，请重新提现！";
                            return false;
                        }
                    }
                }
            }
            else
            {
                message = "提现操作失败，账户被冻结！";
                return false;
            }
        }
        #endregion

        #region 商户账户充值 +bool Recharge(int id, decimal rechargeAmount, out string message)
        /// <summary>
        /// 商户账户充值
        /// </summary>
        /// <param name="id">商户ID</param>
        /// <param name="rechargeAmount">充值金额</param>
        /// <returns></returns>
        public bool Recharge(int id, decimal rechargeAmount, out string message)
        {
            bool status = GetAccountStatus(id);
            if (!status)//如果账户没有冻结
            {
                if (string.IsNullOrEmpty(rechargeAmount.ToString()))
                {
                    message = "必须输入充值金额！";
                    return false;
                }
                else
                {
                    decimal AccountPrice = Auxiliary.ToDecimal(dal.GetAccountPrice(id));
                    int result = dal.Recharge(id, rechargeAmount);
                    if (result > 0)
                    {
                        message = "充值成功！";
                        return true;
                    }
                    else
                    {
                        message = "充值操作失败，请重新充值！";
                        return false;
                    }
                }
            }
            else
            {
                message = "充值操作失败，账户被冻结！";
                return false;
            }
        } 
        #endregion

        #region 获取账户状态 +bool GetAccountStatus(int id)
        /// <summary>
        /// 获取账户状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetAccountStatus(int id)
        {
            return dal.GetAccountStatus(id);//获取账户状态

        }
        #endregion

        #region 获取冻结账户信息 +BankAccountEntity GetWithdrawCashs(int id)
        /// <summary>
        /// 获取冻结账户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BankAccountEntity GetWithdrawCashs(int id)
        {
            return dal.GetWithdrawCashs(id);
        }
        #endregion
    }


}
