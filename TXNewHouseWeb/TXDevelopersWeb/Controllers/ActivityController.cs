using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using TXModel;
using Webdiyer.WebControls.Mvc;
using TXOrm;
using TXCommons;
using TXModel.Commons;
using System.Data;
using TXModel.NHouseActivies.Discount;
using TXBll.NHouseActivies.Discount;
using TXBll.NHouseActivies.Arranging;
using TXBll.NHouseActivies.TuanGou;
using TXModel.Dev;
using TXBll.NHouseActivies;
using TXBll.NHouseActivies.Biding;
using TXModel.NHouseActivies.Common;
namespace TXDevelopersWeb.Controllers
{
    public class ActivityController : BaseController
    {
        ExportExcel excel = new ExportExcel();
        int excelRows = 0;
        int excelColumns;

        #region 竞价
        /// <summary>
        /// 报名用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult BidingUserList()
        {
            ViewData["CurrentMenu"] = 52;
            BidingBll bll = new BidingBll();
            int houseId = Convert.ToInt32(Request.QueryString["houseId"]);
            ViewData["HouseId"] = houseId;
            ViewData["Title"] = Request.QueryString["title"] ?? "";
            List<TXModel.NHouseActivies.Biding.CT_UserList> list = bll.GetActivitieBidingUser(houseId);
            foreach (var item in list)
            {
                if (item.Status == 2)
                {
                    ViewData["Status"] = item.Status;//说明已经成交了
                    break;
                }
                else
                {
                    ViewData["Status"] = 0;//否则没有成交
                }
            }
            return View("Biding/UserList", list);
        }

