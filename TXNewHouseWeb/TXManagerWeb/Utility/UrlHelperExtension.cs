using System.Web.Mvc;
using System.Web.Routing;

namespace TXManagerWeb.Utility
{
    public static class UrlHelperExtension
    {
        public static string RouteAction(this UrlHelper helper, string routeName)
        {
            return RouteAction(helper, routeName, null, null);
        }

        public static string RouteAction(this UrlHelper helper, string routeName, string actionName)
        {
            return RouteAction(helper, routeName, actionName, null);
        }

        public static string RouteAction(this UrlHelper helper, string routeName, string actionName, string controllerName)
        {
            return RouteAction(helper, routeName, actionName, controllerName, null);
        }

        public static string RouteAction(this UrlHelper helper, string routeName, string actionName, object routeValues)
        {
            return RouteAction(helper, routeName, actionName, null, routeValues);
        }

        public static string RouteAction(this UrlHelper helper,
            string routeName, string actionName, string controllerName, object routeValues)
        {
            if (!string.IsNullOrWhiteSpace(actionName))
                SetRouteValue(helper.RequestContext.RouteData, "action", actionName);
            if (!string.IsNullOrWhiteSpace(controllerName))
                SetRouteValue(helper.RequestContext.RouteData, "controller", controllerName);
            return helper.RouteUrl(routeName, routeValues);
        }

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
    }
}