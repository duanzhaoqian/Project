using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TXBll.Dev;
using TXBll.FinancialData;
using TXManagerWeb.Common;
using TXModel.AdminPVM;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    public class NhWithdrawCashController : BaseController
    {
        /// <summary>
        /// 新房提现管理 待审批
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PVS_NH_WithdrawCash search = null)
        {
            if (null == search)
            {
                search = new PVS_NH_WithdrawCash();
            }

            search.UserTypes = GetUserTypes();
            search.UserType = (search.UserType == 0 ? 3 : search.UserType);

            if (3 != search.UserType)
            {
                var urlstr = "";
                switch (search.Status)
                {                    
                    case 1:
                        urlstr = "WithdrawCash/WaitPayMoney.html";
                        break;
                    case 2:
                        urlstr = "WithdrawCash/ApplyNoPass.html";
                        break;
                    case 3:
                        urlstr = "WithdrawCash/PayMoneySuccess.html";
                        break;
                    case 4:
                        urlstr = "WithdrawCash/PayMoneyFail.html";
                        break;
                    default :
                        urlstr = "WithdrawCash/Index.html";
                        break;
                }
                
                var urls = string.Format("{0}{7}?ram={1}&begintime={2}&endtime={3}&usertype={4}&keyword={5}&status={6}",
                    Auxiliary.Instance.ManagerUrl,
                    new Random().Next(1, 999999999),
                    search.BeginTime,
                    search.EndTime,
                    search.UserType,
                    search.KeyWord,
                    search.Status,
                    urlstr
                    );
                return Redirect(urls);
            }

            switch (search.Status)
            {
                case 0:
                    ViewData["_AdminPageInfo"] = Auxiliary.Instance.Model_GetModelsInfo(3, (int) AdminNavi.Financial.PickCash.WaitingApproval);
                    break;
                case 1:
                    ViewData["_AdminPageInfo"] = Auxiliary.Instance.Model_GetModelsInfo(3, (int) AdminNavi.Financial.PickCash.PassApprovalWaitPay);
                    break;
                case 2:
                    ViewData["_AdminPageInfo"] = Auxiliary.Instance.Model_GetModelsInfo(3, (int) AdminNavi.Financial.PickCash.NotPass);
                    break;
                case 3:
                    ViewData["_AdminPageInfo"] = Auxiliary.Instance.Model_GetModelsInfo(3, (int) AdminNavi.Financial.PickCash.SuccessPay);
                    break;
                case 4:
                    ViewData["_AdminPageInfo"] = Auxiliary.Instance.Model_GetModelsInfo(3, (int) AdminNavi.Financial.PickCash.FailedPay);
                    break;
            }

            return View(search);
        }

        /// <summary>
        /// 根据查询条件查询提现列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_WithdrawCash search, int pageindex, int pagesize)
        {
            pageindex = pageindex + 1;
            List<PVM_NH_WithdrawCash> list = null;
            switch (search.UserType)
            {
                case 3:
                    list = new WithdrawCashBll().GetPageList_BySearch_WithdrawCash(search, pageindex, pagesize);
                    break;
            }
            return PartialView("PageTables/Financial/_WithdrawCash", list);
        }

        /// <summary>
        /// 根据查询条件查询提现总记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult SearchCount(PVS_NH_WithdrawCash search)
        {
            int count = new WithdrawCashBll().GetTotalCount_BySearch_WithdrawCash(search);

            return Json(new {result = count});
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchResult_Export(PVS_NH_WithdrawCash search)
        {
            var list = new List<PVM_NH_WithdrawCash>();
            switch (search.UserType)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    list = new WithdrawCashBll().GetWithdrawCash_Export(search);
                    break;
            }

            const string fileName = "提现记录";

            #region 信息

            string[] titlestr =
            {
                "提现申请时间",
                "登录名",
                "手机号码",
                "本次提现金额",
                "收款账户信息"
            };
            var listResult = list.ConvertAll(it => new OutputExcel
            {
                CellText1 = Convert.ToString(it.CreateTime),
                CellText2 = it.LoginName.Trim(),
                CellText3 = it.Mobile.Trim(),
                CellText4 = string.Format("{0:N2}", it.Price),
                CellText5 = (it.ReceiveType == 0 ? ("[支付宝] 真实姓名：" + it.RealName + " 支付宝账户：" + it.ALiPayAccount) : ("[银行卡] 开户城市：" + it.CityName + "开户银行：" + it.BankName + "开户支行：" + it.PubBankName + "真实姓名：" + it.RealName + "银行账户：" + it.BankAccount))
            });

            #endregion

            return new ExcelResult<OutputExcel>(listResult, fileName, titlestr);
        }

        /// <summary>
        /// 获取用户类型
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetUserTypes()
        {
            var userTypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "个人", Value = "1"},
                new SelectListItem {Text = "经纪人", Value = "2"},
                new SelectListItem {Text = "开发商", Value = "3"}
            };

            return userTypes;
        }

        /// <summary>
        /// 修改提现状态
        /// </summary>
        /// <param name="id">提现记录编号</param>
        /// <param name="status">提现状态</param>
        /// <param name="adminId">操作员Id</param>
        /// <param name="adminName">操作员姓名</param>
        /// <param name="price">提现金额</param>
        /// <param name="userId">用户Id</param>
        /// <param name="currentStatus"></param>
        /// <returns></returns>
        public ActionResult HandleWithdrawCash(int? id, int? status, int? adminId, String adminName, decimal price, int userId, int currentStatus, int userType)
        {
            if (id.HasValue && status.HasValue)
            {
                try
                {

                    int i = 0;

                    var cash = (new WithdrawCashBll().GetEntity_ById(id.Value)) as Developer_WithdrawCash;
                    i = new WithdrawCashBll().UpdateWithdrawCashStatus(id.Value, status.Value, _ServiceContext.CurrentUser.Id, _ServiceContext.CurrentUser.LoginName, userId, price, currentStatus);

                    //switch (userType)
                    //{
                    //        //case 1:
                    //        //    i = _localUserBll.UpdateWithdrawCashStatus(id.Value, status.Value, adminId.Value, adminName, userId, price, currentStatus);
                    //        //    var cashsuccess = _localUserBll.SelU_WithdrawCash((int)id); //根据提款Id获取提款实体信息
                    //        //    AddUserMessage((int)status, userType, userId, cashsuccess.RealName, cashsuccess.Price, cashsuccess.UpdateTime);
                    //        //    break;
                    //        //case 2:
                    //        //    i = _agentBll.UpdateWithdrawCashStatus(id.Value, status.Value, adminId.Value, adminName, userId, price, currentStatus);
                    //        //    var cashfail = _agentBll.SelA_WithdrawCash((int)id); //根据提款Id获取提款实体信息
                    //        //    AddUserMessage((int)status, userType, userId, cashfail.RealName, cashfail.Price, cashfail.UpdateTime); //1  2 
                    //        //    break;
                    //    case 3:
                    //        i = new WithdrawCashBll().UpdateWithdrawCashStatus(id.Value, status.Value, adminId.Value, adminName, userId, price, currentStatus);
                    //        break;
                    //}

                    if (null != cash && 0 < cash.DeveloperId)
                    {
                        var developer = (new DevelopersBll().GetEntity_ById(cash.DeveloperId)) as Developer;
                        AddUserMessage(status.Value, userType, developer.Id, developer.LoginName, cash.Price, cash.CreateTime);
                    }


                    return Json(new {result = i > 0, message = ""}, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new {result = false, message = "操作数据库失败：" + ex.Message}, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new {result = false, message = "该提现记录不存在或者你设置的状态出错了"}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 发送站内信
        /// </summary>
        /// <param name="status"></param>
        /// <param name="userType"></param>
        /// <param name="developerId"></param>
        /// <param name="developerName"></param>
        /// <param name="money"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool AddUserMessage(int status, int userType, int developerId, string developerName, decimal money, DateTime time)
        {
            if (status == 1 || status == 2)
            {

                var message = new DeveloperMessage
                {
                    SendUserId = 0, 
                    ReceiveUserId = developerId
                };
                if (status == 1)
                {
                    message.Title = "提现审核通过";
                    message.Content = string.Format("尊敬的{0}，您在{1}提交的{2}元的提现申请已审核通过，请注意查收，详情致电快有家客户服务热线：{3}", developerName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", time), string.Format("{0:0.00}", money), Auxiliary.Instance.NhServiceHotLine1); //内容
                }
                else
                {
                    message.Title = "提现审核未通过"; //标题
                    message.Content = string.Format("尊敬的{0}，您在{1}提交的{2}元的提现申请已被拒绝，提现金额已返还到您的快有家账户中。详情致电快有家客户服务热线：{3}", developerName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", time), string.Format("{0:0.00}", money), Auxiliary.Instance.NhServiceHotLine1); //内容
                }

                return new DeveloperMessageBll().Add(message) > 0;
            }
            return true;
        }
    }
}