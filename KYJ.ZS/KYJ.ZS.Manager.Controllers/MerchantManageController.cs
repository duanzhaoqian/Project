using System.Collections.Generic;
using System.Web.Mvc;
using KYJ.ZS.BLL.Merchants;
using KYJ.ZS.BLL.Orders;
using KYJ.ZS.BLL.WithdrawCashs;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Merchants;
using KYJ.ZS.Models.Orders;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-23
    /// Desc：商户管理控制器
    /// </summary>
    public class MerchantManageController : BaseController
    {
        #region 成员及变量
        private readonly MerchantBll bll = new MerchantBll();
        #endregion

        /// <summary>
        /// 商户管理-商户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Manage(int? pageIndex)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            string name = Request.QueryString["txt_name"]; //企业名称
            string loginName = Request.QueryString["txt_loginname"]; //企业账号

            IList<MerchantIndexEntity> merchantList = bll.GetMerchantList(name, loginName, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((merchantList == null || merchantList.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                merchantList = bll.GetMerchantList(name, loginName, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            ViewData["name"] = name;
            ViewData["loginName"] = loginName;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(merchantList);
        }

        /// <summary>
        /// 企业基本资料
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BasicInfo(int merchantId)
        {
            ViewData["merchantId"] = merchantId;

            return View();
        }

        /// <summary>
        /// 租售记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderRecord(int? pageIndex, int merchantId)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            // 商品名称
            string title = Request.QueryString["txt_title"];
            // 订单编号
            string number = Request.QueryString["txt_number"];
            // 买家昵称
            string userNikeName = Request.QueryString["txt_userNikeName"];

            IList<OrderManageEntity> list = new OrderBll().GetOrderRecord(merchantId, title, number, userNikeName, pageIndex.Value, pageSize, out totalRecord, out totalPage);

            ViewData["merchantId"] = merchantId;
            ViewData["goodsTitle"] = title;
            ViewData["number"] = number;
            ViewData["userNikeName"] = userNikeName;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(list);
        }

        /// <summary>
        /// 信誉记录
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreditRecord(int merchantId)
        {

            ViewData["merchantId"] = merchantId;

            return View();
        }

        /// <summary>
        /// 保障服务
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public ActionResult SecurityServices(int merchantId)
        {
            ViewData["merchantId"] = merchantId;

            return View();
        }

        /// <summary>
        /// 提现日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WithdrawalsLog(int? pageIndex, int merchantId)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            IList<WithdrawCash> list = new WithdrawCashBll().GetWithdrawCashList(merchantId, Models.WithdrawCashs.WithdrawCashAreaEnum.All, pageIndex.Value, pageSize, out totalRecord, out totalPage);

            ViewData["merchantId"] = merchantId;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(list);
        }

        /// <summary>
        /// 添加商户
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加商户Post请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool CreateMerchant()
        {
            MerchantCreateEntity entity = new MerchantCreateEntity();
            entity.LoginName = Request.Form["txt_loginname"];
            entity.Pwd = Request.Form["txt_pwd"];
            entity.Name = Request.Form["txt_name"];
            entity.Introduction = Request.Form["txt_introduction"];
            entity.Guid = Request.Form["hdn_guid"];
            entity.OperateCategory = Request.Form["txt_operatecategory"];
            entity.AdminId = _ServiceContext.CurrentUser.UserID;
            entity.AdminName = _ServiceContext.CurrentUser.RealName;
            if (!TryValidateModel(entity))
                return false;

            bool result = bll.Create(entity);
            if (result)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = (int)ManagerNavigation.TIANJIASHANGHU;
                logEntity.ModuleName = "添加商户";
                logEntity.Remark = "添加商户" + entity.Name;
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }

        /// <summary>
        /// 账户验证
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        [HttpGet]
        public bool ValidateLoginName(string loginName)
        {
            return bll.ValidateLoginName(loginName);
        }
    }
}
