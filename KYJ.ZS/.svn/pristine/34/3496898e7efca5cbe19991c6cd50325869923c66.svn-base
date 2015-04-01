using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.DB;
using KYJ.ZS.BLL.OrderGoodses;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.Models.OrderGoodses;

namespace KYJ.ZS.Merchant.Controllers
{
    /// <summary>
    /// Auther:李晓波
    /// Time:2014-4-17
    /// Desc:交易管理
    /// </summary>   
    public class TransManageController : BaseController
    {
        OrderGoodsBll bll = new OrderGoodsBll();
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-17
        /// Desc:出租的订单
        /// </summary>   
        /// <param name="orderDate">商品类型</param>
        /// <param name="orderDate">订单时间范围</param>
        /// <param name="State">订单状态</param>
        /// <param name="Title">订单商品名称</param>
        /// <param name="Number">订单编号</param>
        /// <param name="NickName">用户昵称呢过</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public ActionResult RentalOrder(int? pageIndex)
        {
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            //int id = 7;//获取商户ID        
            RentalOrderEntity mdlRentalOrder = InitRentalOrder();                
            List<RentalOrderEntity> orderGoodsList = new List<RentalOrderEntity>();
            int index = pageIndex ?? 1;
            int totalCount = 0;
            int totalPage = 0;
            orderGoodsList = bll.GetList(id,mdlRentalOrder, index, 5, out  totalCount, out totalPage);
            PagedList<RentalOrderEntity> mPage = null;
            if (orderGoodsList != null)
            {
                mPage = new PagedList<RentalOrderEntity>(orderGoodsList, index, 5, totalCount);
            }
            return View(mPage);
        }


        /// <summary>
        /// Auther:李晓波
        /// Time:2014-5-8
        /// Desc:根据商户操作改变订单状态
        /// </summary>  
        /// <param name="tblType">订单所属表</param>
        /// <param name="operationType">订单操作类型</param>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public ActionResult ChangeOrderStatus(int operationType,int orderId,string res)
        {
            bool result;
            //退货驳回
            if (operationType==1111) 

            {
                //从订单扩展表中取出当前订单的返回订单状态和时间
              int orderState=  bll.getOderState(orderId);//从订单扩展表中根据订单id取得订单状态
              if (!string.IsNullOrEmpty(res))
              {
                  bool ok = bll.updateRes(orderId, res);//更新订单扩展表驳回理由   
                  if (!ok)
                  {
                       return Json(ok);
                  }
              }
              DateTime orderTime = bll.getOderTime(orderState);
             result= bll.updateOrder(orderId,orderState,orderTime);//更新订单表的状态和时间
            }
            else
            {
               result  = bll.ChangeOrderStatus(operationType, orderId);
            }
         
            return Json(result);
        }
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-17
        /// Desc:接受前台传过来的数据
        /// </summary>          
        private RentalOrderEntity InitRentalOrder()
        {
            RentalOrderEntity mdlRentalOrder = new RentalOrderEntity();
            if (!string.IsNullOrEmpty(Request["orderType"]))
            {
                mdlRentalOrder.OrderType = Convert.ToInt32(Request["orderType"]);
                ViewBag.OrderType =mdlRentalOrder.OrderType;
            }     
            if (!string.IsNullOrEmpty(Request["state"]))
            {
                mdlRentalOrder.State = Convert.ToInt32(Request["state"]);
                ViewBag.State = mdlRentalOrder.State;
            }
            if (!string.IsNullOrEmpty(Request["orderDate"]))
            {
                mdlRentalOrder.OrderDate = Request["orderDate"];
                ViewBag.OrderDate = mdlRentalOrder.OrderDate;
            }
           
            mdlRentalOrder.Title = Request["title"];
            ViewBag.OrderName = mdlRentalOrder.Title;
            mdlRentalOrder.Number = Request["number"];
            ViewBag.Number = mdlRentalOrder.Number;
            mdlRentalOrder.NickName = Request["nickName"];
            ViewBag.NickName = mdlRentalOrder.NickName;
            return mdlRentalOrder;
        }
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-17
        /// Desc:获取代售商品数据列表
        /// </summary>         
        /// <param name="orderDate">商品类型</param>
        /// <param name="State">订单状态</param>
        /// <param name="Title">订单商品名称</param>
        /// <param name="Number">订单编号</param>
        /// <param name="NickName">用户昵称呢过</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public ActionResult BookingOrder(int? pageIndex)
        {
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            //int id = 7;//获取商户ID
        
            RentalOrderEntity mdlRentalOrder = InitRentalOrder();
            if (mdlRentalOrder.OrderType == null)
            {
                mdlRentalOrder.OrderType = 2;//代售
            }
            if (mdlRentalOrder.State == null)
            {
                mdlRentalOrder.State = 16;
            }
            List<RentalOrderEntity> orderGoodsList = new List<RentalOrderEntity>();
            int index = pageIndex ?? 1;
            int totalCount = 0;
            int totalPage = 0;
            orderGoodsList = bll.GetList(id,mdlRentalOrder, index, 2, out  totalCount, out totalPage);
            PagedList<RentalOrderEntity> mPage = null;
            if (orderGoodsList != null)
            {
                mPage = new PagedList<RentalOrderEntity>(orderGoodsList, index, 2, totalCount);
            }
            return View(mPage);
        }

    }
}
