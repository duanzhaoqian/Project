using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.BLL.LocalUsers;
using System.Web.Mvc;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.LocalUsers;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-04
    /// Desc：普通用户管理控制器
    /// </summary>
    public class LocalUserController : BaseController
    {
        #region 成员及变量
        private readonly LocalUserBll bll = new LocalUserBll();
        #endregion

        /// <summary>
        /// 普通用户管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Manage(int? pageIndex)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            string loginName = Request.QueryString["txt_loginName"]; //账号
            int status = Auxiliary.ToInt32(Request.QueryString["sel_status"], -1); //身份认证
            string realName = Request.QueryString["txt_realName"]; //姓名
            int state = Auxiliary.ToInt32(Request.QueryString["sel_state"], -1); //状态

            IList<LocalUserManageEntity> userList = bll.GetUserManageList(loginName, status, realName, state, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((userList == null || userList.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                userList = bll.GetUserManageList(loginName, status, realName, state, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            if (userList != null && userList.Count > 0)
            {
                foreach (var user in userList)
                {
                    user.Price = bll.GetUserAccountInfo(user.Id).Price;
                }
            }

            IDictionary<int, string> statuDic = new Dictionary<int, string>();
            statuDic.Add(1, "未认证");
            statuDic.Add(2, "认证中");
            statuDic.Add(3, "认证未通过");
            statuDic.Add(4, "认证通过");

            IDictionary<int, string> stateDic = new Dictionary<int, string>();
            stateDic.Add(0, "正常");
            stateDic.Add(1, "已冻结");

            ViewData["statuDic"] = statuDic;
            ViewData["stateDic"] = stateDic;

            ViewData["loginName"] = loginName;
            ViewData["status"] = status;
            ViewData["realName"] = realName;
            ViewData["state"] = state;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(userList);
        }

        /// <summary>
        /// 用户资料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            LocalUserEntity userEntity = bll.GetLocalUserByUserID(id);
            ViewData["price"] = bll.GetUserAccountInfo(id).Price;
            return View(userEntity);
        }

        /// <summary>
        /// 认证审核
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult AuditManage(int? pageIndex)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            string loginName = Request.QueryString["txt_loginName"]; //账号
            string realName = Request.QueryString["txt_realName"]; //姓名

            IList<LocalUserManageEntity> userList = bll.GetUserManageList(loginName, realName, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((userList == null || userList.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                userList = bll.GetUserManageList(loginName, realName, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }

            ViewData["loginName"] = loginName;
            ViewData["realName"] = realName;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(userList);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="loginName">用户账号</param>
        /// <param name="papersRemark">认证描述</param>
        /// <param name="isValidate">是否通过认证</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public string Audit(int id, string loginName, string papersRemark, bool isValidate)
        {
            if (!isValidate && string.IsNullOrEmpty(papersRemark))
                return "false||请填写驳回理由！";

            bool result = bll.Audited(id, papersRemark, isValidate);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.RENZHENGSHENHE;
                logEntity.ModuleName = "认证审核";
                logEntity.Remark = (isValidate ? "通过" : "驳回") + loginName + "的认证信息";
                new LogBll().CreateLog(logEntity);
            }

            return result.ToString();
        }

        /// <summary>
        /// 冻结操作
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名称</param>
        /// <param name="isFreeze">是否冻结</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public bool Freeze(int userId, string userName, bool isFreeze)
        {
            bool result = bll.Freeze(userId, isFreeze);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.GUANLIPUTONGYONGHU;
                logEntity.ModuleName = "管理普通用户";
                logEntity.Remark = (isFreeze ? "冻结用户" : "解除冻结用户") + userName;
                new LogBll().CreateLog(logEntity);
            }
            return result;
        }
    }
}
