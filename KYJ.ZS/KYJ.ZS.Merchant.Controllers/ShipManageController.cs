using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.Orders;
using KYJ.ZS.Models.Orders;
using KYJ.ZS.Commons;

namespace KYJ.ZS.Merchant.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-29
    /// Desc：发货管理控制器
    /// </summary>
    public class ShipManageController : BaseController
    {
        #region 成员及变量
        private OrderBll bll = new OrderBll();
        #endregion

        #region 发货管理

        /// <summary>
        /// 发货管理
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="id">订单编号</param>
        /// <returns></returns>
        public ActionResult Manage(int? pageIndex, int? id)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数
            // 是否发货
            bool isSend = string.IsNullOrEmpty(Request.QueryString["hdn_isSend"]) ? false : Auxiliary.ToBoolen(Request.QueryString["hdn_isSend"]);
            string recipientName = Request.QueryString["txt_Name"]; //收件人名称
            string orderNumber = Request.QueryString["txt_Number"]; //订单编号
            string nickName = Request.QueryString["txt_nickName"]; //买家昵称

            // 创建时间
            DateTime? startDate = string.IsNullOrEmpty(Request.QueryString["txt_startDate"]) ? (DateTime?)null : Convert.ToDateTime(Request.QueryString["txt_startDate"]);
            DateTime? endDate = string.IsNullOrEmpty(Request.QueryString["txt_endDate"]) ? (DateTime?)null : Convert.ToDateTime(Request.QueryString["txt_endDate"] + " 23:59:59");

            // 构建订单搜索Entity
            OrderSearchEntity searchEntity = GetSearchEntity(isSend, recipientName, orderNumber, nickName, startDate, endDate);
            // 查询分页数据
            IList<OrderEntity> orderList = bll.GetOrdersByState(id, searchEntity, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((orderList == null || orderList.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                orderList = bll.GetOrdersByState(id, searchEntity, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            // 查询条件
            ViewData["SearchEntity"] = searchEntity;

            // 分页
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(orderList);
        }

        #endregion

        #region 修改发货信息

        /// <summary>
        /// 修改发货信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Edit()
        {
            string orderId = Request.Form["orderId"]; //订单ID
            string company = Request.Form["company"]; //物流、快递公司
            string expressNum = Request.Form["expressNum"]; //快递单号
            //string type = Request.Form["type"]; //标识数据来源表（1、近三个月的订单；2、三个月以前的订单）

            if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(company) || string.IsNullOrEmpty(expressNum))
                return false;
            int merchantId = _ServiceContext.CurrentUser.UserID;
            return bll.UpdateSendInfo(Auxiliary.ToInt32(orderId), company, expressNum, merchantId);
        }

        #endregion

        /// <summary>
        /// 构建订单搜索Entity
        /// </summary>
        /// <param name="isSend">是否发货</param>
        /// <param name="recipientName">收件人名称</param>
        /// <param name="orderNumber">订单编号</param>
        /// <param name="nickName">买家昵称</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        private OrderSearchEntity GetSearchEntity(bool isSend, string recipientName, string orderNumber, string nickName, DateTime? startDate, DateTime? endDate)
        {
            OrderSearchEntity entity = new OrderSearchEntity();
            entity.OperId = _ServiceContext.CurrentUser.UserID; //商品所属用户/商户ID
            entity.IsSend = isSend;
            entity.RecipientName = recipientName;
            entity.OrderNumber = orderNumber;
            entity.NickName = nickName;
            entity.startDate = startDate;
            entity.endDate = endDate;
            return entity;
        }
    }
}
