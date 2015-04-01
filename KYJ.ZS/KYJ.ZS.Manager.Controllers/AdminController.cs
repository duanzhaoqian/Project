using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.Authority;
using KYJ.ZS.Commons;
using KYJ.ZS.BLL.Authority;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Admin;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-06
    /// Desc：职员管理控制器
    /// </summary>
    public class AdminController : BaseController
    {
        private readonly AdminsBll bll = new AdminsBll();

        /// <summary>
        /// 职员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage(int? pageIndex)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            AdminSearchEntity entity = new AdminSearchEntity();
            entity.AdminName = Request.QueryString["txt_loginname"]; //登录账号
            entity.RoleId = Auxiliary.ToInt32(Request.QueryString["sel_Role"], 0); //权限角色
            entity.RealName = Request.QueryString["txt_realname"]; //职员姓名
            entity.Mobile = Request.QueryString["txt_mobile"]; //联系电话

            IList<AdminsEntity> list = bll.GetPageData(entity, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((list == null || list.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                list = bll.GetPageData(entity, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }

            ViewData["searchEntity"] = entity;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(list);
        }

        /// <summary>
        /// 添加职员
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateAdmin()
        {
            return View();
        }

        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Create()
        {
            AdminCreateEntity admin = new AdminCreateEntity();
            string id = Request.Form["hdn_id"];
            admin.AdminId = string.IsNullOrEmpty(id) ? null : (int?)Auxiliary.ToInt32(id);
            admin.AdminName = Request.Form["txt_loginName"];
            admin.PassWord = Request.Form["txt_pwd"];
            admin.RealName = Request.Form["txt_realName"];
            admin.Mobile = Request.Form["txt_phone"];
            admin.RoleId = Auxiliary.ToInt32(Request.Form["sel_role"]);
            admin.OperId = _ServiceContext.CurrentUser.UserID;
            admin.OperName = _ServiceContext.CurrentUser.RealName;

            if (!TryValidateModel(admin))
                return false;

            bool result = bll.AddAdmin(admin);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity();
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.GUANLIZHIYUANQUANXIAN;
                logEntity.ModuleName = "管理职员权限";
                logEntity.Remark = (string.IsNullOrEmpty(id) ? "添加" : "修改") + "职员" + admin.RealName;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 账户名称校验
        /// </summary>
        /// <param name="oldName"></param>
        /// <returns></returns>
        [HttpGet]
        public bool ValidateLoginName(string name, string oldName)
        {
            if (!string.IsNullOrEmpty(oldName) && name.Trim() == oldName.Trim())
                return true;

            return bll.ValidateLoginName(name);
        }

        /// <summary>
        /// 删除职员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public bool Delete(int id, string name)
        {
            bool result = bll.Delete(id);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity();
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.GUANLIZHIYUANQUANXIAN;
                logEntity.ModuleName = "管理职员权限";
                logEntity.Remark = "删除职员" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }
    }
}
