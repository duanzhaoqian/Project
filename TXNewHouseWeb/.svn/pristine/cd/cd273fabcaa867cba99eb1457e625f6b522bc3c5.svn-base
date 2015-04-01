using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXDal.NHouseActivies.APrice
{
    public class APrice_AdminDal:BaseDal_Admin
    {
        #region 一口价价活动信息列表
        /// <summary>
        /// 一口价价活动信息列表
        /// Author:wangzhikun
        /// </summary>
        /// <param name="search">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_Bargain> GetAPriceListByPages(PVS_NH_Bargain search, int pageIndex, int pageSize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                       SELECT * FROM (
                                       SELECT  ROW_NUMBER() OVER ( ORDER BY active.CreateTime DESC ) AS RowID,
                                         active.Id,
                                         house.Name AS HouseNum, 
				                         house.Floor,
                                         house.TotalPrice,
                                         unit.Name AS UnitName,
                                         build.Name AS FloorNum,
                                         build.NameType,
                                         premise.ProvinceId,
                                         premise.ProvinceName,
                                         premise.CityId,
                                         premise.CityName,
                                         premise.Developer AS DeveloperName,
                                         premise.Agent,   
                                         --deve.CompanyName AS DeveloperName,
                                         active.BidPrice,
                                         active.AddPrice,
                                         active.MaxPrice,
                                         active.BeginTime,
                                         active.EndTime,
                                         active.Type,
                                         active.ActivitieState,
                                         premise.Id AS PremisesId,
                                         house.Id AS HouseId
               

                                         FROM Activities AS active (NOLOCK)
                                         INNER JOIN Developer_Identity AS deve (NOLOCK) ON active.DeveloperId=deve.DeveloperId
                                         INNER JOIN dbo.ActivitiesHouse (NOLOCK) ah ON active.Id=ah.ActivitiesId
                                         INNER JOIN House AS house (NOLOCK) ON ah.HouseId=house.Id
                                         INNER JOIN Building AS build (NOLOCK) ON ah.BuildingId=build.Id
                                         INNER JOIN Premises AS premise (NOLOCK) ON ah.PremisesId=premise.Id
                                         INNER JOIN Unit AS unit (NOLOCK) ON ah.UnitId=unit.Num  and build.Id=unit.BuildingId
				                                    
				                         WHERE active.IsDel=0 AND deve.IsDel=0 AND house.IsDel=0 AND build.IsDel=0 AND premise.IsDel=0 AND Unit.IsDel=0  {0}

                                    ) HOSE  
			    WHERE   HOSE.RowID BETWEEN {1} AND {2} ", new object[]
                    {
                        GetSearchSqlMess(search),
                        ((pageIndex - 1)*pageSize) + 1,
                        pageIndex*pageSize
                    });

                    #region 执行操作

                    ObjectResult<PVM_NH_Bargain> query = houseDb.ExecuteStoreQuery<PVM_NH_Bargain>(sql);
                    List<PVM_NH_Bargain> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0},{1},{2})", new object[] { search, pageIndex, pageSize }), ex);
                throw;
            }
        }

        #endregion

        #region 一口价活动信息总记录数
        /// <summary>
        /// 一口价活动信息总记录数
        /// Author:wangzhikun
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetAPriceByPageCounts(PVS_NH_Bargain search)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT COUNT(1) AS Result
			                             FROM Activities AS active (NOLOCK)
                                         INNER JOIN Developer_Identity AS deve (NOLOCK) ON active.DeveloperId=deve.DeveloperId
                                         INNER JOIN dbo.ActivitiesHouse (NOLOCK) ah ON active.Id=ah.ActivitiesId
                                         INNER JOIN House AS house (NOLOCK) ON ah.HouseId=house.Id
                                         INNER JOIN Building AS build (NOLOCK) ON ah.BuildingId=build.Id
                                         INNER JOIN Premises AS premise (NOLOCK) ON ah.PremisesId=premise.Id
                                         INNER JOIN Unit AS unit (NOLOCK) ON ah.UnitId=unit.Num  and build.Id=unit.BuildingId
				                                    
				                         WHERE active.IsDel=0 AND deve.IsDel=0 AND house.IsDel=0 AND build.IsDel=0 AND premise.IsDel=0 AND Unit.IsDel=0  {0}                                       
                                    ", new object[]
                    {
                        GetSearchSqlMess(search)
                    });

                    #region 执行操作

                    try
                    {
                        ObjectResult<SqlResultScalarInt> query = houseDb.ExecuteStoreQuery<SqlResultScalarInt>(sql);
                        List<SqlResultScalarInt> resultlist = query.ToList();
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
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0})", new object[] { search }), ex);
                throw;
            }
        }

        #endregion

        #region 获取查询砍价活动列表sqlwhere语句
        /// <summary>
        /// 获取查询一口价活动列表sqlwhere语句
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public string GetSearchSqlMess(PVS_NH_Bargain search)
        {

            var whereStr = new StringBuilder();

            #region 过滤数据

            //根据活动类型搜索
            if (search.Type > 0)
            {
                whereStr.AppendFormat(" AND active.Type={0}", search.Type);
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

            //根据楼盘搜索
            if (search.PremisesId > 0)
            {
                whereStr.AppendFormat(" AND premise.Id={0}", search.PremisesId);
            }
            //根据楼栋搜索
            if (search.BuildingId > 0)
            {
                whereStr.AppendFormat(" AND build.Id={0}", search.BuildingId);
            }
            //根据单元搜索
            if (search.UnitId > 0)
            {
                whereStr.AppendFormat(" AND unit.Num={0}", search.UnitId);
            }
            //根据楼层搜索
            if (search.FloorId > 0)
            {
                whereStr.AppendFormat(" AND house.Floor={0}", search.FloorId);
            }

            //根据活动状态搜索
            if (search.ActivitieState != -1)
            {
                whereStr.AppendFormat(" AND active.ActivitieState={0}", search.ActivitieState);
            }

            //根据开发商名称模糊查询
            if (!string.IsNullOrWhiteSpace(search.DeveloperName))
            {
                whereStr.AppendFormat(@"AND ( premise.Developer LIKE '{0}%' OR premise.Agent LIKE '{0}%')", search.DeveloperName);
            }

            #endregion

            return whereStr.ToString();
        }

        /// <summary>
        /// SQL执行返回首行首列(整型)
        /// Author:wangzhikun
        /// </summary>
        private class SqlResultScalarInt
        {
            public int? Result { set; get; }
        }

        #endregion

        #region 获取一口价报名用户列表
        /// <summary>
        /// 获取一口价报名用户列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_BidingUsers> GetUsersListByPages(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                       SELECT * FROM (
                                SELECT 
                 ROW_NUMBER() OVER ( ORDER BY bd.CreateTime DESC ) AS RowID,
                 bd.Id,
                 bd.BidUserName,
                 bd.BidRealName,
                 bd.BidUserMobile,
                 bd.ActivitiesId,
                 bd.BidUserQQ,
                 bd.BidUserMail,
                 bd.CreateTime,
                 bd.BidCount,
                 bd.BidUserPrice,
                 bd.Status,
                 bd.IDCard,
                 bd.HouseId,
(select count(1) from dbo.BidPrice where IsDel=0 and (Status=1 or Status=2) and ActivitiesId={3} and HouseId={4})AS HaveWinCount              
                 FROM                 
                (SELECT MAX(Id) AS Id,BidUserId,ActivitiesId 
                FROM dbo.BidPrice 
                GROUP BY BidUserId,ActivitiesId) Man
                JOIN dbo.BidPrice bd ON bd.Id=Man.Id
                WHERE bd.IsDel=0 {0} 
                            ) AC 
                WHERE   AC.RowID BETWEEN {1} AND {2} ", new object[]
                    {
                        GetRegistUsersSqlMess(search),
                        ((pageIndex - 1)*pageSize) + 1,
                        pageIndex*pageSize,
                        search.Id,
                        search.HouseId
                    });

                    #region 执行操作

                    ObjectResult<PVM_NH_BidingUsers> query = houseDb.ExecuteStoreQuery<PVM_NH_BidingUsers>(sql);
                    List<PVM_NH_BidingUsers> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0},{1},{2})", new object[] { search, pageIndex, pageSize }), ex);
                throw;
            }
        }
        #endregion

        #region 获取一口价报名用户信息总记录数
        /// <summary>
        /// 获取一口价报名用户信息总记录数
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
                            SELECT 
                 COUNT(1) AS Result     
                 FROM                 
                (SELECT MAX(Id) AS Id,BidUserId,ActivitiesId 
                FROM dbo.BidPrice 
                GROUP BY BidUserId,ActivitiesId) Man
                JOIN dbo.BidPrice bd ON bd.Id=Man.Id
                WHERE bd.IsDel=0 {0}                                      
                                    ", new object[]
                    {
                        GetRegistUsersSqlMess(search)
                    });

                    #region 执行操作

                    try
                    {
                        ObjectResult<SqlResultScalarInt> query = houseDb.ExecuteStoreQuery<SqlResultScalarInt>(sql);
                        List<SqlResultScalarInt> resultlist = query.ToList();
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
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0})", new object[] { search }), ex);
                throw;
            }
        }
        #endregion

        #region 获取一口价报名用户列表sqlwhere语句
        /// <summary>
        /// 获取一口价报名用户列表sqlwhere语句
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public string GetRegistUsersSqlMess(PVS_NH_RegistrationUser search)
        {
            var whereStr = new StringBuilder();

            #region 过滤数据
            //根据活动类型搜索
            //if (search.Type > 0)
            //{
            //    whereStr.AppendFormat(" AND active.Type={0}", search.Type);
            //}


            //根据活动编号搜索      
            if (search.Id > 0)
            {
                whereStr.AppendFormat(" AND Man.ActivitiesId={0}", search.Id);
            }

            #endregion

            return whereStr.ToString();
        }
        #endregion

        #region 导出报名用户列表
        /// <summary>
        /// 导出报名用户列表
        /// </summary>
        /// <returns></returns>
        public List<PVM_NH_BidingUsers> GetOutPutRegistUsersByList(PVS_NH_RegistrationUser search)
        {
            try
            {
                using (var kyjEnt = new kyj_NewHouseDBEntities())
                {
                    #region sql 参数
                    string sql = string.Format(@" SELECT * FROM (
                                SELECT  ROW_NUMBER() OVER ( ORDER BY bid.CreateTime DESC ) AS RowID,
                                    bid.Id,
                                    bid.BidUserName,
                                    bid.BidRealName,
                                    bid.BidUserMobile,
                                    bid.ActivitiesId,
                                    bid.BidUserQQ,
                                    bid.BidUserMail,
                                    bid.CreateTime,
                                    bid.BidCount,
                                    bid.BidUserPrice,
                                    bid.Status,   
                                    bid.IDCard
                                FROM (SELECT MAX(Id) AS Id,BidUserId,ActivitiesId FROM dbo.BidPrice GROUP BY BidUserId,ActivitiesId) Man
                                JOIN dbo.BidPrice bid ON bid.Id=Man.Id
                                WHERE 1=1  {0}
                            ) AC ", new object[]{
                                                GetRegistUsersSqlMess(search)
                                                });
                    #endregion

                    #region 执行操作
                    ObjectResult<PVM_NH_BidingUsers> query = kyjEnt.ExecuteStoreQuery<PVM_NH_BidingUsers>(sql);
                    List<PVM_NH_BidingUsers> ls = query.ToList();

                    #endregion
                    return ls;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("wangzhikun", "导出列表", e);
                throw;
            }
        }
        #endregion

        #region 根据活动id获取活动相关信息
        /// <summary>
        /// 根据活动id获取活动相关信息
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public PVM_NH_BargainActive GetAPriceActiviesInfo(int id)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var query = from activi in kyj.Activities
                                join ah in kyj.ActivitiesHouses on activi.Id equals ah.ActivitiesId
                                join h in kyj.Houses on ah.HouseId equals h.Id
                                join p in kyj.Premises on ah.PremisesId equals p.Id
                                join b in kyj.Buildings on ah.BuildingId equals b.Id
                                join u in kyj.Units on ah.UnitId equals u.Num
                                where activi.Id == id && b.Id == u.BuildingId && p.Id == u.PremisesId && activi.IsDel == false && ah.IsDel == false && h.IsDel == false && p.IsDel == false && b.IsDel == false && u.IsDel == false
                                select new PVM_NH_BargainActive
                                {
                                    ActivitiesId = activi.Id,
                                    HouseId = h.Id,
                                    HouseNum = h.Name,
                                    Floor = h.Floor,
                                    UnitId = u.Id,
                                    UnitName = u.Name,
                                    BuildingId = b.Id,
                                    BuildingName = b.Name,
                                    NameType = b.NameType,
                                    PremisesId = p.Id,
                                    PremisesName = p.Name,
                                    BidPrice = activi.BidPrice,
                                    AddPrice = activi.AddPrice,
                                    MaxPrice = activi.MaxPrice,
                                    Bond = activi.Bond,
                                    BeginTime = activi.BeginTime,
                                    EndTime = activi.EndTime,
                                    SalesStatus = h.SalesStatus,
                                    ActivitiesHouseId = ah.Id
                                };

                    return query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("wangzhikun", id, ex);
                throw;
            }
        }
        #endregion

        #region 获取一口价活动参与房源列表
        /// <summary>
        /// 获取一口价活动参与房源列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Biding_House> GetAPriceHousePageList_BySearch_Admin(PVS_NH_Biding_House search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var query = from house in kyj.Houses
                                join building in kyj.Buildings on house.BuildingId equals building.Id
                                join premises in kyj.Premises on house.PremisesId equals premises.Id
                                join unti in kyj.Units on house.UnitId equals unti.Num
                                join ah in kyj.ActivitiesHouses on house.Id equals ah.HouseId into leftjoinah
                                from ah in leftjoinah.DefaultIfEmpty()
                                where house.IsDel == false && building.IsDel == false && premises.IsDel == false && (ah == null || (ah.ActivitiesId == search.ActivitiesId && building.Id == unti.BuildingId && premises.Id == unti.PremisesId && ah.IsDel == false))
                                select new
                                {
                                    house,
                                    unitName = unti.Name,
                                    building.Name,
                                    building.NameType,
                                    premisesName = premises.Name

                                };
                    #region 筛选
                    //楼盘
                    if (search.PremisesId > 0)
                    {
                        query = query.Where(it => it.house.PremisesId == search.PremisesId);
                    }
                    //楼栋
                    if (search.BuildingId > 0)
                    {
                        query = query.Where(it => it.house.BuildingId == search.BuildingId);
                    }
                    //单元
                    if (search.UnitId != 0)
                    {
                        query = query.Where(it => it.house.UnitId == search.UnitId);
                    }
                    //楼层
                    if (search.Floor != 0)
                    {
                        query = query.Where(it => it.house.Floor == search.Floor);
                    }
                    //销售状态 

                    if (search.SalesStatus > -1)
                    {
                        query = query.Where(it => it.house.SalesStatus == search.SalesStatus);
                    }
                    #endregion
                    #region 返回数据
                    return
                        query.OrderByDescending(it => it.house.CreateTime)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList()
                            .ConvertAll(it => new PVM_NH_Biding_House
                            {
                                Id = it.house.Id,
                                Name = it.house.Name,
                                InnerCode = it.house.InnerCode,
                                DeveloperId = it.house.DeveloperId,
                                PremisesId = it.house.PremisesId,
                                BuildingId = it.house.BuildingId,
                                UnitId = it.house.UnitId,
                                Floor = it.house.Floor,
                                RId = it.house.RId,
                                BuildingType = it.house.BuildingType,
                                Orientation = it.house.Orientation,
                                PriceType = it.house.PriceType,
                                TotalPrice = it.house.TotalPrice,
                                SinglePrice = it.house.SinglePrice,
                                DownPayment = it.house.DownPayment,
                                SalesStatus = it.house.SalesStatus,
                                BuildingArea = it.house.BuildingArea,
                                Area = it.house.Area,
                                Room = it.house.Room,
                                Hall = it.house.Hall,
                                Toilet = it.house.Toilet,
                                Kitchen = it.house.Kitchen,
                                //Balcony = it.house.Balcony,
                                CreateTime = it.house.CreateTime,
                                UpdateTime = it.house.UpdateTime,
                                IsDel = it.house.IsDel,
                                BuildingName = it.Name,
                                PremisesName = it.premisesName,
                                UnitName=it.unitName
                            });
                    #endregion
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", search, e);
                throw;
            }
        }
        #endregion

        #region 获取一口价活动参与房源列表记录数
        /// <summary>
        /// 获取一口价活动参与房源列表记录数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetAPriceHousePageCount_BySearch_Admin(PVS_NH_Biding_House search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var query = from house in kyj.Houses
                                join building in kyj.Buildings on house.BuildingId equals building.Id
                                join premises in kyj.Premises on house.PremisesId equals premises.Id
                                join unti in kyj.Units on house.UnitId equals unti.Num
                                join ah in kyj.ActivitiesHouses on house.Id equals ah.HouseId into leftjoinah
                                from ah in leftjoinah.DefaultIfEmpty()
                                where house.IsDel == false && building.IsDel == false && premises.IsDel == false && (ah == null || (ah.ActivitiesId == search.ActivitiesId && building.Id == unti.BuildingId && premises.Id == unti.PremisesId && ah.IsDel == false))
                                select house;
                    #region 筛选
                    //楼盘
                    if (search.PremisesId > 0)
                    {
                        query = query.Where(it => it.PremisesId == search.PremisesId);
                    }
                    //楼栋
                    if (search.BuildingId > 0)
                    {
                        query = query.Where(it => it.BuildingId == search.BuildingId);
                    }
                    //单元
                    if (search.UnitId != 0)
                    {
                        query = query.Where(it => it.UnitId == search.UnitId);
                    }
                    //楼层
                    if (search.Floor != 0)
                    {
                        query = query.Where(it => it.Floor == search.Floor);
                    }
                    //销售状态

                    if (search.SalesStatus > -1)
                    {
                        query = query.Where(it => it.SalesStatus == search.SalesStatus);
                    }
                    #endregion
                    #region 返回数据

                    return query.Count();

                    #endregion
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", search, e);
                throw;
            }
        }
        #endregion

        #region 一口价活动 房源数据

        /// <summary>
        /// 获取一口价活动参与房源列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Biding_House> GetAPriceHousePageList_BySearch_Admin2(PVS_NH_Biding_House search, int pageIndex, int pageSize)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  z.HouseId AS Id, z.HouseName AS Name, z.[Floor], z.UnitName, z.BuildingName, z.BuildingNameType, z.Room, z.Hall, z.Toilet,
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

                    var list = db.ExecuteStoreQuery<PVM_NH_Biding_House>(sql).ToList();

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
        /// 获取一口价活动参与房源列表记录数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetAPriceHousePageCount_BySearch_Admin2(PVS_NH_Biding_House search)
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

        private object[] GetParms_PageList_BySearch_EditHouses_Admin(PVS_NH_Biding_House search, int pageIndex = 0, int pageSize = 0)
        {
            var list = new List<object>
            {
                string.Format(@"AND a.Id NOT IN ( SELECT d1.HouseId FROM ActivitiesHouse (NOLOCK) AS d1 LEFT JOIN Activities (NOLOCK) AS d2 ON d2.Id = d1.ActivitiesId AND d2.ActivitieState IN ( 0, 1 ) WHERE d2.Id <> {0} )", search.ActivitiesId),
                search.PremisesId,
                (0 < search.BuildingId) ? string.Format("AND a.BuildingId = {0}", search.BuildingId) : string.Empty,
                (0 < search.Floor) ? string.Format("AND a.[Floor] = {0}", search.Floor) : string.Empty,
                (0 < search.UnitId) ? string.Format("AND a.UnitId = {0}", search.UnitId) : string.Empty,
                (-1 != search.SalesStatus) ? string.Format("AND a.SalesStatus = {0}", search.SalesStatus) : string.Empty,
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

        #endregion

        #region 修改一口价活动
        /// <summary>
        /// 修改一口价活动
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public ESqlResult UpdateAPrice_Admin(PVM_NH_Biding model)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
--修改一口价活动  --
DECLARE @t_AId INT,@t_AHId INT
SELECT @t_AId=Id FROM dbo.Activities WHERE Id=@AId AND IsDel=0
IF(@t_AId IS NULL)
    BEGIN
            SELECT  '0' AS Code, '没有对应的活动申请记录，修改失败。' AS Msg
             RETURN
    END
SELECT @t_AHId=Id FROM dbo.ActivitiesHouse WHERE Id=@AHId AND IsDel=0
IF(@t_AHId IS NULL)
    BEGIN
            SELECT  '0' AS Code, '没有对应的活动房源关系申请记录，修改失败。' AS Msg
             RETURN
    END
-- 更新 活动表  --
BEGIN TRY
BEGIN TRAN
UPDATE dbo.Activities
   SET 
      [BidPrice] = @BidPrice
      ,[AddPrice] = @AddPrice
      ,[MaxPrice] = @MaxPrice
      ,[BeginTime] = @BeginTime
      ,[EndTime] = @EndTime
      ,[Bond] = @Bond,
      ,[ActivitieState] = @ActivitieState,     
      ,[UpdateTime] = GETDATE()
 WHERE  Id=@t_AId
 -- 更新活动 房源关系--
 UPDATE dbo.ActivitiesHouse
	SET
	 DeveloperId=@DeveloperId,
	 PremisesId=@PremisesId,
	 BuildingId=@BuildingId,
	 UnitId=@UnitId,
	 HouseId=@HouseId,
	 UpdateTime=GETDATE()	 
 WHERE Id=@t_AHId
-------------------------------------
COMMIT
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '事物中断，操作失败' AS Msg
END CATCH
IF ( @@ERROR > 0 ) 
    BEGIN
        SELECT  '0' AS Code, '出现错误，操作失败' AS Msg
        RETURN
    END
SELECT  '1' AS Code, '操作成功' AS Msg
";
                    object[] parameters =
                    {
                        new SqlParameter("@AId",model.ActivitiesId),
                        new SqlParameter("@AHId",model.ActivitiesHouseId),
                        new SqlParameter("@BidPrice",model.BidPrice),
                        new SqlParameter("@AddPrice",model.AddPrice),
                        new SqlParameter("@MaxPrice",model.MaxPrice),
                        new SqlParameter("@BeginTime",model.BeginTime),
                        new SqlParameter("@EndTime",model.EndTime),
                        new SqlParameter("@Bond",model.Bond),
                        new SqlParameter("@ActivitieState", model.ActivitieState),
                       
                        new SqlParameter("@DeveloperId",model.DeveloperId),
                        new SqlParameter("@PremisesId",model.PremisesId),
                        new SqlParameter("@BuildingId",model.BuildingId),
                        new SqlParameter("@UnitId",model.UnitId),
                        new SqlParameter("@HouseId",model.HouseId)
                       
                    };
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql, parameters).ToList().FirstOrDefault();

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", model, ex);
                throw;
            }
        }
        #endregion

        #region 删除一口价活动
        /// <summary>
        /// 删除一口价活动
        /// </summary>
        /// <param name="aid">活动Id</param>
        /// <returns></returns>
        public ESqlResult DeleteActive(int aid)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
