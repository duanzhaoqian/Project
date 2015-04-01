using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Manager.Controllers.ActionFilters
{
    public class LoginCheckedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool invalidRequestUrl = true;
            var returnUrl = filterContext.HttpContext.Request.RawUrl;
            var serviceContext = filterContext.Controller.GetServiceContext();
            if (serviceContext != null && serviceContext.CurrentUser != null)
            {
                invalidRequestUrl = false;
            }
            if (invalidRequestUrl)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var url = new UrlHelper(filterContext.RequestContext).RouteUrl("Default", new { action = "login", controller = "common" });
                    var script = @"$.alert('登录超时，请重新登录！',280, 140, 3, function () {location.href='" + url + "';})";
                    filterContext.Result = new JavaScriptResult() { Script = script };
                }
                else
                {
                    filterContext.Result = new RedirectResult(
                       new UrlHelper(filterContext.RequestContext).RouteUrl("Default", new { action = "login", controller = "common", returnUrl = KYJ.ZS.Commons.PubConstant.ManagerBaseUrl + returnUrl }));
                }
            }
        }
    }
}
