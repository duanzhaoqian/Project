using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Log4netService;
using TXModel.AdminPVM;
using TXOrm;

namespace TXDal.NHouseActivies.TuanGou
{
    public class TuanGou_AdminDal : BaseDal_Admin
    {
        #region 阶梯团购活动信息列表

        /// <summary>
        ///     阶梯团购活动信息列表
        /// </summary>
        /// <param name="search">搜索实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_LadderBuy> GetLadderBuyListByPages(PVS_NH_LadderBuy search, int pageIndex, int pageSize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                       SELECT * FROM (
                                       SELECT  ROW_NUMBER() OVER ( ORDER BY active.CreateTime DESC ) AS RowID,
                                         active.Id,
                                         active.Type,
				                         active.Name,
                                         active.BeginTime, 
                                         active.EndTime,
                                         active.DeveloperId,
                                         active.HouseCount,
                                         active.ActivitieState,

                                         premise.Id AS PremisesId,
                                         premise.Name AS PremisesName,
                                         premise.ProvinceId,
                                         premise.ProvinceName,
                                         premise.CityId,
                                         premise.CityName,
                                         
                                         --deve.CompanyName AS DeveloperName
                                         premise.Developer AS DeveloperName

				                         FROM Activities (NOLOCK) as active  
                                         LEFT JOIN  ( SELECT  ActivitiesId, DeveloperId, PremisesId
                                                        FROM    ActivitiesHouse (NOLOCK)
                                                        GROUP BY ActivitiesId, DeveloperId, PremisesId
                                                      ) AS activehouse ON activehouse.ActivitiesId = active.Id
                                         INNER JOIN Premises (NOLOCK) AS premise ON premise.Id = activehouse.PremisesId                      
                                         INNER JOIN Developer_Identity (NOLOCK) AS deve ON deve.DeveloperId = activehouse.DeveloperId
                  
				                         WHERE active.IsDel=0 AND premise.IsDel=0 AND deve.IsDel=0  {0}

                                    ) HOSE  
			    WHERE   HOSE.RowID BETWEEN {1} AND {2} ", new object[]
                    {
                        GetSearchSqlMess(search),
                        ((pageIndex - 1)*pageSize) + 1,
                        pageIndex*pageSize
                    });

                    #region 执行操作

                    ObjectResult<PVM_NH_LadderBuy> query = houseDb.ExecuteStoreQuery<PVM_NH_LadderBuy>(sql);
                    List<PVM_NH_LadderBuy> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("王志坤", string.Format("({0},{1},{2})", new object[] {search, pageIndex, pageSize}), ex);
                throw;
            }
        }

        #endregion

        #region 阶梯团购活动信息总记录数

        /// <summary>
        ///     阶梯团购活动信息总记录数
        ///     Author:wangzhikun
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetLadderBuyListByPageCounts(PVS_NH_LadderBuy search)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT  COUNT(1) AS Result FROM  Activities (NOLOCK) AS active
                                        LEFT JOIN ( SELECT  ActivitiesId, DeveloperId, PremisesId
                                                        FROM    ActivitiesHouse (NOLOCK)
                                                        GROUP BY ActivitiesId, DeveloperId, PremisesId
                                                      ) AS activehouse ON activehouse.ActivitiesId = active.Id
                                        INNER JOIN Premises (NOLOCK) AS premise ON premise.Id = activehouse.PremisesId
                                        INNER JOIN Developer_Identity (NOLOCK) AS deve ON deve.DeveloperId = activehouse.DeveloperId
            
			                        WHERE active.IsDel=0 AND premise.IsDel=0 AND deve.IsDel=0  {0}                                       
                                    ", new object[]
                    {
                        GetSearchSqlMess(search)
                    });

                    #region 执行操作

                    try
                    {
                        ObjectResult<SqlResult_ScalarInt> query = houseDb.ExecuteStoreQuery<SqlResult_ScalarInt>(sql);
                        List<SqlResult_ScalarInt> resultlist = query.ToList();
                        if (resultlist.Any())
                            return Convert.ToInt32(resultlist[0].Result);
                        return 0;
                    }
                    catch
                    {
                        return 0;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("王志坤", string.Format("({0})", new object[] {search}), ex);
                throw;
            }
        }

        #endregion

        #region 获取查询阶梯团购活动列表sqlwhere语句

        /// <summary>
        ///     获取查询阶梯团购活动列表sqlwhere语句
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public string GetSearchSqlMess(PVS_NH_LadderBuy search)
        {
            var whereStr = new StringBuilder();

            #region 过滤数据

            //根据活动类型搜索(1摇号 2限时折扣 3排号购房 4阶梯团购 5竞价 6砍价 7秒杀 8一口价)
            switch (search.Type)
            {
                case 3:
                    whereStr.AppendFormat(" AND active.Type={0}", search.Type);
                    break;
                case 4:
                    whereStr.AppendFormat(" AND active.Type={0}", search.Type);
                    break;
            }

            //根据省份搜索
            if (search.ProvinceID > 0)
            {
                whereStr.AppendFormat(" AND premise.ProvinceId={0}", search.ProvinceID);
            }
            //根据城市搜索
            if (search.CityId > 0)
            {
                whereStr.AppendFormat(" AND premise.CityId={0}", search.CityId);
            }
            //根据楼盘进行搜索
            if (search.PremisesId > 0)
            {
                whereStr.AppendFormat(" AND premise.Id={0}", search.PremisesId);
            }

            //根据活动状态搜索
            if (search.ActivitieState != -1)
            {
                whereStr.AppendFormat(" AND active.ActivitieState={0}", search.ActivitieState);
            }

            //根据开发商名称模糊查询
            if (!string.IsNullOrWhiteSpace(search.DeveloperName))
            {
                whereStr.AppendFormat(@"AND ( premise.Developer LIKE '{0}%' OR premise.Agent LIKE '{0}%' )", search.DeveloperName);
            }

            #endregion

            return whereStr.ToString();
        }

        /// <summary>
        ///     SQL执行返回首行首列(整型)
        ///     Author:wangzhikun
        /// </summary>
        private class SqlResult_ScalarInt
        {
            public int? Result { set; get; }
        }

        #endregion

        #region 获取排号购房报名用户列表

        /// <summary>
        ///     获取阶梯团购报名用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_RegistrationUser> GetUsersListByPages(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
