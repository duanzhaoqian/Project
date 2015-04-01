using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TXOrm;
using Webdiyer.WebControls.Mvc;
using TXModel.Commons;
using TXCommons.PictureModular;
using TXModel.Web;

namespace TXDevelopersWeb.Controllers
{
    public class NhBuildingController : BaseController
    {

        TXBll.HouseData.PremisesBll _premissbll = new TXBll.HouseData.PremisesBll();
        TXBll.HouseData.BuildingBll _buildingbll = new TXBll.HouseData.BuildingBll();
        TXBll.HouseData.PermitPresaleBll _permitpresalebll = new TXBll.HouseData.PermitPresaleBll();
        TXBll.HouseData.SandTableLabelBll _sandtablelabelbll = new TXBll.HouseData.SandTableLabelBll();

        #region 楼盘管理


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/13 9:52:47
        /// 功能描述：楼盘管理
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult List(int? page, int pid = 0)
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
            ViewData["CurrentMenu"] = 32;
            //int UserID = LoginUserInfo.Id;
            //int totalCount = 0;
            //int CurrentPage = Request.QueryString["pager"] == null ? 1 : Convert.ToInt32(Request.QueryString["pager"]);
            //int PremisesId = Request.QueryString["pid"] == null ? 0 : Convert.ToInt32(Request.QueryString["pid"]);
            //List<Building> list = _buildingbll.GetBuildingList(UserID, PremisesId, CurrentPage, 10, out totalCount);
            //var pagedList = new PagedList<Building>(list, CurrentPage, 10, totalCount);
            //ViewData["totalCount"] = totalCount;
            //return View("List", pagedList);

            #region 设置分页参数
            Paging<PremisesBuilding> paging = new Paging<PremisesBuilding>();
            paging.PageIndex = page.HasValue ? page.Value : 1;
            paging.PageSize = 10;
            #endregion

            #region 读取分页数据
            paging = _buildingbll.GetBuildingList(paging, LoginUserInfo.Id, pid);
            ViewData["totalCount"] = paging.TotalCount;
            PagedList<PremisesBuilding> pagelist = new PagedList<PremisesBuilding>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            #endregion

            //检查是否为Ajax请求
            if (Request.IsAjaxRequest())
                return PartialView("NhBuildingTable", pagelist);
            else
                return View(pagelist);
        }
        #endregion

        #region 发布楼栋
        public ActionResult Create(int? pid)
        {
            if (pid != null)
                ViewData["pid"] = pid;

            ViewData["CurrentMenu"] = 31;
            premises();

            //PermitPresaleList(7);
            return View();
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/28 11:34:54
        /// 功能描述：发布楼栋
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {

            Building model = new Building();

            model.DeveloperId = LoginUserInfo.Id;
            model.PremisesId = Convert.ToInt32(collection["PremisesId"]);
            model.Periods = string.IsNullOrEmpty(collection["Periods"]) ? 0 : Convert.ToInt32(collection["Periods"]);


            model.PropertyType = string.IsNullOrEmpty(collection["PropertyType"]) ? "" : collection["PropertyType"];
            model.CreateTime = DateTime.Now;
            model.Name = collection["BuildingName"];
            model.NameType = collection["TypeN"];

            model.FamilyNum = string.IsNullOrEmpty(collection["FamilyNum"]) ? 0 : Convert.ToInt32(collection["FamilyNum"]);
            model.TheFloor = string.IsNullOrEmpty(collection["TheFloor"]) ? 0 : Convert.ToInt32(collection["TheFloor"]);
            model.Underloor = string.IsNullOrEmpty(collection["Underloor"]) ? 0 : Convert.ToInt32(collection["Underloor"]);

            model.Ladder = string.IsNullOrEmpty(collection["Ladder"]) ? 0 : Convert.ToInt32(collection["Ladder"]);
            model.Renovation = string.IsNullOrEmpty(collection["Renovation"]) ? 0 : Convert.ToInt32(collection["Renovation"]);
            model.BuildingPosition = string.IsNullOrEmpty(collection["BuildingPosition"]) ? 0 : Convert.ToInt32(collection["BuildingPosition"]);

            model.OpeningTime = DateTime.Parse(collection["OpeningTime"]);
            model.OthersTime = DateTime.Parse(collection["OthersTime"]);

            model.State = string.IsNullOrEmpty(collection["State"]) ? 0 : Convert.ToInt32(collection["State"]);
            model.PNum = string.IsNullOrEmpty(collection["PNum"]) ? 0 : Convert.ToInt32(collection["PNum"]);

            model.PresaleId = string.IsNullOrEmpty(collection["PresaleId"]) ? -1 : Convert.ToInt32(collection["PresaleId"]);
            model.PermitPresale = collection["PermitPresale"];

            model.PictureData = collection["PictureData"];

            model.UpdateTime = DateTime.Now;
            model.IsDel = false;
            string PresaleName = collection["PresaleName"];

            string[] Unit = collection["UnitName"].Split(',');

            var i = 0;
            if (model.PresaleId != -1)
                i = _buildingbll.Add(model, Unit);
            else
                i = _buildingbll.Add(model, Unit, PresaleName);


            if (i > 0)
            {
                var p = _premissbll.GetPremisesbyId(model.PremisesId);
                if (p != null)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("insert", p.Id, p.CityId);
                }
                //发布成功
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("List");
            }


        }
        #endregion

