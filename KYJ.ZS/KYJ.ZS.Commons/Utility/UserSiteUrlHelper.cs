using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Commons
{
    public class UserSiteUrlHelper
    {
        private static readonly UserSiteUrlHelper instance = new UserSiteUrlHelper();

        private static string BaseUrl;

        internal static UserSiteUrlHelper Instance
        {
            get
            {
                if (string.IsNullOrEmpty(BaseUrl))
                    BaseUrl = PubConstant.UserBaseUrl;
                return instance;
            }
        }

        private UserSiteUrlHelper()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal UserSiteUrlHelper SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }


        #region URL集装箱

        #region Author:zhuzh Date:2014-04-30 Desc:用户后台帐号相关

        /// <summary>
        /// 用户后台主页
        /// </summary>
        public string Index
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "index", "user"); }
        }

        #region Author:zhuzh Date:2014-05-28 Desc:登录相关
        /// <summary>
        /// 用户后台登录
        /// </summary>
        public string Login
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "login", "common"); }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public string Logout
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "logout", "user"); }
        }

        /// <summary>
        /// 获取图片验证码图片流
        /// </summary>
        public string Code
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "code", "common"); }
        }

        /// <summary>
        /// 找回密码页面
        /// </summary>
        public string FindPWD
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "findpwd", "common"); }
        }
        /// <summary>
        /// 重置密码请求
        /// </summary>
        public string ResetPWD
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "resetpwd", "common"); }
        }

        #endregion

        #region Author:zhuzh Date:2014-5-7 Desc:帐号中心
        /// <summary>
        /// 帐号中心-基础资料页面
        /// </summary>
        public string Information
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "information", "user"); }
        }
        /// <summary>
        /// 帐号中心-基础资料修改请求
        /// </summary>
        public string ResetInformation
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "resetinformation", "user"); }
        }
        /// <summary>
        /// 帐号中心-教育情况
        /// </summary>
        public string Education
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "education", "user"); }
        }
        /// <summary>
        /// 帐号中心-教育情况修改请求
        /// </summary>
        public string ResetEducation
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "reseteducation", "user"); }
        }
        /// <summary>
        /// 帐号中心-头像照片
        /// </summary>
        public string Avatar
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "avatar", "user"); }
        }
        /// <summary>
        /// 帐号中心-名片
        /// </summary>
        public string Card
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "card", "user"); }
        }
        /// <summary>
        /// 帐号中心-名片
        /// </summary>
        public string ResetCard
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "resetcard", "user"); }
        }

        /// <summary>
        /// 帐号中心-修改密码
        /// </summary>
        public string Password
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "modifypwd", "user"); }
        }
        /// <summary>
        /// 帐号中心-修改密码请求
        /// </summary>
        public string ResetPassword
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "resetpassword", "user"); }
        }
        /// <summary>
        /// 帐号中心-申请认证
        /// </summary>
        public string Apply
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "apply", "user"); }
        }
        /// <summary>
        /// 帐号中心-个人认证
        /// </summary>
        public string Authentication
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "authentication", "user"); }
        }


        #endregion

        #region Author:zhuzh Date:2014-05-08 Desc:地理位置
        /// <summary>
        /// Author:zhuzh Date:2014-05-08 Desc:地理位置
        /// </summary>
        /// <param name="actionName">ActionName</param>
        public string GeographyLocation(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "common");
        }

        #endregion

        #region Author:zhuzh Date:2014-05-28 Desc:注册相关

        /// <summary>
        /// 用户后台登录
        /// </summary>
        public string Register
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "register", "common"); }
        }
        /// <summary>
        /// 注册请求地址
        /// </summary>
        public string SubRegister
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "subregister", "common"); }
        }

        /// <summary>
        /// 手机验证码
        /// </summary>
        public string MobileCode
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "mobilecode", "common"); }
        }

        /// <summary>
        /// 记录发送短信时间
        /// </summary>
        public string RecordMobile
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "recordmobile", "common"); }
        }

        public string CheckMobile
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "checkmobile", "common"); }
        }
        public string CheckCode {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "checkcode", "common"); }
        }
        #endregion

        #endregion

        #region baiyan
        /// <summary>
        /// 用户后台出租订单条件查询
        /// </summary>
        public string RentOrdersList
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "rentorderslist", "order"); }
        }
        /// <summary>
        /// 用户后台出租订单详情
        /// </summary>
        public string RentOrdersInfo(int orderGoodsId)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "rentordersinfo", "order", new { orderGoodsId = orderGoodsId });
        }
        /// <summary>
        /// 用户后台收藏列表
        /// </summary>
        public string CollectList
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "list", "collect"); }
        }
        /// <summary>
        /// 用户后台收货地址页面
        /// </summary>
        public string AddressList
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "list", "deliveryaddress"); }
        }
        /// <summary>
        /// 用户后台添加收货地址页面
        /// </summary>
        public string Insert
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "insert", "deliveryaddress"); }
        }
        /// <summary>
        /// 用户后台删除收货地址
        /// </summary>
        public string DelAddress
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deladdress", "deliveryaddress"); }
        }
        /// <summary>
        /// 用户后台添加收货地址
        /// </summary>
        public string AddDeliveryAddress
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "adddeliveryaddress", "deliveryaddress"); }
        }
        /// <summary>
        /// 用户后台修改收货地址页面
        /// </summary>
        public string DeliveryAddressUpdate(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "update", "deliveryaddress", new { id = id });
        }
        /// <summary>
        /// 用户后台修改收货地址
        /// </summary>
        public string UpdateDeliveryAddress
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "updatedeliveryaddress", "deliveryaddress"); }
        }
        /// <summary>
        /// 用户后台删除收藏的商品
        /// </summary>
        public string DelCollection
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delcollection", "collect"); }
        }
        /// <summary>
        /// 缴费
        /// </summary>
        public string Payment
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "payment", "order"); }
        }
        /// <summary>
        /// 买断
        /// </summary>
        public string Buyout
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "buyout", "order"); }
        }
        /// <summary>
        /// 确认收货
        /// </summary>
        public string ConfirmReceipt
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "confirmreceipt", "order"); }
        }
        /// <summary>
        /// 确认起租
        /// </summary>
        public string ConfirmHire
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "confirmhire", "order"); }
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        public string CancelOrders
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "cancelorders", "order"); }
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        public string DelOrders
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delorders", "order"); }
        }
        /// <summary>
        /// 续租
        /// </summary>
        public string GetNotPayment(int orderGoodsId)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "getnotpayment", "order", new { orderGoodsId = orderGoodsId });
        }
        /// <summary>
        /// 退租
        /// </summary>
        public string ApplicationSurrender
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "applicationsurrender", "order"); }
        }
        /// <summary>
        /// 退货
        /// </summary>
        public string ApplicationReturnOfGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "applicationreturnofgoods", "order"); }
        }
        /// <summary>
        /// 退换货
        /// </summary>
        public string ReturnsGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "returnsgoods", "order"); }
        }
        /// <summary>
        /// 撤消订单
        /// </summary>
        public string CancelOrderOperating
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "cancelorderoperating", "order"); }
        }
        /// <summary>
        /// 退租扣除赔损金额
        /// </summary>
        public string SurrenderPayout
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "surrenderpayout", "order"); }
        }
        /// <summary>
        /// 退货扣除赔损金额
        /// </summary>
        public string ReturnOfGoodsPayout
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "returnofgoodspayout", "order"); }
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        public string GetCities
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "getcities", "deliveryaddress"); }
        }
        /// <summary>
        /// 获取商品图片地址
        /// </summary>
        public string GetGoodsPic
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "getgoodspic", "order"); }
        }
        #endregion

        #region wangyu
        /// <summary>
        /// 用户后台发布商品
        /// </summary>
        public string PublishSaleGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "publishsalegoods", "salegoods"); }
        }

        /// <summary>
        /// 保存用户后台发布商品
        /// </summary>
        public string SavePublishSaleGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "savepublishsalegoods", "salegoods"); }
        }
        /// <summary>
        /// 用户后台修改商品
        /// </summary>
        public string UpdateSaleGoods(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "updatesalegoods", "salegoods", new { id = id });
        }

        /// <summary>
        /// 保存用户后台修改商品
        /// </summary>
        public string SaveUpdateSaleGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "saveupdatesalegoods", "salegoods"); }
        }
        #endregion

        //作者：孟国栋 
        //时间：2014.05.14
        //描述：商品的隐藏显示，删除,列表的展示等
        /// <summary>
        /// 显示/隐藏商品
        /// </summary>
        /// <param name="id">商品编号</param>
        /// <param name="flag">动作编号</param>
        /// <returns></returns>
        public string ShowGoods(int id, int flag, int pageIndex)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "showgoods", "Infomanager", new { saleId = id, flag = flag, pageIndex = pageIndex });
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public string Delete(int id, int pageIndex)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "deletegoods", "Infomanager", new { saleId = id, pageIndex = pageIndex });
        }
        public string DelGoods {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deletegoods", "infomanager"); }
        }
        /// <summary>
        /// 展示商品
        /// </summary>
        public string ShowPage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "showpage", "Infomanager"); }
        }
        #endregion

        #region URL工具方法

        /// <summary>
        /// 获取Action的绝对URL
        /// </summary>
        /// <param name="actionName">action的名称</param>
        /// <param name="controllerName">controller的名称</param>
        /// <returns></returns>
        private string ActionUrlAbsolute(string actionName, string controllerName)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.Action(actionName, controllerName)).AbsoluteUri;
        }

        /// <summary>
        /// 获取Action的绝对URL
        /// </summary>
        /// <param name="actionName">action的名称</param>
        /// <param name="controllerName">controller的名称</param>
        /// <param name="routeValues">路由数据</param>
        /// <returns></returns>
        private string ActionUrlAbsolute(string actionName, string controllerName, object routeValues)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.Action(actionName, controllerName, routeValues)).AbsoluteUri;
        }

        /// <summary>
        /// 获取Route的绝对URL
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeValues">路由数据</param>
        /// <returns></returns>
        private string RouteUrlAbsolute(string routeName, object routeValues)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.RouteUrl(routeName, routeValues)).AbsoluteUri;
        }

        private Uri GetBaseUrl(UrlHelper url)
        {
            Uri contextUri = new Uri(url.RequestContext.HttpContext.Request.Url,
                url.RequestContext.HttpContext.Request.RawUrl);
            UriBuilder realmUri = new UriBuilder(contextUri)
            {
                Path = url.RequestContext.HttpContext.Request.ApplicationPath,
                Query = null,
                Fragment = null
            };
            return realmUri.Uri;
        }

        #endregion
    }
}
