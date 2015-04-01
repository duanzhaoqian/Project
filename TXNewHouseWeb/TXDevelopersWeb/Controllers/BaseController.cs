using System;
using System.Web;
using System.Web.Mvc;
using TXModel.Dev;
using TXCommons;

namespace TXDevelopersWeb.Controllers
{
    /// <summary>
    /// 新房后台控制器基类
    /// Author:台永海
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户私有实体
        /// Author:台永海
        /// </summary>
        private CT_LoginUser _loginUser = new CT_LoginUser();
        /// <summary>
        /// 登录用户信息
        /// Author:台永海
        /// </summary>
        public CT_LoginUser LoginUserInfo
        {
            get { return _loginUser; }
        }
        /// <summary>
        /// Action访问前调用方法
        /// Author:台永海
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //客户端用户
                CT_LoginUser clientUser = GetClientUser();
                //检查是否有客户端用户
                if (clientUser != null)
                {
                    //服务端用户
                    CT_LoginUser serverUser = GetServerUser(clientUser.Id.ToString());
                    //检查是否有服务端用户
                    if (serverUser != null)
                    {
                        //检查客户端和服务端用户是否一致
                        if (clientUser.Id.Equals(serverUser.Id) && clientUser.LoginName.Equals(serverUser.LoginName))// && clientUser.Timestamp.Equals(serverUser.Timestamp) //单点登录验证时间戳
                        {
                            //存入登录用户
                            ViewData["LoginUserInfo"] = serverUser;
                            _loginUser = serverUser;
                            //执行父类Action内容
                            base.OnActionExecuting(filterContext);
                        }
                    }

                    //读取未读站内信数量
                    TXBll.Dev.DeveloperMessageBll _msgBll = new TXBll.Dev.DeveloperMessageBll();
                    ViewData["UnReadMsg"] = _msgBll.GetUnReadMsgCount(clientUser.Id);

                }
                //跳转登录页面
                if (!_loginUser.IsLogin)
                {
                    filterContext.Result = Redirect(String.Concat(TXCommons.GetConfig.BaseUrl, "user/login"));
                }
            }
            catch (Exception)
            {
                //Redirect(String.Concat(TXCommons.GetConfig.BaseUrl, "user/login"));
                throw;
            }
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
        /// <summary>
        /// 读取服务端用户信息
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        private CT_LoginUser GetServerUser(string userId)
        {
            return Redis.GetValue<CT_LoginUser>(KeyList.GetRedisDeveloperLoginKey(userId), ServiceStack.FunctionTypeEnum.NewHouseUserInfo, 0);
        }
    }
}
