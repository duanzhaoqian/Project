using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TXBll.NHouseActivies.Advertise;
using TXBll.WebSite;
using TXManagerWeb.Common;
using TXModel.AdminPVM;
using System;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    [ActionFilters.AdminPageInfo(6, (int)AdminNavi.NewHouseMg.Advertise.SetAdv)]
    public class NhAdvertiseController : BaseController
    {

        /// <summary>
        /// 广告设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int cityid;
            int position;
            if (Request.IsAjaxRequest())
            {
                if (int.TryParse(Request["CityID"], out cityid) && int.TryParse(Request["Type"], out position))
                {
                    var bargainBll = new Advertise_AdminBll();
                    var list = bargainBll.GetAdvertiseList_Admin(cityid, position);
                    return PartialView("_AdList", list);
                }
                return PartialView("_AdList");
            }
            else
            {
                PVE_HN_ADvertise model = new PVE_HN_ADvertise
                {
                    Provinces = new System.Collections.Generic.List<SelectListItem>(),
                    Cityes = new System.Collections.Generic.List<SelectListItem>(),
                    Type = 0
                };
                model.Provinces.Add(new SelectListItem { Text = "请选择" });
                model.Cityes.Add(new SelectListItem { Text = "请选择" });
                var _geographyBll = new GeographyBll();
                var provincelist = _geographyBll.Z_GetProvinces().ToList().ConvertAll(it => new SelectListItem
                {
                    Text = it.GeographyName,
                    Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                });
                model.Provinces.AddRange(provincelist);
                return View(model);
            }


        }

        public JsonResult DelAd()
        {
            int adid;
            if (int.TryParse(Request["adid"], out adid))
            {
                Advertise_AdminBll bll = new Advertise_AdminBll();
                int result = bll.DeleteAdvertise_ById(adid);
                if (result > 0)
                    return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult SaveAD(List<string> data)
        {
            List<TXOrm.Advertise> list = new List<TXOrm.Advertise>();
            int cityid = 0;
            int position = 0;
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    TXOrm.Advertise model = new TXOrm.Advertise();
                    string[] value = item.Split('﹡');
                    if (value != null && value.Length == 9)
                    {

                        int premiseid;
                        int id;
                        int seq;
                        DateTime begin;
                        DateTime end;
                        #region 城市

                        if (int.TryParse(value[0], out cityid))
                        {
                            model.CityId = cityid;
                        }
                        else
                        {
                            return Json(new { success = false, msg = "城市id错误" });
                        }
                        model.CityName = value[1];

                        #endregion

                        #region 位置

                        if (int.TryParse(value[2], out position))
                            model.Position = position;
                        else
                        {
                            return Json(new { success = false, msg = "广告位置错误" });
                        }

                        #endregion

                        #region seq

                        if (int.TryParse(value[3], out seq))
                        {
                            model.SequenceNum = seq;
                        }
                        else
                        {
                            return Json(new { success = false, msg = "排序号错误" });
                        }

                        #endregion
                        #region 楼盘

                        model.PremisesName = value[4];
                        if (int.TryParse(value[5], out premiseid))
                        {
                            model.PremisesId = premiseid;
                        }
                        else
                        {
                            return Json(new { success = false, msg = "楼盘id错误" });
                        }

                        #endregion

                        if (DateTime.TryParse(value[6], out begin) && DateTime.TryParse(value[7], out end))
                        {
                            model.BeginTime = begin;
                            model.EndTime = end;
                        }
                        else
                        {
                            return Json(new { success = false, msg = "时间错误" });
                        }
                        if (!string.IsNullOrEmpty(value[8]))
                        {
                            if (int.TryParse(value[8], out id))
                            {
                                model.Id = id;
                            }
                            else
                            {
                                return Json(new { success = false, msg = "AD id错误" });
                            }
                        }
                        else
                        {
                            model.CreateTime = DateTime.Now;
                        }
                        var user = _ServiceContext.CurrentUser;
                        model.AdminID = user.Id;
                        model.AdminName = user.LoginName;
                        list.Add(model);
                    }
                    else
                    {
                        return Json(new { success = false, msg = "参数信息错误" });
                    }
                }
            }
            var bargainBll = new Advertise_AdminBll();
            if (list != null && list.Count > 0)
            {
                int i = bargainBll.AddOrUpdateAdvertise(list);
                if (i > 0)
                {
                    var adlist = bargainBll.GetAdvertiseList_Admin(cityid, position);
                    return PartialView("_AdList", adlist);
                }
            }
            return Json(new { success = false, msg = "保存失败" });
        }
    }
}
