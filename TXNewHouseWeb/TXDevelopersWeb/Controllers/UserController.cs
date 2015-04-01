using System;
using System.Web;
using System.Web.Mvc;
using TXBll.Dev;
using TXOrm;
using System.Linq;
using TXModel.Dev;
using TXCommons;
using ServiceStack;
using System.Collections.Generic;

namespace TXDevelopersWeb.Controllers
{
    /// <summary>
    /// 用户登录前控制类
    /// Author:台永海
    /// </summary>
    public class UserController : Controller
    {
        #region 登录
        /// <summary>
        /// 登录页
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string backUrl)
        {
            if (!String.IsNullOrWhiteSpace(backUrl))
            {
                ViewData["BackUrl"] = backUrl;
            }
            return View();
        }
        /// <summary>
        /// 提交登录
        /// Author:台永海
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public int AjaxDoLogin(string loginName, string pwd)
        {
            int result = 0;
            result = UserLogin(loginName, pwd);
            return result;
        }
        /// <summary>
        /// 用户登录方法
        /// Author:台永海
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns>用户Id</returns>
        private int UserLogin(string loginName, string pwd)
        {
            int result = 0;
            if (!String.IsNullOrWhiteSpace(loginName) && !String.IsNullOrWhiteSpace(pwd))
            {
                loginName = loginName.Trim().ToLower();//用户名不区分大小写
                pwd = pwd.Trim();//密码区分大小写
                DevelopersBll bll = new DevelopersBll();
                result = bll.Login(loginName, pwd);
                if (result > 0)
                {
                    try
                    {
                        var logindev = new DevelopersBll().GetEntity_ById(result) as Developer;
                        //登录时间
                        DateTime loginTime = DateTime.Now;
                        //实例化登录对象实体
                        CT_LoginUser entity = new CT_LoginUser();
                        entity.Id = result;
                        entity.LoginName = logindev.LoginName;
                        entity.Pwd = String.Empty;//为安全性考虑，密码不保存
                        entity.Timestamp = loginTime.ToBinary();//用于效验登录状态
                        entity.IsLogin = true;
                        //存入服务端Redis
                        Redis.SetValue<CT_LoginUser>(KeyList.GetRedisDeveloperLoginKey(entity.Id.ToString()), entity, loginTime.AddHours(12), FunctionTypeEnum.NewHouseUserInfo, 0);
                        //将对象实体序列化为JSON
                        string userJson = ToJSon.ObjectToJson(entity);
                        //将JSON加密
                        string userEncrypt = DESEncrypt.Encrypt(userJson);
                        //存入客户端Cookie
                        HttpCookie cookie = new HttpCookie(KeyList.CookieDeveloperLoginKey);
                        cookie.Domain = GetConfig.Domain;
                        cookie.Value = userEncrypt;
                        cookie.Expires = loginTime.AddHours(12);
                        Response.Cookies.Add(cookie);
                    }
                    catch (Exception e)
                    {
                        Log4netService.RecordLog.RecordException("台永海", String.Format("登陆错误，LoginName:{0},Pwd:{1}", loginName, pwd), e);
                        //throw;
                    }
                }
            }
            return result;
        }
        #endregion

