using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace KYJ.ZS.Commons
{
    public static class UtilityExtensions
    {
        /// <summary>
        /// 前台URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>WebSiteUrlHelper前台URL统一存放地</returns>
        public static WebSiteUrlHelper WebSiteUrl(this UrlHelper helper)
        {
            return WebSiteUrlHelper.Instance.SetContext(helper);
        }
        /// <summary>
        /// 前台搜索URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>SearchSiteUrlHelper前台URL统一存放地</returns>
        public static SearchSiteUrlHelper SearchSiteUrl(this UrlHelper helper)
        {
            return SearchSiteUrlHelper.Instance.SetContext(helper);
        }
        /// <summary>
        /// 支付URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>PaySiteUrlHelper前台URL统一存放地</returns>
        public static PaySiteUrlHelper PaySiteUrl(this UrlHelper helper)
        {
            return PaySiteUrlHelper.Instance.SetContext(helper);
        }
        /// <summary>
        /// 用户后台URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>UserSiteUrlHelper前台URL统一存放地</returns>
        public static UserSiteUrlHelper UserSiteUrl(this UrlHelper helper)
        {
            return UserSiteUrlHelper.Instance.SetContext(helper);
        }
        /// <summary>
        /// 前台URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>WebSiteUrlHelper前台URL统一存放地</returns>
        public static MerchantSiteUrlHelper MerchantSiteUrl(this UrlHelper helper)
        {
            return MerchantSiteUrlHelper.Instance.SetContext(helper);
        }
        /// <summary>
        /// 管理后台URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>ManagerSiteUrlHelper前台URL统一存放地</returns>
        public static ManagerSiteUrlHelper ManagerSiteUrl(this UrlHelper helper)
        {
            return ManagerSiteUrlHelper.Instance.SetContext(helper);
        }
        /// <summary>
        /// 专题URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>SpecialSiteUrlHelper前台URL统一存放地</returns>
        public static SpecialSiteUrlHelper SpecialSiteUrl(this UrlHelper helper)
        {
            return SpecialSiteUrlHelper.Instance.SetContext(helper);
        }
        /// <summary>
        /// 帮助中心URL扩展
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <returns>HelpSiteUrlHelper前台URL统一存放地</returns>
        public static HelpSiteUrlHelper HelpSiteUrl(this UrlHelper helper)
        {
            return HelpSiteUrlHelper.Instance.SetContext(helper);
        }
        #region 自定义辅助工具

        /// <summary>
        /// 通过路由名称获取URL
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <param name="routeName">路由名称</param>
        /// <returns>URL</returns>
        public static string RouteAction(this UrlHelper helper, string routeName)
        {
            return RouteAction(helper, routeName, null, null);
        }
        /// <summary>
        /// 通过路由名称和ActionName获取URL
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="actionName">对应控制器中的Action名称</param>
        /// <returns>URL</returns>
        public static string RouteAction(this UrlHelper helper, string routeName, string actionName)
        {
            return RouteAction(helper, routeName, actionName, null);
        }
        /// <summary>
        /// 通过路由名称、ActionName和ConrollerName获取URL
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="actionName">控制器中的Action名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <returns>URL</returns>
        public static string RouteAction(this UrlHelper helper, string routeName, string actionName, string controllerName)
        {
            return RouteAction(helper, routeName, actionName, controllerName, null);
        }

        /// <summary>
        /// 通过路由名称、ActionName和routeValues（路由参数）获取URL
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="actionName">控制器中的Action名称</param>
        /// <param name="routeValues">路由参数</param>
        /// <returns>URL</returns>
        public static string RouteAction(this UrlHelper helper, string routeName, string actionName, object routeValues)
        {
            return RouteAction(helper, routeName, actionName, null, routeValues);
        }
        /// <summary>
        /// 自定义RouteAction辅助方法
        /// </summary>
        /// <param name="helper">UrlHelper系统类</param>
        /// <param name="routeName">路由名称</param>
        /// <param name="actionName">控制器中的Action名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="routeValues">路由参数</param>
        /// <returns>URL</returns>
        public static string RouteAction(this UrlHelper helper,
            string routeName, string actionName, string controllerName, object routeValues)
        {
            if (!string.IsNullOrWhiteSpace(actionName))
                SetRouteValue(helper.RequestContext.RouteData, "action", actionName);
            if (!string.IsNullOrWhiteSpace(controllerName))
                SetRouteValue(helper.RequestContext.RouteData, "controller", controllerName);
            return helper.RouteUrl(routeName, routeValues);
        }
        /// <summary>
        /// 自定义辅助方法
        /// </summary>
        /// <param name="routeData">路由参数</param>
        /// <param name="key">参数名称（key）</param>
        /// <param name="value">参数值（value）</param>
        private static void SetRouteValue(RouteData routeData, string key, object value)
        {
            if (!routeData.Values.ContainsKey(key))
            {
                routeData.Values.Add(key, value);
            }
            else
            {
                routeData.Values[key] = value;
            }
        }

        #endregion
    }
}
