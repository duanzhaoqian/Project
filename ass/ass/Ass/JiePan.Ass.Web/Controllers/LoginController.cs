using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using JiePan.Ass.IBll;
using JiePan.Ass.Model;
using JiePan.Common.Web;

namespace JiePan.Ass.Web.Controllers
{
    public class LoginController : Controller
    {
        // 
        // GET: /Login/
        private IUserInfoBll userInfoBll;
        public LoginController(IUserInfoBll _userInfoBll)
        {
            userInfoBll = _userInfoBll;
        }
        public ActionResult Index()
        {
            //userInfoBll.Insert(new UserInfo() {LoginId = "admin",LoginPassWord = "admin",UserGroup = "0"});
            return View();
        }
        public ActionResult Login(string uname, string upwd)
        {
            IQueryable<UserInfo> users = userInfoBll.GetList(u => u.LoginId == uname && u.LoginPassWord == upwd);
            BackMessge bm = new BackMessge();
            bm.Success = users.Count() > 0;
            if (bm.Success)
            {
                bm.Message = "登陆成功，正在跳转....";
                bm.Action = BackAction.Redirect;
                bm.StrUrl = Url.Action("Index","Home");
            }
            else
            {
                bm.Message = "用户名和密码错误！";
            }
            return Json(bm);
        }
    }
}