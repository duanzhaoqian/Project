using System;

namespace TXCommons
{
    public class KeyList
    {
        #region Cookies数据
        /// <summary>
        /// 开发商登陆CookieKey
        /// </summary>
        public static string CookieDeveloperLoginKey
        {
            get { return "KyjDeveloperLogin"; }
        }
        /// <summary>
        /// 注册发送消息数量key
        /// </summary>
        public static string CookieRegisterSendMsgKey
        {
            get { return "KyjRegisterSendMsgCount_"; }
        }
        /// <summary>
        /// 找回密码发送消息数量key
        /// </summary>
        public static string CookieForgetPwdSendMsgKey
        {
            get { return "KyjForgetPwdSendMsgCount_"; }
        }

        #endregion

        #region Redis数据
        /// <summary>
        /// 获得开发商登陆RedisKey
        /// </summary>
        /// <param name="id">用户ID</param>
        public static string GetRedisDeveloperLoginKey(string id)
        {
            return String.Concat("KyjDeveloperLogin_", id);
        }
        /// <summary>
        /// 获得手机验证码Key
        /// </summary>
        /// <param name="mobile">手机号</param>
        public static string GetRedisMobileValidateCodeKey(string mobile)
        {
            return String.Concat("KyjValidateCode_", mobile);
        }
        #endregion
    }
}
