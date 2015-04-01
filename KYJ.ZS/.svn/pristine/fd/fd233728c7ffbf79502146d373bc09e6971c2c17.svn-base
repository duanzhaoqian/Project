using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.Commons;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-27
    /// Desc：订单管理控制器
    /// </summary>
    public class HomeController:BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int adminId = 1; //当前账户ID
            int pageIndex = Auxiliary.ToInt32(Request.Params["pageIndex"], 1);
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            IList<LogIndexEntity> list = new LogBll().GetLogList(adminId, pageIndex, pageSize, out totalRecord, out totalPage);
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pagesize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(list);
        }
    }
}
