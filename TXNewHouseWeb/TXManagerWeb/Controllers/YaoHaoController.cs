using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TXBll.HouseData;
using TXBll.NHouseActivies;
using TXBll.NHouseActivies.Yaohao;
using TXBll.WebSite;
using TXCommons.Admins;
using TXManagerWeb.Common;
using TXManagerWeb.Controllers.ActionFilters;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [AdminPageInfo(6, (int) AdminNavi.NewHouseMg.Activities.Yaohao)]
    public class YaoHaoController : BaseController
    {
        private GeographyBll _geographyBll;
        private PremisesBll _premisesbll;
        private YaohaoBll _yaohaobll;

        #region 列表入口

        /// <summary>
        ///     摇号活动列表
        /// </summary>
        /// <param name="id">0 活动列表 1 审批列表</param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            _geographyBll = new GeographyBll();
            if (!id.HasValue)
            {
                id = 0;
            }
            var search = new PVS_NH_YaoHao
            {
                Type = id.Value,
                ProvinceID = 0,
                CityId = 0,
                ActivitieState = -1,
                ActivitieStateList =
                    AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates(),
                State = -1,
                StateList = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_MarkStates().Where(it => it.Value != "0" && it.Value != "6").ToList(),
                CompanyType = 0,
                CompanyTypeList =
                    AdminTypes.Db_NewHouse.Tb_Developer.Fc_SelectListItems.Get_CompanyType()
            };
            List<SelectListItem> listProvinces = _geographyBll.Z_GetProvinces().ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            search.Provinces.AddRange(listProvinces);

            CurrentPageIndex();
            if (search.Type == 0)
                return View("index", search);
            return View("applyindex", search);
        }

        #endregion

        #region 列表数据搜索

        /// <summary>
        ///     列表数据
        ///     author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(PVS_NH_YaoHao search)
        {
            search.ActivitieStateList =
                AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_ActivitieStates();
            search.StateList =
                AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_MarkStates().Where(it => it.Value != "0" && it.Value != "6").ToList();
            search.CompanyTypeList =
                AdminTypes.Db_NewHouse.Tb_Developer.Fc_SelectListItems.Get_CompanyType();
            _geographyBll = new GeographyBll();
            List<SelectListItem> listProvinces = _geographyBll.Z_GetProvinces().ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            search.Provinces.AddRange(listProvinces);
            if (search.ProvinceID != 0)
            {
                List<SelectListItem> lisetCities = _geographyBll.Z_GetCities(search.ProvinceID).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                search.Cities.AddRange(lisetCities);
            }
            search.ActivitieName = Server.UrlEncode(search.Name);
            search.BeginTime = Server.UrlEncode(search.BeginTime);
            search.EndTime = Server.UrlEncode(search.EndTime);
            CurrentPageIndex();
            if (search.Type == 0)
                return View("index", search);
            return View("applyindex", search);
        }

        /// <summary>
        ///     返回数据
        ///     author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_YaoHao search, int pageIndex, int pageSize)
        {
            _yaohaobll = new YaohaoBll();
            pageIndex = pageIndex + 1;
            if (search.Type == 0)
            {
                List<PVM_NH_YaoHao> list = _yaohaobll.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
                return PartialView("PageTables/Activities/_yaohao", list);
            }
            if (search.Type == 1)
            {
                List<PVM_NH_YaoHaoApply> list = _yaohaobll.GetYaoHaoApplyPageList_BySearch_Admin(search, pageIndex, pageSize);
                return PartialView("PageTables/Activities/_yaohaoapproval", list);
            }
            return PartialView("PageTables/Activities/_yaohao", null);
        }

        /// <summary>
        ///     结果数量
        ///     author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_YaoHao search)
        {
            _yaohaobll = new YaohaoBll();
            int count = 0;
            if (search.Type == 0)
                count = _yaohaobll.GetPageCount_BySearch_Admin(search);
            if (search.Type == 1)
                count = _yaohaobll.GetYaoHaoApplyPageCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #region 发布摇号活动

        /// <summary>
        ///     发布摇号活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add(int id)
        {
            var model = new PVE_NH_YaoHao
            {
                Id = id,
                NotarialOfficeList = AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_NotarialOffice()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Addv01(PVE_NH_YaoHao model)
        {
            _yaohaobll = new YaohaoBll();
            ActivitiesYaoHaoApply apply = _yaohaobll.GetActivitiesYaoHaoApply_ById_Admin(model.Id);
            var yaohao = new PVM_NH_YaoHao
            {
                Id = apply.ActivitiesId,
                DeveloperId = apply.DeveloperId,
                Name = model.Name,
                BidPrice = 0,
                AddPrice = 0,
                MaxPrice = 0,
                BeginTime = model.BeginTime,
                EndTime = model.EndTime,
                Bond = model.Bond,
                ActivitiesYaoHaoApplyId = model.Id,
                ActivitieLocation = "", //摇号地点  取配置
                BuildingIds = apply.BuildingIds,
                NotarialOffice = model.NotarialOffice,
                SignupBeginTime = model.ApplyBeginTime,
                SignupEndTime = model.ApplyEndTime,
                ChooseHouseTime = model.LectHouseTime,
                Introduction = model.ActivitiesIntroduction,
                Notice = model.ActivitiesNotice,
                Process = model.ActivitiesProcess
            };
            ESqlResult result = _yaohaobll.AddYaoHaoActivities_Admin(yaohao);
            if (result.Code != "0")
            {
                int adminId = Convert.ToInt32(Request["adminId"]);
                ExecuteOperResult(new Z_OperAdminLog
                {
                    AdminID = adminId,
                    CreateTime = DateTime.Now,
                    OperIP = Request.UserHostAddress,
                    OperDes = string.Format("新房管理-发布摇号活动[{0}]", result.Code),
                    Type = 0
                });
                return Redirect(Url.SiteUrl().YaoHaoIndex(0));
            }
            return Content(GetPageMsg(result.Msg, Url.SiteUrl().YaoHaoIndex(1)));
        }

        [HttpPost]
        public JsonResult Add(PVE_NH_YaoHao model)
        {
            _yaohaobll = new YaohaoBll();
            var apply = _yaohaobll.GetActivitiesYaoHaoApply_ById_Admin(model.Id);

            var buildingList = apply.BuildingIds.Split(",".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList();

            if (CheckInBuildingsHouseIsInActivity(buildingList))
            {
                return Json(new {suc = 0, msg = "申请的楼栋中有房源正在参加活动"});
            }

            var yaohao = new PVM_NH_YaoHao
            {
                Id = apply.ActivitiesId,
                DeveloperId = apply.DeveloperId,
                Name = model.Name,
                BidPrice = 0,
                AddPrice = 0,
                MaxPrice = 0,
                BeginTime = model.BeginTime,
                EndTime = model.EndTime,
                Bond = model.Bond,
                ActivitiesYaoHaoApplyId = model.Id,
                ActivitieLocation = "", //摇号地点  取配置
                BuildingIds = apply.BuildingIds,
                NotarialOffice = model.NotarialOffice,
                SignupBeginTime = model.ApplyBeginTime,
                SignupEndTime = model.ApplyEndTime,
                ChooseHouseTime = model.LectHouseTime,
                Introduction = model.ActivitiesIntroduction,
                Notice = model.ActivitiesNotice,
                Process = model.ActivitiesProcess
            };

            ESqlResult result = _yaohaobll.AddYaoHaoActivities_Admin(yaohao);

            return Json(new { suc = result.Code, msg = result.Msg });
        }

        /// <summary>
        /// 检查楼栋列表中是否有房源正
        /// </summary>
        /// <param name="buildingIds"></param>
        /// <returns></returns>
        private bool CheckInBuildingsHouseIsInActivity(List<string> buildingIds)
        {
            for (int i = 0; i < buildingIds.Count; i++)
            {
                if (0 < new ActiviesUtilsBll().GetLivingHouseCountByBuildingId(Convert.ToInt32(buildingIds[i])))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region 修改

        [HttpGet]
        public ActionResult Modify(int id)
        {
            _yaohaobll = new YaohaoBll();
            PVE_NH_YaoHao yaohao = _yaohaobll.GetYaoHaoInfo(id);
            yaohao.NotarialOfficeList =
                AdminTypes.Db_NewHouse.Tb_Activities.Fc_SelectListItems.Get_NotarialOffice();
            return View(yaohao);
        }

        [HttpPost]
        public ActionResult Modify(PVE_NH_YaoHao model)
        {
            _yaohaobll = new YaohaoBll();
            ESqlResult yaohao = _yaohaobll.UpdateYaoHaoInfo_Admin(model);
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().YaoHaoIndex(0) : Request.Params["backurl"];
            if (yaohao.Code == "1")
                return Content(GetPageMsg("修改成功", ViewData["backurl"].ToString()));
            return Content(GetPageMsg("修改失败", Url.SiteUrl().ModifyYaoHao(model.Id)));
        }

        #endregion

        #region 详情

        [HttpGet]
        public ActionResult Info(int id)
        {
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().YaoHaoIndex(0) : Request.Params["backurl"];
            _yaohaobll = new YaohaoBll();
            PVE_NH_YaoHao yaohao = _yaohaobll.GetYaoHaoInfo(id);
            return View(yaohao);
        }

        #endregion

        #region 删除

        public ActionResult Delete(int id)
        {
            _yaohaobll = new YaohaoBll();
            //var yaohao = _yaohaobll.GetYaoHaoInfo(id);
            //删除摇号
            //return Json(new { result = true, message = "操作成功。" });
            return Json(new {result = false, message = "暂未删除"});
        }

        #endregion

        #region 更新摇号活动标记状态

        /// <summary>
        ///     更新摇号活动标记状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="adminId"></param>
        /// <param name="adminName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateYaoHaoState(int id, int state, int adminId, string adminName)
        {
            try
            {
                _yaohaobll = new YaohaoBll();
                ESqlResult result = _yaohaobll.UpdateYaoHaoState_Admin(id, state);
                if (result.Code == "1")
                {
                    ExecuteOperResult(new Z_OperAdminLog
                    {
                        AdminID = adminId,
                        CreateTime = DateTime.Now,
                        OperIP = Request.UserHostAddress,
                        OperDes = string.Format("新房管理-修改摇号活动id[{0}],state[{1}]", id, state),
                        Type = 0
                    });

                    return Json(new {result = true, message = ""});
                }

                return Json(new {result = false, message = result.Msg});
            }
            catch (Exception ex)
            {
                return Json(new {result = false, message = "操作失败：" + ex.Message});
            }
        }

        #endregion

        /// <summary>
        ///     报名用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UsersIndex(int id, int pid)
        {
            _premisesbll = new PremisesBll();
            Premis premises = _premisesbll.GetPremisesbyId(pid);
            var yaohao = new PVS_NH_YaoHaoUsers
            {
                ActivitiesId = id,
                PremisesId = pid,
                PremisesName = premises.Name
            };
            return View(yaohao);
        }

        #region 报名用户列表数据搜索

        /// <summary>
        ///     列表数据
        ///     author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserSearch(PVS_NH_YaoHaoUsers search)
        {
            _premisesbll = new PremisesBll();
            Premis premises = _premisesbll.GetPremisesbyId(search.PremisesId);
            search.PremisesName = premises.Name;
            return View("UsersIndex", search);
        }

        /// <summary>
        ///     返回数据
        ///     author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult UserSearchResult(PVS_NH_YaoHaoUsers search, int pageIndex, int pageSize)
        {
            _yaohaobll = new YaohaoBll();
            pageIndex = pageIndex + 1;
            List<PVM_NH_YaoHaoUsers> list = _yaohaobll.GetYaoHaoApplyUsersPageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/Activities/_yaohaousers", list);
        }

        /// <summary>
        ///     结果数量
        ///     author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult UserSearchCount(PVS_NH_YaoHaoUsers search)
        {
            _yaohaobll = new YaohaoBll();
            int count = _yaohaobll.GetYaoHaoApplyUsersPageCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #region HELP

        #region page Content msg

        /// <summary>
        ///     page Content msg
        /// </summary>
        /// <param name="msg">提示语</param>
        /// <param name="url">返回地址url</param>
        /// <returns></returns>
        private string GetPageMsg(string msg, string url)
        {
            string html = string.Format(@"<script>
                                        alert('{0}！');
                                        window.location.href='{1}'
                                        </script>",
                msg, url);
            return html;
        }

        #endregion

        #region 返回当前页码

        /// <summary>
        ///     返回当前页码
        /// </summary>
        private void CurrentPageIndex()
        {
            int pageIndex;
            if (!int.TryParse(Convert.ToString(Request.Params["pageIndex"]), out pageIndex))
            {
                pageIndex = -1;
            }
            ViewData["CurrentPageIndex"] = pageIndex;
        }

        #endregion

        #endregion
    }
}