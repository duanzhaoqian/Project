using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.OrderGoodses;
using KYJ.ZS.Models.OrderGoodses;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Commons;
using KYJ.ZS.User.Controllers.ActionFilters;
using KYJ.ZS.Commons.PictureModular;
namespace KYJ.ZS.User.Controllers
{
    public class OrderController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-24
        /// 用户后台租用订单列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RentOrdersList(int? pageIndex)
        {
            int index = pageIndex ?? 1;
            int totalRecord = 0;
            int totalPage = 0;
            int pageSize = 5;//每页显示的个数
            OrderGoodsEntity where = new OrderGoodsEntity();
            where.UserId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            where.OrderGoodsTitle = Request["txtTitle"];
            ViewBag.txtTitle = Request["txtTitle"];

            where.MerchantName = Request["txtMerchantName"];
            ViewBag.txtMerchantName = Request["txtMerchantName"];

            where.OrderNumber = Request["txtNumber"];
            ViewBag.txtNumber = Request["txtNumber"];

            string timestart = Request["txtTimeStart"];
            ViewBag.txtTimeStart = Request["txtTimeStart"];

            string timeend = Request["txtTimeEnd"];
            ViewBag.txtTimeEnd = Request["txtTimeEnd"];

            if (string.IsNullOrEmpty(timestart))
            {
                where.OrderCreateTime = null;
            }
            else
            {
                where.OrderCreateTime = Convert.ToDateTime(timestart);
            }
            if (string.IsNullOrEmpty(timeend))
            {
                where.OrderUpdateTime = null;
            }
            else
            {
                where.OrderUpdateTime = Convert.ToDateTime(timeend);
            }
            List<OrderGoodsEntity> list = new List<OrderGoodsEntity>();
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            list = bll.GetUserRentOrdersPage(where, index, pageSize, out totalRecord, out totalPage);
            PagedList<OrderGoodsEntity> pageList = null;
            if (list != null)
            {
                pageList = new PagedList<OrderGoodsEntity>(list, index, pageSize, totalRecord);
            }
            else
            {
                if (index > 1)
                {
                    index = index - 1;
                    string url = Url.UserSiteUrl().RentOrdersList + "?pageIndex=" + index;
                    return Redirect(url);
                }
            }
            return View(pageList);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-24
        /// 用户后台租用订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult RentOrdersInfo(int orderGoodsId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            OrderGoodsEntity entity = bll.GetUserRentOrdersInfo(orderGoodsId, userId);
            //OrderState等于8为待续租状态   些状态下会产生滞纳金 
            if (entity.OrderState == 8)
            {
                //根据订单ID 查找扩展表 计算滞纳金用
                ViewData["OrderOther"] = bll.GetOrderOtherByOrderId(entity.OrderId);
            }
            return View(entity);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-30
        /// 续租页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNotPayment(int orderGoodsId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            OrderGoodsEntity entity = bll.GetUserRentOrdersInfo(orderGoodsId, userId);
            //OrderState等于8为待续租状态   此状态下会产生滞纳金 
            if (entity.OrderState == 8)
            {
                //根据订单ID 查找扩展表 计算滞纳金用
                ViewData["OrderOther"] = bll.GetOrderOtherByOrderId(entity.OrderId);
            }
            ViewData["UserRentOrdersInfo"] = entity;
            ViewData["Payment"] = bll.GetPaymentRecord(userId, orderGoodsId);
            ViewData["NotPayment"] = bll.GetNotPayment(userId, orderGoodsId);
            return View();
        }




        /// <summary>
        /// 续租
        /// Author:baiyan
        /// Time:2014-4-30
        /// </summary>
        /// <param name="id">订单商品租期ID</param>
        /// <returns></returns>
        public ActionResult Payment(int tenancyId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            GoodsTenanci tenanci = bll.GetPayment(tenancyId, userId);
            int result = 0;
            if (tenanci != null)
            {
                tenanci.IsDelivery = true;
                result = bll.Payment(tenanci);
            }
            return Json(result);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-4
        /// 买断
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public ActionResult Buyout(int orderGoodsId)
        {
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            decimal price = bll.GetBuyoutSum(orderGoodsId);
            int result = bll.Buyout(orderGoodsId, userId);
            return Json(result);
        }

        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-21
        /// Desc:撤消订单返回上一级操作
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelOrderOperating(int orderId)
        {
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int result = bll.CancelOrderOperating(orderId, userId);
            return Json(result);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 申请退货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public ActionResult ApplicationReturnOfGoods(int orderId)
        {
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int state = 12;
            int result = bll.ApplicationReturnOfGoods(orderId, userId, state);
            return Json(result);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-7
        /// 申请换货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public ActionResult ReturnsGoods(int orderId)
        {
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int state = 15;
            int result = bll.ReturnsGoods(orderId, userId, state);
            return Json(result);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-28
        /// 确认收货
        /// </summary>
        /// <param name="orderId">订单ID</param>
        public ActionResult ConfirmReceipt(int orderId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int state = 5;//要更改成的状态
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int result = bll.ConfirmReceipt(orderId, userId, state);
            return Json(result);
        }



        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 申请退租
        ///  <param name="orderId">订单ID</param>
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplicationSurrender(int orderId)
        {
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int state = 9;
            int result = bll.ApplicationSurrender(orderId, userId, state);
            return Json(result);
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public int CancelOrders(int orderId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int state = 19;
            return bll.CancelOrders(userId, orderId, state);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-23
        /// 删除订单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="orderGoodsId">订单商品ID</param>
        /// <returns></returns>
        public int DelOrders(int orderGoodsId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            return bll.DelOrders(userId, orderGoodsId);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-28
        /// 确认起租协议
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public ActionResult ConfirmHire(int orderId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int state = 7;//要更改成的状态
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int result = bll.ConfirmHire(orderId, userId, state);
            return Json(result);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 退租扣除赔损金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult SurrenderPayout(int orderId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int state = 11;//要更改成的状态
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int result = bll.ConfirmPayout(orderId, userId, state);
            return Json(result);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-29
        /// 退货扣除赔损金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult ReturnOfGoodsPayout(int orderId)
        {
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int state = 14;//要更改成的状态
            var bll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            int result = bll.ConfirmPayout(orderId, userId, state);
            return Json(result);
        }


        /// <summary>
        /// 获取商品图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsPic(string guid)
        {
            var pic = GetPicture.GetFirstGoodsPicture(guid, true, "SHOW");
            //if (string.IsNullOrEmpty(pic))
            //{
            //    pic = "http://static.zushou.com/static_v1.0/web/user/images/pic7.jpg";
            //}
            //else
            //{
            //    pic = pic + ".60_60.jpg";
            //}
            return Json(new { guid = guid, url = pic }, JsonRequestBehavior.AllowGet);
        }
    }
}
