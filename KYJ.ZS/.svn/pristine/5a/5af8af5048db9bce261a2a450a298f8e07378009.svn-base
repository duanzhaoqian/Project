using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Merchant.Controllers.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public bool NoCache { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new HttpNotFoundResult();
            if (NoCache)
            {
                filterContext.HttpContext.Response.Buffer = true;
                filterContext.HttpContext.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                filterContext.HttpContext.Response.Expires = 0;
                filterContext.HttpContext.Response.CacheControl = "no-cache";
            }
        }
    }
}
