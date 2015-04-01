using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TXOrm;
using Webdiyer.WebControls.Mvc;
using TXModel.Commons;
using TXCommons.PictureModular;
using TXModel.Web;
using ServiceStack;

namespace TXDevelopersWeb.Controllers
{
    public class NHouseController : BaseController
    {
        TXBll.HouseData.PremisesBll _premissbll = new TXBll.HouseData.PremisesBll();
        TXBll.HouseData.HouseBll _housebll = new TXBll.HouseData.HouseBll();
        TXBll.HouseData.UnitBll _unitbll = new TXBll.HouseData.UnitBll();
        TXBll.HouseData.BuildingBll _buildingbll = new TXBll.HouseData.BuildingBll();

        TXBll.HouseData.HouseTemplateBll _housetemplatebll = new TXBll.HouseData.HouseTemplateBll();

        #region 房源列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/6 15:08:56
        /// 功能描述：房源列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int? page, int rid = 1, int pid = 0, int bid = 0, int uid = 0, int fid = 0, int sid = -1, int order = 1)
        {
            if (pid == 0)
            {
                List<Premis> list = _premissbll.GetPremisByDeveloperId(LoginUserInfo.Id);
                if (list != null)
                {
                    list.Reverse();
                    pid = list.FirstOrDefault().Id;
                }
                else
                {
                    list = new List<Premis>();
                    list.Add(new Premis { Id = 0, Name = "暂无楼盘" });
                }
            }

            ViewData["CurrentMenu"] = 42;

            #region 筛选条件
            ViewData["rid"] = rid;
            ViewData["pid"] = pid;
            ViewData["bid"] = bid;
            ViewData["uid"] = uid;
            ViewData["fid"] = fid;
            ViewData["sid"] = sid;
            ViewData["order"] = order;
            #endregion

            #region 设置分页参数
            Paging<HouseAndBuilding> paging = new Paging<HouseAndBuilding>();
            paging.PageIndex = page.HasValue ? page.Value : 1;
            paging.PageSize = 10;
            #endregion

            if (rid == 0)
            {
                //未发布
                paging = _housebll.GetHouseList(paging, LoginUserInfo.Id, pid, bid, uid, fid, sid, false, order);
            }
            else if (rid == 1)
            {
                //已发布
                paging = _housebll.GetHouseList(paging, LoginUserInfo.Id, pid, bid, uid, fid, sid, true, order);
            }
            else if (rid == 2)
            {
                //已删除
                paging = _housebll.GetHouseList(paging, LoginUserInfo.Id, pid, bid, uid, fid, sid);

            }
            else
            {
                paging = _housebll.GetHouseList(paging, LoginUserInfo.Id, pid, bid, uid, fid, sid, false, order);
            }

            #region 读取分页数据

            ViewData["totalCount"] = paging.TotalCount;
            PagedList<HouseAndBuilding> pagelist = new PagedList<HouseAndBuilding>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            #endregion

            //检查是否为Ajax请求
            if (Request.IsAjaxRequest())
                return PartialView("HouseTable", pagelist);
            else
                return View(pagelist);
        }

        #endregion

        /// <summary>
        /// 检查房号是否重复
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckHouseName(string houseName, int buildingId, int unitId)
        {
            House entity = new House();
            entity.Name = houseName;
            entity.BuildingId = buildingId;
            entity.UnitId = unitId;
            var result = _housebll.GetHouseNameCountByIdAndName(entity);
            if (result == 0)
                return Json(true);
            else
                return Json(false);
        }
        /// <summary>
        /// 检查房号是否重复(更新信息)
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckHouseName_Update(int hid, string houseName, int buildingId, int unitId)
        {
            House entity = new House();
            entity.Id = hid;
            entity.Name = houseName;
            entity.BuildingId = buildingId;
            entity.UnitId = unitId;
            var result = _housebll.GetHouseNameCountByIdAndName_Update(entity);
            if (result == 0)
                return Json(true);
            else
                return Json(false);
        }

