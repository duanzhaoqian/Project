using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.Authority;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-27
    /// 描述：用户退出
    /// </summary>
   public class ManagerManageController:BaseController
    {
       LoginFilterBll bll = new LoginFilterBll();
       [HttpGet]
       public ActionResult LoginOut()
       {
           // 日志记录
           LogCreateEntity logEntity = new LogCreateEntity() { };
           logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
           logEntity.Name = _ServiceContext.CurrentUser.LoginName;
           logEntity.RealName = _ServiceContext.CurrentUser.RealName;
           logEntity.ModuleId = -1;
           logEntity.ModuleName = "登录退出";
           logEntity.Remark = "退出平台";
           new LogBll().CreateLog(logEntity);

           bll.LoginOut(_ServiceContext.CurrentUser._Guid);
           RedirectResult result = new RedirectResult(Url.ManagerSiteUrl().Login);
           return result;
       }

    }
}
