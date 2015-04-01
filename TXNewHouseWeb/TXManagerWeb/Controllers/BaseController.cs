using System.Web.Mvc;
using TXBll.WebSite;
using TXManagerWeb.Common;
using TXModel.AdminLogs;

namespace TXManagerWeb.Controllers
{
    public class BaseController : Controller
    {
        public ServiceContext _ServiceContext = new ServiceContext();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult != null)
            {
                viewResult.ViewData["_CurrentUser"] = _ServiceContext.CurrentUser;
            }
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 添加管理员操作日志
        /// </summary>
        /// <param name="operAdminLog"></param>
        protected void ExecuteOperResult(Z_OperAdminLog operAdminLog)
        {
            new AdminService<Z_OperAdminLog>(new Z_OperAdminLog()
            {
                AdminID = operAdminLog.AdminID,
                CreateTime = operAdminLog.CreateTime,
                OperDes = operAdminLog.OperDes, //"会员管理-修改普通会员[" + id.Value + "]的账号状态。",
                OperIP = operAdminLog.OperIP
            }).ExecuteResult();
        }

    }
}