using System;
using TXModel.Commons;
namespace TXBll.Dev
{
    /// <summary>
    /// 开发商账号 (开发商后台)
    /// </summary>
    public partial class Developer_AccountBll
    {
        TXDal.Dev.Developer_AccountDal _dal = new TXDal.Dev.Developer_AccountDal();
        #region 账户流水
        /// <summary>
        /// 账户流水
        /// author: 汪敏
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="typeId">类型</param>
        /// <param name="paging">分页类</param>
        /// <param name="TotalExpenses">总支出</param>
        /// <param name="TotalIncome">总收入</param>
        /// <returns></returns>
        public Paging<TXOrm.Developer_AccountLog> GetAccountLog(int developerId, DateTime starttime, DateTime endtime, int typeId, Paging<TXOrm.Developer_AccountLog> paging, out decimal TotalExpenses, out decimal TotalIncome)
        {
            return _dal.GetAccountLog(developerId, starttime, endtime, typeId, paging, out TotalExpenses, out TotalIncome);
        }
        #endregion

        #region  我要充值
        /// <summary>
        /// 我要充值
        /// author: 汪敏
        /// </summary>
        /// <param name="UserId">开发商Id</param>
        /// <param name="price">充值金额</param>
        /// <param name="RechargeNo">充值编号</param>
        /// <param name="Status">充值状态</param>
        /// <returns>执行错误返回:false</returns>
        public bool AddRechargeInfo(int UserId, decimal price, string RechargeNo, int Status)
        {
            return _dal.AddRechargeInfo(UserId, price, RechargeNo, Status);
        }
        #endregion

        #region  更新充值订单的状态
        /// <summary>
        /// 更新充值订单的状态
        /// author:汪敏
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="RechargeNo">订单号</param>
        /// <param name="Status">状态</param>
        /// <param name="price">充值金额</param>
        /// <returns></returns>
        public bool RechargeChangeState(string RechargeNo, int Status, decimal price)
        {
            return _dal.RechargeChangeState(RechargeNo, Status, price);
        }
        #endregion

        #region 提现
        /// <summary>
        /// 提现
        /// author:汪敏
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>执行错误返回:false</returns>
        public bool DrawCash(TXOrm.Developer_WithdrawCash model)
        {
            return _dal.DrawCash(model);
        }
        #endregion

        #region 查询账户余额及充值金币总额
        /// <summary>
        /// 查询账户余额及充值金币总额
        /// author:汪敏
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <returns></returns>
        public string[] GetAccountMoneyAndTotalRechargeById(int developerId)
        {
            return _dal.GetAccountMoneyAndTotalRechargeById(developerId);
        }
        #endregion

        #region 获取充值的最大ID
        /// <summary>
        /// 获取充值的最大ID
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public int GetRechargeMaxId()
        {
            return _dal.GetRechargeMaxId();
        }
        #endregion

        #region 生成充值编号
        /// <summary>
        /// 生成充值编号
        /// author:汪敏
        /// </summary>
        /// <param name="MaxId"></param>
        /// <returns></returns>
        public string GetRechargeNo(string MaxId)
        {
            //为了财务以后和第三方支付平台对账，这个订单充值编号必须和其他支付做区分，所以暂时以1601开头
            // return "1501" + DateTime.Now.ToString("yyMMdd") + MaxId.PadLeft(6, '0');
            return "1601" + DateTime.Now.ToString("yyMMdd") + MaxId.PadLeft(6, '0');
        }
        #endregion

        #region  添加充值记录
        /// <summary>
        /// 添加充值记录
        /// author:汪敏
        /// </summary>
        /// <param name="UserId">开发商Id</param>
        /// <param name="type">类型（1保证金，2 账户提现，3 账户充值）</param>
        /// <param name="paytype">支付类型(1支出,2 收入)</param>
        /// <param name="Price">充值金额</param>
        /// <param name="Desc">说明</param>
        /// <param name="RechargeNo">充值编号</param>
        /// <param name="Status">充值状态</param>
        /// <param name="IsSuc">是否成功</param>
        public void AddUserAccountInfo(int UserId, decimal Price, string RechargeNo, int Status)
        {
            _dal.AddUserAccountInfo(UserId, Price, RechargeNo, Status);
        }
        #endregion
    }
}