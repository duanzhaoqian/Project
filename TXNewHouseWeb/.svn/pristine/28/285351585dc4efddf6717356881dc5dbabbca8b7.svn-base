using System;
using System.Collections.Generic;
using System.Linq;
using TXModel.Geography;
using TXOrm;

namespace TXDal.WebSite
{
    public class GeographyDal
    {
        /// <summary>
        /// 获取所有省份
        /// </summary>
        /// <returns>省份Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetProvinces()
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    return webEntity.Areas.Where(it => it.PId == 1).OrderBy(it => it.Sort).Select(area => new Z_GeographyItem {GeographyCode = area.Id, GeographyName = area.Name, GeographyLng = area.Lng, GeographyLat = area.Lat}).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", null, ex);
                return null;
            }
        }

        /// <summary>
        ///  根据省份ID获取所有城市
        /// </summary>
        /// <param name="provinceID">省份Id</param>
        /// <returns>城市Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetCities(int provinceID)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    return webEntity.Areas.Where(it => it.PId == provinceID && it.PId > 0).OrderBy(it => it.Sort).Select(area => new Z_GeographyItem {GeographyCode = area.Id, GeographyName = area.Name, GeographyLng = area.Lng, GeographyLat = area.Lat}).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("provinceId: {0}", provinceID), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取指定城市下的所有区域
        /// </summary>
        /// <param name="cityId">城市ID</param>
        /// <returns>区域Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetDistricts(int cityId)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    return webEntity.Districts.Where(district => district.CityId == cityId).OrderBy(it => it.Sort).Select(area => new Z_GeographyItem {GeographyCode = area.Id, GeographyName = area.Name, GeographyLng = area.Lng, GeographyLat = area.Lat})
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("cityId: {0}", cityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取指定区域下的所有商圈
        /// </summary>
        /// <param name="districtId">商圈ID</param>
        /// <returns>商圈Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetBussineses(int districtId)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    return webEntity.Businesses.Where(it => it.DId == districtId).OrderBy(it => it.Sort).Select(area => new Z_GeographyItem {GeographyCode = area.Id, GeographyName = area.Name, GeographyLng = area.Lng, GeographyLat = area.Lat})
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("districtId: {0}", districtId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据城市Id查询线路列表信息
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public List<Z_GeographyItem> GetSubWayLinesByCityID(int cityId)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    return webEntity.Traffic.Where(it => it.CityId == cityId && it.PId == 0).OrderBy(it => it.Sort).Select(it => new Z_GeographyItem {GeographyCode = it.TId, GeographyName = it.Name}).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("cityId: {0}", cityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据楼盘ID获取预售许可证列表
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPermitPresalesByPremisesId(int id)
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.PermitPresales.Where(it => it.PremisesId == id && it.IsDel == false).Select(it => new Z_GeographyItem {GeographyCode = it.Id, GeographyName = it.Name}).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id: {0}", id), ex);
                return null;
            }
        }

        #region 获取所有楼盘

        /// <summary>
        ///  获取所有楼盘
        /// </summary>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetPremises()
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.Premises.Where(it => it.IsDel == false).OrderBy(it => it.Id).Select(area =>
                        new Z_GeographyItem
                        {
                            GeographyCode = area.Id,
                            GeographyName = area.Name
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("胡航飞", "", ex);
                return null;
            }
        }

        #endregion

        #region 根据城市Id获取楼盘

        /// <summary>
        ///  根据城市Id获取楼盘
        /// </summary>
        /// <param name="cityId">楼城市Id</param> 
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetPremisesById(int cityId)
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.Premises.Where(it => it.CityId == cityId && it.IsDel == false).OrderBy(it => it.Id).Select(area =>
                        new Z_GeographyItem
                        {
                            GeographyCode = area.Id,
                            GeographyName = area.Name
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("胡航飞", "", ex);
                return null;
            }
        }

        #endregion

        #region 根据楼盘ID获取楼栋

        /// <summary>
        ///  根据楼盘ID获取楼栋
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetBuildings(int id)
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.Buildings.Where(it => it.PremisesId == id && it.IsDel == false).OrderBy(it => it.Id).Select(area =>
                        new Z_GeographyItem
                        {
                            GeographyCode = area.Id,
                            GeographyName = area.Name+area.NameType
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("id: {0}", id), ex);
                return null;
            }
        }

        #endregion

        #region 根据楼盘ID获取楼栋 剔除不可发布房源的楼栋（4 建设中 5规划中）

        /// <summary>
        ///  根据楼盘ID获取楼栋 剔除不可发布房源的楼栋（4 建设中 5规划中）
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetBuildings_Find(int id)
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.Buildings.Where(it => it.PremisesId == id && it.IsDel == false && it.State != 4 && it.State != 5).OrderBy(it => it.Id).Select(area =>
                        new Z_GeographyItem
                        {
                            GeographyCode = area.Id,
                            GeographyName = area.Name
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("id: {0}", id), ex);
                return null;
            }
        }

        #endregion

        #region 根据楼栋ID获取单元

        /// <summary>
        ///  根据楼栋ID获取单元
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetUnits(int id)
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.Units.Where(it => it.BuildingId == id && it.IsDel == false).OrderBy(it => it.Num).Select(area =>
                        new Z_GeographyItem
                        {
                            GeographyCode = area.Num,
                            GeographyName = area.Name
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("id: {0}", id), ex);
                return null;
            }
        }

        #endregion

        #region 根据楼栋ID获取楼层列表

        /// <summary>
        ///  根据楼栋ID获取楼层列表
        /// </summary>
        /// <param name="id">楼栋ID</param>
        /// <returns>Id和名称列表</returns>
        public List<Z_GeographyItem> Z_GetBuildingFloors(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var query = db.Buildings.FirstOrDefault(it => it.Id == id && it.IsDel == false);
                    var result = new List<Z_GeographyItem>();
                    if (query == null)
                        return result;
                    for (int i = -query.Underloor; i <= query.TheFloor; i++)
                    {
                        if (i != 0)
                            result.Add(new Z_GeographyItem
                            {
                                GeographyCode = i,
                                GeographyName = (i > 0 ? string.Format("{0}F", i) : string.Format("B{0}", Math.Abs(i))) //(i > 0 ? i : i*-1) + (i > 0 ? "F" : "B")
                            });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("id: {0}", id), ex);
                return null;
            }
        }

        #endregion
    }
}