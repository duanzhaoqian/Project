using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXDal.NHouseActivies.Discount
{
    /// <summary>
    /// 折扣 (网站管理平台)
    /// </summary>
    public partial class DiscountDal : BaseDal_Admin
    {
        /// <summary>
        /// 分页搜索显示折扣列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Discount> GetPageList_BySearch_Admin(PVS_NH_Discount search, int pageIndex, int pageSize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  z.Id, z.Name, z.BuildingId, z.BuildingName, z.CompanyName, z.BeginTime, z.EndTime, z.MinDiscount, z.MaxDiscount,
        z.CityId, z.CityName, z.ActivitieState, z.HouseCount, z.PremisesId, z.PremisesName
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.CreateTime DESC ) AS rowid, a.Id, a.Name, b.BuildingId,
                    c.Name AS BuildingName, d.CompanyName, a.BeginTime, a.EndTime,
                    ( SELECT    MinDiscount
                      FROM      ( SELECT    ActivitiesId, MIN(Discount) AS MinDiscount
                                  FROM      ActivitiesHouse (NOLOCK) AS ii1
                                  WHERE     IsDel = 0
                                  GROUP BY  ActivitiesId
                                ) AS i1
                      WHERE     i1.ActivitiesId = a.Id
                    ) AS MinDiscount, ( SELECT  MaxDiscount
                                        FROM    ( SELECT    ActivitiesId, MAX(Discount) AS MaxDiscount
                                                  FROM      ActivitiesHouse (NOLOCK) AS ii1
                                                  WHERE     IsDel = 0
                                                  GROUP BY  ActivitiesId
                                                ) AS i1
                                        WHERE   i1.ActivitiesId = a.Id
                                      ) AS MaxDiscount, e.CityId, e.CityName, a.ActivitieState, a.HouseCount,
                    c.PremisesId, e.Name AS PremisesName
          FROM      Activities (NOLOCK) AS a
                    LEFT JOIN ( SELECT  ActivitiesId, DeveloperId, PremisesId, BuildingId
                                FROM    ActivitiesHouse (NOLOCK)
                                WHERE   IsDel = 0
                                GROUP BY ActivitiesId, DeveloperId, PremisesId, BuildingId
                              ) AS b ON b.ActivitiesId = a.Id
                    INNER JOIN Building (NOLOCK) AS c ON c.Id = b.BuildingId
                                                         AND c.IsDel = 0
                    INNER JOIN Developer_Identity (NOLOCK) AS d ON d.DeveloperId = b.DeveloperId
                                                                   AND d.IsDel = 0
                    INNER JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
                                                         AND e.IsDel = 0
          WHERE     a.[Type] = 2
                    AND a.IsDel = 0
                    {0}
                    {1}
                    {2}
                    {3}
                    {4}
                    {5}
        ) AS z
WHERE   z.rowid BETWEEN {6} AND {7}";
                    sql = string.Format(sql, GetParms_PageList_BySearch_Admin(search, pageIndex, pageSize));

                    var list = houseDb.ExecuteStoreQuery<PVM_NH_Discount>(sql).ToList();

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
        /// 分页搜索显示折扣列表总记录
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Admin(PVS_NH_Discount search)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  COUNT(1) AS Result
FROM    Activities (NOLOCK) AS a
        LEFT JOIN ( SELECT  ActivitiesId, DeveloperId, PremisesId, BuildingId
                    FROM    ActivitiesHouse (NOLOCK)
                    WHERE   IsDel = 0
                    GROUP BY ActivitiesId, DeveloperId, PremisesId, BuildingId
                    ) AS b ON b.ActivitiesId = a.Id
        INNER JOIN Building (NOLOCK) AS c ON c.Id = b.BuildingId
                                             AND c.IsDel = 0
        INNER JOIN Developer_Identity (NOLOCK) AS d ON d.DeveloperId = b.DeveloperId
                                                       AND d.IsDel = 0
        INNER JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
                                             AND e.IsDel = 0
