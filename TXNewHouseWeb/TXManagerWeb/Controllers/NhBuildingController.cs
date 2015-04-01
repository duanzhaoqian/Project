using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Newtonsoft.Json;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.NHouseActivies;
using TXCommons.Admins;
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
    public class NhBuildingController : BaseController
    {
        private BuildingBll _buildingBll;
        private PremisesBll _premisesBll;

        #region 列表

        /// <summary>
        /// 楼栋
        /// </summary>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.HouseData.Building)]
        public ActionResult Index(PVS_NH_Building search)
        {

            if (null == search)
            {
                search = new PVS_NH_Building();
            }

            _premisesBll = new PremisesBll();
            var premis = _premisesBll.GetPremises_ById(search.PremisesId);

            ViewData["premisesName"] = (null == premis ? string.Empty : premis.Name);

            return View(search);
        }

        /// <summary>
        /// 搜索列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_Building search, int pageIndex, int pageSize)
        {
            _buildingBll = new BuildingBll();

            pageIndex = pageIndex + 1;
            var list = _buildingBll.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/HouseData/_Building", list);
        }

        /// <summary>
        /// 搜索列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_Building search)
        {
            _buildingBll = new BuildingBll();
            int count = _buildingBll.GetTotalCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #region 添加、编辑楼栋

        /// <summary>
        /// 添加新楼栋
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.HouseData.Building)]
        public ActionResult AddNewBuilding(int premisesId)
        {
            _premisesBll = new PremisesBll();
            var premis = _premisesBll.GetPremises_ById(premisesId);
            ViewData["premisesName"] = (null == premis ? string.Empty : premis.Name);
            ViewData["backurl"] = Url.SiteUrl().BuildingIndex + "?premisesid=" + premisesId;

            var building = new PVE_NH_Building
            {
                PremisesId = premisesId,
                Premises = Auxiliary.Instance.GetPremises(),
                NameTypes = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_NameTypes(),
                Ladders = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_Ladder(),
                Renovations = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_Renovation(),
                BuildingPositions = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_BuildingPosition(),
                States = GetStateByPremisesStateForSelectListItems(premis.SalesStatus), //AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_State(),
                PropertyTypes = AdminTypes.Db_NewHouse.Tb_Building.Fc_Pairs.Get_PropertyType(),
                PermitPresales = Auxiliary.Instance.GetPermitPresaleByPremisesId(premisesId),
                //PNums = Auxiliary.Instance.GetPNumsByPremisesId(premisesId)
                //BuildingPlans = Auxiliary.Instance.GetBuildingPlansByPremisesId(premisesId)
            };
            //户型图
            var rlist = new List<SelectListItem>();
            if (null != premis)
            {
                var list = GetPicture.GetPremisesPictureList(premis.InnerCode, true, PremisesPictureType.Plane.ToString(), premis.CityId);
                if (null != list)
                {
                    rlist = list.ConvertAll(it => new SelectListItem
                    {
                        Text = it.Title,
                        Value = Convert.ToString(it.ID)
                    });
                }
            }
            building.BuildingPlans = rlist;

            //沙盘图

            //var rlistp = new List<SelectListItem>();

            //var listp = GetSand(premisesId);
            //if (null != listp)
            //{
            //    rlistp = listp.ConvertAll(it => new SelectListItem
            //    {
            //        Text = Convert.ToString(it.Number),
            //        Value = Convert.ToString(it.Id)
            //    });    
            //}

            building.PNums = new List<SelectListItem>(); //rlistp;

            building.PermitPresales.Insert(0, new SelectListItem {Selected = true, Text = "请选择", Value = "0"});
            building.BuildingPlans.Insert(0, new SelectListItem {Selected = true, Text = "请选择", Value = "0"});
            building.PNums.Insert(0, new SelectListItem {Selected = true, Text = "请选择", Value = "0"});

            return View(building);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public JsonResult AddNewBuildingDo(PVE_NH_Building building)
        {
            _buildingBll = new BuildingBll();

            if (0 < _buildingBll.GetBuildingNameCountByIdAndName(building))
            {
                return Json(new {suc = false, msg = "楼栋名称已存在", code = 2});
            }

            // 转换单元名称(特殊字符 ニ 转换为 " )
            var tmpUnitNameList = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(building.UnitNameListString);
            if (null != tmpUnitNameList && 0 < tmpUnitNameList.Count)
            {
                building.UnitNameList = new List<KeyValuePair<string, string>>();
                for (int i = 0; i < tmpUnitNameList.Count; i++)
                {
                    building.UnitNameList.Add(new KeyValuePair<string, string>(tmpUnitNameList[i].Key, Regex.Replace(tmpUnitNameList[i].Value, "ニ", "\"")));
                }
            }

            building.PropertyType = building.PropertyType.TrimEnd(',');
            building.Periods = (0 == building.Periods ? 1 : building.Periods);

            var buildingId = _buildingBll.AddNewBuilding(building);

            if (buildingId > 0)
            {
                var p = new PremisesBll().GetPremisesbyId(building.PremisesId);
                if (p != null)
                {
                    //TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("insert", p.Id, p.CityId);
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", p.Id, p.CityId);
                }

                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-楼栋管理-添加楼栋编号为：[{0}]", buildingId),
                    Type = 0
                });

                return Json(new {suc = true, msg = "操作成功"});
            }
            return Json(new {suc = false, msg = "操作失败"});
        }

        /// <summary>
        /// 添加新预售许可证
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <param name="name">预售许可证编号</param>
        /// <returns></returns>
        public JsonResult AddNewPermitPresale(int premisesId, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Json(new {suc = false, msg = "预售许可证不可为空"});
            }

            var permitPresale = new PVE_PermitPresale();
            permitPresale.PremisesId = premisesId;
            permitPresale.Name = name;

            var flag = new PermitPresaleBll().AddNewPermitPresale(permitPresale);
            if (flag)
            {
                return Json(new {suc = true, msg = "操作成功"});
            }
            return Json(new {suc = false, msg = "操作失败"});
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.HouseData.Building)]
        public ActionResult EditBuilding(int buildingId)
        {
            _buildingBll = new BuildingBll();
            var building = _buildingBll.GetEntity_ById(buildingId);
            if (null == building)
            {
                return RedirectToAction("Index", "NhBuilding");
            }
            ViewData["buildingName"] = building.Name + building.NameType;

            _premisesBll = new PremisesBll();
            var premis = _premisesBll.GetPremises_ById(building.PremisesId);
            if (null == premis)
            {
                return RedirectToAction("Index", "NhBuilding");
            }
            ViewData["premisesName"] = premis.Name;

            ViewData["backurl"] = string.IsNullOrEmpty(Request.Params["backurl"]) ? Url.SiteUrl().BuildingIndex + "?premisesId=" + premis.Id : Request.Params["backurl"];

            var unitList = new UnitBll().GetUnitList(building.Id);

            var premisesId = premis.Id;
            var editBuilding = new PVE_NH_Building
            {
                PremisesId = premisesId,
                Id = building.Id,
                Name = building.Name,
                NameType = building.NameType,
                Periods = building.Periods,
                UnitCount = unitList.Count,
                UnitNameList = unitList,
                OpeningTime = string.Format("{0:yyyy-MM-dd}", building.OpeningTime),
                OthersTime = string.Format("{0:yyyy-MM-dd}", building.OthersTime),
                FamilyNum = building.FamilyNum,
                TheFloor = building.TheFloor,
                Underloor = building.Underloor,
                Ladder = building.Ladder,
                Renovation = building.Renovation,
                BuildingPosition = building.BuildingPosition,
                PictureData = building.PictureData,
                State = building.State,
                PresaleId = building.PresaleId,
                PNum = building.PNum,
                PropertyType = "," + building.PropertyType + ",",

                Premises = Auxiliary.Instance.GetPremises(),
                NameTypes = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_NameTypes(),
                Ladders = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_Ladder(),
                Renovations = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_Renovation(),
                BuildingPositions = AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_BuildingPosition(),
                States = GetStateByPremisesStateForSelectListItems(premis.SalesStatus), //AdminTypes.Db_NewHouse.Tb_Building.Fc_SelectListItems.Get_State_ByCurrentState(building.State),
                PropertyTypes = AdminTypes.Db_NewHouse.Tb_Building.Fc_Pairs.Get_PropertyType(),
                PermitPresales = Auxiliary.Instance.GetPermitPresaleByPremisesId(premisesId)
            };
            //户型图
            List<PremisesPictureInfo> list = GetPicture.GetPremisesPictureList(premis.InnerCode, true, PremisesPictureType.Plane.ToString(), premis.CityId);

            var rlist = new List<SelectListItem>();
            if (null != list)
            {
                rlist = list.ConvertAll(it => new SelectListItem
                {
                    Text = it.Title,
                    Value = Convert.ToString(it.ID)
                });
            }

            editBuilding.BuildingPlans = rlist;

            //沙盘图
            var listp = GetSand(premisesId);

            var rlistp = listp.ConvertAll(it => new SelectListItem
            {
                Text = Convert.ToString(it.Number),
                Value = Convert.ToString(it.Id)
            });
            editBuilding.PNums = rlistp;

            editBuilding.PermitPresales.Insert(0, new SelectListItem {Selected = true, Text = "请选择", Value = "0"});
            editBuilding.BuildingPlans.Insert(0, new SelectListItem {Selected = true, Text = "请选择", Value = "0"});
            editBuilding.PNums.Insert(0, new SelectListItem {Selected = true, Text = "请选择", Value = "0"});

            return View(editBuilding);
        }

        /// <summary>
        /// 更新楼栋信息
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public JsonResult UpdateNewBuildingDo(PVE_NH_Building building)
        {
            _buildingBll = new BuildingBll();
            var oldBuilding = _buildingBll.GetEntity_ById(building.Id);
            building.PremisesId = oldBuilding.PremisesId;
            if (0 < _buildingBll.GetBuildingNameCountByIdAndName_Update(building))
            {
                return Json(new {suc = false, msg = "楼栋名称已存在"});
            }

            // 判断状态是否变更
            var isStateChange = (oldBuilding.State != building.State); // true:变更 false:未变更
            if (isStateChange)
            {
                var activeHouseCount = new ActiviesUtilsBll().GetLivingHouseCountByBuildingId(building.Id);
                if (0 < activeHouseCount)
                {
                    return Json(new {suc = false, msg = "当前楼栋下有营销活动进行中，不能变更。"});
                }
            }

            // 转换单元名称(特殊字符 ニ 转换为 " )
            var tmpUnitNameList = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(building.UnitNameListString);
            if (null != tmpUnitNameList && 0 < tmpUnitNameList.Count)
            {
                building.UnitNameList = new List<KeyValuePair<string, string>>();
                for (int i = 0; i < tmpUnitNameList.Count; i++)
                {
                    building.UnitNameList.Add(new KeyValuePair<string, string>(tmpUnitNameList[i].Key, Regex.Replace(tmpUnitNameList[i].Value, "ニ", "\"")));
                }
            }

            building.PropertyType = building.PropertyType.TrimEnd(',');
            building.Periods = (0 == building.Periods ? 1 : building.Periods);

            bool opFlag; // 操作标志
            if (isStateChange)
            {
                // 楼栋状态变更，变更所属房源状态
                opFlag = (0 < _buildingBll.UpdateNewBuildingAndUpdateHouseState(building, AdminTypes.Db_NewHouse.Tb_Building.Fc_ById.For_MatchHouseStateByState(building.State)));
            }
            else
            {
                opFlag = (0 < _buildingBll.UpdateNewBuilding(building));
            }

            if (opFlag)
            {
                var p = new PremisesBll().GetPremisesbyId(building.PremisesId);
                if (p != null)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", p.Id, p.CityId);
                }

                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-楼栋管理-编辑楼栋编号为：[{0}]", building.Id),
                    Type = 0
                });

                return Json(new {suc = true, msg = "操作成功"});
            }
            return Json(new {suc = false, msg = "操作失败"});
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除楼栋
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public JsonResult DelNewBuildingDo(int buildingId)
        {
            _buildingBll = new BuildingBll();

            var activeHouseCount = new ActiviesUtilsBll().GetLivingHouseCountByBuildingId(buildingId);
            if (0 < activeHouseCount)
            {
                return Json(new {suc = false, msg = "当前楼栋有营销活动进行，不能删除。"});
            }

            var building = _buildingBll.GetEntity_ById(buildingId);
            if (_buildingBll.DelNewBuilding(buildingId) > 0)
            {
                var p = new PremisesBll().GetPremisesbyId(building.PremisesId);
                if (p != null)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", p.Id, p.CityId);

                    // 循环给房源发消息
                    var houses = new HouseBll().GetHouseIdListByBuildingId(building.Id);
                    var premis = new PremisesBll().GetPremises_ById(building.PremisesId);
                    if (null != houses && 0 < houses.Count && null != premis)
                    {
                        for (int i = 0; i < houses.Count; i++)
                        {
                            TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("delete", houses[i], premis.CityId);
                        }
                    }
                }

                if (0 < building.DeveloperId)
                {
                    var premises = new PremisesBll().GetPremises_ById(building.PremisesId);
                    var developer = (new DevelopersBll().GetEntity_ById(building.DeveloperId)) as Developer;

                    // 发送站内信
                    new DeveloperMessageBll().Add(new DeveloperMessage
                    {
                        SendUserId = 0,
                        ReceiveUserId = building.DeveloperId,
                        Title = "删除楼栋",
                        Content = string.Format(@"尊敬的{0}，{1}被删除，请如有问题请致电快有家客户服务热线：{2}。",
                            null == developer ? string.Empty : developer.LoginName,
                            (null == premises ? string.Empty : premises.Name) + building.Name + building.NameType,
                            Auxiliary.Instance.NhServiceHotLine1)
                    });
                }

                // 操作日志
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-楼栋管理-删除楼栋编号为：[{0}]", building.Id),
                    Type = 0
                });

                return Json(new {suc = true, msg = "删除成功"});
            }
            return Json(new {suc = false, msg = "删除失败"});
        }

        #endregion

        #region ajax 根据楼盘id获取最新楼栋平面图

        /// <summary>
        ///  ajax 根据楼盘id获取最新楼栋平面图
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public JsonResult AjaxGetPlanes(int id)
        {
            try
            {
                _premisesBll = new PremisesBll();
                var tPremises = _premisesBll.GetPremises_ById(id);
                //户型图
                List<PremisesPictureInfo> list = GetPicture.GetPremisesPictureList(tPremises.InnerCode, true, PremisesPictureType.Plane.ToString(), tPremises.CityId);
                if (list != null && list.Count() > 0)
                    return Json(new {success = true, items = list}, JsonRequestBehavior.AllowGet);
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(new {success = false}, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/11 10:48:28
        /// 功能描述：获取楼盘沙盘列表
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <param name="pnum"></param>
        /// <returns></returns>
        public ActionResult GetSandBoxList(int id, int pnum = 0)
        {
            var list = new SandTableLabelBll().GetSandBoxSelectionsByPremisesId(id, pnum);
            return Json(new {success = true, list}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取楼盘沙盘列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<TXOrm.SandTableLabel> GetSand(int premisesId)
        {
            SandTableLabelBll sandtablelabelbll = new SandTableLabelBll();
            _premisesBll = new PremisesBll();
            var tPremises = _premisesBll.GetPremises_ById(premisesId);
            var list = sandtablelabelbll.GetSandList(premisesId, tPremises.DeveloperId);
            return list;
        }

        /// <summary>
        /// 获取楼盘沙盘列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public ActionResult GetSandList(int id)
        {
            SandTableLabelBll sandtablelabelbll = new SandTableLabelBll();
            _premisesBll = new PremisesBll();
            var tPremises = _premisesBll.GetPremises_ById(id);
            var list = sandtablelabelbll.GetSandList(id, tPremises.DeveloperId);
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 根据楼盘状态获取状态列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public JsonResult GetStateByPremisesId(int premisesId)
        {
            var premis = new PremisesBll().GetPremises_ById(premisesId);
            var state = premis.SalesStatus;
            var list = new List<Z_GeographyItem>();
            switch (state)
            {
                    // 待售
                case 0:
                    list.Add(new Z_GeographyItem {GeographyCode = 1, GeographyName = "待售"});
                    break;
                    // 在售
                case 1:
                    list.Add(new Z_GeographyItem {GeographyCode = 1, GeographyName = "待售"});
                    list.Add(new Z_GeographyItem {GeographyCode = 2, GeographyName = "在售"});
                    list.Add(new Z_GeographyItem {GeographyCode = 3, GeographyName = "售罄"});
                    list.Add(new Z_GeographyItem {GeographyCode = 4, GeographyName = "建设中"});
                    list.Add(new Z_GeographyItem {GeographyCode = 5, GeographyName = "规划中"});
                    break;
                    // 售罄
                case 2:
                    list.Add(new Z_GeographyItem {GeographyCode = 3, GeographyName = "售罄"});
                    break;
            }
            return Json(new {success = true, items = list.ToArray()}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据楼盘状态获取状态列表
        /// </summary>
        /// <param name="objState"></param>
        /// <returns></returns>
        public List<SelectListItem> GetStateByPremisesStateForSelectListItems(object objState)
        {
            var state = Convert.ToInt32(objState);
            var list = new List<SelectListItem> {new SelectListItem {Value = "0", Text = "请选择"}};
            switch (state)
            {
                    // 待售
                case 0:
                    list.Add(new SelectListItem {Value = "1", Text = "待售"});
                    break;
                    // 在售
                case 1:
                    list.Add(new SelectListItem {Value = "1", Text = "待售"});
                    list.Add(new SelectListItem {Value = "2", Text = "在售"});
                    list.Add(new SelectListItem {Value = "3", Text = "售罄"});
                    list.Add(new SelectListItem {Value = "4", Text = "建设中"});
                    list.Add(new SelectListItem {Value = "5", Text = "规划中"});
                    break;
                    // 售罄
                case 2:
                    list.Add(new SelectListItem {Value = "3", Text = "售罄"});
                    break;
            }

            return list;
        }
    }
}