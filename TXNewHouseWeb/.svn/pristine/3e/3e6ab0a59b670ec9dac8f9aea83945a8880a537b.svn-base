using System;
using System.Web;
using System.Web.Mvc;
using TXCommons.user.cjkjb.webservice;
using System.Text;
using TXCommons;
using TXCommons.Xml;
using ServiceStack;

namespace TXNewHouseWeb.Controllers
{
    public class LoginController : BaseController
    {
        #region 登陆相关

        /// <summary>
        /// Ajax判断登陆
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxIsLogin()
        {
            if (this.IsLogin)
            {
                UserInfo user = this.GetUserInfo;
                if (user != null)
                {
                    return Json(new {IsLogin = IsLogin, lname = user.LoginName, utype = user.FcUserType, rname = user.RealName, isvip = user.IsVip, vipdate = string.Format("{0:yyyy-MM-dd}", user.VipEndTime)});
                }
                else
                {
                    return Json(new { IsLogin = false, lname = "", utype = "", rname = "", isvip = 0, vipdate = "" });
                }
            }
            else
            {
                return Json(new { IsLogin = IsLogin, lname = "", utype = "", rname = "", no = "" });
            }
        }

        /// <summary>
        /// 退出
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            TXCommons.CookieHelper.DelCookie(TXCommons.GetConfig.PrivateCookie);
            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// 登陆
        /// author：sunlin
        /// </summary>
        public ActionResult login()
        {
            #region 清除已登录用户信息
            try
            {
                HttpCookie privateAuthCookie = Request.Cookies[GetConfig.PrivateCookie];

                if (privateAuthCookie != null)
                {
                    CookieHelper.DelCookie(GetConfig.PrivateCookie);
                }
            }
            catch
            {

            }
            #endregion

            var username = Request["username"];
            var password = Request["password"];
            var userXml = "";
            userXml = _ous.Login("binPath", "classPath", username, password, "FC");
            if (string.IsNullOrEmpty(userXml)) { return Json(new { Message = "该用户不存在！", IsLogin = false }); }
            var user = ComObjOrXml.Deserialize(typeof(UserInfo), userXml) as UserInfo;
            if (user != null)
            {
                #region 记录用户登陆信息
                StringBuilder userDate = new StringBuilder();
                userDate.Append(user.Id);
                userDate.Append(";");
                userDate.Append(user.Guid);
                userDate.Append(";");
                userDate.Append("FC");
                userDate.Append(";");
                userDate.Append(user.LoginName);
                TXCommons.CookieHelper.SetCookie(GetConfig.PrivateCookie, userDate.ToString());
                #endregion

                Redis.AddItemToList<string>(userXml, user.Guid, DateTime.Now.AddMinutes(60), FunctionTypeEnum.UserInfo, 0); //二手房缓存用户信息
                ViewData["url"]= Redirect(Request.UrlReferrer.ToString());
                return Json(new { Message = "登录成功", IsLogin = true });
            }
            else
            {
                ViewData["url"] = Redirect(Request.UrlReferrer.ToString());
                return Json(new { Message = "用户名或密码错误", IsLogin = false });
            }
        }

        #endregion

    }
}
