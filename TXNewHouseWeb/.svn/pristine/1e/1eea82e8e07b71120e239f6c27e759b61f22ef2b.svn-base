using System.Web.Mvc;

namespace TXManagerWeb.Controllers
{
    public class CurrentAttribute : ActionFilterAttribute
    {
        public string _Current = string.Empty;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (string.IsNullOrEmpty(_Current))
            {
                filterContext.Controller.ViewData["_Current"] = _Current;
                return;
            }

            filterContext.Controller.ViewData["_Current"] = _Current;
        }
    }
}