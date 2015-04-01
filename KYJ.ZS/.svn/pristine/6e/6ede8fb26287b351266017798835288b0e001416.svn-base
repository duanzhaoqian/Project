using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KYJ.ZS.Commons.Web;

namespace KYJ.ZS.MerchantWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "", // Route name
               "", // URL with parameters
               new { controller = "common", action = "index" } // Parameter defaults
           );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}.html", // URL with parameters
                new { controller = "common", action = "login" } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Register(new DependencyPathViewEngine("Views", "Views/Shared/Controls"));
        }
    }
}