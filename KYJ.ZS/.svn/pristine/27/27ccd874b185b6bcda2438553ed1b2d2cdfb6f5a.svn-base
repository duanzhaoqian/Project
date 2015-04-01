using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.BLL.Attributes;
using KYJ.ZS.BLL.Categories;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.Commons;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-04
    /// Desc：属性规格管理控制器
    /// </summary>
    public class AttrCategoryController : BaseController
    {
        /// <summary>
        /// 基础数据-属性规格管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage()
        {
            return View();
        }

        #region 属性相关操作

        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public JsonResult GetAttrs(int categoryId)
        {
            try
            {
                AttributeBll _attrBll = new AttributeBll();
                var attrs = _attrBll.GetSelectAttrs(categoryId).ToArray();
                return Json(new { success = true, items = attrs }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string CreateAttr(int categoryId, string name)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";

            string result = new AttributeBll().Create(categoryId, name);
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "添加属性" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string EditAttr(int id, string name, string oldName)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";

            if (name.Trim() == oldName.Trim())
                return "true";

            string result = new AttributeBll().Edit(id, name).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "将属性" + oldName + "改为" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="id">属性ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool DeleteAttr(int categoryId, int id, string name)
        {
            bool result = new AttributeBll().DeleteAttr(categoryId, id);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "删除属性" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        #endregion

        #region 属性值相关操作

        /// <summary>
        /// 获取属性值列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="attrId">属性ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public JsonResult GetAttrValues(int categoryId, int attrId)
        {
            try
            {
                AttributeValueBll _attrBll = new AttributeValueBll();
                var attrs = _attrBll.GetSelectAttrValues(categoryId, attrId).ToArray();
                return Json(new { success = true, items = attrs }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string CreateAttrValue(int categoryId, int attrId, string name)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";

            string result = new AttributeValueBll().Create(categoryId, attrId, name);
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "添加属性值" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string EditAttrValue(int id, string name, string oldName)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";
            if (name.Trim() == oldName.Trim())
                return "true";

            string result = new AttributeValueBll().Edit(id, name).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "将属性值" + oldName + "改为" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="id">属性值ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool DeleteAttrValue(int id, string name)
        {
            bool result = new AttributeValueBll().Delete(id);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "删除属性值" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        #endregion

        #region 分类颜色相关操作

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool ColorIsShow(int categoryId)
        {
            return new CategoryColorBll().IsShow(categoryId);
        }

        /// <summary>
        /// 更改显示状态
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool ChangeColorShow(int categoryId, bool isShow)
        {
            bool result = new CategoryColorBll().EditShow(categoryId, isShow);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "将颜色改为" + (isShow ? "显示" : "不显示");
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 获取分类颜色列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public JsonResult GetColors(int categoryId)
        {
            try
            {
                CategoryColorBll _colorBll = new CategoryColorBll();
                var colors = _colorBll.GetSelectColors(categoryId).ToArray();
                return Json(new { success = true, items = colors }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加分类颜色
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string CreateColor(int categoryId, string name, bool colorIsShow)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";

            string result = new CategoryColorBll().Create(categoryId, name, colorIsShow);
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "添加颜色" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 修改分类颜色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string EditColor(int id, string name, string oldName)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";
            if (name.Trim() == oldName.Trim())
                return "true";

            string result = new CategoryColorBll().Edit(id, name).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "将颜色" + oldName + "改为" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 删除分类颜色
        /// </summary>
        /// <param name="id">分类颜色ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool DeleteColor(int id, string name)
        {
            bool result = new CategoryColorBll().DeleteColor(id);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "删除颜色" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        #endregion

        #region 分类规格相关操作

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool NormIsShow(int categoryId)
        {
            return new CategoryNormBll().IsShow(categoryId);
        }

        /// <summary>
        /// 更改显示状态
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool ChangeNormShow(int categoryId, bool isShow)
        {
            bool result = new CategoryNormBll().EditShow(categoryId, isShow);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "将尺码改为" + (isShow ? "显示" : "不显示");
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 获取分类规格列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public JsonResult GetNorms(int categoryId)
        {
            try
            {
                CategoryNormBll _normBll = new CategoryNormBll();
                var norms = _normBll.GetSelectNorms(categoryId).ToArray();
                return Json(new { success = true, items = norms }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 添加分类规格
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string CreateNorm(int categoryId, string name, bool normIsShow)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";

            string result = new CategoryNormBll().Create(categoryId, name, normIsShow);
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "添加尺码" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 修改分类规格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public string EditNorm(int id, string name, string oldName)
        {
            if (string.IsNullOrEmpty(name))
                return "fals||请填写名称！";
            if (name.Trim() == oldName.Trim())
                return "true";

            string result = new CategoryNormBll().Edit(id, name).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "将尺码" + oldName + "改为" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 删除分类规格
        /// </summary>
        /// <param name="id">分类规格ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public bool DeleteNorm(int id, string name)
        {
            bool result = new CategoryNormBll().DeleteNorm(id);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.SHUXINGGUIGEGUANLI;
                logEntity.ModuleName = "属性规格管理";
                logEntity.Remark = "删除尺码" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        #endregion
    }
}
