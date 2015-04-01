using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TXBll.Dev;
using TXBll.HouseData;
using TXBll.WebSite;
using TXCommons.Admins;
using TXCommons.PictureModular;
using TXManagerWeb.Common;
using TXManagerWeb.Utility;
using TXModel.AdminPVM;

namespace TXManagerWeb.Controllers
{
    [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Developer.DeveloperAccount)]
    public class DevelopersAccountController : BaseController
    {
        private DevelopersBll _developersBll;
        private GeographyBll _geographyBll;

        #region 账号管理列表

        /// <summary>
        /// 账号管理列表
        /// author:wangzhikun
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var search = new PVS_NH_Developer
            {
                ProvinceID = 0,
                CityId = 0,
                LockState = -1,
                State = -1
            };
            _geographyBll = new GeographyBll();
            var listProvinces = _geographyBll.Z_GetProvinces().ToList()
                .ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
            search.Provinces.AddRange(listProvinces);
            search.LockStateList = AdminTypes.Db_NewHouse.Tb_Developer.Fc_SelectListItems.Get_LockState();
            CurrentPageIndex();
            return View(search);
        }

        #endregion

        #region 列表数据搜索

        /// <summary>
        /// 列表数据
        /// author:wangzhikun
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
            search.LockStateList = AdminTypes.Db_NewHouse.Tb_Developer.Fc_SelectListItems.Get_LockState();
            search.LoginName = Server.UrlEncode(search.LoginName);
            search.BeginTime = Server.UrlEncode(search.BeginTime);
            search.EndTime = Server.UrlEncode(search.EndTime);
            search.Name = Server.UrlEncode(search.Name);
            CurrentPageIndex();
            return View("index", search);
        }

        /// <summary>
        /// 返回数据
        /// author:wangzhikun
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
            return PartialView("PageTables/Dev/_developersAccount", list);
        }

        /// <summary>
        /// 结果数量
        /// author:wangzhikun
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public ActionResult SearchCount(PVS_NH_Developer search)
        {
            _developersBll = new DevelopersBll();
            int count = _developersBll.GetPageCount_BySearch_Admin(search);
            return Json(new { result = count });
        }

        #endregion

        #region 修改开发商资料

        #region 基本资料

        /// <summary>
        /// 基本资料
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ActionResult Handle(int id)
        {
            _developersBll = new DevelopersBll();
            _geographyBll = new GeographyBll();
            var pvsModel = new PVS_NH_Developer();
            var model = _developersBll.GetDevelopersById(id);

            var list = Auxiliary.Instance.GetProvinces().ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });
            pvsModel.ProvinceID = model.NHDeveloper.ProvinceId;
            pvsModel.CityId = model.NHDeveloper.CityId;

            model.ProvinceId = model.NHDeveloper.ProvinceId;
            model.CityId = model.NHDeveloper.CityId;

            //加载省份数据
            if (list.Count > 0)
            {
                pvsModel.Provinces.AddRange(list);
            }

            // 加载城市数据
            if (model.NHDeveloper.ProvinceId > 0)
            {
                var lisetCities = _geographyBll.Z_GetCities(model.NHDeveloper.ProvinceId).ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });
                pvsModel.Cities.AddRange(lisetCities);
            }

            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().DevelopersIndex : Request.Params["backurl"];
            ViewData["PVS_NH_Developer"] = pvsModel;

            return View(model);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitHandle(PVM_NH_Developer model)
        {
            try
            {
                var developAccount = new Developer_AccountBll();
                developAccount.Update(model, 1);
                return Content("<script type=\"text/javascript\">alert('保存成功!');window.location.href='" + Url.SiteUrl().DevelopersAccount_Handle(model.Id) + "';</script>");
            }
            catch (Exception)
            {
                return Content("<script type=\"text/javascript\">alert('保存失败!');window.location.href='" + Url.SiteUrl().DevelopersAccount_Handle(model.Id) + "';</script>");
            }

        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获得用户图片集合存入ViewData
        /// Author:台永海
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="picType">图片类型：1头像，2身份认证</param>
        /// <returns></returns>
        public void GetUserPicListToViewData(string guid, int picType)
        {
            //图片参数
            string GUID = guid;
            string pictureType = String.Empty;
            switch (picType)
            {
                case 1:
                    pictureType = TXCommons.PictureModular.UserPictureType.LOGO.ToString();
                    break;
                case 2:
                    pictureType = TXCommons.PictureModular.UserPictureType.Identification.ToString();
                    break;
                default:
                    break;
            }

            int cityId = 0;
            //得到图片集合
            TXCommons.PictureModular.UserPictureInfo picList = null;
            //picList = TXCommons.PictureModular.GetPicture.GetUserPictureInfo(GUID, true, pictureType);
            picList = TXCommons.PictureModular.GetPicture.GetUserPictureInfo(GUID, true, pictureType);
            //存入视图
            ViewData["GUID"] = GUID;
            ViewData["pictureType"] = pictureType;
            ViewData["cityId"] = cityId;
            ViewData["picList"] = picList;
        }

        /// <summary>
        /// 获得用户文件存入ViewData
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="docType">文件类型：0合同，1附件</param>
        public void GetDocumentInfoListToViewData(string guid, int docType)
        {
            //文件参数
            string GUID = guid;
            string documentType = String.Empty;
            switch (docType)
            {
                case 0:
                    documentType = TXCommons.PictureModular.DocumentType.PACT.ToString();
                    break;
                case 1:
                    documentType = TXCommons.PictureModular.DocumentType.ANNEX.ToString();
                    break;
                default:
                    break;
            }

            int cityId = 0;
            //得到文件集合
            TXCommons.PictureModular.DocumentInfo docList = null;
            docList = TXCommons.PictureModular.GetPicture.GetDocumentInfo(GUID, true, documentType);
            //存入视图
            ViewData["GUID"] = GUID;
            ViewData["documentType"] = documentType;
            ViewData["cityId"] = cityId;
            ViewData["docList"] = docList;
        }

        #endregion

        #region 头像处理

        /// <summary>
        /// 头像处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ActionResult HeadPictureHandle(int id)
        {
            _developersBll = new DevelopersBll();
            var model = _developersBll.GetDevelopersById(id);
            GetUserPicListToViewData(model.InnerCode, 1); //设置数据
            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Auxiliary.Instance.NhManagerUrl + "developersaccount/headpicturehandle.html?id=" + id : Request.Params["backurl"];
            return View("HeadPicture", model);
        }

        /// <summary>
        /// 修改头像高级
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePhotoExpert(int id)
        {
            _developersBll = new DevelopersBll();
            var model = _developersBll.GetDevelopersById(id);
            GetUserPicListToViewData(model.InnerCode, 1);
            return View(model);
        }

        #endregion

        #region 密码初始化处理

        /// <summary>
        /// 密码初始化处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ActionResult PasswordHandle(int id)
        {
            ViewData["Id"] = id.ToString(CultureInfo.InvariantCulture);
            return View("Password");
        }

        /// <summary>
        /// 密码初始化
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ActionResult GetNewPwd(int id)
        {
            var developAccount = new Developer_AccountBll();
            var model = new PVM_NH_Developer();
            model.Id = id;
            model.Pwd = GetRandomNum(); //密码初始化

            bool flag = developAccount.Update(model, 3); //密码修改

            //密码初始化后发送给开发商
            _developersBll = new DevelopersBll();
            var sms = new TXCommons.SMSTool();
            var developer = developAccount.GetDevelopersById(id);
            if (flag && developer != null)
            {
                sms.sendSms(TXCommons.SMSOptionType.SEND_SMS, developer.Mobile,
                    "尊敬的" + developer.LoginName + ",您的快有家平台-新房管理后台账户密码:" + developer.Pwd +
                    " ,如有问题请致电快有家客户服务热线 :" + Auxiliary.Instance.NhServiceHotLine1 + "。");
            }
            else
            {
                flag = false;
            }
            return Json(new { result = flag });
        }

        /// <summary>
        /// 获取随机字符
        /// </summary>
        /// <returns></returns>
        public string GetRandomNum()
        {
            char[] constant = { '1', '2', '3', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }; //, 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char t;
            var newRandom = new System.Text.StringBuilder(constant.Length);
            var rd = new Random(Guid.NewGuid().GetHashCode()); //保证生成的随机字符不重复 

            for (int i = 0; i < 8; i++)
            {
                t = constant[rd.Next(constant.Length)];
                if (newRandom.ToString().Contains(t))
                {
                    i--;
                }
                else
                {
                    newRandom.Append(t);
                }
            }
            return newRandom.ToString();
        }

        #endregion

        #region 身份认证处理

        /// <summary>
        /// 身份认证处理
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public ActionResult AuthenticationHandle(int id)
        {
            _developersBll = new DevelopersBll();
            _geographyBll = new GeographyBll();
            var pvsModel = new PVS_NH_Developer();
            var model = _developersBll.GetDevelopersById(id);
            var list = Auxiliary.Instance.GetProvinces().ConvertAll(it => new SelectListItem { Text = it.Text, Value = it.Value });
            pvsModel.ProvinceID = model.NHDeveloperIdentity.ProvinceId;
            pvsModel.CityId = model.NHDeveloperIdentity.CityId;
            pvsModel.DistrctId = model.NHDeveloperIdentity.CityId;

            model.ProvinceId = model.NHDeveloperIdentity.ProvinceId;
            model.CityId = model.NHDeveloperIdentity.CityId;
            model.DistrictId = model.NHDeveloperIdentity.DId;

            //加载省份数据
            if (list.Count > 0)
            {
                pvsModel.Provinces.AddRange(list);
            }

            if (pvsModel.ProvinceID > 0)
            {
                var listCities = _geographyBll.Z_GetCities(pvsModel.ProvinceID).ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });
                pvsModel.Cities.AddRange(listCities);
            }

            if (pvsModel.CityId > 0)
            {
                var listDistricts = _geographyBll.Z_GetDistricts(pvsModel.CityId).ConvertAll(it => new SelectListItem { Text = it.GeographyName, Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture) });
                pvsModel.Districts.AddRange(listDistricts);
            }

            ViewData["PVS_NH_Developer"] = pvsModel;
            pvsModel.CompanyTypeList = AdminTypes.Db_NewHouse.Tb_Developer.Fc_SelectListItems.Get_CompanyType();

            ViewData["backurl"] = string.IsNullOrEmpty(Convert.ToString(Request.Params["backurl"])) ? Url.SiteUrl().DevelopersIndex : Request.Params["backurl"];
            return View("IdentityAuthentication", model);
        }

        /// <summary>
        /// 提交认证数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SubmitAuthentication(PVM_NH_Developer model)
        {
            try
            {
                var developAccount = new Developer_AccountBll();
                var b = developAccount.UpdateIdentity(model);
                if (b)
                    return Content("<script type=\"text/javascript\">alert('保存成功!');window.location.href='" + Url.SiteUrl().DevelopersAccount_Handle(model.Id) + "';</script>");
                return Content("<script type=\"text/javascript\">alert('保存失败!');window.location.href='" + Url.SiteUrl().DevelopersAccount_Handle(model.Id) + "';</script>");
            }
            catch (Exception)
            {
                return Content("<script type=\"text/javascript\">alert('保存失败!');window.location.href='" + Url.SiteUrl().DevelopersAccount_Handle(model.Id) + "';</script>");
            }

        }

        #endregion

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadAuthenticationPhoto(string guid, string picturetype, int cityId)
        {
            GetUserPicListToViewData(guid, 2);
            List<TXCommons.PictureModular.UserPictureInfo> list = new List<TXCommons.PictureModular.UserPictureInfo>();
            list = TXCommons.PictureModular.GetPicture.GetUserPictureInfos(guid, true, picturetype);
            ViewData["list"] = list != null && list.Count > 0 ? list[0] : null;
            return View("PageTables/Dev/_UploadAuthenticationPhoto");
        }

        /// <summary>
        /// 文件上传(合同)
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="filetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ActionResult UploadAuthenticationFile(string guid, string filetype, int cityId)
        {
            GetDocumentInfoListToViewData(guid, 0);
            var file = new TXCommons.PictureModular.DocumentInfo();
            file = TXCommons.PictureModular.GetPicture.GetDocumentInfo(guid, true, filetype);
            // ViewData["list"] = list != null && list.Count > 0 ? list[0] : null;
            ViewData["file"] = file;
            return View("PageTables/Dev/_UploadAuthenticationHeTong");
        }

        /// <summary>
        /// 是否上传认证图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public JsonResult GetAuthenticationPhotoCount(string guid, string picturetype, int cityId)
        {
            GetUserPicListToViewData(guid, 2);
            if (null == (ViewData["picList"] as UserPictureInfo)
                || 0 == (ViewData["picList"] as UserPictureInfo).ID)
            {
                return Json(new { suc = false });
            }

            return Json(new { suc = true });
        }

        /// <summary>
        /// 是否上传认证文件
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="filetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public JsonResult GetAuthenticationFileCount(string guid, string filetype, int cityId)
        {
            GetDocumentInfoListToViewData(guid, 0);
            if (null == (ViewData["docList"] as DocumentInfo)
                || 0 == (ViewData["docList"] as DocumentInfo).ID)
            {
                return Json(new { suc = false });
            }

            return Json(new { suc = true });
        }

        #endregion

        #region 账号锁定/解除

        /// <summary>
        /// 账号锁定/解除
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Locked(int? id, int? state)
        {
            if (id.HasValue && state.HasValue)
            {
                try
                {
                    var developAccount = new Developer_AccountBll();

                    developAccount.Locked(id.Value, state.Value);

                    return Json(new { result = true, message = "" });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = "操作失败：" + ex.Message });
                }

            }
            return Json(new { result = false, message = "账号状态设置失败。" });
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

        #region 账号推荐/取消推荐

        /// <summary>
        /// 账号锁定/解除
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="restate">状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Recommend(int? id, int? restate)
        {
            if (id.HasValue && restate.HasValue)
            {
                try
                {
                    var developAccount = new Developer_AccountBll();
                    developAccount.Recommend(id.Value, restate.Value);
                    var premises = new PremisesBll().GetPremisByDeveloperId(id.Value);
                    if (null != premises && 0 < premises.Count)
                    {
                        foreach (var p in premises)
                        {
                            TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", p.Id, p.CityId);
                        }
                        return Json(new { result = true, message = "" });
                    }
                    return Json(new { result = true, message = "" });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = "操作失败：" + ex.Message });
                }

            }
            return Json(new { result = false, message = "账号推荐设置失败。" });
        }

        #endregion
    }
}