using System.Web.Mvc;
using TXManager.Controllers;
using TXManagerWeb.Common;

namespace TXManagerWeb.Controllers
{
    public class LoginCheckedAttribute : ActionFilterAttribute
    {
        private bool _isLonginChecked = true;

        /// <summary>
        /// 是否判断登录，默认为true 判断
        /// </summary>
        public bool IsLonginChecked
        {
            get { return _isLonginChecked; }
            set { _isLonginChecked = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool invalidRequestUrl = true;

            var serviceContext = filterContext.Controller.GetServiceContext();
            if (IsLonginChecked)
            {
                if (serviceContext != null && serviceContext.CurrentUser != null)
                    invalidRequestUrl = false;
            }
            else
                invalidRequestUrl = false;
            if (invalidRequestUrl)
            {
                AuthenticationService.SignOut();
                // 租房管理平台登录页
                filterContext.Result = new RedirectResult(Auxiliary.Instance.ManagerUrl);
                //filterContext.Result = new RedirectResult(string.Format("{0}common/login.html", Auxiliary.Instance.ManagerUrl));
                //new UrlHelper(filterContext.RequestContext).Action("login", "common"));
            }
        }

    }
}