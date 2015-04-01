using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TXBll.Dev;
using TXOrm;
using TXCommons.bd.cjkjb.webservice;
using Webdiyer.WebControls.Mvc;
using TXModel.Geography;
using TXModel.Commons;
using System.Net;
using TXBll.HouseData;
using TXBll.NHouseActivies;
using TXCommons;
namespace TXDevelopersWeb.Controllers
{
    public class PremisesController : BaseController
    {
        TXBll.HouseData.PremisesBll _premissbll = new TXBll.HouseData.PremisesBll();
        TXBll.HouseData.PermitPresaleBll _permitpresalebll = new TXBll.HouseData.PermitPresaleBll();
        BaseDataWebService bdW = new BaseDataWebService();

        #region 初始化数据

        /// <summary>
        /// 返回省份数据
        /// </summary>
        /// <param name="CountryId"></param>
        /// <returns></returns>
        public ActionResult SearchProvinceList(int CountryId)
        {

            TXCommons.bd.cjkjb.webservice.Area[] area = bdW.SearchProvinceList(CountryId);
            return Json(area, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 返回城市数据
        /// </summary>
        /// <param name="ProvinceId"></param>
        /// <returns></returns>
        public ActionResult SearchCityList(int ProvinceId)
        {
            TXCommons.bd.cjkjb.webservice.Area[] area = bdW.SearchCityList(ProvinceId);
            return Json(area, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取城市环线
        /// </summary>
        /// <param name="provinceId">城市id</param>
        /// <returns></returns>
        public JsonResult Ring(int CityId)
        {
            try
            {
                var ring = TXCommons.Admins.AdminComs.Instance.GetRingLine(CityId);
                return Json(new { success = true, items = ring }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { sucess = false }, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 返回区域数据
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public ActionResult SearchDidList(int CityId)
        {
            TXCommons.bd.cjkjb.webservice.District[] district = bdW.SearchDistrictList(CityId);
            return Json(district, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回商圈数据
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public ActionResult SearchBussiness(int districtId)
        {
            TXCommons.bd.cjkjb.webservice.Business[] business = bdW.SearchBusinessList(districtId);
            return Json(business, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/3 14:39:38
        /// 功能描述：楼盘特色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPremisesFeatureList()
        {
            TXBll.HouseData.PremisesFeatureBll _preisesFeatureBll = new TXBll.HouseData.PremisesFeatureBll();
            var list = _preisesFeatureBll.GetList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/3 13:48:38
        /// 功能描述：根据楼盘Id 得到预售许可证列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public ActionResult GetPermitPresaleList(int PremisesId)
        {
            var list = _permitpresalebll.GetList(PremisesId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 设置地图
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/2 13:33:57
        /// 功能描述：地图弹出层
        /// </summary>
        /// <returns></returns>
        public ActionResult Coordinate()
        {

            int icityid = 253;
            if (null != Request.QueryString["cityId"])
            {
                int.TryParse(Request.QueryString["cityId"].ToString(), out icityid);
            }
            Coordinate _Coordinate = this.bdW.GetCoordinateByCityId(icityid);
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
            return PartialView("Coordinate", null);
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
            return PartialView("Sandbox", null);
        }

        #endregion



        #region 设置地铁
        public ActionResult Traffic(int CityID)
        {
            //获取当前地域地铁线路信息
            var _list = this.bdW.SearchTrafficLineList(CityID);
            List<Z_Traffic> list = new List<Z_Traffic>();
            foreach (var item in _list)
            {
                Z_Traffic model = new Z_Traffic();
                model.TId = item.TId;
                model.Name = item.Name;
                list.Add(model);
            }
            return PartialView("Traffic", list);

        }

        /// <summary>
        /// 页面获取指定地铁线路下面的每一个站
        /// </summary>
        /// <param name="geographyCode">几号线ID</param>
        /// <returns></returns>
        public ActionResult GetTrafficByLine(int lineId)
        {
            try
            {
                var traffics = bdW.SearchTrafficStationList(lineId).ToArray();
                return Json(new { success = true, items = traffics }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }




        #endregion



        #region 楼盘列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/6 15:08:13
        /// 功能描述：楼盘列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int? page)
        {
            ViewData["CurrentMenu"] = 22;

            //int totalCount = 0;
            //int CurrentPage = Request.QueryString["pager"] == null ? 1 : Convert.ToInt32(Request.QueryString["pager"]);
            //List<Premis> list = _premissbll.GetPremisList(LoginUserInfo.Id, CurrentPage, 10, out totalCount);
            //var pagedList = new PagedList<Premis>(list, CurrentPage, 10, totalCount);
            //ViewData["totalCount"] = totalCount;
            //return View("List", pagedList);

            #region 设置分页参数
            Paging<Premis> paging = new Paging<Premis>();
            paging.PageIndex = page.HasValue ? page.Value : 1;
            paging.PageSize = 10;
            #endregion

            #region 读取分页数据
            paging = _premissbll.GetPremisList(paging, LoginUserInfo.Id);
            ViewData["totalCount"] = paging.TotalCount;
            PagedList<Premis> pagelist = new PagedList<Premis>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            #endregion

            //检查是否为Ajax请求
            if (Request.IsAjaxRequest())
                return PartialView("PremisesTable", pagelist);
            else
                return View(pagelist);
        }
        #endregion

        #region 发布楼盘
        public ActionResult Create()
        {
            ViewData["CurrentMenu"] = 21;
            ViewData["guid"] = Guid.NewGuid().ToString();
            return View();
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/26 14:29:32
        /// 功能描述：发布楼盘
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            TXOrm.Premis model = new TXOrm.Premis();
            model.DeveloperId = LoginUserInfo.Id;
            model.InnerCode = collection["guid"];
            string Sandbox = collection["SandboxCoordinate"];

            //调用方法
            List<TXOrm.SandTableLabel> sandlist = new List<SandTableLabel>();
            sandlist = TXCommons.Util.JSONStringToList<SandTableLabel>(Sandbox);

            //TXCommons.Util.DeserializeObject
            #region 楼盘基本信息

            model.Name = Convert.ToString(collection["PropertyName"]).Trim();
            model.ReferencePrice = decimal.Parse(string.Format("{0:F}", collection["ReferencePrice"]));
            model.TelePhone = collection["TelePhone"];
            model.SalesStatus = Convert.ToInt32(collection["SalesStatus"]);
            model.ProvinceId = Convert.ToInt32(collection["ProvinceId"]);

            model.ProvinceName = collection["ProvinceName"];
            model.CityId = Convert.ToInt32(collection["CityId"]);
            model.CityName = collection["CityName"];
            model.DId = Convert.ToInt32(collection["DId"]);
            model.DName = collection["DistrictName"];
            model.BId = Convert.ToInt32(collection["BId"]);
            model.BName = collection["BusinessName"];
            model.Ring = Convert.ToInt32(collection["Ring"]);
            model.PremisesAddress = collection["PremisesAddress"];
            model.salesAddress = collection["salesAddress"];
            model.Developer = collection["Developer"];
            model.Agent = string.IsNullOrEmpty(collection["Agent"]) ? model.Developer : collection["Agent"];

            //预售许可证
            model.SalePermit = "";

            model.TId = collection["Traffic"];//地铁
            model.Lng = string.IsNullOrEmpty(collection["Lng"]) ? "" : collection["Lng"];
            model.Lat = string.IsNullOrEmpty(collection["Lat"]) ? "" : collection["Lat"];

            #endregion

            #region 楼盘建筑信息
            model.PropertyRight = collection["PropertyRight"];
            model.BuildingArea = double.Parse(collection["BuildingArea"]);
            model.Area = double.Parse(collection["Area"]);
            model.UserCount = string.IsNullOrEmpty(collection["UserCount"]) ? 0 : Convert.ToInt32(collection["UserCount"]);
            model.Characteristic = string.IsNullOrEmpty(collection["Characteristic"]) ? "" : collection["Characteristic"];
            model.BuildingType = Convert.ToInt32(collection["BuildingType"]);


            #endregion

            #region 楼盘物业类型

            model.PropertyType = collection["PropertyType"];
            model.AreaRatio = string.IsNullOrEmpty(Request.Form["AreaRatio"]) ? 0 : Convert.ToDouble(collection["AreaRatio"]);
            model.RoomRate = string.IsNullOrEmpty(Request.Form["RoomRate"]) ? 0 : Convert.ToDouble(collection["RoomRate"]);
            model.PropertyPrice = string.IsNullOrEmpty(Request.Form["PropertyPrice"]) ? 0 : decimal.Parse(string.Format("{0:F}", collection["PropertyPrice"]));
            model.ParkingLot = collection["ParkingLot"];
            model.PropertyCompany = collection["PropertyCompany"];
            model.GreeningRate = string.IsNullOrEmpty(Request.Form["GreeningRate"]) ? 0 : Convert.ToDouble(collection["GreeningRate"]);

            #endregion

            #region 楼盘交通配套
            model.TrafficIntroduction = collection["TrafficIntroduction"];
            model.SupportingIntroduction = collection["SupportingIntroduction"];

            #endregion

            #region 楼盘介绍
            model.PremisesIntroduction = collection["PremisesIntroduction"];
            #endregion

            #region 其他信息
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            #endregion

            try
            {
                //string[] Premises = model.SalePermit.Split(',');
                var subways = _premissbll.GetSubwaysByIds(model.TId);

                var PremisesId = 0;
                if (sandlist != null)
                    PremisesId = _premissbll.Add(model, sandlist, subways);
                else
                    PremisesId = _premissbll.Add(model, subways);

                if (PremisesId > 0)
                {
                    //楼盘发布成功

                    // 接入400
                    //if (Convert.ToString(collection["IsShow400"]).Equals("1"))
                    //{
                    ConnectTo400(true, PremisesId, model.TelePhone);
                    //}
                    //TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("insert", PremisesId, model.CityId);
                    return RedirectToAction("PremisesImage/" + PremisesId, "Premises");
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion



        #region 编辑楼盘信息

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/3 11:32:58
        /// 功能描述：编辑楼盘信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewData["CurrentMenu"] = 21;
            Premis model = _premissbll.GetPremises_ById(id);

            if (model != null)
            {
                if (model.DeveloperId != this.LoginUserInfo.Id)
                {
                    return RedirectToAction("List");
                }
                model.TId = new PremisesBll().GetSubwaysJsonStringByPremisesId(model.Id);
                string Tid_JsonString = new PremisesBll().GetSubwaysJsonStringByPremisesId(model.Id);
                ViewData["Tid_JsonString"] = Tid_JsonString;
                return View(model);
            }
            else
            {
                return RedirectToAction("List");
            }
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/9 13:34:01
        /// 功能描述：编辑楼盘
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Premis model = _premissbll.GetPremises_ById(id);
                if (model == null)
                {
                    return RedirectToAction("List");
                }

                model.DeveloperId = LoginUserInfo.Id;
                string Sandbox = collection["SandboxCoordinate"];
                List<TXOrm.SandTableLabel> sandlist = null;
                if (collection["updateState"] == "1")
                {
                    if (!string.IsNullOrEmpty(Sandbox))
                    {
                        //调用方法
                        sandlist = new List<SandTableLabel>();
                        sandlist = TXCommons.Util.JSONStringToList<SandTableLabel>(Sandbox);
                    }
                }

                //TXCommons.Util.DeserializeObject
                #region 楼盘基本信息

                model.Name = Convert.ToString(collection["PropertyName"]).Trim();
                model.ReferencePrice = decimal.Parse(string.Format("{0:F}", collection["ReferencePrice"]));
                model.TelePhone = collection["TelePhone"];
                model.SalesStatus = Convert.ToInt32(collection["SalesStatus"]);
                model.ProvinceId = Convert.ToInt32(collection["ProvinceId"]);

                model.ProvinceName = collection["ProvinceName"];
                model.CityId = Convert.ToInt32(collection["CityId"]);
                model.CityName = collection["CityName"];
                model.DId = Convert.ToInt32(collection["DId"]);
                model.DName = collection["DistrictName"];
                model.BId = Convert.ToInt32(collection["BId"]);
                model.BName = collection["BusinessName"];
                model.Ring = Convert.ToInt32(collection["Ring"]);
                model.PremisesAddress = collection["PremisesAddress"];
                model.salesAddress = collection["salesAddress"];
                model.Developer = collection["Developer"];
                model.Agent = string.IsNullOrEmpty(collection["Agent"]) ? model.Developer : collection["Agent"];

                //预售许可证
                model.SalePermit = "";

                model.TId = string.IsNullOrEmpty(collection["Traffic"]) ? model.TId : collection["Traffic"];//地铁
                model.Lng = string.IsNullOrEmpty(collection["lng"]) ? model.Lng : collection["lng"];
                model.Lat = string.IsNullOrEmpty(collection["Lat"]) ? model.Lat : collection["Lat"];

                #endregion

                #region 楼盘建筑信息
                model.PropertyRight = collection["PropertyRight"];
                model.BuildingArea = double.Parse(collection["BuildingArea"]);
                model.Area = double.Parse(collection["Area"]);
                model.UserCount = string.IsNullOrEmpty(collection["UserCount"]) ? 0 : Convert.ToInt32(collection["UserCount"]);
                model.Characteristic = string.IsNullOrEmpty(collection["Characteristic"]) ? "" : collection["Characteristic"];
                model.BuildingType = Convert.ToInt32(collection["BuildingType"]);


                #endregion

                #region 楼盘物业类型

                model.PropertyType = string.IsNullOrEmpty(collection["PropertyType"]) ? "" : collection["PropertyType"];
                model.AreaRatio = string.IsNullOrEmpty(Request.Form["AreaRatio"]) ? 0 : Convert.ToDouble(collection["AreaRatio"]);
                model.RoomRate = string.IsNullOrEmpty(Request.Form["RoomRate"]) ? 0 : Convert.ToDouble(collection["RoomRate"]);
                model.PropertyPrice = string.IsNullOrEmpty(Request.Form["PropertyPrice"]) ? 0 : decimal.Parse(string.Format("{0:F}", collection["PropertyPrice"]));
                model.ParkingLot = collection["ParkingLot"];
                model.PropertyCompany = collection["PropertyCompany"];
                model.GreeningRate = string.IsNullOrEmpty(Request.Form["GreeningRate"]) ? 0 : Convert.ToDouble(collection["GreeningRate"]);

                #endregion

                #region 楼盘交通配套
                model.TrafficIntroduction = collection["TrafficIntroduction"].Trim();
                model.SupportingIntroduction = collection["SupportingIntroduction"].Trim();

                #endregion

                #region 楼盘介绍
                model.PremisesIntroduction = collection["PremisesIntroduction"].Trim();
                #endregion

                #region 其他信息
                model.UpdateTime = DateTime.Now;
                #endregion

                var subways = _premissbll.GetSubwaysByIds(model.TId);



                //string[] Premises = model.SalePermit.Split(',');
                var PremisesId = 0;
                if (sandlist != null)
                    PremisesId = _premissbll.Update(model, sandlist, subways);
                else
                    PremisesId = _premissbll.Update(model, subways);

                if (PremisesId > 0)
                {
                    //楼盘修改成功

                    // 接入/注销400

                    //ConnectTo400(collection["IsShow400"].Equals("1"), PremisesId, model.TelePhone);
                    ConnectTo400(true, model.Id, model.TelePhone);

                    //TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", model.Id, model.CityId);
                    //return RedirectToAction("PremisesImage/" + model.Id, "Premises");
                    return RedirectToAction("List");
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        #endregion

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
            if (premises != null && 0 < premises.DeveloperId)
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
                TXCommons.MsgQueue.SendMessage.Send400TelMessage(loginname, premisesId, companyname, username, usermobile, useremail, string.Empty, (premises == null ? 0 : premises.CityId));
                //new TXCommons.user.cjkjb.webservice.OperaUserService().GetPremiseTel("binPath", "classPath", string.Empty, premisesId);
            }
            else
            {
                // 注销400
                TXCommons.MsgQueue.SendMessage.Remove400TelMessage(loginname, premisesId, (premises == null ? 0 : premises.CityId));
            }
        }


        #region 楼盘图片处理

        /// <summary>
        /// 沙盘图片上传
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
            return View();
        }
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
            List<TXCommons.PictureModular.PremisesPictureInfo> list = new List<TXCommons.PictureModular.PremisesPictureInfo>();
            list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, true, picturetype, cityId);
            ViewData["logo"] = list != null && list.Count > 0 ? list[0] : null;
            return View();
        }

        public ActionResult PremisesImage(int id)
        {
            ViewData["CurrentMenu"] = 22;
            TXOrm.Premis p = _premissbll.GetPremisesbyId(id);

            return View(p);
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

            list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, false, picturetype, cityId);
            if (list != null && list.Count > 0)
            {
                List<TXCommons.PictureModular.PremisesPictureInfo> nolist = list.Where(r => r.IsNew == 1).ToList();
                if (nolist != null && nolist.Count() > 0)
                {
                    list = list.Where(r => r.IsNew == 0).ToList();
                    TXCommons.PictureModular.GetPicture.RemoveRedisKey(guid, picturetype, cityId);
                    TXCommons.PictureModular.GetPicture.AddRedisKey(guid, picturetype, cityId, list);
                    foreach (TXCommons.PictureModular.PremisesPictureInfo info in nolist)
                    {
                        TXCommons.MsgQueue.SendMessage.SendDeletePictureMessage(guid, "Premises", picturetype, info.Path, 0);
                    }
                }
            }
            ViewData["list"] = list != null && list.Count > 0 ? list.OrderByDescending(r => r.ID).ToList<TXCommons.PictureModular.PremisesPictureInfo>() : list;
            return PartialView();
        }

        /// <summary>
        /// 图片信息
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public PartialViewResult PremisesPhotoAdvert(string guid, string picturetype, int cityId, string opt)
        {
            ViewData["guid"] = guid;
            ViewData["picturetype"] = picturetype;

            ViewData["cityId"] = cityId;
            List<TXCommons.PictureModular.PremisesPictureInfo> list = new List<TXCommons.PictureModular.PremisesPictureInfo>();

            list = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, false, picturetype, cityId);
            if (list != null && list.Count > 0)
            {
                List<TXCommons.PictureModular.PremisesPictureInfo> nolist = list.Where(r => r.IsNew == 1).ToList();
                if (nolist != null && nolist.Count() > 0)
                {
                    list = list.Where(r => r.IsNew == 0).ToList();
                    TXCommons.PictureModular.GetPicture.RemoveRedisKey(guid, picturetype, cityId);
                    TXCommons.PictureModular.GetPicture.AddRedisKey(guid, picturetype, cityId, list);
                    foreach (TXCommons.PictureModular.PremisesPictureInfo info in nolist)
                    {
                        TXCommons.MsgQueue.SendMessage.SendDeletePictureMessage(guid, "Premises", picturetype, info.Path, 0);
                    }
                }
            }
            ViewData["list"] = list != null && list.Count > 0 ? list.OrderByDescending(r => r.ID).ToList<TXCommons.PictureModular.PremisesPictureInfo>() : list;
            return PartialView();
        }
        /// <summary>
        /// 修改图片标题描述
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult ImageSub(string guid, string picturetype, string cityId, string data, string pid)
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
                        temp[0].PictureType = TXCommons.PictureModular.PremisesPictureType.AdvertList.ToString();

                    TXCommons.PictureModular.GetPicture.AddRedisKey(guid, TXCommons.PictureModular.PremisesPictureType.AdvertList.ToString(), int.Parse(cityId), temp);
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", int.Parse(pid), int.Parse(cityId));

                }
            }
            catch
            {

            }
            string url = "/Premises/UploadPremisesPhoto?guid=" + guid + "&picturetype=" + picturetype + "&cityId=" + cityId + "&id=" + pid;
            TempData["message"] = "保存成功";

            TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", Util.ToInt(pid), Util.ToInt(cityId));
            return new RedirectResult(url);

        }

        public void BeginUrl(string url)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.GetRequestStream();
        }

        /// <summary>
        /// 新房图片提交
        /// </summary>
        /// <param name="innerCode"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult UploadPremisesPhotoSub(string innerCode, string pid)
        {
            TXOrm.Premis p = _premissbll.GetPremisesbyId(int.Parse(pid));

            return RedirectToAction("PremisesImage/" + pid);
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
            return Content(_premissbll.GetROOMTYPEImageUseCount(id));
        }
        /// <summary>
        /// 返回平面图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetPlaneImageUseCount(string id)
        {
            return Content(_premissbll.GetPlaneImageUseCount(id));
        }

        public ActionResult UpdateROOMTYPEImageUse(string id)
        {
            return Content(_premissbll.UpdateROOMTYPEImageUse(id));
        }
        /// <summary>
        /// 返回平面图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdatePlaneImageUse(string id)
        {
            return Content(_premissbll.UpdatePlaneImageUse(id));
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
            return Content(TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(guid, false, picturetype, cityId).Count().ToString());
        }
        #endregion

        #region 删除楼盘沙盘标记
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/10 17:03:26
        /// 功能描述：删除楼盘沙盘标记
        /// </summary>
        /// <param name="Did"></param>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public ActionResult DeleteSandBox(int Did, int Pid)
        {
            TXBll.HouseData.SandTableLabelBll _sandtablebll = new TXBll.HouseData.SandTableLabelBll();
            int i = _sandtablebll.DelSandTableLabelByDeveloperIdAndPremisesId(Pid, Did);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 查询每个地区 只有一个城市
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/11 15:53:07
        /// 功能描述：同地区的楼盘名称不可重复，如重复则提示“楼盘名称已存在
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Did"></param>
        /// <returns></returns>
        public ActionResult CheckPremises(string Name, int Did)
        {
            var i = _premissbll.CheckPremises(Name, Did);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 根据楼盘编号获取正在参与活动的房源数量
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/26 16:31:42
        /// 功能描述：根据楼盘编号获取正在参与活动的房源数量
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetLivingHouseCountByPremisesId(int Id)
        {
            var i = new ActiviesUtilsBll().GetLivingHouseCountByPremisesId(Id);
            return Json(i, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region 根据楼栋编号获取正在参与活动的房源数量
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/26 16:31:42
        /// 功能描述：根据楼栋编号获取正在参与活动的房源数量
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetLivingHouseCountByBuildingId(int Id)
        {
            var i = new ActiviesUtilsBll().GetLivingHouseCountByBuildingId(Id);
            return Json(i, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 成交信息
        /// <summary>
        /// 今日成交信息
        /// </summary>
        /// <returns></returns>
        public ActionResult BargainInfo(int premisesId)
        {
            TXOrm.PremisStatisticsInfo statisticsInfo = _premissbll.GetPremisStatisticsInfo(premisesId);
            ViewData["Info"] = statisticsInfo;
            ViewData["premisesId"] = premisesId;
            return View();
        }

        /// <summary>
        /// 添加今日成交信息
        /// </summary>
        /// <param name="premisesId">楼盘ID</param>
        /// <param name="SubscribedCount">认购数</param>
        /// <param name="BargainCount">成交数</param>
        /// <returns></returns>
        public int SubBargainInfo(string PremisesId, string SubscribedCount, string BargainCount)
        {
            return _premissbll.SubBargainInfo(PremisesId, SubscribedCount, BargainCount);
        }
        #endregion
    }
}
