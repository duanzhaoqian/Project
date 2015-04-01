using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXDal.NHouseActivies.Biding
{
    /// <summary>
    /// 竞价活动管理(网站管理平台)
    /// </summary>
    public partial class BidingDal : BaseDal_Admin
    {
        #region 分页获取竞价活动列表 活动审批列表

        /// <summary>
        /// 分页获取竞价活动列表 活动审批列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">尺寸</param>
        /// <returns></returns>
        public List<PVM_NH_Biding> GetPageList_BySearch_Admin(PVS_NH_Biding search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"  
SELECT * FROM (
                SELECT 
                 ROW_NUMBER() OVER ( ORDER BY a.CreateTime DESC ) AS RowID,
                 a.Id AS ActivitiesId,
                 h.Id AS HouseId,
                 h.Name AS HouseName,
                 h.Floor,
                 h.UnitId,
                 u.Name AS UnitName,
                 h.BuildingId,
                 b.Name +b.NameType AS  BuildingName,
                 h.PremisesId,
                 p.Name AS PremisesName,
                 a.DeveloperId,
                 p.Developer,
                 p.Agent,
                 h.TotalPrice,
                 a.BidPrice,
                 a.AddPrice,
                 a.MaxPrice,
                 a.BeginTime,
                 a.EndTime,
                 p.ProvinceId,
                 p.ProvinceName,
                 p.CityId,
                 p.CityName,
                 a.ActivitieState,
                 ah.Id AS ActivitiesHouseId
                 FROM dbo.Activities (NOLOCK) a
                 --JOIN dbo.Developer_Identity (NOLOCK) d ON a.DeveloperId=d.DeveloperId
                 JOIN dbo.ActivitiesHouse (NOLOCK) ah ON a.Id=ah.ActivitiesId
                 JOIN dbo.Premises (NOLOCK) p ON p.Id=ah.PremisesId
                 JOIN dbo.Building (NOLOCK) b ON b.Id=ah.BuildingId
                 JOIN dbo.Unit (NOLOCK) u ON u.PremisesId=ah.PremisesId AND u.BuildingId=ah.BuildingId AND u.Num=ah.UnitId
                 JOIN dbo.House (NOLOCK) h ON h.Id=ah.HouseId
                 
                 WHERE a.IsDel=0 AND ah.IsDel=0 AND p.IsDel=0 AND b.IsDel=0 AND u.IsDel=0 AND h.IsDel=0 {0}
                 
                ) AC 
                WHERE   AC.RowID BETWEEN {1} AND {2}",
                        new object[]
                        {
                            GetBidingWhere_BySearch_Admin(search),
                            ((pageIndex - 1)*pageSize) + 1,
                            pageIndex*pageSize
                        });

                    var list = kyj.ExecuteStoreQuery<PVM_NH_Biding>(sql).ToList();
                    return list;
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

        #region 分页获取竞价活动列表 活动审批列表

        /// <summary>
        /// 分页获取竞价活动列表 活动审批列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_Biding search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                 SELECT COUNT(1) AS Result
                 FROM dbo.Activities (NOLOCK) a
                 --JOIN dbo.Developer_Identity (NOLOCK) d ON a.DeveloperId=d.DeveloperId
                 JOIN dbo.ActivitiesHouse (NOLOCK) ah ON a.Id=ah.ActivitiesId
                 JOIN dbo.Premises (NOLOCK) p ON p.Id=ah.PremisesId
                 JOIN dbo.Building (NOLOCK) b ON b.Id=ah.BuildingId
                 JOIN dbo.Unit (NOLOCK) u ON u.PremisesId=ah.PremisesId AND u.BuildingId=ah.BuildingId AND u.Num=ah.UnitId
                 JOIN dbo.House (NOLOCK) h ON h.Id=ah.HouseId
                 
                 WHERE a.IsDel=0 AND ah.IsDel=0 AND p.IsDel=0 AND b.IsDel=0 AND u.IsDel=0 AND h.IsDel=0 {0}  ",
                        new object[]
                        {
                            GetBidingWhere_BySearch_Admin(search)
                        });

                    var list = kyj.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();
                    if (list.Count > 0)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
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

        #region 搜索条件

        /// <summary>
        /// 搜索条件
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public string GetBidingWhere_BySearch_Admin(PVS_NH_Biding search)
        {
            StringBuilder s = new StringBuilder();
            s.Append(" AND a.Type=5");
            if (search.ProvinceId > 0)
                s.AppendFormat(" AND p.ProvinceId={0}", search.ProvinceId);
            if (search.CityId > 0)
                s.AppendFormat(" AND p.CityId={0}", search.CityId);
            if (!string.IsNullOrWhiteSpace(search.Name))
                //s.AppendFormat(" AND d.CompanyName LIKE '{0}%'", search.Name);
                s.AppendFormat(" AND ( p.Developer LIKE '%{0}%' OR p.Agent LIKE '%{0}%' )", search.Name);
            if (search.PremisesId > 0)
                s.AppendFormat(" AND h.PremisesId={0}", search.PremisesId);
            if (search.BuildingId > 0)
                s.AppendFormat(" AND h.BuildingId={0}", search.BuildingId);
            if (search.UnitId > 0)
                s.AppendFormat(" AND h.UnitId={0}", search.UnitId);
            if (search.Floor > 0)
                s.AppendFormat(" AND h.Floor={0}", search.Floor);
            if (search.ActivitieState > -1)
                s.AppendFormat(" AND a.ActivitieState={0}", search.ActivitieState);
            return s.ToString();
        }

        #endregion

        #region 分页获取竞价用户列表

        public List<PVM_NH_BidingUsers> GetUserPageList_BySearch_Admin(PVS_NH_BidingUsers search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
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
(select count(1) from dbo.BidPrice where IsDel=0 and (Status=2) and ActivitiesId={3} and HouseId={4})AS HaveWinCount
                 FROM                 
                (SELECT MAX(Id) AS Id,BidUserId,ActivitiesId 
                FROM dbo.BidPrice 
                GROUP BY BidUserId,ActivitiesId) Man
                JOIN dbo.BidPrice bd ON bd.Id=Man.Id
                WHERE bd.IsDel=0 {0}  
                ) AC 
                WHERE   AC.RowID BETWEEN {1} AND {2} ",
                        new object[]
                        {
                            GetBidingUserWhere_BySearch_Admin(search),
                            ((pageIndex - 1)*pageSize) + 1,
                            pageIndex*pageSize,
                            search.ActivitiesId,
                            search.HouseId
                        });

                    var list = kyj.ExecuteStoreQuery<PVM_NH_BidingUsers>(sql).ToList();
                    return list;
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

        #region 获取竞价用户记录数

        /// <summary>
        /// 获取竞价用户记录数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetUserPageCount_BySearch_Admin(PVS_NH_BidingUsers search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                SELECT 
                 COUNT(1) AS Result     
                 FROM                 
                (SELECT MAX(Id) AS Id,BidUserId,ActivitiesId 
                FROM dbo.BidPrice 
                GROUP BY BidUserId,ActivitiesId) Man
                JOIN dbo.BidPrice bd ON bd.Id=Man.Id
                WHERE bd.IsDel=0 {0}  ",
                        new object[]
                        {
                            GetBidingUserWhere_BySearch_Admin(search)
                        });

                    var list = kyj.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();
                    if (list.Count > 0)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
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

        #region 获取条件

        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public string GetBidingUserWhere_BySearch_Admin(PVS_NH_BidingUsers search)
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat(" AND Man.ActivitiesId={0}", search.ActivitiesId);
            if (search.HouseId > 0)
                s.AppendFormat(" AND bd.HouseId={0}", search.HouseId);
            return s.ToString();
        }

        #endregion

        #region 根据活动id获取活动相关信息

        /// <summary>
        /// 根据活动id获取活动相关信息
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public PVE_NH_Biding GetBidingActiviesInfo(int id)
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
                                where activi.Id == id && activi.IsDel == false && ah.IsDel == false && h.IsDel == false && p.IsDel == false && b.IsDel == false && u.IsDel == false && u.PremisesId == ah.PremisesId && u.BuildingId == ah.BuildingId
                                select new PVE_NH_Biding
                                {
                                    ActivitiesId = activi.Id,
                                    HouseId = h.Id,
                                    HouseName = h.Name,
                                    Floor = h.Floor,
                                    UnitId = u.Num,
                                    UnitName = u.Name,
                                    BuildingId = b.Id,
                                    BuildingName = b.Name,
                                    BuildingType = b.NameType,
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
                Log4netService.RecordLog.RecordException("胡航飞", id, ex);
                throw;
            }
        }

        #endregion

        #region 获取竞价 参与房源列表

        #region 获取竞价 参与房源列表

        /// <summary>
        /// 获取竞价 参与房源列表
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Biding_House> GetBidingHousePageList_BySearch_Admin(PVS_NH_Biding_House search, int pageIndex, int pageSize)
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
                                where house.IsDel == false && building.IsDel == false && premises.IsDel == false && unti.IsDel == false && unti.PremisesId == ah.PremisesId && unti.BuildingId == ah.BuildingId && (ah == null || (ah.ActivitiesId == search.ActivitiesId && ah.IsDel == false))
                                select new
                                {
                                    house,
                                    building.Name,
                                    premisesName = premises.Name,
                                    unitName = unti.Name
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

                    //if (search.SalesStatus > -1)
                    //{
                    //    query = query.Where(it => it.house.SalesStatus == search.SalesStatus);
                    //}
                    query = query.Where(it => it.house.SalesStatus == 2); //只要在售房源

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
                                UnitName = it.unitName
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

        #region 获取竞价 参与房源列表记录数

        /// <summary>
        /// 获取竞价 参与房源列表记录数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetBidingHousePageCount_BySearch_Admin(PVS_NH_Biding_House search)
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
                                where house.IsDel == false && building.IsDel == false && premises.IsDel == false && unti.IsDel == false && unti.PremisesId == ah.PremisesId && unti.BuildingId == ah.BuildingId && (ah == null || (ah.ActivitiesId == search.ActivitiesId && ah.IsDel == false))
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

                    //if (search.SalesStatus > -1)
                    //{
                    //    query = query.Where(it => it.SalesStatus == search.SalesStatus);
                    //}
                    query = query.Where(it => it.SalesStatus == 2); //只要在售房源

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

        #region 竞价房源列表

        public List<PVM_NH_Biding_House> GetBidingHousePageList_BySearch_Admin2(PVS_NH_Biding_House search, int pageIndex, int pageSize)
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

        public int GetBidingHousePageCount_BySearch_Admin2(PVS_NH_Biding_House search)
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

        #endregion

        #region 修改竞价活动

        /// <summary>
        /// 修改竞价活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ESqlResult UpdateBiding_Admin(PVM_NH_Biding model)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
--修改竞价活动  --
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
      ,[Bond] = @Bond
      ,[ActivitieState] = @ActivitieState
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
                        new SqlParameter("@AId", model.ActivitiesId),
                        new SqlParameter("@AHId", model.ActivitiesHouseId),
                        new SqlParameter("@BidPrice", model.BidPrice),
                        new SqlParameter("@AddPrice", model.AddPrice),
                        new SqlParameter("@MaxPrice", model.MaxPrice),
                        new SqlParameter("@BeginTime", model.BeginTime),
                        new SqlParameter("@EndTime", model.EndTime),
                        new SqlParameter("@Bond", model.Bond),
                        new SqlParameter("@ActivitieState", model.ActivitieState),

                        new SqlParameter("@DeveloperId", model.DeveloperId),
                        new SqlParameter("@PremisesId", model.PremisesId),
                        new SqlParameter("@BuildingId", model.BuildingId),
                        new SqlParameter("@UnitId", model.UnitId),
                        new SqlParameter("@HouseId", model.HouseId)

                    };
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql, parameters).ToList().FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", model, ex);
                throw;
            }
        }

        #endregion

        #region 删除竞价活动

        /// <summary>
        /// 删除竞价活动
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ESqlResult DeleteBiding_Admin(int aid)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
----删除竞价活动---------------------
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
----更新 BidPrice-------------
UPDATE dbo.BidPrice
SET
IsDel=1,
UpdateTime=GETDATE()	
WHERE ActivitiesId={0}
----更新 AmountTiming-------------
 UPDATE  AmountTiming
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
                Log4netService.RecordLog.RecordException("胡航飞", "活动id" + aid, ex);
                throw;
            }
        }

        #endregion

        #region 导出竞价用户列表

        /// <summary>
        /// 导出竞价用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<PVM_NH_BidingUsers> GetAllUserPageList_BySearch_Admin(PVS_NH_BidingUsers search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
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
                 bd.IDCard                  
                 FROM                 
                (SELECT MAX(Id) AS Id,BidUserId,ActivitiesId 
                FROM dbo.BidPrice 
                GROUP BY BidUserId,ActivitiesId) Man
                JOIN dbo.BidPrice bd ON bd.Id=Man.Id
                WHERE 1=1 {0}  
                ",
                        new object[]
                        {
                            GetBidingUserWhere_BySearch_Admin(search)
                        });

                    var list = kyj.ExecuteStoreQuery<PVM_NH_BidingUsers>(sql).ToList();
                    return list;
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

        #region 参与用户中标操作

        /// <summary>
        /// 参与用户中标操作(Old)
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="status">0未成交1已成交2已中标</param>
        /// <returns></returns>
        public bool UpdateBidingStatus_Admin(int bid, int status)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var query = kyj.BidPrices.FirstOrDefault(it => it.Id == bid && it.IsDel == false);
                    if (query != null)
                    {
                        query.Status = status;
                        query.UpdateTime = DateTime.Now;
                        (from order in kyj.Orders where order.BidUserId == query.BidUserId && order.ActivitiesId == query.ActivitiesId select order).FirstOrDefault().Status = status == 2 ? 2 : 0;
                        return kyj.SaveChanges() > 0;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", "出价id" + bid, ex);
                throw;
            }
        }

        /// <summary>
        /// 中标
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public bool UpdateBidingState_Admin_ZhongBiao(int bid)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
DECLARE @tHouseId INT ,
    @tActivityId INT ,
    @tBidUserId INT
    
