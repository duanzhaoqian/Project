using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXDal.FinancialData
{
    /// <summary>
    /// 体现管理
    /// </summary>
    public class WithdrawCashDal : BaseDal_Admin
    {

        /// <summary>
        /// 获取提现列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_WithdrawCash> GetPageList_BySearch_WithdrawCash(PVS_NH_WithdrawCash search, int pageIndex, int pageSize)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  z.Id, z.CreateTime, z.LoginName, z.Mobile, z.Price, z.ReceiveType, z.RealName, z.ALiPayAccount, z.CityId, z.CityName,
        z.ProvinceId, z.ProvinceName, z.BankName, z.PubBankName, z.BankAccount, z.[Status]
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.Id ASC ) AS rowid, a.Id, a.CreateTime, b.LoginName, b.Mobile, a.Price,
                    a.ReceiveType, a.RealName, a.ALiPayAccount, a.CityId, a.CityName, a.ProvinceId, a.ProvinceName,
                    a.BankName, a.PubBankName, a.BankAccount, a.[Status]
          FROM      Developer_WithdrawCash (NOLOCK) AS a
                    INNER JOIN Developer (NOLOCK) AS b ON b.Id = a.DeveloperId
                                                          AND b.IsDel = 0
          WHERE     1 = 1
                    {2}
                    {1}
                    {0}
        ) AS z
WHERE   z.rowid BETWEEN {3} AND {4}";

                    sql = string.Format(sql, GetParms_BySearch_WithdrawCash(search, pageIndex, pageSize));

                    var list = db.ExecuteStoreQuery<PVM_NH_WithdrawCash>(sql).ToList();

                    return list;

                }
            }
            catch (SqlException ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取提现记录总数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_WithdrawCash(PVS_NH_WithdrawCash search)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  COUNT(1) AS Result
FROM    Developer_WithdrawCash (NOLOCK) AS a
        INNER JOIN Developer (NOLOCK) AS b ON b.Id = a.DeveloperId
                                                AND b.IsDel = 0
WHERE   1 = 1
        {2}
        {1}
        {0}";
                    sql = string.Format(sql, GetParms_BySearch_WithdrawCash(search));

                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取提现记录搜索条件
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private object[] GetParms_BySearch_WithdrawCash(PVS_NH_WithdrawCash search, int pageIndex = 0, int pageSize = 0)
        {
            var time = new StringBuilder();

            if (!string.IsNullOrEmpty(search.BeginTime))
            {
                time.Append("AND (");
                time.AppendFormat("'{0}' < a.CreateTime", search.BeginTime);
                if (!string.IsNullOrEmpty(search.EndTime))
                {
                    time.AppendFormat(" AND a.CreateTime < '{0}')", search.EndTime);
                }
                else
                {
                    time.Append(")");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(search.EndTime))
                {
                    time.AppendFormat("AND (a.CreateTime < '{0}')", search.EndTime);
                }
            }
            
            var list = new List<object>
            {
                (-1 < search.Status ? string.Format("AND a.[Status] = {0}", search.Status) : string.Empty),
                (string.IsNullOrEmpty(search.KeyWord) ? string.Empty : string.Format("AND ( b.Mobile LIKE '{0}%' OR b.LoginName LIKE '{0}%' )", search.KeyWord)),
                time
            };
            

            if (pageIndex > 0
                && pageSize > 0)
            {
                int startIndex = (pageIndex - 1) * pageSize + 1;
                int endIndex = pageIndex * pageSize;

                list.Add(startIndex);
                list.Add(endIndex);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 审批通过
        /// 李雨钊
        /// </summary>
        /// <param name="id">提现记录编号</param>
        /// <param name="status">提现记录状态</param>
        /// <param name="adminId">操作员编号</param>
        /// <param name="adminName">操作员名称</param>
        /// <returns></returns>
        public int UpdateStatus(int id, int status, int adminId, String adminName)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var withdrawCash = db.Developer_WithdrawCash.SingleOrDefault(it => it.Id == id);
                    if (withdrawCash != null)
                    {
                        withdrawCash.Status = status;
                        withdrawCash.AdminId = adminId;
                        withdrawCash.AdminName = adminName;
                    }
                    return db.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", String.Format("({0},{1},{2},{3})", id, status, adminId, adminName), ex);
                return -1;
            }
        }

        /// <summary>
        /// 提现打款成功
        /// 李雨钊
        /// </summary>
        /// <param name="id">提现编号</param>
        /// <param name="status">提现状态</param>
        /// <param name="adminId">操作员Id</param>
        /// <param name="adminName">操作员姓名</param>
        /// <param name="developerId">提现用户Id</param>
        /// <param name="price">提现金额</param>
        /// <returns></returns>
        public int UpdateSuccess(int id, int status, int adminId, String adminName, int developerId, decimal price)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = @"
BEGIN TRY
    BEGIN TRAN
    UPDATE  Developer_WithdrawCash
    SET     AdminId = {0}, AdminName = '{1}', Status = {2}
    WHERE   Id = {3}

    UPDATE  Developer_Account
    SET     UpdateTime = GETDATE(), Price = Price + {4}
    WHERE   DeveloperId = {5}
    COMMIT
    
    SELECT  '1' AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    SELECT  '0' AS Code, '操作失败' AS Msg
    ROLLBACK TRAN
END CATCH";
                    sql = string.Format(sql, new object[]
                    {
                        adminId,
                        adminName,
                        status,
                        id,
                        price,
                        developerId
                    });

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Code);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", String.Format("(id:{0},status:{1},adminid:{2},adminname:{3},developerid:{4},price:{5})", id, status, adminId, adminName, developerId, price), ex);
                return 0;
            }
        }

        /// <summary>
        /// 提现审批未通过
        /// 李雨钊
        /// </summary>
        /// <param name="id">提现编号</param>
        /// <param name="status">提现状态</param>
        /// <param name="adminId">操作员Id</param>
        /// <param name="adminName">操作员姓名</param>
        /// <param name="developerId">提现用户Id</param>
        /// <param name="price">提现金额</param>
        /// <returns></returns>
        public int UpdateFail(int id, int status, int adminId, String adminName, int developerId, decimal price)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = @"