WHERE   a.[Type] = 2
        AND a.IsDel = 0
        {0}
        {1}
        {2}
        {3}
        {4}
        {5}";
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
        /// 搜索条件
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        private object[] GetParms_PageList_BySearch_Admin(PVS_NH_Discount search, int pageIndex = 0, int pageSize = 0)
        {
            var list = new List<object>
            {
                (0 < search.ProvinceId) ? string.Format("AND e.ProvinceID = {0}", search.ProvinceId) : string.Empty,
                (0 < search.CityId) ? string.Format("AND e.CityId = {0}", search.CityId) : string.Empty,
                (0 < search.DeveloperId) ? string.Format("AND b.DeveloperId = {0}", search.DeveloperId) : string.Empty,
                !string.IsNullOrWhiteSpace(search.DeveloperName)?string.Format(" AND CompanyName LIKE '%{0}%'",search.DeveloperName):string.Empty,
                (0 < search.PremisesId) ? string.Format("AND b.PremisesId = {0}", search.PremisesId) : string.Empty,
                //(-1 < search.ActivitieState) ? string.Format("AND a.ActivitieState = {0}", search.ActivitieState) : string.Empty
            };
            if (-1 < search.ActivitieState)
            {
                if (0 == search.ActivitieState)
                {
                    list.Add(string.Format("AND (a.ActivitieState <> 2 AND ( GETDATE() < a.BeginTime ))"));
                }
                else if (1 == search.ActivitieState)
                {
                    list.Add(string.Format("AND (a.ActivitieState <> 2 AND (( a.BeginTime < GETDATE() ) AND ( GETDATE() < a.EndTime )))"));
                }
                else if (2 == search.ActivitieState)
                {
                    list.Add(string.Format("AND ( a.EndTime < GETDATE() )"));
                }
                else
                {
                    list.Add(string.Empty);
                }
            }
            else
            {
                list.Add(string.Empty);
            }

            if (pageIndex > 0
                && pageSize > 0)
            {
                int startIndex = (pageIndex - 1) * pageSize + 1;
                int endIndex = pageIndex * pageSize;

                list.Add(startIndex);
                list.Add(endIndex);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 根据编号删除活动
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <returns></returns>
        public ESqlResult DelDiscountById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
DECLARE @tAid INT
SET @tAid = {0}

BEGIN TRY
    BEGIN TRAN
    UPDATE  Activities
    SET     IsDel = 1
    WHERE   Id = @tAid
    IF ( @@ERROR > 0 ) 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, '删除活动失败' AS Msg
        END

    UPDATE  ActivitiesHouse
    SET     IsDel = 1
    WHERE   ActivitiesId = @tAid
    IF ( @@ERROR > 0 ) 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, '删除活动房源失败' AS Msg
        END
        
    COMMIT TRAN
    SELECT  '1' AS Code, '删除活动房源失败' AS Msg
END TRY
BEGIN CATCH
    SELECT  '0' AS Code, '处理错误' AS Msg
    ROLLBACK TRAN
END CATCH", id);
                    var list = db.ExecuteStoreQuery<ESqlResult>(sql).ToList();
                    return list[0];
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id:{0}", id), ex);
                return new ESqlResult { Code = "-1", Msg = "服务器执行错误" };
            }
        }

        #region History

        //        /// <summary>
        //        /// 根据活动编号获取房源信息列表
        //        /// </summary>
        //        /// <param name="id">活动编号</param>
        //        /// <returns></returns>
        //        public List<PVE_NH_Discount_HousesJson> GetHousesByActivitiesId(int id)
        //        {
        //            try
        //            {
        //                using (var db = new kyj_NewHouseDBEntities())
        //                {
        //                    string sql = string.Format(@"
        //SELECT  Id, DeveloperId, PremisesId, BuildingId, UnitId, HouseId, Discount, 0 AS IsDel, 0 AS IsUpdate
        //FROM    ActivitiesHouse (NOLOCK)
        //WHERE   ActivitiesId = {0}
        //        AND IsDel = 0", id);
        //                    var list = db.ExecuteStoreQuery<PVE_NH_Discount_HousesJson>(sql).ToList();
        //                    return list;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id:{0}", id), ex);
        //                return null;
        //            }
        //        }

        //        /// <summary>
        //        /// 根据限时折扣编辑页面搜索条件获取房源列表
        //        /// </summary>
        //        /// <param name="search">搜索条件</param>
        //        /// <param name="pageIndex">页码</param>
        //        /// <param name="pageSize">页面记录数</param>
        //        /// <returns></returns>
        //        public List<PVE_NH_Discount_Houses> GetHousesInDiscountEdit(PVS_NH_Discount_Houses search, int pageIndex, int pageSize)
        //        {
        //            try
        //            {
        //                using (var db = new kyj_NewHouseDBEntities())
        //                {
        //                    string sql = @"
        //SELECT  HouseId, HouseName, [Floor], BuidingId, BuildingName, Room, Hall, Toilet, Kitchen, BuildingArea, SinglePrice,
        //        TotalPrice, SalesStatus, CreateTime
        //FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.Id ASC ) AS rowid, a.Id AS HouseId, a.Name AS HouseName, a.[Floor],
        //                    b.Id AS BuidingId, b.Name AS BuildingName, a.Room, a.Hall, a.Toilet, a.Kitchen, a.BuildingArea,
        //                    a.SinglePrice, a.TotalPrice, a.SalesStatus, a.CreateTime
        //          FROM      House (NOLOCK) AS a
        //                    INNER JOIN Building (NOLOCK) AS b ON b.Id = a.BuildingId
        //                                                         AND b.IsDel = 0
        //          WHERE     a.IsDel = 0
        //                    {0}
        //                    {1}
        //                    {2}
        //                    {3}
        //                    {4}
        //        ) AS z
        //WHERE   z.rowid BETWEEN {5} AND {6}";

        //                    sql = string.Format(sql, GetParms_PageList_BySearch_HousesInDiscountEdit(search, pageIndex, pageSize));

        //                    var list = db.ExecuteStoreQuery<PVE_NH_Discount_Houses>(sql).ToList();

        //                    return list;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
        //                return null;
        //            }
        //        }

        //        /// <summary>
        //        /// 分页搜索显示折扣列表总记录
        //        /// </summary>
        //        /// <param name="search">搜索条件</param>
        //        /// <returns></returns>
        //        public int GetTotalCount_BySearch_HousesInDiscountEdit(PVS_NH_Discount_Houses search)
        //        {
        //            try
        //            {
        //                using (var db = new kyj_NewHouseDBEntities())
        //                {
        //                    var sql = @"
        //SELECT  COUNT(1) AS Result
        //FROM    House (NOLOCK) AS a
        //        INNER JOIN Building (NOLOCK) AS b ON b.Id = a.BuildingId
        //                                             AND b.IsDel = 0
        //WHERE   a.IsDel = 0
        //        {0}
        //        {1}
        //        {2}
        //        {3}
        //        {4}";
        //                    sql = string.Format(sql, GetParms_PageList_BySearch_HousesInDiscountEdit(search));

        //                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

        //                    if (0 < list.Count)
        //                    {
        //                        return Convert.ToInt32(list[0].Result);
        //                    }

        //                    return 0;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
        //                return 0;
        //            }
        //        }

        //        /// <summary>
        //        /// 获取搜索条件列表
        //        /// </summary>
        //        /// <param name="search"></param>
        //        /// <param name="pageIndex"></param>
        //        /// <param name="pageSize"></param>
        //        /// <returns></returns>
        //        private object[] GetParms_PageList_BySearch_HousesInDiscountEdit(PVS_NH_Discount_Houses search, int pageIndex = 0, int pageSize = 0)
        //        {
        //            var list = new List<object>
        //            {
        //                string.Format("AND a.PremisesId = {0}", search.PremisesId),
        //                (0 < search.BuildingId) ? string.Format("AND a.BuildingId = {0}", search.BuildingId) : string.Empty,
        //                (0 < search.UnitId) ? string.Format("AND a.UnitId = {0}", search.UnitId) : string.Empty,
        //                (0 < search.Floor) ? string.Format("AND a.Floor = {0}", search.Floor) : string.Empty,
        //                (0 < search.SalesStatus) ? string.Format("AND a.SalesStatus = {0}", search.SalesStatus) : string.Empty,
        //            };

        //            if (pageIndex > 0
        //                && pageSize > 0)
        //            {
        //                int startIndex = (pageIndex - 1)*pageSize + 1;
        //                int endIndex = pageIndex*pageSize;

        //                list.Add(startIndex);
        //                list.Add(endIndex);
        //            }

        //            return list.ToArray();
        //        }

        #endregion

        #region 修改限时折扣房源列表

        /// <summary>
        /// 获取修改限时折扣房源列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Discount_Edit1> GetHousesInDiscountEdit1(PVS_NH_Discount_Houses_Edit1 search, int pageIndex, int pageSize)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = @"