SELECT  @tActivityId = ActivitiesId, @tHouseId = HouseId, @tBidUserId = BidUserId
FROM    BidPrice (NOLOCK)
WHERE   Id = {0}
        AND IsDel = 0
        
IF ( @tHouseId IS NULL
     OR @tActivityId IS NULL
     OR @tBidUserId IS NULL
   ) 
    BEGIN
        SELECT  '0' AS Code, '没有找到合适的信息' AS Msg
        RETURN
    END
    
BEGIN TRY
    BEGIN TRAN
    UPDATE  BidPrice
    SET     Status = 0, UpdateTime = GETDATE()
    WHERE   ActivitiesId = @tActivityId
            AND (Status <> 0 AND Status <> 1)
    UPDATE  BidPrice
    SET     Status = 2, UpdateTime = GETDATE()
    WHERE   Id = {0}
    --UPDATE  Activities
    --SET     ActivitieState = 2, UpdateTime = GETDATE()
    --WHERE   Id = @tActivityId
    UPDATE  House
    SET     SalesStatus = 3, UpdateTime = GETDATE()
    WHERE   Id = @tHouseId
    UPDATE  [Order]
    SET     Status = 2, UpdateTime = GETDATE()
    WHERE   ActivitiesId = @tActivityId
            AND HouseId = @tHouseId
            AND BidUserId = @tBidUserId
    COMMIT TRAN
    SELECT  '1' AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作失败' AS Msg
