using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using KYJ.ZS.BLL.Adverts;
using KYJ.ZS.BLL.Generalizes;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.Commons;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Generalizes;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.Manager.Controllers
{

    /// <summary>
    /// 作者：孟国栋
    ///  时间：2014-06-05
    ///  描述：管理商品排序
    /// </summary>
    public class GeneralizeController : BaseController
    {
        private GeneralizeManageBll bll = new GeneralizeManageBll();
        AdvertManageBll advertBll = new AdvertManageBll();
        #region 商品排序展示
        /// <summary>
        /// 商品排序的浏览
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="stateId">状态id</param>
        /// <param name="name">搜索名称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GeneralizeShow(int? pageIndex, int? stateId, string name, DateTime? beginTime, DateTime? endTime)
        {
            //
            GeneralizeSearchEntity generalizeSearch = new GeneralizeSearchEntity();
            //页码参数为空默认显示第一页
            if (!pageIndex.HasValue)
                pageIndex = 1;
            generalizeSearch.State = stateId == null ? (int)GeneralizeSate.NoAduit : stateId.Value;
            generalizeSearch.NameSearch = name;
            generalizeSearch.BeginTime = beginTime;
            generalizeSearch.EndTime = endTime;
            PageData<Generalize> pageData = bll.GetGeneralizeDataNoAduit(pageIndex.Value, generalizeSearch);
            // 后一页无数据情况
            if (pageData.DataList == null || pageData.DataList.Count <= 0 && pageIndex > 1)
            {
                pageIndex -= 1;
                pageData = bll.GetGeneralizeDataNoAduit(pageIndex.Value, generalizeSearch);
            }
            ViewData["pageData"] = pageData;
            ViewData["State"] = (GeneralizeSate)generalizeSearch.State;
            ViewData["name"] = name;
            ViewData["beginTime"] = beginTime;
            ViewData["endTime"] = endTime;
            return View();

        }
        #endregion
        #region 商品排序修改
        /// <summary>
        /// 修改商品排序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GeneralizeUpdate(int id)
        {
            List<Category> listCategory = new List<Category>();
            //获取位置信息
            Manager_GeneralizeLocationEntity location = bll.GetLacationById(id);
            //获取分类信息
            listCategory = bll.GetParentCategoryName(location.CategoryId.Value, listCategory);
            //获取排序信息
            Generalize generalize = bll.GetGeneralizeInfoById(id);
            List<Manager_RentalGoodsAduitEntity> list = bll.GetGeneralizeGoods(id);
            AjaxSearchGet(null, null,null);
            ViewData["location"] = location;
            ViewData["listCategory"] = listCategory;
            ViewData["generalize"] = generalize;
            ViewData["goodsList"] = list;
            return View();
        }
        /// <summary>
        /// 修改商品排序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GeneralizeUpdate(int id, object obj)
        {
            Generalize generalize = new Generalize();
            Manager_RentalGoodsAduitEntity rentalGoods = new Manager_RentalGoodsAduitEntity();
            string state = Request.Form["state"];
            string title = Request.Form["title"];
            string remark = Request.Form["remark"];
            string beginTime = Request.Form["beginTime"];
            string endTime = Request.Form["endTime"];
            string goodssort = Request.Form["goodssort"];
            int adminId = _ServiceContext.CurrentUser.UserID;
            string adminName = _ServiceContext.CurrentUser.RealName;
            bll.DelelteGoods(id);
            
                if (state == "1"&&title!=null&&remark!=null&&beginTime!=null&&endTime!=null)
                {
                    generalize.GeneralizeId = id;
                    generalize.Name = title;
                    generalize.Remark = remark;
                    generalize.BeginTime =DateTime.Parse(beginTime) ;
                    generalize.EndTime = DateTime.Parse(endTime);
                    generalize.AdminId = adminId;
                    generalize.AdminName = adminName;
                    generalize.AduitState = 2;
                    generalize.State = 1;
                    bll.UpdateGeneralize(generalize);
                    SetRentalEntity(rentalGoods,goodssort,generalize.GeneralizeId);
                    // 日志记录
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                    logEntity.ModuleId = (int)ManagerNavigation.GUANLISHANGPINPAIXU;
                    logEntity.ModuleName = "商品排序管理";
                    logEntity.Remark = "修改商品排序" +generalize.Name;
                    new LogBll().CreateLog(logEntity);
                    return RedirectToAction("GeneralizeShow");
                }
                else
                {
                    generalize.GeneralizeId = id;
                    generalize.Name = title;
                    generalize.Remark = remark;
                    generalize.BeginTime = DateTime.Parse(beginTime);
                    generalize.EndTime = DateTime.Parse(endTime);
                    generalize.AdminId = adminId;
                    generalize.AdminName = adminName;
                    generalize.AduitState = 1;
                    generalize.State = 1;
                    bll.UpdateGeneralize(generalize);
                    SetRentalEntity(rentalGoods, goodssort, generalize.GeneralizeId);
                    // 日志记录
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                    logEntity.ModuleId = (int)ManagerNavigation.GUANLISHANGPINPAIXU;
                    logEntity.ModuleName = "商品排序管理";
                    logEntity.Remark = "修改商品排序" +generalize.Name;
                    new LogBll().CreateLog(logEntity);
                    return RedirectToAction("GeneralizeShow");
                }
        }
        #endregion
        #region 获取该位置允许发布商品数量 +ActionResult MaxNum(int? locationId)
        /// <summary>
        /// 获取该位置允许发布商品数量
        /// </summary>
        /// <param name="locationId">商品位置ID</param>
        /// <returns></returns>
        public ActionResult MaxNum(int? locationId)
        {
            BackMessge bm = new BackMessge();
            var result = true;
            bm.Message = bll.GetLocationEntityById(locationId.Value).Number.ToString();
            bm.Success = result;
            return Json(bm);
        }
        #endregion
        #region 获取位置 +JsonResult GetLocation(int advertTypeId, int? categoryId)
        /// <summary>
        /// 获取位置
        /// </summary>
        /// <param name="advertTypeId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public JsonResult GetLocation(int generalizeTypeId, int? categoryId)
        {
            try
            {
                Manager_GeneralizeLocationEntity location = new Manager_GeneralizeLocationEntity();
                location.GeneralizeTypeId = generalizeTypeId;
                location.CategoryId = categoryId;
                var list = bll.GetLocation(location).ToArray();
                return Json(new { success = true, items = list }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        /// <summary>
        /// 添加商品排序
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GeneralizeAdd()
        {
            GeneralizeRentalGoodsSearchEntity generalizeRentalGoodsSearch = new GeneralizeRentalGoodsSearchEntity();

            //获取页面数据
        //    List<AdvertTypes> list = advertBll.GetAdvertTypesData();
            List<GeneralizeTypes> list = bll.GetGeneralizeTypesData();
            ViewBag.pageData = list;
            //向视图传参
            ViewBag.categoryName = null;   //父分类名称
            ViewBag.locationEntity = null;
            GeneralizeRentalGoodsSearchEntity searchEntity = new GeneralizeRentalGoodsSearchEntity();
            searchEntity.GoodsName = null;
            searchEntity.MerchantName = null;
            searchEntity.GoodsCode = null;
            PageData<Manager_RentalGoodsAduitEntity> pageData = bll.GetGeneralizeGoods(1, searchEntity);
            ViewData["goodsList"] = pageData;
            return View();
        }
        /// <summary>
        /// 添加商品排序
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GeneralizeAdd(object obj)
        {
            Generalize generalize = new Generalize();
            //添加
            //获取表单提交过来的数据
            int generalizeTypeId = Auxiliary.ToInt32(Request["page"]);
            int location = Auxiliary.ToInt32(Request["location"]);
            int categoryId = Request["category"] == null ? 0 : Auxiliary.ToInt32(Request["category"]);
            string goodssort = Request.Form["goodssort"];
            generalize.CategoryId = categoryId;
            generalize.GeneralizeTypeId = generalizeTypeId;
            generalize.GeneralizeLocationId = location;
            generalize.CreateTime = DateTime.Now; 
            string generalizeName = Request["title"];
            string generalizeRemark = "";
            generalize.Name = generalizeName;
            generalize.Remark = Request["remark"];
            string beginTime = Request["beginTime"];
            string endTime = Request["endTime"];
            //2014-05-14 00:00:00.000
            generalize.BeginTime = Convert.ToDateTime(beginTime);
            generalize.EndTime = Convert.ToDateTime(endTime);
            generalize.AdminId = _ServiceContext.CurrentUser.UserID;
            generalize.AdminName = _ServiceContext.CurrentUser.RealName;
            generalize.AduitState = Request["state"] == "0" ? 1 : 2;
            generalize.UpdateTime = DateTime.Now;
            generalize.Isdel = 0;
            generalize.State = 1;  //上架
            BackMessge bm = new BackMessge();
            int result = 0;
            //添加
            result = bll.PublishGeneralize(generalize);
            generalize.GeneralizeId = result;
            Manager_RentalGoodsAduitEntity rentalEntity = new Manager_RentalGoodsAduitEntity();
            SetRentalEntity(rentalEntity, goodssort, generalize.GeneralizeId);
            bm.Success = result > 0;
            string url = @Url.ManagerSiteUrl().Generalize_GeneralizeShow;
            //记录日志
            LogCreateEntity logEntity = new LogCreateEntity() { };
            logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
            logEntity.Name = _ServiceContext.CurrentUser.LoginName;
            logEntity.RealName = _ServiceContext.CurrentUser.RealName;
            logEntity.ModuleId = (int)ManagerNavigation.TIANJIASHANGPINPAIXU;
            logEntity.ModuleName = "添加商品排序";
            logEntity.Remark = "添加商品排序"  + generalize.Name;
            new LogBll().CreateLog(logEntity);
            if (Request["state"] == "0")
            {
                return Redirect(url + "?stateId=1");
            }
            else
            {
                return Redirect(url + "?stateId=2");
            }
        }

        /// <summary>
        /// 展示详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowInfo(int id,bool isAdvert)
        {
            List<Category> listCategory = new List<Category>();
            //获取位置信息
            Manager_GeneralizeLocationEntity location = bll.GetLacationById(id);
            //获取分类信息
            listCategory = bll.GetParentCategoryName(location.CategoryId.Value, listCategory);
            //获取排序信息
            Generalize generalize = bll.GetGeneralizeInfoById(id);
            List<Manager_RentalGoodsAduitEntity> list = bll.GetGeneralizeGoods(id);
            ViewData["location"] = location;
            ViewData["listCategory"] = listCategory;
            ViewData["generalize"] = generalize;
            ViewData["goodsList"] = list;
            ViewData["isAdvert"] = isAdvert;
            return View();

        }
        /// <summary>
        /// 删除商品排序
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GeneralizeDelete()
        {
             int id = Auxiliary.ToInt32(Request["id"]);
            int pageIndex = Auxiliary.ToInt32(Request["pageIndex"]);
            int state = Auxiliary.ToInt32(Request["state"]);
            Generalize generalize = new Generalize();
            generalize.GeneralizeId = id;
            generalize = bll.GetGeneralizeInfoById(id);
            generalize.AdminId = _ServiceContext.CurrentUser.UserID;
            generalize.AdminName = _ServiceContext.CurrentUser.RealName;
            bll.DeleteGeneralize(generalize);
            bll.DelelteGoods(generalize.GeneralizeId);
            //记录日志
            LogCreateEntity logEntity = new LogCreateEntity() { };
            logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
            logEntity.Name = _ServiceContext.CurrentUser.LoginName;
            logEntity.RealName = _ServiceContext.CurrentUser.RealName;
            logEntity.ModuleId = (int)ManagerNavigation.GUANLISHANGPINPAIXU;
            logEntity.ModuleName = "商品排序管理";
            logEntity.Remark = "删除商品排序"  + generalize.Name;
            new LogBll().CreateLog(logEntity);
            return RedirectToAction("GeneralizeShow", new { pageIndex = pageIndex ,stateId=state});
        }
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GeneralizeApply(int id,int state,int pageIndex)
        {
            Generalize generalize = new Generalize();
            generalize.GeneralizeId = id;
            generalize = bll.GetGeneralizeInfoById(id);
            generalize.AdminId = _ServiceContext.CurrentUser.UserID;
            generalize.AdminName = _ServiceContext.CurrentUser.RealName;
            generalize = bll.GetGeneralizeInfoById(id);   
            bll.ApplyGeneralize(generalize);
            //记录日志
            LogCreateEntity logEntity = new LogCreateEntity() { };
            logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
            logEntity.Name = _ServiceContext.CurrentUser.LoginName;
            logEntity.RealName = _ServiceContext.CurrentUser.RealName;
            logEntity.ModuleId = (int)ManagerNavigation.GUANLISHANGPINPAIXU;
            logEntity.ModuleName = "商品排序管理";
            logEntity.Remark ="提交核审" + generalize.Name;
            new LogBll().CreateLog(logEntity);
            return RedirectToAction("GeneralizeShow", new { stateId = state });
        }
        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GeneralizeDownLine(int id,int state,int pageIndex)
        {

            GeneralizeSearchEntity generalizeSearchEntity = new GeneralizeSearchEntity();
            Generalize generalize = new Generalize();
            generalize.GeneralizeId = id;
            generalize.AdminId = _ServiceContext.CurrentUser.UserID;
            generalize.AdminName = _ServiceContext.CurrentUser.RealName;
            generalizeSearchEntity.State = state;
            generalize = bll.GetGeneralizeInfoById(id);  
            bll.DownLine(generalize);
            if (bll.GetGeneralizeDataNoAduit(pageIndex, generalizeSearchEntity).DataList.Count == 0)
            {
                pageIndex = pageIndex - 1;
                pageIndex = pageIndex == 0 ? 1 : pageIndex;

            }
            //记录日志
            LogCreateEntity logEntity = new LogCreateEntity() { };
            logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
            logEntity.Name = _ServiceContext.CurrentUser.LoginName;
            logEntity.RealName = _ServiceContext.CurrentUser.RealName;
            logEntity.ModuleId = (int)ManagerNavigation.GUANLISHANGPINPAIXU;
            logEntity.ModuleName = "商品排序管理";
            logEntity.Remark =  "强制下线"+ generalize.Name ;
            new LogBll().CreateLog(logEntity);
            return RedirectToAction("GeneralizeShow",new {stateId = state});
        }
        /// <summary>
        /// 核审商品排序
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="name"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ActionResult ShowAduitInfo(int? pageIndex,string name,DateTime? beginTime,DateTime? endTime)
        {
            //
            GeneralizeSearchEntity generalizeSearch = new GeneralizeSearchEntity();
            //页码参数为空默认显示第一页
            if (!pageIndex.HasValue)
                pageIndex = 1;
            generalizeSearch.NameSearch = name;
            generalizeSearch.BeginTime = beginTime;
            generalizeSearch.EndTime = endTime;
            generalizeSearch.State = 2;
            PageData<Generalize> pageData = bll.GetGeneralizeDataNoAduit(pageIndex.Value, generalizeSearch);
            // 后一页无数据情况
            if (pageData.DataList == null || pageData.DataList.Count <= 0 && pageIndex > 1)
            {
                pageIndex -= 1;
                pageData = bll.GetGeneralizeDataNoAduit(pageIndex.Value, generalizeSearch);
            }
            ViewData["pageData"] = pageData;
            ViewData["name"] = name;
            ViewData["beginTime"] = beginTime;
            ViewData["endTime"] = endTime;
            ViewData["State"] = 2;
            return View();


        }

        /// <summary>
        /// 核审
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PassAduit(int id,int flag)
        {
            string remark = Request["AuditOpinion"];
            BackMessge bm = new BackMessge();
           
            if (string.IsNullOrEmpty(id.ToString()))
            {
                bm.Message = "参数无效！";
            }
            else
            {
                //通过
                if (Request["flag"] != null && Request["flag"] == "1")
                {   
                    //根据ID获取广告对象
                 Generalize generalize = bll.GetGeneralizeInfoById(id);
                 var result = false;
                 if (generalize != null)
                 {
                     if (remark==null)
                     {
                         generalize.AduitRemark = "";
                     }
                    generalize.AdminId = _ServiceContext.CurrentUser.UserID;
                    generalize.AdminName = _ServiceContext.CurrentUser.RealName;
                    generalize.AduitState = 5;   //审核通过
                    result = bll.CheckGeneralize(generalize);
                    //记录日志
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                     logEntity.ModuleId = (int) ManagerNavigation.SHENHESHANGPINPAIXU;
                    logEntity.ModuleName = "审核商品排序";
                    logEntity.Remark ="通过审核" + generalize.Name ;
                    new LogBll().CreateLog(logEntity);
                 }
                if (result)
                {
                    bm.Message = "通过审核！";
                }
                else
                {
                    bm.Message = "审核过程中出现错误，请重试！";
                }
                bm.Success = result;

                }
                  //驳回
                else 
                {
                    //根据ID获取广告对象
                    Generalize generalize = bll.GetGeneralizeInfoById(id);
                    var result = false;
                    if (generalize != null)
                    {
                        generalize.AdminId = _ServiceContext.CurrentUser.UserID;
                        generalize.AdminName = _ServiceContext.CurrentUser.RealName;
                        generalize.AduitState = 1;
                        generalize.AduitRemark = remark;
                        result = bll.CheckGeneralize(generalize);
                        //记录日志
                        LogCreateEntity logEntity = new LogCreateEntity() { };
                        logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                        logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                        logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                        logEntity.ModuleId = (int)ManagerNavigation.SHENHESHANGPINPAIXU;
                        logEntity.ModuleName = "审核商品排序";
                        logEntity.Remark = "驳回审核" + generalize.Name;
                        new LogBll().CreateLog(logEntity);
                    }
                    if(result)
                    {
                        bm.Message = "操作成功！";
                    }
                    else
                    {
                        bm.Message = "操作过程中出现错误，请重试！";
                    }
                    bm.Success = result;
                }
             
            }
            return new JsonResult() {Data = bm, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }


        #region 排序商品相关
        /// <summary>
        /// 添加排序商品
        /// </summary>
        /// <param name="rentalEntity"></param>
        /// <param name="value"></param>
        /// <param name="generalizeId"></param>
        public void SetRentalEntity(Manager_RentalGoodsAduitEntity rentalEntity,string value,int generalizeId)
        {
            string[] strs = value.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                string[] str = strs[i].Split('|');
                rentalEntity.GoodsId = Auxiliary.ToInt32( str[0]);
                rentalEntity.Sort =Auxiliary.ToInt32( str[1]);
                bll.UpdateGeneralizeGoods(rentalEntity, generalizeId);
            }
        }

        #endregion

        #region 分页相关
        /// <summary>
        /// 异步搜索分页
        /// </summary>
        /// <param name="title">商品名称</param>
        /// <param name="name">商家名称</param>
        /// <param name="code">商家编号</param>
        /// <param name="id">页码</param>
        /// <returns></returns>
        public ActionResult AjaxSearchGet(string title, string name, string code, int pageIndex=1)
        {
            return AjaxSearchGetResult(title, name, code, pageIndex);
        }
        /// <summary>
        /// 异步搜索分页
        /// </summary>
        /// <param name="title">商品名称</param>
        /// <param name="name">商家名称</param>
        /// <param name="code">商家编号</param>
        /// <param name="id">页码</param>
        /// <returns></returns>
        public ActionResult AjaxSearchGetResult(string title, string name, string code, int id=1)
        {
            GeneralizeRentalGoodsSearchEntity searchEntity = new GeneralizeRentalGoodsSearchEntity();
            searchEntity.GoodsName = title;
            searchEntity.MerchantName = name;
            searchEntity.GoodsCode = code;
            PageData<Manager_RentalGoodsAduitEntity> pageData = bll.GetGeneralizeGoods(id, searchEntity);
            ViewData.Model = pageData;
            if (Request.IsAjaxRequest())
                return PartialView("~/Views/Shared/_AjaxSearchGet.cshtml", ViewData.Model);
            return View("GeneralizeUpdate", ViewData.Model);
        }
        #endregion
    }
}
