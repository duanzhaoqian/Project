using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;

namespace KYJ.ZS.Task.Task
{
    /// <summary>
    /// 作者：baiyan
    /// 时间：2014-06-05
    /// 描述;租用中订单到期状态变为待续租
    /// </summary>
    public class OrderRenewalStateTask : BaseTask
    {

        public override string Name
        {
            get { return "租用中订单到期状态变为待续租"; }
        }

        public override void Run()
        {
            GetOrder();
//            int resule = 0;
//            var sql = @"update dbo.Orders set order_state=8 where order_id in
//                        (
//	                        select
//	                        og.order_id
//	                        from dbo.OrderGoodses(nolock) as og
//	                        inner join dbo.GoodsTenancies(nolock) as gt on og.ordergoods_id=gt.ordergoods_id
//	                        where gt.tenancy_isdelivery='true' and gt.tenancy_isdel='false' and tenancy_id in
//	                        (
//		                        select max(tenancy_id) from GoodsTenancies where tenancy_isdelivery='true' and tenancy_isdel='false' group by ordergoods_id
//	                        )
//	                        and convert(varchar(10),tenancy_monthtime,120)<convert(varchar(10),getdate(),120)
//                        ) and order_state=7";
//            KYJ.ZS.Log4net.RecordLog.ServiceLogs("baiyan", this.Name + string.Format(": run; DateTime:{0}", DateTime.Now));
//            try
//            {
//                resule = KYJ_ZushouWDB.SqlExecute(sql);
//            }
//            catch (Exception ex)
//            {

//                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", this.Name, ex);
//            }
//            finally {
//                KYJ.ZS.Log4net.RecordLog.ServiceLogs("baiyan", this.Name + string.Format(": end; DateTime:{0}; resule: {1}", DateTime.Now, resule));
//            }

        }
        /// <summary>
        /// 查询所有等于租用中状态的订单
        /// </summary>
        public void GetOrder() {
            int resule = 0;
            var sql = @"select
                        og.ordergoods_id,--订单商品ID
                        og.order_id,--订单ID
                        og.ordergoods_type,--订单类型 1 租 2 售
                        og.ordergoods_month,--租期
                        og.ordergoods_lowestmonth,--最低租期
                        og.ordergoods_sellmonth,--待售期
                        og.ordergoods_latefees,--滞纳金
                        o.order_number,--订单号
                        o.order_state,--状态
                        o.order_createtime,--创建时间
                        o.order_updatetime--修改时间
                        from dbo.OrderGoodses(nolock) as og
                        inner join dbo.Orders(nolock) as o on og.order_id=o.order_id
                        where og.ordergoods_type=1 and o.order_state=7 and og.ordergoods_isdel='false' and o.order_isdel='false'";
            //KYJ.ZS.Log4net.RecordLog.ServiceLogs("baiyan", this.Name + string.Format(": run; DateTime:{0}", DateTime.Now));
            try
            {
                //查询出所有订单状态等于租用中的订单
                DataTable dt = KYJ_ZushouRDB.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //根据订单ID查询租期表
                       resule= GetGoodsTenancies(Auxiliary.ToInt32(item["ordergoods_id"]), Auxiliary.ToInt32(item["order_id"]));
                    }
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", this.Name, ex);
            }
            finally
            {
                KYJ.ZS.Log4net.RecordLog.ServiceLogs("baiyan", this.Name + string.Format(": end; DateTime:{0};resule: {1}", DateTime.Now, resule));
            }
        }
        /// <summary>
        /// 根据订单ID查询租期表
        /// </summary>
        /// <param name="orderId">订单ID</param>
        public int GetGoodsTenancies(int orderGoodsId, int orderId)
        {
            var sql = @"SELECT TOP 1 [tenancy_monthtime]
                         FROM [dbo].[GoodsTenancies](NOLOCK) where [ordergoods_id]=@ordergoods_id and [tenancy_isdelivery]='true' and [tenancy_isdel]='false' order by [tenancy_sort] desc";
            SqlParam param = new SqlParam();
            param.AddParam("@ordergoods_id", orderGoodsId);
            try
            {
                string time = KYJ_ZushouRDB.GetFirst(sql, param).ToString();
                if (!string.IsNullOrEmpty(time)) {
                    DateTime ti = string.IsNullOrEmpty(time) ? System.DateTime.Now : Convert.ToDateTime(time);
                    //如果租期表中tenancy_monthtime当月时间小于当前时间为已到租期     状态更改为待续租
                    if (ti <= System.DateTime.Now)
                    {
                        //根据订单ID更改订单状态为待续租
                       return UpdateOrderState(orderId);
                    }
                }
                
               
            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }
        /// <summary>
        /// 根据订单ID更改订单状态为待续租
        /// </summary>
        /// <param name="orderId"></param>
        public int UpdateOrderState(int orderId)
        {
            //订单表状态更改  updatetime 时间更改   订单扩展表待续租时间更改
            var sql = @"UPDATE [dbo].[Orders]
                       SET [order_state] = @order_state,
                           [order_updatetime]=@order_updatetime
                     WHERE order_id=@order_id;
                    UPDATE [dbo].[OrderOthers]
                       SET [orderother_renewtime] = @orderother_renewtime
                     WHERE order_id=@order_id";
            DateTime time = System.DateTime.Now;
            SqlParam param = new SqlParam();
            param.AddParam("@order_state", 8);
            param.AddParam("@order_id", orderId);
            param.AddParam("@order_updatetime", time);
            param.AddParam("@orderother_renewtime", time);
            try
            {
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
