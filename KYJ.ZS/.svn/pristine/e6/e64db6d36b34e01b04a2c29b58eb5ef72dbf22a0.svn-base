using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.DAL.AccountLogs;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.BLL.AccountLogs
{
    /// <summary>
    /// Auther:李晓波
    /// Time:2014-4-29
    /// Desc:商户账户日志
    /// </summary> 
   public class AccountLogBll
    {
        AccountLogDal dal = new AccountLogDal();       
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
        public List<AccountLog> GetList(int id,  int pageIndex, int pageSize, out int totalCount, out int totalPage)
        {
           
            return dal.GetList(id, pageIndex, pageSize, out  totalCount, out  totalPage);
        }

      
    }
}