        #region 联动信息
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
        /// 作者：谢江
        /// 时间：2013/12/5 10:54:54
        /// 功能描述：楼盘列表
        /// </summary>
        public ActionResult PremisList()
        {
            List<Premis> list = _premissbll.GetPremisByDeveloperId(LoginUserInfo.Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/29 16:34:31
        /// 功能描述：根据楼盘id 获取预售许可证 列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public ActionResult PermitPresaleList(int PremisesId)
        {
            List<PermitPresale> list = _permitpresalebll.GetList(PremisesId);
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
        /// 时间：2014/1/11 10:48:28
        /// 功能描述：获取沙盘楼盘列表
        /// </summary>
        /// <param name="PremisesId">楼盘ID</param>
        /// <returns></returns>
        public ActionResult GetSelectSandBoxList(int PremisesId)
        {
            var Alreadylist = _buildingbll.GetPNumList(PremisesId);
            var list = _sandtablelabelbll.GetSandList(PremisesId, LoginUserInfo.Id);
            foreach (var item in list)
            {
                if (Alreadylist.Contains(item.Id))
                {
                    item.SandBox = "selected";//用SandBox 承载
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/11 10:48:28
        /// 功能描述：获取楼盘沙盘列表
        /// </summary>
        /// <param name="PremisesId">楼盘ID</param>
        /// <returns></returns>
        public ActionResult GetSandBoxList(int PremisesId)
        {

            var list = _sandtablelabelbll.GetSandList(PremisesId, LoginUserInfo.Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 9:53:28
        /// 功能描述：获取楼栋相册
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public ActionResult GetBuildingPic(int PremisesId)
        {
            List<PremisesPictureInfo> list = null;
            var model = _premissbll.GetPremisesbyId(PremisesId);
            if (model != null)
            {
                list = GetPicture.GetPremisesPictureList(model.InnerCode, true, PremisesPictureType.Plane.ToString(), model.CityId);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 编辑楼栋


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/13 16:18:04
        /// 功能描述：
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewData["CurrentMenu"] = 31;
            var model = _buildingbll.GetEntity_ById(id);
            if (model != null)
            {
                if (model.DeveloperId != LoginUserInfo.Id)
                {
                    return RedirectToAction("List", "NhBuilding");
                }
            }
            else
            {
                return RedirectToAction("List", "NhBuilding");
            }

            premises();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id)
        {
            premises();

            var model = _buildingbll.GetEntity_ById(id);
            if (model == null)
            {
                return RedirectToAction("List", "NhBuilding");
            }
            model.Id = id;
            model.Name = collection["BuildingName"];
            model.NameType = collection["TypeN"];
            model.Periods = Convert.ToInt32(collection["Periods"]);

            model.PropertyType = string.IsNullOrEmpty(collection["PropertyType"]) ? "" : collection["PropertyType"];
            model.FamilyNum = Convert.ToInt32(collection["FamilyNum"]);
            model.TheFloor = Convert.ToInt32(collection["TheFloor"]);
            model.Underloor = Convert.ToInt32(collection["Underloor"]);
            model.Ladder = Convert.ToInt32(collection["Ladder"]);

            model.Renovation = Convert.ToInt32(collection["Renovation"]);
            model.BuildingPosition = Convert.ToInt32(collection["BuildingPosition"]);
            model.OpeningTime = DateTime.Parse(collection["OpeningTime"]);
            model.OthersTime = DateTime.Parse(collection["OthersTime"]);

            model.UpdateTime = DateTime.Now;
            model.State = Convert.ToInt32(collection["State"]);

            model.PresaleId = string.IsNullOrEmpty(collection["PresaleId"]) ? -1 : Convert.ToInt32(collection["PresaleId"]);

            model.PermitPresale = collection["PermitPresale"];
            model.PNum = Convert.ToInt32(collection["PNum"]);
            model.PictureData = collection["PictureData"];
            string PresaleName = collection["PresaleName"];

            string[] Unit = collection["UnitName"].Split(',');


            var f = false;
            if (string.IsNullOrEmpty(PresaleName) || PresaleName == "若选项中没有请在此输入预售许可证号")
                f = _buildingbll.Update(model, Unit);
            else
                f = _buildingbll.Update(model, Unit, PresaleName);

            if (f)
            {
                var p = _premissbll.GetPremisesbyId(model.PremisesId);
                if (p != null)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", p.Id, p.CityId);
                }
                //编辑成功
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("List");
            }
        }
        #endregion


        #region 楼栋名称在同楼盘名称下不能相重
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/27 16:03:07
        /// 功能描述：楼栋名称在同楼盘名称下不能相重
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="NameType"></param>
        /// <returns></returns>
        public ActionResult CheckBuilding(string Name, string NameType, int PremisesId)
        {
            var i = _buildingbll.CheckBuilding(Name, NameType, PremisesId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 根据楼盘ID 查询楼盘状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/24 16:04:51
        /// 功能描述：根据楼盘ID 查询楼盘状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetPremisesState(int id)
        {
            var i = _premissbll.GetPremisesState(id);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取开发商楼盘列表
        /// <summary>
        /// 获取开发商楼盘列表
        /// </summary>
        public ActionResult GetPremises()
        {
            List<Premis> list = _premissbll.GetPremisByDeveloperId(LoginUserInfo.Id);
            //List<Premis> list = _premissbll.GetPremisByDeveloperId(LoginUserInfo.Id)
            if (list != null)
                list.Reverse();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}
