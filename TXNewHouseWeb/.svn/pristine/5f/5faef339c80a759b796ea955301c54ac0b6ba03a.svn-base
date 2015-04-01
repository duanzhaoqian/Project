using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace TXManagerWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region 通用

            routes.MapRoute(
                "", // 路由名称
                "", // 带有参数的 URL
                new {controller = "common", action = "nhouseindex"} // 参数默认值
                );

            //普通URL
            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}.html", // 带有参数的 URL
                new {controller = "common", action = "nhouseindex"} // 参数默认值
                );

            //带ID的URL
            routes.MapRoute(
                "Default_ID", // 路由名称
                "{controller}/{action}/{id}/{random}.html", // 带有参数的 URL
                new {controller = "common", action = "nhouseindex", id = 0, random = new Random().Next(1000, 1000000)}, // 参数默认值
                new {id = @"\d+"}
                );
            //带ID的URL
            routes.MapRoute(
                "Default_ID_Type", // 路由名称
                "{controller}/{action}/{id}/{type}/{random}.html", // 带有参数的 URL
                new {controller = "common", action = "nhouseindex", id = 0, type = 0, random = new Random().Next(1000, 1000000)}, // 参数默认值
                new {id = @"\d+", type = @"\d+"}
                );

            #endregion

            //新房管理 --> 楼盘搜索路由
            routes.MapRoute(
                "premisesUrl", // 路由名称
                "Premises/{action}/{ProvinceID}-{CityID}/{SalesStatus}/{random}.html", // 带有参数的URL 
                new {controller = "Premises", action = "search", ProvinceID = 0, CityID = 0, SalesStatus = -1, random = new Random().Next(1000, 1000000)}, // 参数默认值
                new {ProvinceID = @"\d+", CityID = @"\d+"}
                );
            //营销活动管理--> 砍价活动管理搜索路由
            routes.MapRoute(
                "BargainActiveUrl", // 路由名称
                "BargainActive/{action}/{Type}/{ProvinceID}-{CityId}/{PremisesId}/{BuildingId}/{UnitId}/{FloorId}/{ActivitieState}/{random}.html", // 带有参数的URL 
                new { controller = "BargainActive", action = "search", Type = 0, ProvinceID = 0, CityID = 0,PremisesId=0,BuildingId=0,UnitId=0,FloorId=0, ActivitieState = -1, random = new Random().Next(1000, 1000000) }, // 参数默认值
                new { ProvinceID = @"\d+", CityId = @"\d+", PremisesId = @"\d+", BuildingId = @"\d+", UnitId = @"\d+", FloorId = @"\d+" }
                );
            //营销活动管理--> 一口价活动管理搜索路由
            routes.MapRoute(
                "APriceUrl", // 路由名称
                "APrice/{action}/{Type}/{ProvinceID}-{CityId}/{ActivitieState}/{random}.html", // 带有参数的URL 
                new { controller = "APrice", action = "search", Type = 0, ProvinceID = 0, CityID = 0, ActivitieState = -1, random = new Random().Next(1000, 1000000) }, // 参数默认值
                new { ProvinceID = @"\d+", CityId = @"\d+" }
                );
            //营销活动管理--> 阶梯团购活动搜索路由
            routes.MapRoute(
                "LadderGroupUrl", // 路由名称
                "LadderGroup/{action}/{Type}/{ProvinceID}-{CityId}/{PremisesId}/{ActivitieState}/{random}.html", // 带有参数的URL 
                new { controller = "LadderGroup", action = "search", Type=0, ProvinceID = 0, CityId = 0, PremisesId = 0, ActivitieState = -1, random = new Random().Next(1000, 1000000) }, // 参数默认值
                new { ProvinceID = @"\d+", CityId = @"\d+", PremisesId = @"\d+" }
                );
            //营销活动管理--> 排号购房活动搜索路由
            routes.MapRoute(
                "PurchaseHouseUrl", // 路由名称
                "PurchaseHouse/{action}/{Type}/{ProvinceID}-{CityId}/{PremisesId}/{ActivitieState}/{random}.html", // 带有参数的URL 
                new { controller = "PurchaseHouse", action = "search", Type=0, ProvinceID = 0, CityId = 0, PremisesId = 0, ActivitieState = -1, random = new Random().Next(1000, 1000000) }, // 参数默认值
                new { ProvinceID = @"\d+", CityId = @"\d+", PremisesId = @"\d+" }
                );
            //营销活动管理--> 竞价活动管理搜索路由
            routes.MapRoute(
                "NhBidingSearchUrl", // 路由名称
                "NhBiding/{action}/{ProvinceId}-{CityId}-{PremisesId}-{BuildingId}-{UnitId}-{Floor}/{ActivitieState}/{random}.html", // 带有参数的URL 
                new { controller = "NhBiding", action = "search", ProvinceId = 0, CityId = 0,PremisesId=0,BuildingId=0,UnitId=0,Floor=0, ActivitieState = -1, random = new Random().Next(1000, 1000000) }, // 参数默认值
                new { ProvinceID = @"\d+", CityId = @"\d+" }
                );
            //ID-PID （摇号）
            routes.MapRoute(
                "Default_ID_PId", // 路由名称
                "{controller}/{action}/{id}/p{pid}/{random}.html", // 带有参数的 URL
                new { controller = "common", action = "nhouseindex", id = 0, pid = 0, random = new Random().Next(1000, 1000000) }, // 参数默认值
                new { id = @"\d+", pid = @"\d+" }
                );
            //ID-PID-HId （竞价用户列表）
            routes.MapRoute(
                "Default_ID_PId_HId", // 路由名称
                "{controller}/{action}/{id}/p{pid}/h{hid}/{random}.html", // 带有参数的 URL
                new { controller = "common", action = "nhouseindex", id = 0, pid = 0,hid=0, random = new Random().Next(1000, 1000000) }, // 参数默认值
                new { id = @"\d+", pid = @"\d+", hid = @"\d+" }
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}