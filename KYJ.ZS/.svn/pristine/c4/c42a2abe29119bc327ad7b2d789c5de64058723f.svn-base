using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.BLL.AccountLogs;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
namespace KYJ.ZS.Merchant.Controllers
{
    public class OperationLogController : BaseController
    {
        AccountLogBll bll = new AccountLogBll();

        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-24
        /// 提现管理-流水明细
        /// 备注：2014年5月23日10:20:40 之后的修改 maq
        /// </summary>
        /// <returns></returns>       
        public ActionResult WaterSubsidiary(int? pageIndex)
        {
            List<AccountLog> liSubsidiary = new List<AccountLog>();
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            int index = pageIndex ?? 1;
            int totalCount = 0;
            int totalPage = 0;
            liSubsidiary = bll.GetList(id,index, 10, out  totalCount, out totalPage);
            PagedList<AccountLog> mPage = null;
            if (liSubsidiary != null)
            {
                mPage = new PagedList<AccountLog>(liSubsidiary, index, 10, totalCount);
            }
            ViewBag.Tag = 2;
            return View(mPage);
         
        }

    }
}
