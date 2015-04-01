using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.NHouseActivies.APrice;
using TXBll.NHouseActivies.SecKill;
using TXBll.WebSite;
using TXCommons.Admins;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.Activities.Aprice)]
    public class APriceController : BaseController
    {
        #region 一口价活动列表

        /// <summary>
        /// 一口价活动列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var geographyBll = new GeographyBll();
            var search = new PVS_NH_Bargain();
            search.Type = 8; //活动类型

            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem {Text = "省", Value = "0"});

            search.Cityes = (0 == search.ProvinceID ? new List<SelectListItem>() : Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceID));
            search.Cityes.Insert(0, new SelectListItem {Text = "市", Value = "0"});

            search.Premises = (0 == search.CityId ? new List<SelectListItem>() : geographyBll.Z_GetPremisesById(search.CityId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Premises.Insert(0, new SelectListItem {Text = "选择楼盘", Value = "0"});

            search.Buildings = (0 == search.PremisesId ? new List<SelectListItem>() : geographyBll.Z_GetBuildings(search.PremisesId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Buildings.Insert(0, new SelectListItem {Text = "选择楼栋", Value = "0"});

            search.Units = (0 == search.BuildingId ? new List<SelectListItem>() : geographyBll.Z_GetUnits(search.BuildingId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Units.Insert(0, new SelectListItem {Text = "选择单元", Value = "0"});

            search.Floors = (0 == search.BuildingId ? new List<SelectListItem>() : geographyBll.Z_GetBuildingFloors(search.BuildingId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Floors.Insert(0, new SelectListItem {Text = "选择楼层", Value = "0"});

            search.ActivitieState = -1;
            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();

            return View("Index", search);
        }

        #endregion

        #region 列表搜索

        /// <summary>
        /// 列表搜索
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult Search(PVS_NH_Bargain search)
        {
            var geographyBll = new GeographyBll();

            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem {Text = "省", Value = "0"});

            search.Cityes = (0 == search.ProvinceID ? new List<SelectListItem>() : Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceID));
            search.Cityes.Insert(0, new SelectListItem {Text = "市", Value = "0"});

            search.Premises = (0 == search.CityId ? new List<SelectListItem>() : geographyBll.Z_GetPremisesById(search.CityId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Premises.Insert(0, new SelectListItem {Text = "选择楼盘", Value = "0"});

            search.Buildings = (0 == search.PremisesId ? new List<SelectListItem>() : geographyBll.Z_GetBuildings(search.PremisesId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Buildings.Insert(0, new SelectListItem {Text = "选择楼栋", Value = "0"});

            search.Units = (0 == search.BuildingId ? new List<SelectListItem>() : geographyBll.Z_GetUnits(search.BuildingId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Units.Insert(0, new SelectListItem {Text = "选择单元", Value = "0"});

            search.Floors = (0 == search.BuildingId ? new List<SelectListItem>() : geographyBll.Z_GetBuildingFloors(search.BuildingId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Floors.Insert(0, new SelectListItem {Text = "选择楼层", Value = "0"});

            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();

            search.DeveloperName = Server.UrlEncode(search.DeveloperName);
            CurrentPageIndex();
            return View("index", search);
        }

        #endregion

        #region 集合列表

        /// <summary>
        /// 集合列表
        /// </summary>
        /// <param name="search">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_Bargain search, int pageIndex, int pageSize)
        {
            var ApriceBll = new APrice_AdminBll();

            pageIndex = pageIndex + 1;

            var list = ApriceBll.GetAPriceListByPages(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_APrice", list);
        }

        #endregion

        #region 结果总记录数

        /// <summary>
        /// 结果总记录数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_Bargain search)
        {
            var ApriceBll = new APrice_AdminBll();
            int count = ApriceBll.GetAPriceByPageCounts(search);
            return Json(new {result = count});
        }

        #endregion

        #region 报名用户列表

        /// <summary>
        /// 报名用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistrationUserIndex()
        {
            return View();
        }

        public ActionResult RegistrationUserShow(int id, int premisesId, int houseId)
        {
            var _premisesbll = new PremisesBll();
            var premises = _premisesbll.GetPremisesbyId(premisesId);
            var pvs = new PVS_NH_RegistrationUser
            {
                Id = id,
                PremisesId = premisesId,
                PremisesName = premises.Name,
                HouseId = houseId
            };
            ViewData["GetHouseInfos"] = GetHouseInfos(id);
            return View("RegistrationUserIndex", pvs);
        }

        #endregion

        #region 报名用户搜索及结果信息

        /// <summary>
        /// 报名用户搜索
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public ActionResult RegistrationUserSearch(PVS_NH_RegistrationUser search)
        {
            var _premisesbll = new PremisesBll();
            var premises = _premisesbll.GetPremisesbyId(search.PremisesId);
            search.PremisesName = premises.Name;
            return View("RegistrationUserIndex", search);
        }

        /// <summary>
        /// 集合信息
        /// </summary>
        /// <param name="search">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public ActionResult RegistrationUserSearchResult(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            var ApriceBll = new APrice_AdminBll();

            pageIndex = pageIndex + 1;

            var list = ApriceBll.GetUsersListByPages(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_APriceUserList", list);

        }

        /// <summary>
        /// 结果记录数
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public ActionResult RegistrationUserSearchCount(PVS_NH_RegistrationUser search)
        {
            var ApriceBll = new APrice_AdminBll();
            int count = ApriceBll.GetUsersListByPageCounts(search);
            return Json(new {result = count});

        }

        /// <summary>
        /// 根据活动Id获取房源信息
        /// </summary>
        /// <param name="activeId"></param>
        /// <returns></returns>
        public string GetHouseInfos(int activeId)
        {
            var _secKillBll = new SecKillBll();

            var list = new List<PVM_NH_SecKill_UserReport>();

            var houses = _secKillBll.GetHouseInfoByActivityId(activeId);

            string houseInfo = string.Empty;

            if (null != houses && 0 < houses.Count)
            {
                var house = houses[0];
                houseInfo = string.Format("{0} {1}{2} {3} {4}层 {5}房-一口价活动报名用户列表", house.PremiseName, house.BuildingName, house.BuildingNameType, house.UnitName, house.FloorString, house.HouseName);
            }

            return houseInfo;
        }
        #endregion

        #region 导出报名用户列表

        /// <summary>
        /// 导出报名用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExcel(PVS_NH_RegistrationUser search)
        {
            var ApriceBll = new APrice_AdminBll();
            var list = ApriceBll.GetOutPutRegistUsersByList(search);

            var titlestr = new[]
            {

                "序号",
                "出价人",
                "真实姓名",
                "身份证号",
                "手机号",
                "QQ号码",
                "E-mail",
                "出价时间"


            };
            var listResult = list.ConvertAll(it => new OutputExcel()
            {
                CellText1 = it.Id.ToString(CultureInfo.InvariantCulture),
                CellText2 = it.BidUserName,
                CellText3 = it.BidRealName,
                CellText4 = it.IDCard,
                CellText5 = it.BidUserMobile,
                CellText6 = it.BidUserQQ,
                CellText7 = it.BidUserMail,
                CellText8 = string.Format("{0:yyyy-MM-dd HH:mm}", it.CreateTime)


            });
            const string fileName = "活动名称-一口价活动报名用户列表";
            return new ExcelResult<OutputExcel>(listResult, fileName, titlestr);
        }

        #endregion

        #region 修改一口价活动

        /// <summary>
        /// 修改一口价活动
        /// </summary>
        /// <param name="activeId">活动Id</param>
        /// <returns></returns>
        public ActionResult UpdateAPrice(int Id)
        {
            var ApriceBll = new APrice_AdminBll();
            var _geographyBll = new GeographyBll();
            PVM_NH_BargainActive model = ApriceBll.GetAPriceActiviesInfo(Id);
            //楼盘
            var premises = _geographyBll.Z_GetPremises().ConvertAll(it => new SelectListItem
            {
                Text = it.GeographyName,
                Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            });
            if (premises.Count > 0)
                model.Premises = premises;
            else
                model.Premises.Insert(0, new SelectListItem {Text = "选择楼盘", Value = "0"});
            //楼栋
            if (model.PremisesId > 0)
            {
                var buildings = _geographyBll.Z_GetBuildings(model.PremisesId).ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
                if (buildings.Count > 0)
                    model.Buildings = buildings;
                else
                    model.Buildings.Insert(0, new SelectListItem {Text = "选择楼栋", Value = "0"});
            }
            //单元
            if (model.BuildingId > 0)
            {
                var units = _geographyBll.Z_GetUnits(model.BuildingId).ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
                if (units.Count > 0)
                    model.Units = units;
                else
                    model.Units.Insert(0, new SelectListItem {Text = "选择单元", Value = "0"});
            }
            //楼层
            if (model.BuildingId > 0)
            {
                var buildingFloors = _geographyBll.Z_GetBuildingFloors(model.BuildingId).ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
                if (buildingFloors.Count > 0)
                    model.Floors = buildingFloors;
                else
                    model.Floors.Insert(0, new SelectListItem {Text = "选择楼层", Value = "0"});

            }
            model.SalesStatuss = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus();
            CurrentPageIndex();
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().APriceIndex : Request.Params["backurl"];
            return View(model);
        }

        #endregion

        #region 保存修改数据

        /// <summary>
        /// 保存修改数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Modify(PVM_NH_Biding model)
        {
            var _APriceBll = new APrice_AdminBll();
            var _houseBll = new HouseBll();
            var house = _houseBll.GetEntity_ById(model.HouseId);
            if (house == null)
                return Json(new {result = false, message = "所选房源信息不存在！"});
            model.PremisesId = house.PremisesId;
            model.BuildingId = house.BuildingId;
            model.UnitId = house.UnitId;
            model.Floor = house.Floor;
            model.DeveloperId = house.DeveloperId;
            model.EndTime = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd 23:59:59}", model.EndTime));

            model.ActivitieState = new SecKillBll().GetEntity_ById(model.ActivitiesId).ActivitieState;
            if (Auxiliary.Instance.IsActivityStartInHour(model.BeginTime))
            {
                model.ActivitieState = 1;
            }

            var result = _APriceBll.UpdateAPrice_Admin(model);
            if (result.Code == "1")
            {
                var list = new SecKillBll().GetActivityPremisesInfoList(model.ActivitiesId);

                foreach (var ah in list)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", ah.PremisesId, ah.CityId);
                }

                int adminId = Convert.ToInt32(Request["adminId"]);
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = adminId,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-一口价活动管理-修改活动编号[{0}]", model.ActivitiesId),
                    Type = 0
                });
                return Json(new {result = true, message = "修改成功。"});
            }

            return Json(new {result = false, message = result.Msg});
        }

        #endregion

        #region 房源数据

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <param name="search">搜索实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public ActionResult HouseSearchResult(PVS_NH_Biding_House search, int pageIndex, int pageSize)
        {
            ViewData["HouseId"] = string.IsNullOrWhiteSpace(Request["HouseId"]) ? "0" : Request["HouseId"];
            var ApriceBll = new APrice_AdminBll();
            pageIndex = pageIndex + 1;
            var list = ApriceBll.GetAPriceHousePageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_BargainActiveHouse", list);

        }

        /// <summary>
        /// 结果数量
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult HouseSearchCount(PVS_NH_Biding_House search)
        {
            var ApriceBll = new APrice_AdminBll();
            int count = ApriceBll.GetAPriceHousePageCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #region 删除活动

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="id">活动Id</param>
        /// <returns></returns>
        public ActionResult Delete(int id, int adminId)
        {
            var ApriceBll = new APrice_AdminBll();
            var activity = ApriceBll.GetEntity_ById(id);
            var result = ApriceBll.DeleteActive(id);
            if (result.Code == "1")
            {
                // 站内信
                var developer = (new DevelopersBll().GetEntity_ById(Convert.ToInt32(activity.DeveloperId))) as Developer;
                if (0 < Convert.ToInt32(activity.DeveloperId) && null != developer)
                {
                    var list = new SecKillBll().GetActivityPremisesInfoList(id);

                    foreach (var ah in list)
                    {
                        TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", ah.PremisesId, ah.CityId);
                    }

                    new DeveloperMessageBll().Add(new DeveloperMessage
                    {
                        SendUserId = 0,
                        ReceiveUserId = Convert.ToInt32(activity.DeveloperId),
                        Title = "删除活动(一口价)",
                        Content = string.Format(@"尊敬的{0}，您所创建的{1}被删除，请如有问题请致电快有家客户服务热线：{2}。", developer.LoginName, activity.Name, Auxiliary.Instance.NhServiceHotLine1)
                    });
                }

                // 操作日志
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = adminId,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-一口价活动管理-删除活动编号[{0}]", id),
                    Type = 0
                });
                return Json(new {result = true, message = "操作成功。"});
            }

            return Json(new {result = false, message = result.Msg});
        }

        #endregion

        #region 返回当前页码

        /// <summary>
        /// 返回当前页码
        /// </summary>
        private void CurrentPageIndex()
        {
            int pageIndex;
            if (!int.TryParse(Convert.ToString(Request.Params["pageIndex"]), out pageIndex))
            {
                pageIndex = -1;
            }
            ViewData["CurrentPageIndex"] = pageIndex;
        }

        #endregion
    }
}