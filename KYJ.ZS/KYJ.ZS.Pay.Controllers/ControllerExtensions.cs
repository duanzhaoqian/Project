using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons.Common;
using System.Web.Mvc;

namespace KYJ.ZS.Pay.Controllers
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
