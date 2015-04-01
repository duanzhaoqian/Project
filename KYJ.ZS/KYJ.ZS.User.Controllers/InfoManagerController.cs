using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.SaleGoodses;
using KYJ.ZS.Models.SaleGoodses;
using KYJ.ZS.Commons;
namespace KYJ.ZS.User.Controllers
{
    /// <summary>
    /// 作者：menggd
    /// 时间：2014.04.25
    /// 描述：发布信息管理
    /// </summary>
    public class InfoManagerController : BaseController
    {

        //创建bll层的一个操作类的实例
        SaleGoods_InformationManagementBll bll = new SaleGoods_InformationManagementBll();
        /// <summary>
        /// 展示信息
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowPage(int? pageIndex)
        {
            //页码参数为空默认显示第一页
            if (!pageIndex.HasValue)
                pageIndex = 1;
            int userId = _ServiceContext.CurrentUser.UserID;
            if (bll.GetPageData(pageIndex.Value, userId).DataList.Count == 0)
            {
                pageIndex = pageIndex - 1;
                pageIndex = pageIndex == 0 ? 1 : pageIndex;
            }
            ViewData.Model = bll.GetPageData(pageIndex.Value, userId);
            return View();
        }
        /// <summary>
        /// 显示/隐藏商品信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowGoods()
        {
            //获取商品的ID
            int saleId = Auxiliary.ToInt32(Request["saleId"]);
            //获取商品的状态信息（显示/隐藏）
            int actionFlag = Auxiliary.ToInt32(Request["flag"]);
            int pageIndex = Auxiliary.ToInt32(Request["pageIndex"]);
            int userId = _ServiceContext.CurrentUser.UserID;
            bll.ShowGoods(saleId, actionFlag, userId);
            return RedirectToAction("showpage", new { pageIndex = pageIndex });
        }
        /// <summary>
        /// 删除商品信息(标识删除)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteGoods()
        {
            //获取商品ID
            int saleId = Auxiliary.ToInt32(Request["saleId"]);
            int pageIndex = Auxiliary.ToInt32(Request["pageIndex"]);
            int userId = _ServiceContext.CurrentUser.UserID;
            bool result = bll.DeleteGoods(saleId, userId);
            if (result)
            {
                return Json(1);
            }
            return Json(0);
            //return RedirectToAction("showpage", new { pageIndex = pageIndex });
        }


    }
}