BEGIN TRY
    BEGIN TRAN
    UPDATE  Developer_WithdrawCash
    SET     AdminId = {0}, AdminName = '1', Status = {2}
    WHERE   Id = {3}

    UPDATE  Developer_Account
    SET     UpdateTime = GETDATE(), Price = Price + {4}
    WHERE   DeveloperId = {5}

    INSERT  INTO Developer_AccountLog ( DeveloperId, Price, PayType, Type, RechargeNo, Status, IsSuc, [Desc], CreateTime,
                                        UpdateTime, IsDel )
    VALUES  ( {5}, {4}, 2, 2, N'', 1, 1, N'提现失败退款', GETDATE(), GETDATE(), 0 )
    COMMIT
    
    SELECT  '1' AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    SELECT  '0' AS Code, '操作失败' AS Msg
    ROLLBACK TRAN
END CATCH";
                    sql = string.Format(sql, new object[]
                    {
                        adminId,
                        adminName,
                        status,
                        id,
                        price,
                        developerId
                    });

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Code);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", String.Format("(id:{0},status:{1},adminid:{2},adminname:{3},developerid:{4},price:{5})", id, status, adminId, adminName, developerId, price), ex);
                return 0;
            }
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<PVM_NH_WithdrawCash> GetWithdrawCash_Export(PVS_NH_WithdrawCash search)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
 SELECT a.CreateTime, b.LoginName, b.Mobile, a.Price, a.ReceiveType, a.RealName, a.ALiPayAccount, a.CityId, a.CityName,
        a.ProvinceId, a.ProvinceName, a.BankName, a.PubBankName, a.BankAccount
 FROM   Developer_WithdrawCash (NOLOCK) AS a
        INNER JOIN Developer (NOLOCK) AS b ON b.Id = a.DeveloperId
                                              AND b.IsDel = 0
 WHERE  1 = 1
        {2}
        {1}
        {0}";

                    sql = string.Format(sql, GetParms_BySearch_WithdrawCash(search));

                    var list = db.ExecuteStoreQuery<PVM_NH_WithdrawCash>(sql).ToList();

                    return list;

                }
            }
            catch (SqlException ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据编号获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetEntity_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Developer_WithdrawCash.SingleOrDefault(it => it.Id == id);
                }
            }
            catch (SqlException ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format(@"id={0}", id), ex);
                return null;
            }
        }
    }
}