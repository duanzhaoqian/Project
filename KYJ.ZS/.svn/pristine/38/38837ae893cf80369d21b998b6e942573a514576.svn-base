using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.WithdrawCashs;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.WithdrawCashs;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-28
    /// Desc：商户提现管理控制器
    /// </summary>
    public class WithdrawCashController : BaseController
    {
        #region 成员及变量
        private readonly WithdrawCashBll bll = new WithdrawCashBll();
        #endregion

        /// <summary>
        /// 管理商户提现
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Manage(int? pageIndex, int? areaId)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord = 0; //记录总条数
            int totalPage = 0; //总页数
            // 订单区域
            WithdrawCashAreaEnum areaEnum = areaId == null ? WithdrawCashAreaEnum.All : (WithdrawCashAreaEnum)areaId;

            IList<WithdrawCash> list = bll.GetWithdrawCashList(null, areaEnum, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((list == null || list.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                list = bll.GetWithdrawCashList(null, areaEnum, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            ViewData["areaEnum"] = areaEnum;

            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;
            return View(list);
        }

        /// <summary>
        /// 提现详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            WithdrawCash withdrawCash = bll.GetWithdrawCash(id);
            return View(withdrawCash);
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <param name="reason">驳回理由</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public string Reject(int id, string reason, string number)
        {
            if (string.IsNullOrEmpty(reason))
                return "false||请填写驳回理由！";
            string result = bll.Reject(id, reason).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.GUANLISHANGHUTIXIAN;
                logEntity.ModuleName = "管理商户提现";
                logEntity.Remark = "驳回提现" + number;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 确认提现
        /// </summary>
        /// <param name="id">提现ID</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public bool ConfirmWithdrawal(int id, string number)
        {
            bool result = bll.ConfirmWithdrawal(id);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.GUANLISHANGHUTIXIAN;
                logEntity.ModuleName = "管理商户提现";
                logEntity.Remark = "确认提现" + number;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }
    }
}
