using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Merchant.Controllers
{
    using System.Web.Mvc;

    using KYJ.ZS.BLL.Categories;
    using KYJ.ZS.Commons;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/23 17:16:10
    /// 描述：类目controller
    /// </summary>
    public class CategoryController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var category = new CategoryBll();

            var list = category.GetCateGoryList();

            return View(list);
        }

        [HttpGet]
        public ActionResult GetSonCategory()
        {
            var pid = Request["id"].ToInt();

            var category = new CategoryBll();

            var list = category.GetCateGoryList(pid);

            return PartialView("Category/_Index", list);
        }

    }
}
