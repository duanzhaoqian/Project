using System.Web.Mvc;
using TXManagerWeb.Common;

namespace TXManagerWeb.Controllers
{
    public class CommonController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult Index()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            return new RedirectResult(string.Format("{0}common/signout.html", Auxiliary.Instance.ManagerUrl));
        }

        /// <summary>
        /// 公共管理首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult PIndex()
        {
            return new RedirectResult(string.Format("{0}common/pindex.html", Auxiliary.Instance.ManagerUrl));
        }

        /// <summary>
        /// 资讯管理首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult CIndex()
        {
            return new RedirectResult(string.Format("{0}common/cindex.html", Auxiliary.Instance.ManagerUrl));
        }

        /// <summary>
        /// 房产客服管理首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult RIndex()
        {
            return new RedirectResult(string.Format("{0}common/rindex.html", Auxiliary.Instance.ManagerUrl));
        }

        /// <summary>
        /// 财务管理首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult FIndex()
        {
            return new RedirectResult(string.Format("{0}common/findex.html", Auxiliary.Instance.ManagerUrl));
        }

        /// <summary>
        /// 经纪人管理首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult BIndex()
        {
            return new RedirectResult(string.Format("{0}common/bindex.html", Auxiliary.Instance.ManagerUrl));
        }

        /// <summary>
        /// 经纪公司管理首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult BCIndex()
        {
            return new RedirectResult(string.Format("{0}common/bcindex.html", Auxiliary.Instance.ManagerUrl));
        }

        /// <summary>
        /// 新房管理首页
        /// </summary>
        /// <returns></returns>
        [LoginChecked]
        public ActionResult NHouseIndex()
        {
            return View();
        }

    }
}