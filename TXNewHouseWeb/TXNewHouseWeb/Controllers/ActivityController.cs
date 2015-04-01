using System.Collections.Generic;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using TXBll.Activitys;
using TXModel.Web;
using TXCommons;
using System.Text.RegularExpressions;

namespace TXNewHouseWeb.Controllers
{
    public class ActivityController : Controller
    {

        #region 摇号活动列表

        /// <summary>
        /// 摇号活动列表
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityList(string id)
        {
            ActivitysBll _activitysBll = new ActivitysBll();
            TXModel.Web.ActivityList activity = new TXModel.Web.ActivityList();
            activity = _activitysBll.GetExercise();
            ViewData["activity"] = activity;
            int pageIndex = 1;//当前页
            int pageSize = 2;//每页大小
            int totalCount = 0;//总记录数
            if (id != null) pageIndex = Util.ToInt(id); id = pageIndex.ToString();
            //分页页码
            if (id != null && id != "0")
                pageIndex = Util.ToInt(id);
            ViewData["conPage"] = "xinfang/yhhd-i" + id + "";
            IList<ActivityList> list = _activitysBll.GetExerciseList(pageIndex, pageSize, out totalCount);
            PagedList<ActivityList> searchList = new PagedList<ActivityList>(list, pageIndex, pageSize, totalCount);
            return View(searchList);
        }

        #endregion

        #region 摇号结果列表

        /// <summary>
        /// 摇号结果列表
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityResult()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 摇号直播页
        /// author:sunlin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ActivityLive(string id)
        {
            var acid = Regex.Match(id,@"\d{1,}").ToString();
            int activityId = acid == "" ? 0 : Util.ToInt(acid);
            ActivitysBll _activitysBll = new ActivitysBll();
            TXModel.Web.ActivityList activity = new TXModel.Web.ActivityList();
            activity = _activitysBll.GetActivityById(activityId);
            ViewData["activity"] = activity;
            return View();
        }
    }
}
