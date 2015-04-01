using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.Authority;
using KYJ.ZS.Models.Authority;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.RolePermission;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.Manager.Controllers.ActionFilters;

namespace KYJ.ZS.Manager.Controllers
{
    public class RoleManagerController : BaseController
    {
        RoleModulesBll bll = new RoleModulesBll();
        private AdminsBll adminbll = new AdminsBll();

        /// <summary>
        /// 管理角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage()
        {
            IList<RoleShowEntity> list = bll.GetRoleInfo();
            return View(list);
        }

        /// <summary>
        /// 作者：孟国栋
        /// 时间：2014-06-03
        /// 描述：添加角色管理信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleAdd(int? roleId, string type)
        {
            List<Modules> list = bll.GetAllMouleData();
            List<RoleManagement> permissionList = null;
            string roleName = "";
            if (roleId.HasValue)
            {
                permissionList = bll.GetPermissionByRoleId(roleId.Value);
                roleName = bll.GetRoleInfoByRoleId(roleId.Value).RoleName;
            }
            ViewData["roleId"] = roleId;
            ViewData["roleName"] = roleName;
            ViewData["type"] = type;
            ViewData["permissionList"] = permissionList;
            return View(list);
        }

        /// <summary>
        /// 添加、修改角色权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleUpdate()
        {
            RolePermissionEntity entity = new RolePermissionEntity();
            entity.RoleName = Request.Form["txt_roleName"];
            string roleId = Request.Form["hdn_roleId"];
            entity.Id = string.IsNullOrEmpty(roleId) ? null : (int?)Auxiliary.ToInt32(roleId);
            entity.OperId = _ServiceContext.CurrentUser.UserID;
            entity.OperName = _ServiceContext.CurrentUser.RealName;
            string permissions = Request.Form["chk_per"];
            if (!string.IsNullOrEmpty(permissions))
            {
                IDictionary<int, string> dic = new Dictionary<int, string>();
                foreach (var item in permissions.Split(','))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int moduleId = Auxiliary.ToInt32(item.Split('|')[0]);
                        string permission = item.Split('|')[1];
                        if (dic.ContainsKey(moduleId))
                        {
                            dic[moduleId] += permission + ",";
                        }
                        else
                        {
                            dic.Add(moduleId, permission + ",");
                        }
                    }
                }
                if (dic.Count > 0)
                {
                    entity.PermissionList = new List<PermissionEntity>();
                    foreach (var item in dic)
                    {
                        entity.PermissionList.Add(new PermissionEntity()
                        {
                            ModuleId = item.Key,
                            Permission = item.Value.Substring(0, item.Value.Length - 1)
                        });
                    }
                }
            }
            bool result = bll.CreateOrUpdate(entity);
            if (!result)
                return Content("添加失败！");
            else
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.GUANLIQUANXIANJUESE;
                logEntity.ModuleName = "管理权限角色";
                logEntity.Remark = string.IsNullOrEmpty(roleId) ? "添加" : "修改" + "角色" + entity.RoleName;
                new LogBll().CreateLog(logEntity);

                return RedirectToAction("manage", "rolemanager");
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public string Delete(int roleId, string roleName)
        {
            if (!bll.DeleteValidate(roleId))
                return "false||此权限角色已在使用，无法删除！";
            bool result = bll.Delete(roleId);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.GUANLIQUANXIANJUESE;
                logEntity.ModuleName = "管理权限角色";
                logEntity.Remark = "删除角色" + roleName;
                new LogBll().CreateLog(logEntity);
            }
            return result.ToString();
        }

        /// <summary>
        /// 删除校验
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public bool DeleteValidate(int roleId)
        {
            return bll.DeleteValidate(roleId);
        }
    }
}