SELECT  z.HouseId, z.HouseName, z.[Floor], z.UnitId, z.UnitName, z.BuildingId, z.BuildingName, z.BuildingNameType,
        z.Room, z.Hall, z.Toilet, z.Kitchen, z.BuildingArea, z.SinglePrice, z.TotalPrice, z.SalesStatus, z.CreateTime
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.Id ASC ) AS rowid, a.Id AS HouseId, a.Name AS HouseName, a.[Floor],
                    a.UnitId, c.Name AS UnitName, a.BuildingId, b.Name AS BuildingName, b.NameType AS BuildingNameType,
                    a.Room, a.Hall, a.Toilet, a.Kitchen, a.BuildingArea, a.SinglePrice, a.TotalPrice, a.SalesStatus,
                    a.CreateTime
          FROM      House (NOLOCK) AS a
                    LEFT JOIN Building (NOLOCK) AS b ON b.Id = a.BuildingId
                                                        AND b.IsDel = 0
                    LEFT JOIN Unit (NOLOCK) AS c ON c.Num = a.UnitId
                                                    AND c.IsDel = 0
                                                    AND c.PremisesId = a.PremisesId
                                                    AND c.BuildingId = b.Id
          WHERE     a.IsDel = 0
                    AND a.SalesStatus = 2
                    {0}
                    {1}
                    {2}
                    {3}
                    {4}
        ) AS z
