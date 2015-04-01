using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using TXBll.Activitys;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.NHouseActivies;
using TXBll.NHouseActivies.Discount;
using TXBll.NHouseActivies.SecKill;
using TXBll.NHouseActivies.TuanGou;
using TXBll.WebSite;
using TXCommons.Admins;
using TXManagerWeb.Common;
using TXManagerWeb.Controllers.ActionFilters;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXOrm;
using Newtonsoft.Json;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [AdminPageInfo(6, (int) AdminNavi.NewHouseMg.Activities.PurchaseHouse)]
    public class PurchaseHouseController : BaseController
    {
        #region 排号购房活动列表

        /// <summary>
        ///     排号购房活动列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var geographyBll = new GeographyBll();

            var search = new PVS_NH_LadderBuy();
            search.Type = 3;
            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem {Text = "省", Value = "0"});

            search.Cityes = (0 == search.ProvinceID ? new List<SelectListItem>() : Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceID));
            search.Cityes.Insert(0, new SelectListItem {Text = "市", Value = "0"});

            search.Premises = (0 == search.CityId ? new List<SelectListItem>() : geographyBll.Z_GetPremisesById(search.CityId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Premises.Insert(0, new SelectListItem {Text = "选择楼盘", Value = "0"});

            search.ActivitieState = -1;
            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();
            return View(search);
        }

        #endregion

        #region 列表搜索

        /// <summary>
        ///     列表搜索
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public ActionResult Search(PVS_NH_LadderBuy search)
        {
            var geographyBll = new GeographyBll();

            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem {Text = "省", Value = "0"});

            search.Cityes = (0 == search.ProvinceID ? new List<SelectListItem>() : Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceID));
            search.Cityes.Insert(0, new SelectListItem {Text = "市", Value = "0"});

            search.Premises = (0 == search.CityId ? new List<SelectListItem>() : geographyBll.Z_GetPremisesById(search.CityId).ConvertAll(it => new SelectListItem {Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)}));
            search.Premises.Insert(0, new SelectListItem {Text = "选择楼盘", Value = "0"});

            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();

            search.DeveloperName = Server.UrlEncode(search.DeveloperName);
            return View("Index", search);
        }

        #endregion

        #region 列表集合

        /// <summary>
        ///     列表集合
        /// </summary>
        /// <param name="search">搜索实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_LadderBuy search, int pageIndex, int pageSize)
        {
            var tuangouBll = new TuanGou_AdminBll();

            pageIndex = pageIndex + 1;

            List<PVM_NH_LadderBuy> list = tuangouBll.GetLadderBuyListByPages(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_PurchaseHouse", list);
        }

        #endregion

        #region 搜索结果总数

        /// <summary>
        ///     搜索结果总数
        /// </summary>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_LadderBuy search)
        {
            var tuangouBll = new TuanGou_AdminBll();
            int count = tuangouBll.GetLadderBuyListByPageCounts(search);
            return Json(new {result = count});
        }

        #endregion

        #region 报名用户列表

        /// <summary>
        ///     报名用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistrationUserIndex()
        {
            return View();
        }

        public ActionResult RegistrationUserShow(int id, int types)
        {
            var pvs = new PVS_NH_RegistrationUser
            {
                Id = id,
                Type = types
            };
            return View("RegistrationUserIndex", pvs);
        }

        #endregion

        #region 报名用户搜索及结果信息

        /// <summary>
        ///     报名用户搜索
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public ActionResult RegistrationUserSearch(PVS_NH_RegistrationUser search)
        {
            return View("RegistrationUserIndex", search);
        }

        /// <summary>
        ///     集合信息
        /// </summary>
        /// <param name="search">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        [AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Activities.PurchaseHouse)]
        public ActionResult RegistrationUserSearchResult(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            var tuangouBll = new TuanGou_AdminBll();

            pageIndex = pageIndex + 1;

            List<PVM_NH_RegistrationUser> list = tuangouBll.GetUsersListByPages(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_RegistPurchaseUser", list);
        }

        /// <summary>
        ///     结果记录数
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public ActionResult RegistrationUserSearchCount(PVS_NH_RegistrationUser search)
        {
            var tuangouBll = new TuanGou_AdminBll();
            int count = tuangouBll.GetUsersListByPageCounts(search);
            return Json(new {result = count});
        }

        #endregion

        #region 导出报名用户列表

        /// <summary>
        ///     导出报名用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportExcel(int id)
        {
            var tuangouBll = new TuanGou_AdminBll();
            var activity = new ActiviesUtilsBll().GetActivityById(id);
            List<PVM_NH_RegistrationUser> list = tuangouBll.GetOutPutRegistUsersByList(id);

            var titlestr = new[]
            {
                //序号 出价人 真实姓名 身份证号 手机号 QQ号码 E-mail paihao 报名时间

                "序号",
                "出价人",
                "真实姓名",
                "身份证号",
                "手机号",
                "QQ号码",
                "E-mail",
                "报名号",
                "报名时间"
            };
            List<OutputExcel> listResult = list.ConvertAll(it => new OutputExcel
            {
                CellText1 = it.Id.ToString(CultureInfo.InvariantCulture),
                CellText2 = it.BidUserName,
                CellText3 = it.RealName,
                CellText4 = it.IDCard,
                CellText5 = it.Mobile,
                CellText6 = it.BidUserQQ,
                CellText7 = it.BidUserMail,
                CellText8 = it.RegNum.ToString(CultureInfo.InvariantCulture),
                CellText9 = string.Format("{0:yyyy-MM-dd HH:mm}", it.CreateTime)
            });
            string fileName = activity.Name + "-排号购房报名用户列表";
            return new ExcelResult<OutputExcel>(listResult, fileName, titlestr);
        }

        #endregion

        #region 编辑

        /// <summary>
        ///     编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditPurchaseHouse(int id)
        {
            Activity activity = new SecKillBll().GetEntity_ById(id);
            var model = new PVM_NH_Purchase();
            Premis premis = new PremisesBll().GetEntity_ByActvityId(id);
            model.PremisesId = premis.Id;
            model.PremisesName = premis.Name;
            model.Id = activity.Id;
            model.Name = activity.Name;
            model.BeginTime = Convert.ToDateTime(activity.BeginTime);
            model.EndTime = Convert.ToDateTime(activity.EndTime);
            model.Bond = activity.Bond;
            model.UserCount = activity.UserCount;
            model.BeginTimeString = string.Format("{0:yyyy-MM-dd}", model.BeginTime);
            model.EndTimeString = string.Format("{0:yyyy-MM-dd}", model.EndTime);
            model.BondString = string.Format("{0:F0}", model.Bond);

            model.BuildingId = 0;
            model.Buildings = Auxiliary.Instance.GetBuildingsByPremisesId(model.PremisesId);
            model.Buildings.Insert(0, new SelectListItem {Text = "选择楼栋", Value = "0"});

            ViewData["Join_Houses"] = GetHousesJoinActivity_JSON(new DiscountBll().GetHousesJoinDiscount(id));

            return View(model);
        }

        /// <summary>
        /// 获取创建活动时的房源信息(Json格式)
        /// </summary>
        /// <param name="houses"></param>
        /// <returns></returns>
        private string GetHousesJoinActivity_JSON(List<ActivitiesHouse> houses)
        {
            if (null == houses
                || 0 == houses.Count)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            for (int i = 0; i < houses.Count; i++)
            {
                // "HouseId": "房源编号", "IsOriData": "创建活动时加入的房源(1:是 0:否)", "IsDel": "是否删除(0:否 1:是)"
                builder.Append("{\"HouseId\":\"" + houses[i].HouseId + "\",\"IsOriData\":\"1\",\"IsDel\":\"0\"}");
                if (i < houses.Count - 1)
                {
                    builder.Append(",");
                }
            }

            return builder.ToString();
        }

        /// <summary>
        ///     编辑房源集合(阶梯团购通用)
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPurchaseHouse_Edit_Houses(PVS_NH_Purchase_Edit search, int pageIndex = 0, int pageSize = 20)
        {
            pageIndex += 1;
            var list = new TuanGou_AdminBll().GetPageList_BySearch_EditPurchaseHouses_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_PurchaseHouse_Edit_Houses", list);
        }

        /// <summary>
        ///     编辑房源数量(阶梯团购通用)
        /// </summary>
        /// <returns></returns>
        public JsonResult EditPurchaseHouse_Edit_Houses_Count(PVS_NH_Purchase_Edit search)
        {
            int count = new TuanGou_AdminBll().GetTotalCount_BySearch_EditPurchaseHouses_Admin(search);

            return Json(new {result = count});
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult EditPurchaseHouseDo(PVM_NH_Purchase model)
        {
            model.JoinHouses = JsonConvert.DeserializeObject<List<PVE_NH_Purchase_JoinHouse>>(model.HousesJsonString);

            model.ActivitieState = new SecKillBll().GetEntity_ById(model.Id).ActivitieState;

            if (Auxiliary.Instance.IsActivityStartInToday(model.BeginTime))
            {
                model.ActivitieState = 1;
            }

            var result = new TuanGou_AdminBll().UpdatePurchase_Admin(model);

            if (result)
            {
                var list = new SecKillBll().GetActivityPremisesInfoList(model.Id);
                foreach (var ah in list)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", ah.PremisesId, ah.CityId);
                }

                // 操作日志
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-排号购房活动-更新活动编号[{0}]", model.Id),
                    Type = 0
                });

                return Json(new { suc = true, msg = "更新成功" });
            }

            return Json(new { suc = false, msg = "更新失败" });
        }

        #endregion

        #region 删除

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Del(int id)
        {
            int count = new TuanGou_AdminBll().DelTuanGou(id);

            if (0 < count)
            {
                var list = new SecKillBll().GetActivityPremisesInfoList(id);
                foreach (var ah in list)
                {
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", ah.PremisesId, ah.CityId);
                }

                // 站内信
                var activity = new SecKillBll().GetEntity_ById(id);
                var developer = (new DevelopersBll().GetEntity_ById(Convert.ToInt32(activity.DeveloperId))) as Developer;
                if (0 < Convert.ToInt32(activity.DeveloperId) && null != developer)
                {
                    new DeveloperMessageBll().Add(new DeveloperMessage
                    {
                        SendUserId = 0,
                        ReceiveUserId = Convert.ToInt32(activity.DeveloperId),
                        Title = "删除活动(排号购房)",
                        Content = string.Format(@"尊敬的{0}，您所创建的{1}被删除，请如有问题请致电快有家客户服务热线：{2}。", developer.LoginName, activity.Name, Auxiliary.Instance.NhServiceHotLine1)
                    });
                }

                // 操作日志
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = _ServiceContext.CurrentUser.Id,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-排号购房活动-删除活动编号[{0}]", id),
                    Type = 0
                });

                return Json(new {suc = true, msg = "删除成功"});
            }

            return Json(new {suc = false, msg = "删除失败"});
        }

        #endregion

        public ActionResult Detail(int id)
        {
            var model = new TuanGou_AdminBll().GetPurchaseDetail(id);
            model.PremiseName = model.Houses[0].PremiseName;
            model.BondString = string.Format("{0:F0}", model.Bond);
            model.BeginTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", model.BeginTime);
            model.EndTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", model.EndTime);
            ViewData["backurl"] = Request.UrlReferrer == null ? Url.SiteUrl().PurchaseHouse_SearchResult("index") + "?activitiestate=-1" : Request.UrlReferrer.ToString();
            return View(model);
        }
    }
}