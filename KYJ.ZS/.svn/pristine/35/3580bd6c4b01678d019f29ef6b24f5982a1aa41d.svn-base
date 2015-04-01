using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.Merchants;
using KYJ.ZS.BLL.Orders;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Merchant.Controllers.ActionFilters;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.Merchant.Controllers
{

    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月24日
    /// 描述：未登录用户访问Controller，后台一般用于登录注册页
    /// </summary>
    public class CommonController : Controller
    {
        MerchantBll bll = new MerchantBll();
        VerificationCode verificationcode = new VerificationCode();
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-04-28
        /// Desc:商户后台首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 签署协议
        /// </summary>
        /// <returns></returns>
        public ActionResult SignAgreement()
        {
            return View();
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-04-28
        /// Desc:用户登录视图
        /// </summary>
        /// <param name="returnUrl">登录后跳转页面URL</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (IsLogin())
            {
                return new RedirectResult(Url.MerchantSiteUrl().Order_Main());
            }
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ValidateCode = verificationcode.GetCode();
            return View();
        }
        /// <summary>
        /// 检测用户是否登录
        /// </summary>
        /// <returns></returns>
        private bool IsLogin()
        {
            bool result = false;
            string cookieName = PubConstant.COOKIE_NAME;
            string guid = CookieTool.GetCookie(cookieName);
            if (!string.IsNullOrWhiteSpace(guid))
            {
                LoginUserInfo loginUser = RedisTool.GerLoginUserInfo(guid, UserInfoType.MERCHANTLOGIN);
                if (loginUser != null)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Login(string loginName, string loginPwd, string loginCode, string returnUrl)
        {
            BackMessge bm = new BackMessge();
            string message;
            if (!string.IsNullOrEmpty(loginName) && loginName.Length > 1)//校验登录名
            {
                if (!string.IsNullOrEmpty(loginPwd) && loginPwd.Length >= 6)//校验密码
                {
                    if (loginCode != null || loginCode.Length == 4)//校验验证码是否为空或者null
                    {
                        if (verificationcode.CheckCode(loginCode, true)) //校验验证码是否正确
                        {
                            bm.Success = bll.Login(loginName, loginPwd, out message);
                            bm.Message = message;
                            if (bm.Success)//登录成功！
                            {
                                bm.StrUrl = Url.MerchantSiteUrl().Order_Main();
                                if (!string.IsNullOrEmpty(returnUrl))
                                {
                                    bm.StrUrl = returnUrl;
                                }
                                bm.Action = BackAction.Redirect;
                            }
                        }
                        else
                        {
                            bm.Message = "请正确填写验证码！";
                        }
                    }
                    else
                    {
                        bm.Message = "请正确填写验证码！";
                    }
                }
                else
                {
                    bm.Message = "密码最少为6位！";
                }
            }
            else
            {
                bm.Message = "请输入正确的登录名！";
            }
            return Json(bm);
        }
        #region 辅助
        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Code()
        {
            Response.Clear();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(verificationcode.CreateCodeImage());
            Response.End();
            return new EmptyResult();
        }
        #endregion


        #region Geography地理位置

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        public JsonResult Provinces()
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var provinces = _geographyBll.GetProvinces().ToArray();
                return Json(new { success = true, items = provinces }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取城市
        /// <param name="geographyCode">省份ID</param>
        /// </summary>
        public JsonResult Cities(int geographyCode)
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var cities = _geographyBll.GetCities(geographyCode).ToArray();
                return Json(new { success = true, items = cities }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 页面获取区域
        /// </summary>
        /// <param name="geographyCode">城市ID</param>
        public JsonResult Districts(int geographyCode)
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var channels = _geographyBll.GetDistricts(geographyCode).ToArray();
                return Json(new { success = true, items = channels }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 页面获取商圈
        /// </summary>
        /// <param name="geographyCode">区域ID</param>
        public JsonResult Businesses(int geographyCode)
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var channels = _geographyBll.GetBussineses(geographyCode).ToArray();
                return Json(new { success = true, items = channels }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
