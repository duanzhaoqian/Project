using System.Web.Mvc;
using TXManagerWeb.Common;

namespace TXManagerWeb.Controllers.ActionFilters
{
    public class AdminPageInfoAttribute : ActionFilterAttribute
    {
        public AdminPageInfoModel _AdminPageInfo { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cardId">选项卡编号</param>
        /// <param name="modelId">模块编号</param>
        public AdminPageInfoAttribute(int cardId, int modelId)
        {
            _AdminPageInfo = Auxiliary.Instance.Model_GetModelsInfo(cardId, modelId);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (null == _AdminPageInfo)
            {
                filterContext.Controller.ViewData["_AdminPageInfo"] = new AdminPageInfoModel();
                return;
            }

            filterContext.Controller.ViewData["_AdminPageInfo"] = _AdminPageInfo;
        }
    }
}