WHERE   rowid BETWEEN {5} AND {6}";
                    sql = string.Format(sql, GetParms_PageList_BySearch_HousesInDiscountEdit1(search, pageIndex, pageSize));

                    var list = db.ExecuteStoreQuery<PVM_NH_Discount_Edit1>(sql).ToList();

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
        /// 获取修改限时折扣房源列表总记录数
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_GetHousesInDiscountEdit1(PVS_NH_Discount_Houses_Edit1 search)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = @"
SELECT  COUNT(1) AS Result
FROM    House (NOLOCK) AS a
        LEFT JOIN Building (NOLOCK) AS b ON b.Id = a.BuildingId
                                            AND b.IsDel = 0
        LEFT JOIN Unit (NOLOCK) AS c ON c.Num = a.UnitId
                                        AND c.IsDel = 0
                                        AND c.PremisesId = a.PremisesId
                                        AND c.BuildingId = b.Id
WHERE   a.IsDel = 0
        AND a.SalesStatus = 2
        {0}
        {1}
        {2}
        {3}
        {4}";
                    sql = string.Format(sql, GetParms_PageList_BySearch_HousesInDiscountEdit1(search));

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
        /// 获取修改限时折扣房源列表条件参数
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        private object[] GetParms_PageList_BySearch_HousesInDiscountEdit1(PVS_NH_Discount_Houses_Edit1 search, int pageIndex = 0, int pageSize = 0)
        {
            var list = new List<object>
            {
                string.Format("AND a.PremisesId = {0}", search.PremisesId),
                (0 < search.BuildingId) ? string.Format("AND a.BuildingId = {0}", search.BuildingId) : string.Empty,
                (0 < search.UnitId) ? string.Format("AND a.UnitId = {0}", search.UnitId) : string.Empty,
                (0 != search.Floor) ? string.Format("AND a.Floor = {0}", search.Floor) : string.Empty,
                (0 < search.SalesStatus) ? string.Format("AND a.SalesStatus = {0}", search.SalesStatus) : string.Empty
            };

            if (pageIndex > 0
                && pageSize > 0)
            {
                int startIndex = (pageIndex - 1) * pageSize + 1;
                int endIndex = pageIndex * pageSize;

                list.Add(startIndex);
                list.Add(endIndex);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取参与活动的房源列表
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<ActivitiesHouse> GetHousesJoinDiscount(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.ActivitiesHouses.Where(it => it.ActivitiesId == activityId && it.IsDel == false).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取选取参加活动的房源信息
        /// </summary>
        /// <param name="activitiesId">活动编号</param>
        /// <param name="houseIds">房源编号集合(逗号分隔)</param>
        /// <returns></returns>
        public List<PVM_NH_Discount_Edit2> GetHousesDiscountInDiscountEdit(string activitiesId, string houseIds)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  a.Id AS HouseId, a.Name AS HouseName, a.[Floor], a.BuildingId, b.Name AS BuildingName,
        b.NameType AS BuildingNameType, a.Room, a.Hall, a.Toilet, a.Kitchen, a.BuildingArea, a.TotalPrice,
        ISNULL(( SELECT Discount
                 FROM   ActivitiesHouse (NOLOCK) AS i1
                 WHERE  i1.IsDel = 0
                        AND i1.ActivitiesId = {0}
                        AND i1.HouseId = a.Id
               ), -1) AS Discount
FROM    House (NOLOCK) AS a
        LEFT JOIN Building (NOLOCK) AS b ON b.Id = a.BuildingId
                                            AND b.IsDel = 0
WHERE   a.Id IN ( {1} )
        AND a.IsDel = 0";

                    sql = string.Format(sql, activitiesId, houseIds);

                    var list = db.ExecuteStoreQuery<PVM_NH_Discount_Edit2>(sql).ToList();

                    return list;

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activitiesId:{0}, houseIds:{1}", activitiesId, houseIds), ex);
                return null;
            }
        }

        #endregion

        #region 修改 限时折扣

        public int UpdateDiscount_Admin_UpdateActivities(PVE_NH_Discount discount)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
UPDATE  Activities
SET     Name = @Name, HouseCount = @HouseCount, Bond = @Bond, BeginTime = @BeginTime, EndTime = @EndTime,
        UpdateTime = GETDATE()
WHERE   Id = @Id";
                    var parms = new object[]
                    {
                        new SqlParameter("@Id", discount.Id),
                        new SqlParameter("@Name", discount.Name),
                        new SqlParameter("@HouseCount", discount.DiscountHouses.Count),
                        new SqlParameter("@Bond", discount.Bond),
                        new SqlParameter("@BeginTime", discount.BeginTime),
                        new SqlParameter("@EndTime", string.Format("{0:yyyy-MM-dd 23:59:59}", discount.EndTime))
                    };

                    var count = db.ExecuteStoreCommand(sql, parms);

                    return count;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", discount, ex);
                throw;
            }
        }

        public int UpdateDiscount_Admin_UpdateActivityHouse(PVE_NH_Discount discount)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    for (int i = 0; i < discount.DiscountHouses.Count; i++)
                    {
                        var house = discount.DiscountHouses[i];
                        if (house.IsOriData == 1)
                        {
                            // 删除原有活动房源只打标记
                            if (house.IsDel == 1)
                            {
                                sql.AppendFormat(@"
 UPDATE  ActivitiesHouse
SET     IsDel = 1, Discount = {2}, UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}
        AND HouseId = {1};", discount.Id, house.HouseId, house.Discount);
                            }
                            else
                            {
                                sql.AppendFormat(@"
 UPDATE  ActivitiesHouse
SET     Discount = {2}, UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}
        AND HouseId = {1};", discount.Id, house.HouseId, house.Discount);
                            }
                        }
                        else
                        {
                            sql.AppendFormat(@"
IF NOT EXISTS ( SELECT  1
                FROM    ActivitiesHouse (NOLOCK)
                WHERE   ActivitiesId = {0}
                        AND HouseId = {2} ) 
    BEGIN
        INSERT  INTO ActivitiesHouse ( ActivitiesId, CityId, DeveloperId, PremisesId, BuildingId, UnitId, HouseId,
                                       Discount, UpdateTime, CreateTime, IsDel )
                SELECT  {0}, b.CityId, a.DeveloperId, a.PremisesId, a.BuildingId, a.UnitId, a.Id, {1}, GETDATE(), GETDATE(), 0
                FROM    House (NOLOCK) AS a
                        LEFT JOIN Premises (NOLOCK) AS b ON b.Id = a.PremisesId
                WHERE   a.Id = {2}        
    END
ELSE 
    BEGIN
        UPDATE  ActivitiesHouse
        SET     IsDel = 0, Discount = {1}, UpdateTime = GETDATE()
        WHERE   ActivitiesId = {0}
                AND HouseId = {2}
    END;", discount.Id, house.Discount, house.HouseId);
                        }
                    }

                    var count = db.ExecuteStoreCommand(sql.ToString());

                    return count;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", discount, ex);
                throw;
            }
        }

        #endregion

        #region 详情

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PVE_NH_Discount_Detail GetDiscountDetail(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    Activities (NOLOCK)
WHERE   Id = {0}", id);

                    var list = db.ExecuteStoreQuery<PVE_NH_Discount_Detail>(sql).ToList();

                    if (list.Any())
                    {
                        return list[0];
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", id, ex);
                throw;
            }
        }

        /// <summary>
        /// 活动详情 房源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PVE_NH_Discount_Houses> GetDiscountDetail_Houses(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  a.Discount, b.Id AS HouseId, b.Name AS HouseName, b.[Floor], b.BuildingId, d.Name AS BuildingName, b.Room,
        b.Hall, b.Toilet, b.Kitchen, b.BuildingArea, b.SinglePrice, b.TotalPrice, b.SalesStatus, c.Name AS UnitName, e.Name AS PremiseName
