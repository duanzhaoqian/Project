using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Commons;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.BLL.Module;
using KYJ.ZS.BLL.Role;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-03
    /// Desc：日志查阅控制器
    /// </summary>
    public class LogController : BaseController
    {
        private readonly LogBll bll = new LogBll();

        /// <summary>
        /// 基础数据-日志查阅
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Manage(int? pageIndex)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 20;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            LogSearchEntity entity = new LogSearchEntity();
            entity.LoginName = Request.QueryString["txt_loginname"]; //登录账号
            entity.RoleId = Auxiliary.ToInt32(Request.QueryString["sel_Role"], 0); //权限角色
            entity.StaffName = Request.QueryString["txt_realname"]; //职员姓名
            int areaId = Auxiliary.ToInt32(Request.QueryString["sel_Module"], 0); //功能区
            entity.ModuleIds = new ModuleBll().GetModulesByArea(areaId); //模块

            IList<LogIndexEntity> list = bll.GetLogList(entity, pageIndex.Value, pageSize, out totalRecord, out totalPage);

            ViewData["searchEntity"] = entity;
            ViewData["areaId"] = areaId;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(list);
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public JsonResult GetModules()
        {
            try
            {
                ModuleBll _moduleBll = new ModuleBll();
                var modules = _moduleBll.GetAreaList().ToArray();
                return Json(new { success = true, items = modules }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取权限角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public JsonResult GetRoles()
        {
            try
            {
                RoleBll _roleBll = new RoleBll();
                var roles = _roleBll.GetSelectRoles().ToArray();
                return Json(new { success = true, items = roles }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
