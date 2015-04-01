using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TXBll.Dev;
using TXBll.NHouseActivies.SecKill;
using TXCommons.Admins;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.Activities.SecKill)]
    public class SecKillController : BaseController
    {
        private SecKillBll _secKillBll;

        /// <summary>
        /// 秒杀
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PVS_NH_SecKill search, int pageindex = 0)
        {
            if (null == search)
            {
                search = new PVS_NH_SecKill();
                search.ActivitieState = -1;
            }

            ViewData["CurrentPageIndex"] = pageindex;

            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem {Text = "省", Value = "0"});

            if (null == search.Cities)
            {
                search.Cities = new List<SelectListItem>();
            }

            if (0 < search.ProvinceId)
            {
                search.Cities = Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceId);
            }
            search.Cities.Insert(0, new SelectListItem {Text = "市", Value = "0"});

            search.Premisess = search.Premisess ?? new List<SelectListItem>();
            search.Premisess.Insert(0, new SelectListItem {Text = "选择楼盘", Value = "0"});

            search.Buildings = search.Buildings ?? new List<SelectListItem>();
            search.Buildings.Insert(0, new SelectListItem {Text = "选择楼栋", Value = "0"});

            search.Units = search.Units ?? new List<SelectListItem>();
            search.Units.Insert(0, new SelectListItem {Text = "选择单元", Value = "0"});

            search.Floors = search.Floors ?? new List<SelectListItem>();
            search.Floors.Insert(0, new SelectListItem {Text = "选择楼层", Value = "0"});

            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();
            search.ActivitieStates[0].Text = "选择活动状态";

            return View(search);
        }

        /// <summary>
        /// 开发商 联想输入
        /// </summary>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult AutoCmpDeveloper(int provinceId, int cityId, string name)
        {
            var list = new SecKillBll().GetList_ByProvinceIdCityIdDeveloperAgent(provinceId, cityId, name);

            return Json(new {suc = true, list});
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public ActionResult Search(PVS_NH_SecKill search, int pageindex = 0)
        {
            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem {Text = "省", Value = "0"});

            ViewData["CurrentPageIndex"] = pageindex;

            if (null == search.Cities)
            {
                search.Cities = new List<SelectListItem>();
            }

            if (0 < search.ProvinceId)
            {
                search.Cities = Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceId);
            }
            search.Cities.Insert(0, new SelectListItem {Text = "市", Value = "0"});

            search.Premisess = Auxiliary.Instance.GetPremisesByProvinceOrCityOrDeveloperId(search.ProvinceId, search.CityId, search.DeveloperId);
            search.Premisess = search.Premisess ?? new List<SelectListItem>();
            search.Premisess.Insert(0, new SelectListItem {Text = "选择楼盘", Value = "0"});

            if (0 < search.PremisesId)
            {
                search.Buildings = Auxiliary.Instance.GetBuildingsByPremisesId(search.PremisesId);
            }
            else
            {
                search.Buildings = new List<SelectListItem>();
            }
            search.Buildings.Insert(0, new SelectListItem {Text = "选择楼栋", Value = "0"});

            if (0 < search.BuildingId)
            {
                search.Units = Auxiliary.Instance.GetUnitsByBuildingId(search.BuildingId);
                search.Floors = Auxiliary.Instance.GetFloorsByBuildingId(search.BuildingId);
            }
            else
            {
                search.Units = new List<SelectListItem>();
                search.Floors = new List<SelectListItem>();
            }
            search.Units.Insert(0, new SelectListItem {Text = "选择单元", Value = "0"});
            search.Floors.Insert(0, new SelectListItem {Text = "选择楼层", Value = "0"});

            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();
            search.ActivitieStates[0].Text = "选择活动状态";

            return View("Index", search);
        }

        /// <summary>
        /// 检索数据结果
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_SecKill search, int pageIndex = 0, int pageSize = 20)
        {
            _secKillBll = new SecKillBll();

            pageIndex += 1;
            var list = _secKillBll.GetPageList_BySearch_Admin(search, pageIndex, pageSize);

            return PartialView("PageTables/Activities/_SecKill", list);
        }

        /// <summary>
        /// 搜索数据数量
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult SearchCount(PVS_NH_SecKill search)
        {
            _secKillBll = new SecKillBll();

            int count = _secKillBll.GetTotalCount_BySearch_Admin(search);

            return Json(new {result = count});
        }

        /// <summary>
        /// 用户报名
        /// </summary>
        /// <param name="activeId"></param>
        /// <returns></returns>
        public ActionResult UserReportList(int activeId)
        {
            _secKillBll = new SecKillBll();

            var list = new List<PVM_NH_SecKill_UserReport>();

            var houses = _secKillBll.GetHouseInfoByActivityId(activeId);

            string houseInfo = string.Empty;

            if (null != houses && 0 < houses.Count)
            {
                var house = houses[0];
                houseInfo = string.Format("{0} {1}{2} {3} {4}层 {5}房-秒杀活动报名用户列表", house.PremiseName, house.BuildingName, house.BuildingNameType, house.UnitName, house.FloorString, house.HouseName);
                list = _secKillBll.GetUserReportsByActivityId(activeId, house.HouseId);
            }

            ViewData["HouseInfo"] = houseInfo;
            ViewData["ActivityId"] = activeId;

            return View(list);
        }

        #region 导出数据

        /// <summary>
        /// 导出excel
        /// author:liyuzhao
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExcel(int activeId)
        {
            _secKillBll = new SecKillBll();
            var list = new List<PVM_NH_SecKill_UserReport>();
            var houses = _secKillBll.GetHouseInfoByActivityId(activeId);

            #region 标题

            string[] titlestr =
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

            #endregion

            // string houseInfo = string.Empty;

            if (null != houses && 0 < houses.Count)
            {
                var house = houses[0];
                // houseInfo = string.Format("{0} {1}{2} {3} {4}层 {5}房-秒杀活动报名用户列表", house.PremiseName, house.BuildingName, house.BuildingNameType, house.UnitName, house.FloorString, house.HouseName);
                list = _secKillBll.GetUserReportsByActivityId(activeId, house.HouseId);
            }

            var listResult = list.ConvertAll(it => new OutputExcel()
            {
                CellText1 = Convert.ToString(it.Id),
                CellText2 = it.BidUserName,
                CellText3 = it.BidRealName,
                CellText4 = it.IDCard,
                CellText5 = it.BidUserMobile,
                CellText6 = it.BidUserQQ,
                CellText7 = it.BidUserMail,
                CellText8 = it.BidTimeString
            });
            const string fileName = "秒杀活动报名用户列表";
            return new ExcelResult<OutputExcel>(listResult, fileName, titlestr);
        }

        #endregion

        /// <summary>
        /// 根据活动编号删除活动信息
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public JsonResult DelByActivityId(int activityId)
        {
            _secKillBll = new SecKillBll();

            var activity = new SecKillBll().GetEntity_ById(activityId);
            if (_secKillBll.DelByActivityId(activityId))
            {
                // 发送站内信
                var developer = (new DevelopersBll().GetEntity_ById(Convert.ToInt32(activity.DeveloperId))) as Developer;
                if (0 < Convert.ToInt32(activity.DeveloperId) && null != developer)
                {
                    var list = _secKillBll.GetActivityPremisesInfoList(activityId);

                    foreach (var ah in list)
                    {
                        TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", ah.PremisesId, ah.CityId);
                    }

                    new DeveloperMessageBll().Add(new DeveloperMessage
                    {
                        SendUserId = 0,
                        ReceiveUserId = Convert.ToInt32(activity.DeveloperId),
                        Title = "删除活动(秒杀)",
                        Content = string.Format(@"尊敬的{0}，您所创建的{1}被删除，请如有问题请致电快有家客户服务热线：{2}。", developer.LoginName, activity.Name, Auxiliary.Instance.NhServiceHotLine1)
                    });
                }

                // 操作日志
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-秒杀活动管理-删除活动编号[{0}]", activityId),
                    Type = 0
                });

                return Json(new {suc = true, msg = "删除成功"});
            }

            return Json(new {suc = false, msg = "删除失败"});
        }

        /// <summary>
        /// 编辑活动信息
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <param name="backurl">返回链接</param>
        /// <returns></returns>
        public ActionResult EditActivity(int activityId, string backurl)
        {
            _secKillBll = new SecKillBll();

            if (string.IsNullOrEmpty(backurl))
            {
                backurl = Url.SiteUrl().SecKillIndex;
            }

            var houses = _secKillBll.GetHouseInfoByActivityId(activityId);

            string houseInfo = string.Empty;

            if (null != houses && 0 < houses.Count)
            {
                var house = houses[0];
                houseInfo = string.Format("{0} {1}{2} {3} {4}层 {5}房-秒杀活动", house.PremiseName, house.BuildingName, house.BuildingNameType, house.UnitName, house.FloorString, house.HouseName);
            }

            var model = new PVE_NH_SecKill_Edit();

            var activity = new SecKillBll().GetEntity_ById(activityId);

            model.PremisesId = (null == houses ? 0 : houses[0].PremisesId);
            model.Premisess = Auxiliary.Instance.GetPremises();

            model.Buildings = Auxiliary.Instance.GetBuildingsByPremisesId(model.PremisesId);
            model.Buildings.Insert(0, new SelectListItem {Text = "选择楼栋", Value = "0"});

            model.SalesStatus = -1;
            model.SalesStatusList = AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus();

            model.ActivityId = activityId;
            model.HouseId = (null == houses ? 0 : houses[0].HouseId);
            model.BidPrice = activity.BidPrice;
            model.BidPriceString = string.Format("{0:0}", model.BidPrice);
            model.Bond = activity.Bond;
            model.BondString = string.Format("{0:0}", model.Bond);

            model.BeginTime = activity.BeginTime;
            model.BeginTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", model.BeginTime);

            model.EndTime = activity.EndTime;
            model.EndTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", model.EndTime);

            ViewData["houseinfo"] = houseInfo;
            ViewData["backurl"] = backurl;

            return View(model);
        }

        /// <summary>
        /// 搜索结果
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult EditActivity_SearchResult(PVS_NH_SecKill_Edit search, int pageIndex = 0, int pageSize = 20)
        {
            _secKillBll = new SecKillBll();

            pageIndex += 1;
            var list = _secKillBll.GetPageList_BySearch_EditHouses_Admin(search, pageIndex, pageSize);

            return PartialView("PageTables/Activities/_SecKill_Edit_Houses", list);
        }

        /// <summary>
        /// 搜索数据数量
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult EditActivity_SearchCount(PVS_NH_SecKill_Edit search)
        {
            _secKillBll = new SecKillBll();

            int count = _secKillBll.GetTotalCount_BySearch_EditHouses_Admin(search);

            return Json(new {result = count});
        }

        /// <summary>
        /// 保存活动信息
        /// </summary>
        /// <param name="secKill"></param>
        /// <returns></returns>
        public JsonResult EditActivity_Save(PVE_NH_SecKill_Edit secKill)
        {
            if (null == secKill)
            {
                return Json(new {suc = false, msg = "保存失败"});
            }

            _secKillBll = new SecKillBll();

            secKill.ActivitieState = _secKillBll.GetEntity_ById(secKill.ActivityId).ActivitieState;
            if (Auxiliary.Instance.IsActivityStartInHour(secKill.BeginTime))
            {
                secKill.ActivitieState = 1;
            }

            if (_secKillBll.SaveActivityInfo(secKill))
            {
                var list = _secKillBll.GetActivityPremisesInfoList(secKill.ActivityId);

                foreach (var ah in list)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", ah.PremisesId, ah.CityId);
                }

                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-秒杀活动管理-编辑编号[{0}]", secKill.ActivityId),
                    Type = 0
                });

                return Json(new {suc = true, msg = "保存成功"});
            }

            return Json(new {suc = false, msg = "保存失败"});
        }
    }
}