FROM    ActivitiesHouse (NOLOCK) AS a
        LEFT JOIN House (NOLOCK) AS b ON b.Id = a.HouseId
        LEFT JOIN Unit (NOLOCK) AS c ON c.Num = b.UnitId
                                        AND c.PremisesId = b.PremisesId
        LEFT JOIN Building (NOLOCK) AS d ON d.Id = b.BuildingId
        LEFT JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
WHERE   a.ActivitiesId = {0}
        AND a.IsDel = 0", id);

                    var list = db.ExecuteStoreQuery<PVE_NH_Discount_Houses>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", id, ex);
                throw;
            }
        }

        #endregion

        #region 用户报名列表

        /// <summary>
        /// 用户报名列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildingId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<PVM_NH_Discount_UserReport> GetDiscountUserReports(int id, int buildingId, int unitId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  a.*, b.Name AS HouseName, c.Name AS UnitName, d.Name AS PremiseName, b.[Floor], a.CreateTime AS BidTime, e.Name + e.NameType AS BuildingName
FROM    BidPrice (NOLOCK) AS a
        LEFT JOIN House (NOLOCK) AS b ON b.Id = a.HouseId
        LEFT JOIN Unit (NOLOCK) AS c ON c.Num = b.UnitId
                                        AND c.PremisesId = b.PremisesId
        LEFT JOIN Premises (NOLOCK) AS d ON d.Id = b.PremisesId
        LEFT JOIN Building (NOLOCK) AS e ON e.Id=b.BuildingId
WHERE   ActivitiesId = {0}
        AND a.IsDel = 0", id, unitId, buildingId);

                    if (0 < unitId)
                    {
                        sql += string.Format(@"
        AND c.Num = {0}", unitId);
                    }
                    if (0 < buildingId)
                    {
                        sql += string.Format(@"
        AND b.BuildingId = {0}", buildingId);
                    }

                    var list = db.ExecuteStoreQuery<PVM_NH_Discount_UserReport>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", id, ex);
                throw;
            }
        }

        #endregion
    }
}