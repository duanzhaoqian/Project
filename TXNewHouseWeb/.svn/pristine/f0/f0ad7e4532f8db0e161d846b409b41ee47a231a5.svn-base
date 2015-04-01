using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TXBll.HouseData;
using TXBll.NHouseActivies.Advertise;
using TXBll.WebSite;
using TXModel.Geography;

namespace TXManagerWeb.Controllers
{
    public class GeographyController : BaseController
    {
        //
        // GET: /Geography/

        #region 获取省份

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        public JsonResult Provinces()
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var provinces = _geographyBll.Z_GetProvinces().ToArray();
                return Json(new { success = true, items = provinces }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 获取城市

        /// <summary>
        /// 获取城市
        /// <param name="geographyCode">省份ID</param>
        /// </summary>
        public JsonResult Cities(int geographyCode)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var cities = _geographyBll.Z_GetCities(geographyCode).ToArray();
                return Json(new { success = true, items = cities }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        /// <summary>
        /// 获取环线
        /// </summary>
        /// <param name="geographyCode">城市id</param>
        /// <returns></returns>
        public JsonResult Ring(int geographyCode)
        {
            try
            {
                var ring = TXCommons.Admins.AdminComs.Instance.GetRingLine(geographyCode);
                return Json(new { success = true, items = ring }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { sucess = false }, JsonRequestBehavior.AllowGet);
            }

        }

        #region 页面获取区域

        /// <summary>
        /// 页面获取区域
        /// </summary>
        /// <param name="geographyCode">城市ID</param>
        public JsonResult Districts(int geographyCode)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var channels = _geographyBll.Z_GetDistricts(geographyCode).ToArray();
                return Json(new { success = true, items = channels }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 页面获取商圈

        /// <summary>
        /// 页面获取商圈
        /// </summary>
        /// <param name="geographyCode">区域ID</param>
        public JsonResult Businesses(int geographyCode)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var channels = _geographyBll.Z_GetBussineses(geographyCode).ToArray();
                return Json(new { success = true, items = channels }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据城市Id获取楼盘

        /// <summary>
        /// 根据城市Id获取楼盘
        /// </summary>
        /// <param name="cityId">城市Id</param>
        public JsonResult Premises(int cityId)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var channels = _geographyBll.Z_GetPremisesById(cityId).ToArray();
                return Json(new { success = true, items = channels }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据楼盘ID获取楼栋

        /// <summary>
        ///  根据楼盘ID获取楼栋
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public JsonResult Buildings(int id)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var buildings = _geographyBll.Z_GetBuildings(id).ToArray();
                return Json(new { success = true, items = buildings }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据楼盘ID获取楼栋 剔除不可发布房源的楼栋（3 建设中 4规划中）

        /// <summary>
        ///  根据楼盘ID获取楼栋 剔除不可发布房源的楼栋（3 建设中 4规划中）
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public JsonResult Buildings_Find(int id)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var buildings = _geographyBll.Z_GetBuildings_Find(id).ToArray();
                return Json(new { success = true, items = buildings }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据楼栋ID获取单元

        /// <summary>
        ///  根据楼栋ID获取单元
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns>Id和名称列表</returns>
        public JsonResult Units(int id)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var units = _geographyBll.Z_GetUnits(id).ToArray();
                return Json(new { success = true, items = units }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据楼栋ID获取楼层列表

        /// <summary>
        ///  根据楼栋ID获取楼层列表
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns>Id和名称列表</returns>
        public JsonResult BuildingFloors(int id)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var buildingFloors = _geographyBll.Z_GetBuildingFloors(id).ToArray();
                return Json(new { success = true, items = buildingFloors }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 根据楼栋ID获取楼层列表
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns></returns>
        public JsonResult GetFloorsByBuildingId(int id)
        {
            try
            {
                var building = new BuildingBll().GetEntity_ById(id);
                if (null == building)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

                var theFloor = building.TheFloor;
                var underFloor = building.Underloor;

                var list = new List<Z_GeographyItem>();

                if (0 < theFloor)
                {
                    list.Add(new Z_GeographyItem
                    {
                        GeographyCode = 0,
                        GeographyName = "地上",
                        GeographyLng = "--",
                        GeographyLat = "--"
                    });

                    for (int i = 1; i <= theFloor; i++)
                    {
                        list.Add(new Z_GeographyItem
                        {
                            GeographyCode = i,
                            GeographyName = string.Format("{0}F", i),
                            GeographyLng = "0",
                            GeographyLat = "0"
                        });
                    }
                }

                //  地下
                if (0 < Math.Abs(underFloor))
                {
                    list.Add(new Z_GeographyItem
                    {
                        GeographyCode = 0,
                        GeographyName = "地下",
                        GeographyLng = "--",
                        GeographyLat = "--"
                    });

                    for (int i = 1; i <= Math.Abs(underFloor); i++)
                    {
                        list.Add(new Z_GeographyItem
                        {
                            GeographyCode = (0 - i),
                            GeographyName = string.Format("B{0}", i),
                            GeographyLng = "0",
                            GeographyLat = "0"
                        });
                    }
                }

                return Json(new { success = true, items = list.ToArray() }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据楼盘Id获取预售许可证

        /// <summary>
        /// 根据楼盘Id获取预售许可证
        /// </summary>
        /// <param name="id">楼盘Id</param>
        /// <returns></returns>
        public JsonResult GetPermitPresalesByPremisesId(int id)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var permitPresales = _geographyBll.GetPermitPresalesByPremisesId(id).ToArray();
                return Json(new { success = true, items = permitPresales }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据楼盘Id获取楼栋平面图

        /// <summary>
        /// 根据楼盘Id获取楼栋平面图
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">楼盘编号</param>
        /// <returns></returns>
        public JsonResult GetBuildingPlanListByPremisesId(int id)
        {
            try
            {
                var _geographyBll = new GeographyBll();
                var permitPresales = _geographyBll.GetPermitPresalesByPremisesId(id).ToArray();
                return Json(new { success = true, items = permitPresales }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 根据关键字搜索开放商列表

        /// <summary>
        /// 根据关键字搜索开放商列表
        /// </summary>
        /// <param name="q">key</param>
        /// <returns></returns>
        public JsonResult GetDevelopersByKeyWord(string q)
        {
            var developersbll = new TXBll.Dev.DevelopersBll();
            var list = developersbll.GetDevelopersByKeyWord(q);
            //string resultStr = list.Aggregate(string.Empty, (current, m) => current + ("{" + string.Format("result:'true',id:'{0}',Name:'{1}'", m.Id, m.Name) + "}\n"));
            if (list.Any())
                return Json(new { result = list.Any(), redate = list }, JsonRequestBehavior.AllowGet);
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        #region 根据关键字搜索楼盘列表
        [HttpPost]
        public JsonResult CityPremisesByKeyword()
        {
            var adbll = new PremisesBll();
            int cityid;
            string keyword = Request["q"];
            if (int.TryParse(Request["c"], out cityid) && !string.IsNullOrWhiteSpace(keyword))
            {
                var list = adbll.GetJsonPremises_ByCityIDORKeywords(cityid, keyword);
                if (!string.IsNullOrWhiteSpace(list))
                    return Json(new { success = true, result = list });
            }
            return Json(new { success = false });
        }
        #endregion
        #endregion

        #region 根据楼栋编号获取单元列表

        /// <summary>
        /// 根据楼栋编号获取单元编号集合
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public JsonResult GetUnitsByBuildingId(int buildingId)
        {
            try
            {
                var list = new UnitBll().GetUnitListByBuildingId(buildingId).ToList()
                    .ConvertAll(
                        it => new Z_GeographyItem
                        {
                            GeographyCode = it.Num,
                            GeographyName = it.Name,
                            GeographyLng = "0",
                            GeographyLat = "0"
                        });
                return Json(new { success = true, items = list }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

    }
}