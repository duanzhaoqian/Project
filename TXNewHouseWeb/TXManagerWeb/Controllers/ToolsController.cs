using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack;
using TXCommons;
using TXManagerWeb.Common;

namespace TXManagerWeb.Controllers
{
    /// <summary>
    /// 一些小功能
    /// </summary>
    public class ToolsController : BaseController
    {
        //
        // GET: /Tools/
        [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Developer.DevelCode)]
        public ActionResult TestGetCode()
        {
            if (Request.IsAjaxRequest())
            {
                string mobile = Request["mobile"];
                if (!string.IsNullOrWhiteSpace(mobile))
                {
                    string code = Redis.GetValue<string>(KeyList.GetRedisMobileValidateCodeKey(mobile),
                                                         FunctionTypeEnum.NewHouseVerificationCode, 0);
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        return Json(new { has = true, code = code });
                    }
                    else
                    {
                        return Json(new { has = false });
                    }
                }
            }
            return View();
        }
    }
}
