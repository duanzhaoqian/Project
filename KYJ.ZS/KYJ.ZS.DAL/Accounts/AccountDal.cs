using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;

namespace KYJ.ZS.DAL.Accounts
{

    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月6日11:25:55
    /// 描述：商户账户
    /// </summary>
    public class AccountDal
    {
        /// <summary>
        /// 获取账户总金额
        /// </summary>
        /// <param name="merId"></param>
        /// <returns></returns>
  
        public decimal GetMoney(int merId)
        {
            string strSql = "SELECT  account_price FROM [dbo].[M_Accounts] WHERE merchant_id=@id";
            SqlParam sqlParam =new SqlParam();
            sqlParam.AddParam("@id",merId);
            string result = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
            if (!string.IsNullOrEmpty(result))
            {
                return Convert.ToDecimal(result);
            }
            else
            {
                return 0;
            }
        }
    }
}
