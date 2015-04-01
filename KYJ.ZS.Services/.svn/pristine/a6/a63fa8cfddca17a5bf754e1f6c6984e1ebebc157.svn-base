using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;
using System.Data;
using KYJ.ZS.Models.Orders;

namespace KYJ.ZS.Task.Task
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-06-05
    /// 描述;订单待付款、付款失败超过24小时服务
    /// </summary>
    public class OrderPendingPaymentTask : BaseTask
    {
        public override string Name
        {
            get
            {
                return "订单待付款、付款失败超过24小时服务(返回结果0失败1成功2没有需要修改订单)";
            }
        }
        public override void Run()
        {
            int result = 0;
            string strSql = @"
                                Declare @result int --返回结果0失败1成功2没有需要修改订单
                                Set @result=2
                                /*表变量*/
                                Declare @OrderService Table(OrderId int,GoodsId int,OrderGoodsNum int,GoodsPriceId int )
                                Insert @OrderService(OrderId,GoodsId,OrderGoodsNum,GoodsPriceId)
                                Select o.order_id as OrderId,
                                       og.goods_id as GoodsId,
                                       og.ordergoods_num as OrderGoodsNum, 
                                       og.goodsprice_id as GoodsPriceId
                                From dbo.Orders o With(NoLock)
                                Left Join dbo.OrderGoodses og With(NoLock) on o.order_id=og.order_id
                                Where order_state in (1,2) and                      --订单状态（1待付款2付款失败）
                                      dateadd(day,1,order_updatetime)<getdate() and --订单超过24小时
                                      order_type=1 and                              --订单类型（1租商品）
                                      order_isdel=0                                 --是否删除
                                /*判断是否需要修改操作*/
                                If Exists (Select 1 from @OrderService )
                                Begin
	                                Begin Tran --事务开始

	                                /*修改商品数量*/
	                                Update rg set rg.[rental_number]=rg.[rental_number]+OrderGoodsNum  --商品数量
	                                From [dbo].[RentalGoodses] rg With(NoLock) --商品表
	                                Left Join @OrderService os on rg.rental_id =os.GoodsId  --表变量
	                                where rg.rental_id =os.GoodsId and GoodsPriceId is null
	
	                                /*修改订单状态*/
	                                Update [dbo].[Orders]
	                                   Set [order_state] = 19,                             --订单状态（19订单关闭）
	                                       [order_updatetime] = getdate()                  --修改时间
	                                Where order_id in (Select OrderId From @OrderService)

	                                If @@error<>0 --判断有任何一条出现错误
	                                Begin 
		                                Rollback Tran --开始执行事务的回滚
		                                set @result=0 
		                                Delete From @OrderService --数据清理
	                                End
	                                Else
	                                Begin
		                                Commit Tran --执行这个事务的操作
		                                set @result=1 
	                                End   
                                End 

                                --返回执行结果
                                Select @result as Result   
                                --返回需要MQ执行的数据
                                Select OrderId,GoodsId,OrderGoodsNum,GoodsPriceId 
                                From @OrderService 
                                Where GoodsPriceId is not null
              ";
            try
            { 
                //执行SQL与语句获取结果
                var ds= KYJ_ZushouWDB.GetDataSet(strSql);
                if (ds!=null && ds.Tables.Count >=2)
                {
                    //获取第一张表的第一行第一列数据
                    result = ds.Tables[0].Rows[0]["Result"].ToString().ToInt();
                    //获取第二张表的数据
                    var list=DataHelper<Services_OrderPPEntity>.GetEntityList(ds.Tables[1]);
                    //判断是否向MSMQ中写入数据
                    if (result == 1 && list != null && list.Count > 0)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", this.Name, ex);
            }
            finally
            {
                KYJ.ZS.Log4net.RecordLog.ServiceLogs("dengfw", this.Name + string.Format(": end; DateTime:{0}; resule: {1}", DateTime.Now, result));
            }           
        }
    }
}
