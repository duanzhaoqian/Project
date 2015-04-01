using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Merchant.Controllers.ActionFilters
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
                filterContext.Result = new RedirectResult(
                   new UrlHelper(filterContext.RequestContext).RouteUrl("Default", new { action = "login", controller = "common", returnUrl = KYJ.ZS.Commons.PubConstant.MerchantBaseUrl + returnUrl }));
            }
        }
    }
}
