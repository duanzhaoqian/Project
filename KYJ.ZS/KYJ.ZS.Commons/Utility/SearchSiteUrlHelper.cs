using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Commons
{
    public class SearchSiteUrlHelper
    {
        private static readonly SearchSiteUrlHelper instance = new SearchSiteUrlHelper();

        private static string BaseUrl;

        internal static SearchSiteUrlHelper Instance
        {
            get
            {
                if (string.IsNullOrEmpty(BaseUrl))
                    BaseUrl = PubConstant.SearchBaseUrl;
                return instance;
            }
        }

        private SearchSiteUrlHelper()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal SearchSiteUrlHelper SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }

        #region URL集装箱
        #region wwang
        /// <summary>
        /// 搜索首页
        /// </summary>
        public string Index
        {
            get { return BaseUrl+"/search"; }
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
