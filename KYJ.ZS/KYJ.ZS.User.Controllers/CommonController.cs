using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Models.LocalUsers;

namespace KYJ.ZS.User.Controllers
{
    /// <summary>
    /// Auther:zhuzh
    /// Time:2014-4-17
    /// Desc:未登录用户访问Controller，后台一般用于登录注册页
    /// </summary>
    public class CommonController : Controller
    {
        /// <summary>
        /// 登录页
        /// </summary>
        /// <param name="returnUrl">登录后跳转页</param>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            //服务上下文
            var _Context = new ServiceContext(UserInfoType.USERLOGIN);
            //判断是否登录，登录自动跳转到相应的页面
            if (_Context.CurrentUser != null)
            {
                return Redirect(string.IsNullOrEmpty(returnUrl) ? Url.UserSiteUrl().Index : returnUrl);
            }
            //登录计数
            var login_num = CookieTool.GetCookie(PubConstant.NUMBER_COOKIE);
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginNumber = string.IsNullOrEmpty(login_num) ? 0 : Auxiliary.ToInt32(login_num);
            return View();
        }

        [HttpPost]
        [KYJ.ZS.User.Controllers.ActionFilters.AjaxOnly(NoCache = true)]
        public JsonResult Login(string loginname, string password, string code, string returnUrl, bool? isautologin)
        {
            var _code = new VerificationCode();
            var type = UserLoginType.NORMAL;
            var number_cookie = PubConstant.NUMBER_COOKIE;//Auxiliary.ConfigKey("code_cookie");
            var login_num = CookieTool.GetCookie(number_cookie);
            var int_num = Auxiliary.ToInt32(login_num);
            if ((int_num > 0 && !string.IsNullOrEmpty(code) && _code.CheckCode(code)) || (int_num <= 0 && string.IsNullOrEmpty(code)))
            {
                var _bll = new KYJ.ZS.BLL.LocalUsers.LocalUserBll();
                //登录操作
                var num = _bll.Login(loginname, password, isautologin.HasValue ? isautologin.Value : false);
                if (num == 2)
                {
                    return Json(new { result = false, type = 3, message = "用户已冻结，请联系管理员！" });
                }
                //记录登录次数CookieName
                if (num > 0)
                {
                    CookieTool.RemoveCookie(number_cookie);
                    if (!returnUrl.IsNullOrEmpty())
                        return Json(new { result = true, type = type, returnurl = returnUrl });
                    return Json(new { result = true, type = type, returnurl = Url.WebSiteUrl().Index });
                }
                else
                {
                    if (string.IsNullOrEmpty(login_num))
                    {
                        //首次登录
                        type = UserLoginType.ERRORFIRSTLOGIN;
                        CookieTool.SetCookie("1", number_cookie, DateTime.Now.AddHours(1));
                    }
                    else
                    {
                        var limit_login = Auxiliary.ToInt32(PubConstant.LIMIT_LOGIN);
                        if (int_num >= limit_login)
                        {
                            //登录错误限制
                            type = UserLoginType.ERRORLIMITLOGIN;
                        }
                        else
                        {
                            //本次登录错误 未限制
                            type = UserLoginType.ERROROTHERLOGIN;
                            CookieTool.SetCookie((int_num + 1).ToString(), number_cookie, DateTime.Now.AddHours(1));
                        }
                    }
                    return Json(new { result = false, type = type, message = "用户名或密码不正确！" });
                }
            }
            else
            {
                return Json(new { result = false, type = UserLoginType.ERRORCODE, message = "用户名或密码不正确！" });
            }
        }

        /// <summary>
        /// 注册页
        /// </summary>
        [HttpGet]
        public ActionResult Register()
        {
            var _bll = new KYJ.ZS.BLL.LocalUsers.LocalUserBll();
            var cookie_guid = CookieTool.GetCookie(PubConstant.COMMON_COOKIE);
            if (!string.IsNullOrEmpty(cookie_guid))
            {
                var key = KeyManager.GetUserCommonKey(cookie_guid);
                var s = RedisTool.GetValue<int>(key, FunctionType.ZuShouCommon, 0);
                ViewBag.Common = s;
            }
            return View();
        }
        /// <summary>
        /// 提交注册信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SubRegister(RegisterUserEntity entity)
        {
            var key = KeyManager.GetVerificationCodeKey(entity.LoginName);
            var code = RedisTool.GetAllItemsFromList<List<string>>(key, FunctionType.ZuShouVerificationCode, 0);
            if (code != null && code.Count > 0)
            {
                if (code.First() != entity.VerificationCode)
                {
                    return Json(new { result = false, message = "请填写正确信息！" });
                }
            }
            if (ModelState.IsValid)
            {
                KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
                var num = _bll.Register(entity);
                if (num == RegisterType.SUCCESS || num == RegisterType.PASS)
                    return Json(new { result = true, message = "注册成功" });
                else
                    return Json(new { result = false, message = "注册失败！" });
            }
            return Json(new { result = false, message = "请填写正确信息！" });
        }

