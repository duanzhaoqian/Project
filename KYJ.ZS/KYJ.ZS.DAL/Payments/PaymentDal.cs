using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Payments;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.Payments
{
    /// <summary>
    /// Auther:zhuzh
    /// Date  :2014-5-19
    /// Desc  :支付端口专用DAL
    /// </summary>
    public class PaymentDal
    {
        /// <summary>
        /// 获取指定订单编号的订单需支付金额信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public PaymentEntity GetPaymentByOrderNum(string ordernum, int userid)
        {
            #region SQL
            var sql = @"SELECT TOP 1 [order_id] ,
                                [user_id] ,
                                [oper_id],
                                [order_type],
                                [order_number] ,
                                [order_totalprice] ,
                                [order_totaldeposit] ,
                                [order_monthprice] ,
                                [order_freightcost]
                        FROM    [dbo].[Orders](NOLOCK)
                        WHERE   [order_state] = 1
                                AND [order_isdel] = 0
                                AND [order_number] = @order_number AND [user_id] = @user_id
                        ORDER BY [order_id] DESC";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@order_number", ordernum);
            param.AddParam("@user_id", userid);

            #endregion

            #region 执行
            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
            #endregion

            #region 返回值
            if (Auxiliary.CheckDt(dt))
            {
                PaymentEntity entity = new PaymentEntity();
                var item = dt.Rows[0];
                entity.UserID = Auxiliary.ToInt32(item["user_id"]);
                var order_type = Auxiliary.ToInt32(item["user_id"]);
                if (1 == order_type)
                    entity.MerchantID = Auxiliary.ToInt32(item["oper_id"]);
                entity.OrderNum = item["order_number"] == DBNull.Value ? string.Empty : item["order_number"].ToString();
                entity.TotalPrice = item["order_totalprice"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["order_totalprice"].ToString());
                entity.TotalDeposit = item["order_totaldeposit"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["order_totaldeposit"].ToString());
                entity.MonthPrice = item["order_monthprice"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["order_monthprice"].ToString());
                entity.FreightCost = item["order_freightcost"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["order_freightcost"].ToString());
                return entity;
            }
            return null;
            #endregion
        }
        /// <summary>
        /// 获取指定订单编号的订单续租金额信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public PaymentEntity GetTenancyByOrderNum(string ordernum, int userid)
        {
            #region SQL
            var sql = @"SELECT TOP 1
                                [O].[order_id] ,
                                [O].[user_id] ,
                                [O].[oper_id],
                                [O].[order_type],
                                [O].[order_number] ,
                                [O].[order_state] ,
                                [O].[order_monthprice] ,
                                [OG].[ordergoods_latefees] ,
                                [OG].[ordergoods_id] ,
                                [OG].[ordergoods_month] ,
                                [GT].[tenancy_monthtime]
                        FROM    [dbo].[Orders] (NOLOCK) AS O
                                JOIN dbo.OrderGoodses (NOLOCK) AS OG ON O.order_id = OG.order_id
                                JOIN ( SELECT   [ordergoods_id],[tenancy_monthtime]
                                       FROM     dbo.GoodsTenancies (NOLOCK)
                                       WHERE    [tenancy_isdelivery] = 1 AND [tenancy_isdel] = 0
                                     ) AS GT ON OG.ordergoods_id = GT.ordergoods_id
                        WHERE   ([order_state] = 7
                                OR [order_state] = 8)
                                AND [order_isdel] = 0
                                AND [order_number] = @order_number
                                AND [user_id] = @user_id
                        ORDER BY GT.tenancy_monthtime DESC";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@order_number", ordernum);
            param.AddParam("@user_id", userid);

            #endregion

            #region 执行
            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
            #endregion

            #region 返回值
            if (Auxiliary.CheckDt(dt))
            {
                PaymentEntity entity = new PaymentEntity();
                var item = dt.Rows[0];
                entity.UserID = Auxiliary.ToInt32(item["user_id"]);
                var order_type = Auxiliary.ToInt32(item["user_id"]);
                if (1 == order_type)
                    entity.MerchantID = Auxiliary.ToInt32(item["oper_id"]);
                entity.OrderNum = item["order_number"] == DBNull.Value ? string.Empty : item["order_number"].ToString();
                entity.Latefees = item["ordergoods_latefees"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["ordergoods_latefees"].ToString());
                entity.OrderGoodesID = Auxiliary.ToInt32(item["ordergoods_id"]);
                entity.Month = Auxiliary.ToInt32(item["ordergoods_month"]);
                entity.MonthPrice = item["order_monthprice"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["order_monthprice"].ToString());
                DateTime time = item["tenancy_monthtime"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(item["tenancy_monthtime"]);
                var state = Auxiliary.ToInt32(item["order_state"]);
                if (time.AddMonths(1) < DateTime.Now && state == 8)
                {
                    entity.LatefeesDay = DateTime.Now.Subtract(time.AddMonths(1)).Days;
                    entity.TotalPrice = entity.MonthPrice + (entity.Latefees * entity.LatefeesDay);
                }
                else
                {
                    entity.TotalPrice = entity.MonthPrice;
                    entity.LatefeesDay = 0;
                }
                return entity;
            }
            return null;
            #endregion
        }
        /// <summary>
        /// 获取订单赔损信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public PaymentEntity GetClaimByOrderNum(string ordernum, int userid)
        {
            #region SQL
            var sql = @"SELECT TOP 1
                                [O].[order_id] ,
                                [O].[user_id] ,
                                [O].[oper_id],
                                [O].[order_type],
                                [O].[order_number] ,
                                [O].[order_monthprice] ,
                                [O].[order_totaldeposit] ,
                                [OO].[orderother_loseprice]
                        FROM    [dbo].[Orders] (NOLOCK) AS O
                                JOIN dbo.OrderOthers (NOLOCK) AS OO ON O.order_id = OO.order_id
                        WHERE   ([O].[order_state] = 23
                                OR [O].[order_state] = 25
                                OR [O].[order_state] = 26)
                                AND [O].[order_isdel] = 0
                                AND [O].[order_number] = @order_number
                                AND [O].[user_id] = @user_id";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@order_number", ordernum);
            param.AddParam("@user_id", userid);

            #endregion

            #region 执行
            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
            #endregion

            #region 返回值
            if (Auxiliary.CheckDt(dt))
            {
                PaymentEntity entity = new PaymentEntity();
                var item = dt.Rows[0];
                entity.UserID = Auxiliary.ToInt32(item["user_id"]);
                var order_type = Auxiliary.ToInt32(item["user_id"]);
                if (1 == order_type)
                    entity.MerchantID = Auxiliary.ToInt32(item["oper_id"]);
                entity.OrderNum = item["order_number"] == DBNull.Value ? string.Empty : item["order_number"].ToString();
                entity.TotalPrice = item["orderother_loseprice"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["orderother_loseprice"].ToString());
                entity.TotalDeposit = item["order_totaldeposit"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["order_totaldeposit"].ToString());
                entity.MonthPrice = item["order_monthprice"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["order_monthprice"].ToString());
                entity.Loseprice = item["orderother_loseprice"] == DBNull.Value ? 0 : Auxiliary.ToDecimal(item["orderother_loseprice"].ToString());
                return entity;
            }
            return null;
            #endregion
        }
        /// <summary>
        /// 获取指定订单编号的订单续租金额信息
        /// </summary>
        /// <param name="ordernum">订单编号</param>
        /// <param name="userid">用户ID</param>
        /// <returns>PaymentEntity数据实体</returns>
        public int GetPayoutByOrderGoodesID(int ordergoods_id)
        {
            #region SQL
            var sql = @"SELECT  COUNT(1)
                        FROM    dbo.GoodsTenancies
                        WHERE   ordergoods_id = @ordergoods_id
                                AND tenancy_isdelivery = 1";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", ordergoods_id);

            #endregion

            #region 执行

            var result = KYJ_ZushouRDB.GetFirst(sql, param);

            return Auxiliary.ToInt32(result);

            #endregion
        }
        /// <summary>
        /// 变更订单状态为已支付,其它状态不允许使用该方法，订单状态：3 支付订单完成后“带发货”
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int UpdateOrderStatus(string ordernum, int userid)
        {
            try
            {
                #region SQL
                //订单待收货状态
                var state = 3;

                var sql = @"BEGIN TRANSACTION
                            BEGIN
                                IF EXISTS ( SELECT  [order_id]
                                            FROM    [dbo].[Orders](NOLOCK)
                                            WHERE   [order_state] = 1
                                                    AND [user_id] = @user_id
                                                    AND [order_isdel] = 0
                                                    AND [order_number] = @order_number ) 
                                    BEGIN
                                        DECLARE @order_id INT
                                        DECLARE @ordergoods_id INT
                                        SELECT TOP 1  @order_id = O.[order_id] ,
                                                @ordergoods_id = OG.[ordergoods_id]
                                        FROM    [dbo].[Orders] (NOLOCK) AS O
                                                JOIN dbo.OrderGoodses (NOLOCK) AS OG ON O.order_id = OG.order_id
                                        WHERE   [order_state] = 1
                                                AND [user_id] = @user_id
                                                AND [order_isdel] = 0
                                                AND [order_number] = @order_number
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10006    
                                                RETURN
                                            END  
                                        UPDATE  dbo.Orders
                                        SET     [order_state] = @order_state
                                        WHERE   [order_id] = @order_id 
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10007
                                                RETURN  
                                            END 
                                        UPDATE  dbo.GoodsTenancies
                                        SET     tenancy_isdelivery = 1 ,
                                                tenancy_updatetime = GETDATE()
                                        WHERE   ordergoods_id = @ordergoods_id
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10008    
                                                RETURN
                                            END 
                                        UPDATE  [dbo].[OrderOthers]
                                        SET     [orderother_paidtime] = GETDATE()
                                        WHERE   [order_id] = @order_id
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10009  
                                                RETURN
                                            END 
                                        COMMIT TRANSACTION 
                                        SELECT  1
                                    END
                                ELSE 
                                    BEGIN
                                        ROLLBACK TRANSACTION
                                        SELECT  -10100  
                                        RETURN
                                    END      
                            END
                        ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userid);
                param.AddParam("@order_number", ordernum);
                param.AddParam("@order_state", state);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                return Auxiliary.ToInt32(result);

                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", ordernum + "," + userid, ex);
                return 0;
            }
        }
        /// <summary>
        /// 验证是否需要续租
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public bool IsRelet(string ordernum, int userid)
        {
            try
            {

                #region SQL

                var sql = @"IF EXISTS ( SELECT  [order_id]
                                    FROM    [dbo].[Orders](NOLOCK)
                                    WHERE   ([order_state] = 7
                                            OR [order_state] = 8)
                                            AND [user_id] = @user_id
                                            AND [order_isdel] = 0
                                            AND [order_number] = @order_number ) 
                            BEGIN
                                DECLARE @ordergoods_id INT
                                DECLARE @ordergoods_month INT
                                DECLARE @month INT          
                                SELECT TOP 1
                                        @ordergoods_id = OG.[ordergoods_id] ,
                                        @ordergoods_month = OG.[ordergoods_month]
                                FROM    [dbo].[Orders] (NOLOCK) AS O
                                        JOIN dbo.OrderGoodses (NOLOCK) AS OG ON O.order_id = OG.order_id
                                WHERE   [user_id] = @user_id
                                        AND [order_isdel] = 0
                                        AND [order_number] = @order_number

                                SELECT  @month = COUNT(1)
                                FROM    dbo.GoodsTenancies (NOLOCK)
                                WHERE   ordergoods_id = @ordergoods_id
                                        AND tenancy_isdelivery = 1

                                IF @ordergoods_month IS NULL 
                                    BEGIN
                                        SELECT  0  
                                    END
                                ELSE 
                                    BEGIN
                                        IF @month >= @ordergoods_month 
                                            BEGIN
                                                SELECT  0                      
                                            END
                                        ELSE 
                                            BEGIN
                                                SELECT  1                      
                                            END                  
                                    END               
                     
                            END
                        ELSE 
                            BEGIN
                                SELECT  0      
                            END
                        ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userid);
                param.AddParam("@order_number", ordernum);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                if (Auxiliary.ToInt32(result) > 0)
                    return true;
                else
                    return false;
                #endregion

            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", ordernum + "," + userid, ex);
                return false;
            }
        }
        /// <summary>
        /// 续租
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int OrderRelet(string ordernum, int userid)
        {
            try
            {

                #region SQL

                var sql = @"BEGIN TRANSACTION
                            BEGIN
                                DECLARE @order_id INT
                                IF EXISTS (SELECT  [order_id]
                                            FROM    [dbo].[Orders](NOLOCK)
                                            WHERE   ([order_state] = 7
                                                    OR [order_state] = 8)
                                                    AND [user_id] = @user_id
                                                    AND [order_isdel] = 0
                                                    AND [order_number] = @order_number ) 
                                    BEGIN
                                        DECLARE @tenancy_monthtime DATETIME
                                        DECLARE @order_monthprice MONEY
                                        DECLARE @ordergoods_id INT
                                        SELECT TOP 1
                                                @order_id = O.[order_id] ,
                                                @order_monthprice = O.[order_monthprice] ,
                                                @ordergoods_id = OG.[ordergoods_id]
                                        FROM    [dbo].[Orders] (NOLOCK) AS O
                                                JOIN dbo.OrderGoodses (NOLOCK) AS OG ON O.order_id = OG.order_id
                                        WHERE   [user_id] = @user_id
                                                AND [order_isdel] = 0
                                                AND [order_number] = @order_number
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10007    
                                                RETURN       
                                            END
                                        SELECT TOP 1
                                                @tenancy_monthtime = tenancy_monthtime
                                        FROM    dbo.GoodsTenancies (NOLOCK)
                                        WHERE   ordergoods_id = @ordergoods_id
                                                AND tenancy_isdelivery = 1
                                        ORDER BY tenancy_monthtime DESC
            
                                        IF @tenancy_monthtime IS NULL 
                                            BEGIN
                                                SET @tenancy_monthtime = GETDATE()
                    
                                            END
                                        ELSE 
                                            BEGIN
                                                SET @tenancy_monthtime = DATEADD(MONTH, 1,
                                                                                 @tenancy_monthtime)              
                                            END              
                                        IF EXISTS ( SELECT  tenancy_id
                                                    FROM    dbo.GoodsTenancies (NOLOCK)
                                                    WHERE   tenancy_monthtime = @tenancy_monthtime
                                                            AND ordergoods_id = @ordergoods_id ) 
                                            BEGIN                        
                                                UPDATE  dbo.GoodsTenancies
                                                SET     tenancy_isdelivery = 1
                                                WHERE   tenancy_monthtime = @tenancy_monthtime
                                                        AND ordergoods_id = @ordergoods_id
                                                IF @@ERROR <> 0 
                                                    BEGIN
                                                        ROLLBACK TRANSACTION
                                                        SELECT  -10008    
                                                        RETURN 
                                                    END                          
                                            END 
                                        ELSE 
                                            BEGIN
                                                INSERT  INTO [dbo].[GoodsTenancies]
                                                        ( [ordergoods_id] ,
                                                          [tenancy_price] ,
                                                          [tenancy_monthtime] ,
                                                          [tenancy_isdelivery] ,
                                                          [tenancy_isfavorable] ,
                                                          [tenancy_favorableprice] ,
                                                          [tenancy_sort] ,
                                                          [tenancy_createtime] ,
                                                          [tenancy_updatetime] ,
                                                          [tenancy_isdel]
                                                        )
                                                VALUES  ( @ordergoods_id , -- ordergoods_id - int
                                                          @order_monthprice , -- tenancy_price - money
                                                          @tenancy_monthtime , -- tenancy_monthtime - datetime
                                                          1 , -- tenancy_isdelivery - bit
                                                          0 , -- tenancy_isfavorable - bit
                                                          0 , -- tenancy_favorableprice - money
                                                          0 , -- tenancy_sort - int
                                                          GETDATE() , -- tenancy_createtime - datetime
                                                          GETDATE() , -- tenancy_updatetime - datetime
                                                          0  -- tenancy_isdel - bit
                                                        ) 
                                                IF @@ERROR <> 0 
                                                    BEGIN
                                                        ROLLBACK TRANSACTION
                                                        SELECT  -10009  
                                                        RETURN 
                                                    END
                                            END
                                        IF @tenancy_monthtime > GETDATE() 
                                            BEGIN
                                                UPDATE  dbo.Orders
                                                SET     [order_state] = 7
                                                WHERE   [order_id] = @order_id 
                                                IF @@ERROR <> 0 
                                                    BEGIN
                                                        ROLLBACK TRANSACTION
                                                        SELECT  -10010  
                                                        RETURN 
                                                    END     
                                            END 
                                        COMMIT TRANSACTION 
                                        SELECT  1                   
			
                                    END
                                ELSE 
                                    BEGIN
                                        ROLLBACK TRANSACTION
                                        SELECT  -10100  
                                        RETURN     
                                    END  
                            END
                        ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userid);
                param.AddParam("@order_number", ordernum);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                return Auxiliary.ToInt32(result);

                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", ordernum + "," + userid, ex);
                return 0;
            }
        }
        /// <summary>
        /// 购买
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int OrderBuyout(string ordernum, int userid)
        {
            try
            {

                #region SQL

                var sql = @"BEGIN TRANSACTION
                            BEGIN
                                DECLARE @order_id INT
                                IF EXISTS (SELECT  [order_id]
                                            FROM    [dbo].[Orders](NOLOCK)
                                            WHERE   ([order_state] = 7
                                                    OR [order_state] = 8)
                                                    AND [user_id] = @user_id
                                                    AND [order_isdel] = 0
                                                    AND [order_number] = @order_number ) 
                                    BEGIN
                                        DECLARE @tenancy_monthtime DATETIME
                                        DECLARE @order_monthprice MONEY
                                        DECLARE @ordergoods_id INT
                                        DECLARE @month INT--商品代售期
                                        DECLARE @monthcount INT--已支付月份          
                                        SELECT TOP 1
                                                @order_id = O.[order_id] ,
                                                @order_monthprice = O.[order_monthprice] ,
                                                @ordergoods_id = OG.[ordergoods_id] ,
                                                @month = OG.[ordergoods_month]
                                        FROM    [dbo].[Orders] (NOLOCK) AS O
                                                JOIN dbo.OrderGoodses (NOLOCK) AS OG ON O.order_id = OG.order_id
                                        WHERE   [user_id] = @user_id
                                                AND [order_isdel] = 0
                                                AND [order_number] = @order_number
                                        SELECT TOP 1
                                                @tenancy_monthtime = tenancy_monthtime
                                        FROM    dbo.GoodsTenancies (NOLOCK)
                                        WHERE   ordergoods_id = @ordergoods_id
                                                AND tenancy_isdelivery = 1
                                        ORDER BY tenancy_monthtime DESC
            
                                        SELECT  @monthcount = COUNT(1)
                                        FROM    dbo.GoodsTenancies
                                        WHERE   ordergoods_id = @ordergoods_id
                                                AND tenancy_isdelivery = 1
                                        IF @month > @monthcount 
                                            BEGIN
                                                DECLARE @i INT
                                                SET @i = 1              
                                                WHILE @i <= @month - @monthcount 
                                                    BEGIN
                                                        IF @tenancy_monthtime IS NULL 
                                                            BEGIN
                                                                SET @tenancy_monthtime = GETDATE()
                    
                                                            END
                                                        ELSE 
                                                            BEGIN
                                                                SET @tenancy_monthtime = DATEADD(MONTH, 1,
                                                                                          @tenancy_monthtime)              
                                                            END              
                                                        IF EXISTS ( SELECT  tenancy_id
                                                                    FROM    dbo.GoodsTenancies (NOLOCK)
                                                                    WHERE   tenancy_monthtime = @tenancy_monthtime
                                                                            AND ordergoods_id = @ordergoods_id ) 
                                                            BEGIN                        
                                                                UPDATE  dbo.GoodsTenancies
                                                                SET     tenancy_isdelivery = 1
                                                                WHERE   tenancy_monthtime = @tenancy_monthtime
                                                                        AND ordergoods_id = @ordergoods_id
                                                                IF @@ERROR <> 0 
                                                                    BEGIN
                                                                        ROLLBACK TRANSACTION
                                                                        SELECT  -10007  
                                                                        RETURN 
                                                                    END                          
                                                            END 
                                                        ELSE 
                                                            BEGIN
                                                                INSERT  INTO [dbo].[GoodsTenancies]
                                                                        ( [ordergoods_id] ,
                                                                          [tenancy_price] ,
                                                                          [tenancy_monthtime] ,
                                                                          [tenancy_isdelivery] ,
                                                                          [tenancy_isfavorable] ,
                                                                          [tenancy_favorableprice] ,
                                                                          [tenancy_sort] ,
                                                                          [tenancy_createtime] ,
                                                                          [tenancy_updatetime] ,
                                                                          [tenancy_isdel]
                                                                        )
                                                                VALUES  ( @ordergoods_id , -- ordergoods_id - int
                                                                          @order_monthprice , -- tenancy_price - money
                                                                          @tenancy_monthtime , -- tenancy_monthtime - datetime
                                                                          1 , -- tenancy_isdelivery - bit
                                                                          0 , -- tenancy_isfavorable - bit
                                                                          0 , -- tenancy_favorableprice - money
                                                                          0 , -- tenancy_sort - int
                                                                          GETDATE() , -- tenancy_createtime - datetime
                                                                          GETDATE() , -- tenancy_updatetime - datetime
                                                                          0  -- tenancy_isdel - bit
                                                                        ) 
                                                                IF @@ERROR <> 0 
                                                                    BEGIN
                                                                        ROLLBACK TRANSACTION
                                                                        SELECT  -10008  
                                                                        RETURN 
                                                                    END
                                                            END
                                                        SET @i = @i + 1                              
                                                    END                      
                                            END
                                        IF @tenancy_monthtime > GETDATE() 
                                            BEGIN
                                                UPDATE  dbo.Orders
                                                SET     [order_state] = 7
                                                WHERE   [order_id] = @order_id 
                                                IF @@ERROR <> 0 
                                                    BEGIN
                                                        ROLLBACK TRANSACTION
                                                        SELECT  -10009  
                                                        RETURN 
                                                    END     
                                            END               
                                        COMMIT TRANSACTION 
                                        SELECT  1  
                                    END
                                ELSE 
                                    BEGIN
                                        ROLLBACK TRANSACTION
                                        SELECT  -10100  
                                        RETURN     
                                    END  
                            END
                        ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userid);
                param.AddParam("@order_number", ordernum);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                return Auxiliary.ToInt32(result);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", ordernum + "," + userid, ex);
                return 0;
            }
        }
        /// <summary>
        /// 赔付
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="userid">用户ID</param>
        /// <param name="state">需变更订单ID</param>
        /// <returns></returns>
        public int OrderCliam(string ordernum, int userid)
        {
            try
            {

                #region SQL

                var sql = @"BEGIN TRANSACTION
                            BEGIN
                                IF EXISTS ( SELECT  [order_id]
                                            FROM    [dbo].[Orders](NOLOCK)
                                            WHERE   ( [order_state] = 23
                                                      OR [order_state] = 25
                                                      OR [order_state] = 26
                                                    )
                                                    AND [user_id] = @user_id
                                                    AND [order_isdel] = 0
                                                    AND [order_number] = @order_number ) 
                                    BEGIN
                                        DECLARE @order_state INT
                                        DECLARE @order_id INT
                                        SELECT TOP 1 @order_id = order_id ,
                                                @order_state = order_state
                                        FROM    [dbo].[Orders] (NOLOCK)
                                        WHERE   ( [order_state] = 23
                                                  OR [order_state] = 25
                                                  OR [order_state] = 26
                                                )
                                                AND [user_id] = @user_id
                                                AND [order_isdel] = 0
                                                AND [order_number] = @order_number
                                        IF @order_state = 23 
                                            BEGIN
                                                SET @order_state = 11
                                            END
                                        ELSE 
                                            IF @order_state = 25 
                                                BEGIN
                                                    SET @order_state = 14              
                                                END
                                            ELSE 
                                                IF @order_state = 26 
                                                    BEGIN
                                                        SET @order_state = 3              
                                                    END       
                                        UPDATE  [dbo].[Orders]
                                        SET     [order_state] = @order_state
                                        WHERE   [order_id] = @order_id
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10009  
                                                RETURN               
                                            END                  
                                        COMMIT TRANSACTION 
                                        SELECT  1                   
                                    END
                                ELSE 
                                    BEGIN
                                        ROLLBACK TRANSACTION
                                        SELECT  -10100  
                                        RETURN     
                                    END  
                            END
                        ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userid);
                param.AddParam("@order_number", ordernum);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                return Auxiliary.ToInt32(result);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", ordernum + "," + userid, ex);
                return 0;
            }
        }
        /// <summary>
        /// 商户帐户金额操作函数
        /// </summary>
        /// <param name="mid">商户ID</param>
        /// <param name="price">操作金额</param>
        /// <param name="mlog_type">日志类型 0 未知，1 充值，2 提现，3 租金4驳回</param>
        /// <param name="mlog_state">日志状态 0 未知，1 出账，2 入账</param>
        /// <param name="mlog_desc">日志说明</param>
        /// <returns></returns>
        public int MerhantAmount(int mid, decimal price, int mlog_type, int mlog_state, string mlog_desc)
        {
            try
            {
                #region SQL

                var sql = @"BEGIN TRANSACTION
                            BEGIN
                                IF EXISTS ( SELECT  [merchant_id]
                                            FROM    [dbo].[M_Accounts]
                                            WHERE   [merchant_id] = @merchant_id ) 
                                    BEGIN
                                        UPDATE  [dbo].[M_Accounts]
                                        SET     [account_price] = [account_price] + @account_price ,
                                                [account_updatetime] = GETDATE()
                                        WHERE   [merchant_id] = @merchant_id
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10008  
                                                RETURN 
                                            END 
                                        INSERT  INTO dbo.M_AccountLogs
                                                ( [merchant_id] ,
                                                  [mlog_type] ,
                                                  [mlog_state] ,
                                                  [mlog_price] ,
                                                  [mlog_desc] ,
                                                  [mlog_createtime]
                                                )
                                        VALUES  ( @merchant_id , -- merchant_id - int
                                                  @mlog_type , -- mlog_type - int
                                                  @mlog_state , -- mlog_state - int
                                                  @account_price , -- mlog_price - money
                                                  @mlog_desc , -- mlog_desc - nvarchar(200)
                                                  GETDATE()  -- mlog_createtime - datetime
                                                )
                                        IF @@ERROR <> 0 
                                            BEGIN
                                                ROLLBACK TRANSACTION
                                                SELECT  -10009  
                                                RETURN 
                                            END 
                                        COMMIT TRANSACTION 
                                        SELECT  1  
                                    END
                                ELSE 
                                    BEGIN
                                        ROLLBACK TRANSACTION
                                        SELECT  -10100  
                                        RETURN     
                                    END
                            END
                        ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@merchant_id", mid);
                param.AddParam("@account_price", price);
                param.AddParam("@mlog_type", mlog_type);
                param.AddParam("@mlog_state", mlog_state);
                param.AddParam("@mlog_desc", mlog_desc);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                return Auxiliary.ToInt32(result);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", "MerhantAmount:" + mid + "," + price + "," + mlog_type + "," + mlog_state + "," + mlog_desc, ex);
                return 0;
            }
        }
    }
}
