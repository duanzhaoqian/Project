using System;
using System.Collections.Generic;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.WithdrawCashs;

namespace KYJ.ZS.DAL.WithdrawCashs
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：操作商户提现(数据库表M_WithdrawCashs)，包括查询、添加、修改
    /// </summary>
    public class WithdrawCashDal
    {
        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="withdrawCash">商户提现</param>
        /// <returns></returns>
        public int Create(WithdrawCash withdrawCash)
        {
            #region SQL
            var sql = @"INSERT INTO [dbo].[M_WithdrawCashs]
                               ([merchant_id]
                               ,[withdraw_state]
                               ,[withdraw_type]
                               ,[mbank_number]
                               ,[mbank_name]
                               ,[mbank_bankname]
                               ,[withdraw_price]
                               ,[withdraw_createtime]
                               ,[withdraw_updatetime])
                         VALUES
                               (@merchant_id
                               ,@withdraw_state
                               ,@withdraw_type
                               ,@mbank_number
                               ,@mbank_name
                               ,@mbank_bankname
                               ,@withdraw_price
                               ,@withdraw_createtime
                               ,@withdraw_updatetime)";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@merchant_id", withdrawCash.MerchantId);
            param.AddParam("@withdraw_state", withdrawCash.State);
            param.AddParam("@withdraw_type", withdrawCash.Type);
            param.AddParam("@mbank_number", withdrawCash.Number);
            param.AddParam("@mbank_name", withdrawCash.Name);
            param.AddParam("@mbank_bankname", withdrawCash.BankName);
            param.AddParam("@withdraw_price", withdrawCash.Price);
            param.AddParam("@withdraw_createtime", withdrawCash.CreateTime);
            param.AddParam("@withdraw_updatetime", withdrawCash.UpdateTime);

            #endregion

            #region 执行

            return KYJ_ZushouWDB.SqlExecute(sql, param);

            #endregion
        }

        /// <summary>
        /// 修改提现信息
        /// </summary>
        /// <param name="withdrawCashId">提现表ID</param>
        /// <param name="state">提现状态：0 未知，1 待审批，2 审批通过待打款，3 审批未通过，4 打款成功，5 打款失败</param>
        /// <param name="updateTime">更新时间</param>
        /// <returns></returns>
        public int Update(int withdrawCashId, int state, DateTime updateTime)
        {
            #region SQL
            var sql = @"Update [dbo].[M_WithdrawCashs]
                        SET    [withdraw_state] = @withdraw_state
                               ,[withdraw_updatetime] = @withdraw_updatetime
                        WHERE  [withdraw_id] = @withdraw_id";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@withdraw_id", withdrawCashId);
            param.AddParam("@withdraw_state", state);
            param.AddParam("@withdraw_updatetime", updateTime);

            #endregion

            #region 执行

            return KYJ_ZushouWDB.SqlExecute(sql, param);

            #endregion
        }

        /// <summary>
        /// 获取提现信息
        /// </summary>
        /// <param name="withdrawCashId">提现记录ID</param>
        /// <returns></returns>
        public WithdrawCash GetWithdrawCash(int withdrawCashId)
        {
            try
            {
                #region SQL
                var sql = @"SELECT 
                               [withdraw_id] as Id
                               ,[merchant_id] as MerchantId
                               ,[withdraw_state] as State
                               ,[withdraw_type] as Type
                               ,[mbank_number] as BandNumber
                               ,[mbank_name] as Name
                               ,[mbank_bankname] as BankName
                               ,[withdraw_price] as Price
                               ,[withdraw_createtime] as CreateTime
                               ,[withdraw_updatetime] as UpdateTime
                               ,[withdraw_isdel] as IsDel
                               ,[withdraw_number] as Number
                               ,[withdraw_rejectreason] as Rejectreason
                               ,[withdraw_balance] as Balance
                               ,[mbank_remark] as Remark
                        FROM   [dbo].[M_WithdrawCashs](NOLOCK)
                        WHERE  [withdraw_id] = @withdraw_id";
                #endregion

                var param = new SqlParam();
                param.AddParam("@withdraw_id", withdrawCashId);

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<WithdrawCash>.GetEntity(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", withdrawCashId, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取提现记录
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
            try
            {
                #region SQL
                //表名
                string tableName = "M_WithdrawCashs(NOLOCK)";
                //查询条件
                var strWhere = "[withdraw_isdel]='false'"
                            + (merchantId.HasValue ? " and [merchant_id] =" + merchantId.Value : "")
                            + (startDate.HasValue ? " and [withdraw_createtime]>=" + startDate.Value : "")
                            + (endDate.HasValue ? " and [withdraw_createtime]<=" + endDate.Value : "")
                            + (areaEnum == WithdrawCashAreaEnum.All ? "" : " and [withdraw_state]=" + (int)areaEnum);
                //排序
                string orderBy = " withdraw_updatetime desc";
                //列名
                string columnList = "[withdraw_id] as Id,[merchant_id] as MerchantId,[withdraw_state] as State,[withdraw_type] as Type"
                                + ",[mbank_number] as BandNumber,[mbank_name] as Name,[mbank_bankname] as BankName,[withdraw_price] as Price"
                                + ",[withdraw_createtime] as CreateTime,[withdraw_updatetime] as UpdateTime,[withdraw_isdel] as IsDel"
                                + ",[withdraw_number] as Number,[withdraw_rejectreason] as Rejectreason,[withdraw_balance] as Balance,[mbank_remark] as Remark";
                #endregion

                DataTable dt = KYJ_ZushouRDB.GetPages(index, strWhere, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<WithdrawCash>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalPage = 0;
                totalRecord = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <param name="reason">驳回理由</param>
        /// <returns></returns>
        public int Reject(int id, string reason)
        {
            try
            {
                #region SQL
                var sql = @"begin tran
			            DECLARE @results int, --执行结果，0失败，1成功
                        @merchantId int,    --商户ID
                        @price money    --提现金额

                        SET @merchantId=(SELECT merchant_id FROM M_WithdrawCashs WHERE withdraw_id=@withdraw_id)
                        SET @price=(SELECT withdraw_price FROM M_WithdrawCashs WHERE withdraw_id=@withdraw_id)

                        /*提现记录表*/
                        UPDATE  [dbo].[M_WithdrawCashs]
                        SET     [withdraw_state] = 3
                                ,[withdraw_rejectreason] = @withdraw_rejectreason
                                ,[withdraw_updatetime] = @updateTime
                        WHERE withdraw_id = @withdraw_id

                        /*商户账户*/
                        UPDATE  [dbo].[M_Accounts]
                        SET     [account_price] = [account_price]+@price
                                ,[account_updatetime] = @updateTime
                        WHERE merchant_id = @merchantId
                        
                        /*商户账户冻结金额*/
                        UPDATE  [dbo].[M_FreezePrice]
                        SET     [freezeprice_handlestate] = 'true'
                                ,[freezeprice_updatetime] = @updateTime
                        WHERE account_id = @merchantId and freezeprice_state=1
                        
                        /*商户账户日志*/
                        INSERT INTO [dbo].[M_AccountLogs]
                                    (merchant_id
                                    ,mlog_type
                                    ,mlog_state
                                    ,mlog_price
                                    ,mlog_desc
                                    ,mlog_createtime)
                        VALUES  (@merchantId,4,2,@price,'驳回提现金额'+cast(@price as nvarchar)+'元',@updateTime)

                        /*判断事务回滚或提交*/
	                    if @@error<>0 --判断有任何一条出现错误
	                    begin 
		                    rollback tran --开始执行事务的回滚
		                    set @results=0  
	                    end
	                    else
	                    begin
		                    commit tran --执行这个事务的操作
		                    set @results=1 
	                    end
                        /*返回执行结果*/
                        select  @results";
                #endregion
                var param = new SqlParam();
                param.AddParam("@withdraw_id", id);
                param.AddParam("@withdraw_rejectreason", reason);
                param.AddParam("@updateTime", DateTime.Now);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id + "," + reason, ex);
                return 0;
            }
        }

        /// <summary>
        /// 确认提现
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <returns></returns>
        public int ConfirmWithdrawal(int id)
        {
            try
            {
                #region SQL
                var sql = @"begin tran
			            DECLARE @results int, --执行结果，0失败，1成功
                        @merchantId int,    --商户ID
                        @price money    --提现金额

                        SET @merchantId=(SELECT merchant_id FROM M_WithdrawCashs WHERE withdraw_id=@withdraw_id)

                        /*提现记录表*/
                        UPDATE  [dbo].[M_WithdrawCashs]
                        SET     [withdraw_state] = 4
                                ,[withdraw_updatetime] = @updateTime
                        WHERE withdraw_id = @withdraw_id
                        
                        /*商户账户冻结金额*/
                        UPDATE  [dbo].[M_FreezePrice]
                        SET     [freezeprice_handlestate] = 'true'
                                ,[freezeprice_updatetime] = @updateTime
                        WHERE account_id = @merchantId and freezeprice_state=1

                        /*判断事务回滚或提交*/
	                    if @@error<>0 --判断有任何一条出现错误
	                    begin 
		                    rollback tran --开始执行事务的回滚
		                    set @results=0  
	                    end
	                    else
	                    begin
		                    commit tran --执行这个事务的操作
		                    set @results=1 
	                    end
                        /*返回执行结果*/
                        select  @results";
                #endregion
                var param = new SqlParam();
                param.AddParam("@withdraw_id", id);
                param.AddParam("@updateTime", DateTime.Now);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return 0;
            }
        }
    }
}
