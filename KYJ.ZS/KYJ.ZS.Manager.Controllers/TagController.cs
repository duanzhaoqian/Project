using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.Tags;
using KYJ.ZS.Models.Tags;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.Commons;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-27
    /// Desc：信息标签管理控制器
    /// </summary>
    public class TagController : BaseController
    {
        #region 成员及变量
        private readonly TagBll bll = new TagBll();
        #endregion

        /// <summary>
        /// 标签管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Manage(int? pageIndex)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            IList<TagEntity> list = bll.GetTagsManageList(pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((list == null || list.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                list = bll.GetTagsManageList(pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(list);
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public string CreateTag(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "false||请填写标签名称！";
            if (!bll.isValidateName(name))
                return "false||内容重复，无法添加！";
            string result = bll.Create(name.Trim()).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.XINXIBIAOQIANGUANLI;
                logEntity.ModuleName = "信息标签管理";
                logEntity.Remark = "添加标签" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public string DeleteTag(int tagId, string name)
        {
            if (!bll.isValidateDelete(tagId))
                return "false||此标签下有用户发布的闲置商品信息，无法删除！";
            string result = bll.Delete(tagId).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.XINXIBIAOQIANGUANLI;
                logEntity.ModuleName = "信息标签管理";
                logEntity.Remark = "删除标签" + name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 删除校验
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public bool DeleteValidate(int tagId)
        {
            return bll.isValidateDelete(tagId);
        }
    }
}
