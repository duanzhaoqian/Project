using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TXNewHouseWeb.Controllers
{
    /// <summary>
    /// 新房前台-地图找房控制器类
    /// </summary>
    public class MapSearchPremisesController : BaseController
    {
        /// <summary>
        /// 楼盘BLL类-前台
        /// </summary>
        readonly TXBll.HouseData.PremisesBll _premisesbll = new TXBll.HouseData.PremisesBll();

        //
        // GET: /MapSearchPremises/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 新房前台-地图找房页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MapSearch()
        {
            return View();
        }

        /// <summary>
        /// 地图找房页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MapSearchIndex()
        {
            return View();
        }

        /// <summary>
        /// 周边地图
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public ActionResult MapPremises(string premisesid)
        {
            TXOrm.Premis model = new TXOrm.Premis();
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                model = _premisesbll.GetPremisesbyId(premisesId);
            }
            return PartialView("MapSearch/_MapSearchPremises", model);
        }

        public ActionResult MapPremisessearch(string premiseskey)
        {
            TXOrm.Premis model = new TXOrm.Premis();
            List<TXOrm.Premis> premisList = _premisesbll.GetPremisesbyName(premiseskey);
            if (null != premisList && premisList.Count > 0)
            {

                model = premisList[0];
                model.Name = premiseskey;
            }
            model.Name = premiseskey;
            //ViewData["premisList"] = premisList;
            //ViewData["premiseskey"] = premiseskey;
            return PartialView("MapSearch/_MapSearchPremises", model);
        }

        /// <summary>
        /// 异步获取楼盘相关信息（装修情况、物业类型、开盘时间、预售许可证）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetAsyPremisesInfo(string id)
        {
            string _address = string.Empty;
            string _title = string.Empty;
            string _Lng = string.Empty;
            string _Lat = string.Empty;

            List<TXOrm.Premis> premisList = _premisesbll.GetPremisesbyName(id);
            if (null != premisList && premisList.Count > 0)
            {
                foreach (TXOrm.Premis tPremis in premisList) {
                    _address = _address + "##" + tPremis.PremisesAddress;
                    _title = _title + "##" + tPremis.Name;
                    _Lng = _Lng + "##" + tPremis.Lng;
                    _Lat = _Lat + "##" + tPremis.Lat;
                }
            }
            var result = new { address = _address, title = _title, Lng = _Lng, Lat = _Lat, icount = premisList.Count};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