//                    string sql = string.Format(@"
//                       SELECT * FROM (
//                                       SELECT  ROW_NUMBER() OVER ( ORDER BY activeUser.CreateTime ASC ) AS RowID, activeUser.Id, active.Type,
//                    bid.BidUserName, activeUser.BidRealName AS RealName, activeUser.IDCard, bid.BidUserMobile AS Mobile,
//                    bid.BidUserQQ, bid.BidUserMail, premise.Name AS PremiseName, build.Name AS BuildingName,
//                    units.Name AS UnitName, house.Floor, house.Name AS HouseNum, activeUser.CreateTime
//				                         FROM BidPrice (NOLOCK) as activeUser  
//                                         LEFT JOIN  ActivitiesHouse (NOLOCK) AS activehouse ON activehouse.ActivitiesId = activeUser.ActivitiesId
//                                         LEFT JOIN  Activities (NOLOCK) AS active ON active.Id = activeUser.ActivitiesId
//                                         LEFT JOIN BidPrice (NOLOCK) AS bid ON bid.ActivitiesId = activeUser.ActivitiesId                      
//                                         LEFT JOIN Premises (NOLOCK) AS premise ON premise.Id = activehouse.PremisesId
//                                         LEFT JOIN Building (NOLOCK) AS build ON build.Id = activehouse.BuildingId
//                                         LEFT JOIN House (NOLOCK) AS house ON house.Id = activehouse.HouseId
//                                         LEFT JOIN Unit (NOLOCK) AS units ON units.Num = house.UnitId
//                                                        AND units.BuildingId = house.BuildingId
//                  
//				                         WHERE activeUser.IsDel=0 AND activehouse.IsDel=0  {0} 
//
//                                    ) HOSE  
//			    WHERE   HOSE.RowID BETWEEN {1} AND {2} 
//                ORDER BY CreateTime ASC", new object[]
//                    {
//                        GetRegistUsersSqlMess(search),
//                        ((pageIndex - 1)*pageSize) + 1,
//                        pageIndex*pageSize
//                    });

                    string sql = string.Format(@"
SELECT  *
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.CreateTime ASC ) AS rowid, a.Id, g.[Type], a.BidUserName,
                    a.BidRealName AS RealName, a.IDCard, a.BidUserMobile AS Mobile, a.BidUserQQ, a.BidUserMail,
                    e.Name AS PremiseName, d.Name AS BuildingName, f.Name AS UnitName, c.Floor, c.Name AS HouseNum,
                    a.CreateTime, ROW_NUMBER() OVER ( ORDER BY a.CreateTime ASC ) AS RegNum
          FROM      BidPrice (NOLOCK) AS a
                    LEFT JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
                    LEFT JOIN House (NOLOCK) AS c ON c.Id = b.HouseId
                    LEFT JOIN Building (NOLOCK) AS d ON d.Id = b.BuildingId
                    LEFT JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
                    LEFT JOIN Unit (NOLOCK) f ON f.Num = b.UnitId
                                                 AND f.BuildingId = b.BuildingId
                    LEFT JOIN Activities (NOLOCK) AS g ON g.Id = a.ActivitiesId
          WHERE     a.IsDel = 0
                    AND g.IsDel = 0
                    {0}
        ) AS tab
