using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;

namespace KYJ.ZS.Task.Task
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年6月5日16:42:57
    /// 描述：待确认起租的订单超过24小时，自动起租
    /// </summary>
    public class OrderStartRentTask : BaseTask
    {
        public override string Name
        {
            get { return "待确认起租的订单超过24小时，自动起租"; }
        }

        public override void Run()
        {
            int resule = 0;
            string idList = "";
            string strSql1 = @"SELECT order_id from Orders WHERE  [order_state] =6 and--订单状态（）
                                      dateadd(day,1,[order_updatetime])<getdate() and --订单超过24小时
                                      order_type=1 and                                --订单类型（1租商品）
                                      order_isdel=0 ";
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql1))
            {
                if (Auxiliary.CheckDt(table))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        idList += row["order_id"] + ",";
                    }
                    idList = idList.TrimEnd(',');
                    string strSql ="UPDATE [dbo].[Orders] SET [order_state] = 7 ,order_updatetime = getdate()  where order_id in (" + idList + ")";
                    strSql += "update OrderOthers set orderother_accruetime= getdate() where order_id in(" + idList + ")";
                    //KYJ.ZS.Log4net.RecordLog.ServiceLogs("maq", this.Name + string.Format(": run; DateTime:{0}", DateTime.Now));
                    try
                    {
                        resule = KYJ_ZushouWDB.SqlExecute(strSql);
                    }
                    catch (Exception ex)
                    {
                        KYJ.ZS.Log4net.RecordLog.RecordException("maq", this.Name, ex);
                    }
                    finally
                    {
                        KYJ.ZS.Log4net.RecordLog.ServiceLogs("maq", this.Name + string.Format(": end; DateTime:{0}; resule: {1}", DateTime.Now, resule));
                    }
                }
            }
        }
    }
}
