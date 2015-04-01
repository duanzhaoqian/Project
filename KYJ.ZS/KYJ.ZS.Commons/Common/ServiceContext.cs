using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web;

namespace KYJ.ZS.Commons.Common
{
    public class ServiceContext
    {
        private static string _CookieName = PubConstant.COOKIE_NAME;//Auxiliary.ConfigKey("cookie_name");

        private static UserInfoType LoginUserType;

        public ServiceContext(UserInfoType loginUserType)
        {
            //指定登录KEY值类型
            LoginUserType = loginUserType;// Convert.ToString(loginUserType);
        }

        private string _Value;

        public string Value
        {
            get
            {
                if (_Value == null)
                {
                    var cookie = CookieHelper.GetCookie(_CookieName);//HttpContext.Current.Request.Cookies[_CookieName];
                    if (!string.IsNullOrWhiteSpace(cookie))
                    {
                        return cookie;
                    }
                    return null;
                }
                else
                    return _Value;
            }
        }

        public LoginUserInfo CurrentUser
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Value))
                {
                    //获取当前登录的GUID
                    var user_guid = this.Value;
                    //请求Redis获取当前登录用户信息
                    var userinfo = RedisTool.GerLoginUserInfo(user_guid, LoginUserType);
                    return userinfo;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Refresh()
        {
            CookieHelper.DelCookie(_CookieName);
        }

    }
}
