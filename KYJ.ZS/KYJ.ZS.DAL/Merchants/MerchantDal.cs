using System;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Merchants;
using System.Collections.Generic;

namespace KYJ.ZS.DAL.Merchants
{
    /// <summary>
    /// 作者：maq
    /// 时间:2014年4月24日
    /// 描述：商户基本信息数据访问类
    /// </summary>
    public partial class MerchantDal
    {
        /// <summary>
        /// 根据登陆名获取实体
        /// </summary>
        /// <param name="loginName">登陆名</param>
        /// <returns></returns>
        public Merchant GetModel(string loginName)
        {
            Merchant merchant = null;
            #region SQL+参数
            string strSql = @"SELECT [merchant_id]  as Id
                                  ,[merchant_loginname] as LoginName
                                  ,[merchant_email] as Email
                                  ,[merchant_mobile] as Moblie
                                  ,[merchant_pwd] as Pwd
                                  ,[merchant_paypwd] as PayPwd
                                  ,[merchant_name] as Name
                                  ,[merchant_realname] as RealName
                                  ,[merchant_card] as Card
                                  ,[merchant_state] as State
                                  ,[merchant_rank] as Rank
                                  ,[merchant_status] as Status
                                  ,[merchant_guid] as Guid
                                  ,[merchant_createtime] as CreateTime
                                  ,[merchant_updatetime] as UpdateTime
                                  ,[merchant_lastlogintime] as LastLoginName
                                  ,[merchant_isdel] as IsDel
                                  ,[admin_id] as AdminId
                                  ,[admin_name] as AdminName
                              FROM [Merchants] where merchant_isdel=0 and merchant_loginname=@LoginName";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@LoginName", loginName);
            #endregion
            #region 处理
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql, sqlParam))
            {
                if (table.Rows.Count > 0)
                {
                    merchant = DataHelper<Merchant>.GetEntity(table.Rows[0]);
                }
            }
            #endregion
            return merchant;
        }
        /// <summary>
        /// 更新最后登录时间
        /// </summary>
        /// <returns></returns>
        public int UpDatelastLoginTime(int merId)
        {
            #region Sql
            string strSql = "update Merchants set merchant_lastlogintime=@t where merchant_id =@id";
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@t", DateTime.Now);
            sqlParam.AddParam("@id", merId);
            #endregion

            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam);
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>true 存在，false不存在</returns>
        public Boolean IsExist(int userId)
        {
            string strSql = " select merchant_id from Merchants where merchant_id=@id and merchant_isdel=0";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", userId);
            string result = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
            if (!string.IsNullOrEmpty(result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-24
        /// 检查当前商户输入密码是否正确
        /// </summary>    
        /// <param name="pwd">商户id</param>       
        /// <returns></returns>
        public string getPwd(int id)
        {
            string strSql = " select merchant_pwd from Merchants where merchant_id=@merchant_id and merchant_isdel=0";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@merchant_id", id);
            return KYJ_ZushouRDB.GetFirst(strSql, sqlParam);

        }

        public int UpdatePwd(int id, string oldPwd, string newPwd)
        {

            #region SQL
            var sql = @"IF EXISTS ( SELECT  [merchant_id]
                                    FROM    [dbo].[Merchants]
                                    WHERE   [merchant_id] = @merchant_id
                                            AND [merchant_pwd] = @oldpwd ) 
                            BEGIN
                                UPDATE  [dbo].[Merchants]
                                SET     [merchant_pwd] = @newPwd
                                WHERE   [merchant_id] = @merchant_id
                                SELECT  1
                            END
                        ELSE 
                            BEGIN
                                SELECT  0  
                            END    ";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@merchant_id", id);
            param.AddParam("@oldpwd", oldPwd);
            param.AddParam("@newPwd", newPwd);

            #endregion

            #region 执行

            var result = KYJ_ZushouWDB.SqlExecute(sql, param);

            #endregion
            return result;
            //string strSql = " update  Merchants set merchant_pwd=@merchant_pwd where merchant_id=@merchant_id and merchant_isdel=0";
            //SqlParam sqlParam = new SqlParam();
            //sqlParam.AddParam("@merchant_id", id);
            //sqlParam.AddParam("@merchant_pwd", newPwd);
            //return KYJ_ZushouRDB.SqlExecute(strSql, sqlParam);
        }

        #region 商户管理----ningjd

        /// <summary>
        /// 账户验证
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int ValidateLoginName(string loginName)
        {
            try
            {
                var sql = "select count(1) from Merchants(nolock) where merchant_loginname=@merchant_loginname";
                var param = new SqlParam();
                param.AddParam("@merchant_loginname", loginName);
                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", loginName, ex);
                return 1;
            }
        }

        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="entity">商户添加Entity</param>
        /// <returns></returns>
        public int Create(MerchantCreateEntity entity)
        {
            try
            {
                #region SQL
                var sql = @"BEGIN TRAN
                        DECLARE @merchantId int, --商户ID
			                    @results int --执行结果，0失败，1成功

                        /*商户信息*/
                        INSERT INTO [Merchants]
                            ([merchant_loginname]
                            ,[merchant_pwd]
                            ,[merchant_name]
                            ,[merchant_guid]
                            ,[merchant_createtime]
                            ,[merchant_updatetime]
                            ,[admin_id]
                            ,[admin_name]
                            ,[merchant_mobile]
                            ,[merchant_lastlogintime])
                        VALUES
                            (@merchant_loginname
                            ,@merchant_pwd
                            ,@merchant_name
                            ,@merchant_guid
                            ,@create_time
                            ,@create_time
                            ,@admin_id
                            ,@admin_name
                            ,'',@create_time)
                        SELECT @merchantId=@@IDENTITY

                        /*商户扩展*/
                        INSERT INTO [MerchantOthers]
                            ([merchant_id]
                            ,[merchantother_introduction]
                            ,[merchantother_operatecategory])
                        VALUES
                            (@merchantId
                            ,@merchantother_introduction
                            ,@merchantother_operatecategory)

                        /*商户账户*/
                        INSERT INTO [M_Accounts]
                            ([merchant_id]
                            ,[account_price]
                            ,[account_backprice]
                            ,[account_drawnum]
                            ,[account_drawtotalprice]
                            ,[account_updatetime])
                        VALUES
                            (@merchantId
                            ,0
                            ,0
                            ,0
                            ,0
                            ,@create_time)

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
                param.AddParam("@merchant_loginname", entity.LoginName);
                param.AddParam("@merchant_pwd", entity.Pwd);
                param.AddParam("@merchant_name", entity.Name);
                param.AddParam("@merchant_guid", entity.Guid);
                param.AddParam("@create_time", DateTime.Now);
                param.AddParam("@admin_id", entity.AdminId);
                param.AddParam("@admin_name", entity.AdminName);
                param.AddParam("@merchantother_introduction", entity.Introduction);
                param.AddParam("@merchantother_operatecategory", entity.OperateCategory);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取商户管理列表
        /// </summary>
        /// <param name="name">企业名称</param>
        /// <param name="loginName">企业账号</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<MerchantIndexEntity> GetMerchantList(string name, string loginName, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                // 表名
                string tableName = "Merchants(NOLOCK) t1 left join MerchantOthers(NOLOCK) t2 on t1.merchant_id=t2.merchant_id";
                // 查询条件
                string where = " merchant_isdel = 'false'";
                // 企业名称过滤
                if (!string.IsNullOrEmpty(name))
                    where += " and merchant_name like '%" + name.Trim() + "%'";
                // 企业账号过滤
                if (!string.IsNullOrEmpty(loginName))
                    where += " and merchant_loginname like '%" + loginName.Trim() + "%'";
                // 排序
                string orderBy = " merchant_loginname";
                // 列名
                string columnList = "t1.[merchant_id] as Id,t1.[merchant_loginname] as LoginName,t1.[merchant_createtime] as CreateTime,t1.[merchant_guid] as Guid,t1.[merchant_name] as Name,t2.[merchantother_introduction] as Introduction";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<MerchantIndexEntity>.GetEntityList(dt);
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
        /// 获取商户名称
        /// </summary>
        /// <param name="id">商户ID</param>
        /// <returns></returns>
        public string GetMerchantName(int id)
        {
            try
            {
                var sql = @"select merchant_name from Merchants(NOLOCK) where merchant_id=@merchant_id";
                var param = new SqlParam();
                param.AddParam("@merchant_id", id);
                return KYJ_ZushouRDB.GetFirst(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return "";
            }
        }

        #endregion
    }
}