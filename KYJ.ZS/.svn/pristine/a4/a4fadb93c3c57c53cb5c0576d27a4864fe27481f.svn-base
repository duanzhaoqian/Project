#define notes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KYJ.ZS.BLL.Adverts;
using System.Web.Mvc;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.Adverts;
using KYJ.ZS.Models.View;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：cheny
    /// Time：2014-06-05
    /// Desc：广告管理控制器
    /// </summary>
    public class AdvertManageController : BaseController
    {
        #region 成员及变量
        /// <summary>
        /// AdvertManageBll 对象
        /// </summary>
        private readonly AdvertManageBll bll = new AdvertManageBll();
        #endregion

        #region 广告管理 +ActionResult Manage(int? pageIndex,int? sateId,string name,DateTime? beginTime,DateTime?endTime)
        /// <summary>
        /// 广告预览
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="sateId">状态ID</param>
        /// <param name="name">搜索名称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public ActionResult Manage(int? pageIndex, int? stateId, string name, DateTime? beginTime, DateTime? endTime)
        {
            //获取相关数据
            AdvertsSearchEntity advertsSearch = null;
            //pageIndex.HasValue?pageIndex:1;
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            advertsSearch = new AdvertsSearchEntity();

            advertsSearch.State = stateId == null ? (int)GeneralizeSate.NoAduit : stateId.Value;
            advertsSearch.NameSearch = name;
            advertsSearch.BeginTime = beginTime;
            advertsSearch.EndTime = endTime;
            PageData<Advert> pageData = bll.GetAdvertDataNoAduit(pageIndex.Value, advertsSearch);
            // 后一页无数据情况
            if (pageData.DataList == null || pageData.DataList.Count <= 0 && pageIndex > 1)
            {
                pageIndex -= 1;
                pageData = bll.GetAdvertDataNoAduit(pageIndex.Value, advertsSearch);
            }
            ViewData["pageData"] = pageData;
            ViewData["State"] = (GeneralizeSate)advertsSearch.State;
            ViewData["name"] = name;
            ViewData["beginTime"] = beginTime;
            ViewData["endTime"] = endTime;
            return View();
        }
        #endregion

        #region 广告详细信息 +ActionResult Detail(int id)
        /// <summary>
        /// 获取广告详细信息
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns></returns>
        public ActionResult Detail(int id, bool? isCheckAdvert)
        {
            Advert advert = bll.GetAdvertById(id);
            Manager_AdvertLocationEntity locationEntity = bll.GetLacationById(id);
            ViewBag.locationEntity = locationEntity;
            string categoryName = string.Empty;
            List<Category> listCategory = new List<Category>();
            if (locationEntity == null)
            {
                categoryName = "";
            }
            else
            {

                //categoryName = bll.GetCategoryName(locationEntity.CategoryId);
                listCategory = bll.GetParentCategoryName(locationEntity.CategoryId, listCategory);
                ViewData["listCategory"] = listCategory;
            }
            if (string.IsNullOrEmpty(categoryName))
            {
                categoryName = "";
            }
            else
            {
                categoryName = categoryName + " - ";
            }
            ViewBag.categoryName = categoryName;   //父分类名称
            ViewBag.isCheckAdvert = isCheckAdvert;  //是否是审核广告
            ViewBag.state = advert.AduitState;    //广告状态
            //获取图片大小
            AdvertSize advertSize = bll.GetAdvertSize(advert.AdvertLocationId);
            ViewBag.advertSize = advertSize;
            return View(advert);
        }
        #endregion

#if !notes
        #region 修改广告信息[HttpGet] +ActionResult Modify(int id)
        /// <summary>
        /// 修改广告信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Modify(int id)
        {
            //根据ID获取广告信息
            Advert advert = bll.GetAdvertById(id);
            Manager_AdvertLocationEntity locationEntity = bll.GetLacationById(id);
            ViewBag.locationEntity = locationEntity;
            string categoryName = string.Empty;
            categoryName = bll.GetCategoryName(advert.CategoryId);
            if (string.IsNullOrEmpty(categoryName))
            {
                categoryName = "";
            }
            else
            {
                categoryName = categoryName + " - ";
            }
            ViewBag.categoryName = categoryName;   //父分类名称
            return View(advert);
        }
        #endregion

        #region 修改广告信息[HttpPost] +ActionResult Modify(FormCollection collection)
        /// <summary>
        /// 修改广告信息
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Modify(FormCollection collection)
        {

            return Json(new { result = true, message = "操作成功" });
        }
        #endregion
  
#endif

        #region 发布广告[HttpGet] +ActionResult Publish()
        /// <summary>
        /// 发布广告
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Publish()
        {
            if (!string.IsNullOrEmpty(Request["stateId"]))
            {
                string stateId = Request["stateId"];
                ViewBag.stateId = stateId;
            }


            //获取页面数据
            List<AdvertTypes> list = bll.GetAdvertTypesData();
            //获取小时
            List<string> listHour = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                string str = this.FormatString(i);
                listHour.Add(str);
            }
            //获取分钟
            List<string> listMinute = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                string str = this.FormatString(i);
                listMinute.Add(str);
            }
            //向视图传参
            ViewBag.pageData = list;
            ViewBag.listHour = listHour;
            ViewBag.listMinute = listMinute;

            Advert advert = new Advert();
            int id = 0;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                //修改
                advert = bll.GetAdvertById(id);
                string maxNum = bll.GetMaxNum(advert.AdvertLocationId);
                ViewBag.maxNum = maxNum;
                Manager_AdvertLocationEntity locationEntity = bll.GetLacationById(id);
                string categoryName = "";
                List<Category> listCategory = new List<Category>();
                if (locationEntity == null)
                {
                    categoryName = "";
                }
                else
                {
                    //categoryName = bll.GetCategoryName(locationEntity.CategoryId);
                    listCategory = bll.GetParentCategoryName(locationEntity.CategoryId, listCategory);
                    ViewData["listCategory"] = listCategory;
                }
                if (string.IsNullOrEmpty(categoryName))
                {
                    categoryName = "";
                }
                else
                {
                    categoryName = categoryName + " - ";
                }
                ViewBag.categoryName = categoryName;   //父分类名称
                ViewBag.locationEntity = locationEntity;

                //获取图片大小
                AdvertSize advertSize = bll.GetAdvertSize(advert.AdvertLocationId);
                ViewBag.advertSize = advertSize;
                return View(advert);
            }
            else
            {
                //添加
                ViewBag.categoryName = null;   //父分类名称
                ViewBag.locationEntity = null;
                advert = null;
                return View(advert);
            }

        }
        #endregion

        #region 获取位置 +JsonResult GetLocation(int advertTypeId, int? categoryId)
        /// <summary>
        /// 获取位置
        /// </summary>
        /// <param name="advertTypeId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public JsonResult GetLocation(int advertTypeId, int? categoryId)
        {
            try
            {
                var list = bll.GetLocationData(advertTypeId, categoryId).ToArray();
                return Json(new { success = true, items = list }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 获取该位置允许发布广告数量（图片） +ActionResult MaxNum(int? locationId)
        /// <summary>
        /// 获取该位置允许发布广告数量（图片）
        /// </summary>
        /// <param name="locationId">广告位置ID</param>
        /// <returns></returns>
        public ActionResult MaxNum(int? locationId)
        {
            BackMessge bm = new BackMessge();
            int num = 0;
            var result = false;
            if (int.TryParse(bll.GetMaxNum(locationId), out num))
                result = true;
            else
                result = false;
            bm.Message = num.ToString();
            bm.Success = result;
            return Json(bm);
        }
        #endregion

        #region 获取广告图片的尺寸 +ActionResult AdvertSize(int? locationId)
        /// <summary>
        /// 获取广告图片的尺寸
        /// </summary>
        /// <param name="locationId">位置ID</param>
        /// <returns></returns>
        public ActionResult AdvertSize(int? locationId)
        {
            BackMessge bm = new BackMessge();
            AdvertSize advertSize = bll.GetAdvertSize(locationId);
            if (advertSize != null)
            {
                bm.Success = true;
                bm.Message = advertSize.AdvertWidth.ToString() + "*" + advertSize.AdvertHeight.ToString();
            }
            else
            {
                bm.Success = false;
            }
            ViewBag.advertSize = advertSize;
            return Json(bm);
        }
        #endregion

        #region 发布广告[HttpPost] +ActionResult Publish()
        /// <summary>
        /// 发布广告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Publish(bool isSave)
        {
            int isModify = 0;
            Advert advert = new Advert();
            if (int.TryParse(Request["isModify"], out isModify))
            {
                //修改
                advert.AdvertId = Auxiliary.ToInt32(Request["hidAdvertId"]);
            }
            else
            {
                //添加
                //获取表单提交过来的数据
                int advertTypeId = Auxiliary.ToInt32(Request["sel_Page"]);
                int firstCategory = Auxiliary.ToInt32(Request["sel_firstCategory"]);
                int secondCategory = Auxiliary.ToInt32(Request["sel_secondCategory"]);
                int thirdCategory = Auxiliary.ToInt32(Request["sel_thirdCategory"]);
                int location = Auxiliary.ToInt32(Request["sel_Position"]);
                if (advertTypeId == 1)   //1级
                {
                    advert.CategoryId = firstCategory;
                }
                else if (advertTypeId == 2)  //2级
                {
                    advert.CategoryId = secondCategory;
                }
                else if (advertTypeId == 3) //3级
                {
                    advert.CategoryId = thirdCategory;
                }

                advert.AdvertTypeId = advertTypeId;
                advert.AdvertLocationId = location;
                advert.CreateTime = Convert.ToDateTime(DateTime.Now.ToString());
            }
            advert.Guid = Request["hidGuid"];
            string advertName = Request["txtAdvertName"].ToString();
            string advertRemark = "";
            if (!string.IsNullOrEmpty(Request["txtAdvertRemark"]))
            {
                advertRemark = Request["txtAdvertRemark"].ToString();
            }

            string advertAddress = Request["txtAdvertUrl"].ToString();
            if (advertAddress.IndexOf("http:") < 0)
            {
                advertAddress = "http://" + advertAddress ;
            }
            string begin_year = Request["text_beginTime"].ToString();
            string begin_hour = Request["sel_begin_hour"].ToString();
            string begin_minute = Request["sel_begin_minute"].ToString();
            string end_year = Request["text_endTime"].ToString();
            string end_hour = Request["sel_end_hour"].ToString();
            string end_minute = Request["sel_end_minute"].ToString();
            int advertSort = Auxiliary.ToInt32(Request["sel_Order"]);

            advert.Name = advertName;
            advert.Remark = advertRemark;
            advert.Address = advertAddress;
            string beginTime = begin_year + " " + begin_hour + ":" + begin_minute;
            string endTime = end_year + " " + end_hour + ":" + end_minute;
            //2014-05-14 00:00:00.000
            advert.BeginTime = Convert.ToDateTime(beginTime);
            advert.EndTime = Convert.ToDateTime(endTime);
            advert.Sort = advertSort;
            advert.AdminId = _ServiceContext.CurrentUser.UserID;
            advert.AdminName = _ServiceContext.CurrentUser.RealName;
            //advert.Guid = Guid.NewGuid().ToString();
            advert.Weight = 0;
            if (isSave)
            {
                advert.AduitState = 1;
            }
            else
            {
                advert.AduitState = 2;
            }

            advert.UpdateTime = Convert.ToDateTime(DateTime.Now.ToString());
            advert.Isdel = 0;
            advert.State = 1;  //下架
            advert.Price = 1;
            advert.MerchantName = "测试";

            BackMessge bm = new BackMessge();
            int result = 0;
            if (isModify == 1)
            {
                //修改
                result = bll.ModifyAdvert(advert);
            }
            else
            {
                //添加
                result = bll.PublishAdvert(advert);
            }
            // 日志记录
            LogCreateEntity logEntity = new LogCreateEntity() { };
            logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
            logEntity.Name = _ServiceContext.CurrentUser.LoginName;
            logEntity.RealName = _ServiceContext.CurrentUser.RealName;
            logEntity.ModuleId = (int)ManagerNavigation.FABUGUANGGAO;
            logEntity.ModuleName = "发布广告";
            logEntity.Remark = "发布广告" + advert.Name;
            new LogBll().CreateLog(logEntity);
            bm.Success = result > 0;
            string url = Url.ManagerSiteUrl().AdvertManage_Manage;
            if (!string.IsNullOrEmpty(Request["hidStateId"]))
            {
                string stateId = Request["hidStateId"].ToString();
                return Redirect(url + "?stateId=" + stateId);
            }
            else
            {
                if (isSave)
                {
                    return Redirect(url + "?stateId=1");
                }
                else
                {
                    return Redirect(url + "?stateId=2");
                }
            }



        }
        #endregion

        #region 删除广告 +ActionResult Delete(int id)
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                BackMessge bm = new BackMessge();
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    bm.Message = "参数无效！";
                }
                else
                {
                    //根据ID获取广告对象
                    Advert advert = bll.GetAdvertById(id);
                    var result = false;
                    if (advert != null)
                    {
                        advert.AdminId = _ServiceContext.CurrentUser.UserID;
                        advert.AdminName = _ServiceContext.CurrentUser.RealName;
                        result = bll.DeleteAdvert(advert);
                    }
                    if (result)
                    {
                        bm.Message = "删除成功！";
                    }
                    else
                    {
                        bm.Message = "删除失败！";
                    }
                    // 日志记录
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                    logEntity.ModuleId = (int)ManagerNavigation.GUANLIGUANGGAO;
                    logEntity.ModuleName = "广告管理";
                    logEntity.Remark = "删除广告" + advert.Name;
                    new LogBll().CreateLog(logEntity);
                    bm.Success = result;
                }
                return Json(bm);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return Json(new BackMessge() { Success = false, Message = "删除失败" });
            }
        }
        #endregion

        #region 提交审核 +ActionResult Apply(int id)
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Apply(int id)
        {
            try
            {
                BackMessge bm = new BackMessge();
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    bm.Message = "参数无效！";
                }
                else
                {
                    //根据ID获取广告对象
                    Advert advert = bll.GetAdvertById(id);
                    var result = false;
                    if (advert != null)
                    {
                        advert.AdminId = _ServiceContext.CurrentUser.UserID;
                        advert.AdminName = _ServiceContext.CurrentUser.RealName;
                        //改变广告的状态
                        if (advert.AduitState == 1 || advert.AduitState == 3)
                        {
                            advert.AduitState = advert.AduitState + 1;
                        }
                        result = bll.ApplyAduit(advert);
                    }
                    if (result)
                    {
                        bm.Message = "申请审核成功！";
                    }
                    else
                    {
                        bm.Message = "申请审核失败！";
                    }
                    // 日志记录
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                    logEntity.ModuleId = (int)ManagerNavigation.GUANLIGUANGGAO;
                    logEntity.ModuleName = "广告管理";
                    logEntity.Remark = "提交审核" + advert.Name;
                    new LogBll().CreateLog(logEntity);
                    bm.Success = result;
                }
                return Json(bm);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return Json(new BackMessge() { Success = false, Message = "操作失败" });
            }
        }
        #endregion

        #region 强制下线 +ActionResult DownLine(int id)
        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DownLine(int id)
        {
            try
            {
                BackMessge bm = new BackMessge();
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    bm.Message = "参数无效！";
                }
                else
                {
                    //根据ID获取广告对象
                    Advert advert = bll.GetAdvertById(id);
                    var result = false;
                    if (advert != null)
                    {
                        advert.AdminId = _ServiceContext.CurrentUser.UserID;
                        advert.AdminName = _ServiceContext.CurrentUser.RealName;
                        result = bll.DownLine(advert);
                    }
                    if (result)
                    {
                        bm.Message = "强制下线成功！";
                    }
                    else
                    {
                        bm.Message = "强制下线失败！";
                    }
                    bm.Success = result;
                    // 日志记录
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                    logEntity.ModuleId = (int)ManagerNavigation.GUANLIGUANGGAO;
                    logEntity.ModuleName = "管理广告";
                    logEntity.Remark = "强制下线" + advert.Name;
                    new LogBll().CreateLog(logEntity);
                }
                return Json(bm);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return Json(new BackMessge() { Success = false, Message = "操作失败" });
            }
        }
        #endregion

        #region 审核广告 +CheckAdvert(int? pageIndex, string name, DateTime? beginTime, DateTime? endTime)
        /// <summary>
        /// 审核广告
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckAdvert(int? pageIndex, string name, DateTime? beginTime, DateTime? endTime)
        {
            //获取相关数据
            AdvertsSearchEntity advertsSearch = null;
            //pageIndex.HasValue?pageIndex:1;
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            advertsSearch = new AdvertsSearchEntity();

            advertsSearch.State = (int)GeneralizeSate.Aduit;
            advertsSearch.NameSearch = name;
            advertsSearch.BeginTime = beginTime;
            advertsSearch.EndTime = endTime;
            PageData<Advert> pageData = bll.GetAdvertDataNoAduit(pageIndex.Value, advertsSearch);
            // 后一页无数据情况
            if (pageData.DataList == null || pageData.DataList.Count <= 0 && pageIndex > 1)
            {
                pageIndex -= 1;
                pageData = bll.GetAdvertDataNoAduit(pageIndex.Value, advertsSearch);
            }
            ViewData["pageData"] = pageData;
            ViewData["State"] = (GeneralizeSate)advertsSearch.State;
            ViewData["name"] = name;
            ViewData["beginTime"] = beginTime;
            ViewData["endTime"] = endTime;
            return View();
        }
        #endregion

        #region 通过审核 +ActionResult PassApply(int id)
        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PassApply(int id)
        {
            try
            {
                BackMessge bm = new BackMessge();
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    bm.Message = "参数无效！";
                }
                else
                {
                    //根据ID获取广告对象
                    Advert advert = bll.GetAdvertById(id);
                    var result = false;
                    if (advert != null)
                    {
                        advert.AdminId = _ServiceContext.CurrentUser.UserID;
                        advert.AdminName = _ServiceContext.CurrentUser.RealName;
                        advert.AduitState = 5;   //审核通过
                        result = bll.ApplyAduit(advert);
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
                    // 日志记录
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                    logEntity.ModuleId = (int)ManagerNavigation.HESHENGUANGGAO;
                    logEntity.ModuleName = "审核广告";
                    logEntity.Remark = "审核广告" + advert.Name;
                    new LogBll().CreateLog(logEntity);
                }
                return Json(bm);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return Json(new BackMessge() { Success = false, Message = "操作失败" });
            }
        }
        #endregion

        #region 驳回广告审核 +ActionResult RejectApply(int id, string auditOpinion)
        /// <summary>
        /// 驳回广告审核
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <param name="auditOpinion">驳回理由</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RejectApply(int id, string auditOpinion)
        {
            try
            {
                BackMessge bm = new BackMessge();
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    bm.Message = "参数无效！";
                }
                else
                {
                    //根据ID获取广告对象
                    Advert advert = bll.GetAdvertById(id);
                    var result = false;
                    if (advert != null)
                    {
                        advert.AdminId = _ServiceContext.CurrentUser.UserID;
                        advert.AdminName = _ServiceContext.CurrentUser.RealName;
                        advert.AduitState = 1;
                        advert.AduitRemark = auditOpinion;
                        result = bll.CheckAdvert(advert);
                    }
                    if (result)
                    {
                        bm.Message = "操作成功！";
                    }
                    else
                    {
                        bm.Message = "操作过程中出现错误，请重试！";
                    }
                    bm.Success = result;
                    // 日志记录
                    LogCreateEntity logEntity = new LogCreateEntity() { };
                    logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                    logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                    logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                    logEntity.ModuleId = (int)ManagerNavigation.HESHENGUANGGAO;
                    logEntity.ModuleName = "审核广告_驳回";
                    logEntity.Remark = "驳回广告" + advert.Name;
                    new LogBll().CreateLog(logEntity);
                }
                return Json(bm);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return Json(new BackMessge() { Success = false, Message = "操作失败" });
            }
        }
        #endregion

        #region 私有方法

        #region 获取类别数据 void GetCategoryData()
        /// <summary>
        /// 获取类别数据
        /// </summary>
        private void GetCategoryData()
        {
            int firstCategoryId = Auxiliary.ToInt32(Request.QueryString["sel_firstCategory"], 0); //商品类别(一级类目)
            int secondCategoryId = Auxiliary.ToInt32(Request.QueryString["sel_secondCategory"], 0); //商品类别(二级类目)
            int thirdCategoryId = Auxiliary.ToInt32(Request.QueryString["sel_thirdCategory"], 0); //商品类别(三级类目)
            // 类目
            ViewData["firstCategory"] = firstCategoryId;
            ViewData["secondCategory"] = secondCategoryId;
            ViewData["thirdCategory"] = thirdCategoryId;
        }
        #endregion

        #region 格式化数字 string FormatString(int index)
        /// <summary>
        /// 格式化数字
        /// <para>eg：01  02  03……</para>
        /// </summary>
        /// <param name="index">待转换的数字</param>
        /// <returns></returns>
        private string FormatString(int index)
        {
            string str = string.Empty;
            if (index < 10)
            {
                str = string.Format("0{0}", index);
            }
            else
            {
                str = index.ToString();
            }
            return str;
        }
        #endregion

        #endregion

    }
}
