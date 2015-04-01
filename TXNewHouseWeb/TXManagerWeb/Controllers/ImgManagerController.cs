using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TXBll.HouseData;
using TXBll.WebSite;
using TXCommons.PictureModular;
using TXManagerWeb.Common;
using TXModel.AdminPVM;
using TXOrm;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.HouseData.PicMg)]
    public class ImgManagerController : BaseController
    {
        public ActionResult Index()
        {
            List<SelectListItem> province = new List<SelectListItem>();
            List<SelectListItem> city = new List<SelectListItem>();
            province.Add(new SelectListItem { Text = "请选择" });
            city.Add(new SelectListItem { Text = "请选择" });
            var _geographyBll = new GeographyBll();
            var provincelist = _geographyBll.Z_GetProvinces().ToList().ConvertAll(it => new SelectListItem
            {
                Text = it.GeographyName,
                Value = it.GeographyCode.ToString()
            });
            province.AddRange(provincelist);
            ViewData["ProvinceID"] = province;
            ViewData["CityId"] = city;
            return View();
        }
        [HttpPost]
        public ActionResult SearchImg()
        {
            List<PremiseImgMap> imgslist = new List<PremiseImgMap>();
            int city;
            int province;
            int pageindex;
            int pagesize = 20;
            if (int.TryParse(Request["city"], out city) && int.TryParse(Request["province"], out province))
            {

                if (int.TryParse(Request["pindex"], out pageindex) && int.TryParse(Request["pagesize"], out pagesize))
                {
                    imgslist = new PremiseImgMapBll().GetPermitImg(city, pageindex, pagesize);
                }
            }
            return PartialView("_imgList", imgslist);
        }
        [HttpPost]
        public ActionResult SearchImgCount()
        {
            int cityid;
            if (int.TryParse(Request["cityid"], out cityid))
            {
                PremiseImgMapBll bll = new PremiseImgMapBll();
                return Json(bll.GetAllCount(cityid));
            }
            return Json("0");
        }

        public ActionResult DelPerimseMap()
        {
            int id;
            if (int.TryParse(Request["id"], out id))
            {
                var admin = _ServiceContext.CurrentUser;
                if (admin != null)
                {
                    PremiseImgMapBll bll = new PremiseImgMapBll();
                    PremiseImgMap model = new PremiseImgMap()
                        {
                            ID = id,
                            adminid = admin.Id,
                            adminName = admin.LoginName
                        };
                    int result = bll.DelPermitImgMap(model);
                    return Json(result);
                }
            }
            return Json("");
        }
    }
}
