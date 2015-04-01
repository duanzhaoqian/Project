using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Commons.Common;

namespace KYJ.ZS.User.Controllers
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
