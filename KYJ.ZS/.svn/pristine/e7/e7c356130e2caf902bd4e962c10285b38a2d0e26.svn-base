using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using KYJ.ZS.BLL.Merchants;
using KYJ.ZS.Commons;
using KYJ.ZS.Merchant.Controllers.ActionFilters;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.Merchant.Controllers
{
    public class MerchantManageController : BaseController
    {
        MerchantBll bll = new MerchantBll();
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-24s
        /// 检查当前商户输入密码是否正确
        /// </summary>     
        /// <param name="pwd">用户输入密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckPwd(string pwd)
        {

            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            if (!string.IsNullOrEmpty(pwd))
            {
                bool isEqual = bll.getPwd(id, pwd);
                return Json(isEqual);
            }
            return Json(false);

        }
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-5-5
        /// 商户修改密码
        /// </summary>     
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModifyPwd()
        {
            return View();

        }
        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月7日13:55:04
        /// 描述：用户退出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoginOut()
        {
            bll.LoginOut(_ServiceContext.CurrentUser._Guid);
            RedirectResult redirect = new RedirectResult(Url.MerchantSiteUrl().Login);
            return redirect;
        }

        /// <summary>
        /// Auther:李晓波
        /// Time:2014-5-5
        /// 商户修改密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <param name="confirmPwd">确认新密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyPwd(string oldPwd, string newPwd, string confirmPwd)
        {
            //Response.Write("修改密码");
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            int result = bll.UpdatePwd(id, oldPwd, newPwd);
            if (result > 0)
            {
                return Json(new { result = true });
            }
            else
                return Json(new { result = false });
        }
    }
}
