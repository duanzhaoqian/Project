using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.Orders;
using KYJ.ZS.Commons;
using KYJ.ZS.BLL.Orders;
using KYJ.ZS.Models.RentalGoodses;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-26
    /// Desc：订单管理控制器
    /// </summary>
    public class OrderController:BaseController
    {
        #region 成员及变量
        private readonly OrderBll bll = new OrderBll();
        #endregion

        /// <summary>
        /// 订单管理
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

            int areaId = Auxiliary.ToInt32(Request.QueryString["hdn_area"], 1);
            // 订单区域
            OrderStateAreaType areaEnum = (OrderStateAreaType)areaId;
            // 订单状态
            int stateId = Auxiliary.ToInt32(Request.QueryString["hdn_state"], -1);
            OrderState? stateEnum = (OrderState?)stateId;
            // 商品名称
            string title = Request.QueryString["txt_title"];
            // 订单编号
            string number = Request.QueryString["txt_number"];
            // 买家昵称
            string userNikeName = Request.QueryString["txt_userNikeName"];

            IList<OrderManageEntity> list = bll.GetRentalOrdersList(areaEnum, stateEnum, title, number, userNikeName, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            
            ViewData["area"] = areaEnum;
            ViewData["state"] = stateEnum.Value;
            ViewData["goodsTitle"] = title;
            ViewData["number"] = number;
            ViewData["userNikeName"] = userNikeName;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;
            return View(list);
        }
    }
}
