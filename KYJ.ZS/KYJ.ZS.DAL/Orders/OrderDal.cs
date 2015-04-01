using System;
using System.Collections.Generic;
using System.Text;
using KYJ.ZS.DAL.LocalUsers;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.OrderGoodses;
using KYJ.ZS.Models.Orders;
using System.Data;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;

namespace KYJ.ZS.DAL.Orders
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-24
    /// Desc：操作订单表
    /// </summary>
    public class OrderDal
    {
        #region 发货管理(租)—ningjd

        /// <summary>
        /// 获取订单列表(发货、未发货)
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <param name="searchEntity">搜索条件Entity</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<OrderEntity> GetOrdersByState(int? id, OrderSearchEntity searchEntity, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                #region //分页

                // 列名
                //            string columnList = @"t2.[order_id] as OrderId,[order_number] as Number,[order_address] as Address,[order_expresscompany] as ExpressCompany,[order_expressnum] as ExpressNum,[order_createtime] as CreateTime,[goods_id] as GoodsId,
                //                                [ordergoods_title] as Title,[ordergoods_guid] as Guid,[ordercolor_name] as ColorName,[ordernorm_name] as NormName,[ordergoods_num] as GoodsNum,
                //                                [ordergoods_monthprice] as MonthPrice,[order_tel] as DeliveryTel,[order_code] as DeliveryCode,[order_realname] as DeliveryRealName";

                #region 查询条件及参数

                //// 商户所属   租的订单
                //string where = " WHERE t2.oper_id=@oper_id and t2.order_type=1 and t2.[order_state]=@order_state and t2.order_isdel = 'false'";
                //var param = new SqlParam();
                //param.AddParam("@oper_id", searchEntity.OperId);
                //param.AddParam("@order_state", searchEntity.IsSend ? 4 : 3);

                ////订单ID
                //if (id.HasValue)
                //{
                //    where += " and t2.order_id =@order_id";
                //    param.AddParam("@order_id", id.Value);
                //}
                //// 收件人名称
                //if (!string.IsNullOrEmpty(searchEntity.RecipientName))
                //{
                //    where += " and t2.order_realname like '%'+ @order_realname +'%'";
                //    param.AddParam("@order_realname", searchEntity.RecipientName.Trim());
                //}
                //// 订单编号
                //if (!string.IsNullOrEmpty(searchEntity.OrderNumber))
                //{
                //    where += " and t2.order_number like '%'+ @order_number +'%'";
                //    param.AddParam("@order_number", searchEntity.OrderNumber.Trim());
                //}
                ////// 买家昵称
                //if (!string.IsNullOrEmpty(searchEntity.NickName))
                //{
                //    where += " and t3.user_nickname like '%' + @user_nickname + '%'";
                //    param.AddParam("@user_nickname", searchEntity.NickName.Trim());
                //}
                ////// 时间过滤
                //if (searchEntity.startDate.HasValue)
                //{
                //    where += " and t2.order_createtime >= @startDate";
                //    param.AddParam("@startDate", searchEntity.startDate.Value);
                //}
                //if (searchEntity.endDate.HasValue)
                //{
                //    where += " and t2.order_createtime <= @endDate";
                //    param.AddParam("@endDate", searchEntity.endDate.Value);
                //}

                #endregion
                #region SQL及执行
                //            // 近三个月（type=1）
                //            StringBuilder sql = new StringBuilder("SELECT " + columnList + ",type=1" +
                //                                                    @" FROM OrderGoodses(NOLOCK) as t1 right 
                //                                                        join Orders(NOLOCK) as t2 on t1.order_id = t2.order_id 
                //                                                        left join LocalUsers(NOLOCK) as t3 on t2.user_id = t3.user_id"
                //                                                    + where);
                //            // 三个月以前（type=2）
                //            StringBuilder oldSql = new StringBuilder("SELECT " + columnList + ",type=2" +
                //                                                    @" FROM OldOrderGoodses(NOLOCK) as t1 right 
                //                                                        join OldOrders(NOLOCK) as t2 on t1.order_id = t2.order_id 
                //                                                        left join LocalUsers(NOLOCK) as t3 on t2.user_id = t3.user_id"
                //                                                    + where);
                //            var recordParam = param.Copy();
                //            // 记录总个数
                //            var sqlRecord = "SELECT COUNT(1) FROM (" + sql.ToString() + " UNION " + oldSql.ToString() + ") AS T";
                //            totalRecord = Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sqlRecord, recordParam));
                //            // 总页数
                //            totalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize);

                //            var sqlstr = "SELECT * FROM(SELECT *,ROW_NUMBER() OVER(order by CreateTime desc) AS RowNum FROM (" + sql.ToString() + " UNION "
                //                    + oldSql.ToString() + ") AS T) AS T1 WHERE RowNum BETWEEN @StartRowNum AND @EndRowNum";
                //            // 开始的rownum
                //            int startRowNum = pageSize * (index - 1) + 1;
                //            param.AddParam("@StartRowNum", startRowNum);
                //            param.AddParam("@EndRowNum", startRowNum + pageSize - 1);

                //            DataTable dt = KYJ_ZushouRDB.GetTable(sqlstr, param);

                #endregion

                #endregion

                #region 存储过程分页
                //表名
                string tableName = @"OrderGoodses(NOLOCK) as t1 right join Orders(NOLOCK) as t2 on t1.order_id = t2.order_id left join LocalUsers(NOLOCK) as t3 on t2.user_id = t3.user_id";

                #region 查询条件

                // 商户所属   租的订单
                string where = " t2.oper_id=" + searchEntity.OperId + " and t2.order_type=1 and t2.[order_state]=" + (searchEntity.IsSend ? 4 : 3) + " and t2.order_isdel = 'false' ";

                //订单ID
                if (id.HasValue)
                    where += " and t2.order_id =" + id.Value;
                // 收件人名称
                if (!string.IsNullOrEmpty(searchEntity.RecipientName))
                    where += " and t2.order_realname like '%" + searchEntity.RecipientName + "%'";
                // 订单编号
                if (!string.IsNullOrEmpty(searchEntity.OrderNumber))
                    where += " and t2.order_number like '%" + searchEntity.OrderNumber.Trim() + "%'";
                // 买家昵称
                if (!string.IsNullOrEmpty(searchEntity.NickName))
                    where += " and t3.user_nickname like '%" + searchEntity.NickName.Trim() + "%'";
                // 时间过滤
                if (searchEntity.startDate.HasValue)
                    where += " and t2.order_createtime >= '" + searchEntity.startDate.Value.ToString("yyyy-MM-dd 00:00:00") + "'";
                if (searchEntity.endDate.HasValue)
                    where += " and t2.order_createtime <= '" + searchEntity.endDate.Value.ToString("yyyy-MM-dd 23:59:59") + "'";

                #endregion

                //排序
                string orderBy = " order_createtime";
                if (searchEntity.IsSend)
                    orderBy += " desc";

                //列名
                string columnList = @"t2.[order_id] as OrderId,[order_number] as Number,[order_address] as Address,[order_expresscompany] as ExpressCompany,[order_expressnum] as ExpressNum,[order_createtime] as CreateTime,[goods_id] as GoodsId,
                                            [ordergoods_title] as Title,[ordergoods_guid] as Guid,[ordercolor_name] as ColorName,[ordernorm_name] as NormName,[ordergoods_num] as GoodsNum,
                                            [ordergoods_monthprice] as MonthPrice,[order_tel] as DeliveryTel,[order_code] as DeliveryCode,[order_realname] as DeliveryRealName";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                #endregion

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<OrderEntity>.GetEntityList(dt);
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
        /// 修改订单发货信息（管理后台）
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="expressCompany">快递/物流公司名称</param>
        /// <param name="expressNum">快递/物流单号</param>
        /// <param name="type">标识数据来源表（1、近三个月的订单；2、三个月以前的订单）</param>
        /// <returns></returns>
        public int UpdateSendInfo(int orderId, string expressCompany, string expressNum)
        {
            try
            {
                #region SQL
                var sql = @"begin tran
			            DECLARE @results int --执行结果，0失败，1成功

                        /*订单表*/
                        UPDATE  [dbo].[Orders]
                        SET     [order_expresscompany] = @order_expresscompany
                                ,[order_expressnum] = @order_expressnum
                                ,[order_state] = 4
                                ,[order_updatetime] = @sendGoodsTime
                        WHERE order_id = @order_id

                        /*订单扩展表*/
                        UPDATE  [dbo].[OrderOthers]
                        SET     [orderother_deliverytime] = @sendGoodsTime
                        WHERE order_id = @order_id

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

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@order_id", orderId);
                param.AddParam("@order_expresscompany", expressCompany);
                param.AddParam("@order_expressnum", expressNum);
                param.AddParam("@sendGoodsTime", DateTime.Now);

                #endregion

                #region 执行

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));

                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", orderId + "," + expressCompany + "," + expressNum, ex);
                return 0;
            }
        }

        /// <summary>
        /// 修改订单发货信息(商户判断)
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="expressCompany">快递/物流公司名称</param>
        /// <param name="expressNum">快递/物流单号</param>
        /// <param name="type">标识数据来源表（1、近三个月的订单；2、三个月以前的订单）</param>
        /// <param name="merchantId">当前商户ID</param>
        /// <returns></returns>
        public int UpdateSendInfo(int orderId, string expressCompany, string expressNum, int merchantId)
        {
            try
            {
                #region 判断是否属于当前商户
                var sqlstr = "select oper_id from Orders(NOLOCK) where order_id=@order_id";
                var paramList = new SqlParam();
                paramList.AddParam("@order_id", orderId);

                if (merchantId != Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sqlstr, paramList)))
                    return 0;
                #endregion

                #region SQL
                var sql = @"begin tran
			            DECLARE @results int --执行结果，0失败，1成功

                        /*订单表*/
                        UPDATE  [dbo].[Orders]
                        SET     [order_expresscompany] = @order_expresscompany
                                ,[order_expressnum] = @order_expressnum
                                ,[order_state] = 4
                                ,[order_updatetime] = @sendGoodsTime
                        WHERE order_id = @order_id

                        /*订单扩展表*/
                        UPDATE  [dbo].[OrderOthers]
                        SET     [orderother_deliverytime] = @sendGoodsTime
                        WHERE order_id = @order_id

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

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@order_id", orderId);
                param.AddParam("@order_expresscompany", expressCompany);
                param.AddParam("@order_expressnum", expressNum);
                param.AddParam("@sendGoodsTime", DateTime.Now);

                #endregion

                #region 执行

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));

                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", orderId + "," + expressCompany + "," + expressNum, ex);
                return 0;
            }
        }

        #endregion
        #region 获取各个状态的订单
        /// <summary>
        /// 作者：maq
        /// 时间:2014年4月25日16:45:16
        /// 描述：获取各个状态的订单的数量
        /// </summary>
        /// <param name="operId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetOrdersStateNum(int operId)
        {
            string strSql = "select count(1) as Num,order_state as state  from Orders WITH(NOLOCK) where oper_id=@id and order_isdel=0 group by order_state  ";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", operId);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql, sqlParam))
            {
                foreach (DataRow row in table.Rows)
                {
                    dic.Add(Convert.ToInt32(row["state"]), Convert.ToInt32(row["Num"]));
                }
            }
            return dic;
        }
        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月6日09:40:16
        /// 描述：获取历史订单中的各个状态的订单数量
        /// </summary>
        /// <param name="operId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetOldOrderSataeNum(int operId)
        {
            string strSql = "select count(1) as Num,order_state as state  from oldOrders  WITH(NOLOCK)  where oper_id=@id and order_isdel=0 group by order_state ";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", operId);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql, sqlParam))
            {
                foreach (DataRow row in table.Rows)
                {
                    dic.Add(Convert.ToInt32(row["state"]), Convert.ToInt32(row["Num"]));
                }
            }
            return dic;
        }
        #endregion

        #region 邓福伟
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-06
        /// 描述：订单生成参数判断是否正确--订单确认页
        /// </summary>
        /// <param name="entity">订单生成需要的参数</param>
        /// <returns></returns>
        public int Web_OrderParameterJudge(Web_OrderParameterEntity entity)
        {
            try
            {
                #region sql
                var sql = @"
                        declare @goodsid int,   @goodsnum int, @goodsmonth int, 
                                @goodscolorid int, @goodsnormid int,
                                @deliveryid int,   @userid int
                        select  @goodsid=@goods_id, @goodsnum=@goods_num, @goodsmonth=@goods_month, 
                                @goodscolorid=@goods_colorid,@goodsnormid=@goods_normid,
                                @deliveryid=@delivery_id,   @userid=@user_id

                        --判断是否正确：-1为未知、0为正确、1为商品Id不正确、2商品颜色Id不正确、
                        --              3商品规格Id不确定、4商品数量不正确、5租期不正确、6收货地址不正确
                        declare @iscorrect int 
                        set     @iscorrect=-1   --默认赋值为未知

                        /*判断商品Id是否正确*/
                        if exists (select 1 from dbo.RentalGoodses with(nolock) where rental_id=@goodsid and rental_isdel=0 ) 
                        --商品Id正确进入
                        begin 
                           set @iscorrect=0 --0为正确

                           if(0=@goodscolorid and 0=@goodsnormid) --没颜色没规格 
                           begin
                                /*商品数量不正确*/
                                if not exists (select 1 from dbo.RentalGoodses with(nolock) where rental_id=@goodsid and rental_number>=@goodsnum)
                                   set @iscorrect=4           
                           end
                           else if(0<@goodscolorid and 0=@goodsnormid )--有颜色没规格 
                           begin
                                /*商品颜色正确*/
                                if exists (select 1 from dbo.GoodsColors with(nolock) where goods_id=@goodsid and goodscolor_id=@goodscolorid )
                                begin
                                    /*商品数量不正确*/
                                    if not exists (select 1 from dbo.RentalGoodses with(nolock) where rental_id=@goodsid and rental_number>=@goodsnum )
                                       set  @iscorrect=4
                                end
                                /*商品颜色不正确*/
                                else 
                                   set @iscorrect=2  
                           end
                           else if(0=@goodscolorid and 0<@goodsnormid)--没颜色有规格 
                           begin
                                /*商品规格正确*/
                                if exists (select 1 from dbo.GoodsNorms with(nolock) where goods_id=@goodsid and goodsnorm_id=@goodsnormid)
                                begin
                                    /*商品数量不正确*/
                                    if not exists (select 1 from dbo.RentalGoodses with(nolock) where rental_id=@goodsid and rental_number>=@goodsnum)
                                       set @iscorrect=4
                                end
                                /*商品规格不正确*/
                                else 
                                   set @iscorrect=3
                           end
                           else --有颜色有规格
                           begin
                                 /*商品颜色不正确*/
                                if not exists (select 1 from dbo.GoodsColors with(nolock) where goods_id=@goodsid and goodscolor_id=@goodscolorid )
                                   set  @iscorrect=2
                                 /*商品规格不正确*/
                                else if not exists (select 1 from dbo.GoodsNorms with(nolock) where goods_id=@goodsid and goodsnorm_id=@goodsnormid)
                                   set  @iscorrect=3
                                 else
                                 begin
                                  /*商品数量不正确*/
                                  if not exists (select 1 from dbo.GoodsPrices with(nolock) where goods_id=@goodsid and goodscolor_id=@goodscolorid and goodsnorm_id=@goodsnormid and goodsprice_num>=@goodsnum )
                                     set  @iscorrect=4
                                 end        
                           end

                           /*商品租期是否正确*/
                           if(@iscorrect=0)
                           begin
                              if not exists(select 1 from dbo.RentalGoodses tg with(nolock)
                                            left join dbo.TenancyTemplates tt with(nolock) on tg.ttemp_id=tt.ttemp_id 
                                            where tg.rental_id=@goodsid and charindex(','+cast(@goodsmonth as varchar(50))+',',','+tt.ttemp_months+',')>0
                              )
                                  set  @iscorrect=5
                           end
                           /*收获地址是否正确*/
                           if(@iscorrect=0 and @userid<>0 and @deliveryid<>0)
                           begin
                              if not exists(select 1 from dbo.DeliveryAddresses with(nolock) where delivery_id=@deliveryid and [user_id]=@userid and delivery_isdel=0)
                                  set  @iscorrect=6
                           end
                        end
                        else --商品Id不正确进入
                        begin
                          set @iscorrect=1 
                        end

                        select  @iscorrect 
                        ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@goods_id", entity.GoodsId, SqlDbType.Int, 4);
                param.AddParam("@goods_num", entity.GoodsNum, SqlDbType.Int, 4);
                param.AddParam("@goods_month", entity.GoodsMonth, SqlDbType.Int, 4);
                param.AddParam("@goods_colorid", entity.GoodsColorId, SqlDbType.Int, 4);
                param.AddParam("@goods_normid", entity.GoodsNormId, SqlDbType.Int, 4);
                param.AddParam("@delivery_id", entity.DeliveryId, SqlDbType.Int, 4);
                param.AddParam("@user_id", entity.UserId, SqlDbType.Int, 4);
                #endregion
                #region 返回
                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "订单生成参数判断是否正确--订单确认页", ex);
                return -1;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-06
        /// 描述：订单详情--订单确认页
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Web_OrderConfirm Web_GetOrderConfirm(Web_OrderParameterEntity entity)
        {
            try
            {
                #region sql
                var sql = @" 
                        declare @goodsid int,   @goodsnum int, @goodsmonth int, @goodscolorid int, @goodsnormid int
                        select  @goodsid=@goods_id, @goodsnum=@goods_num,@goodsmonth=@goods_month, @goodscolorid=@goods_colorid,@goodsnormid=@goods_normid

                        select rg.[rental_id] as Id --商品Id
                              ,rg.[rental_guid] as RentalGuid    --商品GUId
                              ,rg.[rental_title] as Title        --商品名称
                              ,rg.[rental_deposit] as Deposit    --商品押金
                              ,rg.[rental_weight] as Weight      --重量
                              ,rg.[rental_volume] as Volume      --体积
                              ,rg.[ftemp_id] as FtempId         --运费模板Id

                              ,case when @goodscolorid>0 and @goodsnormid>0
	                           then gp.goods_price 
	                           else rg.rental_monthprice end as MothPrice  --商品月价格

                              ,gc.color_name as ColorName       --颜色名称
                              ,gc.color_code as ColorCode       --颜色代码
                              ,gn.norm_name as NormName         --规格名称

                              ,@goodsnum as GoodsNum             --商品数量
                              ,@goodsmonth as GoodsMonth        --商品租期
                              ,@goodscolorid as GoodsColorId       --颜色Id
                              ,@goodsnormid as GoodsNormId       --规格Id
                              ,substring(tt.[ttemp_months]+',',0,charindex(',',tt.[ttemp_months]+',')) as GoodsLowestMonth  --最低起租期

                        from dbo.RentalGoodses rg  with(nolock)                                     ----租商品表
                        left join dbo.TenancyTemplates tt with(nolock) on rg.ttemp_id=tt.ttemp_id   ----租期表                               
                        left join dbo.GoodsPrices gp with(nolock) on rg.rental_id=gp.goods_id and gp.goodscolor_id=@goodscolorid and gp.goodsnorm_id=@goodsnormid   ----商品价格表
                        left join dbo.GoodsColors gc with(nolock) on gc.goodscolor_id=@goodscolorid            ----商品颜色表
                        left join dbo.GoodsNorms gn with(nolock) on gn.goodsnorm_id=@goodsnormid               ----商品规格表
                        where  rg.rental_id=@goodsid";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@goods_id", entity.GoodsId, SqlDbType.Int, 4);
                param.AddParam("@goods_num", entity.GoodsNum, SqlDbType.Int, 4);
                param.AddParam("@goods_month", entity.GoodsMonth, SqlDbType.Int, 4);
                param.AddParam("@goods_colorid", entity.GoodsColorId, SqlDbType.Int, 4);
                param.AddParam("@goods_normid", entity.GoodsNormId, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_OrderConfirm>.GetEntity(dt.Rows[0]);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "订单生成参数判断是否正确--订单确认页", ex);
                return null;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-09
        /// 描述：提交订单--订单确认页
        /// </summary>
        /// <param name="entity">订单生成需要的参数</param>
        /// <returns></returns>
        public int Web_SubmitOrder(Web_OrderParameterEntity entity)
        {
            try
            {
                #region sql
                var sql = @"
                declare @goodsid int,   @goodsnum int, @goodsmonth int, 
                        @goodscolorid int, @goodsnormid int, 
                        @deliveryid int, @userid int,
                        @isphoneconfirm int, @shippingmethod int,
                        @ordernumber nvarchar(100),
                        @usernickname nvarchar(20),
                        @goodsotherprices nvarchar(4000)
        
                select  @goodsid=@goods_id, @goodsnum=@goods_num,   @goodsmonth=@goods_month,   
                        @goodscolorid=@goods_colorid,   @goodsnormid=@goods_normid, 
                        @deliveryid =@delivery_id, @userid=@user_id,
                        @isphoneconfirm=@order_isphoneconfirm,  @shippingmethod=@order_shippingmethod,
                        @ordernumber=@order_number,
                        @usernickname=@user_nickname,
                        @goodsotherprices=@goods_otherprices

                declare @results int  --返回结果 -1未知0失败2成功
                set @results=-1

                /*商品表中的数据*/
                declare @merchantid int, 
                        @deposit money,
                        @monthprice money,
                        @ftempid int,
                        @lowestmonth int,
                        @sellmonth int,
                        @weight decimal(18,1),
                        @volume decimal(18,1)

                select @merchantid=rg.merchant_id         --商铺Id
                      ,@deposit=rg.rental_deposit         --押金
                      ,@monthprice=(case when @goodscolorid>0 and @goodsnormid>0 then gp.goods_price else rg.rental_monthprice end)        --月价格
                      ,@ftempid=rg.ftemp_id  --运费模版
                      ,@lowestmonth=substring(tt.[ttemp_months]+',',0,charindex(',',tt.[ttemp_months]+','))                                --最低期租期
                      ,@sellmonth= reverse(substring(reverse(','+tt.[ttemp_months]),1,charindex(',',reverse(','+tt.[ttemp_months])) - 1))  --代售期
                      ,@weight=rental_weight --重量
                      ,@volume=rental_volume --体积
                from dbo.RentalGoodses rg with(nolock) 
                left join dbo.GoodsPrices gp with(nolock) on rg.rental_id=gp.goods_id and gp.goodscolor_id=@goodscolorid and gp.goodsnorm_id=@goodsnormid   ----商品价格表 
                left join dbo.TenancyTemplates tt with(nolock) on rg.ttemp_id=tt.ttemp_id   ----租期表
                where rg.rental_id=@goodsid


                /*计算运费*/

                --获取运费模版的基本信息
                declare @ftemp_cityid int ,@isfreeship bit,@ftempmode int
                select @ftemp_cityid=ftemp_cityid, --发货城市
                       @isfreeship=ftemp_isfreeship, --是否包邮
                       @ftempmode=ftemp_mode --计价方式
                from dbo.FreightTemplates with(nolock)
                where ftemp_id=@ftempid
                --获取收货地址的信息
                declare @delivery_cityid int
                select @delivery_cityid=delivery_cityid from dbo.DeliveryAddresses with(nolock)  where delivery_id=@deliveryid and [user_id]=@userid  

                declare @freightcost  money
                --不包邮
                if(0=@isfreeship)
                begin
	                --获取商品的总重量、件数、体积
	                declare @GoodsTotal decimal(18,1)   --计算总量
	                select @GoodsTotal=(case @ftempmode when 1 then @goodsnum              --按件数
										           when 2 then @goodsnum*@weight      --按重量
										           when 3 then @goodsnum*@volume      --按体积
						            end) 
	 
                    --获取商品的运费
                    select @freightcost= case when @GoodsTotal> fcost_firstime
                                         then
			                                 fcost_firstprice +
			                                 (floor((@GoodsTotal-fcost_firstime)/fcost_continuetime)* fcost_continueprice) +
			                                 case when (@GoodsTotal-fcost_firstime)%fcost_continuetime>0
				                                   then fcost_continueprice else 0 end
                                         else
                                             fcost_firstprice
                                         end
                         
                    from dbo.FreightCosts fc with(nolock)
                    where ftemp_id=@ftempid and 
                          fcost_cityids=case when @delivery_cityid=@ftemp_cityid then @ftemp_cityid else '-1' end
                end
                --包邮
                else
                begin
                  set @freightcost=0
                end

                begin tran --开始执行事务 

                /*订单表添加*/
                insert dbo.Orders (
			                [user_id]
                              ,user_nickname
			                ,oper_id ,order_type
			                ,order_number
			                ,order_provinceid
			                ,order_provincename
			                ,order_cityid
			                ,order_cityname
			                ,order_districtid
			                ,order_districtname
			                ,order_address
			                ,order_code
			                ,order_realname
			                ,order_tel
			                ,order_restel
			                ,order_expresscompany
			                ,order_expressnum
			                ,order_freightcost
			                ,order_paytype
			                ,order_state
			                ,order_totalprice
			                ,order_totaldeposit
			                ,order_shippingmethod
			                ,order_isphoneconfirm
			                ,order_monthprice
			                ,order_invoicemode
			                ,order_invoicerise
			                ,order_invoicetitle
			                ,order_invoicetype
			                ,order_isdiscoupon
			                ,order_disprice
			                ,order_iscomment
			                ,order_createtime
			                ,order_updatetime
			                ,order_isdel
                )
                select @userid                 --用户Id
                      ,@usernickname           --昵称
                      ,@merchantid             --商铺用户Id
                      ,1                       --订单类型1租2售
                      ,@ordernumber            --订单编号

                      ,[delivery_provinceid]   --省份Id
	                  ,[delivery_provincename] --省份名称
	                  ,[delivery_cityid]       --城市Id
	                  ,[delivery_cityname]     --城市名称
	                  ,[delivery_districtid]   --区域Id
	                  ,[delivery_districtname] --区域名称
	                  ,[delivery_address]      --详细地址
	                  ,[delivery_code]         --邮编
	                  ,[delivery_realname]     --收获人
	                  ,[delivery_tel]          --电话
	                  ,[delivery_restel]       --备用电话
                      ,null                    --快递/物流公司名称
                      ,null                    --快递/物流单号
                      ,@freightcost            --订单运费
                      ,0                       --支付方式
                      ,1                       --订单状态1待付款
                      ,@freightcost+@deposit*@goodsnum +@monthprice*@goodsnum*@lowestmonth  --订单总价格
                      ,@deposit*@goodsnum      --订单押金
                      ,@shippingmethod         --送货方式
                      ,@isphoneconfirm         --是否电话确认
                      ,@monthprice*@goodsnum   --每月总价格
                      ,0                       --发票方式
                      ,0                       --发票抬头
                      ,null                    --发票抬头内容
                      ,0                       --发票内容
                      ,0                       --是否优惠券
                      ,0                       --优惠券金额
                      ,0                       --是否评论
                      ,getdate()               --创建时间
                      ,getdate()               --更新时间
                      ,0                       --是否删除  
                from dbo.DeliveryAddresses da with(nolock) 
                where da.delivery_id=@deliveryid and da.[user_id]=@userid  
                declare @order_id int
                set @order_id=@@IDENTITY


                /*订单扩展表添加*/
                insert into [dbo].[OrderOthers] ([order_id]) VALUES (@order_id)
                /*订单商品表添加*/
                insert dbo.OrderGoodses (
				                 order_id
				                ,goods_id
				                ,ordergoods_type
				                ,ordergoods_title
				                ,ordergoods_guid
				                ,ordercolor_name
				                ,ordercolor_code
				                ,ordernorm_name
				                ,ordergoods_num
				                ,ordergoods_deposit
				                ,ordergoods_monthprice
				                ,ordergoods_month
				                ,ordergoods_latefees
				                ,ordergoods_lowestmonth
                                  ,ordergoods_sellmonth
				                ,ordergoods_discount
				                ,ordergoods_isdel
                                  ,goodsprice_id
                )
                select @order_id                        --订单Id
                      ,rg.rental_id                     --商品Id
                      ,1                                --商品1租2售
                      ,rg.rental_title                  --商品标题
                      ,rg.rental_guid                   --商品Guid
                      ,gc.color_name                    --颜色名称
                      ,gc.color_code                    --颜色代码
                      ,gn.norm_name                     --规格名称
                      ,@goodsnum                        --购买数量
                      ,rental_deposit                   --商品押金
                      ,@monthprice                      --月租金
                      ,@goodsmonth                      --租期
                      ,rental_latefees                  --带纳金
                      ,@lowestmonth                     --最低起租期
                      ,@sellmonth                       --代售期
                      ,0                                --商品折扣
                      ,0                                --是否删除
                      ,gp.goodsprice_id                 --商品价格Id
                from dbo.RentalGoodses rg  with(nolock)                                  ----租商品表
                left join dbo.TenancyTemplates tt with(nolock) on rg.ttemp_id=tt.ttemp_id   ----租期表                               
                left join dbo.GoodsColors gc with(nolock) on gc.goodscolor_id=@goodscolorid            ----商品颜色表
                left join dbo.GoodsNorms gn with(nolock) on gn.goodsnorm_id=@goodsnormid               ----商品规格表
                left join dbo.GoodsPrices gp with(nolock) on gp.goodscolor_id=@goodscolorid and gp.goodsnorm_id=@goodsnormid   ----商品价格表
                where rg.rental_id=@goodsid

                declare @ordergoods_id int
                set @ordergoods_id=@@IDENTITY

                /*添加订单商品租期*/
                Declare @nowdate datetime,
                        @monthnum int

                Set @nowdate=getdate()
                Set @monthnum=1

                While(@monthnum<=@lowestmonth)
                Begin
                INSERT INTO [dbo].[GoodsTenancies]
                           ([ordergoods_id]
                           ,[tenancy_price]
                           ,[tenancy_monthtime]
                           ,[tenancy_isdelivery]
                           ,[tenancy_isfavorable]
                           ,[tenancy_favorableprice]
                           ,[tenancy_sort]
                           ,[tenancy_createtime]
                           ,[tenancy_updatetime]
                           ,[tenancy_isdel])
	                 VALUES
		                   (@ordergoods_id          --订单Id
		                   ,@monthprice         --月价格
		                   ,dateadd(month,@lowestmonth,@nowdate) --当月时间
		                   ,0    --是否交付
		                   ,0    --是否优惠
		                   ,0         --优惠价格
		                   ,0         --排序
		                   ,getdate() --创建时间
		                   ,getdate() --修改时间
		                   ,0) -- 是否删除

                     Set @monthnum=@monthnum+1
                End

                /*修改商品数量*/
                If(@goodscolorid>0 and @goodsnormid>0) -- 有尺寸有颜色
                Begin
                    --商品扩展表
                    Update [dbo].[RentalOthers] Set [other_prices]=@goodsotherprices
                    Where rental_id=@goodsid
                    --商品价格表
				  Update [dbo].[GoodsPrices] Set [goodsprice_num] = [goodsprice_num]-@goods_num
				  Where goods_id=@goodsid and goodscolor_id=@goodscolorid and goodsnorm_id=@goodsnormid
                End
                Else --其它
                Begin
                    --商品表
                    Update [dbo].[RentalGoodses] set [rental_number]=[rental_number]-@goods_num
                    Where rental_id=@goodsid
                End


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
                select  @results as Id
             ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@goods_id", entity.GoodsId, SqlDbType.Int, 4);
                param.AddParam("@goods_num", entity.GoodsNum, SqlDbType.Int, 4);
                param.AddParam("@goods_month", entity.GoodsMonth, SqlDbType.Int, 4);
                param.AddParam("@goods_colorid", entity.GoodsColorId, SqlDbType.Int, 4);
                param.AddParam("@goods_normid", entity.GoodsNormId, SqlDbType.Int, 4);
                param.AddParam("@delivery_id", entity.DeliveryId, SqlDbType.Int, 4);
                param.AddParam("@user_id", entity.UserId, SqlDbType.Int, 4);
                param.AddParam("@order_isphoneconfirm", entity.IsPhoneConfirm, SqlDbType.Int, 4);
                param.AddParam("@order_shippingmethod", entity.ShippingMethod, SqlDbType.Int, 4);
                param.AddParam("@order_number", entity.OrderNumber, SqlDbType.NVarChar, 100);
                param.AddParam("@user_nickname", entity.UserNickName, SqlDbType.NVarChar, 20);
                param.AddParam("@goods_otherprices", entity.GoodsOtherPrices, SqlDbType.NVarChar, 4000);
                
                #endregion
                #region 返回
                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "提交订单--订单确认页", ex);
                return -1;
            }
        }
        #endregion

        #region 订单管理(租)—ningjd

        /// <summary>
        /// 获取订单列表(租)
        /// </summary>
        /// <param name="areaEnum">订单区域Enum</param>
        /// <param name="title">商品名称</param>
        /// <param name="number">订单编号</param>
        /// <param name="userNikeName">买家昵称</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public List<OrderManageEntity> GetRentalOrdersList(OrderStateAreaType areaEnum, OrderState? orderState, string title, string number, string userNikeName, int index, int pageSize, out int totalRecord, out int totalPage, int? mid)
        {
            try
            {
                #region 存储过程分页
                //表名
                string tableName = @"OrderGoodses(NOLOCK) as t1 right join Orders(NOLOCK) as t2 on t1.order_id = t2.order_id left join LocalUsers(NOLOCK) as t3 on t2.user_id = t3.user_id";
                #region 查询条件
                StringBuilder where = new StringBuilder("t2.order_isdel = 'false' and t2.order_type=1");

                // 订单状态过滤
                //if (orderState.HasValue)
                //    where.AppendFormat(" and t2.order_state={0}", Auxiliary.ToInt32(orderState.Value));
                //else if (areaEnum == OrderStateAreaType.WaitPay) //未付款区
                //    where.Append(" and t2.order_state in (1,2)");
                //else if (areaEnum == OrderStateAreaType.SendGoods) //发货区
                //    where.Append(" and t2.order_state in (3,4,5)");
                //else if (areaEnum == OrderStateAreaType.RentGoods) //租用区
                //    where.Append(" and t2.order_state in (6,7,8)");
                //else if (areaEnum == OrderStateAreaType.ReturnAndBackGoods) //退租及退换货区
                //    where.Append(" and t2.order_state in (9,10,12,13,15,16)");
                //else if (areaEnum == OrderStateAreaType.Settlement) //结算区
                //    where.Append(" and t2.order_state in (11,14,21,22,23,24,25,26)");
                //else if (areaEnum == OrderAreaEnum.Success) //成功订单
                //    where.Append(" and t2.order_state in (17,18)");
                //else if (areaEnum == OrderAreaEnum.Closed) //关闭订单
                //    where.Append(" and t2.order_state in (19,20)");
                if (orderState.HasValue && orderState.Value != OrderState.All && orderState.Value != OrderState.UnKnown)
                {
                    where.AppendFormat(" and t2.order_state={0}", (int)orderState.Value);
                }
                else if (areaEnum != OrderStateAreaType.All)
                {
                    where.Append(" and t2.order_state in (" + new OrderStateEnumOper().GetOrderState(areaEnum) + ")");
                }
                //商户Id
                if (mid != null)
                {
                    where.AppendFormat(" and t2.oper_id={0}", mid);
                }
                // 商品名称过滤
                if (!string.IsNullOrEmpty(title))
                    where.AppendFormat(" and t1.ordergoods_title like '%{0}%'", title);
                // 订单编号过滤
                if (!string.IsNullOrEmpty(number))
                    where.AppendFormat(" and t2.order_number like '%{0}%'", number);
                // 买家昵称过滤
                if (!string.IsNullOrEmpty(userNikeName))
                    where.AppendFormat(" and t3.user_nickname like '%{0}%'", userNikeName);

                #endregion

                //排序
                string orderBy = " t2.order_updatetime desc";

                //列名
                string columnList = @"t2.[order_id] as OrderId,[order_number] as Number,[goods_id] as GoodsId,[order_updatetime] as UpdateTime,t3.[user_nickname] as UserNikeName,
                                            [ordergoods_title] as Title,[ordergoods_guid] as Guid,[ordercolor_name] as ColorName,[ordernorm_name] as NormName,[ordergoods_num] as GoodsNum,
                                            [ordergoods_monthprice] as MonthPrice,[order_totalprice] as TotalPrice,[order_freightcost] as FreightCost,[order_state] as State";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where.ToString(), orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                #endregion

                if (!Auxiliary.CheckDt(dt))
                {
                    return new List<OrderManageEntity>();
                }
                else
                {

                    List<OrderManageEntity> list = DataHelper<OrderManageEntity>.GetEntityList(dt);
                    return list;
                }
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
        /// 作者：maq
        /// 时间：2014年5月26日14:54:16
        /// 描述：出租的订单查询
        /// </summary>
        /// <param name="rentOrderPms"></param>
        /// <returns></returns>
        public PageData<OrderManageEntity> GetRentalOrdersList(RentOrderPms rentOrderPms)
        {

            int pageCount;
            int recoredCount;
            List<OrderManageEntity> list = GetRentalOrdersList((OrderStateAreaType)rentOrderPms.OrderAreaType, rentOrderPms.OrderStateType,
                rentOrderPms.GoodsName, rentOrderPms.OrderNum, rentOrderPms.BuyersNickName,
                rentOrderPms.PageIndex, rentOrderPms.PageSize, out recoredCount, out pageCount, rentOrderPms.MerchantId);
            PageData<OrderManageEntity> pageData = new PageData<OrderManageEntity>(pageCount, recoredCount, rentOrderPms.PageIndex, rentOrderPms.PageSize);
            pageData.DataList = list;
            return pageData;
        }



        #endregion

        #region   作者：maq   时间：2014年5月28日14:25:31   描述：订单状态相关操作

        /// <summary>
        /// 退货同意
        /// </summary>
        /// <returns></returns>
        public Boolean BackGoodsAgree(int orderId)
        {
            string strSql = "update Orders set order_state=13 where order_id=@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderId);
            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) == 1;
        }
        /// <summary>
        /// 发送起租协议
        /// </summary>
        /// <returns></returns>
        public Boolean SendAgreement(int orderId)
        {
            string strSql = "update Orders set order_state = 6,order_updatetime='" + DateTime.Now + "' where order_id =@id; ";
            strSql += "update OrderOthers set orderother_sendsgreementtime='" + DateTime.Now + "' where order_id=@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderId);
            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) == 2;
        }

        /// <summary>
        /// 退租同意
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean BackRentAgree(int orderId)
        {
            string strSql = "update OrderOthers set orderother_surrendertime='" + DateTime.Now + "' where order_id =@id;";
            strSql += "update Orders set order_state=10,order_updatetime='" + DateTime.Now + "' where  order_id =@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderId);
            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) == 2;
        }
        /// <summary>
        /// 换货同意
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean ExChangeGoodsAgree(int orderId, OrderState orderState)
        {

            orderState = GetOrderStateBefore(orderId);
            if (orderState == OrderState.UnKnown)
            {
                return false;
            }
            if (orderState == OrderState.Renting)//租用中
            {
                string strSql = " update Orders set order_state =16,order_updatetime='" + DateTime.Now + "' where order_id=@id;";
                strSql += @"INSERT INTO [dbo].[OrderReturns]
                       ([orderreturn_begintime]
                       ,[orderreturn_state]
                       ,[orderreturn_createtime],order_id)
                         VALUES(@btime,@state,@ctime,@id)";
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", orderId);
                sqlParam.AddParam("@state", 2);
                sqlParam.AddParam("@btime", DateTime.Now);
                sqlParam.AddParam("@ctime", DateTime.Now);
                return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) == 2;
            }
            else if (orderState == OrderState.Received || orderState == OrderState.WaitRent)//待确认起租和已收货
            {
                string strSql = " update Orders set order_state =16,order_updatetime='" + DateTime.Now + "' where order_id=@id;";
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", orderId);
                return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) == 1;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 得到以前的订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderState GetOrderStateBefore(int orderId)
        {
            string strSql = "select orderother_orderstate from OrderOthers with(nolock) where order_id =@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderId);
            string orderState = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
            if (!string.IsNullOrEmpty(orderState))
            {
                return (OrderState)Convert.ToInt32(orderState);
            }
            else
            {
                return OrderState.UnKnown;
            }
        }

        /// <summary>
        /// 订单状态返回到上级状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean OrderGoBack(int orderId, int orders, string message)
        {
            Boolean result = false;
            string strSql = "select  * from OrderOthers with (nolock) where order_id =@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderId);
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql, sqlParam))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    string orderState = table.Rows[0]["orderother_orderstate"].ToString();
                    string datef = "";
                    switch (orders)
                    {
                        case 9://申请退租
                            switch (orderState)
                            {
                                case "7"://租用中
                                    datef = "orderother_accruetime";
                                    break;
                                case "8"://带续租
                                    datef = "orderother_renewtime";
                                    break;
                            }
                            break;
                        case 12://申请退货
                            switch (orderState)
                            {
                                case "3"://代发货退货
                                    datef = "orderother_paidtime";
                                    break;
                                case "4"://待收货
                                    datef = "orderother_deliverytime";
                                    break;
                                case "5"://已收货，驳回到已收货
                                    datef = "orderother_harvesttime";
                                    break;
                                case "6"://待确认起租换货
                                    datef = "orderother_sendsgreementtime";
                                    break;
                            }
                            break;
                        case 15://申请换货
                            switch (orderState)
                            {
                                case "5"://已收货，驳回到已收货
                                    datef = "orderother_harvesttime";
                                    break;
                                case "7"://租用中换货
                                    datef = "orderother_accruetime";
                                    break;
                                case "6"://待确认起租换货
                                    datef = "orderother_harvesttime";
                                    break;
                            }
                            break;
                    }
                    if (datef == "")
                    {
                        return false;
                    }
                    string accruetime = table.Rows[0][datef].ToString();
                    if (!string.IsNullOrEmpty(orderState) && !string.IsNullOrEmpty(accruetime))
                    {
                        sqlParam = new SqlParam();
                        sqlParam.AddParam("@id", orderId);
                        string strSql1 = "update Orders set order_state=@state,order_updatetime=@updateTime where order_id=@id;";
                        strSql1 += "update OrderOthers set orderother_rejectreason = @reason  where order_id=@id ";
                        sqlParam.AddParam("@state", orderState);
                        sqlParam.AddParam("@updateTime", accruetime);
                        sqlParam.AddParam("@reason", message);
                        result = KYJ_ZushouWDB.SqlExecute(strSql1, sqlParam) == 2;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 修改赔损金额,改变订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Boolean ChangeMoney(int orderId, OrderState orderState, decimal money, OrderState oldState)
        {
            Boolean result = false;
            if (oldState == OrderState.ExchangeGoodsCheck && money == 0)
            {
                string strSql = "update Orders set order_state=3 where order_id=@id";
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", orderId);
                result = KYJ_ZushouRDB.SqlExecute(strSql, sqlParam) == 1;
            }
            else
            {
                string strSql = "update OrderOthers set orderother_loseprice=@money where order_id=@orderId;";
                strSql += " update Orders set order_state =@orderState where  order_id=@orderId ";
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@orderId", orderId);
                sqlParam.AddParam("@money", money);
                sqlParam.AddParam("orderState", (int)orderState);
                result = KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) == 2;
            }
            return result;
        }

        /// <summary>
        /// 换货验收撤销
        /// </summary>
        /// <returns></returns>
        public Boolean CancleExchange(int orderId)
        {
            string strSql = "select orderreturn_begintime from OrderReturns with(nolock) where order_id=@id and orderreturn_endtime is null";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderId);
            string reDate = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
            if (string.IsNullOrEmpty(reDate))
            {
                return false;
            }
            else
            {
                strSql = "select orderother_orderstate from OrderOthers where order_id=@id";
                sqlParam = new SqlParam();
                sqlParam.AddParam("@id", orderId);
                string strOrderState = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
                if (string.IsNullOrEmpty(strOrderState))
                {
                    return false;
                }
                else
                {
                    strSql = "update Orders set order_state=@state,order_updatetime=@updateTime where order_id=@id;";
                    strSql += "delete from OrderReturns where order_id=@id and orderreturn_endtime is null";
                    sqlParam = new SqlParam();
                    sqlParam.AddParam("@state", strOrderState);
                    sqlParam.AddParam("@updateTime", reDate);
                    sqlParam.AddParam("@id", orderId);
                    return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) > 0;
                }
            }
        }

        /// <summary>
        /// 根据订单ID得到UserId
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public int GetUserId(int orderid)
        {
            string strSql = "select user_id from Orders with (nolock) where order_id=@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderid);
            string userId = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
            if (!string.IsNullOrEmpty(userId))
            {
                return Auxiliary.ToInt32(userId);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 得到订单的总押金
        /// </summary>
        /// <returns></returns>
        public decimal GetOrderDeposit(int orderId)
        {
            string strSql = "select order_totaldeposit from Orders with(nolock) where order_id =@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", orderId);
            return Auxiliary.ToDecimal(KYJ_ZushouRDB.GetFirst(strSql, sqlParam));
        }

        /// <summary>
        /// 修改 金额，不改变订单状态，指示修改订单赔损金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public Boolean ChangeMoney(int orderId, decimal money)
        {
            string strSql = " update OrderOthers set orderother_loseprice =@money where order_id=@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@money", money);
            sqlParam.AddParam("@id", orderId);
            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam) == 1;
        }

        /// <summary>
        /// 商户结算
        /// </summary>
        /// <returns></returns>
        public Boolean MerchantSettlement(int merId, OrderState orderState, int orderId, out string message)
        {
            Boolean result = false;
            #region 订单状态
            OrderState state = OrderState.UnKnown;
            switch (orderState)
            {
                case OrderState.BackRentSettlement:
                    state = OrderState.BackRentSucceed;
                    break;
                case OrderState.BackGoodsSettlement:
                    state = OrderState.BackGoodsClosed;
                    break;
                case OrderState.SaleSettlement:
                    state = OrderState.SaleSucceed;
                    break;
            }
            if (state == OrderState.UnKnown)
            {
                message = "订单状态有误！";
                return false;
            }
            #endregion
            #region SQL
            string strSql = @"
                --DECLARE @merId INT
                --DECLARE @orderState INT
                --DECLARE @orderId INT
                DECLARE @orderMoney MONEY
                DECLARE @merMoney MONEY
                DECLARE @result INT
                DECLARE @userId INT
                DECLARE @monthPrice  DECIMAL(8,2)
                DECLARE @dayPrice  DECIMAL(8,2)
                DECLARE @totalPrice DECIMAL(8,2)
                DECLARE @dayNum INT
                --SET @orderState=17
                --SET @merId=7
                SET @result=0
                --SET @orderId=186

                SELECT @orderMoney=order_totaldeposit,@userId=user_id FROM dbo.Orders WHERE order_id=@orderId 
                SELECT @merMoney =account_price FROM dbo.M_Accounts WHERE merchant_id=@merId
                --result状态
                --0、执行失败
                --1、商户账户余额不足
                --2、商户扣款成功，等待用户加钱
                --3、数据有误
                IF @orderMoney<=0 OR @merMoney<=0
	                BEGIN
		                SET @result=3
	                END
                ELSE
	                BEGIN
	                IF @orderMoney>@merMoney
		                BEGIN
			                set @result=1--商户账户余额不足
		                END
	                ELSE
		                BEGIN
			                BEGIN TRANSACTION
                            SELECT @monthPrice= rental_monthprice FROM dbo.RentalGoodses WHERE rental_id=( SELECT goods_id FROM OrderGoodses WHERE order_id=@orderId)
                            SELECT @dayNum= SUM(CAST(DATEDIFF(hh,orderreturn_begintime,orderreturn_endtime)/24.0 as decimal(38, 0 )))  FROM dbo.OrderReturns WITH(NOLOCK) WHERE order_id=@orderId
                            SET @dayPrice=@monthPrice/30
                            SET @totalPrice=@dayPrice*@dayNum
                            if @totalPrice is null
                            begin
                               set @totalPrice=0
                            end
                            if @orderMoney is null
                            begin
                               set @orderMoney=0
                            end                           
			                UPDATE dbo.Orders SET order_totaldeposit=0,order_state=@orderState WHERE order_id=@orderId
			                UPDATE dbo.M_Accounts SET account_price=(account_price-@orderMoney-@totalPrice) WHERE merchant_id=@merId
			                IF @@ERROR<>0
				                BEGIN
					                ROLLBACK TRANSACTION
					                SET @result=0--执行失败
				                END
			                ELSE
				                BEGIN
					                COMMIT TRANSACTION
					                SET @result=2--商户余额足以支付押金
				                END
		                END	
	                END
                SELECT @result AS result,@orderMoney+@totalPrice AS money,@userId AS userId";
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@orderId", orderId);
            sqlParam.AddParam("@merId", merId);
            sqlParam.AddParam("@orderState", state);
            #endregion
            #region 数据处理
            using (DataTable table = KYJ_ZushouWDB.GetTable(strSql, sqlParam))
            {
                if (Auxiliary.CheckDt(table))
                {
                    decimal money = Auxiliary.ToDecimal(table.Rows[0]["money"].ToString());
                    int userId = Auxiliary.ToInt32(table.Rows[0]["userId"].ToString());
                    switch (table.Rows[0]["result"].ToString())
                    {
                        case "0":
                            message = "执行失败,请稍后再试!";
                            break;
                        case "1":
                            message = "商户账户余额不足";
                            break;
                        case "2":
                            LocalUserDal localUserDal = new LocalUserDal();
                            if (localUserDal.UserMoneyAdd(money, userId))
                            {
                                result = true;
                                message = "结算成功！";
                                SaleNumAdd(orderId,merId);
                            }
                            else
                            {
                                message = "结算失败！";
                            }
                            break;
                        case "3":
                            message = "账户余额不足";
                            break;
                        default:
                            message = "未知错误！";
                            break;
                    }
                }
                else
                {
                    message = "执行失败！";
                }
            }
            #endregion
            return result;
        }
        /// <summary>
        /// 销量自增
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="merId"></param>
        public void SaleNumAdd(int orderId, int merId)
        {
            string strSql = @"DECLARE @DaiShouQi int
                            DECLARE @PayTimes int
                            DECLARE @GoodsID int
                            DECLARE @orderGoodsId int
                            SELECT  @orderGoodsId = ordergoods_id,@DaiShouQi= ordergoods_sellmonth ,@GoodsID=goods_id FROM OrderGoodses WHERE order_id=@oid
                            SELECT @PayTimes =COUNT(1) FROM GoodsTenancies WHERE ordergoods_id =@orderGoodsId
                            IF @PayTimes=@DaiShouQi
	                            BEGIN
		                            UPDATE dbo.RentalGoodses SET rental_salesvolume=rental_salesvolume+1 WHERE rental_id=@GoodsID AND merchant_id=@merId
	                            END
                            SELECT @PayTimes ,@DaiShouQi,@GoodsID ,@@rowcount";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@oid", orderId);
            sqlParam.AddParam("@merId", merId);
            KYJ_ZushouRDB.SqlExecute(strSql, sqlParam);
        }


        /// <summary>
        /// 判断该订单是否是当前商户的
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="merId"></param>
        /// <returns></returns>
        public Boolean IsMerOrder(int orderId, int merId)
        {
            string strSql = "select count(1) from Orders with(nolock) where oper_id=@merId and order_id=@orderId";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@orderId", orderId);
            sqlParam.AddParam("@merId", merId);
            string result = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
            if (Auxiliary.ToInt32(result) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 商户支付成功回调函数
        /// </summary>
        /// <returns></returns>
        public void MerPaySuccessCallBack(int merId, int orderId, decimal money)
        {
            string strSql = "";
        }


        #endregion
    }
}
