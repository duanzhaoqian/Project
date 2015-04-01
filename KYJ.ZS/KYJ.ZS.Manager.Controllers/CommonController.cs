using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.Manager;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.View;
using KYJ.ZS.BLL.Categories;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;
using KYJ.ZS.BLL.Authority;

namespace KYJ.ZS.Manager.Controllers
{
      /// <summary>
    /// 作者：孟国栋
    /// 时间：2014年5月27日
    /// 描述：管理员登录
    /// </summary>
    public class CommonController : Controller
    {
        ManagerBll bll = new ManagerBll();
        VerificationCode verificationcode = new VerificationCode();
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
                LoginUserInfo loginUser = RedisTool.GerLoginUserInfo(guid, UserInfoType.MANAGERLOGIN);
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
        /// <param name="returnUrl">登录后跳转页面URL</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (IsLogin())
            {
                return new RedirectResult(string.IsNullOrEmpty(returnUrl) ? Url.ManagerSiteUrl().Home_Index : returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
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
            if (!string.IsNullOrEmpty(loginName) && loginName.Length > 2)//校验登录名
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
                                bm.StrUrl = Url.ManagerSiteUrl().Home_Index; ;
                                if (!string.IsNullOrEmpty(returnUrl))
                                {
                                    bm.StrUrl = returnUrl;
                                }
                                bm.Action = BackAction.Redirect;

                                // 日志记录
                                LogCreateEntity logEntity = new LogCreateEntity() { };
                                logEntity.AdminId = new AdminsBll().GetAdminId(loginName);
                                logEntity.Name = loginName;
                                logEntity.RealName = new AdminsBll().GetRealName(loginName); ;
                                logEntity.ModuleId = -1;
                                logEntity.ModuleName = "登录退出";
                                logEntity.Remark = "登录平台";
                                new LogBll().CreateLog(logEntity);
                            }
                        }
                        else
                        {
                            bm.Message = "验证码错误！";
                        }
                    }
                    else
                    {
                        bm.Message = "请填写正确的验证码！";
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

        #region 类目下拉列表相关

        /// <summary>
        /// 获取一级类目
        /// </summary>
        /// <returns></returns>
        public JsonResult FirstCategory()
        {
            try
            {
                CategoryBll _categoryBll = new CategoryBll();
                var provinces = _categoryBll.GetCategorySelectList().ToArray();
                return Json(new { success = true, items = provinces }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取二级、三级类目
        /// </summary>
        /// <param name="categoryCode">父类目ID</param>
        /// <returns></returns>
        public JsonResult SecondThirdCategory(int categoryCode)
        {
            try
            {
                CategoryBll _categoryBll = new CategoryBll();
                var provinces = _categoryBll.GetCategorySelectList(categoryCode).ToArray();
                return Json(new { success = true, items = provinces }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
