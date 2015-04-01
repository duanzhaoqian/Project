using System.Web.Mvc;
using System.Web.Routing;

namespace TXNewHouseWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Default", // 路由名称
            //    "{controller}/{action}/{id}", // 带有参数的 URL
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            //);

            routes.MapRoute(
               "Default2", // 路由名称
               "{controller}/{action}/{id}", // 带有参数的 URL
               new { controller = "Search", action = "PremisesList", id = UrlParameter.Optional } // 参数默认值
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //// 重写视图引擎
            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new WebViewEngine());
            RegisterRoutes(RouteTable.Routes);
        }

        protected class WebViewEngine : WebFormViewEngine
        {
            public WebViewEngine()
            {
                base.MasterLocationFormats = new string[] { "~/Views/{1}/{0}.master", "~/Views/Shared/{0}.master" };
                base.ViewLocationFormats = new string[] { 
                "~/Views/Users/{1}/{0}.aspx",
                 //"~/Views/Search/{0}.ascx",
                 "~/Views/Shared/{0}.ascx",
                 "~/Views/{1}/{0}.aspx"};
                base.PartialViewLocationFormats = base.ViewLocationFormats;
            }

        }
    }
}