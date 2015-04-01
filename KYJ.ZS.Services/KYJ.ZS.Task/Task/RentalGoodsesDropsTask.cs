﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Task.Task
{
    using KYJ.ZS.DAL.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/6/5 9:03:53
    /// 描述：
    /// </summary>
    public class RentalGoodsesDropsTask : BaseTask
    {
        public override string Name
        {
            get
            {
                return "租售商品下架服务";
            }
        }

        public override void Run()
        {
            var time = DateTime.Now;
            var vtime = string.Format("{0:g}", time);

            int resule = 0;
            string strSql = @"update RentalGoodses set rental_status=2 where rental_status =1 and rental_endtime = @ed and rental_isdel=0";

            var sqlParam = new SqlParam();
            sqlParam.AddParam("@ed", vtime);

            //KYJ.ZS.Log4net.RecordLog.ServiceLogs("wangyu", this.Name + string.Format(": run; DateTime:{0}", time));

            try
            {
                resule = KYJ_ZushouWDB.SqlExecute(strSql, sqlParam);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", this.Name, ex);
            }
            finally
            {
                KYJ.ZS.Log4net.RecordLog.ServiceLogs("wangyu", this.Name + string.Format(": end; DateTime:{0}; resule: {1}", time, resule));
            }
        }
    }
}