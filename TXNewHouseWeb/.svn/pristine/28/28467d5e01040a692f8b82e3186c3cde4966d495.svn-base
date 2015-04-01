using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TXBll.Dev;
using TXBll.WebSite;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminLogs;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [ActionFilters.AdminPageInfo(6, (int) AdminNavi.NewHouseMg.Developer.DeveloperAuth)]
    public class DevelopersController : BaseController
    {
        private DevelopersBll _developersBll;
        private GeographyBll _geographyBll;

        #region index

        /// <summary>
        /// 列表
        /// author:huhangfei
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PVS_NH_Developer search = new PVS_NH_Developer
            {
                ProvinceID = 0,
                CityId = 0,
                State = 3
            };
            _geographyBll = new GeographyBll();
            var listProvinces = _geographyBll.Z_GetProvinces().ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            search.Provinces.AddRange(listProvinces);
            CurrentPageIndex();
            return View(search);
        }

        #endregion

        #region 列表数据搜索

        /// <summary>
        /// 列表数据
        /// author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search(PVS_NH_Developer search)
        {
            _geographyBll = new GeographyBll();
            var listProvinces = _geographyBll.Z_GetProvinces().ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            search.Provinces.AddRange(listProvinces);
            if (search.ProvinceID != 0)
            {
                var lisetCities = _geographyBll.Z_GetCities(search.ProvinceID).ToList()
                    .ConvertAll(it => new SelectListItem
                    {
                        Text = it.GeographyName,
                        Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                    });
                search.Cities.AddRange(lisetCities);
            }
            search.Name = Server.UrlEncode(search.Name);
            search.BeginTime = Server.UrlEncode(search.BeginTime);
            search.EndTime = Server.UrlEncode(search.EndTime);
            CurrentPageIndex();
            return View("index", search);
        }

        /// <summary>
        /// 返回数据
        /// author:huhangfei
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_Developer search, int pageIndex, int pageSize)
        {
            _developersBll = new DevelopersBll();
            pageIndex = pageIndex + 1;
            var list = _developersBll.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            return PartialView("PageTables/Dev/_developers", list);
        }

        /// <summary>
        /// 结果数量
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_Developer search)
        {
            _developersBll = new DevelopersBll();
            int count = _developersBll.GetPageCount_BySearch_Admin(search);
            return Json(new {result = count});
        }

        #endregion

        #region 返回当前页码

        /// <summary>
        /// 返回当前页码
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

        #region 审核

        /// <summary>
        /// 审核
        /// author:huhangfei
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Handle(int id)
        {
            _developersBll = new DevelopersBll();
            var model = _developersBll.GetDevelopersById(id);
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().DevelopersIndex : Request.Params["backurl"];
            return View(model);
        }

        /// <summary>
        /// 状态处理
        /// author:huhangfei
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="refuse"></param>
        /// <param name="adminId"></param>
        /// <param name="adminName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Handle(int id, int state, string refuse, int adminId, string adminName)
        {
            try
            {
                _developersBll = new DevelopersBll();
                ESqlResult result = _developersBll.UpdateDevelopersStateById(id, state, refuse + " " + DateTime.Now.ToString("yyyy-MM-dd"), adminId, adminName);



                if (result.Code.Equals("1"))
                {
                    // 发送站内信
                    var developer = (new DevelopersBll().GetEntity_ById(id)) as Developer;
                    if (state == 1)
                    {
                        new DeveloperMessageBll().Add(new DeveloperMessage
                        {
                            SendUserId = 0,
                            ReceiveUserId = id,
                            Title = "通过身份审核",
                            Content = string.Format(@"尊敬的{0}，您的身份认证已经通过快有家的审核通过。", (null == developer ? string.Empty : developer.LoginName))
                        });
                    }
                    else
                    {
                        new DeveloperMessageBll().Add(new DeveloperMessage
                        {
                            SendUserId = 0,
                            ReceiveUserId = id,
                            Title = "未通过身份审核",
                            Content = string.Format(@"尊敬的{0}，您的身份认证未通过快有家的审核，请<a href='{1}'>点击</a>查看。", (null == developer ? string.Empty : developer.LoginName), Auxiliary.Instance.NhDevelopersUrl + "home/identification")
                        });
                    }

                    // 管理员操作日志
                    ExecuteOperResult(new Z_OperAdminLog
                    {
                        AdminID = adminId,
                        CreateTime = DateTime.Now,
                        OperIP = Request.UserHostAddress,
                        OperDes = string.Format("新房管理-开发商认证[{0}]-{1}", id, TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Developer.Fc_ById.For_State(state)),
                        Type = 0
                    });
                }

                return Json(new {flag = result.Code.Equals("1"), msg = result.Msg}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new {flag = false, msg = e.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}