        #region 发布房源
        public ActionResult Create(int? pid = -1, int? bid = -1)
        {
            ViewData["pid"] = pid;
            ViewData["bid"] = bid;
            ViewData["CurrentMenu"] = 41;
            premises();
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            premises();

            #region 房源参数
            House model = new House();
            model.Name = collection["HouseName"];
            model.InnerCode = Guid.NewGuid().ToString();
            model.DeveloperId = LoginUserInfo.Id;
            model.PremisesId = string.IsNullOrEmpty(collection["PremisesId"]) ? 0 : Convert.ToInt32(collection["PremisesId"]);
            model.BuildingId = string.IsNullOrEmpty(collection["BuildingId"]) ? 0 : Convert.ToInt32(collection["BuildingId"]);
            model.UnitId = string.IsNullOrEmpty(collection["UnitId"]) ? 0 : Convert.ToInt32(collection["UnitId"]);
            model.Floor = string.IsNullOrEmpty(collection["Floor"]) ? 0 : Convert.ToInt32(collection["Floor"]);
            model.RId = string.IsNullOrEmpty(collection["RId"]) ? 0 : Convert.ToInt32(collection["RId"]);
            model.BuildingType = string.IsNullOrEmpty(collection["BuildingType"]) ? 0 : Convert.ToInt32(collection["BuildingType"]);
            model.Orientation = string.IsNullOrEmpty(collection["Orientation"]) ? 0 : Convert.ToInt32(collection["Orientation"]);
            model.PriceType = string.IsNullOrEmpty(collection["PriceType"]) ? 0 : Convert.ToInt32(collection["PriceType"]);
            model.TotalPrice = string.IsNullOrEmpty(collection["TotalPrice"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["TotalPrice"]));
            model.SinglePrice = string.IsNullOrEmpty(collection["SinglePrice"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["SinglePrice"]));
            model.DownPayment = string.IsNullOrEmpty(collection["DownPayment"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["DownPayment"]));
            model.SalesStatus = string.IsNullOrEmpty(collection["SalesStatus"]) ? 0 : Convert.ToInt32(collection["SalesStatus"]);
            model.BuildingArea = string.IsNullOrEmpty(collection["BuildingArea"]) ? 0 : double.Parse(collection["BuildingArea"]);
            model.Area = string.IsNullOrEmpty(collection["Area"]) ? 0 : double.Parse(collection["Area"]);

            model.Room = string.IsNullOrEmpty(collection["Room"]) ? 0 : Convert.ToInt32(collection["Room"]);
            model.Hall = string.IsNullOrEmpty(collection["Hall"]) ? 0 : Convert.ToInt32(collection["Hall"]);
            model.Toilet = string.IsNullOrEmpty(collection["Toilet"]) ? 0 : Convert.ToInt32(collection["Toilet"]);

            model.Kitchen = string.IsNullOrEmpty(collection["Kitchen"]) ? 0 : Convert.ToInt32(collection["Kitchen"]);

            model.RId = string.IsNullOrEmpty(collection["RId"]) ? 0 : Convert.ToInt32(collection["RId"]);
            model.PictureData = "";
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.IsDel = false;


            if (!string.IsNullOrEmpty(collection["IsRelease"]))
                model.IsRelease = true;
            else
                model.IsRelease = false;




            #endregion

            #region 房源模版

            HouseTemplate ht = null;
            if (!string.IsNullOrEmpty(collection["SaveHouseTemplate"]))
            {
                ht = new HouseTemplate();

                if (!string.IsNullOrEmpty(collection["TitleTemplate"]))
                {
                    ht.Title = collection["TitleTemplate"];
                }
                else
                {
                    ht.Title = collection["PremisesName"] + " " + collection["BuildingName"] + collection["UnitName"] + " " + model.Room + "室"
                    + model.Hall + "厅" + model.Toilet + "卫" + model.Kitchen + "厨";
                }


                ht.Remark = collection["RemarkTemplate"];

                ht.IsDel = false;
                ht.CreateTime = DateTime.Now;
                ht.UpdateTime = DateTime.Now;
                ht.Content = TXCommons.Util.SerializeObject<House>(model);
            }
            #endregion

            #region 房间位置标注

            string HouseCoordinate = collection["HouseCoordinate"];

            if (!string.IsNullOrEmpty(HouseCoordinate))
            {
                List<House> list = TXCommons.Util.JSONStringToList<House>(HouseCoordinate);
                foreach (var item in list)
                {
                    model.CoordX = item.CoordX;
                    model.CoordY = item.CoordY;
                }
            }

            #endregion

            int i = _housebll.Add(model, ht);
            if (i > 0)
            {

                //发布成功 发消息
                if (model.IsRelease)
                {
                    var p = _premissbll.GetPremisesbyId(model.PremisesId);
                    if (p != null)
                    {
                        TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("insert", model.PremisesId, p.CityId);
                        TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("insert", model.PremisesId, p.CityId);//楼盘消息
                        string key = string.Format("sizechart_{0}_{1},", model.BuildingArea, model.RId);
                        TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, p.CityId, key.TrimEnd(','));
                    }
                }


                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("List");
            }

        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/28 9:42:12
        /// 功能描述：开发商楼盘列表
        /// </summary>
        public void premises()
        {
            List<SelectListItem> item = new List<SelectListItem>();

            List<Premis> list = _premissbll.GetPremisByDeveloperId(LoginUserInfo.Id);
            item.Add(new SelectListItem { Text = "请选择楼盘", Value = "-1" });
            if (list != null)
            {
                foreach (var model in list)
                {
                    item.Add(new SelectListItem { Text = model.Name, Value = model.Id.ToString() });
                }
            }

            this.ViewData["PremisList"] = item;
        }