        #region 退出
        /// <summary>
        /// 用户退出
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            //客户端用户
            CT_LoginUser clientServer;
            //取出用户Cookie
            HttpCookie cookie = Request.Cookies[KeyList.CookieDeveloperLoginKey];
            if (cookie != null)
            {
                //取出客户端加密串
                string userDecrypt = cookie.Value;
                //解密Cookie为JSON
                string userJson = DESEncrypt.Decrypt(userDecrypt);
                //反序列化JSON为对象
                clientServer = ToJSon.JsonToObject<CT_LoginUser>(userJson);
                //检查是否有客户端用户
                if (clientServer != null)
                {
                    //删除客户端用户
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                    Response.Cookies.Add(cookie);

                    // 兼容 chrome 浏览器
                    var cookie2 = new HttpCookie(KeyList.CookieDeveloperLoginKey);
                    cookie2.Domain = GetConfig.Domain;
                    cookie2.Expires = DateTime.Now.AddMonths(-1);
                    Response.Cookies.Add(cookie2);

                    //删除服务端用户
                    Redis.RemoveAllFromList(KeyList.GetRedisDeveloperLoginKey(clientServer.Id.ToString()), FunctionTypeEnum.NewHouseUserInfo, 0);
                }
            }
            return Redirect(String.Concat(GetConfig.BaseUrl, "user/login"));
        }
        #endregion

        #region 注册
        /// <summary>
        /// 注册页
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            //Redis.RemoveAllFromList("13911893577count_register", FunctionTypeEnum.NewHouseVerificationCode, 0);
            if (GetClientUser() != null)
                return RedirectToAction("index", "home");
            else
                return View();
        }
        /// <summary>
        /// 提交注册
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoRegister()
        {
            int result = 0;
            string loginName = Request.Form["txtLoginName"];
            string realName = String.Empty;
            string mobile = Request.Form["txtMobile"];
            string pwd = Request.Form["txtPwd"];
            string code = Request.Form["txtVerifyCode"];
            //再次验证验证码
            JsonResult flag = AjaxCheckValidate(mobile, code);
            if (flag.Data.ToBool())
            {
                //删除验证码
                Redis.RemoveAllFromList(KeyList.GetRedisMobileValidateCodeKey(mobile), FunctionTypeEnum.NewHouseVerificationCode, 0);
                //提交注册信息
                result = UserRegister(loginName, pwd, realName, mobile);
            }
            if (result > 0)
            {
                int userId = UserLogin(loginName, pwd);//注册成功后自动登录
                //发送站内信
                DeveloperMessageBll messBll = new DeveloperMessageBll();
                DeveloperMessage messEnt = new DeveloperMessage();
                messEnt.ReceiveUserId = userId;
                messEnt.Title = "注册成功";
                messEnt.Content = String.Format("尊敬的{0}，恭喜您已成功注册，请尽快进行身份认证，通过身份认证才可发布信息，希望您使用愉快！如有问题请致电快有家客户服务热线：400-999-3535。", loginName);
                messBll.Add(messEnt);
                //跳转成功
                return Redirect(String.Concat(GetConfig.BaseUrl, "user/registersuccess"));
            }
            else
                return Redirect(String.Concat(GetConfig.BaseUrl, "user/register"));
        }
        /// <summary>
        /// 用户注册
        /// Author:台永海
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pwd">密码</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        private int UserRegister(string loginName, string pwd, string realName, string mobile)
        {
            int result = 0;
            Developer entity = new Developer();
            entity.LoginName = loginName.Trim().ToLower();//用户名不区分大小写
            entity.RealName = realName;
            entity.Mobile = mobile;
            entity.Pwd = pwd;//密码区分大小写
            entity.InnerCode = Guid.NewGuid().ToString();
            DevelopersBll bll = new DevelopersBll();
            result = bll.Register(entity);
            return result;
        }
        /// <summary>
        /// 注册成功
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterSuccess()
        {
            return View();
        }

        /// <summary>
        /// 读取客户端用户信息
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        private CT_LoginUser GetClientUser()
        {
            //取出用户Cookie
            HttpCookie cookie = Request.Cookies[KeyList.CookieDeveloperLoginKey];
            if (cookie != null)
            {
                //取出客户端加密串
                string userDecrypt = cookie.Value;
                //解密Cookie为JSON
                string userJson = DESEncrypt.Decrypt(userDecrypt);
                //反序列化JSON为对象
                return ToJSon.JsonToObject<CT_LoginUser>(userJson);
            }
            else
                return null;
        }
        #endregion

        #region 忘记密码

        /// <summary>
        /// 忘记密码步骤一
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgetPassword1()
        {
            //删除验证码
            //Redis.RemoveAllFromList("13811818919count_repwd", FunctionTypeEnum.NewHouseVerificationCode, 0);
            return View();
        }
        /// <summary>
        /// 忘记密码步骤二
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgetPassword2(string k)
        {
            //Redis.RemoveAllFromList("13811818919count_repwd", FunctionTypeEnum.NewHouseVerificationCode, 0);
            ViewData["Mobile"] = k;
            return View();
        }
        /// <summary>
        /// 忘记密码步骤三修改密码
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public JsonResult DoForgetPassword(string code, string mobile, string newPwd)
        {
            bool result = false;
            //再次验证验证码
            JsonResult flag = AjaxCheckValidate(mobile, code);
            if (flag.Data.ToBool())
            {
                //删除验证码
                Redis.RemoveAllFromList(KeyList.GetRedisMobileValidateCodeKey(mobile), FunctionTypeEnum.NewHouseVerificationCode, 0);
                DevelopersBll bll = new DevelopersBll();
                if (bll.UpdatePassword(mobile, newPwd) > 0)
                    result = true;
            }
            return Json(result);
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 验证手机号并发送验证码
        /// Author:台永海
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public JsonResult AjaxCheckAndSendValidate(string mobile)
        {
            bool result = false;
            JsonResult flag = AjaxCheckMobile(mobile);
            if (flag.Data.ToBool())
            {
                result = AjaxSendValidate(mobile, 0).Data.ToBool();
            }
            return Json(result);
        }
        /// <summary>
        /// 发送验证码(0:开发商注册，1:找回密码)
        /// Author:台永海
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public JsonResult AjaxSendValidate(string mobile, int? t)
        {
            bool result = false;
            try
            {
                string code = PubConstant.CreateValidateNumber(6);
                SMSTool sms = new SMSTool();
                sms.sendSms(SMSOptionType.SEND_SMS, mobile, String.Format("您的验证码是：{0}", code));
                Redis.SetValue<string>(KeyList.GetRedisMobileValidateCodeKey(mobile), code, DateTime.Now.AddMinutes(30), FunctionTypeEnum.NewHouseVerificationCode, 0);
                SetValidateCount(mobile, t ?? 0);
                result = true;
            }
            catch (Exception)
            {
                //异常后发送失败
                throw;
            }
            return Json(result);
        }
        /// <summary>
        /// 验证验证码是否正确
        /// Author:台永海
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult AjaxCheckValidate(string mobile, string code)
        {
            string trueCode = Redis.GetValue<string>(KeyList.GetRedisMobileValidateCodeKey(mobile), FunctionTypeEnum.NewHouseVerificationCode, 0);
            //配合前台JQuery.Validate.js使用，返回值必须为JsonResult格式
            if (trueCode == code)
                return Json(true);
            else
                return Json(false);
        }
        /// <summary>
        /// 验证手机号是否重复
        /// Author:台永海
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public JsonResult AjaxCheckMobile(string mobile)
        {
            DevelopersBll bll = new DevelopersBll();
            //配合前台JQuery.Validate.js使用，返回值必须为JsonResult格式
            return Json(bll.CheckMobile(mobile));
        }
        /// <summary>
        /// 验证登录名是否重复
        /// Author:台永海
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public JsonResult AjaxCheckLoginName(string loginName)
        {
            DevelopersBll bll = new DevelopersBll();
            //配合前台JQuery.Validate.js使用，返回值必须为JsonResult格式
            return Json(bll.CheckLoginName(loginName));
        }

        /// <summary>
        /// 发送次数是否合法
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public JsonResult AjaxCheckRePwdValidateCount(string mobile, int? t)
        {
            if (GetValidateCount(mobile, t ?? 0) < 3)
                return Json(true);
            else
                return Json(false);
        }

        /// <summary>
        /// 存储找回密码发送验证码次数
        /// Author:sunlin
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private void SetValidateCount(string mobile, int t)
        {
            var key = mobile.Trim() + "count_repwd";
            if (t != 1)
                key = mobile.Trim() + "count_register";
            var count = GetValidateCount(mobile, t);
            if (count > 0)
            {
                Redis.RemoveAllFromList(key, FunctionTypeEnum.NewHouseVerificationCode, 0);
            }
            count++;
            Redis.AddItemToList<string>(count.ToString(), key, DateTime.Now.Date.AddDays(1), FunctionTypeEnum.NewHouseVerificationCode, 0); //存储次数        
        }

        /// <summary>
        /// 获取找回密码一天内发送验证码的次数
        /// </summary>
        /// Author:sunlin
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        private static int GetValidateCount(string mobile, int t)
        {
            var key = mobile.Trim() + "count_repwd";
            if (t != 1)
                key = mobile.Trim() + "count_register";
            var counts = Redis.GetAllItemsFromList<List<string>>(key, FunctionTypeEnum.NewHouseVerificationCode, 0);
            if (counts == null || counts.Count == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(counts.Last());
            }
        }

        #endregion
    }
}
