using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TXCommons.Index;
using TXBll.NHouseSearch;
using System.Text.RegularExpressions;
using TXCommons;
using Webdiyer.WebControls.Mvc;

namespace TXNewHouseWeb.Controllers
{
    public class MapPremiseController : BaseController
    {
        /// <summary>
        /// 楼盘BLL类-前台
        /// </summary>
        readonly TXBll.HouseData.PremisesBll _premisesbll = new TXBll.HouseData.PremisesBll();

        /// <summary>
        /// 地图找房
        /// </summary>
        /// <param name="premiseskey"></param>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            TXOrm.Premis model = new TXOrm.Premis();
            List<TXOrm.Premis> premisList = _premisesbll.GetPremisesbyName("54");
            var p = TXCommons.ToJSon.ObjectToJson("premises", premisList).ToString();
            ViewData["premises"] = p;
            return View();
        }

        /// <summary>
        /// 搜索列表页
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public JsonResult PremisesInfo(string Id, string key, string type, int pageIndex)
        {
            #region 搜索数据处理
            string cityId = ViewData["cityId"] as string;//取城市
            if (string.IsNullOrWhiteSpace(cityId))
            {
                cityId = "253";
            }
            string _key = ViewData["cityPinyin"] as string + "-xinfang";

            if (!string.IsNullOrEmpty(Id))
            {
                var _p = Regex.Match(Id, @"-p\d{1,}").ToString();
                var _r = Regex.Match(Id, @"-r\d{1,}").ToString();
                if (!string.IsNullOrEmpty(_p))
                {
                    _key += Id.Replace(_p, "-quyu-" + PremisesType.GetSearchPrice(_p) + "-s4");
                }
                if (!string.IsNullOrEmpty(_r))
                {
                    _key += Id.Replace(_r, "-quyu" + _r + "-s4");
                }
                if (string.IsNullOrEmpty(_r) && string.IsNullOrEmpty(_p))
                {
                    _key += "-" + Id + "-s4";
                }
            }
            else
            {
                _key += "-quyu-s4";
            }
            
            if (!string.IsNullOrEmpty(key))
                _key += "-w_" + key;
            if (pageIndex > 1)
                _key += "-i" + pageIndex;

            PagedList<IndexPremises> searchList = null;
            #endregion
            try
            {
                //Id:条件，地址，城市
                searchList = SearchBll.NHouseListResult(_key.Replace("я", "·"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
            }
            catch (Exception) { }
            if (searchList != null)
            {
                var result = new { TotalItemCount = searchList.TotalItemCount, TotalPageCount = searchList.TotalPageCount, CityName = ViewData["cityName"] as string, PageSize = searchList.PageSize, CurrentPageIndex = searchList.CurrentPageIndex, searchList };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { TotalItemCount = 0, TotalPageCount = 0, PageSize = 10, CurrentPageIndex = 1, CityName = ViewData["cityName"] as string, searchList }, JsonRequestBehavior.AllowGet); ;
            }

        }

    }
}
