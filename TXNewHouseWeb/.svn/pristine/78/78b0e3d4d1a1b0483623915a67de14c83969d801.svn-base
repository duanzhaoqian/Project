using System;
using System.Collections.Generic;
using System.Linq;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXModel.NHouseActivies.Common;
using TXOrm;

namespace TXDal.NHouseActivies.SecKill
{
    /// <summary>
    /// 秒杀(网站管理平台)
    /// </summary>
    public partial class SecKillDal : BaseDal_Admin
    {
        /// <summary>
        /// 根据省编号、市编号、开发商名称获取开发商列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="provinceId">省编号</param>
        /// <param name="cityId">市编号</param>
        /// <param name="cmpname">开发商名称</param>
        /// <returns></returns>
        public List<Developer_Identity> GetList_ByProvinceIdCityIdCompanyName(int provinceId, int cityId, string cmpname)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string s_province = string.Empty, s_city = string.Empty;

                    string sql = @"
SELECT  b.*
FROM    Developer (NOLOCK) AS a
        INNER JOIN Developer_Identity (NOLOCK) AS b ON b.DeveloperId = a.Id
                                                       AND b.IsDel = 0
WHERE   a.IsDel = 0
        {0}
        {1}
        AND b.CompanyName LIKE '%{2}%'";

                    if (0 < provinceId)
                    {
                        s_province = string.Format("AND a.ProvinceId = {0}", provinceId);
                    }
                    if (0 < cityId)
                    {
                        s_city = string.Format("AND a.CityId = {0}", cityId);
                    }

                    sql = string.Format(sql, s_province, s_city, cmpname);

                    var list = db.ExecuteStoreQuery<Developer_Identity>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("provinceId:{0}, cityId:{1}, cmpname:{2}", new object[] {provinceId, cityId, cmpname}), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据省编号、市编号、开发商名称获取开发商列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="provinceId">省编号</param>
        /// <param name="cityId">市编号</param>
        /// <param name="name">开发商/代理商名称</param>
        /// <returns></returns>
        public List<PVS_NH_DeveloperAgentName> GetList_ByProvinceIdCityIdDeveloperAgent(int provinceId, int cityId, string name)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string s_province = string.Empty, s_city = string.Empty;

                    string sql = @"
SELECT  DISTINCT
        Developer AS Name
FROM    Premises (NOLOCK)
WHERE   1 = 1
        {0}
        {1}
        AND ( Developer LIKE '%{2}%' )
UNION
SELECT  DISTINCT
        Agent AS Name
FROM    Premises (NOLOCK)
WHERE   1 = 1
        {0}
        {1}
        AND ( Agent LIKE '%{2}%' )";

                    if (0 < provinceId)
                    {
                        s_province = string.Format("AND ProvinceId = {0}", provinceId);
                    }
                    if (0 < cityId)
                    {
                        s_city = string.Format("AND CityId = {0}", cityId);
                    }

                    sql = string.Format(sql, s_province, s_city, name);

                    var list = db.ExecuteStoreQuery<PVS_NH_DeveloperAgentName>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("provinceId:{0}, cityId:{1}, name:{2}", new object[] {provinceId, cityId, name}), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据条件获取秒杀活动信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_SecKill> GetPageList_BySearch_Admin(PVS_NH_SecKill search, int pageIndex, int pageSize)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  z.Id, z.HouseId, z.Name AS HouseName, z.[Floor], z.UnitName, z.BuildingId, z.BuildingName, z.BuildingNameType, z.Developer, z.Agent,
        z.TotalPrice, z.BidPrice, z.BeginTime, z.EndTime, z.CityId, z.CityName as BelongCity, z.ActivitieState
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.Id DESC ) AS rowid, a.Id, b.HouseId, d.Name, d.[Floor],
                    e.Name AS UnitName, b.BuildingId, c.Name AS BuildingName, c.NameType AS BuildingNameType,
                    f.Developer, f.Agent, d.TotalPrice, a.BidPrice, a.BeginTime, a.EndTime, f.CityId, f.CityName, a.ActivitieState
          FROM      Activities (NOLOCK) AS a
                    INNER JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
                                                                AND b.IsDel = 0
                    LEFT JOIN Building (NOLOCK) AS c ON c.Id = b.BuildingId
                                                        AND c.IsDel = 0
                    LEFT JOIN House (NOLOCK) AS d ON d.Id = b.HouseId
                                                     AND d.BuildingId = b.BuildingId
                                                     AND d.IsDel = 0
                    INNER JOIN Unit (NOLOCK) AS e ON e.Num = d.UnitId
                                                     AND e.BuildingId = b.BuildingId
                                                     AND e.IsDel = 0
                    INNER JOIN Premises (NOLOCK) AS f ON f.Id = b.PremisesId
                                                         AND f.IsDel = 0
          WHERE     a.IsDel = 0
                    AND a.[Type] = 7
                    {0} --AND f.ProvinceId = 1
                    {1} --AND f.CityId = 1
                    {2} --AND f.DeveloperId LIKE '%1%'
                    {3} --AND b.PremisesId = 1
                    {4} --AND b.BuildingId = 1
                    {5} --AND d.UnitId = 1
                    {6} --AND d.[Floor] = 1
                    {7} --AND ( a.BeginTime < GETDATE() )
        ) AS z
