using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.NHouseActivies.Discount;
using TXBll.NHouseActivies.SecKill;
using TXCommons.Admins;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    /// <summary>
    /// 限时折扣
    /// </summary>
    [LoginChecked]
    public class DiscountController : BaseController
    {
        private DiscountBll _discountBll;

        /// <summary>
        /// 限时折扣
        /// </summary>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Activities.Discount)]
        public ActionResult Index(PVS_NH_Discount search)
        {
            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem { Text = "省", Value = "0" });

            search.Cityes = (0 == search.ProvinceId ? new List<SelectListItem>() : Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceId));
            search.Cityes.Insert(0, new SelectListItem { Text = "市", Value = "0" });

            search.Premisess = Auxiliary.Instance.GetPremisesByProvinceOrCityOrDeveloperId(search.ProvinceId, search.CityId, search.DeveloperId);
            search.Premisess.Insert(0, new SelectListItem { Text = "选择楼盘", Value = "0" });

            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();
            search.ActivitieState = -1;
            return View(search);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Activities.Discount)]
        public ActionResult Search(PVS_NH_Discount search)
        {
            search.Provinces = Auxiliary.Instance.GetProvinces();
            search.Provinces.Insert(0, new SelectListItem { Text = "省", Value = "0" });

            search.Cityes = (0 == search.ProvinceId ? new List<SelectListItem>() : Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceId));
            search.Cityes.Insert(0, new SelectListItem { Text = "市", Value = "0" });

            search.Premisess = Auxiliary.Instance.GetPremisesByProvinceOrCityOrDeveloperId(search.ProvinceId, search.CityId, search.DeveloperId);
            search.Premisess.Insert(0, new SelectListItem { Text = "选择楼盘", Value = "0" });

            search.ActivitieStates = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();

            return View("Index", search);
        }

        /// <summary>
        /// 搜索结果
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_Discount search, int pageIndex, int pageSize)
        {
            _discountBll = new DiscountBll();

            pageIndex = pageIndex + 1;
            var list = _discountBll.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_Discount", list);
        }

        /// <summary>
        /// 搜索结果数量
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult SearchCount(PVS_NH_Discount search)
        {
            _discountBll = new DiscountBll();

            int count = _discountBll.GetTotalCount_BySearch_Admin(search);
            return Json(new { result = count });
        }



        /// <summary>
        /// 编辑限时折扣
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Activities.Discount)]
        public ActionResult EditDiscount(int id)
        {
            _discountBll = new DiscountBll();

            var tmpDiscount = _discountBll.GetEntity_ById(id);

            var discount = new PVE_NH_Discount
            {
                Id = id,
                Name = tmpDiscount.Name,
                Bond = tmpDiscount.Bond,
                BondString = string.Format("{0:0}", tmpDiscount.Bond),
                BeginTime = tmpDiscount.BeginTime,
                BeginTimeString = string.Format("{0:yyyy-MM-dd}", tmpDiscount.BeginTime),
                EndTime = tmpDiscount.EndTime,
                EndTimeString = string.Format("{0:yyyy-MM-dd}", tmpDiscount.EndTime)
            };

            var premises = _discountBll.GetPremisesEntity_ByActivitiesId(id);
            discount.PremisesId = premises.Id;
            discount.Premises = Auxiliary.Instance.GetPremises();

            discount.Buildings = Auxiliary.Instance.GetBuildingsByPremisesId(premises.Id);
            if (null != discount.Buildings)
            {
                discount.Buildings.Insert(0, new SelectListItem { Text = "请选择楼栋", Value = "0" });
            }
            //discount.SalesStatusList = AdminTypes.Db_NewHouse.Tb_House.Fc_SelectListItems.Get_SalesStatus();

            ViewData["Join_Houses"] = GetHousesJoinActivity_JSON(_discountBll.GetHousesJoinDiscount(id));

            return View(discount);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public JsonResult UpdateDiscountDo(PVE_NH_Discount discount)
        {
            discount.DiscountHouses = JsonConvert.DeserializeObject<List<PVE_NH_Discount_JoinHouse>>(discount.HousesJsonString);

            var result = new DiscountBll().UpdateDiscount_Admin(discount);

            if (result)
            {
                var list = new SecKillBll().GetActivityPremisesInfoList(discount.Id);
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
                    OperDes = string.Format("新房管理-限时折扣活动-更新活动编号[{0}]", discount.Id),
                    Type = 0
                });

                return Json(new { suc = true, msg = "更新成功" });
            }

            return Json(new { suc = false, msg = "更新失败" });
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
        /// 获取房源列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchResult_Edit1(PVS_NH_Discount_Houses_Edit1 search, int pageIndex, int pageSize)
        {
            _discountBll = new DiscountBll();

            pageIndex = pageIndex + 1;
            var list = _discountBll.GetHousesInDiscountEdit1(search, pageIndex, pageSize);

            return PartialView("PageTables/Activities/_Discount_Edit", list);
        }

        /// <summary>
        /// 获取房源数量
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult SearchCount_Edit1(PVS_NH_Discount_Houses_Edit1 search)
        {
            _discountBll = new DiscountBll();

            int count = _discountBll.GetTotalCount_BySearch_GetHousesInDiscountEdit1(search);
            return Json(new { result = count });
        }

        /// <summary>
        /// 根据房源编号获取参与活动的房源显示表
        /// </summary>
        /// <param name="activitiesId">活动编号</param>
        /// <param name="houseIds">房源编号</param>
        /// <returns></returns>
        public ActionResult GetHousesTable_JoinActivity(string activitiesId, string houseIds)
        {
            _discountBll = new DiscountBll();
            var list = _discountBll.GetHousesDiscountInDiscountEdit(activitiesId, houseIds);
            return PartialView("PageTables/Activities/_Discount_Edit_Houses", list);
        }



        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <returns></returns>
        public JsonResult DelDiscountDo(int id)
        {
            _discountBll = new DiscountBll();
            var result = _discountBll.DelDiscountById(id);
            if (!result.Code.Equals("1"))
            {
                return Json(new { suc = false, msg = "删除失败" });
            }

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
                    Title = "删除活动(限时折扣)",
                    Content = string.Format(@"尊敬的{0}，您所创建的{1}被删除，请如有问题请致电快有家客户服务热线：{2}。", developer.LoginName, activity.Name, Auxiliary.Instance.NhServiceHotLine1)
                });
            }

            // 操作日志
            ExecuteOperResult(new Z_OperAdminLog
            {
                AdminID = _ServiceContext.CurrentUser.Id,
                CreateTime = DateTime.Now,
                OperIP = Request.UserHostAddress,
                OperDes = string.Format("新房管理-限时折扣活动-删除活动编号[{0}]", id),
                Type = 0
            });
            return Json(new { suc = true, msg = "删除成功" });
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Activities.Discount)]
        public ActionResult Detail(int id)
        {
            var model = new DiscountBll().GetDiscountDetail(id);
            model.PremiseName = model.Houses[0].PremiseName;
            model.BondString = string.Format("{0:F0}", model.Bond);
            model.BeginTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", model.BeginTime);
            model.EndTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", model.EndTime);
            ViewData["backurl"] = Request.UrlReferrer == null ? Url.SiteUrl().Discount_Search("index") + "?activitiestate=-1" : Request.UrlReferrer.ToString();
            return View(model);
        }


        /// <summary>
        /// 用户报名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildingId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Activities.Discount)]
        public ActionResult UserIndex(int id, int buildingId, int unitId)
        {
            var model = new DiscountBll().GetDiscountUserReports(id, buildingId, unitId);
            var premis = new PremisesBll().GetEntity_ByActvityId(id);
            ViewData["Premis"] = premis;
            ViewData["Buildings"] = new BuildingBll().GetList_ByPremisId(premis.Id).ConvertAll<SelectListItem>(t => new SelectListItem
            {
                Text = t.Name + t.NameType,
                Value = Convert.ToString(t.Id)
            });
            return View(model);
        }

        #region 导出数据

        /// <summary>
        /// 导出excel
        /// author:liyuzhao
        /// </summary>
        /// <returns></returns>
        public ExcelResult<OutputExcel> ExportExcel(int id, int buildingId, int unitId)
        {
            var list = new DiscountBll().GetDiscountUserReports(id, buildingId, unitId);

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
                "出价的房源",
                "出价时间"
            };

            #endregion

            var listResult = list.ConvertAll(it => new OutputExcel()
            {
                CellText1 = Convert.ToString(it.Id),
                CellText2 = it.BidUserName,
                CellText3 = it.BidRealName,
                CellText4 = it.IDCard,
                CellText5 = it.BidUserMobile,
                CellText6 = it.BidUserQQ,
                CellText7 = it.BidUserMail,
                CellText8 = it.HouseInfo,
                CellText9 = it.BidTimeString
            });
            const string fileName = "限时折扣活动报名用户列表";
            return new ExcelResult<OutputExcel>(listResult, fileName, titlestr);
        }

        #endregion
    }
}