WHERE   rowid BETWEEN {1} AND {2}", new object[] {GetRegistUsersSqlMess(search), ((pageIndex - 1)*pageSize) + 1, pageIndex*pageSize});

                    #region 执行操作

                    ObjectResult<PVM_NH_RegistrationUser> query = houseDb.ExecuteStoreQuery<PVM_NH_RegistrationUser>(sql);
                    List<PVM_NH_RegistrationUser> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("王志坤", string.Format("({0},{1},{2})", new object[] {search, pageIndex, pageSize}), ex);
                throw;
            }
        }

        #endregion

        #region 获取排号购房报名用户信息总记录数

        /// <summary>
        ///     获取阶梯团购报名用户信息总记录数
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetUsersListByPageCounts(PVS_NH_RegistrationUser search)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  COUNT(1) AS Result
FROM    BidPrice (NOLOCK) AS a
        LEFT JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
        LEFT JOIN House (NOLOCK) AS c ON c.Id = b.HouseId
        LEFT JOIN Building (NOLOCK) AS d ON d.Id = b.BuildingId
        LEFT JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
        LEFT JOIN Unit (NOLOCK) f ON f.Num = b.UnitId
                                     AND f.BuildingId = b.BuildingId
        LEFT JOIN Activities (NOLOCK) AS g ON g.Id = a.ActivitiesId
WHERE   a.IsDel = 0
        AND g.IsDel = 0
        {0}", new object[]
                    {
                        GetRegistUsersSqlMess(search)
                    });

                    #region 执行操作

                    try
                    {
                        ObjectResult<SqlResult_ScalarInt> query = houseDb.ExecuteStoreQuery<SqlResult_ScalarInt>(sql);
                        List<SqlResult_ScalarInt> resultlist = query.ToList();
                        if (resultlist.Any())
                            return Convert.ToInt32(resultlist[0].Result);
                        return 0;
                    }
                    catch
                    {
                        return 0;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("王志坤", string.Format("({0})", new object[] {search}), ex);
                throw;
            }
        }

        #endregion

        #region 获取排号购房报名用户列表sqlwhere语句

        /// <summary>
        ///     获取阶梯团购报名用户列表sqlwhere语句
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public string GetRegistUsersSqlMess(PVS_NH_RegistrationUser search)
        {
            var whereStr = new StringBuilder();

            #region 过滤数据

            //根据活动类型搜索      
            if (search.Type > 0)
            {
                whereStr.AppendFormat(" AND g.Type={0}", search.Type);
            }

            //根据活动编号搜索      
            if (search.Id > 0)
            {
                whereStr.AppendFormat(" AND a.ActivitiesId={0}", search.Id);
            }

            #endregion

            return whereStr.ToString();
        }

        #endregion

        #region 阶梯团购

        public List<PVM_NH_RegistrationUser> GetUsersListByPages_TuanGou(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.CreateTime DESC ) AS rowid, a.Id, g.[Type], a.BidUserName,
                    a.BidRealName AS RealName, a.IDCard, a.BidUserMobile AS Mobile, a.BidUserQQ, a.BidUserMail,
                    e.Name AS PremiseName, d.Name + d.NameType AS BuildingName, f.Name AS UnitName, c.Floor, c.Name AS HouseNum,
                    a.CreateTime, ROW_NUMBER() OVER ( ORDER BY a.CreateTime ASC ) AS RegNum
          FROM      BidPrice (NOLOCK) AS a
                    LEFT JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
                    LEFT JOIN House (NOLOCK) AS c ON c.Id = b.HouseId
                    LEFT JOIN Building (NOLOCK) AS d ON d.Id = b.BuildingId
                    LEFT JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
                    LEFT JOIN Unit (NOLOCK) f ON f.Num = b.UnitId
                                                 AND f.BuildingId = b.BuildingId
                    LEFT JOIN Activities (NOLOCK) AS g ON g.Id = a.ActivitiesId
          WHERE     a.IsDel = 0
                    AND g.IsDel = 0
                    {0}
        ) AS tab
