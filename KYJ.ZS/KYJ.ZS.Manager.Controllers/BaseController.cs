using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons.Common;
using System.Web.Mvc;
using KYJ.ZS.Models.Authority;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Commons;

namespace KYJ.ZS.Manager.Controllers
{
    [LoginChecked]
    public class BaseController : Controller
    {
        /// <summary>
        /// 服务上下文，用于获取当前登录用户信息
        /// </summary>
        public ServiceContext _ServiceContext = new ServiceContext(UserInfoType.MANAGERLOGIN);

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult != null)
            {
                viewResult.ViewData["_CurrentUser"] = _ServiceContext.CurrentUser;
            }
            base.OnActionExecuted(filterContext);
        }
       
    }
}
