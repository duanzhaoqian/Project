using System.Collections.Generic;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.BankAccounts;
using System;

namespace KYJ.ZS.DAL.BankAccounts
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：操作商家银行账户(数据库表M_BankAccounts)，包括查询、添加、修改
    /// </summary>
    public class BankAccountDal
    {
        #region 添加银行账户 +int Create(BankAccount bankAccount)
        /// <summary>
        /// Author：李晓波
        /// Time：2014-04-29
        /// Desc：添加银行账户    
        /// </summary>
        /// <param name="bankAccount">银行账户</param>
        /// <returns></returns>
        public int Create(BankAccount bankAccount)
        {
            try
            {
                #region SQL
                var sql = @"INSERT INTO [dbo].[M_BankAccounts]
                               ([merchant_id]
                               ,[mbank_number]
                               ,[mbank_name]
                               ,[mbank_bankname]
                               ,[mbank_remark]
                               ,[mbank_isdefault]
                               ,[mbank_createtime]
                               ,[mbank_updatetime])
                         VALUES
                               (@merchant_id
                               ,@mbank_number
                               ,@mbank_name
                               ,@mbank_bankname
                               ,@mbank_remark
                               ,@mbank_isdefault
                              ,getdate()
                               ,getdate())";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@merchant_id", bankAccount.MerchantId);
                param.AddParam("@mbank_number", bankAccount.Number);
                param.AddParam("@mbank_name", bankAccount.Name);
                param.AddParam("@mbank_bankname", bankAccount.BankName);
                param.AddParam("@mbank_remark", bankAccount.Remark);
                param.AddParam("@mbank_isdefault", bankAccount.IsDefault);

                #endregion

                #region 执行
                return KYJ_ZushouWDB.SqlExecute(sql, param);
                #endregion
            }
            catch (System.Exception e)
            {
                //记录日志
                Log4net.RecordLog.RecordException<BankAccount>("cheny", bankAccount, e);
                return 0;
            }

        }
        #endregion

        #region 修改银行账户  +int Update(BankAccount bankAccount)
        /// <summary>
        /// 修改银行账户
        /// </summary>
        /// <param name="bankAccount">银行账户</param>
        /// <returns></returns>
        public int Update(BankAccount bankAccount)
        {
            try
            {
                #region SQL
                var sql = @"UPDATE [dbo].[M_BankAccounts]
                        SET    [mbank_number] = @mbank_number
                               ,[mbank_name] = @mbank_name
                               ,[mbank_bankname]=@mbank_bankname
                               ,[mbank_remark] = @mbank_remark
                               ,[mbank_updatetime] = getdate()
                        WHERE  [mbank_id] = @mbank_id";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@mbank_id", bankAccount.Id);
                param.AddParam("@mbank_number", bankAccount.Number);
                param.AddParam("@mbank_name", bankAccount.Name);
                param.AddParam("@mbank_bankname", bankAccount.BankName);
                param.AddParam("@mbank_remark", bankAccount.Remark);
                #endregion

                #region 执行

                return KYJ_ZushouWDB.SqlExecute(sql, param);

                #endregion
            }
            catch (Exception e) 
            {
                Log4net.RecordLog.RecordException<BankAccount>("cheny", bankAccount, e);
                return 0;
            }
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
            try
            {
                #region SQL
                var sql = @"SELECT 
                               [mbank_id] as Id
                               ,[merchant_id] as MerchantId
                               ,[mbank_number] as Number
                               ,[mbank_name] as Name
                               ,mbank_bankname as BankName
                               ,[mbank_remark] as Remark
                               ,[mbank_isdefault] as IsDefault
                               ,[mbank_createtime] as CreateTime
                               ,[mbank_updatetime] as UpdateTime
                               ,[mbank_isdel] as IsDel
                         FROM  [dbo].[M_BankAccounts]
                         WHERE mbank_id = @mbank_id";
                #endregion

                var param = new SqlParam();
                param.AddParam("@mbank_id", bankAccountId);

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<BankAccount>.GetEntity(dt.Rows[0]);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny",bankAccountId.ToString(),e);
                return null;
            }
           
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
            try
            {
                #region SQL
                var sql = @"SELECT 
                               [mbank_id] as Id
                               ,[merchant_id] as MerchantId
                               ,[mbank_number] as Number
                               ,[mbank_name] as Name
                               ,[mbank_bankname] as BankName
                               ,[mbank_remark] as Remark
                               ,[mbank_isdefault] as IsDefault
                               ,[mbank_createtime] as CreateTime
                               ,[mbank_updatetime] as UpdateTime
                               ,[mbank_isdel] as IsDel
                         FROM  [dbo].[M_BankAccounts]
                         WHERE merchant_id = @merchant_id and  mbank_isdel='false'";
                #endregion

                var param = new SqlParam();
                param.AddParam("@merchant_id", merchantId);

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<BankAccount>.GetEntityList(dt);
            }
            catch (Exception e) 
            {
                Log4net.RecordLog.RecordException("cheny", merchantId.ToString(), e);
                return null;
            }
        }
        #endregion

        #region 删除银行账户 +int Delete(int bankId)
        /// <summary>
        /// 删除银行账户
        /// </summary>
        /// <param name="bankId">银行账户ID</param>
        /// <returns></returns>
        public int Delete(int bankId)
        {
            try
            {
                var sql = @"UPDATE  [dbo].[M_BankAccounts] SET mbank_isdel='true'
                      WHERE        mbank_id = @mbank_id";

                var param = new SqlParam();
                param.AddParam("@mbank_id", bankId);
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", bankId.ToString(), e);
                return 0;
            }
            
        }
        #endregion

        #region 删除商户所有银行账户 +int DelByMerchantID(int merchantId)
        /// <summary>
        /// 删除商户所有银行账户
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public int DelByMerchantID(int merchantId)
        {
            try
            {
                var sql = @"DELETE FROM  [dbo].[M_BankAccounts]
                      WHERE        merchant_id = @merchant_id";

                var param = new SqlParam();
                param.AddParam("@merchant_id", merchantId);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny",merchantId.ToString(),e);
                return 0;
            }
        }
        #endregion

        #region 获取商户银行账号列表以及账号金额 +List<BankAccountEntity> GetBankList(int merchantId)
        /// <summary>
        /// Author：李晓波
        /// Time：2014-04-28
        /// Desc：获取商户银行账号列表以及账号金额
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public List<BankAccountEntity> GetBankList(int merchantId)
        {
            try
            {
                #region SQL
                var sql = @"SELECT 
                                mb.merchant_id as MerchantId,
                                mb.mbank_id as Id,
                                mb.mbank_name as Name,
                                mb.mbank_number as Number,
                                mb.mbank_bankname as BankName,
                                mb.mbank_remark as Remark
                         FROM  M_BankAccounts mb
                         WHERE mb.merchant_id=@merchant_id and mb.mbank_isdel='false' order by mb.mbank_updatetime desc";

                #endregion
                var param = new SqlParam();
                param.AddParam("@merchant_id", merchantId);
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<BankAccountEntity>.GetEntityList(dt);
            }
            catch(Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", merchantId.ToString(), e);
                return null;
            }
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
            try
            {
                var sql = @" select account_price from  M_Accounts where merchant_id=@merchant_id";
                var param = new SqlParam();
                param.AddParam("@merchant_id", id);
                return KYJ_ZushouRDB.GetFirst(sql, param);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny",id.ToString(),e);
                return string.Empty;
            }
        }
        #endregion

        #region 商户提现 +int ApplyFor(int id, BankAccountEntity mdlAccountEntity, decimal withdraw, decimal withdrawbalance, string orderNumber)
        /// <summary>
        /// Author：李晓波
        /// Time：2014-05-05
        /// Desc：商户提现  
        /// <para>20140528新添加字段：</para>
        /// <para>withdraw_number:提现单号</para>
        /// <para>withdraw_balance:提现后的账户余额</para>
        /// </summary>
        /// <param name="id">商户id</param>
        /// <param name="withdraw">提现金额</param>
        /// <returns></returns>
        public int ApplyFor(int id, BankAccountEntity mdlAccountEntity, decimal withdraw, decimal withdrawbalance, string orderNumber)
        {
            try
            {
                #region SQL
                var sql = string.Empty;
                //商户提现，更新商户账户表->更新商户提现表->更新商户账户日志表

                /*
                 * 商户账户表 M_Accounts
                        merchant_id(商户ID)、account_price(账户金额)、account_backprice(上次提现金额)、account_drawnum(提现次数)、account_drawtotalpricce(提现总金额)
                   商户提现表 M_WithdrawCashs
                        merchant_id(商户ID)、withdraw_price(提现金额)
                   商户账户日志表 M_AccountLogs
                        merchant_id(商户ID)、mlog_price(金额)
                 */

                sql = @"BEGIN TRANSACTION
                            --更新商户账户表--
                           UPDATE M_Accounts
                           SET account_price=account_price-@withdraw
                            ,account_backprice=@withdraw
                            ,account_drawnum=account_drawnum+1
                            ,account_drawtotalprice=account_drawtotalprice+@withdraw    
                            ,account_updatetime=getdate()
                            WHERE merchant_id=@merchant_id
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                              --更新商户提现表--
                            INSERT INTO M_WithdrawCashs
                            (
                            merchant_id
                            ,withdraw_state
                            ,withdraw_type
                            ,mbank_number
                            ,mbank_name
                            ,mbank_bankname
                            ,withdraw_price
                            ,withdraw_createtime
                            ,withdraw_updatetime
                            ,mbank_remark
                            ,withdraw_isdel
                            ,withdraw_number
                            ,withdraw_balance
                            ) 
                            values(@merchant_id,1,1,@mbank_number,@mbank_name,@mbank_bankname,@withdraw,getdate(),getdate(),@mbank_remark,'false',@withdraw_number,@withdraw_balance)
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                              --更新商户账户日志表--
                             INSERT INTO M_AccountLogs
                            (
                            merchant_id
                            ,mlog_type
                            ,mlog_state
                            ,mlog_price
                            ,mlog_desc
                            ,mlog_createtime
                            )
                            values(@merchant_id,2,1,@withdraw,@mlog_desc,getdate())
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                               --更新商户账户冻结金额--
                            INSERT INTO M_FreezePrice
                            (
                                account_id
                                , freezeprice_price
                                , freezeprice_state
                                , freezeprice_isdel
                                , freezeprice_createtime
                                , freezeprice_updatetime
                                , freezeprice_handlestate
                            ) 
                            values(@merchant_id,@withdraw,1,'false',getdate(),getdate(),'false')
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                            SELECT  1
                            COMMIT TRANSACTION";

                #endregion

                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@merchant_id", id);
                param.AddParam("@withdraw", withdraw);
                param.AddParam("@mbank_number", mdlAccountEntity.Number);
                param.AddParam("@mbank_name", mdlAccountEntity.Name);
                param.AddParam("@mbank_bankname", mdlAccountEntity.BankName);
                param.AddParam("@mbank_remark", mdlAccountEntity.Remark);   //20140527 add by cy

                param.AddParam("@withdraw_number", orderNumber);   //提现单号  20140528 add by cy  
                param.AddParam("@withdraw_balance", withdrawbalance);   //提现后的账户余额  20140528 add by cy

                param.AddParam("@mlog_desc", mdlAccountEntity.Desc);

                #endregion

                #region 执行
                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
                #endregion
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException<BankAccountEntity>("cheny", mdlAccountEntity, e);
                return 0;
            }
        }
        #endregion

        #region 获取账户状态 +bool GetAccountStatus(int id)
        /// <summary>
        /// 获取账户状态
        /// </summary>
        /// <param name="id"></param>
        public bool GetAccountStatus(int id)
        {
            try
            {
                //var sql = @" select account_isfreeze from M_Accounts where merchant_id=@merchant_id";
                var sql = @" select count(*) from M_FreezePrice where account_id=@account_id and freezeprice_handlestate='false'";
                var param = new SqlParam();
                param.AddParam("@account_id", id);
                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param)) > 0;
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return false;
            }
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
            try
            {
                #region SQL  modify by cy
                //20140603 modify by cy   添加了按照 merchant_id 字段倒序排列（避免数据库里面有脏数据）
                var sql = @"select  
                        mbank_number as Number
                        ,mbank_name as Name
                        ,mbank_bankname as BankName
                        ,withdraw_price as Price
                         from M_WithdrawCashs where merchant_id=@merchant_id  and withdraw_state=1 and withdraw_isdel='false' ORDER BY withdraw_id DESC";
                #endregion
                var param = new SqlParam();
                param.AddParam("@merchant_id", id);
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<BankAccountEntity>.GetEntity(dt.Rows[0]);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return null;
            }
        }
        #endregion

        #region 充值 +int Recharge(int id, decimal rechargeAmount)
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="id">商户ID</param>
        /// <param name="rechargeAmount">充值金额</param>
        /// <returns></returns>
        public int Recharge(int id, decimal rechargeAmount)
        {
            try
            {
                #region SQL
                string sql = @"BEGIN TRANSACTION
                            --更新商户账户表--
                            UPDATE M_Accounts
                            SET
	                            account_price = account_price + @rechargeAmount
	                            , account_backprice = @rechargeAmount
	                            , account_drawnum = account_drawnum + 1
	                            , account_drawtotalprice = account_drawtotalprice + @rechargeAmount
	                            , account_updatetime = getdate()
                            WHERE
	                            merchant_id = @merchantId
                            IF @@ERROR<>0
                            BEGIN
                            ROLLBACK TRANSACTION
                            SELECT 0
                            END
                            --更新商户账户日志表--
                            INSERT
                            INTO
	                            M_AccountLogs
	                            (
	                            merchant_id
                              , mlog_type
                              , mlog_state
                              , mlog_price
                              , mlog_desc
                              , mlog_createtime
	                            )
                            VALUES
	                            (@merchantId, 1, 2, @rechargeAmount, @desc, getdate())
                            IF @@ERROR<>0
                            BEGIN
                            ROLLBACK TRANSACTION
                            SELECT 0
                            END
                            SELECT 1
                            COMMIT TRANSACTION";
                #endregion

                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@merchantId", id);  //ID
                param.AddParam("@rechargeAmount", rechargeAmount);   //充值金额
                param.AddParam("@desc", "商户充值了" + rechargeAmount + "元");
                #endregion

                #region 执行
                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
                #endregion
            }
            catch (Exception e)
            {
                string tmp = string.Format("({0},{1})", id, rechargeAmount);
                Log4net.RecordLog.RecordException("cheny", tmp, e);
                return 0;
            }
        } 
        #endregion
    }
}