WHERE   rowid BETWEEN {1} AND {2}", new object[] { GetRegistUsersSqlMess_TuanGou(search), ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize });

                    #region 执行操作

                    ObjectResult<PVM_NH_RegistrationUser> query = houseDb.ExecuteStoreQuery<PVM_NH_RegistrationUser>(sql);
                    List<PVM_NH_RegistrationUser> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("王志坤", string.Format("({0},{1},{2})", new object[] { search, pageIndex, pageSize }), ex);
                throw;
            }
        }

        public int GetUsersListByPageCounts_TuanGou(PVS_NH_RegistrationUser search)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  COUNT(1) AS Result
FROM    BidPrice (NOLOCK) AS a
        LEFT JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
        LEFT JOIN House (NOLOCK) AS c ON c.Id = b.HouseId
        LEFT JOIN Building (NOLOCK) AS d ON d.Id = b.BuildingId
        LEFT JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
        LEFT JOIN Unit (NOLOCK) f ON f.Num = b.UnitId
                                     AND f.BuildingId = b.BuildingId
        LEFT JOIN Activities (NOLOCK) AS g ON g.Id = a.ActivitiesId
WHERE   a.IsDel = 0
        AND g.IsDel = 0
        {0}", new object[]
                    {
                        GetRegistUsersSqlMess_TuanGou(search)
                    });

                    #region 执行操作

                    try
                    {
                        ObjectResult<SqlResult_ScalarInt> query = houseDb.ExecuteStoreQuery<SqlResult_ScalarInt>(sql);
                        List<SqlResult_ScalarInt> resultlist = query.ToList();
                        if (resultlist.Any())
                            return Convert.ToInt32(resultlist[0].Result);
                        return 0;
                    }
                    catch
                    {
                        return 0;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("王志坤", string.Format("({0})", new object[] { search }), ex);
                throw;
            }
        }

        public string GetRegistUsersSqlMess_TuanGou(PVS_NH_RegistrationUser search)
        {
            var whereStr = new StringBuilder();

            #region 过滤数据

            //根据活动类型搜索      
            if (search.Type > 0)
            {
                whereStr.AppendFormat(" AND g.Type={0}", search.Type);
            }

            //根据活动编号搜索      
            if (search.Id > 0)
            {
                whereStr.AppendFormat(" AND a.ActivitiesId={0}", search.Id);
            }

            #endregion

            return whereStr.ToString();
        }

        public List<PVM_NH_RegistrationUser> GetOutPutRegistUsersByList_TuanGou(int id)
        {
            try
            {
                using (var kyjEnt = new kyj_NewHouseDBEntities())
                {
                    #region sql 参数

                    string sql = string.Format(@"
SELECT  ROW_NUMBER() OVER ( ORDER BY a.CreateTime DESC ) AS rowid, a.Id, g.[Type], a.BidUserName,
        a.BidRealName AS RealName, a.IDCard, a.BidUserMobile AS Mobile, a.BidUserQQ, a.BidUserMail,
        e.Name AS PremiseName, d.Name + d.NameType AS BuildingName, f.Name AS UnitName, c.Floor, c.Name AS HouseNum,
        a.CreateTime, ROW_NUMBER() OVER ( ORDER BY a.CreateTime ASC ) AS RegNum
FROM    BidPrice (NOLOCK) AS a
        LEFT JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
        LEFT JOIN House (NOLOCK) AS c ON c.Id = b.HouseId
        LEFT JOIN Building (NOLOCK) AS d ON d.Id = b.BuildingId
        LEFT JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
        LEFT JOIN Unit (NOLOCK) f ON f.Num = b.UnitId
                                     AND f.BuildingId = b.BuildingId
        LEFT JOIN Activities (NOLOCK) AS g ON g.Id = a.ActivitiesId
WHERE   a.IsDel = 0
        AND g.IsDel = 0
        AND a.ActivitiesId = {0}", id);

                    #endregion

                    #region 执行操作

                    ObjectResult<PVM_NH_RegistrationUser> query = kyjEnt.ExecuteStoreQuery<PVM_NH_RegistrationUser>(sql);
                    List<PVM_NH_RegistrationUser> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception e)
            {
                RecordLog.RecordException("wangzhikun", "导出列表", e);
                throw;
            }
        }

        #endregion

        #region 导出报名用户列表

        /// <summary>
        ///     导出报名用户列表
        /// </summary>
        /// <returns></returns>
        public List<PVM_NH_RegistrationUser> GetOutPutRegistUsersByList(int id)
        {
            try
            {
                using (var kyjEnt = new kyj_NewHouseDBEntities())
                {
                    #region sql 参数

                    string sql = string.Format(@"
SELECT  ROW_NUMBER() OVER ( ORDER BY a.CreateTime ASC ) AS rowid, a.Id, g.[Type], a.BidUserName,
        a.BidRealName AS RealName, a.IDCard, a.BidUserMobile AS Mobile, a.BidUserQQ, a.BidUserMail,
        e.Name AS PremiseName, d.Name + d.NameType AS BuildingName, f.Name AS UnitName, c.Floor, c.Name AS HouseNum,
        a.CreateTime, ROW_NUMBER() OVER ( ORDER BY a.CreateTime ASC ) AS RegNum
FROM    BidPrice (NOLOCK) AS a
        LEFT JOIN ActivitiesHouse (NOLOCK) AS b ON b.ActivitiesId = a.Id
        LEFT JOIN House (NOLOCK) AS c ON c.Id = b.HouseId
        LEFT JOIN Building (NOLOCK) AS d ON d.Id = b.BuildingId
        LEFT JOIN Premises (NOLOCK) AS e ON e.Id = b.PremisesId
        LEFT JOIN Unit (NOLOCK) f ON f.Num = b.UnitId
                                     AND f.BuildingId = b.BuildingId
        LEFT JOIN Activities (NOLOCK) AS g ON g.Id = a.ActivitiesId
WHERE   a.IsDel = 0
        AND g.IsDel = 0
        AND a.ActivitiesId = {0}", id);

                    #endregion

                    #region 执行操作

                    ObjectResult<PVM_NH_RegistrationUser> query = kyjEnt.ExecuteStoreQuery<PVM_NH_RegistrationUser>(sql);
                    List<PVM_NH_RegistrationUser> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception e)
            {
                RecordLog.RecordException("wangzhikun", "导出列表", e);
                throw;
            }
        }

        #endregion

        #region 活动详情列表

        /// <summary>
        ///     活动详情列表
        /// </summary>
        /// <param name="activeId">活动Id</param>
        /// <returns></returns>
        public List<PVM_NH_LadderBuyDetail> GetLadderBuyByList(int activeId)
        {
            try
            {
                using (var kyjEnt = new kyj_NewHouseDBEntities())
                {
                    #region sql 参数

                    string sql = string.Format(@"SELECT * FROM (
                            SELECT  ROW_NUMBER() OVER ( ORDER BY active.CreateTime DESC ) AS RowID,
                                         active.Id,
                                         active.Name AS ActionName,
                                         active.Bond,
                                         active.UserCount,
                                         active.BeginTime,
                                         active.EndTime,     
                                         activehouse.Discount,
                                         premise.Name AS PremiseName,
                                         build.Name AS BuildName,                                        
                                         house.Name AS HouseNum,
                                         house.Floor,
                                         house.RId AS RName,
                                         house.Hall,
                                         house.Toilet,
                                         house.Kitchen,
                                         house.BuildingArea,
                                         house.SinglePrice,
                                         house.TotalPrice,
                                         house.SalesStatus,
                                         house.CreateTime,
                                         units.Name AS UnitName


				                         FROM Activities (NOLOCK) as active 
                                         LEFT JOIN  ActivitiesHouse (NOLOCK) AS activehouse ON activehouse.ActivitiesId = active.Id                                                    
                                         LEFT JOIN Premises (NOLOCK) AS premise ON premise.Id = activehouse.PremisesId
                                         LEFT JOIN Building (NOLOCK) AS build ON build.Id = activehouse.BuildingId
                                         LEFT JOIN House (NOLOCK) AS house ON house.Id = activehouse.HouseId
                                         LEFT JOIN Unit (NOLOCK) AS units ON units.Id = house.UnitId

                                         WHERE active.IsDel=0 AND activehouse.IsDel=0 AND active={0}

                                ) HOSE ", new object[]
                    {
                        activeId
                    });

                    #endregion

                    #region 执行操作

                    ObjectResult<PVM_NH_LadderBuyDetail> query = kyjEnt.ExecuteStoreQuery<PVM_NH_LadderBuyDetail>(sql);
                    List<PVM_NH_LadderBuyDetail> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception e)
            {
                RecordLog.RecordException("wangzhikun", "活动详情", e);
                throw;
            }
        }

        #endregion

        #region 列表总记录数

        /// <summary>
        ///     列表总记录数
        /// </summary>
        /// <param name="activeId">活动Id</param>
        /// <returns></returns>
        public int GetLadderBuyByPageCounts(int activeId)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT  COUNT(1) AS Result FROM Activities (NOLOCK) as active 
                                         LEFT JOIN  ActivitiesHouse (NOLOCK) AS activehouse ON activehouse.ActivitiesId = active.Id                                                    
                                         LEFT JOIN Premises (NOLOCK) AS premise ON premise.Id = activehouse.PremisesId
                                         LEFT JOIN Building (NOLOCK) AS build ON build.Id = activehouse.BuildingId
                                         LEFT JOIN House (NOLOCK) AS house ON house.Id = activehouse.HouseId
                                         LEFT JOIN Unit (NOLOCK) AS units ON units.Id = house.UnitId

                                         WHERE active.IsDel=0 AND activehouse.IsDel=0 AND active={0} ", new object[] {activeId});

                    #region 执行操作

                    try
                    {
                        ObjectResult<SqlResult_ScalarInt> query = houseDb.ExecuteStoreQuery<SqlResult_ScalarInt>(sql);
                        List<SqlResult_ScalarInt> resultlist = query.ToList();
                        if (resultlist.Any())
                            return Convert.ToInt32(resultlist[0].Result);
                        return 0;
                    }
                    catch
                    {
                        return 0;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("wangzhikun", "活动详情", ex);
                throw;
            }
        }

        #endregion

        #region 删除活动

        /// <summary>
        ///     删除活动(团购、排号)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelTuanGou(int id)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
UPDATE  Activities
SET     IsDel = 1, UpdateTime = GETDATE()
WHERE   Id = {0}

UPDATE  ActivitiesHouse
SET     IsDel = 1, UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}", new object[] {id});

                    int count = houseDb.ExecuteStoreCommand(sql);

                    return count;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("wangzhikun", "活动详情", ex);
                throw;
            }
        }

        public int DelTuanGou_Social(int id)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
