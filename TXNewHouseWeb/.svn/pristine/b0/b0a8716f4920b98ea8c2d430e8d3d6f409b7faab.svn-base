using System;
using TXManagerWeb.Common;
using TXModel.AdminPVM;

namespace TXManagerWeb.Utility
{
    /// <summary>
    ///     SiteUrlHelper 路由
    /// </summary>
    public partial class SiteUrlHelper
    {
        #region 新房管理

        #endregion

        #region 开发商管理

        #region 开发商管理

        /// <summary>
        ///     开发商管理
        /// </summary>
        public string DevelopersIndex
        {
            get { return UrlHelper.Action("index", "developers"); }
        }

        /// <summary>
        ///     开发商管理搜索
        /// </summary>
        public string Developers_Search(string action)
        {
            return UrlHelper.Action(action, "developers");
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Developers_Handle(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "Developers",
                    action = "Handle",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        #endregion

        /// <summary>
        ///     开发商账号管理
        /// </summary>
        public string DevelopersAccountIndex
        {
            get { return UrlHelper.Action("index", "developersaccount"); }
        }

        /// <summary>
        ///     开发商账号管理搜索
        /// </summary>
        public string DevelopersAccount_Search(string action)
        {
            return UrlHelper.Action(action, "developersaccount");
        }

        /// <summary>
        ///     开发商账号处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public string DevelopersAccount_Handle(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "developersaccount",
                    action = "Handle",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     头像处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public string HeadPictureHandle(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "developersaccount",
                    action = "HeadPictureHandle",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     头像处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public string UpdatePhotoExpert(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "developersaccount",
                    action = "UpdatePhotoExpert",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     密码初始化处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public string PasswordHandle(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "developersaccount",
                    action = "PasswordHandle",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        //密码初始化
        public string GetNewPwd()
        {
            return UrlHelper.RouteUrl("Default", new {controller = "developersaccount", action = "GetNewPwd"});
        }

        /// <summary>
        ///     身份认证处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public string AuthenticationHandle(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "developersaccount",
                    action = "AuthenticationHandle",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     开发商账号锁定(解锁)
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public string SetLockState(int id, int state)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "developersaccount",
                    action = "locked",
                    id,
                    state,
                    random = new Random().Next(1000, 1000000)
                });
        }

        public string SetRecommend(int id, int restate)
        {
            return UrlHelper.RouteUrl("Default_ID",
              new
              {
                  controller = "developersaccount",
                  action = "Recommend",
                  id,
                  restate,
                  random = new Random().Next(1000, 1000000)
              });
        }

        #endregion

        #region 新房数据管理

        #region 房源管理

        /// <summary>
        ///     房源管理
        /// </summary>
        public string NewHouseIndex
        {
            get { return UrlHelper.Action("index", "nhouse"); }
        }

        /// <summary>
        ///     房源列表-楼盘进入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string NewHouseIndex_FromPremises(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "NHouse",
                    action = "FromPremisesIndex",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     房源列表-楼栋进入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string NewHouseIndex_FromBuilding(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "NHouse",
                    action = "FromBuildingIndex",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     房源列表
        /// </summary>
        public string NewHouse_Search(string action)
        {
            return UrlHelper.Action(action, "NHouse");
        }

        /// <summary>
        ///     添加房源
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="type">0楼盘 1 楼栋</param>
        /// <returns></returns>
        public string AddNewHouse(int id, int type)
        {
            return UrlHelper.RouteUrl("Default_ID_Type",
                new
                {
                    controller = "NHouse",
                    action = "Add",
                    id,
                    type,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     删除指定id房源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteHouseById(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "NHouse",
                    action = "DeleteHouseById",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     编辑房源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string NHouseModify(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "NHouse",
                    action = "Modify",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        #endregion

        /// <summary>
        ///     楼栋管理
        /// </summary>
        public string BuildingIndex
        {
            get { return Building_Search("index"); }
        }

        /// <summary>
        ///     楼盘管理
        /// </summary>
        public string PremisesIndex
        {
            get { return UrlHelper.Action("index", "premises"); }
        }

        /// <summary>
        ///     发布楼盘
        /// </summary>
        public string PublishPremises
        {
            get { return UrlHelper.Action("PublishPremises", "premises"); }
        }

        /// <summary>
        ///     楼栋管理(搜索)
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string Building_Search(string action)
        {
            return UrlHelper.Action(action, "nhbuilding");
        }


        /// <summary>
        ///     编辑楼盘
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public string ModifyPremises(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "premises",
                    action = "UpdatePremises",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     楼盘搜索SiteUrl
        /// </summary>
        /// <param name="action"></param>
        /// <param name="premises"></param>
        /// <returns></returns>
        public string Premises_SearchResult(string action, PVS_NH_Premises premises)
        {
            return UrlHelper.RouteUrl("premisesUrl",
                new
                {
                    action,
                    premises.ProvinceID,
                    premises.CityID,
                    premises.SalesStatus,
                    random = new Random().Next(1000, 1000000)
                });
        }

        public string Premises_action(string action)
        {
            return UrlHelper.Action(action, "premises");
        }

        /// <summary>
        ///     楼盘相册
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string PremisesImgs(int id, int type)
        {
            return UrlHelper.RouteUrl("Default_ID_Type",
                new
                {
                    controller = "premises",
                    action = "PremisesImage",
                    id,
                    type,
                    random = new Random().Next(1000, 1000000)
                });
        }

        #endregion

        #region 营销活动管理

        #region 秒杀

        /// <summary>
        ///     秒杀
        /// </summary>
        public string SecKillIndex
        {
            get { return UrlHelper.Action("index", "seckill"); }
        }

        /// <summary>
        ///     秒杀搜索
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string SecKill_Search(string action)
        {
            return UrlHelper.Action(action, "seckill");
        }

        #endregion

        #region 限时折扣

        /// <summary>
        ///     限时折扣
        /// </summary>
        public string DiscountIndex
        {
            get { return UrlHelper.Action("index", "discount", new {ActivitieState = -1}); }
        }

        /// <summary>
        ///     限时折扣搜索
        /// </summary>
        public string Discount_Search(string action)
        {
            return UrlHelper.Action(action, "discount");
        }

        #endregion

        #region 摇号管理

        /// <summary>
        ///     摇号管理列表
        /// </summary>
        /// <param name="id">0 活动管理 1审批管理</param>
        /// <returns></returns>
        public string YaoHaoIndex(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "YaoHao",
                    action = "index",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     摇号活动列表
        /// </summary>
        public string YaoHao_Search(string action)
        {
            return UrlHelper.Action(action, "YaoHao");
        }

        /// <summary>
        ///     发布摇号
        /// </summary>
        /// <param name="id">摇号活动id</param>
        /// <returns></returns>
        public string AddYaoHao(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "YaoHao",
                    action = "Add",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        public string ModifyYaoHao(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "YaoHao",
                    action = "Modify",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        public string InfoYaoHao(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "YaoHao",
                    action = "Info",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        public string DelYaoHao(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "YaoHao",
                    action = "Delete",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     报名用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string YaoHaoUsersIndex(int id, int pid)
        {
            return UrlHelper.RouteUrl("Default_ID_PId",
                new
                {
                    controller = "YaoHao",
                    action = "UsersIndex",
                    id,
                    pid,
                    random = new Random().Next(1000, 1000000)
                });
        }

        #endregion

        #region 砍价活动管理

        /// <summary>
        ///     砍价活动管理
        /// </summary>
        public string BargainActiveIndex
        {
            get { return UrlHelper.Action("index", "BargainActive"); }
        }

        /// <summary>
        ///     砍价搜索SiteUrl
        /// </summary>
        /// <param name="action">方法名</param>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public string BargainActive_SearchResult(string action, PVS_NH_Bargain search)
        {
            return UrlHelper.RouteUrl("BargainActiveUrl",
                new
                {
                    action,
                    search.Type,
                    search.ProvinceID,
                    search.CityId,
                    search.PremisesId,
                    search.BuildingId,
                    search.UnitId,
                    search.FloorId,
                    search.ActivitieState,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     根据砍价活动Id查看报名用户信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="premiseId">楼盘Id</param>
        /// <param name=" houseId">房源Id</param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public string BargainUserIndex(int id, int premiseId, int houseId)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "BargainActive",
                    action = "RegistrationUserShow",
                    id,
                    PremiseId = premiseId,
                    HouseId = houseId,
                    //types,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     砍价活动报名用户信息搜索结果
        /// </summary>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        public string BargainUser_Search(string action)
        {
            return UrlHelper.Action(action, "BargainActive");
        }

        /// <summary>
        ///     修改砍价活动
        /// </summary>
        /// <param name="id">活动Id</param>
        /// <returns></returns>
        public string UpdateBargain(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "BargainActive",
                    action = "UpdateBargain",
                    Id = id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        #endregion

        #region 一口价活动管理

        /// <summary>
        ///     一口价活动管理
        /// </summary>
        public string APriceIndex
        {
            get { return UrlHelper.Action("index", "APrice"); }
        }

        /// <summary>
        ///     一口价搜索SiteUrl
        /// </summary>
        /// <param name="action">方法名</param>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public string APrice_SearchResult(string action, PVS_NH_Bargain search)
        {
            return UrlHelper.RouteUrl("APriceUrl",
                new
                {
                    action,
                    search.Type,
                    search.ProvinceID,
                    search.CityId,
                    search.ActivitieState,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     根据一口价活动Id查看报名用户信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="houseId">房源Id</param>
        /// <returns></returns>
        public string APriceUserIndex(int id, int premisesId, int houseId)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "APrice",
                    action = "RegistrationUserShow",
                    id,
                    PremisesId = premisesId,
                    HouseId = houseId,
                    //types,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     一口价活动报名用户信息搜索结果
        /// </summary>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        public string APriceUser_Search(string action)
        {
            return UrlHelper.Action(action, "APrice");
        }

        /// <summary>
        ///     修改一口价活动
        /// </summary>
        /// <param name="id">活动Id</param>
        /// <returns></returns>
        public string UpdateAPrice(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "APrice",
                    action = "UpdateAPrice",
                    Id = id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        #endregion

        #region 阶梯团购活动管理

        /// <summary>
        ///     阶梯团购
        /// </summary>
        public string LadderGroupIndex
        {
            get { return UrlHelper.Action("index", "LadderGroup"); }
        }

        /// <summary>
        ///     动作指向
        /// </summary>
        /// <param name="actionName">方法名称</param>
        /// <param name="controllerName">路由名称</param>
        /// <returns></returns>
        public string ActionUrl(string actionName, string controllerName)
        {
            return UrlHelper.Action(actionName, controllerName);
        }

        /// <summary>
        ///     阶梯团购活动搜索SiteUrl
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string LadderGroup_SearchResult(string action)
        {
            return UrlHelper.Action(action, "LadderGroup");
        }

        /// <summary>
        ///     阶梯团购活动搜索SiteUrl
        /// </summary>
        /// <param name="action"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public string LadderGroup_SearchResult(string action, PVS_NH_LadderBuy search)
        {
            string url = UrlHelper.RouteUrl("LadderGroupUrl",
                new
                {
                    action,
                    search.Type,
                    search.ProvinceID,
                    search.CityId,
                    search.PremisesId,
                    search.ActivitieState,
                    random = new Random().Next(1000, 1000000)
                });
            return url;
        }

        /// <summary>
        ///     根据阶梯团购活动Id查看报名用户信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="types">活动类型</param>
        /// <returns></returns>
        public string RegistrationUserIndex(int id, int types)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "LadderGroup",
                    action = "RegistrationUserShow",
                    id,
                    types,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     阶梯团购活动报名用户信息搜索结果
        /// </summary>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        public string RegistrationUser_Search(string action)
        {
            return UrlHelper.Action(action, "LadderGroup");
        }

        #endregion

        #region 排号购房活动管理

        /// <summary>
        ///     排号购房
        /// </summary>
        public string PurchaseHouseIndex
        {
            get { return UrlHelper.Action("index", "PurchaseHouse"); }
        }

        /// <summary>
        ///     排号购房活动搜索SiteUrl
        /// </summary>
        /// <param name="action"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public string PurchaseHouse_SearchResult(string action, PVS_NH_LadderBuy search)
        {
            string url = UrlHelper.RouteUrl("PurchaseHouseUrl",
                new
                {
                    action,
                    search.Type,
                    search.ProvinceID,
                    search.CityId,
                    search.PremisesId,
                    search.ActivitieState,
                    random = new Random().Next(1000, 1000000)
                });
            return url;
        }

        /// <summary>
        ///     排号购房活动搜索SiteUrl
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string PurchaseHouse_SearchResult(string action)
        {
            return UrlHelper.Action(action, "PurchaseHouse");
        }

        /// <summary>
        ///     根据排号购房活动Id查看报名用户信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="types">活动类型</param>
        /// <returns></returns>
        public string RegistUserIndex(int id, int types)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "PurchaseHouse",
                    action = "RegistrationUserShow",
                    id,
                    types,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     排号购房活动报名用户信息搜索结果
        /// </summary>
        /// <param name="action">方法名</param>
        /// <returns></returns>
        public string RegistUser_Search(string action)
        {
            return UrlHelper.Action(action, "PurchaseHouse");
        }

        #endregion

        #region 竞价活动

        /// <summary>
        ///     竞价活动
        /// </summary>
        public string NhBidingIndex
        {
            get { return UrlHelper.Action("index", "NhBiding"); }
        }

        /// <summary>
        ///     竞价搜索
        /// </summary>
        public string NhBiding_Search(PVS_NH_Biding search, string action)
        {
            return UrlHelper.RouteUrl("NhBidingSearchUrl",
                new
                {
                    action,
                    search.ProvinceId,
                    search.CityId,
                    search.PremisesId,
                    search.BuildingId,
                    search.UnitId,
                    search.Floor,
                    search.ActivitieState,
                    random = new Random().Next(1000, 1000000)
                });
        }

        public string DelNhBiding(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "NhBiding",
                    action = "Delete",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     竞价用户列表
        /// </summary>
        public string NhBidingUser_Search(string action)
        {
            return UrlHelper.Action(action, "NhBiding");
        }

        /// <summary>
        ///     报名用户列表
        /// </summary>
        /// <param name="id">活动id</param>
        /// <param name="pid">楼盘id</param>
        /// <param name="hid">房源id</param>
        /// <returns></returns>
        public string NhBidingUsersIndex(int id, int pid, int hid)
        {
            return UrlHelper.RouteUrl("Default_ID_PId_HId",
                new
                {
                    controller = "NhBiding",
                    action = "UserIndex",
                    id,
                    pid,
                    hid,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     修改竞价信息
        /// </summary>
        /// <param name="id">活动id </param>
        /// <returns></returns>
        public string NhBidingModify(int id)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    controller = "NhBiding",
                    action = "Modify",
                    id,
                    random = new Random().Next(1000, 1000000)
                });
        }

        /// <summary>
        ///     竞价用户列表导出
        /// </summary>
        public string NhBidingUserOutPut(string action)
        {
            return UrlHelper.Action(action, "NhBiding");
        }

        #endregion

        #endregion

        #region 新房广告管理

        /// <summary>
        /// 新房广告管理
        /// </summary>
        public string NhAdvertise_Index
        {
            get { return UrlHelper.Action("index", "nhadvertise"); }
        }

        #endregion

        #region 财务管理

        #region 提现管理

        /// <summary>
        ///     开发商 提现管理
        /// </summary>
        /// <param name="action"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public string NhWithdrawCash(string action, PVS_NH_WithdrawCash search = null)
        {
            return UrlHelper.Action(action, "NhWithdrawCash");
        }

        /// <summary>
        ///     处理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="userId"></param>
        /// <param name="price"></param>
        /// <param name="currentStatus"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public string HandleWithdrawCash(int id, int status, int userId, decimal price, int currentStatus, int userType)
        {
            return UrlHelper.RouteUrl("Default_ID",
                new
                {
                    action = "HandleWithdrawCash",
                    controller = "NhWithdrawCash",
                    id,
                    status,
                    userId,
                    price,
                    currentStatus,
                    userType,
                    random = new Random().Next(1, 999999999)
                });
        }

        #endregion

        #region 保证金

        /// <summary>
        ///     开发商 提现管理
        /// </summary>
        /// <param name="action"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public string NhGuarantee(string action, PVS_NH_WithdrawCash search = null)
        {
            return UrlHelper.Action(action, "NhGuarantee");
        }

        #endregion

        #endregion

        #region 财务管理(二手房、租房)

        /// <summary>
        ///     待审批
        /// </summary>
        public string WaitApplyIndex
        {
            get { return string.Format("{0}WithdrawCash/Index.html", Auxiliary.Instance.ManagerUrl); } // UrlHelper.Action("Index", "WithdrawCash"); }
        }

        /// <summary>
        ///     审批通过待打款
        /// </summary>
        public string WaitPayMoneyIndex
        {
            get { return string.Format("{0}WithdrawCash/WaitPayMoney.html", Auxiliary.Instance.ManagerUrl); } // UrlHelper.Action("WaitPayMoney", "WithdrawCash"); }
        }

        /// <summary>
        ///     审批未通过
        /// </summary>
        public string ApplyNoPassIndex
        {
            get { return string.Format("{0}WithdrawCash/ApplyNoPass.html", Auxiliary.Instance.ManagerUrl); } // return UrlHelper.Action("ApplyNoPass", "WithdrawCash"); }
        }

        /// <summary>
        ///     打款成功
        /// </summary>
        public string PayMoneySuccessIndex
        {
            get { return string.Format("{0}WithdrawCash/PayMoneySuccess.html", Auxiliary.Instance.ManagerUrl); } // return UrlHelper.Action("PayMoneySuccess", "WithdrawCash"); }
        }

        /// <summary>
        ///     打款失败
        /// </summary>
        public string PayMoneyFailIndex
        {
            get { return string.Format("{0}WithdrawCash/PayMoneyFail.html", Auxiliary.Instance.ManagerUrl); } // return UrlHelper.Action("PayMoneyFail", "WithdrawCash"); }
        }

        /// <summary>
        ///     投资记录主页
        /// </summary>
        public string InvestmentIndex
        {
            get { return string.Format("{0}Investment/index.html", Auxiliary.Instance.ManagerUrl); } //return UrlHelper.Action("index", "Investment"); }
        }

        /// <summary>
        ///     个人充值记录
        ///     Author:李雨钊
        /// </summary>
        public string Gold_PersonalRecharge()
        {
            return string.Format("{0}gold/personalrecharge.html", Auxiliary.Instance.ManagerUrl); //UrlHelper.RouteUrl("Default", new { controller = "gold", action = "personalrecharge" });
        }

        /// <summary>
        ///     经纪人充值记录
        ///     Author:李雨钊
        /// </summary>
        public string Gold_BrokerRecharge()
        {
            return string.Format("{0}gold/brokerrecharge.html", Auxiliary.Instance.ManagerUrl); //return UrlHelper.RouteUrl("Default", new { controller = "gold", action = "brokerrecharge" });
        }

        /// <summary>
        ///     平台赠送记录
        ///     Author:李雨钊
        /// </summary>
        public string Gold_PaltformGive()
        {
            return string.Format("{0}gold/platformgive.html", Auxiliary.Instance.ManagerUrl); //UrlHelper.RouteUrl("Default", new { controller = "gold", action = "platformgive" });
        }

        /// <summary>
        ///     保证金管理
        ///     Author: 李雨钊
        /// </summary>
        /// <returns></returns>
        public string Guarantee_Index()
        {
            return string.Format("{0}guarantee/index.html", Auxiliary.Instance.ManagerUrl); // UrlHelper.RouteUrl("Default", new { controller = "guarantee", action = "index" });
        }

        /// <summary>
        ///     保证金管理
        ///     Author: 李雨钊
        /// </summary>
        /// <returns></returns>
        public string Sincerity_Index()
        {
            return string.Format("{0}sincerity/index.html", Auxiliary.Instance.ManagerUrl); // UrlHelper.RouteUrl("Default", new { controller = "sincerity", action = "index" });
        }

        /// <summary>
        ///     出价信息费
        ///     Author: 李雨钊
        /// </summary>
        /// <returns></returns>
        public string BidFee_Index()
        {
            return string.Format("{0}bidfee/index.html", Auxiliary.Instance.ManagerUrl); // UrlHelper.RouteUrl("Default", new { controller = "bidfee", action = "index" });
        }

        #endregion
    }
}