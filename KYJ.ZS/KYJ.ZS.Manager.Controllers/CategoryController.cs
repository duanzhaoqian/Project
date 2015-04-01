using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.BLL.Categories;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.Commons;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-03
    /// Desc：商品分类管理控制器
    /// </summary>
    public class CategoryController : BaseController
    {
        #region 成员及变量
        private readonly CategoryBll bll = new CategoryBll();
        #endregion

        /// <summary>
        /// 基础数据-商品分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage()
        {
            return View();
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="pId">父类目ID</param>
        /// <param name="categoryName">类目名称</param>
        /// <param name="level">节点深度</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string Create(int pId, string categoryName, int level)
        {
            if(string.IsNullOrEmpty(categoryName))
                return "false||请输入分类名称！";
            if (!bll.CheckName(categoryName.Trim()))
                return "false||已存在的分类名称！";

            string result = bll.Create(pId, categoryName.Trim(), level).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHANGPINFENLEIGUANLI;
                logEntity.ModuleName = "商品分类管理";
                logEntity.Remark = "添加分类" + categoryName;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="id">类目ID</param>
        /// <param name="name">类目名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public string Edit(int id, string name, string oldName)
        {
            if (name.Trim() == bll.GetName(id))
                return "true";
            if (!bll.CheckName(name.Trim()))
                return "false||已存在的分类名称！";

            string result = bll.EditName(id, name).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHANGPINFENLEIGUANLI;
                logEntity.ModuleName = "商品分类管理";
                logEntity.Remark = "将分类" + name + "改为" + oldName;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 基础数据-删除分类
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public string Delete(int categoryCode, string name)
        {
            if (!bll.DeleteValidate(categoryCode))
                return "false||请先删除下级分类！";
            if (!bll.DeleteValidateGoods(categoryCode))
                return "false||此分类下包含租售的商品，删除失败！";

            string result = bll.DeleteCategory(categoryCode).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHANGPINFENLEIGUANLI;
                logEntity.ModuleName = "商品分类管理";
                logEntity.Remark = "删除分类" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }
    }
}