UPDATE  Social
SET     IsDel = 1, UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}", new object[] { id });

                    int count = houseDb.ExecuteStoreCommand(sql);

                    return count;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("李雨钊", "活动详情", ex);
                throw;
            }
        }

        #endregion

        #region 排号购房房源

        /// <summary>
        ///     获取搜索记录
        ///     author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_Purchase_Edit2> GetPageList_BySearch_EditPurchaseHouses_Admin(PVS_NH_Purchase_Edit search, int pageIndex = 0, int pageSize = 0)
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
                    --{5}--AND a.SalesStatus = 1
                    AND a.SalesStatus = 2
                    AND a.IsDel = 0
        ) AS z
WHERE   z.rowid BETWEEN {6} AND {7}";

                    sql = string.Format(sql, GetParms_PageList_BySearch_EditPurchaseHouses_Admin(search, pageIndex, pageSize));

                    List<PVM_NH_Purchase_Edit2> list = db.ExecuteStoreQuery<PVM_NH_Purchase_Edit2>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("李雨钊", search, ex);
                return null;
            }
        }

        /// <summary>
        ///     获取总记录数
        ///     author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_EditPurchaseHouses_Admin(PVS_NH_Purchase_Edit search)
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
        --{5}--AND a.SalesStatus = 1
        AND a.SalesStatus = 2
        AND a.IsDel = 0";

                    sql = string.Format(sql, GetParms_PageList_BySearch_EditPurchaseHouses_Admin(search));

                    List<ESqlResult_ScalarInt> list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                RecordLog.RecordException("李雨钊", search, ex);
                return 0;
            }
        }

        /// <summary>
        ///     获取搜索条件
        ///     author: liyuzhao
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        private object[] GetParms_PageList_BySearch_EditPurchaseHouses_Admin(PVS_NH_Purchase_Edit search, int pageIndex = 0, int pageSize = 0)
        {
            var list = new List<object>
            {
                string.Format(@"AND a.Id NOT IN ( SELECT d1.HouseId FROM ActivitiesHouse (NOLOCK) AS d1 LEFT JOIN Activities (NOLOCK) AS d2 ON d2.Id = d1.ActivitiesId AND d2.ActivitieState IN ( 0, 1 ) WHERE d2.Id <> {0} )", search.ActivityId),
                search.PremisesId,
                (0 < search.BuildingId) ? string.Format("AND a.BuildingId = {0}", search.BuildingId) : string.Empty,
                (0 != search.Floor) ? string.Format("AND a.[Floor] = {0}", search.Floor) : string.Empty,
                (0 < search.UnitNum) ? string.Format("AND a.UnitId = {0}", search.UnitNum) : string.Empty,
                string.Empty
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

        #endregion

        #region 修改 排号购房

        public int UpdatePurchase_Admin_UpdateActivities(PVM_NH_Purchase model)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
UPDATE  Activities
SET     Name = @Name, HouseCount = @HouseCount, Bond = @Bond, UserCount=@UserCount, BeginTime = @BeginTime, EndTime = @EndTime,
        UpdateTime = GETDATE()
WHERE   Id = @Id";

                    var housecount = model.JoinHouses.Select(t => t.IsDel == 0);
                    var parms = new object[]
                    {
                        new SqlParameter("@Id", model.Id),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@HouseCount", housecount.Count()),
                        new SqlParameter("@Bond", model.Bond),
                        new SqlParameter("@UserCount", model.UserCount),
                        new SqlParameter("@BeginTime", model.BeginTime),
                        new SqlParameter("@EndTime", string.Format("{0:yyyy-MM-dd 23:59:59}", model.EndTime))
                    };

                    int count = db.ExecuteStoreCommand(sql, parms);

                    return count;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                RecordLog.RecordException("李雨钊", model, ex);
                throw;
            }
        }

        public int UpdatePurchase_Admin_UpdateActivityHouse(PVM_NH_Purchase model)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    for (int i = 0; i < model.JoinHouses.Count; i++)
                    {
                        PVE_NH_Purchase_JoinHouse house = model.JoinHouses[i];
                        if (house.IsOriData == 1)
                        {
                            // 删除原有活动房源只打标记
                            if (house.IsDel == 1)
                            {
                                sql.AppendFormat(@"
 UPDATE  ActivitiesHouse
SET     IsDel = 1, UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}
        AND HouseId = {1};", model.Id, house.HouseId);
                            }
                            else
                            {
                                sql.AppendFormat(@"
 UPDATE  ActivitiesHouse
SET     UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}
        AND HouseId = {1};", model.Id, house.HouseId);
                            }
                        }
                        else
                        {
                            sql.AppendFormat(@"
IF NOT EXISTS ( SELECT  1
                FROM    ActivitiesHouse (NOLOCK)
                WHERE   ActivitiesId = {0}
                        AND HouseId = {1} ) 
    BEGIN
        INSERT  INTO ActivitiesHouse ( ActivitiesId, CityId, DeveloperId, PremisesId, BuildingId, UnitId, HouseId,
                                       Discount, UpdateTime, CreateTime, IsDel )
                SELECT  {0}, b.CityId, a.DeveloperId, a.PremisesId, a.BuildingId, a.UnitId, a.Id, 0, GETDATE(), GETDATE(), 0
                FROM    House (NOLOCK) AS a
                        LEFT JOIN Premises (NOLOCK) AS b ON b.Id = a.PremisesId
                WHERE   a.Id = {1}        
    END
ELSE 
    BEGIN
        UPDATE  ActivitiesHouse
        SET     IsDel = 0, UpdateTime = GETDATE()
        WHERE   ActivitiesId = {0}
                AND HouseId = {1}
    END;", model.Id, house.HouseId);
                        }
                    }

                    int count = db.ExecuteStoreCommand(sql.ToString());

                    return count;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                RecordLog.RecordException("李雨钊", model, ex);
                throw;
            }
        }

        #endregion

        #region 修改团购活动

        /// <summary>
        /// 获取活动信息部分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PVM_NH_TuanGou_Social> GetTuanGouSocial(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    Social (NOLOCK)
