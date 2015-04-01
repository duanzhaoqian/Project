using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;

namespace JiePan.Ass.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            #region AutoFac组件注册
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new XmlFileReader("AutoFac.config"));
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            AutoFacTool.AssContainer = container;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
        }
    }
}
