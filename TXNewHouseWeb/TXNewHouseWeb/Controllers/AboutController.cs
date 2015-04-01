using System.Web.Mvc;

namespace TXNewHouseWeb.Controllers
{
    public class AboutController : BaseController
    {
        /// <summary>
        /// 普通用户如何注册
        /// </summary>
        /// <returns></returns>
        public ActionResult View1()
        {
            ViewData["para"] = "1";
            return View();
        }
        /// <summary>
        /// 普通用户如何登录
        /// </summary>
        /// <returns></returns>
        public ActionResult View2()
        {
            ViewData["para"] = "2";
            return View();
        }

        /// <summary>
        /// 如何找回密码
        /// </summary>
        /// <returns></returns>
        public ActionResult View3()
        {
            ViewData["para"] = "3";
            return View();
        }

        /// <summary>
        /// 支付限额
        /// </summary>
        /// <returns></returns>
        public ActionResult View4()
        {
            ViewData["para"] = "4";
            return View();
        }

        /// <summary>
        /// 找房指南
        /// </summary>
        /// <returns></returns>
        public ActionResult View5()
        {
            ViewData["para"] = "5";
            return View();
        }

        /// <summary>
        /// 保证金制度
        /// </summary>
        /// <returns></returns>
        public ActionResult View6()
        {
            ViewData["para"] = "6";
            return View();
        }

        /// <summary>
        /// 营销活动说明
        /// </summary>
        /// <returns></returns>
        public ActionResult View7()
        {
            ViewData["para"] = "7";
            return View();
        }

        /// <summary>
        /// 如何修改手机号
        /// </summary>
        /// <returns></returns>
        public ActionResult View14()
        {
            ViewData["para"] = "14";
            return View();
        }

        /// <summary>
        /// 找不到图片上传按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult View15()
        {
            ViewData["para"] = "15";
            return View();
        }

        #region 开发商帮助
        /// <summary>
        /// 如何注册
        /// </summary>
        /// <returns></returns>
        public ActionResult View8()
        {
            ViewData["para"] = "8";
            return View();
        }
        /// <summary>
        /// 如何登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult View9()
        {
            ViewData["para"] = "9";
            return View();
        }
        /// <summary>
        /// 如何发布楼盘 
        /// </summary>
        /// <returns></returns>
        public ActionResult View10()
        {
            ViewData["para"] = "10";
            return View();
        }
        /// <summary>
        /// 如何发布楼栋 
        /// </summary>
        /// <returns></returns>
        public ActionResult View11()
        {
            ViewData["para"] = "11";
            return View();
        }
        /// <summary>
        /// 如何发布房源 
        /// </summary>
        /// <returns></returns>
        public ActionResult View12()
        {
            ViewData["para"] = "12";
            return View();
        }
        /// <summary>
        /// 如何创建营销活动 
        /// </summary>
        /// <returns></returns>
        public ActionResult View13()
        {
            ViewData["para"] = "13";
            return View();
        }

        #endregion 开发商帮助

    }
}
