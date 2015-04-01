using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.WebSite;
using TXCommons.PictureModular;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXModel.Geography;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.HouseData.HouseMg)]
    public class NHouseController : BaseController
    {
        private HouseBll _houseBll;
        private BuildingBll _buildingBll;
        private PremisesBll _premisesBll;
        private GeographyBll _geographyBll;

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #region 列表入口

        /// <summary>
        /// 房源列表-楼栋进入
        /// </summary>
        /// <param name="id">楼栋编号</param>
        /// <returns></returns>
        public ActionResult FromBuildingIndex(int id)
        {
            _geographyBll = new GeographyBll();
            _buildingBll = new BuildingBll();
            _premisesBll = new PremisesBll();
            var building = _buildingBll.GetEntity_ById(id); //楼栋信息
            int premisesId = building.PremisesId; //楼盘id
            var premises = _premisesBll.GetPremises_ById(premisesId); //楼盘
            var search = new PVS_NH_House
            {
                CityId = premises.CityId,
                BuildingId = id,
                PremisesId = premisesId,
                UnitId = 0,
                Floor = 0,
                SalesStatus = -1,
                PremisesName = premises.Name //楼栋name
            };
            var buildings = _geographyBll.Z_GetBuildings(premisesId).ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            if (buildings.Count > 0)
                search.Buildings.AddRange(buildings);
            //单元
            var units = _geographyBll.Z_GetUnits(id).ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            search.Units.AddRange(units);
            //楼层
            var floors = _geographyBll.Z_GetBuildingFloors(id).ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            search.Floors.AddRange(floors);
            search.SalesStatuss = (TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus());
            return View("index", search);
        }

        /// <summary>
        /// 房源列表-楼盘进入
        /// </summary>
        /// <param name="id">楼盘编号</param>
        /// <returns></returns>
        public ActionResult FromPremisesIndex(int id)
        {
            _geographyBll = new GeographyBll();
            _premisesBll = new PremisesBll();
            var premises = _premisesBll.GetPremises_ById(id);
            var search = new PVS_NH_House
            {
                CityId = premises.CityId,
                BuildingId = 0,
                PremisesId = id,
                UnitId = 0,
                Floor = 0,
                SalesStatus = -1,
                PremisesName = premises.Name
            };
            var buildings = _geographyBll.Z_GetBuildings(id).ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            if (buildings.Count > 0)
                search.Buildings.AddRange(buildings);
            search.SalesStatuss = (TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus());
            return View("index", search);
        }

        #endregion

        #region 添加房源

        /// <summary>
        /// 添加房源
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="type">0楼盘 1 楼栋</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add(int id, int type)
        {
            _geographyBll = new GeographyBll();
            _premisesBll = new PremisesBll();
            _buildingBll = new BuildingBll();
            var entity = new PVE_NH_House();
            int premisesId = 0, buildingId = 0, developerId = 0;
            var t_premises = new Premis();
            if (type == 0)
            {
                premisesId = id;
                var premises = _premisesBll.GetPremises_ById(id);
                t_premises = premises;
                developerId = premises.DeveloperId;
                entity.SalesStatuss = GetSalesStatusListByBuildingState(0);
            }
            else if (type == 1)
            {
                var building = _buildingBll.GetEntity_ById(id);
                premisesId = building.PremisesId;
                var premises = _premisesBll.GetPremises_ById(premisesId);
                t_premises = premises;
                buildingId = id;
                developerId = building.DeveloperId;
                entity.SalesStatuss = GetSalesStatusListByBuildingState(building.State);
            }
            //楼盘
            var preSelectitem = _geographyBll.Z_GetPremises().ToList().
                ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)

                });
            //楼栋 已剔除不可发布房源的楼栋
            var buildings = _geographyBll.Z_GetBuildings_Find(premisesId).ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            if (type == 1)
            {
                //单元
                var units = _geographyBll.Z_GetUnits(buildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                entity.Units.AddRange(units);
                //楼层
                var floors = _geographyBll.Z_GetBuildingFloors(buildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                entity.Floors.AddRange(floors);

            }
            //户型图
            var list = GetPicture.GetPremisesPictureList(t_premises.InnerCode, true, PremisesPictureType.ROOMTYPE.ToString(), t_premises.CityId);
            if (list != null && list.Any())
            {
                var rlist = list.ConvertAll(it => new SelectListItem
                {
                    Text = it.Title,
                    Value = it.ID.ToString()
                });
                entity.RIds.AddRange(rlist);
            }

            entity.DeveloperId = developerId;
            entity.BuildingId = buildingId;
            entity.PremisesId = premisesId;
            entity.Premises.AddRange(preSelectitem);
            entity.Buildings.AddRange(buildings);
            entity.BuildingType = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_BuildingType();
            entity.Orientation = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_Orientation();
            //entity.SalesStatuss = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus();
            ViewData["type"] = type;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Add(House model)
        {
            _houseBll = new HouseBll();

            if (0 < _houseBll.GetHouseNameCountByIdAndName(model))
            {
                return Json(new {});
            }

            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.IsDel = false;
            model.IsRelease = true;
            //model.CoordX = 0;
            //model.CoordY = 0;
            int housid = _houseBll.AddHouse_Admin(model);
            if (housid > 0)
            {
                int adminId = Convert.ToInt32(Request["adminId"]);
                int type = Convert.ToInt32(Request["type"]);
                //发送添加消息
                TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("insert", model.PremisesId, _houseBll.GetCityIdByHouseId(housid));
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", model.PremisesId, _houseBll.GetCityIdByHouseId(housid)); //"update", model.PremisesId, housid, _houseBll.GetCityIdByHouseId(housid));
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = adminId,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-发布房源[{0}]", housid),
                    Type = 0
                });

                if (type == 0)
                    return Redirect(Url.SiteUrl().NewHouseIndex_FromPremises(model.PremisesId));
                return Redirect(Url.SiteUrl().NewHouseIndex_FromBuilding(model.BuildingId));
            }
            return Content(GetPageMsg("添加失败！", Request.UrlReferrer == null ? Url.SiteUrl().PremisesIndex : Request.UrlReferrer.ToString()));
        }

        #endregion

        #region 修改房源

        /// <summary>
        /// 修改房源
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Modify(int id)
        {
            _houseBll = new HouseBll();
            _geographyBll = new GeographyBll();
            _buildingBll = new BuildingBll();
            _premisesBll = new PremisesBll();
            var house = _houseBll.GetEntity_ById(id);
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().PremisesIndex : Request.Params["backurl"];
            var entity = new PVE_NH_House();
            if (house != null)
            {
                #region 房源数据

                var houseinfo = new PVM_NH_House
                {
                    Id = house.Id,
                    Name = house.Name,
                    InnerCode = house.InnerCode,
                    DeveloperId = house.DeveloperId,
                    PremisesId = house.PremisesId,
                    BuildingId = house.BuildingId,
                    UnitId = house.UnitId,
                    Floor = house.Floor,
                    RId = house.RId,
                    BuildingType = house.BuildingType,
                    Orientation = house.Orientation,
                    PriceType = house.PriceType,
                    TotalPrice = house.TotalPrice,
                    SinglePrice = house.SinglePrice,
                    DownPayment = house.DownPayment,
                    SalesStatus = house.SalesStatus,
                    BuildingArea = house.BuildingArea,
                    Area = house.Area,
                    Room = house.Room,
                    Hall = house.Hall,
                    Toilet = house.Toilet,
                    Kitchen = house.Kitchen,
                    //Balcony = house.Balcony,
                    CreateTime = house.CreateTime,
                    UpdateTime = house.UpdateTime,
                    IsDel = house.IsDel,
                    CoordX = house.CoordX,
                    CoordY = house.CoordY
                };

                #endregion

                int premisesId = house.PremisesId;
                int buildingId = house.BuildingId;
                int developerId = house.DeveloperId;
                var t_premises = _premisesBll.GetPremises_ById(premisesId); //楼盘
                var buildingmodel = _buildingBll.GetEntity_ById(buildingId); //楼栋

                #region  房源扩展数据（下拉列表）

                //楼盘
                var preSelectitem = _geographyBll.Z_GetPremises().ToList().
                    ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)

                    });
                //楼栋 已剔除不可发布房源的楼栋
                var buildings = _geographyBll.Z_GetBuildings_Find(premisesId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });

                //单元
                var units = _geographyBll.Z_GetUnits(buildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                entity.Units.AddRange(units);
                //楼层
                var floors = _geographyBll.Z_GetBuildingFloors(buildingId).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                entity.Floors.AddRange(floors);
                //户型图
                var list = GetPicture.GetPremisesPictureList(t_premises.InnerCode, true, PremisesPictureType.ROOMTYPE.ToString(), t_premises.CityId);
                if (list != null && list.Any())
                {
                    var rlist = list.ConvertAll(it => new SelectListItem
                    {
                        Text = it.Title,
                        Value = it.ID.ToString()
                    });
                    entity.RIds.AddRange(rlist);
                }

                entity.DeveloperId = developerId;
                entity.BuildingId = buildingId;
                entity.PremisesId = premisesId;
                entity.Premises.AddRange(preSelectitem);
                entity.Buildings.AddRange(buildings);
                entity.BuildingType =
                    TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_BuildingType();
                entity.Orientation =
                    TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_Orientation();
                entity.SalesStatuss = GetSalesStatusListByBuildingState(buildingmodel.State); //GetSalesStatusList(buildingmodel.State); //动态的房源销售状态

                #endregion

                var model = new PVME_NH_House
                {
                    BuildingId = houseinfo.BuildingId,
                    BuildingType = houseinfo.BuildingType,
                    DeveloperId = houseinfo.DeveloperId,
                    Floor = houseinfo.Floor,
                    Orientation = houseinfo.Orientation,
                    PremisesId = houseinfo.PremisesId,
                    RId = houseinfo.RId,
                    SalesStatus = houseinfo.SalesStatus,
                    UnitId = houseinfo.UnitId,
                    PveHouse = entity,
                    PvmHouse = houseinfo
                };
                return View(model);

            }
            //数据不存在
            return Content(GetPageMsg("该记录不存在", ViewData["backurl"].ToString()));
        }

        /// <summary>
        /// 修改房源
        /// </summary>
        /// <param name="house">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Modify(PVM_NH_House house)
        {
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().PremisesIndex : Request.Params["backurl"];
            _houseBll = new HouseBll();
            int coun = _houseBll.UpdateNHouse_Admin(house);
            if (coun == 1)
            {
                //发送消息
                TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("update", house.PremisesId, _houseBll.GetCityIdByHouseId(house.Id));
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", house.PremisesId, _houseBll.GetCityIdByHouseId(house.Id)); //"update", house.PremisesId, house.Id, _houseBll.GetCityIdByHouseId(house.Id));
                int adminId = Convert.ToInt32(Request["adminId"]);
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = adminId,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-修改房源[{0}]", house.Id),
                    Type = 0
                });
                return Content(GetPageMsg("修改成功", ViewData["backurl"].ToString()));
            }

            if (coun == 2)
                return Content(GetPageMsg("房源正在参与活动，不能修改", ViewData["backurl"].ToString()));
            return Content(GetPageMsg("修改失败", Url.SiteUrl().NHouseModify(house.Id) + "?backurl=" + ViewData["backurl"]));
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
        public ActionResult Search(PVS_NH_House search)
        {
            _geographyBll = new GeographyBll();
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
            CurrentPageIndex();
            search.SalesStatuss = (TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus());
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
        public ActionResult SearchResult(PVS_NH_House search, int pageIndex, int pageSize)
        {
            _houseBll = new HouseBll();
            pageIndex = pageIndex + 1;
            var list = _houseBll.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/HouseData/_nhouse", list);
        }

        /// <summary> 
        /// 结果数量
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_House search)
        {
            _houseBll = new HouseBll();
            int count = _houseBll.GetPageCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #region house delete

        /// <summary>
        /// house delete 单个删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteHouseById(int? id, int adminId)
        {
            if (id.HasValue)
            {
                try
                {
                    _houseBll = new HouseBll();
                    var notHouseList = _houseBll.CheckNotUpdateHouses(id.ToString());
                    if (notHouseList != null && notHouseList.Any())
                    {
                        //string msg = notHouseList.Aggregate(string.Empty, (current, m) => current + (m.Name + ","));
                        return Json(new {result = false, message = "房源有营销活动进行中，不能删除。"});
                    }
                    var house = _houseBll.GetEntity_ById(id.Value);
                    var premis = (new PremisesBll().GetPremises_ById(house.PremisesId));
                    var building = (new BuildingBll().GetEntity_ById(house.BuildingId));
                    var unit = (new UnitBll().GetUnit_ByBuildingIdAndNum(house.BuildingId, house.UnitId));
                    var developer = (new DevelopersBll().GetEntity_ById(house.DeveloperId)) as Developer;
                    int i = _houseBll.Del_ById(id.Value);
                    if (i > 0)
                    {
                        if (0 < house.DeveloperId)
                        {
                            // 发站内信
                            new DeveloperMessageBll().Add(new DeveloperMessage
                            {
                                SendUserId = 0,
                                ReceiveUserId = house.DeveloperId,
                                Title = "删除房源",
                                Content = string.Format(@"尊敬的{0}，{1}被删除，请如有问题请致电快有家客户服务热线：{2}。",
                                    null == developer ? string.Empty : developer.LoginName,
                                    premis.Name + building.Name + building.NameType + unit.Name + house.Name,
                                    Auxiliary.Instance.NhServiceHotLine1)
                            });
                        }

                        //发送消息
                        TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", house.PremisesId, _houseBll.GetCityIdByHouseId(id.Value));
                        TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("delete", id.Value, _houseBll.GetCityIdByHouseId(id.Value));
                        ExecuteOperResult(new Z_OperAdminLog
                        {
                            AdminID = adminId,
                            CreateTime = DateTime.Now,
                            OperIP = Request.UserHostAddress,
                            OperDes = string.Format("新房管理-单个删除房源[{0}]", id),
                            Type = 0
                        });

                        return Json(new {result = true, message = ""});
                    }
                    return Json(new {result = false, message = "操作失败。"});
                }
                catch (Exception ex)
                {
                    return Json(new {result = false, message = "操作失败：" + ex.Message});
                }

            }
            return Json(new {result = false, message = "无id信息，操作失败。"});
        }

        /// <summary>
        /// house delete 多个删除
        /// </summary>
        /// <param name="ids">编号</param>
        /// <param name="cid">城市id</param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteHouseByIds(string ids, string cid, int adminId)
        {
            if (!string.IsNullOrWhiteSpace(ids))
            {
                try
                {
                    _houseBll = new HouseBll();
                    var notHouseList = _houseBll.CheckNotUpdateHouses(ids);
                    if (notHouseList != null && notHouseList.Any())
                    {
                        string msg = notHouseList.Aggregate(string.Empty, (current, m) => current + (m.Name + ","));
                        return Json(new {result = false, message = "房号（" + msg.TrimEnd(',') + "）房源有营销活动进行中，不能删除。"});
                    }

                    var arr_ids = ids.Split(',');
                    for (int j = 0; j < arr_ids.Count(); j++)
                    {
                        var house = new HouseBll().GetEntity_ById(Convert.ToInt32(arr_ids[j]));

                        if (0 == house.DeveloperId)
                        {
                            // 没有指定开发商，不执行下列语句
                            continue;
                        }

                        var premis = new PremisesBll().GetPremises_ById(house.PremisesId);
                        var building = new BuildingBll().GetEntity_ById(house.BuildingId);
                        var unit = new UnitBll().GetUnit_ByBuildingIdAndNum(house.BuildingId, house.UnitId);
                        var developer = (new DevelopersBll().GetEntity_ById(house.DeveloperId)) as Developer;

                        // 发站内信
                        new DeveloperMessageBll().Add(new DeveloperMessage
                        {
                            SendUserId = 0,
                            ReceiveUserId = house.DeveloperId,
                            Title = "删除房源",
                            Content = string.Format(@"尊敬的{0}，{1}被删除，请如有问题请致电快有家客户服务热线：{2}。",
                                null == developer ? string.Empty : developer.LoginName,
                                premis.Name + building.Name + building.NameType + unit.Name + house.Name,
                                Auxiliary.Instance.NhServiceHotLine1)
                        });
                    }

                    //可以删除
                    int i = _houseBll.DeleteHouseByIds(ids);
                    if (i > 0)
                    {
                        //发送消息
                        foreach (string s in ids.Split(','))
                        {
                            TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", new HouseBll().GetEntity_ById(int.Parse(s)).PremisesId, int.Parse(cid));
                            TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("delete", int.Parse(s), int.Parse(cid));
                        }
                        //记录
                        ExecuteOperResult(new Z_OperAdminLog
                        {
                            AdminID = adminId,
                            CreateTime = DateTime.Now,
                            OperIP = Request.UserHostAddress,
                            OperDes = string.Format("新房管理-批量删除房源[{0}]", ids),
                            Type = 0
                        });

                        return Json(new {result = true, message = ""});
                    }

                    return Json(new {result = false, message = "操作失败。"});
                }
                catch (Exception ex)
                {
                    return Json(new {result = false, message = "操作失败：" + ex.Message});
                }

            }
            return Json(new {result = false, message = "无id信息，操作失败。"});
        }

        #endregion

        #region 房间位置标注

        /// <summary>
        /// 房间位置标注
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomMarked()
        {
            return PartialView("PageTables/HouseData/_RoomMarked", null);
        }

        #endregion

        #region 查询楼栋平面图

        /// <summary>
        /// 功能描述：查询楼栋平面图
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public ActionResult SelectBuildingPic(int premisesId, int buildingId)
        {
            _premisesBll = new PremisesBll();
            _buildingBll = new BuildingBll();
            string picId = string.Empty;
            string picSrc = string.Empty;
            var model = _premisesBll.GetPremisesbyId(premisesId);
            var buildModel = _buildingBll.GetEntity_ById(buildingId);
            if (buildModel != null)
            {
                picId = buildModel.PictureData;
            }
            if (model != null)
            {
                List<PremisesPictureInfo> list = GetPicture.GetPremisesPictureList(model.InnerCode, true, PremisesPictureType.Plane.ToString(), model.CityId);
                foreach (var item in list)
                {
                    if (picId == item.ID.ToString(CultureInfo.InvariantCulture))
                    {
                        picSrc = item.Path;
                    }
                }
            }
            return Json(picSrc, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region 更新多个房源销售状态

        /// <summary>
        /// 更新多个房源销售状态
        /// </summary>
        /// <param name="ids">编号</param>
        /// <param name="cid"></param>
        /// <param name="state"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateHouseSalesStatusByIds(string ids, string cid, int state, int adminId)
        {
            if (!string.IsNullOrWhiteSpace(ids))
            {
                try
                {
                    _houseBll = new HouseBll();
                    var notHouseList = _houseBll.CheckNotUpdateHouses(ids);
                    if (notHouseList != null && notHouseList.Any())
                    {
                        string msg = notHouseList.Aggregate(string.Empty, (current, m) => current + (m.Name + ","));
                        return Json(new {result = false, message = "房号（" + msg.TrimEnd(',') + "）正在参加活动，状态不能进行修改。"});
                    }
                    if (notHouseList != null && !notHouseList.Any())
                    {
                        #region 可以更新

                        int i = _houseBll.UpdateHouseSalesStatusByIds(ids, state);
                        if (i > 0)
                        {
                            //发送消息
                            foreach (string s in ids.Split(','))
                            {
                                TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("update", new HouseBll().GetEntity_ById(int.Parse(s)).PremisesId, int.Parse(cid));
                            }
                            ExecuteOperResult(new Z_OperAdminLog
                            {
                                AdminID = adminId,
                                CreateTime = DateTime.Now,
                                OperIP = Request.UserHostAddress,
                                OperDes = string.Format("新房管理-批量修改房源销售状态房源ids[{0}],state[{1}]", ids, state),
                                Type = 0
                            });

                            return Json(new {result = true, message = ""});
                        }


                        return Json(new {result = false, message = "操作失败。"});

                        #endregion
                    }
                    return Json(new {result = false, message = "操作失败。"});
                }
                catch (Exception ex)
                {
                    return Json(new {result = false, message = "操作失败：" + ex.Message});
                }

            }
            return Json(new {result = false, message = "无id信息，操作失败。"});
        }

        #endregion

        #region ajax 根据楼栋ID获取销售状态

        /// <summary>
        ///  ajax 根据楼栋ID获取销售状态
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns>Id和名称列表</returns>
        public JsonResult AjaxGetSalesStatus(int id)
        {
            try
            {
                _buildingBll = new BuildingBll();
                var building = _buildingBll.GetEntity_ById(id);
                var units = GetSalesStatusList(building.State).Where(it => it.Value != "-1").ToList().ConvertAll(it => new Z_GeographyItem
                {
                    GeographyCode = int.Parse(it.Value),
                    GeographyName = it.Text
                });
                return Json(new {success = true, items = units}, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region ajax 根据楼盘id获取最新户型图

        /// <summary>
        ///  ajax 根据楼盘id获取最新户型图
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public JsonResult AjaxGetRIds(int id)
        {
            try
            {
                _premisesBll = new PremisesBll();
                var tPremises = _premisesBll.GetPremises_ById(id);
                //户型图
                List<PremisesPictureInfo> list = GetPicture.GetPremisesPictureList(tPremises.InnerCode, true, PremisesPictureType.ROOMTYPE.ToString(), tPremises.CityId);
                if (list != null && list.Any())
                    return Json(new {success = true, items = list}, JsonRequestBehavior.AllowGet);
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region  HELP

        #region  取的对应状态下的房源销售 状态

        /// <summary>
        /// 取的对应状态下的房源销售 状态
        /// </summary>
        /// <param name="state">1待售 2在售3售罄4 建设中5 规划中6开发商保留</param>
        /// <returns></returns>
        public List<SelectListItem> GetSalesStatusList(int state)
        {
            var list = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus();
            if (state == 2) //在售
                return list;
            if (state == 1) //待售
                return list.Where(it => it.Value == "-1" || it.Value == "0" || it.Value == Convert.ToString(state)).ToList();
            if (state == 3) //售罄
                return list.Where(it => it.Value == "-1" || it.Value == "9" || it.Value == Convert.ToString(state)).ToList();
            if (state == 6)
                return list.Where(it => it.Value == "-1" || it.Value == "1" || it.Value == Convert.ToString(state)).ToList();
            return list.Where(it => it.Value == "-1").ToList();

        }

        public List<SelectListItem> GetSalesStatusListByBuildingState(int state)
        {
            var list = new List<SelectListItem> {new SelectListItem {Value = "0", Text = "请选择"}};
            switch (state)
            {
                    // 待售
                case 1:
                    list.Add(new SelectListItem {Value = "0", Text = "待售"});
                    break;
                    // 在售
                case 2:
                    list.Add(new SelectListItem {Value = "0", Text = "待售"});
                    list.Add(new SelectListItem {Value = "1", Text = "开发商保留"});
                    list.Add(new SelectListItem {Value = "2", Text = "在售"});
                    list.Add(new SelectListItem {Value = "3", Text = "已认购"});
                    list.Add(new SelectListItem {Value = "4", Text = "已签约"});
                    list.Add(new SelectListItem {Value = "5", Text = "已备案"});
                    list.Add(new SelectListItem {Value = "6", Text = "已办产权"});
                    list.Add(new SelectListItem {Value = "7", Text = "被限制"});
                    list.Add(new SelectListItem {Value = "8", Text = "拆迁安置"});
                    list.Add(new SelectListItem {Value = "9", Text = "售罄"});
                    break;
                    // 售罄
                case 3:
                    list.Add(new SelectListItem {Value = "9", Text = "售罄"});
                    break;
                    // 建设中
                case 4:
                    break;
                    // 规划中
                case 5:
                    break;
            }

            return list;
        }

        /// <summary>
        /// 根据楼栋编号获取房源可选状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetSalesStatusListByBuildingId(int id)
        {
            var building = new BuildingBll().GetEntity_ById(id);
            var state = building.State;
            var list = new List<Z_GeographyItem>();
            switch (state)
            {
                    // 待售
                case 1:
                    list.Add(new Z_GeographyItem {GeographyCode = 0, GeographyName = "待售"});
                    break;
                    // 在售
                case 2:
                    list.Add(new Z_GeographyItem {GeographyCode = 0, GeographyName = "待售"});
                    list.Add(new Z_GeographyItem {GeographyCode = 1, GeographyName = "开发商保留"});
                    list.Add(new Z_GeographyItem {GeographyCode = 2, GeographyName = "在售"});
                    list.Add(new Z_GeographyItem {GeographyCode = 3, GeographyName = "已认购"});
                    list.Add(new Z_GeographyItem {GeographyCode = 4, GeographyName = "已签约"});
                    list.Add(new Z_GeographyItem {GeographyCode = 5, GeographyName = "已备案"});
                    list.Add(new Z_GeographyItem {GeographyCode = 6, GeographyName = "已办产权"});
                    list.Add(new Z_GeographyItem {GeographyCode = 7, GeographyName = "被限制"});
                    list.Add(new Z_GeographyItem {GeographyCode = 8, GeographyName = "拆迁安置"});
                    list.Add(new Z_GeographyItem {GeographyCode = 9, GeographyName = "售罄"});
                    break;
                    // 售罄
                case 3:
                    list.Add(new Z_GeographyItem {GeographyCode = 9, GeographyName = "售罄"});
                    break;
                case 4:
                    return Json(new {success = false}, JsonRequestBehavior.AllowGet);
                case 5:
                    return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            }
            return Json(new {success = true, items = list.ToArray()}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取楼栋信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetBuildingById(int id)
        {
            var building = new BuildingBll().GetEntity_ById(id);

            return Json(new {state = building.State}, JsonRequestBehavior.AllowGet);
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

        #region 房源名称是否重复

        /// <summary>
        /// 房源名称是否重复
        /// author: liyuzhao
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public JsonResult GetIsHouseNameRepeat(House house)
        {
            if (0 < new HouseBll().GetHouseNameCountByIdAndName(house))
            {
                return Json(new {suc = false, msg = "房源名称已存在"});
            }
            return Json(new {suc = true, msg = "查无该房源名称"});
        }

        /// <summary>
        /// 房源名称是否重复
        /// author: liyuzhao
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public JsonResult GetIsHouseNameRepeat_Update(House house)
        {
            if (0 < new HouseBll().GetHouseNameCountByIdAndName_Update(house))
            {
                return Json(new {suc = false, msg = "房源名称已存在"});
            }
            return Json(new {suc = true, msg = "查无该房源名称"});
        }

        #endregion

        #endregion

        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public ActionResult Detail(int houseId)
        {

            var detail = new HouseBll().GetHouseDetailByHouseId(houseId);

            #region history

            //_houseBll = new HouseBll();
            //_geographyBll = new GeographyBll();
            //_buildingBll = new BuildingBll();
            //_premisesBll = new PremisesBll();
            //var house = _houseBll.GetEntity_ById(houseId);
            //ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().PremisesIndex : Request.Params["backurl"];
            //var entity = new PVE_NH_House();
            //if (house != null)
            //{
            //    #region 房源数据

            //    var houseinfo = new PVM_NH_House
            //    {
            //        Id = house.Id,
            //        Name = house.Name,
            //        InnerCode = house.InnerCode,
            //        DeveloperId = house.DeveloperId,
            //        PremisesId = house.PremisesId,
            //        BuildingId = house.BuildingId,
            //        UnitId = house.UnitId,
            //        Floor = house.Floor,
            //        RId = house.RId,
            //        BuildingType = house.BuildingType,
            //        Orientation = house.Orientation,
            //        PriceType = house.PriceType,
            //        TotalPrice = house.TotalPrice,
            //        SinglePrice = house.SinglePrice,
            //        DownPayment = house.DownPayment,
            //        SalesStatus = house.SalesStatus,
            //        BuildingArea = house.BuildingArea,
            //        Area = house.Area,
            //        Room = house.Room,
            //        Hall = house.Hall,
            //        Toilet = house.Toilet,
            //        Kitchen = house.Kitchen,
            //        //Balcony = house.Balcony,
            //        CreateTime = house.CreateTime,
            //        UpdateTime = house.UpdateTime,
            //        IsDel = house.IsDel,
            //        CoordX = house.CoordX,
            //        CoordY = house.CoordY
            //    };

            //    #endregion

            //    int premisesId = house.PremisesId;
            //    int buildingId = house.BuildingId;
            //    int developerId = house.DeveloperId;
            //    var t_premises = _premisesBll.GetPremises_ById(premisesId); //楼盘
            //    var buildingmodel = _buildingBll.GetEntity_ById(buildingId); //楼栋

            //    #region  房源扩展数据（下拉列表）

            //    //楼盘
            //    var preSelectitem = _geographyBll.Z_GetPremises().ToList().
            //        ConvertAll(it => new SelectListItem
            //        {
            //            Text = it.GeographyName,
            //            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)

            //        });
            //    //楼栋 已剔除不可发布房源的楼栋
            //    var buildings = _geographyBll.Z_GetBuildings_Find(premisesId).ToList()
            //        .ConvertAll(it => new SelectListItem
            //        {
            //            Text = it.GeographyName,
            //            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            //        });

            //    //单元
            //    var units = _geographyBll.Z_GetUnits(buildingId).ToList()
            //        .ConvertAll(it => new SelectListItem
            //        {
            //            Text = it.GeographyName,
            //            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            //        });
            //    entity.Units.AddRange(units);
            //    //楼层
            //    var floors = _geographyBll.Z_GetBuildingFloors(buildingId).ToList()
            //        .ConvertAll(it => new SelectListItem
            //        {
            //            Text = it.GeographyName,
            //            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            //        });
            //    entity.Floors.AddRange(floors);
            //    //户型图
            //    var list = GetPicture.GetPremisesPictureList(t_premises.InnerCode, true, PremisesPictureType.ROOMTYPE.ToString(), t_premises.CityId);
            //    if (list != null && list.Any())
            //    {
            //        var rlist = list.ConvertAll(it => new SelectListItem
            //        {
            //            Text = it.Title,
            //            Value = it.ID.ToString()
            //        });
            //        entity.RIds.AddRange(rlist);
            //    }

            //    entity.DeveloperId = developerId;
            //    entity.BuildingId = buildingId;
            //    entity.PremisesId = premisesId;
            //    entity.Premises.AddRange(preSelectitem);
            //    entity.Buildings.AddRange(buildings);
            //    entity.BuildingType =
            //        TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_BuildingType();
            //    entity.Orientation =
            //        TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_Orientation();
            //    entity.SalesStatuss = GetSalesStatusList(buildingmodel.State); //动态的房源销售状态

            //    #endregion

            //    var model = new PVME_NH_House
            //    {
            //        BuildingId = houseinfo.BuildingId,
            //        BuildingType = houseinfo.BuildingType,
            //        DeveloperId = houseinfo.DeveloperId,
            //        Floor = houseinfo.Floor,
            //        Orientation = houseinfo.Orientation,
            //        PremisesId = houseinfo.PremisesId,
            //        RId = houseinfo.RId,
            //        SalesStatus = houseinfo.SalesStatus,
            //        UnitId = houseinfo.UnitId,
            //        PveHouse = entity,
            //        PvmHouse = houseinfo
            //    };
            //    //return View(model);

            //}
            //数据不存在
            // return Content(GetPageMsg("该记录不存在", ViewData["backurl"].ToString()));

            #endregion

            return View(detail);
        }
    }
}