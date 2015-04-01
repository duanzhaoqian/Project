using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Web.Controllers.ActionFilters;
using KYJ.ZS.BLL.Brands;
using KYJ.ZS.BLL.Categories;

namespace KYJ.ZS.Web.Controllers
{
    public class CommonController : BaseController
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-13
        /// 描述：首页数据显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            BrandBll bankBll = new BrandBll();
            CategoryBll categoryBll = new CategoryBll();
            ViewData["BrandList"] = bankBll.Web_GetNavigationBrands(); //品牌
            ViewData["CFurnitureList"] = categoryBll.Web_GetNavigationCategoryByParentId(11);//家具分类
            ViewData["CApplianceList"] = categoryBll.Web_GetNavigationCategoryByParentId(1);//家电分类
            return View();
        }

        #region Geography地理位置

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        public JsonResult Provinces()
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var provinces = _geographyBll.GetProvinces().ToArray();
                return Json(new { success = true, items = provinces }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取城市
        /// <param name="geographyCode">省份ID</param>
        /// </summary>
        public JsonResult Cities(int geographyCode)
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var cities = _geographyBll.GetCities(geographyCode).ToArray();
                return Json(new { success = true, items = cities }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 页面获取区域
        /// </summary>
        /// <param name="geographyCode">城市ID</param>
        public JsonResult Districts(int geographyCode)
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var channels = _geographyBll.GetDistricts(geographyCode).ToArray();
                return Json(new { success = true, items = channels }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 页面获取商圈
        /// </summary>
        /// <param name="geographyCode">区域ID</param>
        public JsonResult Businesses(int geographyCode)
        {
            try
            {
                KYJ.ZS.BLL.Geographies.GeographyBll _geographyBll = new KYJ.ZS.BLL.Geographies.GeographyBll();
                var channels = _geographyBll.GetBussineses(geographyCode).ToArray();
                return Json(new { success = true, items = channels }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