END CATCH", bid);

                    var result = db.ExecuteStoreQuery<ESqlResult>(sql).ToList();

                    if (0 < result.Count)
                    {
                        return 0 < Convert.ToInt32(result[0].Code);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", "出价id:" + bid, ex);
                throw;
            }
        }

        /// <summary>
        /// 中标
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public bool UpdateBidingState_Admin_CancelZhongBiao(int bid)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
DECLARE @tHouseId INT ,
    @tActivityId INT ,
    @tBidUserId INT
    
SELECT  @tActivityId = ActivitiesId, @tHouseId = HouseId, @tBidUserId = BidUserId
FROM    BidPrice (NOLOCK)
WHERE   Id = {0}
        AND IsDel = 0
        
IF ( @tHouseId IS NULL
     OR @tActivityId IS NULL
     OR @tBidUserId IS NULL
   ) 
    BEGIN
        SELECT  '0' AS Code, '没有找到合适的信息' AS Msg
        RETURN
    END
    
BEGIN TRY
    BEGIN TRAN
    UPDATE  BidPrice
    SET     Status = 0, UpdateTime = GETDATE()
    WHERE   Id = {0}
    --UPDATE  Activities
    --SET     ActivitieState = 1, UpdateTime = GETDATE()
    --WHERE   Id = @tActivityId
    --UPDATE  House
    --SET     SalesStatus = 2, UpdateTime = GETDATE()
    --WHERE   Id = @tHouseId
    UPDATE  [Order]
    SET     Status = 0, UpdateTime = GETDATE()
    WHERE   ActivitiesId = @tActivityId
            AND HouseId = @tHouseId
            AND BidUserId = @tBidUserId
    DECLARE @MaxPrice MONEY
    SET @MaxPrice = 0 
    SELECT  @MaxPrice = A.BidUserPrice
    FROM    ( SELECT TOP 1
                        *
              FROM      ( SELECT    BidUserPrice, COUNT(1) AS Number
                          FROM      dbo.BidPrice(NOLOCK)
                          WHERE     ActivitiesId = @tActivityId
                          GROUP BY  BidUserPrice
                        ) AS BidPrice
              WHERE     BidPrice.Number = 1
              ORDER BY  BidPrice.BidUserPrice DESC
            ) AS A
    IF @MaxPrice <> 0 
        BEGIN
            UPDATE  dbo.BidPrice
            SET     [Status] = 1
            WHERE   BidUserPrice = @MaxPrice
                    AND ActivitiesId = @tActivityId
                    AND IsDel = 0
            UPDATE  [Order]
            SET     Status = 1, UpdateTime = GETDATE()
            WHERE   ActivitiesId = @tActivityId
                    AND HouseId = @tHouseId
                    AND BidUserId = (SELECT BidUserId FROM dbo.BidPrice WHERE BidUserPrice = @MaxPrice AND ActivitiesId = @tActivityId AND IsDel = 0)
        END 
    COMMIT TRAN
    SELECT  '1' AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作失败' AS Msg
END CATCH", bid);

                    var result = db.ExecuteStoreQuery<ESqlResult>(sql).ToList();

                    if (0 < result.Count)
                    {
                        return 0 < Convert.ToInt32(result[0].Code);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", "出价id:" + bid, ex);
                throw;
            }
        }

        #endregion
    }
}