        /// <summary>
        /// 找回密码页面地址
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPWD()
        {
            var _bll = new KYJ.ZS.BLL.LocalUsers.LocalUserBll();
            var cookie_guid = CookieTool.GetCookie(PubConstant.COMMON_COOKIE);
            if (!string.IsNullOrEmpty(cookie_guid))
            {
                var key = KeyManager.GetUserCommonKey(cookie_guid);
                var s = RedisTool.GetValue<int>(key, FunctionType.ZuShouCommon, 0);
                ViewBag.Common = s;
            }
            return View();
        }
        /// <summary>
        /// 提交注册信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResetPWD(RegisterUserEntity entity)
        {
            var key = KeyManager.GetVerificationCodeKey(entity.LoginName);
            var code = RedisTool.GetAllItemsFromList<List<string>>(key, FunctionType.ZuShouVerificationCode, 0);
            if (code != null && code.Count > 0)
            {
                if (code.First() != entity.VerificationCode)
                {
                    return Json(new { result = false, message = "请填写正确信息！" });
                }
            }
            if (ModelState.IsValid)
            {
                KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();

                var num = _bll.UpdatePwd(entity.LoginName, entity.Password);

                if (num > 0)
                {
                    //重置登录状态
                    return Json(new { result = true, message = "密码找回成功！" });
                }
                else
                    return Json(new { result = false, message = "找回失败,您的帐号可能未在本平台使用过！" });
            }
            else
            {
                return Json(new { result = false, message = "请填写正确信息！" });
            }
        }
        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MobileCode(string mobile)
        {
            if (!string.IsNullOrEmpty(mobile) && Auxiliary.KYJRegex(@"^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(14[0-9]{1}))+\d{8})$", mobile.Trim()))
            {
                //判断用户名是否存在
                KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
                if (!_bll.CheckMobile(mobile))
                {
                    //type="1" 是在页面上判断是alert() 还是 在控件后追加提示
                    return Json(new { result = false, message = "该会员名已经被注册！", type = "1" });
                }
                //防止连续获取
                var codes = RedisTool.GetAllItemsFromList<List<string>>(mobile, FunctionType.ZuShouVerificationCode, 0);
                //获取指定手机号码当天发送验证码数量
                int count = VerificationCode.GetMobileVerificationCodeCount(mobile);
                //发送短信大于等于三次，停止发送验证码
                if (count >= 3)
                    return Json(new { result = false, message = "一天内只能获取三次验证码，你已经违规。" });
                var flag = VerificationCode.SendMobileVerificationCode(mobile);
                //是否发送成功
                if (flag)
                {
                    //修改指定手机号码当天发送短信数量
                    // VerificationCode.ChangeMobileVerificationCodeCount(mobile.Trim());
                    return Json(new { result = true, message = "验证码已发送，请注意查收。" });
                }
                else
                {
                    return Json(new { result = false, message = "验证码已发送失败，请从新获取。" });
                }
            }
            else
            {
                //type="1" 是在页面上判断是alert() 还是 在控件后追加提示
                return Json(new { result = false, message = "请输入正确的手机号码！", type = "1" });
            }
        }

        /// <summary>
        /// 记录发送短信时间
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RecordMobile(int second)
        {
            //获取当前辅助COOKIE
            var cookie_guid = CookieTool.GetCookie(PubConstant.COMMON_COOKIE);
            //判断辅助COOKIE是否纯在，不纯在重新生成COOKIE
            if (string.IsNullOrWhiteSpace(cookie_guid))
            {
                cookie_guid = Guid.NewGuid().ToString();
                CookieTool.SetCookie(cookie_guid, PubConstant.COMMON_COOKIE, DateTime.Now.AddSeconds(180));
            }
            //用户辅助RedisKey
            var key = KeyManager.GetUserCommonKey(cookie_guid);
            //存储
            RedisTool.SetValue<int>(key, second, DateTime.Now.AddSeconds(second), FunctionType.ZuShouVerificationCode, 0);

            return Json("");
        }

        /// <summary>
        /// 检查用户名是否注册
        /// </summary>
        /// <param name="mobile">手机号码</param>
        [HttpPost]
        public JsonResult CheckMobile(string mobile)
        {
            if (!string.IsNullOrEmpty(mobile) && Auxiliary.KYJRegex(@"^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(14[0-9]{1}))+\d{8})$", mobile.Trim()))
            {
                KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
                if (_bll.CheckMobile(mobile))
                    return Json(true, JsonRequestBehavior.AllowGet);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 检查用户输入验证码是否正确
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="inputCode">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckCode(string mobile, string inputCode)
        {
            var key = KeyManager.GetVerificationCodeKey(mobile);
            var code = RedisTool.GetAllItemsFromList<List<string>>(key, FunctionType.ZuShouVerificationCode, 0);
            if (code != null && code.Count > 0)
            {
                if (code.First() != inputCode)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
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

        #region 辅助
        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Code()
        {
            VerificationCode verificationcode = new VerificationCode();
            Response.Clear();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(verificationcode.CreateCodeImage());
            Response.End();
            return new EmptyResult();
        }

        #endregion

        #region 填充数据

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-09
        /// Desc:初始化RegisterUserEntity数据
        /// </summary>
        /// <param name="entity">RegisterUserEntity数据实体</param>
        public void InitializeRegisterUserEntity(RegisterUserEntity entity)
        {
            #region 基础数据

            entity.LoginName = string.IsNullOrEmpty(Request.Form["loginname"]) ? string.Empty : Request.Form["loginname"].ToString();
            entity.VerificationCode = string.IsNullOrEmpty(Request.Form["code"]) ? string.Empty : Request.Form["code"].ToString();
            entity.Password = string.IsNullOrEmpty(Request.Form["password"]) ? string.Empty : Request.Form["password"].ToString();
            entity.RePassword = string.IsNullOrEmpty(Request.Form["repassword"]) ? string.Empty : Request.Form["repassword"].ToString();

            #endregion

        }

        #endregion
    }
}
