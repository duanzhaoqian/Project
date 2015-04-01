using System;
using System.Collections.Generic;
using System.Linq;
using TXModel.Commons;
using TXOrm;
using System.Data.SqlClient;
namespace TXDal.Dev
{
    /// <summary>
    /// 开发商账号(开发商后台)
    /// </summary>
    public partial class Developer_AccountDal
    {
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
            List<TXOrm.Developer_AccountLog> result = null;
            int resultCount = 0;
            decimal Expenses = 0;
            decimal Income = 0;
            #region 拼接分页SQL
            //分页SQL
            string sql = String.Empty;
            if (typeId == 0)
            {
                sql = @"SELECT * FROM (
                    SELECT ROW_NUMBER() OVER(ORDER BY Id ASC) AS Row, * FROM Developer_AccountLog WITH(NOLOCK) 
                    WHERE IsDel = 0 AND IsSuc = 1 AND CreateTime BETWEEN @starttime AND @endtime AND DeveloperId=@developerId
                    ) AS tempTable
                    WHERE Row BETWEEN @pageBegin AND @pageEnd";
            }
            else
            {
                sql = @"SELECT * FROM (
                    SELECT ROW_NUMBER() OVER(ORDER BY Id ASC) AS Row, * FROM Developer_AccountLog WITH(NOLOCK) 
                    WHERE IsDel = 0 AND IsSuc = 1 AND CreateTime BETWEEN @starttime AND @endtime AND [Type]=@type AND DeveloperId=@developerId
                    ) AS tempTable
                    WHERE Row BETWEEN @pageBegin AND @pageEnd";
            }
            #endregion

            #region 拼接总记录SQL
            //总记录SQL
            string sqlCount = String.Empty;
            if (typeId == 0)
            {
                sqlCount = @"SELECT COUNT(1) FROM Developer_AccountLog WITH(NOLOCK) WHERE IsDel = 0 AND IsSuc=1 AND CreateTime BETWEEN @starttime AND @endtime AND DeveloperId = @developerId ";
            }
            else
            {
                sqlCount = @"SELECT COUNT(1) FROM Developer_AccountLog WITH(NOLOCK) WHERE IsDel = 0 AND IsSuc=1 AND CreateTime BETWEEN @starttime AND @endtime AND [Type]=@type AND DeveloperId = @developerId ";
            }
            #endregion

            #region 总支出(收入)SQl
            string sqlExpenses = String.Empty;
            string sqlIncome = String.Empty;
            if (typeId == 0)
            {
                sqlExpenses = @"SELECT Isnull(SUM(Price),0) FROM Developer_AccountLog WITH(NOLOCK) WHERE IsDel = 0 AND IsSuc=1 AND CreateTime BETWEEN '" + starttime + "' AND '" + endtime + "'  AND DeveloperId = " + developerId + " AND PayType=1 ";
                sqlIncome = @"SELECT Isnull(SUM(Price),0) FROM Developer_AccountLog WITH(NOLOCK) WHERE IsDel = 0 AND IsSuc=1 AND CreateTime BETWEEN '" + starttime + "' AND '" + endtime + "'  AND DeveloperId = " + developerId + " AND PayType=2 ";
            }
            else
            {
                sqlExpenses = @"SELECT Isnull(SUM(Price),0) FROM Developer_AccountLog WITH(NOLOCK) WHERE IsDel = 0 AND IsSuc=1 AND CreateTime BETWEEN '" + starttime + "' AND '" + endtime + "' AND [Type]=" + typeId + " AND DeveloperId = " + developerId + " AND PayType=1 ";
                sqlIncome = @"SELECT Isnull(SUM(Price),0) FROM Developer_AccountLog WITH(NOLOCK) WHERE IsDel = 0 AND IsSuc=1 AND CreateTime BETWEEN '" + starttime + "' AND '" + endtime + "' AND [Type]=" + typeId + " AND DeveloperId = " + developerId + " AND PayType=2 ";
            }
            #endregion

            #region 设置分页参数
            //分页参数
            SqlParameter[] para = new SqlParameter[] { 
                            new SqlParameter("@pageBegin",((paging.PageIndex - 1) * paging.PageSize) + 1),
                            new SqlParameter("@pageEnd",paging.PageIndex * paging.PageSize),
                            new SqlParameter("@developerId",developerId),
                            new SqlParameter("@starttime",starttime),
                            new SqlParameter("@endtime",endtime),
                            new SqlParameter("@type",typeId)
                    };
            #endregion

            #region 设置总记录参数
            //总记录参数
            SqlParameter[] paraCount = new SqlParameter[] { 
                       new SqlParameter("@developerId",developerId),
                       new SqlParameter("@starttime",starttime),
                       new SqlParameter("@endtime",endtime),
                       new SqlParameter("@type",typeId)
                    };
            #endregion

            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    //查询分页
                    var query = newhouseDb.ExecuteStoreQuery<TXOrm.Developer_AccountLog>(sql, para);
                    result = query.ToList();

                    #endregion

                    #region 查询总记录数据
                    var queryCount = newhouseDb.ExecuteStoreQuery<int>(sqlCount, paraCount);
                    resultCount = queryCount.FirstOrDefault<int>();

                    #endregion

                    #region 查询支出(收入)数据
                    //支出
                    var queryExpenses = newhouseDb.ExecuteStoreQuery<decimal>(sqlExpenses, null);
                    Expenses = queryExpenses.FirstOrDefault<decimal>();
                    //收入
                    var queryIncome = newhouseDb.ExecuteStoreQuery<decimal>(sqlIncome, null);
                    Income = queryIncome.FirstOrDefault<decimal>();
                    #endregion
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("developerId:{0},starttime:{1},endtime:{2},typeId:{3},paging:{4},TotalExpenses:{5},TotalIncome:{6}", developerId, starttime, endtime, typeId, paging, Expenses, Income), e);
                //throw;
            }
            paging.ResultData = result.OrderByDescending(p=>p.CreateTime).ToList();
            paging.TotalCount = resultCount;
            TotalExpenses = Expenses;
            TotalIncome = Income;
            return paging;
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
            using (var kyjUser = new kyj_NewHouseDBEntities())
            {
                bool result = false;
                try
                {
                    //新增 Developer_AccountLog
                    Developer_AccountLog AccountLog = new Developer_AccountLog();
                    AccountLog.DeveloperId = UserId;
                    AccountLog.Price = price;
                    AccountLog.PayType = 2;
                    AccountLog.Type = 3;
                    AccountLog.RechargeNo = RechargeNo;
                    AccountLog.Status = Status;
                    AccountLog.IsSuc = false;
                    AccountLog.Desc = "账户充值";
                    AccountLog.CreateTime = DateTime.Now;
                    AccountLog.UpdateTime = DateTime.Now;
                    AccountLog.IsDel = 0;
                    kyjUser.AddToDeveloper_AccountLog(AccountLog);
                    kyjUser.SaveChanges();
                    result = true;
                }
                catch (Exception e)
                {
                    Log4netService.RecordLog.RecordException("汪敏", String.Format("UserId:{0},price:{1},RechargeNo:{2},Status:{3}", UserId, price, RechargeNo, Status), e);
                    //throw;
                }
                return result;
            }
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
            var kyjUser = new kyj_NewHouseDBEntities();
            kyjUser.Connection.Open();
            using (var tran = kyjUser.Connection.BeginTransaction())
            {
                try
                {
                    var rechargeState = kyjUser.Developer_AccountLog.First(re => re.RechargeNo == RechargeNo);
                    if (rechargeState == null)
                    {
                        return false;
                    }
                    if (Status == 1 && rechargeState.Status != 1)
                    {
                        //Status充值状态
                        //0 待充值
                        //1 充值成功
                        //2 充值失败
                        rechargeState.Status = Status;
                        if (1 == rechargeState.Status) {
                            rechargeState.IsSuc = true;
                        }
                        kyjUser.SaveChanges();
                        TXOrm.Developer_Account uaccount = new Developer_Account();
                        int UserId = rechargeState.DeveloperId;
                        bool check = kyjUser.Developer_Account.Any(ua => ua.DeveloperId == UserId);
                        if (check)
                        {
                            uaccount = kyjUser.Developer_Account.First(ua => ua.DeveloperId == UserId);
                        }
                        uaccount.DeveloperId = UserId;
                        Decimal endPrice = uaccount.Price + price;
                        uaccount.Price = endPrice;
                        uaccount.UpdateTime = DateTime.Now;
                        if (!check)
                        {
                            //如果不存在就添加
                            kyjUser.AddToDeveloper_Account(uaccount);
                        }
                        kyjUser.SaveChanges();
                        AddUserAccountInfo(UserId, price, RechargeNo, Status);
                        tran.Commit();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    Log4netService.RecordLog.RecordException("汪敏", String.Format("RechargeNo:{0},Status:{1},price:{2}", RechargeNo, Status, price), e);
                    return false;
                }
                finally
                {
                    kyjUser.Connection.Close();
                }
            }
        }

        #endregion

        #region  提现
        /// <summary>
        /// 提现
        /// author:汪敏
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>执行错误返回:false</returns>
        public bool DrawCash(TXOrm.Developer_WithdrawCash model)
        {
            using (var newhouseDb = new kyj_NewHouseDBEntities())
            {
                bool result = false;
                try
                {
                    newhouseDb.AddToDeveloper_WithdrawCash(model);
                    newhouseDb.SaveChanges();
                    result = true;
                }
                catch (Exception e)
                {
                    Log4netService.RecordLog.RecordException("汪敏", String.Format("Price:{0},ProvinceName:{1},CityName:{2},BankName:{3},PubBankName:{4},RealName:{5},ALiPayAccount:{6}", model.Price, model.ProvinceName, model.CityName, model.BankName, model.PubBankName, model.RealName, model.ALiPayAccount), e);
                    //throw;
                }
                return result;
            }
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
            string[] temp = new string[] { };
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {

                    //余额
                    var query = from account in newhouseDb.Developer_Account
                                where account.DeveloperId == developerId && account.IsDel == 0
                                select new
                                {
                                    Price = account.Price
                                };
                    //账户充值总额
                    var query2 = from log in newhouseDb.Developer_AccountLog
                                 where log.DeveloperId == developerId && log.IsDel == 0 && log.IsSuc == true
                                 select new
                                 {
                                     Type = log.Type,
                                     Price = log.Price
                                 };
                    //提现总额
                    var query3 = from log in newhouseDb.Developer_AccountLog
                                 where log.DeveloperId == developerId && log.IsDel == 0 && log.Status == 3
                                 select new
                                 {
                                     Type = log.Type,
                                     Price = log.Price
                                 };
                    decimal TotalRecharge = 0;
                    decimal Totaldraw = 0;
                    if (query2.Count() > 0)
                    {
                        foreach (var item in query2.ToList())
                        {
                            if (item.Type == 3)
                            {
                                TotalRecharge = TotalRecharge + item.Price;
                            }
                        }
                    }
                    if (query3.Count() > 0)
                    {
                        foreach (var item in query3.ToList())
                        {
                            if (item.Type == 2)
                            {
                                Totaldraw = Totaldraw + item.Price;
                            }
                        }
                    }
                    temp = new[] { query.FirstOrDefault().Price.ToString(), TotalRecharge.ToString(), Totaldraw.ToString() };
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("developerId:{0}", developerId), e);
                //throw;
            }
            return temp;
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
            using (var kyjUser = new kyj_NewHouseDBEntities())
            {
                try
                {
                    return kyjUser.Developer_AccountLog.Max(ua => ua.Id);
                }
                catch (Exception e)
                {
                    Log4netService.RecordLog.RecordException("汪敏", null, e);
                    return 0;
                }
            }
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
            using (var kyjUser = new kyj_NewHouseDBEntities())
            {
                try
                {
                    Developer_AccountLog uaccountlog = new Developer_AccountLog();
                    uaccountlog.DeveloperId = UserId;
                    uaccountlog.Price = Price;
                    uaccountlog.PayType = 2;
                    uaccountlog.Type = 3;
                    uaccountlog.RechargeNo = RechargeNo;
                    uaccountlog.Status = Status;
                    uaccountlog.IsSuc = true;
                    uaccountlog.Desc = "账户充值";
                    uaccountlog.CreateTime = DateTime.Now;
                    uaccountlog.UpdateTime = DateTime.Now;
                    uaccountlog.IsDel = 0;
                    kyjUser.AddToDeveloper_AccountLog(uaccountlog);
                    kyjUser.SaveChanges();
                }
                catch (Exception e)
                {
                    Log4netService.RecordLog.RecordException("汪敏", String.Format("UserId:{0},Price:{1},RechargeNo:{2},Status:{3}", UserId, Price, RechargeNo, Status), e);
                    //throw;
                }
            }
        }
        #endregion
    }
}