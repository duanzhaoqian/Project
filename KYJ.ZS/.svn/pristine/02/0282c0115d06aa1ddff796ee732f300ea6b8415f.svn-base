using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.DB;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.User.Controllers.ActionFilters;
using KYJ.ZS.Models.Collections;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons;
namespace KYJ.ZS.User.Controllers
{
    public class CollectController : BaseController
    {
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-25
        /// 用户后台收藏列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public ActionResult List(int? pageIndex)
        {
            var bll = new KYJ.ZS.BLL.Collections.CollectionBll();
            int index = pageIndex ?? 1;//当前页
            int totalRecord = 0;//总个数
            int totalPage = 0;//总页数
            int pageSize = 50;//每页显示的个数
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            List<CollectionEntity> list = new List<CollectionEntity>();
            list = bll.GetCollectionGoodsesPage(userId, index, pageSize, out totalRecord, out totalPage);
            PagedList<CollectionEntity> pageList = null;
            if (list != null)
            {
                pageList = new PagedList<CollectionEntity>(list, index, pageSize, totalRecord);
            }
            else
            {
                if (index > 1)
                {
                    index = index - 1;
                    string url = Url.UserSiteUrl().CollectList + "?pageIndex=" + index;
                    return Redirect(url);
                }
            }
            return View(pageList);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-25
        /// Desc:删除收藏方法
        /// </summary>
        /// <param name="collId">收藏表ID</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public ActionResult DelCollection(string collId)
        {
            var bll = new KYJ.ZS.BLL.Collections.CollectionBll();
            int userId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            int result = bll.DelCollection(userId, collId);
            return Json(result);
        }
    }
}