WHERE   z.rowid BETWEEN {8} AND {9}";

                    sql = string.Format(sql, GetParms_PageList_BySearch_Admin(search, pageIndex, pageSize));

                    var list = db.ExecuteStoreQuery<PVM_NH_SecKill>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据条件获取秒杀活动信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Admin(PVS_NH_SecKill search)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  COUNT(1) AS Result
FROM    Activities (NOLOCK) AS a
        INNER JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
                                                    AND b.IsDel = 0
        LEFT JOIN Building (NOLOCK) AS c ON c.Id = b.BuildingId
                                            AND c.IsDel = 0
        LEFT JOIN House (NOLOCK) AS d ON d.Id = b.HouseId
                                         AND d.BuildingId = b.BuildingId
                                         AND d.IsDel = 0
        INNER JOIN Unit (NOLOCK) AS e ON e.Num = d.UnitId
                                         AND e.BuildingId = b.BuildingId
                                         AND e.IsDel = 0
        INNER JOIN Premises (NOLOCK) AS f ON f.Id = b.PremisesId
                                             AND f.IsDel = 0
WHERE   a.IsDel = 0
        AND a.[Type] = 7
        {0} --AND f.ProvinceId = 1
        {1} --AND f.CityId = 1
        {2} --AND f.Developer LIKE '%1%'
        {3} --AND b.PremisesId = 1
        {4} --AND b.BuildingId = 1
        {5} --AND d.UnitId = 1
        {6} --AND d.[Floor] = 1
        {7} --AND ( a.BeginTime < GETDATE() )";
                    sql = string.Format(sql, GetParms_PageList_BySearch_Admin(search));

                    var list = houseDb.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取秒杀活动信息的检索条件
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private object[] GetParms_PageList_BySearch_Admin(PVS_NH_SecKill search, int pageIndex = 0, int pageSize = 0)
        {
            var list = new List<object>
            {
                (0 < search.ProvinceId) ? string.Format("AND f.ProvinceId = {0}", search.ProvinceId) : string.Empty,
                (0 < search.CityId) ? string.Format("AND f.CityId = {0}", search.CityId) : string.Empty,
                (!string.IsNullOrEmpty(search.DeveloperName)) ? string.Format("AND ( f.Developer LIKE '%{0}%' OR f.Agent LIKE '%{0}%' )", search.DeveloperName) : string.Empty,
                (0 < search.PremisesId) ? string.Format("AND b.PremisesId = {0}", search.PremisesId) : string.Empty,
                (0 < search.BuildingId) ? string.Format("AND b.BuildingId = {0}", search.BuildingId) : string.Empty,
                (0 < search.UnitNum) ? string.Format("AND d.UnitId = {0}", search.UnitNum) : string.Empty,
                (0 < search.Floor) ? string.Format("AND d.[Floor] = {0}", search.Floor) : string.Empty
            };
            if (-1 < search.ActivitieState)
            {
                #region 状态(时间控制)

                //if (0 == search.ActivitieState)
                //{
                //    list.Add(string.Format("AND ( GETDATE() < a.BeginTime )"));
                //}
                //else if (1 == search.ActivitieState)
                //{
                //    list.Add(string.Format("AND (( a.BeginTime < GETDATE() ) AND ( GETDATE() < a.EndTime ))"));
                //}
                //else if (2 == search.ActivitieState)
                //{
                //    list.Add(string.Format("AND ( a.EndTime < GETDATE() )"));
                //}
                //else
                //{
                //    list.Add(string.Empty);
                //}

                #endregion

                list.Add(string.Format("AND a.ActivitieState = {0}", search.ActivitieState));
            }
            else
            {
                list.Add(string.Empty);
            }

            if (pageIndex > 0
                && pageSize > 0)
            {
                int startIndex = (pageIndex - 1)*pageSize + 1;
                int endIndex = pageIndex*pageSize;

                list.Add(startIndex);
                list.Add(endIndex);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 根据活动编号获取房源信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<PVE_NH_SecKill_HouseInfo> GetHouseInfoByActivityId(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  a.HouseId, a. PremisesId, b.Name AS PremiseName, c.Name AS BuildingName, c.NameType AS BuildingNameType, e.Name AS UnitName, d.[Floor],
        d.Name AS HouseName
FROM    ActivitiesHouse (NOLOCK) AS a
        LEFT JOIN Premises (NOLOCK) AS b ON b.Id = a.PremisesId
                                            AND b.IsDel = 0
        LEFT JOIN Building (NOLOCK) AS c ON c.Id = a.BuildingId
                                            AND c.IsDel = 0
        LEFT JOIN House (NOLOCK) AS d ON d.Id = a.HouseId
                                         AND d.BuildingId = a.BuildingId
                                         AND d.IsDel = 0
        LEFT JOIN Unit (NOLOCK) AS e ON e.Num = a.UnitId
                                        AND e.BuildingId = d.BuildingId
                                        AND e.IsDel = 0
WHERE   a.ActivitiesId = {0}
        AND a.IsDel = 0";
                    sql = string.Format(sql, activityId);

                    var list = db.ExecuteStoreQuery<PVE_NH_SecKill_HouseInfo>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据活动、房源编号获取报名信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <param name="houseId">房源信息</param>
        /// <returns></returns>
        public List<PVM_NH_SecKill_UserReport> GetUserReportsByActivityId(int activityId, int houseId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  BidUserName, BidRealName, IDCard, BidUserMobile, BidUserMail, BidUserQQ, CreateTime AS BidTime
FROM    BidPrice (NOLOCK) AS a
WHERE   1 = 1
        AND a.HouseId = {0}
        AND a.ActivitiesId = {1}
        AND a.IsDel = 0";
                    sql = string.Format(sql, houseId, activityId);

                    var list = db.ExecuteStoreQuery<PVM_NH_SecKill_UserReport>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activityId:{0}, houseId:{1}", activityId, houseId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据活动编号删除活动相关信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public ESqlResult DelByActivityId(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
BEGIN TRY
    BEGIN TRAN
    -- 更新活动表
    UPDATE  Activities
    SET     IsDel = 1, UpdateTime = GETDATE()
    WHERE   Id = {0}
    -- 更新流动房源表
    UPDATE  ActivitiesHouse
    SET     IsDel = 1, UpdateTime = GETDATE()
    WHERE   ActivitiesId = {0}
    -- 更新出价表（报名表）
    UPDATE  BidPrice
    SET     IsDel = 1, UpdateTime = GETDATE()
    WHERE   ActivitiesId = {0}
    -- 更新定时表
    UPDATE  AmountTiming
    SET     EndTime = DATEADD(DAY, 7,
                              DATENAME(Year, GETDATE()) + N'-' + CAST(DATEPART(Month, GETDATE()) AS VARCHAR) + N'-'
                              + DATENAME(Day, GETDATE()))
    WHERE   ActivityID = {0}
    COMMIT
    SELECT  '1' AS Code, '执行成功' AS Msg
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '执行失败' AS Msg
END CATCH";

                    sql = string.Format(sql, activityId);

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return list[0];
                    }

                    return new ESqlResult {Code = "0", Msg = "操作失败"};
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取搜索记录
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_SecKill_Edit_Houses> GetPageList_BySearch_EditHouses_Admin(PVS_NH_SecKill_Edit search, int pageIndex = 0, int pageSize = 0)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  z.HouseId, z.HouseName, z.[Floor], z.UnitName, z.BuildingName, z.BuildingNameType, z.Room, z.Hall, z.Toilet,
        z.Kitchen, z.BuildingArea, z.SinglePrice, z.TotalPrice, z.SalesStatus, z.CreateTime
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.Id ASC ) AS rowid, a.Id AS HouseId, a.Name AS HouseName, a.[Floor],
                    c.Name AS UnitName, b.Name AS BuildingName, b.NameType AS BuildingNameType, a.Room, a.Hall, a.Toilet,
                    a.Kitchen, a.BuildingArea, a.SinglePrice, a.TotalPrice, a.SalesStatus, a.CreateTime
          FROM      House (NOLOCK) AS a
                    INNER JOIN Building (NOLOCK) AS b ON b.Id = a.BuildingId
                                                         AND b.IsDel = 0
                    LEFT JOIN Unit (NOLOCK) AS c ON c.Num = a.UnitId
                                                    AND c.BuildingId = a.BuildingId
                                                    AND c.IsDel = 0
          WHERE     1 = 1
                    {0}
                    AND a.PremisesId = {1}
                    {2}--AND a.BuildingId = 1
                    {3}--AND a.[Floor] = 1
                    {4}--AND a.UnitId = 1
                    {5}--AND a.SalesStatus = 1
                    AND a.SalesStatus = 2
                    AND a.IsDel = 0
        ) AS z
WHERE   z.rowid BETWEEN {6} AND {7}";

                    sql = string.Format(sql, GetParms_PageList_BySearch_EditHouses_Admin(search, pageIndex, pageSize));

                    var list = db.ExecuteStoreQuery<PVM_NH_SecKill_Edit_Houses>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取总记录数
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_EditHouses_Admin(PVS_NH_SecKill_Edit search)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  COUNT(1) AS Result
FROM    House (NOLOCK) AS a
        INNER JOIN Building (NOLOCK) AS b ON b.Id = a.BuildingId
                                             AND b.IsDel = 0
        LEFT JOIN Unit (NOLOCK) AS c ON c.Num = a.UnitId
                                        AND c.BuildingId = a.BuildingId
                                        AND c.IsDel = 0
WHERE   1 = 1
        {0}
        AND a.PremisesId = {1}
        {2}--AND a.BuildingId = 1
        {3}--AND a.[Floor] = 1
        {4}--AND a.UnitId = 1
        {5}--AND a.SalesStatus = 1
        AND a.SalesStatus = 2
        AND a.IsDel = 0";

                    sql = string.Format(sql, GetParms_PageList_BySearch_EditHouses_Admin(search));

                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取搜索条件
        /// author: liyuzhao
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        private object[] GetParms_PageList_BySearch_EditHouses_Admin(PVS_NH_SecKill_Edit search, int pageIndex = 0, int pageSize = 0)
        {
            var list = new List<object>
            {
                string.Format(@"AND a.Id NOT IN ( SELECT d1.HouseId FROM ActivitiesHouse (NOLOCK) AS d1 LEFT JOIN Activities (NOLOCK) AS d2 ON d2.Id = d1.ActivitiesId AND d2.ActivitieState IN ( 0, 1 ) WHERE d2.Id <> {0} )", search.ActivityId),
                search.PremisesId,
                (0 < search.BuildingId) ? string.Format("AND a.BuildingId = {0}", search.BuildingId) : string.Empty,
                (0 < search.Floor) ? string.Format("AND a.[Floor] = {0}", search.Floor) : string.Empty,
                (0 < search.UnitNum) ? string.Format("AND a.UnitId = {0}", search.UnitNum) : string.Empty,
                (-1 != search.SalesStatus) ? string.Format("AND a.SalesStatus = {0}", search.SalesStatus) : string.Empty,
            };

            if (pageIndex > 0
                && pageSize > 0)
            {
                int startIndex = (pageIndex - 1)*pageSize + 1;
                int endIndex = pageIndex*pageSize;

                list.Add(startIndex);
                list.Add(endIndex);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 保存活动信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public ESqlResult SaveActivityInfo(PVE_NH_SecKill_Edit activity)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
BEGIN TRY
    BEGIN TRAN
    UPDATE  Activities
    SET     BidPrice = {2}, Bond = {3}, BeginTime = '{4}', EndTime = '{5}', ActivitieState = {6}, UpdateTime = GETDATE()
    WHERE   Id = {0}
    UPDATE  ActivitiesHouse
    SET     HouseId = {1}, UpdateTime = GETDATE()
    WHERE   ActivitiesId = {0}
    COMMIT
    SELECT  '1' AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作失败' AS Msg
END CATCH";
                    sql = string.Format(sql, new object[]
                    {
                        activity.ActivityId,
                        activity.HouseId,
                        activity.BidPrice,
                        activity.Bond,
                        activity.BeginTime,
                        activity.EndTime,
                        activity.ActivitieState
                    });

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return list[0];
                    }

                    return new ESqlResult
                    {
                        Code = "0",
                        Msg = "操作失败"
                    };
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", activity, ex);
                return new ESqlResult
                {
                    Code = "0",
                    Msg = "操作失败"
                };
            }
        }

        /// <summary>
        /// 获取活动房源信息（HouseId, PremisesId）
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ActivityHouse> GetActivityHouseInfoList(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  HouseId, PremisesId, CityId
FROM    ActivitiesHouse
WHERE   ActivitiesId = {0}", activityId);

                    var list = db.ExecuteStoreQuery<ActivityHouse>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取指定活动的楼盘信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ActivityHouse> GetActivityPremisesInfoList(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  DISTINCT PremisesId, CityId
FROM    ActivitiesHouse (NOLOCK)
WHERE   ActivitiesId = {0}", activityId);

                    var list = db.ExecuteStoreQuery<ActivityHouse>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }
    }
}