WHERE   ActivitiesId = {0}", id);

                    var list = db.ExecuteStoreQuery<PVM_NH_TuanGou_Social>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                RecordLog.RecordException("李雨钊", string.Format("id:{0}", id), ex);
                throw;
            }
        }

        public int UpdateTuanGou_Admin_UpdateActivities(PVM_NH_TuanGou model)
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
                    var housecount = model.JoinHouses.Select(t => t.IsDel == 0);
                    var parms = new object[]
                    {
                        new SqlParameter("@Id", model.Id),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@HouseCount", housecount.Count()),
                        new SqlParameter("@Bond", model.Bond),
                        new SqlParameter("@BeginTime", model.BeginTime),
                        new SqlParameter("@EndTime", string.Format("{0:yyyy-MM-dd 23:59:59}", model.EndTime))
                    };

                    int count = db.ExecuteStoreCommand(sql, parms);

                    return count;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                RecordLog.RecordException("李雨钊", model, ex);
                throw;
            }
        }

        public int UpdateTuanGou_Admin_UpdateSocial(PVM_NH_TuanGou model)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    sql.AppendFormat(@"
DELETE  FROM Social
WHERE   ActivitiesId = {0};", model.Id);

                    for (int i = 0; i < model.Socials.Count; i++)
                    {
                        sql.AppendFormat(@"
INSERT  INTO Social ( ActivitiesId, UserCount, Discount, CreateTime, UpdateTime, IsDel )
VALUES  ( {0}, {1}, {2}, GETDATE(), GETDATE(), 0 );", model.Socials[i].ActivitiesId, model.Socials[i].UserCount, model.Socials[i].Discount);
                    }

                    int count = db.ExecuteStoreCommand(sql.ToString());

                    return count;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                RecordLog.RecordException("李雨钊", model, ex);
                throw;
            }
        }

        public int UpdateTuanGou_Admin_UpdateActivityHouse(PVM_NH_TuanGou model)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    for (int i = 0; i < model.JoinHouses.Count; i++)
                    {
                        PVE_NH_Purchase_JoinHouse house = model.JoinHouses[i];
                        if (house.IsOriData == 1)
                        {
                            // 删除原有活动房源只打标记
                            if (house.IsDel == 1)
                            {
                                sql.AppendFormat(@"
 UPDATE  ActivitiesHouse
SET     IsDel = 1, UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}
        AND HouseId = {1};", model.Id, house.HouseId);
                            }
                            else
                            {
                                sql.AppendFormat(@"
 UPDATE  ActivitiesHouse
SET     UpdateTime = GETDATE()
WHERE   ActivitiesId = {0}
        AND HouseId = {1};", model.Id, house.HouseId);
                            }
                        }
                        else
                        {
                            sql.AppendFormat(@"
IF NOT EXISTS ( SELECT  1
                FROM    ActivitiesHouse (NOLOCK)
                WHERE   ActivitiesId = {0}
                        AND HouseId = {1} ) 
    BEGIN
        INSERT  INTO ActivitiesHouse ( ActivitiesId, CityId, DeveloperId, PremisesId, BuildingId, UnitId, HouseId,
                                       Discount, UpdateTime, CreateTime, IsDel )
                SELECT  {0}, b.CityId, a.DeveloperId, a.PremisesId, a.BuildingId, a.UnitId, a.Id, 0, GETDATE(), GETDATE(), 0
                FROM    House (NOLOCK) AS a
                        LEFT JOIN Premises (NOLOCK) AS b ON b.Id = a.PremisesId
                WHERE   a.Id = {1}        
    END
ELSE 
    BEGIN
        UPDATE  ActivitiesHouse
        SET     IsDel = 0, UpdateTime = GETDATE()
        WHERE   ActivitiesId = {0}
                AND HouseId = {1}
    END;", model.Id, house.HouseId);
                        }
                    }

                    int count = db.ExecuteStoreCommand(sql.ToString());

                    return count;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                RecordLog.RecordException("李雨钊", model, ex);
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
        public PVE_NH_Purchase_Detail GetPurchaseDetail(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    Activities (NOLOCK)
WHERE   Id = {0}", id);

                    var list = db.ExecuteStoreQuery<PVE_NH_Purchase_Detail>(sql).ToList();

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
        public List<PVE_NH_Discount_Houses> GetPurchaseDetail_Houses(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  a.Discount, b.Id AS HouseId, b.Name AS HouseName, b.[Floor], b.BuildingId, d.Name AS BuildingName, b.Room,
        b.Hall, b.Toilet, b.Kitchen, b.BuildingArea, b.SinglePrice, b.TotalPrice, b.SalesStatus, c.Name AS UnitName, e.Name AS PremiseName, b.CreateTime
FROM    ActivitiesHouse (NOLOCK) AS a
        LEFT JOIN House (NOLOCK) AS b ON b.Id = a.HouseId
        LEFT JOIN Unit (NOLOCK) AS c ON c.Num = b.UnitId
                                        AND c.BuildingId = b.BuildingId
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
    }
}