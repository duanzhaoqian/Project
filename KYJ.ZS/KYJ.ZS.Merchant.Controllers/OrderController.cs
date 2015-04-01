using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.Accounts;
using KYJ.ZS.BLL.Orders;
using KYJ.ZS.BLL.RentalGoodses;
using KYJ.ZS.Commons;
using KYJ.ZS.Merchant.Controllers.ActionFilters;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.OrderGoodses;
using KYJ.ZS.Models.Orders;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.Merchant.Controllers
{
    public class OrderController : BaseController
    {
        OrderBll orderBll = new OrderBll();
        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月4日11:11:19
        /// 描述：商户登陆后的主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Main()
        {
            MerchantMainView mainView = new MerchantMainView();
            #region 获取各个状态的订单
            OrderBll bll = new OrderBll();
            mainView.OrderStateNum = bll.GetOrderSatteNum(_ServiceContext.CurrentUser.UserID);
            #endregion
            #region 获取各个状态的商品
            RentalGoodsBll rentalGoodsBll = new RentalGoodsBll();
            mainView.GetRentalGoodsStateNum = rentalGoodsBll.GetRentalGoodsStateNum(_ServiceContext.CurrentUser.UserID);
            #endregion
            #region 获取账户总金额
            AccountBll accountBll = new AccountBll();
            mainView.Money = accountBll.GetMoney(_ServiceContext.CurrentUser.UserID);
            #endregion

            string picPath = Url.MerchantSiteUrl().Merchant_DefultLogo;
            string netPic =
                KYJ.ZS.Commons.PictureModular.GetPicture.GetUserPictureInfo(_ServiceContext.CurrentUser._Guid, true,
                    KYJ.ZS.Commons.PictureModular.UserPictureType.MERCHANTLOGO.ToString()).Path;
            if (!string.IsNullOrEmpty(netPic))
            {
                picPath = netPic + ".98_34.jpg";
            }
            ViewBag.LogoPic = picPath;
            return View(mainView);
        }
        /// <summary>
        /// 订单管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderManager(RentOrderPms rentOrderPms)
        {
            #region 订单状态所在区域参数判断

            string strOrderTypeArea = Request["areaType"];
            string orderStateType = Request["orderStateType"];
            if (strOrderTypeArea == null && orderStateType == null)
            {
                rentOrderPms.OrderAreaType = OrderStateAreaType.All;
            }
            else
            {
                rentOrderPms.OrderAreaType = (OrderStateAreaType)Convert.ToInt32(strOrderTypeArea);
            }
            #endregion

            #region 订单状态判断

            if (orderStateType == null)
            {
                rentOrderPms.OrderStateType = OrderState.All;
            }
            else
            {
                rentOrderPms.OrderStateType = (OrderState)Convert.ToInt32(orderStateType);
            }
            #endregion
            OrderManagerView orderManagerView = new OrderManagerView();
            rentOrderPms.MerchantId = _ServiceContext.CurrentUser.UserID;
            orderManagerView.PageData = orderBll.GetRentalOrdersList(rentOrderPms);
            orderManagerView.RentOrderPms = rentOrderPms;
            return View(orderManagerView);
        }
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="expressCompany"></param>
        /// <param name="expressNum"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult SendGoods(int? orderId, string expressCompany, string expressNum, int? area)
        {
            if (area == null)
            {
                area = 1;
            }
            BackMessge bm = new BackMessge();

            if (orderId == null || expressCompany == null || expressNum == null || area == null)
            {
                bm.Message = "请您输入完整的发货信息！";
            }
            else
            {
                bm.Success = orderBll.UpdateSendInfo((int)orderId, expressCompany, expressNum);
                if (bm.Success)
                {
                    bm.Message = "发货成功，正在刷新！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "发货失败，请重试！";
                }
            }
            return Json(bm);
        }
        /// <summary>
        /// 发送起租协议
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult SendAgreement(int? orderId, int? area)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || area == null)
            {
                bm.Message = "参数错误，请重试！";
            }
            else
            {
                bm.Success = orderBll.SendAgreement((int)orderId);
                if (bm.Success)
                {
                    bm.Message = "发送起租协议成功！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "发送起租协议失败!";
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 同意退租
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult BackRentAgree(int? orderId, int? area)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || area == null)
            {
                bm.Message = "参数有误，请您重试!";
            }
            else
            {
                bm.Success = orderBll.BackRentAgree((int)orderId);
                if (bm.Success)
                {
                    bm.Message = "操作成功，正在刷新！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "操作失败，请稍后重试！";
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 返回到上级订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="area"></param>
        /// <param name="orderState"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult GoBack(int? orderId, int? area, int? orderState, string message)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || area == null || orderState == null)
            {
                bm.Message = "参数错误，请刷新重试!";
            }
            else
            {
                bm.Success = orderBll.OrderGoBack((int)orderId, (int)orderState, message);
                if (bm.Success)
                {
                    bm.Message = "操作成功，正在刷新！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "操作失败，请重试！";
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 赔损金额，改变订单状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult LossMoney(int? orderId, decimal? money, int? area, int? orderState)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || money == null || area == null || orderState == null)
            {
                bm.Message = "请输入正确的参数！";
            }
            else
            {
                bm.Success = orderBll.ChangeMoney((int)orderId, (decimal)money, (OrderState)(int)orderState);
                if (bm.Success)
                {
                    bm.Message = "操作成功！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "操作失败，请重试！";
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 改变金额，但是不改变订单状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult ChangeMoney(int? orderId, decimal? money, int? area)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || money == null || area == null)
            {
                bm.Message = "请输入正确的参数！";
            }
            else
            {
                bm.Success = orderBll.ChangeMoney((int)orderId, (decimal)money);
                if (bm.Success)
                {
                    bm.Message = "修改成功！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 换货同意
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult ExChangeGoodsAgree(int? orderId, int? orderState, int? area)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || orderState == null || area == null)
            {
                bm.Message = "参数有误，请重试！";
            }
            else
            {
                bm.Success = orderBll.ExChangeGoodsAgree((int)orderId, (OrderState)orderState);
                if (bm.Success)
                {
                    bm.Message = "操作成功！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "操作失败，请重试！";
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 换货验收撤销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult CancleExchange(int? orderId, int? area)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || area == null)
            {
                bm.Message = "参数错误！";
            }
            else
            {
                bm.Success = orderBll.CancleExchange((int)orderId);
                if (bm.Success)
                {
                    bm.Message = "操作成功";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "操作失败！";
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 退货同意
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult BackGoodsAgree(int? orderId, int? area)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || area == null)
            {
                bm.Message = "参数错误！";
            }
            else
            {
                bm.Success = orderBll.BackGoodsAgree((int)orderId);
                if (bm.Success)
                {
                    bm.Message = "操作成功！";
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int)area);
                }
                else
                {
                    bm.Message = "操作失败，请重试！";
                }
            }
            return Json(bm);
        }

        /// <summary>
        /// 商户结算
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [IsMerOrder]
        public ActionResult MerchantSettlement(int? orderId, int? orderState, int? area)
        {
            BackMessge bm = new BackMessge();
            if (orderId == null || area == null || orderState == null)
            {
                bm.Message = "参数错误！";
            }
            else
            {
                string mg;
                bm.Success = orderBll.MerchantSettlement((int)_ServiceContext.CurrentUser.UserID, (int)orderId, (OrderState)orderState, out mg);
                bm.Message = mg;
                if (bm.Success)
                {
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().OrderManager((int) area);
                }
            }
            return Json(bm);
        }


    }
    /// <summary>
    /// 用于判断该订单是不是当前用户的
    /// </summary>
    public class IsMerOrderAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 作者：maq
        /// 时间：2014年6月5日14:26:05
        /// 描述：判断当前用户要操作的订单是不是他自己的
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BackMessge bm = new BackMessge();
            Boolean result = false;
            int userId = filterContext.Controller.GetServiceContext().CurrentUser.UserID;
            string strOrderId = filterContext.HttpContext.Request["orderId"];
            if (!string.IsNullOrEmpty(strOrderId))
            {
                if (new OrderBll().IsMerOrder(Auxiliary.ToInt32(strOrderId), userId))
                {
                    result = true;
                }
            }
            if (!result)
            {
                bm.Message = "非法操作";
                filterContext.Result=new JsonResult(){Data = bm};
            }
        }
    }

}