----删除一口价活动---------------------
DECLARE @t_AId INT
SELECT @t_AId=Id FROM dbo.Activities WHERE Id={0} AND IsDel=0
IF(@t_AId IS NULL)
    BEGIN
            SELECT  '0' AS Code, '没有对应的活动申请记录，修改失败。' AS Msg
             RETURN
    END
BEGIN TRY
BEGIN TRAN
----更新 activities------------------
UPDATE dbo.Activities
SET
IsDel=1,
UpdateTime=GETDATE()	
WHERE Id={0}
----更新 ActivitiesHouse-------------
UPDATE dbo.ActivitiesHouse
SET
IsDel=1,
UpdateTime=GETDATE()	
WHERE ActivitiesId={0}
UPDATE  AmountTiming
----更新 定时表-------------
SET     EndTime = DATEADD(DAY, 7,
                            DATENAME(Year, GETDATE()) + N'-' + CAST(DATEPART(Month, GETDATE()) AS VARCHAR) + N'-'
                            + DATENAME(Day, GETDATE()))
WHERE   ActivityID = {0}
-------------------------------------
COMMIT
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '事物中断，操作失败' AS Msg
END CATCH
IF ( @@ERROR > 0 ) 
    BEGIN
        SELECT  '0' AS Code, '出现错误，操作失败' AS Msg
        RETURN
    END
SELECT  '1' AS Code, '操作成功' AS Msg
", aid);
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql).ToList().FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("wangzhikun", "活动Id" + aid, ex);
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 根据编号获取一口价实体
        /// </summary>
        /// <param name="id">一口价编号</param>
        /// <returns>返回：一口价实体</returns>
        public object GetEntity_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Activities.FirstOrDefault(active => active.Id == id && active.Type == 8 && active.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("Id:{0}", new object[] { id }), ex);
                return null;
            }
        }
    }
}
