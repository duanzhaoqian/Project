using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.NHouseActivies;
using TXBll.WebSite;
using TXCommons;
using TXCommons.Admins;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXModel.Geography;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.HouseData.Premises)]
    public class PremisesController : BaseController
    {
        public ActionResult Index()
        {
            //TXCommons.MsgQueue.SendMessage.Remove400TelMessage("douhaichao", 1);

            //ViewData["c400"] = TXCommons.MsgQueue.SendMessage.Send400TelMessage("")

            //ViewData["c400"] = TXCommons.MsgQueue.SendMessage.Send400TelMessage("")



            var model = new PVS_NH_Premises { SalesStatus = -1 };
            var list = Auxiliary.Instance.GetProvinces().ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });

            //加载省份数据
            if (list.Count > 0)
                model.Provinces.AddRange(list);

            model.SalesStatus = -1;
            model.SalesState = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_SalesStatus();

            return View("Index", model);
        }

        public ActionResult Search(PVS_NH_Premises search)
        {
            var list = Auxiliary.Instance.GetProvinces().ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });

            //加载省份数据
            if (list.Count > 0)
                search.Provinces.AddRange(list);

            //加载城市数据
            if (search.ProvinceID != 0)
            {
                var listCities = Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceID)
                    .ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });
                search.Cities.AddRange(listCities);
            }

            search.SalesState = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_SalesStatus();

            search.DeveloperName = Server.UrlEncode(search.DeveloperName);
            search.PremisesName = Server.UrlEncode(search.PremisesName);
            return View("index", search);
        }

        public ActionResult SearchResult(PVS_NH_Premises search, int pageIndex, int pageSize)
        {
            var premisesBll = new PremisesBll();

            pageIndex = pageIndex + 1;
            //ViewData["ReturnUrl"] = Url.SiteUrl().AgentSignUp_Index();
            var list = premisesBll.GetPremisesListByPages(search, pageIndex, pageSize);
            return PartialView("PageTables/HouseData/_Premises", list);
        }

        /// <summary>
        /// 搜索结果数量
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_Premises search)
        {
            var premisesBll = new PremisesBll();
            int count = premisesBll.GetPremisesListByPageCounts(search);
            return Json(new { result = count });
        }
        /// <summary>
        /// 发布楼盘
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public ActionResult PublishPremises()
        {
            var geographyBll = new GeographyBll();
            var premises = new PVE_NH_Premises2
            {
                SalesStatus = -1,
                Ring = -1,
                BuildingType = -1,

                SalesStatusList = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_SalesStatus(),
                RingList = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_Ring(),
                BuildingTypeList = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_BuildingType(),
                PremisesFeatureList = Auxiliary.Instance.GetPremisesFeatures(),
            };

            var list = geographyBll.Z_GetProvinces().ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });

            //加载省份数据
            if (list.Count > 0)
            {
                premises.ProvinceList.AddRange(list);
            }

            ViewData["backurl"] = Url.SiteUrl().PremisesIndex;
            ViewData["guid"] = Guid.NewGuid().ToString();

            return View("PublishPremises", premises);
        }

        /// <summary>
        /// 提交发布数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitPremises(PVE_NH_Premises2 premises)
        {
            var countName = new PremisesBll().GetNameCountByPremises(premises);
            if (0 < countName)
            {
                return Json(new { suc = false, msg = "楼盘名称已存在", id = 0, state = -1 });
            }

            premises.Name = premises.Name.Trim();
            premises.Lng = premises.Lng ?? "";
            premises.Lat = premises.Lat ?? "";
            premises.TId = premises.TId ?? "";
            premises.TelePhone = premises.TelePhone ?? "";
            premises.PropertyCompany = premises.PropertyCompany ?? "";
            premises.PremisesIntroduction = premises.PremisesIntroduction ?? "";
            premises.SupportingIntroduction = premises.SupportingIntroduction ?? "";
            premises.TrafficIntroduction = premises.TrafficIntroduction ?? "";
            premises.Characteristic = premises.Characteristic ?? "";
            premises.ParkingLot = premises.ParkingLot ?? "";
            premises.Agent = (string.IsNullOrEmpty(premises.Agent) ? string.Empty : premises.Agent);
            premises.PermitPresales = JsonConvert.DeserializeObject<List<PVEL_NH_PermitPresale>>(premises.PermitPresales_Json);

            string sandbox = Convert.ToString(Request["SandboxCoordinate"]);
            List<SandTableLabel> sandlist = null;
            if (!string.IsNullOrEmpty(sandbox))
            {
                sandlist = TXCommons.Util.JSONStringToList<SandTableLabel>(sandbox);
            }

            var id = new PremisesBll().AddNew(premises, sandlist);

            if (0 < id)
            {
                ConnectTo400(premises.IsShow400 == 1, id, premises.TelePhone);

                // 调用接入400的时候已经给楼盘发送消息
                //TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("insert", id, premises.CityId);

                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-楼盘管理-添加楼盘编号为：[{0}]", id),
                    Type = 0
                });

                return Json(new { suc = true, msg = "添加成功", id = id, state = 0 });
            }

            return Json(new { suc = false, msg = "添加失败", id = 0, state = 0 });
        }

        /// <summary>
        /// 编辑楼盘
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePremises(int id)
        {
            var geographyBll = new GeographyBll();
            var premisesBll = new PremisesBll();

            var models = new PVS_NH_Premises();
            var premise = premisesBll.GetPVM_PremisesById(id);
            PVM_NH_Premises model = premise;
            model.PvsPremises = models;
            model.SalesStatus = premise.SalesStatus;
            model.Ring = premise.Ring;
            model.BuildingType = premise.BuildingType;
            model.PvsPremises.SalesState = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_SalesStatus();//AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_SalesStateByCurrentState(premise.SalesStatus);
            //model.PvsPremises.RingList = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_Ring();
            var ringItems = new List<SelectListItem>();
            var ringList = AdminComs.Instance.GetRingLine(premise.CityId);
            if (ringList != null)
                ringList.ForEach(m => ringItems.Add(new SelectListItem() { Text = m.Value, Value = m.Key.ToString() }));
            model.PvsPremises.RingList = ringItems;
            model.PvsPremises.BuildingTypes = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_BuildingType();
            model.PremisesFeatures = Auxiliary.Instance.GetPremisesFeatures();
            model.Characteristic = premise.Characteristic;
            model.Characteristic_JsonString = new PremisesFeatureBll().GetListJsonStringByIds(model.Characteristic);
            model.SalePermitList = new PremisesBll().GetSalePermitByPremisesId(premise.Id);
            model.Tid_JsonString = new PremisesBll().GetSubwaysJsonStringByPremisesId(premise.Id);

            model.ProvinceId = premise.ProvinceId;
            model.ProvinceName = premise.ProvinceName;
            model.CityId = premise.CityId;
            model.CityName = premise.CityName;
            model.DId = premise.DId;
            model.DName = premise.DName;
            model.BId = premise.BId;
            model.BName = premise.BName;
            model.Lat = premise.Lat;
            model.Lng = premise.Lng;
            model.Phone400 = premise.Phone400;
            model.IsShow400 = premise.IsShow400;
            model.PageUrl = premise.PageUrl;

            var list = geographyBll.Z_GetProvinces().ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });

            //加载省份数据
            if (list.Count > 0)
                model.PvsPremises.Provinces.AddRange(list);
            //加载城市数据
            if (model.ProvinceId > 0)
            {
                var lisetCities = geographyBll.Z_GetCities(model.ProvinceId).ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });
                model.PvsPremises.Cities.AddRange(lisetCities);
            }
            //加载区域数据
            if (model.CityId > 0)
            {
                var lisetDistricts = geographyBll.Z_GetDistricts(model.CityId).ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });
                model.PvsPremises.Districts.AddRange(lisetDistricts);
            }
            //加载商圈数据
            if (model.DId > 0)
            {
                var lisetBusinesses = geographyBll.Z_GetBussineses(model.DId).ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });
                model.PvsPremises.Businesses.AddRange(lisetBusinesses);
            }
            model.LoadingSank = GetSand(id);

            ViewData["backurl"] = Request.UrlReferrer == null ? Auxiliary.Instance.NhManagerUrl + "premises/index.html" : Request.UrlReferrer.ToString();

            return View("UpdatePremises", model);
        }
        [HttpPost]
        public ActionResult DelPremises(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var premises = new PremisesBll().GetPremises_ById(id);
                if (id > 0 && premises != null)
                {
                    int i = new PremisesBll().DelPremise(id);
                    if (i > 0)
                    {
                        ExecuteOperResult(new Z_OperAdminLog
                            {
                                AdminID = _ServiceContext.CurrentUser.Id,
                                CreateTime = DateTime.Now,
                                OperIP = Request.UserHostAddress,
                                OperDes = string.Format("新房管理-楼盘管理-删除楼盘编号为：[{0}]", id),
                                Type = 0
                            });
                        TXCommons.MsgQueue.SendMessage.Remove400TelMessage("", premises.Id, premises.CityId);
                    }
                    return Json(new { data = i });
                }

            }
            return Json(new { data = -1 });
        }

        /// <summary>
        /// 提交编辑数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitHandle(PVE_NH_Premises2 premises)
        {
            var countName = new PremisesBll().GetNameCountByPremises(premises);
            if (0 < countName)
            {
                return Json(new { suc = false, msg = "楼盘名称已存在", id = 0, state = -1 });
            }

            var premises_old = new PremisesBll().GetPremises_ById(premises.Id);
            var isStateChange = (premises_old.SalesStatus != premises.SalesStatus); // true:变更 false:未变更
            if (isStateChange)
            {
                var activeHouseCount = new ActiviesUtilsBll().GetLivingHouseCountByPremisesId(premises.Id);
                if (0 < activeHouseCount)
                {
                    return Json(new { suc = false, msg = "当前楼盘下有营销活动进行中，不能变更。" });
                }
            }

            premises.DeveloperId = premises_old.DeveloperId;
            premises.Name = premises.Name.Trim();
            premises.Lng = premises.Lng ?? "";
            premises.Lat = premises.Lat ?? "";
            premises.TId = premises.TId ?? "";
            premises.TelePhone = premises.TelePhone ?? "";
            premises.PropertyCompany = premises.PropertyCompany ?? "";
            premises.PremisesIntroduction = premises.PremisesIntroduction ?? "";
            premises.SupportingIntroduction = premises.SupportingIntroduction ?? "";
            premises.TrafficIntroduction = premises.TrafficIntroduction ?? "";
            premises.Characteristic = premises.Characteristic ?? "";
            premises.ParkingLot = premises.ParkingLot ?? "";
            premises.Agent = (string.IsNullOrEmpty(premises.Agent) ? string.Empty : premises.Agent);

            premises.PermitPresales = JsonConvert.DeserializeObject<List<PVEL_NH_PermitPresale>>(premises.PermitPresales_Json);

            string sandbox = Convert.ToString(Request["SandboxCoordinate"]);
            List<SandTableLabel> sandlist = null;
            if (!string.IsNullOrEmpty(sandbox))
            {
                sandlist = TXCommons.Util.JSONStringToList<SandTableLabel>(sandbox);
            }

            if (!isStateChange)
            {
                var result = new PremisesBll().UpdatePremises(premises, sandlist);
                // 修改沙盘图，需要清空以前标记的楼栋关联
                if (1 == premises.IsUpdateSandbox)
                {
                    new PremisesBll().UpdateBuildingPNumForUpdateSandBox(premises.Id);
                }
                if (0 < result)
                {
                    ConnectTo400(premises.IsShow400 == 1, premises.Id, premises.TelePhone);
                    // 调用接入400的时候已经给楼盘发送消息
                    // TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", premises.Id, premises.CityId);
                    ExecuteOperResult(new Z_OperAdminLog
                    {
                        AdminID = _ServiceContext.CurrentUser.Id,
                        CreateTime = DateTime.Now,
                        OperIP = Request.UserHostAddress,
                        OperDes = string.Format("新房管理-楼盘管理-编辑楼盘编号为：[{0}]", premises.Id),
                        Type = 0
                    });

                    return Json(new { suc = true, msg = "操作成功" });
                }
                return Json(new { suc = false, msg = "操作失败" });
            }
            else
            {
                var buildingState = Convert.ToInt32(AdminTypes.Db_NewHouse.Tb_Premises.Fc_ById.For_BuildingState(premises.SalesStatus));
                var houseState = Convert.ToInt32(AdminTypes.Db_NewHouse.Tb_Premises.Fc_ById.For_HouseState(premises.SalesStatus));
                var result = new PremisesBll().UpdatePremises_StateChanged(premises, sandlist, buildingState, houseState);
                new PremisesBll().UpdateBuildingPNumForUpdateSandBox(premises.Id);
                if (0 < result)
                {
                    ConnectTo400(premises.IsShow400 == 1, premises.Id, premises.TelePhone);
                    //TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", premises.Id, premises.CityId);
                    ExecuteOperResult(new Z_OperAdminLog
                    {
                        AdminID = _ServiceContext.CurrentUser.Id,
                        CreateTime = DateTime.Now,
                        OperIP = Request.UserHostAddress,
                        OperDes = string.Format("新房管理-楼盘管理-编辑楼盘编号为：[{0}]", premises.Id),
                        Type = 0
                    });

                    return Json(new { suc = true, msg = "操作成功" });
                }
                return Json(new { suc = false, msg = "操作失败" });
            }
        }

        /// <summary>
        /// 接入400电话
        /// </summary>
        /// <param name="isConn"></param>
        /// <param name="premisesId"></param>
        /// <param name="mobile">400转接电话</param>
        private void ConnectTo400(bool isConn, int premisesId, string mobile)
        {
            var premises = new PremisesBll().GetPremises_ById(premisesId);
            var loginname = string.Empty;
            var companyname = string.Empty;
            var username = string.Empty;
            var useremail = string.Empty;
            var usermobile = mobile;
            if (0 < premises.DeveloperId)
            {
                var dev = new DevelopersBll().GetEntity_ById(premises.DeveloperId) as Developer;
                if (null != dev)
                {
                    loginname = dev.LoginName;

                    var devinfo = new DevelopersBll().GetDeveAndIdenInfo(premises.DeveloperId);
                    companyname = devinfo.CompanyName;
                    username = devinfo.UserName;
                    useremail = devinfo.UserEmail;
                }
            }

            // 临时添加代码，如果用户输入kuaiyoujia服务电话，自动删除400转接电话以起到重新生成lucene效果
            if (usermobile.Trim() == "400-999-3535" || string.IsNullOrEmpty(usermobile.Trim()))
            {
                isConn = false;
            }

            if (isConn)
            {
                // 接入400
                TXCommons.MsgQueue.SendMessage.Send400TelMessage(loginname, premisesId, companyname, username, usermobile, useremail, string.Empty, premises.CityId);
                //new TXCommons.user.cjkjb.webservice.OperaUserService().GetPremiseTel("binPath", "classPath", string.Empty, premisesId);
            }
            else
            {
                // 注销400
                TXCommons.MsgQueue.SendMessage.Remove400TelMessage(loginname, premisesId, premises.CityId);
            }
        }

        #region 实体数据转换

        /// <summary>
        /// 实体数据转换
        /// </summary>
        /// <param name="pvmModel">实体</param>
        /// <param name="para">许可证参数</param> 
        /// <returns></returns>
        public Premis SetModel(PVM_NH_Premises pvmModel)
        {
            var model = new Premis();
            if (pvmModel.Id > 0)
                model.Id = pvmModel.Id;

            model.DeveloperId = pvmModel.DeveloperId; //开发商Id
            model.InnerCode = pvmModel.InnerCode; //pvmModel.InnerCode;      //Guid
            model.ProvinceId = pvmModel.ProvinceId;
            model.ProvinceName = pvmModel.ProvinceName;
            model.CityId = pvmModel.CityId;
            model.CityName = pvmModel.CityName;
            model.DId = pvmModel.DId;
            model.DName = pvmModel.DName;
            model.BId = pvmModel.BId;
            model.BName = pvmModel.BName;
            model.TId = "1,2"; //pvmModel.TId;                //楼盘对应地铁 Id逗号分隔
            model.Name = pvmModel.Name; //楼盘名称
            model.ReferencePrice = pvmModel.ReferencePrice; //参考均价
            model.TelePhone = !string.IsNullOrEmpty(pvmModel.TelePhone) ? pvmModel.TelePhone : "";
            model.SalesStatus = pvmModel.SalesStatus; //销售状态
            model.Ring = pvmModel.Ring; //环线位置(一环...)
            model.PremisesAddress = pvmModel.PremisesAddress; //项目地址
            model.salesAddress = pvmModel.SalesAddress; //售楼地址
            model.Developer = pvmModel.Developer; //开发商
            model.Agent = pvmModel.Agent; //代理商
            model.SalePermit = null; //pvmModel.SalePermit;           //预售许可证
            model.PropertyRight = pvmModel.PropertyRight; //产权
            model.BuildingArea = pvmModel.BuildingArea; //建筑面积
            model.Area = pvmModel.Area; //占地面积
            model.UserCount = pvmModel.UserCount; //总户数
            model.Characteristic = !string.IsNullOrEmpty(pvmModel.Characteristic) ? pvmModel.Characteristic : ""; //项目特色
            model.BuildingType = pvmModel.BuildingType; //建筑类别
            model.PropertyType = pvmModel.PropertyType; //物业类型
            model.AreaRatio = pvmModel.AreaRatio; //容积率
            model.RoomRate = pvmModel.RoomRate; //得房率
            model.PropertyPrice = pvmModel.PropertyPrice; //物业费
            model.ParkingLot = !string.IsNullOrEmpty(pvmModel.ParkingLot) ? pvmModel.ParkingLot : ""; //车位信息
            model.Renovation = pvmModel.Renovation; //装修程度(1毛坯2简装修、3中等装修、4精装修、5豪华装修)
            model.PropertyCompany = !string.IsNullOrEmpty(pvmModel.PropertyCompany) ? pvmModel.PropertyCompany : ""; //物业公司
            model.GreeningRate = pvmModel.GreeningRate; //绿化率
            model.TrafficIntroduction = !string.IsNullOrEmpty(pvmModel.TrafficIntroduction) ? pvmModel.TrafficIntroduction : ""; //交通介绍
            model.SupportingIntroduction = !string.IsNullOrEmpty(pvmModel.SupportingIntroduction) ? pvmModel.SupportingIntroduction : ""; //配套介绍
            model.PremisesIntroduction = !string.IsNullOrEmpty(pvmModel.PremisesIntroduction) ? pvmModel.PremisesIntroduction : ""; //楼盘介绍
            model.Lng = "12"; //pvmModel.Lng;             //经度
            model.Lat = "12"; //pvmModel.Lat;            //纬度
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.IsDel = false;

            return model;
        }

        #endregion

        #region 根据省市检验楼盘名称是否存在

        /// <summary>
        /// 根据省市检验楼盘名称是否存在
        /// </summary>
        /// <param name="ProvinceId">省份Id</param> 
        /// <param name="cityId">城市Id</param> 
        /// <returns></returns>
        public ActionResult RegPremiseName(int ProvinceId, int cityId, string names)
        {

            try
            {
                var premisesBll = new PremisesBll();
                var flag = false;
                var lists = premisesBll.GetPremiseNameById(ProvinceId, cityId);
                if (lists.Count > 0)
                {
                    foreach (var item in lists)
                    {
                        if (item == names)
                        {
                            flag = false;
                            break;
                        }
                        else
                            flag = true;
                    }
                    return Json(new { result = true, lists, flag }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    flag = true;
                    return Json(new { result = true, flag }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { result = false, msg = "未知错误!" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 楼盘项目特色列表

        /// <summary>
        /// 楼盘项目特色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPremisesFeatureList()
        {
            var _preisesFeatureBll = new PremisesFeatureBll();
            var list = _preisesFeatureBll.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 设置地铁线路

        /// <summary>
        /// 设置地铁线路
        /// </summary>
        /// <returns></returns>
        public ActionResult Traffic(int CityId)
        {
            var premisesBll = new PremisesBll();
            //获取当前地域地铁线路信息
            var _list = premisesBll.Z_GetTrafficsByCityIDAndTId(CityId, 0);
            Z_GeographyCommon model = new Z_GeographyCommon();
            model.GeographyItems = _list;
            return PartialView("PageTables/HouseData/_Traffic", model);
        }

        /// <summary>
        /// 页面获取指定地铁线路下面的每一个站
        /// </summary>
        /// <param name="geographyCode">几号线ID</param>
        /// <returns></returns>
        public ActionResult TrafficStation(int geographyCode)
        {
            try
            {
                var premisesBll = new PremisesBll();
                var traffics = premisesBll.Z_GetTrafficsByID(geographyCode).ToArray();
                return Json(new { success = true, items = traffics }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 楼盘地图标注

        /// <summary>
        /// 楼盘地图标注
        /// </summary>
        /// <returns></returns>
        public ActionResult Coordinate(int cityId)
        {
            //=======================
            try
            {
                int icityid = 253;
                if (cityId > 0)
                {
                    icityid = cityId;
                }
                var premisesBll = new PremisesBll();
                Area _Coordinate = premisesBll.Z_GetAreaByCityID(icityid);
                if (null != _Coordinate)
                {
                    ViewData["Lat"] = _Coordinate.Lat;
                    ViewData["Lng"] = _Coordinate.Lng;
                }
                else
                {
                    ViewData["Lat"] = 39.9;
                    ViewData["Lng"] = 116.4;
                }
            }
            catch
            {
                ViewData["Lat"] = 39.9;
                ViewData["Lng"] = 116.4;
            }
            //=======================
            return PartialView("PageTables/HouseData/Coordinate", null);
        }

        #endregion

        #region 删除楼盘沙盘标记
        /// <summary>
        /// 作者：李雨钊
        /// 时间：2014/2/24
        /// 功能描述：删除楼盘沙盘标记
        /// </summary>
        /// <param name="did"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult DeleteSandBox(int did, int pid)
        {
            int i = new SandTableLabelBll().DelSandTableLabelByDeveloperIdAndPremisesId(pid, did);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  沙盘标注

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/2 15:58:49
        /// 功能描述：沙盘标注
        /// </summary>
        /// <returns></returns>
        public ActionResult Sandbox()
        {
            return PartialView("PageTables/HouseData/_Sandbox", null);
        }

        #endregion

        /// <summary>
        /// 获取楼盘沙盘列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        private string GetSand(int premisesId)
        {
            SandTableLabelBll sandtablelabelbll = new SandTableLabelBll();
            var premisesBll = new PremisesBll();
            var tPremises = premisesBll.GetPremises_ById(premisesId);
            var list = sandtablelabelbll.GetSandList(premisesId, tPremises.DeveloperId);
            var loadingSank = "[";
            int i = 0;
            foreach (SandTableLabel item in list)
            {
                if (i == 0)
                {
                    loadingSank += "'" + item.SandBox + "','" + item.Number + "','" + item.CoordX + "','" + item.CoordY + "'";
                    i++;
                }
                else
                {
                    loadingSank += ",'" + item.SandBox + "','" + item.Number + "','" + item.CoordX + "','" + item.CoordY + "'";
                }
            }

            return loadingSank + "]";
            ;
        }

        #region 楼盘相册

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadPremisesPhotoSp(string guid, string picturetype, int cityId)
        {
            ViewData["guid"] = guid;
            ViewData["picturetype"] = picturetype;

            ViewData["cityId"] = cityId;
            List<TXCommons.PictureModular.PremisesPictureInfo> list = new List<TXCommons.PictureModular.PremisesPictureInfo>();
            list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, true, picturetype, cityId);
            ViewData["list"] = list != null && list.Count > 0 ? list[0] : null;
            return View("PageTables/HouseData/UploadPremisesPhotoSp");
        }

        /// <summary>
        /// 楼盘相册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult PremisesImage(int id, int type)
        {
            // 获取楼盘信息
            var premise = new PremisesBll().GetPVM_PremisesById(id);

            // 图片信息
            var imgs = new PVE_NH_Premises_Imgs
            {
                Id = id,
                InnerCode = premise.InnerCode,
                CityId = premise.CityId,
            };

            // 页面Card标签编号
            int intTmp;
            if (!int.TryParse(Convert.ToString(Request.Params["tage"]), out intTmp))
            {
                imgs.Tag = 0;
            }
            else
            {
                imgs.Tag = intTmp;
            }

            // 图片类型
            imgs.PictureType = GetPremisesImgsTypeById(imgs.Tag);

            imgs.PremisesPictureInfo = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(premise.InnerCode, true, imgs.PictureType, premise.CityId);

            return View(imgs);
        }

        public ActionResult PremisesImage10(int id, int type)
        {
            // 获取楼盘信息
            var premise = new PremisesBll().GetPVM_PremisesById(id);

            // 图片信息
            var imgs = new PVE_NH_Premises_Imgs
            {
                Id = id,
                InnerCode = premise.InnerCode,
                CityId = premise.CityId,
            };

            // 页面Card标签编号
            int intTmp;
            if (!int.TryParse(Convert.ToString(Request.Params["tage"]), out intTmp))
            {
                imgs.Tag = 0;
            }
            else
            {
                imgs.Tag = intTmp;
            }

            // 图片类型
            imgs.PictureType = GetPremisesImgsTypeById(imgs.Tag);

            var list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(premise.InnerCode, true, imgs.PictureType, premise.CityId);

            if (list != null && list.Count > 0)
            {
                var nolist = list.Where(r => r.IsNew == 1).ToList();
                if (nolist.Any())
                {
                    list = list.Where(r => r.IsNew == 0).ToList();
                    TXCommons.PictureModular.GetPicture.RemoveRedisKey(premise.InnerCode, imgs.PictureType, premise.CityId);
                    TXCommons.PictureModular.GetPicture.AddRedisKey(premise.InnerCode, imgs.PictureType, premise.CityId, list);
                    foreach (var info in nolist)
                    {
                        TXCommons.MsgQueue.SendMessage.SendDeletePictureMessage(premise.InnerCode, "Premises", imgs.PictureType, info.Path, 0);
                    }
                }
            }

            imgs.PremisesPictureInfo = list != null && list.Count > 0 ? list.OrderByDescending(r => r.ID).ToList() : list;

            return View(imgs);
        }


        /// <summary>
        /// 获取图片类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NonAction]
        public static string GetPremisesImgsTypeById(int id)
        {
            switch (id)
            {
                case 0:
                    return TXCommons.PictureModular.PremisesPictureType.Plan.ToString();
                case 1:
                    return TXCommons.PictureModular.PremisesPictureType.TRAFFIC.ToString();
                case 2:
                    return TXCommons.PictureModular.PremisesPictureType.Location.ToString();
                case 3:
                    return TXCommons.PictureModular.PremisesPictureType.Scene.ToString();
                case 4:
                    return TXCommons.PictureModular.PremisesPictureType.Effect.ToString();
                case 5:
                    return TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString();
                case 6:
                    return TXCommons.PictureModular.PremisesPictureType.Plane.ToString();
                case 10:
                    return TXCommons.PictureModular.PremisesPictureType.Advert.ToString();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 重置已删除图片与房源的关联
        /// </summary>
        /// <param name="id"></param>
        /// <param name="picturetype"></param>
        public void ResetHouseRIdByDelPicId(int id, string picturetype = "")
        {
            var list = new PremisesBll().GetHousesByRId(id); // (使用BuildingId承载CityId)
            new HouseBll().ResetHouseRIdByDelPicId(id);
            if (list != null && list.Count > 0)
            {
                foreach (var house in list)
                {
                    //发送消息
                    TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("update", house.PremisesId, house.BuildingId);
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", house.PremisesId, house.BuildingId);
                }
            }
        }

























        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadPremisesPhoto()
        {
            ViewData["guid"] = Request.QueryString["guid"];

            ViewData["picturetype"] = Request.QueryString["picturetype"];


            ViewData["cityId"] = Request.QueryString["cityId"];
            ViewData["id"] = Request.QueryString["id"];
            //ViewData["opt"] = Request.QueryString["opt"];
            return View();
        }

        /// <summary>
        /// 图片信息
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public PartialViewResult PremisesPhoto(string guid, string picturetype, int cityId, string opt)
        {
            ViewData["guid"] = guid;
            ViewData["picturetype"] = picturetype;

            ViewData["cityId"] = cityId;
            List<TXCommons.PictureModular.PremisesPictureInfo> list = new List<TXCommons.PictureModular.PremisesPictureInfo>();

            list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, true, picturetype, cityId);


            ViewData["list"] = list != null && list.Count > 0 ? list.OrderByDescending(r => r.ID).ToList<TXCommons.PictureModular.PremisesPictureInfo>() : list;
            return PartialView("PageTables/HouseData/_PremisesPhoto");
        }

        /// <summary>
        /// 修改图片标题描述
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult ImageSub(string guid, string picturetype, string cityId, string data, string pid)
        {
            try
            {
                List<TXCommons.PictureModular.PremisesPictureInfo> list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, false, picturetype, int.Parse(cityId));
                UpdateRedisData(data, list);
                TXCommons.PictureModular.GetPicture.RemoveRedisKey(guid, picturetype, int.Parse(cityId));
                TXCommons.PictureModular.GetPicture.AddRedisKey(guid, picturetype, int.Parse(cityId), list);

                if (picturetype.ToLower().Trim() == TXCommons.PictureModular.PremisesPictureType.Effect.ToString().ToLower().Trim())
                {
                    TXCommons.MsgQueue.SendMessage.SendPictureMessage(guid, "Premises", picturetype, pid, int.Parse(cityId));
                }
                if (picturetype.ToLower().Trim() == TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString().ToLower().Trim())
                {
                    TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("update", int.Parse(pid), int.Parse(cityId));
                }
                if (picturetype.ToLower().Trim() == TXCommons.PictureModular.PremisesPictureType.Advert.ToString().ToLower().Trim())
                {
                    TXCommons.PictureModular.GetPicture.RemoveRedisKey(guid, TXCommons.PictureModular.PremisesPictureType.AdvertList.ToString(), int.Parse(cityId));
                    //变更当前广告图key
                    var temp = list.Where(r => r.IsForce == 1).ToList();
                    if (temp.Count > 0)
                    {
                        temp[0].PictureType = TXCommons.PictureModular.PremisesPictureType.AdvertList.ToString();
                    }
                    TXCommons.PictureModular.GetPicture.AddRedisKey(guid, TXCommons.PictureModular.PremisesPictureType.AdvertList.ToString(), int.Parse(cityId), temp);
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", int.Parse(pid), int.Parse(cityId));
                }

                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", Util.ToInt(pid), Util.ToInt(cityId));

                return Json(new { suc = true, msg = "保存成功" });
            }
            catch (Exception ex)
            {
                return Json(new { suc = true, msg = "保存成功" });
            }

        }

        /// <summary>
        /// 新房图片提交
        /// </summary>
        /// <param name="innerCode"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult UploadPremisesPhotoSub(string innerCode, string pid)
        {
            var premissbll = new PremisesBll();
            TXOrm.Premis p = premissbll.GetPremisesbyId(int.Parse(pid));
            //TXCommons.MsgQueue.SendMessage.SendPictureMessage(innerCode, "Premises", "update", int.Parse(pid), p.CityId);

            return Redirect(Url.SiteUrl().PremisesImgs(int.Parse(pid), 0) + pid);

        }

        /// <summary>
        /// 图片信息修改
        /// </summary>
        /// <param name="data"></param>
        /// <param name="list"></param>
        public void UpdateRedisData(string data, List<TXCommons.PictureModular.PremisesPictureInfo> list)
        {

            string[] datas = data.Split("|!|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (datas.Length > 0)
            {
                foreach (string s in datas)
                {
                    string[] ds = s.Split("(,)".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (ds.Length >= 2)
                    {
                        foreach (TXCommons.PictureModular.PremisesPictureInfo info in list)
                        {
                            if (info.ID == int.Parse(ds[0]))
                            {
                                info.Title = ds[1];
                                info.IsForce = int.Parse(ds[2]);
                                info.Desc = ds.Length >= 4 ? ds[3] : "";
                                info.IsNew = 0;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 返回户型图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetROOMTYPEImageUseCount(string id)
        {
            var premissbll = new PremisesBll();
            return Content(premissbll.GetROOMTYPEImageUseCount(id));
        }

        /// <summary>
        /// 返回平面图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetPlaneImageUseCount(string id)
        {
            var premissbll = new PremisesBll();
            return Content(premissbll.GetPlaneImageUseCount(id));
        }

        /// <summary>
        /// 返回已保存图片数量
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public ActionResult GetImageCount(string guid, string picturetype, int cityId)
        {
            var premissbll = new PremisesBll();
            return Content(TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, false, picturetype, cityId).Count().ToString());
        }

        #endregion

        #region 楼盘LOGO

        /// <summary>
        /// logo上传 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ActionResult UploadPremisesPhotoLogo(string guid, string picturetype, int cityId)
        {
            ViewData["guid"] = guid;
            ViewData["picturetype"] = picturetype;

            ViewData["cityId"] = cityId;
            var list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, true, picturetype, cityId);
            ViewData["logo"] = ((list != null && list.Count > 0) ? list[0] : null);
            return PartialView("PageTables/HouseData/UploadPremisesPhotoLogo");
        }

        #endregion

        #region 楼盘特色词相关
        /// <summary>
        /// 管理楼盘特色词
        /// Author:tyh
        /// Time:2014-4-15
        /// </summary>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.HouseData.PremisesFeature)]
        public ActionResult PremisesFeature()
        {
            PremisesFeatureBll bll = new PremisesFeatureBll();
            List<PremisesFeature> list = bll.GetList();
            if (list != null && list.Count > 0)
                list.Reverse();
            if (Request.IsAjaxRequest())
                return PartialView("PremisesFeatureTable", list);
            else
                return View(list);
        }
        /// <summary>
        /// 添加楼盘特色词
        /// </summary>
        /// <returns></returns>
        public JsonResult AddPremisesFeature(string text)
        {
            PremisesFeatureBll bll = new PremisesFeatureBll();
            if (bll.Add(text) > 0)
                return Json(true);
            else
                return Json(false);
        }
        /// <summary>
        /// 修改楼盘特色词
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdatePremisesFeature(int id, string text)
        {
            PremisesFeatureBll bll = new PremisesFeatureBll();
            if (bll.Update(id, text) > 0)
                return Json(true);
            else
                return Json(false);
        }
        /// <summary>
        /// 删除楼盘特色词
        /// </summary>
        /// <returns></returns>
        public JsonResult DeletePremisesFeature(int id)
        {
            PremisesFeatureBll bll = new PremisesFeatureBll();
            if (bll.Delete(id) > 0)
                return Json(true);
            else
                return Json(false);
        }

        #endregion

        #region 开放商楼盘关联

        public ActionResult DevelopRelation()
        {
            var model = new PVS_NH_Premises { SalesStatus = -1 };
            var list = Auxiliary.Instance.GetProvinces().ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });
            var delselect = SelectDevelopListItems();
            ViewData["developlist"] = delselect;
            //加载省份数据
            if (list.Count > 0)
                model.Provinces.AddRange(list);

            model.SalesStatus = -1;
            model.SalesState = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_SalesStatus();
            return View(model);
        }
        public ActionResult SearchNoDevel(PVS_NH_Premises search)
        {
            var list = Auxiliary.Instance.GetProvinces().ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });

            //加载省份数据
            if (list.Count > 0)
                search.Provinces.AddRange(list);

            //加载城市数据
            if (search.ProvinceID != 0)
            {
                var listCities = Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceID)
                    .ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });
                search.Cities.AddRange(listCities);
            }
            var delselect = SelectDevelopListItems();
            ViewData["developlist"] = delselect;
            search.SalesState = AdminTypes.Db_NewHouse.Tb_Premises.Fc_SelectListItems.Get_SalesStatus();

            search.DeveloperName = Server.UrlEncode(search.DeveloperName);
            search.PremisesName = Server.UrlEncode(search.PremisesName);
            return View("DevelopRelation", search);
        }

        private static List<SelectListItem> SelectDevelopListItems()
        {
            var devellist =
                new DevelopersBll().GetPageList_BySearch_Admin(new PVS_NH_Developer() { LockState = 0, Type = 1, State = 1 }, 1,
                                                               10000);
            List<SelectListItem> develselect = new List<SelectListItem>();
            develselect.Add(new SelectListItem() { Text = "请选择", Value = "0" });
            devellist.ForEach(m => develselect.Add(new SelectListItem()
                {
                    Text = m.LoginName + "--" + m.Name + "(" + m.ProvinceName + " " + m.CityName + ")",
                    Value = m.Id.ToString()
                }));
            return develselect;
        }

        public ActionResult SearchNoDevelResult(PVS_NH_Premises search, int pageIndex, int pageSize)
        {
            var premisesBll = new PremisesBll();
            pageIndex = pageIndex + 1;
            //ViewData["ReturnUrl"] = Url.SiteUrl().AgentSignUp_Index();
            var list = premisesBll.GetPremisesListByPagesNodevel(search, pageIndex, pageSize);
            return PartialView("PageTables/HouseData/_PremisesNodevel", list);
        }

        /// <summary>
        /// 搜索结果数量
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchNoDevelCount(PVS_NH_Premises search)
        {
            var premisesBll = new PremisesBll();
            int count = premisesBll.GetPremisesListByPageNodevelCounts(search);
            return Json(new { result = count });
        }
        [HttpPost]
        public ActionResult RelationPremise()
        {
            int develid;
            var premiesid = Request["pids"];
            if (int.TryParse(Request["did"], out develid) && !string.IsNullOrWhiteSpace(premiesid))
            {
                int i = new PremisesBll().UpdatePremisesDevelID(develid, premiesid);
                if (i > 0)
                {
                    return Json(new { msg = "关联成功" });
                }
                else
                {
                    return Json(new { msg = "关联失败" });
                }
            }
            return Json(new { msg = "参数有误，请重试" });
        }

        #endregion
    }
}