        /// <summary>
        /// 获取地上楼层
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public ActionResult TheFloor(int BuildingId)
        {
            var f = _buildingbll.GetTheFloor(BuildingId);

            return Json(f, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 获取地下楼层
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public ActionResult Underloor(int BuildingId)
        {
            var f = _buildingbll.GetUnderloor(BuildingId);
            return Json(f, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/2 10:43:50
        /// 功能描述：获取 单元列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public ActionResult GetUnitList(int PremisesId, int BuildingId)
        {
            List<Unit> list = _unitbll.GetUnitList(LoginUserInfo.Id, PremisesId, BuildingId);
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/2 10:43:50
        /// 功能描述：获取 单元列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public ActionResult SearchUnitList(int PremisesId, int BuildingId)
        {
            List<Unit> list = _unitbll.GetUnitList(LoginUserInfo.Id, PremisesId, BuildingId);
            list.Add(new Unit { Id = 0, Name = "选择单元" });
            list.Reverse();
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/18 15:47:22
        /// 功能描述：根据开发商Id 获取房源模板列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHouseTemplate()
        {
            List<HouseTemplate> list = _housetemplatebll.GetHouseTemplateByDeveloperId(LoginUserInfo.Id);
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据Id 选择模版
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult SelectHouseTemplate(int Id)
        {
            HouseTemplate model = _housetemplatebll.GetHouseTemplateById(Id);
            return this.Json(model, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/9 10:57:02
        /// 功能描述：房源销售状态列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSalesStatusList()
        {
            List<string> list = new List<string>(){
               "请选择","待售","开发商保留","在售","已认购","已签约","已备案","已办产权","被限制","拆迁安置","售罄"
            };
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 9:53:28
        /// 功能描述：获取户型图
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public ActionResult GetHousePic(int PremisesId)
        {
            List<PremisesPictureInfo> list = null;
            var model = _premissbll.GetPremisesbyId(PremisesId);
            if (model != null)
            {
                list = GetPicture.GetPremisesPictureList(model.InnerCode, true, PremisesPictureType.ROOMTYPE.ToString(), model.CityId);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/5 10:54:54
        /// 功能描述：楼盘列表
        /// </summary>
        public ActionResult PremisList()
        {
            List<Premis> list = _premissbll.GetPremisByDeveloperId(LoginUserInfo.Id);
            //list.Add(new Premis { Id = 0, Name = "选择楼盘" });
            if (list != null)
                list.Reverse();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/29 16:36:50
        /// 功能描述：根据楼盘ID 获取楼栋列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public ActionResult SearchBuildingList(int PremisesId)
        {
            List<Building> list = _buildingbll.GetBuildingByPremisesId(PremisesId, LoginUserInfo.Id);
            list = list ?? new List<Building>();
            list.Add(new Building { Id = 0, Name = "选择楼栋" });
            list.Reverse();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/29 16:36:50
        /// 功能描述：根据楼盘ID 获取楼栋列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public ActionResult BuildingList(int PremisesId)
        {
            List<Building> list = _buildingbll.GetBuildingByPremisesId(PremisesId, LoginUserInfo.Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/16 14:33:25
        /// 功能描述：查询楼栋平面图
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public ActionResult SelectBuildingPic(int PremisesId, int BuildingId)
        {
            string PicID = string.Empty;
            string PicSrc = string.Empty;
            List<PremisesPictureInfo> list = null;
            var model = _premissbll.GetPremisesbyId(PremisesId);
            var buildModel = _buildingbll.GetEntity_ById(BuildingId);
            if (buildModel != null)
            {
                PicID = buildModel.PictureData;
            }
            if (model != null)
            {
                list = GetPicture.GetPremisesPictureList(model.InnerCode, true, PremisesPictureType.Plane.ToString(), model.CityId);
                foreach (var item in list)
                {
                    if (PicID == item.ID.ToString())
                    {
                        PicSrc = item.Path;
                    }
                }
            }
            return Json(PicSrc, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 获取楼栋销售状态
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public ActionResult GetState(int BuildingId)
        {
            var s = _buildingbll.GetBuildingState(BuildingId);
            return Json(s, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 编辑房源
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/16 10:48:34
        /// 功能描述：编辑房源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewData["CurrentMenu"] = 40;
            var model = _housebll.GetHouseModel(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }
            else
            {
                if (model.DeveloperId != this.LoginUserInfo.Id)
                {
                    return RedirectToAction("List");
                }
            }
            premises();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            ViewData["CurrentMenu"] = 40;
            var model = _housebll.GetHouseModel(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }

            #region 房源参数
            model.Name = collection["HouseName"];
            model.InnerCode = Guid.NewGuid().ToString();
            model.PremisesId = string.IsNullOrEmpty(collection["PremisesId"]) ? 0 : Convert.ToInt32(collection["PremisesId"]);
            model.BuildingId = string.IsNullOrEmpty(collection["BuildingId"]) ? 0 : Convert.ToInt32(collection["BuildingId"]);
            model.UnitId = string.IsNullOrEmpty(collection["UnitId"]) ? 0 : Convert.ToInt32(collection["UnitId"]);
            model.Floor = string.IsNullOrEmpty(collection["Floor"]) ? 0 : Convert.ToInt32(collection["Floor"]);
            model.RId = string.IsNullOrEmpty(collection["RId"]) ? 0 : Convert.ToInt32(collection["RId"]);
            model.BuildingType = string.IsNullOrEmpty(collection["BuildingType"]) ? 0 : Convert.ToInt32(collection["BuildingType"]);
            model.Orientation = string.IsNullOrEmpty(collection["Orientation"]) ? 0 : Convert.ToInt32(collection["Orientation"]);
            model.PriceType = string.IsNullOrEmpty(collection["PriceType"]) ? 0 : Convert.ToInt32(collection["PriceType"]);
            model.TotalPrice = string.IsNullOrEmpty(collection["TotalPrice"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["TotalPrice"]));
            model.SinglePrice = string.IsNullOrEmpty(collection["SinglePrice"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["SinglePrice"]));
            model.DownPayment = string.IsNullOrEmpty(collection["DownPayment"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["DownPayment"]));
            model.SalesStatus = string.IsNullOrEmpty(collection["SalesStatus"]) ? 0 : Convert.ToInt32(collection["SalesStatus"]);
            model.BuildingArea = string.IsNullOrEmpty(collection["BuildingArea"]) ? 0 : double.Parse(collection["BuildingArea"]);
            model.Area = string.IsNullOrEmpty(collection["Area"]) ? 0 : double.Parse(collection["Area"]);

            model.Room = string.IsNullOrEmpty(collection["Room"]) ? 0 : Convert.ToInt32(collection["Room"]);
            model.Hall = string.IsNullOrEmpty(collection["Hall"]) ? 0 : Convert.ToInt32(collection["Hall"]);
            model.Toilet = string.IsNullOrEmpty(collection["Toilet"]) ? 0 : Convert.ToInt32(collection["Toilet"]);

            model.Kitchen = string.IsNullOrEmpty(collection["Kitchen"]) ? 0 : Convert.ToInt32(collection["Kitchen"]);
            model.PictureData = string.IsNullOrEmpty(collection["PictureData"]) ? "" : collection["PictureData"];
            model.UpdateTime = DateTime.Now;
            //model.IsDel = false;

            if (!string.IsNullOrEmpty(collection["IsRelease"]))
                model.IsRelease = true;
            else
                model.IsRelease = false;

            #endregion

            #region 房间位置标注

            string HouseCoordinate = collection["HouseCoordinate"];

            if (!string.IsNullOrEmpty(HouseCoordinate))
            {
                List<House> list = TXCommons.Util.JSONStringToList<House>(HouseCoordinate);
                foreach (var item in list)
                {
                    model.CoordX = item.CoordX;
                    model.CoordY = item.CoordY;
                }
            }

            #endregion

            var i = _housebll.Update(model);
            if (i == 0)
            {
                ViewData["msg"] = 0;
            }
            else if (i == 1)
            {
                //ModelState.AddModelError("msg", "修改房源成功");
                ViewData["msg"] = 1;
            }
            if (i == 2)
            {
                //ModelState.AddModelError("msg", "此房源正在进行活动");
                ViewData["msg"] = 2;
            }

            //更新消息
            var p = _premissbll.GetPremisesbyId(model.PremisesId);
            if (p != null)
            {
                TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("update", model.PremisesId, p.CityId);
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", model.PremisesId, p.CityId);//楼盘
                string key = string.Format("sizechart_{0}_{1},", model.BuildingArea, model.RId);
                TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, p.CityId, key.TrimEnd(','));
            }

            premises();
            return View(model);
        }

        #endregion

        #region 房源模版列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/16 17:05:36
        /// 功能描述：房源模版列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult TemplateList(int? page)
        {
            ViewData["CurrentMenu"] = 43;

            #region 设置分页参数
            Paging<HouseTemplate> paging = new Paging<HouseTemplate>();
            paging.PageIndex = page.HasValue ? page.Value : 1;
            paging.PageSize = 10;
            #endregion

            #region 读取分页数据
            paging = _housetemplatebll.GetHouseTemplateList(paging, LoginUserInfo.Id);
            ViewData["totalCount"] = paging.TotalCount;
            PagedList<HouseTemplate> pagelist = new PagedList<HouseTemplate>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            #endregion

            //检查是否为Ajax请求
            if (Request.IsAjaxRequest())
                return PartialView("HouseTemplateTable", pagelist);
            else
                return View(pagelist);
        }
        #endregion

        #region 弹出层

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 17:27:21
        /// 功能描述：模板
        /// </summary>
        /// <returns></returns>
        public ActionResult AddTemplate()
        {
            return PartialView("AddTemplate", null);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/2 15:58:49
        /// 功能描述：房间位置标注
        /// </summary>
        /// <returns></returns>
        public ActionResult RoomMarked()
        {
            return PartialView("RoomMarked", null);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/15 16:16:45
        /// 功能描述：更改销售状态弹出层
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateSate()
        {
            return PartialView("HouseIsRelease", null);
        }


        #endregion

        #region 编辑模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/25 14:56:17
        /// 功能描述：编辑模版
        /// </summary>
        /// <returns></returns>
        public ActionResult TemplateEdit(int id)
        {
            ViewData["CurrentMenu"] = 40;
            var HouseTemplate = _housetemplatebll.GetHouseTemplateById(id);
            if (HouseTemplate == null)
            {
                return RedirectToAction("TemplateList");
            }
            var model = TXCommons.Util.DeserializeObject<House>(HouseTemplate.Content);
            premises();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TemplateEdit(int id, FormCollection collection)
        {
            ViewData["CurrentMenu"] = 40;
            var HouseTemplate = _housetemplatebll.GetHouseTemplateById(id);
            if (HouseTemplate == null)
            {
                return RedirectToAction("TemplateList");
            }


            #region 房源参数
            House model = new House();
            model.Name = collection["HouseName"];
            model.InnerCode = Guid.NewGuid().ToString();
            model.DeveloperId = LoginUserInfo.Id;
            model.PremisesId = string.IsNullOrEmpty(collection["PremisesId"]) ? 0 : Convert.ToInt32(collection["PremisesId"]);
            model.BuildingId = string.IsNullOrEmpty(collection["BuildingId"]) ? 0 : Convert.ToInt32(collection["BuildingId"]);
            model.UnitId = string.IsNullOrEmpty(collection["UnitId"]) ? 0 : Convert.ToInt32(collection["UnitId"]);
            model.Floor = string.IsNullOrEmpty(collection["Floor"]) ? 0 : Convert.ToInt32(collection["Floor"]);
            model.RId = string.IsNullOrEmpty(collection["RId"]) ? 0 : Convert.ToInt32(collection["RId"]);
            model.BuildingType = string.IsNullOrEmpty(collection["BuildingType"]) ? 0 : Convert.ToInt32(collection["BuildingType"]);
            model.Orientation = string.IsNullOrEmpty(collection["Orientation"]) ? 0 : Convert.ToInt32(collection["Orientation"]);
            model.PriceType = string.IsNullOrEmpty(collection["PriceType"]) ? 0 : Convert.ToInt32(collection["PriceType"]);
            model.TotalPrice = string.IsNullOrEmpty(collection["TotalPrice"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["TotalPrice"]));
            model.SinglePrice = string.IsNullOrEmpty(collection["SinglePrice"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["SinglePrice"]));
            model.DownPayment = string.IsNullOrEmpty(collection["DownPayment"]) ? 0 : decimal.Parse(string.Format("{0:F2}", collection["DownPayment"]));
            model.SalesStatus = string.IsNullOrEmpty(collection["SalesStatus"]) ? 0 : Convert.ToInt32(collection["SalesStatus"]);
            model.BuildingArea = string.IsNullOrEmpty(collection["BuildingArea"]) ? 0 : double.Parse(collection["BuildingArea"]);
            model.Area = string.IsNullOrEmpty(collection["Area"]) ? 0 : double.Parse(collection["Area"]);

            model.RId = string.IsNullOrEmpty(collection["RId"]) ? 0 : Convert.ToInt32(collection["RId"]);
            model.Room = string.IsNullOrEmpty(collection["Room"]) ? 0 : Convert.ToInt32(collection["Room"]);
            model.Hall = string.IsNullOrEmpty(collection["Hall"]) ? 0 : Convert.ToInt32(collection["Hall"]);
            model.Toilet = string.IsNullOrEmpty(collection["Toilet"]) ? 0 : Convert.ToInt32(collection["Toilet"]);

            model.Kitchen = string.IsNullOrEmpty(collection["Kitchen"]) ? 0 : Convert.ToInt32(collection["Kitchen"]);
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.IsDel = false;
            model.IsRelease = false;

            #endregion

            #region 房源模版

            HouseTemplate ht = new HouseTemplate();
            ht.DeveloperId = LoginUserInfo.Id;
            ht.Id = id;

            ht.Title = collection["PremisesName"] + " " + collection["BuildingName"] + collection["UnitName"] + " " + model.Room + "室"
             + model.Hall + "厅" + model.Toilet + "卫" + model.Kitchen + "厨";

            ht.UpdateTime = DateTime.Now;
            ht.Content = TXCommons.Util.SerializeObject<House>(model);

            #endregion

            var i = _housetemplatebll.update(ht);
            if (i > 0)
                ViewData["msg"] = 1;//保存成功
            else
                ViewData["msg"] = 0;//保存失败


            premises();
            return View(model);
        }

        #endregion

        #region 批量更改房源状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/15 15:32:29
        /// 功能描述：更改发布状态
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public string UpdateIsRelease(string Ids)
        {
            int i = _housebll.UpdateHouse_IsRelease(Ids, 1, LoginUserInfo.Id);
            return IsCallBack(i.ToString());
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/15 16:48:40
        /// 功能描述：更改销售状态
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="IsRelease"></param>
        /// <returns></returns>
        public string UpdateSalesStatus(string Ids, int SalesStatus)
        {
            Ids = Ids.Substring(0, Ids.Length - 1);//去掉最后一个逗号
            int i = _housebll.UpdateHouse_SalesStatus(Ids, SalesStatus, LoginUserInfo.Id);
            return IsCallBack(i.ToString());
        }

        #endregion

        #region 检查是否有回调函数callback方法，如果有则加上，没有则原样返回
        /// <summary>
        /// 检查是否有回调函数callback方法，如果有则加上，没有则原样返回
        /// </summary>
        /// <param name="result">返回值</param>
        /// <returns></returns>
        public string IsCallBack(string result)
        {
            string fun = Request["callback"];
            if (String.IsNullOrEmpty(fun))
                //返回json格式数据
                return result;
            else
                //返回jsonp格式数据，主要用于静态页跨域请求
                return String.Format("{0}({1})", fun, "{\"value\":\"{0}\"}".Replace("{0}", result));
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
        public JsonResult UpdateHouseSalesStatusByIds(string ids, int state)
        {
            if (!string.IsNullOrWhiteSpace(ids))
            {
                try
                {

                    var notHouseList = _housebll.CheckNotUpdateHouses(ids);
                    if (notHouseList != null && notHouseList.Count() > 0)
                    {
                        string msg = notHouseList.Aggregate(string.Empty, (current, m) => current + (m.Name + ","));
                        return Json(new { result = false, message = "房号（" + msg.TrimEnd(',') + "）正在参加活动，状态不能进行修改。" });
                    }
                    if (notHouseList != null && notHouseList.Count() <= 0)
                    {
                        #region 可以更新
                        int i = _housebll.UpdateHouseSalesStatusByIds(ids, state);
                        if (i > 0)
                        {
                            //发送消息
                            foreach (string s in ids.Split(','))
                            {
                                int cid = _housebll.GetCityIDByHouseID(int.Parse(s));
                                var model = _housebll.GetHouseModel(int.Parse(s));
                                if (model != null)
                                {
                                    TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("update", model.PremisesId, cid);
                                }
                            }
                            return Json(new { result = true, message = "更改成功" });
                        }
                        return Json(new { result = false, message = "操作失败。" });
                        #endregion
                    }
                    return Json(new { result = false, message = "操作失败。" });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = "操作失败：" + ex.Message });
                }

            }
            return Json(new { result = false, message = "无id信息，操作失败。" });
        }
        #endregion


        #region 批量删除房源
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/17 13:25:50
        /// 功能描述：批量删除房源
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public JsonResult DeleteHouseByIds(string ids)
        {
            if (!string.IsNullOrWhiteSpace(ids))
            {
                try
                {

                    var notHouseList = _housebll.CheckNotUpdateHouses(ids);
                    if (notHouseList != null && notHouseList.Count() > 0)
                    {
                        string msg = notHouseList.Aggregate(string.Empty, (current, m) => current + (m.Name + ","));
                        return Json(new { result = false, message = "房号（" + msg.TrimEnd(',') + "）房源有营销活动进行中，不能删除。" });
                    }
                    //可以删除
                    int i = _housebll.DeleteHouseByIds(ids);
                    if (i > 0)
                    {
                        //发送消息
                        foreach (string s in ids.Split(','))
                        {
                            int cid = _housebll.GetCityIDByHouseID(int.Parse(s));
                            //发消息
                            var model = _housebll.GetHouseModel(int.Parse(s));
                            if (model != null)
                            {
                                TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("delete", int.Parse(s), cid);
                                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", model.PremisesId, cid);
                            }
                        }
                        return Json(new { result = true, message = "删除成功" });
                    }

                    return Json(new { result = false, message = "操作失败。" });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = "操作失败：" + ex.Message });
                }

            }
            return Json(new { result = false, message = "无id信息，操作失败。" });
        }

        #endregion

        #region 删除房源 单个删除

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/20 16:33:05
        /// 功能描述： 删除房源 单个删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public ActionResult DeleteHouseById(int id)
        {
            var i = _housebll.DelHouseByID(id);
            if (i == 1)
            {
                //发消息
                var model = _housebll.GetHouseModel(id);

                TXCommons.MsgQueue.SendMessage.SendRoomIndexMessage("delete", id, _housebll.GetCityIDByHouseID(id));
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", model.PremisesId, _housebll.GetCityIDByHouseID(id));

                return Json(new { result = true, message = "删除成功" });
            }
            else if (i == 2)
            {
                return Json(new { result = true, message = "此房源正在进行活动" });
            }
            else
            {
                return Json(new { result = true, message = "删除失败" });
            }

        }

        #endregion

        #region 删除模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/20 17:46:41
        /// 功能描述：删除模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteTemplateById(int id)
        {
            var i = _housetemplatebll.DelHouseTemplateByID(id);
            if (i == 1)
            {
                return Json(new { result = true, message = "删除模版成功" });
            }
            else
            {
                return Json(new { result = true, message = "删除模版失败" });
            }

        }
        #endregion
    }
}