        /// <summary>
        /// 导出竞价用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult BidingExportExcel()
        {
            TXBll.NHouseActivies.Biding.BidingBll bll = new TXBll.NHouseActivies.Biding.BidingBll();
            int houseId = Convert.ToInt32(Request.QueryString["id"]);
            List<TXModel.NHouseActivies.Biding.CT_UserList> list = bll.GetActivitieBidingUser(houseId);
            DataTable DT = new DataTable();
            DT.Columns.Add("LoginName");
            DT.Columns.Add("RealName");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("QQ");
            DT.Columns.Add("E-mail");
            DT.Columns.Add("LastBidTime");
            DT.Columns.Add("BidCount");
            DT.Columns.Add("LastBidPrice");
            if (list != null && list.Count > 0)
            {
                foreach (TXModel.NHouseActivies.Biding.CT_UserList user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.LoginName;
                    row[1] = user.RealName;
                    row[2] = user.IDCard;
                    row[3] = user.Mobile;
                    row[4] = user.QQ;
                    row[5] = user.Email;
                    row[6] = string.Format("{0:yyyy-MM-dd HH:mm}", user.LastBidTime);
                    row[7] = user.BidCount;
                    row[8] = user.LastBidPrice;
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("出价人,真实姓名,身份证号,手机号,QQ号码,E-mail,最后出价时间,出价次数,最后出价金额");
            InitExcelData(DT, "LoginName,RealName,IDCard,Mobile,QQ,E-mail,LastBidTime,BidCount,LastBidPrice", Server.UrlEncode("竞价用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建竞价活动页
        /// author：李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult Biding(int Id = 1)
        {
            ViewData["type"] = "5";
            ViewData["CurrentMenu"] = 52;

            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<HouseInfoModel> paging = new Paging<HouseInfoModel>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 5;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = 2, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> property = activityBll.GetPremise(developerId);
                ViewData["Premise"] = property;

                return View("Biding/Biding");
            }
            else
            {
                Paging<HouseInfoModel> houselist = activityBll.GetHouse(developerId, premisesId, buildingId, unitId, floor, status, paging);
                PagedList<HouseInfoModel> plist = new PagedList<HouseInfoModel>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

                return PartialView("Common/Arranging", plist);
            }
        }

        /// <summary>
        /// 提交创建竞价活动
        /// author：李刚
        /// </summary>
        /// <param name="activName">活动名称</param>
        /// <param name="bidPrice">起拍价格</param>
        /// <param name="addPrice">加价幅度</param>
        /// <param name="maxPrice">最大幅度</param>
        /// <param name="bond">活动保证金金额</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="ids">房源Id集合，以逗号分隔</param>
        public ActionResult CreateBiding(int cityId, int premisesId, string activName, decimal bidPrice, decimal addPrice, decimal maxPrice, decimal bond, DateTime start, DateTime end, String ids, int activState)
        {
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            activState = 0;
            if (TXCommons.Admins.AdminComs.Instance.IsActivityStartInToday(start))
            {
                activState = 1;
            }

            int result = activityBll.CreateActivity(cityId, user.Id, 5, null, null, "竞价活动", bidPrice, addPrice, maxPrice, bond, start, end.AddHours(23).AddMinutes(59).AddSeconds(59), ids, activState);
            if (result != 0)
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", premisesId, cityId);
            return Json(new { sucess = result });
        }

        /// <summary>
        /// 竞价活动列表
        /// author：李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult BidingList(int Id = 1)
        {
            ViewData["type"] = "5";
            ViewData["CurrentMenu"] = 52;
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<ActivityHouse> paging = new Paging<ActivityHouse>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 10;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = -1, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            Paging<ActivityHouse> houselist = activityBll.GetActivityHouseList(developerId, premisesId, buildingId, unitId, floor, activState, 5, paging);
            PagedList<ActivityHouse> plist = new PagedList<ActivityHouse>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> premise = activityBll.GetPremise(developerId);
                ViewData["Premise"] = premise;

                return View("Biding/BidingList", plist);
            }
            else
            {
                return PartialView("Common/BarginBidingHouseList", plist);
            }
        }
        #endregion

        #region 砍价
        /// <summary>
        /// 报名用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult BargainUserList()
        {
            ViewData["CurrentMenu"] = 53;
            TXBll.NHouseActivies.Bargain.Bargain_DeveloperBll bll = new TXBll.NHouseActivies.Bargain.Bargain_DeveloperBll();
            int houseId = Convert.ToInt32(Request.QueryString["houseId"]);
            ViewData["HouseId"] = houseId;
            ViewData["Title"] = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString().ToString();
            List<TXModel.NHouseActivies.Bargain.CT_UserList> list = bll.GetActivitieBargainUser(houseId);
            foreach (var item in list)
            {
                if (item.Status == 2)
                {
                    ViewData["Status"] = item.Status;
                    break;
                }
                else
                {
                    ViewData["Status"] = 0;
                }
            }
            return View("Bargain/UserList", list);
        }

        /// <summary>
        /// 导出砍价用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult BargainExportExcel()
        {
            TXBll.NHouseActivies.Bargain.Bargain_DeveloperBll bll = new TXBll.NHouseActivies.Bargain.Bargain_DeveloperBll();
            int houseId = Convert.ToInt32(Request.QueryString["id"]);
            List<TXModel.NHouseActivies.Bargain.CT_UserList> list = bll.GetActivitieBargainUser(houseId);
            DataTable DT = new DataTable();
            DT.Columns.Add("LoginName");
            DT.Columns.Add("RealName");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("QQ");
            DT.Columns.Add("E-mail");
            DT.Columns.Add("LastBidTime");
            DT.Columns.Add("BidCount");
            DT.Columns.Add("LastBidPrice");
            if (list != null && list.Count > 0)
            {
                foreach (TXModel.NHouseActivies.Bargain.CT_UserList user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.LoginName;
                    row[1] = user.RealName;
                    row[2] = user.IDCard;
                    row[3] = user.Mobile;
                    row[4] = user.QQ;
                    row[5] = user.Email;
                    row[6] = string.Format("{0:yyyy-MM-dd HH:mm}", user.LastBidTime);
                    row[7] = user.BidCount;
                    row[8] = user.LastBidPrice;
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("出价人,真实姓名,身份证号,手机号,QQ号码,E-mail,最后出价时间,出价次数,最后出价金额");
            InitExcelData(DT, "LoginName,RealName,IDCard,Mobile,QQ,E-mail,LastBidTime,BidCount,LastBidPrice", Server.UrlEncode("砍价用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建砍价活动页
        /// 李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult Bargain(int Id = 1)
        {
            ViewData["type"] = "6";
            ViewData["CurrentMenu"] = 53;

            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<HouseInfoModel> paging = new Paging<HouseInfoModel>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 5;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = 2, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> property = activityBll.GetPremise(developerId);
                ViewData["Premise"] = property;

                return View("Bargain/Bargain");
            }
            else
            {
                Paging<HouseInfoModel> houselist = activityBll.GetHouse(developerId, premisesId, buildingId, unitId, floor, status, paging);
                PagedList<HouseInfoModel> plist = new PagedList<HouseInfoModel>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

                return PartialView("Common/Arranging", plist);
            }
        }

        /// <summary>
        /// 提交创建砍价活动
        /// 李刚
        /// </summary>
        /// <param name="activName">活动名称</param>
        /// <param name="bidPrice">起拍价格</param>
        /// <param name="addPrice">加价幅度</param>
        /// <param name="maxPrice">最大加价幅度</param>
        /// <param name="bond">保证金</param>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="ids">房源Id集合，已逗号隔开</param>
        /// <returns></returns>
        public ActionResult CreateBargain(int cityId, int premisesId, string activName, decimal bidPrice, decimal addPrice, decimal maxPrice, decimal bond, DateTime start, DateTime end, String ids, int activState)
        {
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            //activState = start <= DateTime.Now ? 1 : 0;
            activState = 0;
            if (TXCommons.Admins.AdminComs.Instance.IsActivityStartInToday(start))
            {
                activState = 1;
            }

            int result = activityBll.CreateActivity(cityId, user.Id, 6, null, null, "砍价活动", bidPrice, addPrice, maxPrice, bond, start, end.AddHours(23).AddMinutes(59).AddSeconds(59), ids, activState);
            if (result != 0)
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", premisesId, cityId);
            return Json(new { sucess = result });
        }

        /// <summary>
        /// 砍价活动房源列表
        /// 李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult BargainList(int Id = 1)
        {
            ViewData["type"] = "6";
            ViewData["CurrentMenu"] = 53;
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<ActivityHouse> paging = new Paging<ActivityHouse>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 10;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = -1, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            Paging<ActivityHouse> houselist = activityBll.GetActivityHouseList(developerId, premisesId, buildingId, unitId, floor, activState, 6, paging);
            PagedList<ActivityHouse> plist = new PagedList<ActivityHouse>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> premise = activityBll.GetPremise(developerId);
                ViewData["Premise"] = premise;

                return View("Bargain/BargainList", plist);
            }
            else
            {
                return PartialView("Common/BarginBidingHouseList", plist);
            }
        }

        #endregion

        #region 秒杀
        /// <summary>
        /// 报名用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult SecKillUserList()
        {
            ViewData["CurrentMenu"] = 54;
            TXBll.NHouseActivies.SecKill.SecKill_DeveloperBll bll = new TXBll.NHouseActivies.SecKill.SecKill_DeveloperBll();
            int houseId = Convert.ToInt32(Request.QueryString["houseId"]);
            ViewData["HouseId"] = houseId;
            ViewData["Title"] = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString().ToString();
            List<TXModel.NHouseActivies.SecKill.CT_UserList> list = bll.GetActivitieSecKillUser(houseId);
            return View("SecKill/UserList", list);
        }

        /// <summary>
        /// 导出秒杀用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult SecKillExportExcel()
        {
            TXBll.NHouseActivies.SecKill.SecKill_DeveloperBll bll = new TXBll.NHouseActivies.SecKill.SecKill_DeveloperBll();
            int houseId = Convert.ToInt32(Request.QueryString["id"]);
            List<TXModel.NHouseActivies.SecKill.CT_UserList> list = bll.GetActivitieSecKillUser(houseId);
            DataTable DT = new DataTable();
            DT.Columns.Add("LoginName");
            DT.Columns.Add("RealName");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("QQ");
            DT.Columns.Add("E-mail");
            DT.Columns.Add("BidTime");
            if (list != null && list.Count > 0)
            {
                foreach (TXModel.NHouseActivies.SecKill.CT_UserList user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.LoginName;
                    row[1] = user.RealName;
                    row[2] = user.IDCard;
                    row[3] = user.Mobile;
                    row[4] = user.QQ;
                    row[5] = user.Email;
                    row[6] = string.Format("{0:yyyy-MM-dd HH:mm}", user.BidTime);
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("出价人,真实姓名,身份证号,手机号,QQ号码,E-mail,出价时间");
            InitExcelData(DT, "LoginName,RealName,IDCard,Mobile,QQ,E-mail,BidTime", Server.UrlEncode("秒杀用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建秒杀活动页
        /// 李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult SecKill(int Id = 1)
        {
            ViewData["type"] = "7";
            ViewData["CurrentMenu"] = 54;

            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<HouseInfoModel> paging = new Paging<HouseInfoModel>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 5;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = 2, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> property = activityBll.GetPremise(developerId);
                ViewData["Premise"] = property;

                return View("SecKill/SecKill");
            }
            else
            {
                Paging<HouseInfoModel> houselist = activityBll.GetHouse(developerId, premisesId, buildingId, unitId, floor, status, paging);
                PagedList<HouseInfoModel> plist = new PagedList<HouseInfoModel>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

                return PartialView("Common/Arranging", plist);
            }
        }

        /// <summary>
        /// 提交创建秒杀活动
        /// 李刚
        /// </summary>
        /// <param name="activName">活动名称</param>
        /// <param name="bidPrice">起拍价格</param>
        /// <param name="addPrice">加价幅度</param>
        /// <param name="maxPrice">最大加价幅度</param>
        /// <param name="bond">保证金</param>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="ids">房源Id集合，已逗号隔开</param>
        /// <returns></returns>
        public ActionResult CreateSecKill(int? cityId, int? premisesId, decimal? bidPrice, decimal? bond, DateTime? start, DateTime? end, String ids, int activState)
        {

            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            //activState = start <= DateTime.Now ? 1 : 0;
            activState = 0;
            if (TXCommons.Admins.AdminComs.Instance.IsActivityStartInHour(Convert.ToDateTime(start)))
            {
                activState = 1;
            }

            int result = activityBll.CreateActivity(cityId.Value, user.Id, 7, null, null, "秒杀活动", bidPrice.Value, 0, 0, bond.Value, start.Value, end.Value, ids, activState);
            if (result != 0)
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", premisesId.Value, cityId.Value);
            return Json(new { sucess = result });
        }

        /// <summary>
        /// 秒杀活动房源列表
        /// 李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult SecKillList(int Id = 1)
        {
            ViewData["type"] = "7";
            ViewData["CurrentMenu"] = 54;
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<ActivityHouse> paging = new Paging<ActivityHouse>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 10;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = -1, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            Paging<ActivityHouse> houselist = activityBll.GetActivityHouseList(developerId, premisesId, buildingId, unitId, floor, activState, 7, paging);
            PagedList<ActivityHouse> plist = new PagedList<ActivityHouse>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> premise = activityBll.GetPremise(developerId);
                ViewData["Premise"] = premise;

                return View("SecKill/SecKillList", plist);
            }
            else
            {
                return PartialView("Common/APriceSecKillHouseList", plist);
            }
        }
        #endregion

        #region 一口价
        /// <summary>
        /// 报名用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult APriceUserList()
        {
            ViewData["CurrentMenu"] = 55;
            TXBll.NHouseActivies.APrice.APrice_DeveloperBll bll = new TXBll.NHouseActivies.APrice.APrice_DeveloperBll();
            int houseId = Convert.ToInt32(Request.QueryString["houseId"]);
            ViewData["HouseId"] = houseId;
            ViewData["Title"] = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString().ToString();
            List<TXModel.NHouseActivies.APrice.CT_UserList> list = bll.GetActivitieAPriceUser(houseId);
            return View("APrice/UserList", list);
        }

        /// <summary>
        /// 导出一口价用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult APriceExportExcel()
        {
            TXBll.NHouseActivies.APrice.APrice_DeveloperBll bll = new TXBll.NHouseActivies.APrice.APrice_DeveloperBll();
            int houseId = Convert.ToInt32(Request.QueryString["id"]);
            List<TXModel.NHouseActivies.APrice.CT_UserList> list = bll.GetActivitieAPriceUser(houseId);
            DataTable DT = new DataTable();
            DT.Columns.Add("LoginName");
            DT.Columns.Add("RealName");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("QQ");
            DT.Columns.Add("E-mail");
            DT.Columns.Add("BidTime");
            if (list != null && list.Count > 0)
            {
                foreach (TXModel.NHouseActivies.APrice.CT_UserList user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.LoginName;
                    row[1] = user.RealName;
                    row[2] = user.IDCard;
                    row[3] = user.Mobile;
                    row[4] = user.QQ;
                    row[5] = user.Email;
                    row[6] = string.Format("{0:yyyy-MM-dd HH:mm}", user.BidTime);
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("出价人,真实姓名,身份证号,手机号,QQ号码,E-mail,出价时间");
            InitExcelData(DT, "LoginName,RealName,IDCard,Mobile,QQ,E-mail,BidTime", Server.UrlEncode("一口价用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建一口价活动页
        /// 李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult APrice(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 55;

            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<HouseInfoModel> paging = new Paging<HouseInfoModel>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 5;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = 2, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> property = activityBll.GetPremise(developerId);
                ViewData["Premise"] = property;

                return View("APrice/APrice");
            }
            else
            {
                Paging<HouseInfoModel> houselist = activityBll.GetHouse(developerId, premisesId, buildingId, unitId, floor, status, paging);
                PagedList<HouseInfoModel> plist = new PagedList<HouseInfoModel>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

                return PartialView("Common/Arranging", plist);
            }
        }

        /// <summary>
        /// 提交创建一口价活动
        /// 李刚
        /// </summary>
        /// <param name="activName">活动名称</param>
        /// <param name="bidPrice">起拍价格</param>
        /// <param name="addPrice">加价幅度</param>
        /// <param name="maxPrice">最大加价幅度</param>
        /// <param name="bond">保证金</param>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="ids">房源Id集合，已逗号隔开</param>
        /// <returns></returns>
        public ActionResult CreateAPrice(int cityId, int premisesId, decimal bidPrice, decimal bond, DateTime start, DateTime end, String ids, int activState)
        {
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            //activState = start <= DateTime.Now ? 1 : 0;
            activState = 0;
            if (TXCommons.Admins.AdminComs.Instance.IsActivityStartInHour(Convert.ToDateTime(start)))
            {
                activState = 1;
            }

            int result = activityBll.CreateActivity(cityId, user.Id, 8, null, null, "一口价活动", bidPrice, 0, 0, bond, start, end.AddHours(23).AddMinutes(59).AddSeconds(59), ids, activState);
            if (result != 0)
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", premisesId, cityId);
            return Json(new { sucess = result });
        }

        /// <summary>
        /// 一口价活动房源列表
        /// 李刚
        /// </summary>
        /// <param name="Id">第几页</param>
        /// <returns></returns>
        public ActionResult APriceList(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 55;
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<ActivityHouse> paging = new Paging<ActivityHouse>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 10;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status = -1, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            Paging<ActivityHouse> houselist = activityBll.GetActivityHouseList(developerId, premisesId, buildingId, unitId, floor, activState, 8, paging);
            PagedList<ActivityHouse> plist = new PagedList<ActivityHouse>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> premise = activityBll.GetPremise(developerId);
                ViewData["Premise"] = premise;

                return View("APrice/APriceList", plist);
            }
            else
            {
                return PartialView("Common/APriceSecKillHouseList", plist);
            }
        }

        #endregion

        #region 成交中标用户
        /// <summary>
        /// 公布活动结果
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult AnnounceActivitieResult()
        {
            TXBll.NHouseActivies.Biding.BidingBll bll = new TXBll.NHouseActivies.Biding.BidingBll();
            int houseId = Convert.ToInt32(Request.QueryString["houseId"]);
            int bidPriceId = Convert.ToInt32(Request.QueryString["bidPriceId"]);

            if (bll.AnnounceActivitieResult(houseId, bidPriceId) > 0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 查询楼栋
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <returns></returns>
        public ActionResult GetBuilding(int PremisesId)
        {
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();
            List<BuildingModel> list = activityBll.GetBuilding(PremisesId);
            return Json(list);
        }
        /// <summary>
        /// 查新单元
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <returns></returns>
        public ActionResult GetUnit(int PremisesId, int BuildingId)
        {
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();
            List<UnitModel> list = activityBll.GetUnit(PremisesId, BuildingId);
            return Json(list);
        }
        /// <summary>
        /// 查询楼层
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <returns></returns>
        public ActionResult GetFloor(int PremisesId, int BuildingId, int UnitId)
        {
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();
            List<int> list = activityBll.GetFloor(PremisesId, BuildingId, UnitId);
            return Json(list);
        }
        /// <summary>
        /// 查询楼层
        /// author: 李刚
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <returns></returns>
        public ActionResult GetFloor2(int PremisesId, int BuildingId)
        {
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();
            List<int> list = activityBll.GetFloor(PremisesId, BuildingId);
            return Json(list);
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// Excel头部
        /// author:汪敏
        /// </summary>
        /// <param name="headStr"></param>
        public void ExcelTitle(string headStr)
        {

            excel.RowStart();
            string[] heads = headStr.Split(',');
            for (int i = 0; i < heads.Length; i++)
            {
                excel.CellWithoutFormula("String", heads[i]);
            }
            excel.RowEnd();
            excelRows++;
            excelColumns = heads.Length;
        }
        /// <summary>
        /// Excel内容
        /// author:汪敏
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="propertyStr"></param>
        /// <param name="FileName"></param>
        private void InitExcelData(DataTable dt, string propertyStr, string FileName)
        {
            try
            {

                string[] propertys = propertyStr.Split(',');
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    excel.RowStart();
                    for (int j = 0; j < propertys.Length; j++)
                    {
                        excel.CellWithoutFormula("String", dt.Rows[i][propertys[j]].ToString());
                    }
                    excel.RowEnd();
                    excelRows++;
                }
                excel.CreateExcelWithMode(excelRows, excelColumns, FileName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 摇号(暂时屏蔽)
        /// <summary>
        /// 摇号活动列表
        /// author:汪敏
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult YaoHaoList(int? page)
        {
            ViewData["CurrentMenu"] = 51;
            Paging<TXModel.NHouseActivies.YaoHao.CT_YaoHao> paging = new Paging<TXModel.NHouseActivies.YaoHao.CT_YaoHao>();
            paging.PageIndex = page.HasValue ? page.Value : 1;
            paging.PageSize = 10;
            TXBll.NHouseActivies.Yaohao.YaohaoBll _bll = new TXBll.NHouseActivies.Yaohao.YaohaoBll();
            try
            {
                paging = _bll.GetYaohaoList(LoginUserInfo.Id, paging);
                PagedList<TXModel.NHouseActivies.YaoHao.CT_YaoHao> pagelist = new PagedList<TXModel.NHouseActivies.YaoHao.CT_YaoHao>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
                ViewData["TotalItemCount"] = pagelist.TotalItemCount;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("YaoHao/YaoHaoTable", pagelist);
                }
                else
                {
                    return View("YaoHao/List", pagelist);
                }
            }
            catch (Exception e)
            {
                ViewData["TotalItemCount"] = 0;
                return View("YaoHao/List");
            }
        }
        /// <summary>
        /// 申请摇号活动页面
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult YaoHaoAdd()
        {
            ViewData["CurrentMenu"] = 51;
            TXBll.HouseData.PremisesBll _bll = new TXBll.HouseData.PremisesBll();
            ViewData["Premises"] = _bll.GetPremisByDeveloperId(LoginUserInfo.Id);
            return View("YaoHao/Add");
        }
        /// <summary>
        /// 根据楼盘id获取楼栋
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBuildingByPremisesId()
        {
            TXBll.HouseData.BuildingBll _bll = new TXBll.HouseData.BuildingBll();
            int premisesId = Convert.ToInt32(Request.QueryString["premisesId"]);
            List<Building> list = _bll.GetBuildingByPremisesId(premisesId, LoginUserInfo.Id);
            if (list != null)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// (楼盘)楼栋下是否有房源参与了摇号
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckYaohao()
        {
            int premisesId = Convert.ToInt32(Request.QueryString["premisesId"]);
            int buildingId = Convert.ToInt32(Request.QueryString["buildingId"]);
            TXBll.NHouseActivies.Yaohao.YaohaoBll _bll = new TXBll.NHouseActivies.Yaohao.YaohaoBll();
            int result;
            if (premisesId == 0)
            {
                result = _bll.CheckYaohao(LoginUserInfo.Id, premisesId, buildingId);
            }
            else
            {
                result = _bll.CheckYaohao(LoginUserInfo.Id, premisesId, 0);
            }
            return Content(result.ToString());
        }
        /// <summary>
        /// 添加摇号活动申请
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddYaoHao()
        {
            TXBll.NHouseActivies.Yaohao.YaohaoBll _bll = new TXBll.NHouseActivies.Yaohao.YaohaoBll();
            TXOrm.ActivitiesYaoHaoApply activity = new TXOrm.ActivitiesYaoHaoApply();
            activity.ActivitiesId = 0;
            activity.DeveloperId = LoginUserInfo.Id;
            activity.State = 0;
            activity.CreateTime = DateTime.Now;
            activity.UpdateTime = DateTime.Now;
            activity.IsDel = false;
            activity.Name = Request.Form["name"].ToString();
            activity.PremisesId = Convert.ToInt32(Request.Form["s_loupan"]);
            activity.IsAllPremises = Convert.ToInt32(Request.Form["hid_IsAllPremises"]) == 1 ? true : false;
            if (activity.IsAllPremises == true)
            {
                TXBll.HouseData.BuildingBll bll = new TXBll.HouseData.BuildingBll();
                List<Building> blist = bll.GetBuildingByPremisesId(activity.PremisesId, LoginUserInfo.Id);//获取该楼盘下所有楼栋
                activity.BuildingIds = "";
                if (blist != null && blist.Count > 0)
                    activity.BuildingIds = string.Join(",", blist.Select(b => b.Id).ToArray());
            }
            else
            {
                activity.BuildingIds = Request.Form["hid_loudong"].ToString();
            }

            int result = _bll.Add(activity);
            if (result == 1)
            {
                return RedirectToAction("Success", "Activity");
            }
            else
            {
                return RedirectToAction("YaoHaoAdd", "Activity");
            }
        }
        /// <summary>
        /// 申请成功页面
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {
            ViewData["CurrentMenu"] = 51;
            return View("YaoHao/Success");
        }
        /// <summary>
        /// 用户报名列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult YaoHaoUserList()
        {
            TXBll.NHouseActivies.Yaohao.YaohaoBll _bll = new TXBll.NHouseActivies.Yaohao.YaohaoBll();
            int activityid = Convert.ToInt32(Request.QueryString["id"]);
            ViewData["Activityid"] = activityid.ToString();
            ViewData["Title"] = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString() == null ? "" : Request.QueryString["title"].ToString();
            List<TXOrm.BidPrice> list = _bll.GetYaohaoUserList(activityid);
            return View("YaoHao/UserList", list);
        }
        /// <summary>
        /// 导出摇号用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult YaoHaoExportExcel()
        {
            TXBll.NHouseActivies.Yaohao.YaohaoBll _bll = new TXBll.NHouseActivies.Yaohao.YaohaoBll();
            int activityid = Convert.ToInt32(Request.QueryString["id"]);
            List<TXOrm.BidPrice> list = _bll.GetYaohaoUserList(activityid);
            DataTable DT = new DataTable();
            DT.Columns.Add("Name");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("Num");
            DT.Columns.Add("CreateTime");
            if (list != null && list.Count > 0)
            {
                foreach (TXOrm.BidPrice user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.BidUserName;
                    row[1] = user.IDCard;
                    row[2] = user.BidUserMobile;
                    row[3] = user.BidUserNumber;
                    row[4] = string.Format("{0:yyyy-MM-dd HH:mm}", user.CreateTime);
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("报名人,身份证号,手机号,摇号号码,报名时间");
            InitExcelData(DT, "Name,IDCard,Mobile,Num,CreateTime", Server.UrlEncode("摇号用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 限时折扣(暂时屏蔽)
        public ActionResult DiscountList(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 57;
            Paging<CT_Activity> paging = new Paging<CT_Activity>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 10;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();
            Paging<CT_Activity> list = activityBll.GetActivityPageList2(paging, 2);
            PagedList<CT_Activity> plist = new PagedList<CT_Activity>(list.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            return View("Discount/DiscountList", plist);
        }

        public ActionResult Discount(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 57;

            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<HouseInfoModel> paging = new Paging<HouseInfoModel>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 3;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> property = activityBll.GetPremise(developerId);
                ViewData["Premise"] = property;
                return View("Discount/Discount");
            }
            else
            {
                Paging<HouseInfoModel> houselist = activityBll.GetHouse(developerId, premisesId, buildingId, unitId, floor, status, paging);
                PagedList<HouseInfoModel> plist = new PagedList<HouseInfoModel>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
                return PartialView("Common/Arranging", plist);
            }
        }

        public ActionResult CreateActivity(string activity, string house)
        {
            Discount_DeveloperBll discountBLL = new Discount_DeveloperBll();
            string[] a = activity.Split(',');
            string[] h = house.Split('|').Where(it => it.Length > 0).ToArray();
            int result = discountBLL.CreateActivity(a, h);
            return Json(new { sucess = result });
        }

        [ValidateInput(false)]
        public ActionResult CreateDiscount(string activity, string house)
        {
            Discount_DeveloperBll discountBLL = new Discount_DeveloperBll();
            string[] a = activity.Split('&');
            string[] h = house.Split(',');
            int result = discountBLL.CreateActivity(a, h);
            if (result != 0)
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", Util.ToInt(a[5]), Util.ToInt(a[6]));
            return Json(new { sucess = result });
        }

        private void GetParams(out int premisesId, out int buildId, out int unitId, out int floorId, out int saleState, out int activState)
        {
            if (!int.TryParse(Request["premisesId"], out premisesId))
            {
                premisesId = -1;
            }
            if (!int.TryParse(Request["buildId"], out buildId))
            {
                buildId = -1;
            }
            if (!int.TryParse(Request["unitId"], out unitId))
            {
                unitId = -1;
            }
            if (!int.TryParse(Request["floorId"], out floorId))
            {
                floorId = -100;
            }
            if (!int.TryParse(Request["saleState"], out saleState))
            {
                saleState = -1;
            }
            if (!int.TryParse(Request["activState"], out activState))
            {
                activState = -1;
            }
        }

        /// <summary>
        /// 报名用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult DiscountUserList()
        {
            ViewData["CurrentMenu"] = 57;
            TXBll.HouseData.PremisesBll _bll = new TXBll.HouseData.PremisesBll();
            ViewData["Premises"] = _bll.GetPremisByDeveloperId(LoginUserInfo.Id);
            TXBll.NHouseActivies.Discount.Discount_DeveloperBll bll = new TXBll.NHouseActivies.Discount.Discount_DeveloperBll();
            int activityId = Convert.ToInt32(Request.QueryString["activityId"]);
            ViewData["ActivityId"] = activityId;
            ViewData["Title"] = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString().ToString();
            List<TXModel.NHouseActivies.Discount.CT_UserList> list = bll.GetActivitieDiscountsUser(activityId, 0, 0, 0);
            return View("Discount/UserList", list);
        }
        /// <summary>
        /// 根据楼栋id获取单元
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUnitByBuildingId()
        {
            TXBll.HouseData.UnitBll _bll = new TXBll.HouseData.UnitBll();
            int buildingId = Convert.ToInt32(Request.QueryString["buildingId"]);
            List<Unit> list = _bll.GetUnitByBuildingId(buildingId);
            if (list != null)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 用户列表搜索
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public JsonResult SearchDiscountUser()
        {
            TXBll.NHouseActivies.Discount.Discount_DeveloperBll bll = new TXBll.NHouseActivies.Discount.Discount_DeveloperBll();
            int activityId = Convert.ToInt32(Request.QueryString["activityId"]);
            int premisesId = Convert.ToInt32(Request.QueryString["premisesId"]);
            int buildingId = Convert.ToInt32(Request.QueryString["buildingId"]);
            int unitId = Convert.ToInt32(Request.QueryString["unitId"]);
            List<TXModel.NHouseActivies.Discount.CT_UserList> list = bll.GetActivitieDiscountsUser(activityId, premisesId, buildingId, unitId);
            if (list != null)
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 导出折扣用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult DiscountExportExcel()
        {
            TXBll.NHouseActivies.Discount.Discount_DeveloperBll bll = new TXBll.NHouseActivies.Discount.Discount_DeveloperBll();
            int activityId = Convert.ToInt32(Request.QueryString["activityId"]);
            int premisesId = Convert.ToInt32(Request.QueryString["premisesId"]);
            int buildingId = Convert.ToInt32(Request.QueryString["buildingId"]);
            int unitId = Convert.ToInt32(Request.QueryString["unitId"]);
            List<TXModel.NHouseActivies.Discount.CT_UserList> list = bll.GetActivitieDiscountsUser(activityId, premisesId, buildingId, unitId);
            DataTable DT = new DataTable();
            DT.Columns.Add("RealName");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("CreateTime");
            if (list != null && list.Count > 0)
            {
                foreach (TXModel.NHouseActivies.Discount.CT_UserList user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.RealName;
                    row[1] = user.IDCard;
                    row[2] = user.Mobile;
                    row[3] = string.Format("{0:yyyy-MM-dd HH:mm}", user.CreateTime);
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("真实姓名,身份证号,手机号,出价时间");
            InitExcelData(DT, "RealName,IDCard,Mobile,CreateTime", Server.UrlEncode("折扣用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <returns></returns>
        public ActionResult DiscountDetail(string activityId)
        {
            ViewData["CurrentMenu"] = 57;

            int id = Util.ToInt(activityId);

            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            ViewData["activity"] = activityBll.GetActivityById(id);
            ViewData["actHouses"] = activityBll.GetActivitiesHousesById(id);
            return View("Discount/Detail");
        }

        #endregion

        #region 排号购房(暂时屏蔽)

        public ActionResult CreateArranging(int PremisesId, int? CityId, string ActivName, int? ActivType, decimal? ActivBond, DateTime? ActivStart, DateTime? ActivEnd, int? Count, String ids)
        {
            CT_LoginUser user = base.LoginUserInfo;
            Arranging_DeveloperBll ArrangingBLL = new Arranging_DeveloperBll();
            var Ids = ids.Split(',').Where(it => it.Length > 0).ToArray();
            int result = ArrangingBLL.CreateArranging(CityId.Value, user.Id, ActivName, ActivType.Value, ActivBond.Value, ActivStart.Value, ActivEnd.Value.AddHours(23).AddMinutes(59).AddSeconds(59), Count.Value, Ids);
            if (result != 0)
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", PremisesId, CityId.Value);
            return Json(new { sucess = result });
        }

        public ActionResult Arranging(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 58;
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<HouseInfoModel> paging = new Paging<HouseInfoModel>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 3;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> property = activityBll.GetPremise(developerId);
                ViewData["Premise"] = property;
            }
            else
            {
                Paging<HouseInfoModel> houselist = activityBll.GetHouse(developerId, premisesId, buildingId, unitId, floor, status, paging);
                PagedList<HouseInfoModel> plist = new PagedList<HouseInfoModel>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
                return PartialView("Common/Arranging", plist);
            }

            return View("Arranging/Arranging");
        }
        public ActionResult ArrangingList(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 58;
            Paging<CT_Activity> paging = new Paging<CT_Activity>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 10;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();
            Paging<CT_Activity> list = activityBll.GetActivityPageList2(paging, 3);
            PagedList<CT_Activity> plist = new PagedList<CT_Activity>(list.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            return View("Arranging/ArrangingList", plist);
        }

        /// <summary>
        /// 报名用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrangingUserList()
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 58;
            TXBll.NHouseActivies.Arranging.Arranging_DeveloperBll bll = new TXBll.NHouseActivies.Arranging.Arranging_DeveloperBll();
            int activityId = Convert.ToInt32(Request.QueryString["activityId"]);
            ViewData["Activityid"] = activityId;
            ViewData["Title"] = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString();
            List<TXModel.NHouseActivies.Arranging.CT_UserList> list = bll.GetActivitieArrangingUser(activityId);
            return View("Arranging/UserList", list);
        }

        /// <summary>
        /// 导出排号用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrangingExportExcel()
        {
            TXBll.NHouseActivies.Arranging.Arranging_DeveloperBll bll = new TXBll.NHouseActivies.Arranging.Arranging_DeveloperBll();
            int activityId = Convert.ToInt32(Request.QueryString["id"]);
            List<TXModel.NHouseActivies.Arranging.CT_UserList> list = bll.GetActivitieArrangingUser(activityId);
            DataTable DT = new DataTable();
            DT.Columns.Add("RealName");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("CreateTime");
            if (list != null && list.Count > 0)
            {
                foreach (TXModel.NHouseActivies.Arranging.CT_UserList user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.RealName;
                    row[1] = user.IDCard;
                    row[2] = user.Mobile;
                    row[3] = string.Format("{0:yyyy-MM-dd HH:mm}", user.CreateTime);
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("真实姓名,身份证号,手机号,出价时间");
            InitExcelData(DT, "RealName,IDCard,Mobile,CreateTime", Server.UrlEncode("排号购房用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrangingDetail(string activityId)
        {
            ViewData["CurrentMenu"] = 58;

            int id = Util.ToInt(activityId);

            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            ViewData["activity"] = activityBll.GetActivityById(id);
            ViewData["actHouses"] = activityBll.GetActivitiesHousesById(id);

            return View("Arranging/Detail");
        }
        #endregion

        #region 阶梯团购(暂时屏蔽)

        public ActionResult TuanGouList(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 56;
            Paging<CT_Activity> paging = new Paging<CT_Activity>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 10;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();
            Paging<CT_Activity> list = activityBll.GetActivityPageList2(paging, 4);
            PagedList<CT_Activity> plist = new PagedList<CT_Activity>(list.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            return View("TuanGou/TuanGouList", plist);
        }

        public ActionResult CreateTuanGou(int PremisesId, int CityId, string ActivName, int ActivType, decimal ActivBond, DateTime? ActivStart, DateTime? ActivEnd, String Condi, string Ids)
        {
            CT_LoginUser user = base.LoginUserInfo;
            string[] Arr = Condi.Split('|').Where(it => it.Length > 0).ToArray();
            string[] ids = Ids.Split(',').Where(it => it.Length > 0).ToArray();
            TuanGou_DeveloperBll bll = new TuanGou_DeveloperBll();
            int sucess = bll.CreateTuanGou(CityId, user.Id, ActivName, ActivType, ActivBond, ActivStart.Value, ActivEnd.Value.AddHours(23).AddMinutes(59).AddSeconds(59), Arr, ids);
            if (sucess != 0)
                TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", PremisesId, CityId);
            return Json(new { sucess = sucess });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult TuanGou(int Id = 1)
        {
            ViewData["type"] = "8";
            ViewData["CurrentMenu"] = 56;
            CT_LoginUser user = base.LoginUserInfo;
            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            Paging<HouseInfoModel> paging = new Paging<HouseInfoModel>();
            paging.PageIndex = Id; paging.TotalCount = 0; paging.PageSize = 3;

            int developerId = user.Id, premisesId, buildingId, unitId, floor, status, activState;
            this.GetParams(out premisesId, out buildingId, out unitId, out floor, out status, out activState);

            if (!Request.IsAjaxRequest())
            {
                List<PremiseModel> property = activityBll.GetPremise(developerId);
                ViewData["Premise"] = property;
            }
            else
            {
                Paging<HouseInfoModel> houselist = activityBll.GetHouse(developerId, premisesId, buildingId, unitId, floor, status, paging);
                PagedList<HouseInfoModel> plist = new PagedList<HouseInfoModel>(houselist.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
                return PartialView("Common/Arranging", plist);
            }

            return View("TuanGou/TuanGou");
        }

        /// <summary>
        /// 报名用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult TuanGouUserList()
        {
            ViewData["CurrentMenu"] = 56;
            TXBll.NHouseActivies.TuanGou.TuanGou_DeveloperBll bll = new TXBll.NHouseActivies.TuanGou.TuanGou_DeveloperBll();
            int activityId = Convert.ToInt32(Request.QueryString["activityId"]);
            ViewData["Activityid"] = activityId;
            ViewData["Title"] = Request.QueryString["title"] == null ? "" : Request.QueryString["title"].ToString().ToString();
            List<TXModel.NHouseActivies.TuanGou.CT_UserList> list = bll.GetActivitieTuanGouUser(activityId);
            return View("TuanGou/UserList", list);
        }

        /// <summary>
        /// 导出团购用户列表
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult TuanGouExportExcel()
        {
            TXBll.NHouseActivies.TuanGou.TuanGou_DeveloperBll bll = new TXBll.NHouseActivies.TuanGou.TuanGou_DeveloperBll();
            int activityId = Convert.ToInt32(Request.QueryString["id"]);
            List<TXModel.NHouseActivies.TuanGou.CT_UserList> list = bll.GetActivitieTuanGouUser(activityId);
            DataTable DT = new DataTable();
            DT.Columns.Add("RealName");
            DT.Columns.Add("IDCard");
            DT.Columns.Add("Mobile");
            DT.Columns.Add("CreateTime");
            if (list != null && list.Count > 0)
            {
                foreach (TXModel.NHouseActivies.TuanGou.CT_UserList user in list)
                {
                    DataRow row = DT.NewRow();
                    row[0] = user.RealName;
                    row[1] = user.IDCard;
                    row[2] = user.Mobile;
                    row[3] = string.Format("{0:yyyy-MM-dd HH:mm}", user.CreateTime);
                    DT.Rows.Add(row);
                }
            }
            ExcelTitle("真实姓名,身份证号,手机号,出价时间");
            InitExcelData(DT, "RealName,IDCard,Mobile,CreateTime", Server.UrlEncode("阶梯团购用户列表"));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 团购活动详情
        /// </summary>
        /// <returns></returns>
        public ActionResult TuanGouDetail(string activityId)
        {
            ViewData["CurrentMenu"] = 56;

            int id = Util.ToInt(activityId);

            ActiviesUtilsBll activityBll = new ActiviesUtilsBll();

            ViewData["activity"] = activityBll.GetActivityById(id);
            ViewData["actHouses"] = activityBll.GetActivitiesHousesById(id);
            ViewData["social"] = activityBll.GetSocialByActivityId(id);
            return View("TuanGou/Detail");
        }
        #endregion

    }
}
