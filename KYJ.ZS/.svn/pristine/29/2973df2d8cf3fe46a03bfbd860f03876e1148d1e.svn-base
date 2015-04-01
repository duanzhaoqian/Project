using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using System.Data;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.AccountLogs
{
    /// <summary>
    /// Auther:李晓波
    /// Time:2014-4-29
    /// Desc:商户账户日志
    /// </summary> 
    public class AccountLogDal
    {
        #region 获取商户账户流水明细

        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-29
        /// Desc:获取商户账户流水明细
        /// </summary> 
       /// <param name="id">商户id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageIndex">查询条件</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="TotalPage">总页数</param>
        /// <returns></returns>
        public List<AccountLog> GetList(int id, int pageIndex, int pageSize, out int totalCount, out int totalPage)
        {
            string tableList = " M_AccountLogs";
            string colList = "  mlog_createtime,mlog_desc,mlog_id";//显示列
            string where_ = " merchant_id=" + id;
            DataTable dt = KYJ_ZushouRDB.GetPages(pageIndex, where_, "mlog_createtime desc", colList, tableList, pageSize, true, out totalCount, out totalPage);
            if (Auxiliary.CheckDt(dt))
            {
                List<AccountLog> list = new List<AccountLog>();
                foreach (DataRow item in dt.Rows)
                {
                    AccountLog log = new AccountLog();
                    log.Id = Auxiliary.ToInt32(item["mlog_id"]);
                    log.CreateTime = Convert.ToDateTime(item["mlog_createtime"]);
                    log.Desc = item["mlog_desc"].ToString();
                    list.Add(log);
                }
                return list;
            }
            return null;
        }

        #endregion


    }
}
