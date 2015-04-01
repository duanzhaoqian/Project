using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Commons
{
    public class PaySiteUrlHelper
    {
        private static readonly PaySiteUrlHelper instance = new PaySiteUrlHelper();

        private static string BaseUrl;

        internal static PaySiteUrlHelper Instance
        {
            get
            {
                if (string.IsNullOrEmpty(BaseUrl))
                    BaseUrl = PubConstant.PayBaseUrl;
                return instance;
            }
        }

        private PaySiteUrlHelper()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal PaySiteUrlHelper SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }

        #region URL集装箱

        #region Author:zhuzh  Date:2014-05-19  Desc:支付订单

        /// <summary>
        /// 支付订单
        /// </summary>
        /// <param name="ordernum">订单号（加密）</param>
        /// <param name="callbackurl">回调URL</param>
        public string Order(string ordernum, string callbackurl)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "order", "payment", new { ordernum = ordernum, callbackurl = callbackurl });
        }

        /// <summary>
        /// 信息确认订单
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="status">订单号类型：1 订单编号，2 充值订单号</param>
        /// <param name="callbackurl">回调URL</param>
        /// <returns></returns>
        public string Verify
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "verify", "payment"); }
        }
        /// <summary>
        /// 发布支付请求
        /// </summary>
        /// <param name="callbackurl">回调URL</param>
        public string Send
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "send", "payment"); }
        }
        /// <summary>
        /// 接收支付后请求
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="status">支付类型：0 未知，1 订单支付，2 充值</param>
        /// <param name="callbackurl">回调URL</param>
        public string Receive(string ordernum, int status, string callbackurl)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "receive", "payment", new { ordernum = ordernum, status = status, callbackurl = callbackurl });
        }

        #endregion

        #region Author:zhuzh  Date:2014-05-19  Desc:租期-支付订单

        /// <summary>
        /// 支付订单
        /// </summary>
        /// <param name="ordernum">订单号（加密）</param>
        /// <param name="type">续租或购买 1：续租,2:购买</param>
        /// <param name="callbackurl">回调URL</param>
        public string TenancyOrder(string ordernum, int type, string callbackurl)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "order", "tenancy", new { ordernum = ordernum, type = type, callbackurl = callbackurl });
        }

        /// <summary>
        /// 信息确认订单
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="status">订单号类型：1 订单编号，2 充值订单号</param>
        /// <param name="callbackurl">回调URL</param>
        /// <returns></returns>
        public string TenancyVerify
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "verify", "tenancy"); }
        }
        /// <summary>
        /// 发布支付请求
        /// </summary>
        /// <param name="callbackurl">回调URL</param>
        public string TenancySend
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "send", "tenancy"); }
        }
        /// <summary>
        /// 接收支付后请求
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="status">支付类型：0 未知，1 订单支付，2 充值</param>
        /// <param name="callbackurl">回调URL</param>
        public string TenancyReceive(string ordernum, int status, string callbackurl)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "receive", "tenancy", new { ordernum = ordernum, status = status, callbackurl = callbackurl });
        }

        #endregion

        #region Author:zhuzh Date:2014-06-11 Desc:赔付支付订单

        /// <summary>
        /// 赔付支付订单
        /// </summary>
        /// <param name="ordernum">订单号（加密）</param>
        /// <param name="type">赔付类型 1：退租用户结算待支付,2:退货用户结算待支付,3:换货用户结算待支付</param>
        /// <param name="callbackurl">回调URL</param>
        public string ClaimOrder(string ordernum, int type, string callbackurl)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "order", "claim", new { ordernum = ordernum, type = type, callbackurl = callbackurl });
        }

        /// <summary>
        /// 赔付信息确认订单
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="status">订单号类型：1 订单编号，2 充值订单号</param>
        /// <param name="callbackurl">回调URL</param>
        /// <returns></returns>
        public string ClaimVerify
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "verify", "claim"); }
        }
        /// <summary>
        /// 赔付发布支付请求
        /// </summary>
        /// <param name="callbackurl">回调URL</param>
        public string ClaimSend
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "send", "claim"); }
        }
        /// <summary>
        /// 赔付接收支付后请求
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="status">支付类型：0 未知，1 订单支付，2 充值</param>
        /// <param name="callbackurl">回调URL</param>
        public string ClaimReceive(string ordernum, int status, string callbackurl)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "receive", "claim", new { ordernum = ordernum, status = status, callbackurl = callbackurl });
        }


        #endregion
 
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
