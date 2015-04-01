using System;
using System.Web.Mvc;
using TXManagerWeb.Common;
using TXManagerWeb.Controllers;

namespace TXManager.Controllers
{
    public static class ControllerExtensions
    {
        public static ServiceContext GetServiceContext(this ControllerBase controllerBase)
        {
            var controller = controllerBase as BaseController;
            if (controller == null)
                throw new Exception("要获取服务上下文属性,Controller必须继承BaseController基类");

            return controller._ServiceContext;
        }
    }
}