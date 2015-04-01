using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.NHouseActivies.Biding;
using TXBll.NHouseActivies.SecKill;
using TXBll.WebSite;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXOrm;
using System.Collections.Generic;
using TXBll.NHouseActivies.Bargain;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.Activities.Biding)]
    public class NhBidingController : BaseController
    {
        private GeographyBll _geographyBll;
        private BidingBll _bidingBll;
        private PremisesBll _premisesbll;
        private HouseBll _houseBll;

        #region 列表入口

        public ActionResult Index()
        {
            _geographyBll = new GeographyBll();
            PVS_NH_Biding search = new PVS_NH_Biding
            {
                ActivitieState = -1,
                ActivitieStates =
                    TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates()
            };
            var provincelist = _geographyBll.Z_GetProvinces().ToList().ConvertAll(it => new SelectListItem
            {
                Text = it.GeographyName,
                Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            });
            search.Provinces.AddRange(provincelist);
            return View(search);
        }

        #endregion

        #region 列表数据搜索

        /// <summary>
        /// 列表数据
        /// author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(PVS_NH_Biding search)
        {
            _geographyBll = new GeographyBll();

            #region 回填数据

            //省份
            var provinces = _geographyBll.Z_GetProvinces().ConvertAll(it => new SelectListItem
            {
                Text = it.GeographyName,
                Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            });
            if (provinces.Count > 0)
                search.Provinces.AddRange(provinces);
            //城市
            if (search.ProvinceId > 0)
            {
                var cities = _geographyBll.Z_GetCities(search.ProvinceId).ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
                if (cities.Count > 0)
                    search.Cities.AddRange(cities);
            }
            //楼盘
            if (search.CityId > 0)
            {
                var premises = _geographyBll.Z_GetPremisesById(search.CityId).ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
                if (premises.Count > 0)
                    search.Premises.AddRange(premises);
            }
            //楼栋
            if (search.PremisesId > 0)
            {
                var buildings = _geographyBll.Z_GetBuildings(search.PremisesId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                if (buildings.Count > 0)
                    search.Buildings.AddRange(buildings);
            }
            //单元
            if (search.BuildingId > 0)
            {
                var units = _geographyBll.Z_GetUnits(search.BuildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                if (units.Count > 0)
                    search.Units.AddRange(units);
            }
            //楼层
            if (search.BuildingId > 0)
            {
                var buildingFloors = _geographyBll.Z_GetBuildingFloors(search.BuildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                if (buildingFloors.Count > 0)
                    search.Floors.AddRange(buildingFloors);

            }

            #endregion

            CurrentPageIndex();
            search.ActivitieStates = (TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates());
            search.Name = Server.UrlEncode(search.Name);
            return View("index", search);
        }

        /// <summary>
        /// 返回数据
        /// author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_Biding search, int pageIndex, int pageSize)
        {
            _bidingBll = new BidingBll();
            pageIndex = pageIndex + 1;
            var list = _bidingBll.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_NHBiding", list);
        }

        /// <summary> 
        /// 结果数量
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_Biding search)
        {
            _bidingBll = new BidingBll();
            int count = _bidingBll.GetPageCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #region 删除活动

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public ActionResult Delete(int id, int adminId)
        {
            _bidingBll = new BidingBll();
            var activity = _bidingBll.GetEntity_ById(id);
            var result = _bidingBll.DeleteBiding_Admin(id);
            if (result.Code == "1")
            {
                // 发消息
                var list = new SecKillBll().GetActivityPremisesInfoList(id);
                foreach (var ah in list)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", ah.PremisesId, ah.CityId);
                }

                // 站内信
                var developer = (new DevelopersBll().GetEntity_ById(Convert.ToInt32(activity.DeveloperId))) as Developer;
                if (0 < Convert.ToInt32(activity.DeveloperId) && null != developer)
                {
                    new DeveloperMessageBll().Add(new DeveloperMessage
                    {
                        SendUserId = 0,
                        ReceiveUserId = Convert.ToInt32(activity.DeveloperId),
                        Title = "删除活动(竞价)",
                        Content = string.Format(@"尊敬的{0}，您所创建的{1}被删除，请如有问题请致电快有家客户服务热线：{2}。", developer.LoginName, activity.Name, Auxiliary.Instance.NhServiceHotLine1)
                    });
                }

                // 操作日志
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = adminId,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-竞价活动管理-删除活动编号[{0}]", id),
                    Type = 0
                });
                return Json(new {result = true, message = "操作成功。"});
            }

            return Json(new {result = false, message = result.Msg});
        }

        #endregion

        #region 报名用户列表

        /// <summary>
        /// 报名用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserIndex(int id, int pid, int hid)
        {
            _premisesbll = new PremisesBll();
            var premises = _premisesbll.GetPremisesbyId(pid);
            PVS_NH_BidingUsers nh = new PVS_NH_BidingUsers
            {
                ActivitiesId = id,
                PremisesId = pid,
                PremisesName = premises.Name,
                HouseId = hid
            };
            ViewData["endtime"] = new BidingBll().GetEntity_ById(id).EndTime;
            ViewData["GetHouseInfos"] = GetHouseInfos(id);
            return View(nh);
        }

        /// <summary>
        /// 列表数据
        /// author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserSearch(PVS_NH_BidingUsers search)
        {
            _premisesbll = new PremisesBll();
            var premises = _premisesbll.GetPremisesbyId(search.PremisesId);
            search.PremisesName = premises.Name;
            return View("UserIndex", search);
        }

        /// <summary>
        /// 返回数据
        /// author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult UserSearchResult(PVS_NH_BidingUsers search, int pageIndex, int pageSize)
        {
            _bidingBll = new BidingBll();
            pageIndex = pageIndex + 1;
            var list = _bidingBll.GetUserPageList_BySearch_Admin(search, pageIndex, pageSize);
            var bargainBll = new Bargain_AdminBll();
            var models = bargainBll.GetMessByWhere(search.ActivitiesId);
            ViewData["models"] = models;
            return PartialView("PageTables/Activities/_NHBidingUsers", list);

        }

        /// <summary>
        /// 结果数量
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult UserSearchCount(PVS_NH_BidingUsers search)
        {
            _bidingBll = new BidingBll();
            int count = _bidingBll.GetUserPageCount_BySearch_Admin(search);
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
                houseInfo = string.Format("{0} {1}{2} {3} {4}层 {5}房-竞价活动报名用户列表", house.PremiseName, house.BuildingName, house.BuildingNameType, house.UnitName, house.FloorString, house.HouseName);
            }

            return houseInfo;
        }
        #endregion

        #region 出价用户中标/取消中标

        /// <summary>
        /// 出价用户中标/取消中标
        /// </summary>
        /// <param name="bid">出价id</param>
        /// <param name="status">状态</param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public ActionResult UpdateUserStatus(int bid, int status, int adminId)
        {
            _bidingBll = new BidingBll();
            bool b = _bidingBll.UpdateBidingStatus_Admin(bid, status);
            if (b)
            {
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = adminId,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-竞价活动管理-出价编号[{0}]修改状态=>{1}", bid, status),
                    Type = 0
                });
                return Json(new {result = true});
            }
            return Json(new {result = false, message = "操作失败！"});
        }

        #endregion

        #region 修改竞价信息

        #region 修改竞价信息 入口

        /// <summary>
        /// 修改竞价信息 入口
        /// </summary>
        /// <param name="id">活动id </param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Modify(int id)
        {
            CurrentPageIndex();
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().NhBidingIndex : Request.Params["backurl"];
            _bidingBll = new BidingBll();
            _geographyBll = new GeographyBll();
            PVE_NH_Biding model = _bidingBll.GetBidingActiviesInfo(id);
            if (model == null)
                return Content(GetPageMsg("活动信息不存在！", ViewData["backurl"].ToString()));
            //楼盘
            var premises = _geographyBll.Z_GetPremises().ConvertAll(it => new SelectListItem
            {
                Text = it.GeographyName,
                Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            });
            if (premises.Count > 0)
                model.Premises.AddRange(premises);
            //楼栋
            if (model.PremisesId > 0)
            {
                var buildings = _geographyBll.Z_GetBuildings(model.PremisesId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                if (buildings.Count > 0)
                    model.Buildings.AddRange(buildings);
            }
            //单元
            if (model.BuildingId > 0)
            {
                var units = _geographyBll.Z_GetUnits(model.BuildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                if (units.Count > 0)
                    model.Units.AddRange(units);
            }
            //楼层
            if (model.BuildingId > 0)
            {
                var buildingFloors = _geographyBll.Z_GetBuildingFloors(model.BuildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                if (buildingFloors.Count > 0)
                    model.Floors.AddRange(buildingFloors);

            }
            model.SalesStatuss = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus();

            return View(model);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Modify(PVM_NH_Biding model)
        {
            _bidingBll = new BidingBll();
            _houseBll = new HouseBll();
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
            if (Auxiliary.Instance.IsActivityStartInToday(model.BeginTime))
            {
                model.ActivitieState = 1;
            }

            var result = _bidingBll.UpdateBiding_Admin(model);
            if (result.Code == "1")
            {
                // 发消息
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
                    OperDes = string.Format("新房管理-竞价活动管理-修改活动编号[{0}]", model.ActivitiesId),
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
        /// author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult HouseSearchResult(PVS_NH_Biding_House search, int pageIndex, int pageSize)
        {
            ViewData["HouseId"] = string.IsNullOrWhiteSpace(Request["HouseId"]) ? "0" : Request["HouseId"];
            _bidingBll = new BidingBll();
            pageIndex = pageIndex + 1;
            var list = _bidingBll.GetBidingHousePageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_NHBidingHouse", list);

        }

        /// <summary>
        /// 结果数量
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult HouseSearchCount(PVS_NH_Biding_House search)
        {
            _bidingBll = new BidingBll();
            int count = _bidingBll.GetBidingHousePageCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #endregion

        #region 导出数据

        /// <summary>
        /// 导出excel
        /// author:huhangfei
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExcel(PVS_NH_BidingUsers search)
        {
            _bidingBll = new BidingBll();
            var list = _bidingBll.GetAllUserPageList_BySearch_Admin(search);

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
                "最后出价时间",
                "出价次数",
                "最后出价金额"
            };

            #endregion

            var listResult = list.ConvertAll(it => new OutputExcel
            {
                CellText1 = it.RowID.ToString(CultureInfo.InvariantCulture),
                CellText2 = it.BidUserName,
                CellText3 = it.BidRealName,
                CellText4 = it.IDCard,
                CellText5 = it.BidUserQQ,
                CellText6 = it.BidUserMobile,
                CellText7 = it.BidUserMail,
                CellText8 = it.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                CellText9 = it.BidCount.ToString(CultureInfo.InvariantCulture),
                CellText10 = Convert.ToDouble(it.BidUserPrice).ToString(CultureInfo.InvariantCulture)
            });
            const string fileName = "竞价用户列表";
            return new ExcelResult<OutputExcel>(listResult, fileName, titlestr);
        }

        #endregion

        #region help

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

        #region page Content msg

        /// <summary>
        /// page Content msg
        /// </summary>
        /// <param name="msg">提示语</param>
        /// <param name="url">返回地址url</param>
        /// <returns></returns>
        private string GetPageMsg(string msg, string url)
        {
            string html = string.Format(@"<script>
                                        alert('{0}！');
                                        window.location.href='{1}'
                                        </script>",
                msg, url);
            return html;
        }

        #endregion

        #endregion
    }
}