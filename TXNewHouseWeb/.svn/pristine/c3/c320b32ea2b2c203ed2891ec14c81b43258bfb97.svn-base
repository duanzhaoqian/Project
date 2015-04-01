using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXDal.HouseData
{
    public partial class HouseDal : BaseDal_Admin
    {
        #region 根据搜索条件获取房源表信息
        /// <summary>
        /// 根据搜索条件获取房源表信息
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_House> GetPageList_BySearch_Admin(PVS_NH_House search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    //var query = from house in kyj.Houses
                    //            join building in kyj.Buildings on house.BuildingId equals building.Id
                    //            join premises in kyj.Premises on house.PremisesId equals premises.Id
                    //            where house.IsDel == false && building.IsDel == false && premises.IsDel == false
                    //            select new
                    //            {
                    //                house,
                    //                building.Name,
                    //                premisesName = premises.Name,
                    //                cityid = premises.CityId

                    //            };
                    StringBuilder sql = new StringBuilder(@" SELECT *
                                    FROM   ( SELECT    
                                    ROW_NUMBER() OVER ( ORDER BY dbo.House.CreateTime DESC ) AS Row_Num ,dbo.house.*
                                    FROM    dbo.House
                                            INNER JOIN dbo.Building ON dbo.House.BuildingId = building.Id
                                            INNER JOIN dbo.Premises ON house.PremisesId = premises.Id
                                    WHERE   dbo.House.IsDel = 0
                                            AND dbo.Building.IsDel = 0
                                            AND dbo.Premises.IsDel = 0");
                    #region 筛选
                    //楼盘
                    if (search.PremisesId > 0)
                    {
                        sql.AppendFormat("   AND dbo.House.PremisesId={0}", search.PremisesId);
                        //query = query.Where(it => it.house.PremisesId == search.PremisesId);
                    }
                    //楼栋
                    if (search.BuildingId > 0)
                    {
                        sql.AppendFormat("   AND dbo.House.BuildingId={0}", search.BuildingId);
                        //query = query.Where(it => it.house.BuildingId == search.BuildingId);
                    }
                    //单元
                    if (search.UnitId != 0)
                    {
                        sql.AppendFormat("   AND dbo.House.UnitId={0}", search.UnitId);
                        //query = query.Where(it => it.house.UnitId == search.UnitId);
                    }
                    //楼层
                    if (search.Floor != 0)
                    {
                        sql.AppendFormat("   AND dbo.House.Floor={0}", search.Floor);
                        //query = query.Where(it => it.house.Floor == search.Floor);
                    }
                    //销售状态 

                    if (search.SalesStatus > -1)
                    {
                        sql.AppendFormat("   AND dbo.House.SalesStatus={0}", search.SalesStatus);
                        //query = query.Where(it => it.house.SalesStatus == search.SalesStatus);
                    }
                    #endregion
                    #region 返回数据

                    sql.AppendFormat(") temp where Row_Num BETWEEN {0} and {1}", (pageIndex - 1) * pageSize + 1,
                                     pageIndex * pageSize);
                    return kyj.ExecuteStoreQuery<PVM_NH_House>(sql.ToString()).ToList();
                    //return
                    //    query.OrderByDescending(it => it.house.CreateTime)
                    //        .Skip((pageIndex - 1) * pageSize)
                    //        .Take(pageSize)
                    //        .ToList()
                    //        .ConvertAll(it => new PVM_NH_House
                    //        {
                    //            Id = it.house.Id,
                    //            Name = it.house.Name,
                    //            InnerCode = it.house.InnerCode,
                    //            DeveloperId = it.house.DeveloperId,
                    //            PremisesId = it.house.PremisesId,
                    //            BuildingId = it.house.BuildingId,
                    //            UnitId = it.house.UnitId,
                    //            Floor = it.house.Floor,
                    //            RId = it.house.RId,
                    //            BuildingType = it.house.BuildingType,
                    //            Orientation = it.house.Orientation,
                    //            PriceType = it.house.PriceType,
                    //            TotalPrice = it.house.TotalPrice,
                    //            SinglePrice = it.house.SinglePrice,
                    //            DownPayment = it.house.DownPayment,
                    //            SalesStatus = it.house.SalesStatus,
                    //            BuildingArea = it.house.BuildingArea,
                    //            Area = it.house.Area,
                    //            Room = it.house.Room,
                    //            Hall = it.house.Hall,
                    //            Toilet = it.house.Toilet,
                    //            Kitchen = it.house.Kitchen,
                    //            //Balcony = it.house.Balcony,
                    //            CreateTime = it.house.CreateTime,
                    //            UpdateTime = it.house.UpdateTime,
                    //            IsDel = it.house.IsDel,
                    //            BuildingName = it.Name,
                    //            PremisesName = it.premisesName,
                    //            CityId = it.cityid
                    //        });

                    #endregion
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("马欢", search, e);
                throw;
            }
        }
        #endregion

        #region 根据搜索条件获取房源表信息记录数
        /// <summary>
        /// 根据搜索条件获取房源表信息记录数
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_House search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    StringBuilder sql = new StringBuilder(@"SELECT    
                                    COUNT(1) count
                                    FROM    dbo.House
                                            INNER JOIN dbo.Building ON dbo.House.BuildingId = building.Id
                                            INNER JOIN dbo.Premises ON house.PremisesId = premises.Id
                                    WHERE   dbo.House.IsDel = 0
                                            AND dbo.Building.IsDel = 0
                                            AND dbo.Premises.IsDel = 0");
                    #region 筛选
                    //楼盘
                    if (search.PremisesId > 0)
                    {
                        sql.AppendFormat("   AND dbo.House.PremisesId={0}", search.PremisesId);
                        //query = query.Where(it => it.house.PremisesId == search.PremisesId);
                    }
                    //楼栋
                    if (search.BuildingId > 0)
                    {
                        sql.AppendFormat("   AND dbo.House.BuildingId={0}", search.BuildingId);
                        //query = query.Where(it => it.house.BuildingId == search.BuildingId);
                    }
                    //单元
                    if (search.UnitId != 0)
                    {
                        sql.AppendFormat("   AND dbo.House.UnitId={0}", search.UnitId);
                        //query = query.Where(it => it.house.UnitId == search.UnitId);
                    }
                    //楼层
                    if (search.Floor != 0)
                    {
                        sql.AppendFormat("   AND dbo.House.Floor={0}", search.Floor);
                        //query = query.Where(it => it.house.Floor == search.Floor);
                    }
                    //销售状态 

                    if (search.SalesStatus > -1)
                    {
                        sql.AppendFormat("   AND dbo.House.SalesStatus={0}", search.SalesStatus);
                        //query = query.Where(it => it.house.SalesStatus == search.SalesStatus);
                    }
                    #endregion
                    return kyj.ExecuteStoreQuery<int>(sql.ToString()).FirstOrDefault();
                    //var query = from house in kyj.Houses
                    //            join building in kyj.Buildings on house.BuildingId equals building.Id
                    //            join premises in kyj.Premises on house.PremisesId equals premises.Id
                    //            where house.IsDel == false && building.IsDel == false && premises.IsDel == false
                    //            select house;
                    //#region 筛选
                    ////楼盘
                    //if (search.PremisesId > 0)
                    //{
                    //    query = query.Where(it => it.PremisesId == search.PremisesId);
                    //}
                    ////楼栋
                    //if (search.BuildingId > 0)
                    //{
                    //    query = query.Where(it => it.BuildingId == search.BuildingId);
                    //}
                    ////单元
                    //if (search.UnitId != 0)
                    //{
                    //    query = query.Where(it => it.UnitId == search.UnitId);
                    //}
                    ////楼层
                    //if (search.Floor != 0)
                    //{
                    //    query = query.Where(it => it.Floor == search.Floor);
                    //}
                    ////销售状态

                    //if (search.SalesStatus > -1)
                    //{
                    //    query = query.Where(it => it.SalesStatus == search.SalesStatus);
                    //}
                    //#endregion
                    //#region 返回数据

                    //return query.Count();

                    //#endregion
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("马欢", search, e);
                throw;
            }
        }
        #endregion

        #region 更新房源信息
        /// <summary>
        /// 更新房源信息
        /// </summary>
        /// <param name="entity">房源实体</param>
        /// <returns>返回：0 失败 1 成功 2 正则参加活动</returns>
        public int UpdateNHouse_Admin(PVM_NH_House entity)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    //var activcount = from ac in kyj.Activities
                    //                 join house in kyj.ActivitiesHouses on ac.Id equals house.ActivitiesId
                    //                 where ac.IsDel == false && house.IsDel == false && house.HouseId == entity.Id
                    //                 select house;
                    //if (activcount.Count() > 0)
                    //{
                    //    return 2;
                    //}
                    //var query = kyj.Houses.FirstOrDefault(it => it.Id == entity.Id && it.IsDel == false);
                    //if (query != null)
                    //{
                    //    query.Name = entity.Name;
                    //    query.InnerCode = entity.InnerCode;
                    //    query.DeveloperId = entity.DeveloperId;
                    //    query.PremisesId = entity.PremisesId;
                    //    query.BuildingId = entity.BuildingId;
                    //    query.UnitId = entity.UnitId;
                    //    query.Floor = entity.Floor;
                    //    query.RId = entity.RId;

                    //    query.BuildingType = entity.BuildingType;
                    //    query.Orientation = entity.Orientation;

                    //    query.PriceType = entity.PriceType;
                    //    query.TotalPrice = entity.TotalPrice;
                    //    query.SinglePrice = entity.SinglePrice;
                    //    query.DownPayment = entity.DownPayment;

                    //    query.SalesStatus = entity.SalesStatus;

                    //    query.BuildingArea = entity.BuildingArea;
                    //    query.Area = entity.Area;

                    //    query.Room = entity.Room;
                    //    query.Hall = entity.Hall;
                    //    query.Toilet = entity.Toilet;
                    //    query.Kitchen = entity.Kitchen;
                    //    //query.Balcony = entity.Balcony;
                    //    query.CoordX = entity.CoordX;
                    //    query.CoordY = entity.CoordY;

                    //    query.UpdateTime = DateTime.Now;
                    //    return kyj.SaveChanges();

                    //}
                    string sql = @"IF EXISTS (SELECT  *
            FROM    dbo.Activities ac
                    INNER JOIN dbo.ActivitiesHouse house ON ac.Id = house.ActivitiesId
            WHERE   ac.IsDel = 0
                    AND house.IsDel = 0
                    AND house.HouseId = @ID )--房源是否参加活动
                    BEGIN
                        SELECT  2
                    END
                ELSE
                    BEGIN
                        UPDATE  dbo.House
                        SET     Name = @Name ,
                                InnerCode = @InnerCode ,
                                DeveloperId = @DeveioperId ,
                                PremisesId = @PremisesId ,
                                BuildingId = @BuildingId ,
                                UnitId = @UnitId ,
                                Floor = @Floor ,
                                RId = @Rid ,
                                BuildingType = @BuildingType ,
                                Orientation = @Orientation ,
                                PriceType = @PriceType ,
                                TotalPrice = @TotalPrice ,
                                SinglePrice = @SinglePrice ,
                                DownPayment = @DownPayment ,
                                SalesStatus = @SalesStatus ,
                                BuildingArea = @BuildingArea ,
                                Area = @Area ,
                                Room = @Room ,
                                Hall = @Hall ,
                                Toilet = @Toilet ,
                                Kitchen = @Kitchen ,
                                CoordX = @CoordX ,
                                CoordY = @CoordY ,
                                UpdateTime = GETDATE()
                        WHERE   Id = @ID and IsDel=0
                        SELECT  0
                    END";
                    SqlParameter[] sqlparams =
                        {
                            new SqlParameter("@ID",entity.Id),
                            new SqlParameter("@Name",entity.Name),
                            new SqlParameter("@InnerCode",entity.InnerCode),
                            new SqlParameter("@DeveioperId",entity.DeveloperId),
                            new SqlParameter("@PremisesId",entity.PremisesId),
                            new SqlParameter("@BuildingId",entity.BuildingId),
                            new SqlParameter("@UnitId",entity.UnitId),
                            new SqlParameter("@Floor",entity.Floor),
                            new SqlParameter("@Rid",entity.RId),
                            new SqlParameter("@BuildingType",entity.BuildingType),
                            new SqlParameter("@Orientation",entity.Orientation),
                            new SqlParameter("@PriceType",entity.PriceType),
                            new SqlParameter("@TotalPrice",entity.TotalPrice),
                            new SqlParameter("@SinglePrice",entity.SinglePrice),
                            new SqlParameter("@DownPayment",entity.DownPayment),
                            new SqlParameter("@SalesStatus",entity.SalesStatus),
                            new SqlParameter("@BuildingArea",entity.BuildingArea),
                            new SqlParameter("@Area",entity.Area),
                            new SqlParameter("@Room",entity.Room),
                            new SqlParameter("@Hall",entity.Hall),
                            new SqlParameter("@Toilet",entity.Toilet),
                            new SqlParameter("@Kitchen",entity.Kitchen),
                            new SqlParameter("@CoordX",entity.CoordX),
                            new SqlParameter("@CoordY",entity.CoordY)
                        };
                    return kyj.ExecuteStoreQuery<int>(sql, sqlparams).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("马欢", entity, e);
                return 0;
            }
        }
        #endregion

        #region 根据楼栋编号集获取房源数量

        /// <summary>
        /// 根据楼栋编号集获取房源数量
        /// author: liyuzhao
        /// </summary>
        /// <param name="buildingIds">楼栋编号集(逗号分隔)</param>
        /// <returns></returns>
        public List<PVS_NH_ForHouseCount> GetHouseCountByBuildingId(string buildingIds)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
                        SELECT  BuildingId, COUNT(1) AS HouseCount
                        FROM    House (NOLOCK)
                        WHERE   BuildingId IN ( {0} )
                                AND IsDel = 0
                        GROUP BY BuildingId";
                    sql = string.Format(sql, buildingIds);

                    var list = db.ExecuteStoreQuery<PVS_NH_ForHouseCount>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return list;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("buildingIds:{0}", buildingIds), ex);
                throw;
            }
        }

        #endregion

        #region 删除多个房源
        /// <summary>
        /// 删除多个房源
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        public int DeleteHouseByIds(string ids)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format("UPDATE House SET IsDel=1 WHERE Id IN ({0})", ids);
                    var count = kyj.ExecuteStoreCommand(sql);
                    return count;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("房源id({0})", ids), e);
                return 0;
            }
        }

        #endregion

        #region 更新多个房源销售状态

        /// <summary>
        /// 更新多个房源销售状态
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <param name="state">销售状态</param>
        /// <returns></returns>
        public int UpdateHouseSalesStatusByIds(string ids, int state)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    //string sql = string.Format("UPDATE House SET SalesStatus={0} WHERE Id IN ({1})", state,ids);
                    //更新房源销售状态 楼栋状态为3 售罄 房源为9 售罄
                    //楼栋状态为 6 开发商保留 房源为 1开发商保留
                    string sql = string.Format(@"UPDATE House  SET 
                                                SalesStatus=(
                                                SELECT (
                                                CASE WHEN j.STATE=3 THEN 9 ELSE 
	                                                CASE WHEN j.STATE=6 THEN 1 ELSE 
	                                                 {0}
	                                                END
                                                END
                                                )FROM
                                                (SELECT b.State,h.SalesStatus FROM dbo.Building b
                                                JOIN dbo.House h ON b.Id=h.BuildingId
                                                WHERE h.Id=House.Id) AS j
                                                ) 
                                                WHERE House.Id IN ({1})", state, ids);
                    var count = kyj.ExecuteStoreCommand(sql);
                    return count;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("房源id({0})状态[{1}]", ids, state), e);
                return 0;
            }
        }

        #endregion

        #region 检测房源集不可更新的房源
        /// <summary>
        /// 检测房源集不可更新的房源
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        public List<HouseIdAndName> CheckNotUpdateHouses(string ids)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"SELECT h.Id,h.Name FROM dbo.House h
JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
AND h.Id IN ({0}) GROUP BY h.Id,h.Name", ids);
                    var result = kyj.ExecuteStoreQuery<HouseIdAndName>(sql).ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("房源id({0})", ids), e);
                return null;
            }
        }
        #endregion

        #region 添加房源
        /// <summary>
        /// 添加房源
        /// </summary>
        /// <param name="model">房源实体</param>
        /// <returns>返回：新添加房源编号</returns>
        public int AddHouse_Admin(House model)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    strSql.Append(@"INSERT INTO [dbo].[House]
                                       ([Name]
                                       ,[InnerCode]
                                       ,[DeveloperId]
                                       ,[PremisesId]
                                       ,[BuildingId]
                                       ,[UnitId]
                                       ,[Floor]
                                       ,[RId]
                                       ,[BuildingType]
                                       ,[Orientation]
                                       ,[PriceType]
                                       ,[TotalPrice]
                                       ,[SinglePrice]
                                       ,[DownPayment]
                                       ,[SalesStatus]
                                       ,[BuildingArea]
                                       ,[Area]
                                       ,[Room]
                                       ,[Hall]
                                       ,[Toilet]
                                       ,[Kitchen]
                                       ,[CreateTime]
                                       ,[UpdateTime]
                                       ,[IsDel]
                                       ,[IsRelease]
                                        ,[CoordX]
                                        ,[CoordY])
                                 VALUES(@Name,
                                        @InnerCode,
                                        @DeveloperId,
                                        @PremisesId,
                                        @BuildingId,
                                        @UnitId,
                                        @Floor,
                                        @RId,
                                        @BuildingType,
                                        @Orientation,
                                        @PriceType,
                                        @TotalPrice,
                                        @SinglePrice,
                                        @DownPayment,
                                        @SalesStatus,
                                        @BuildingArea,
                                        @Area,
                                        @Room,
                                        @Hall,
                                        @Toilet,
                                        @Kitchen,
                                        @CreateTime,
                                        @UpdateTime,
                                        @IsDel,
                                        @IsRelease,
                                        @CoordX,
                                        @CoordY)");
                    strSql.Append(@" 
                                    IF ( @@ERROR > 0 ) 
                                    BEGIN
                                        SELECT  '0' AS Code, '操作失败' AS Msg
                                        RETURN
                                    END
                                    SELECT  STR(@@IDENTITY) AS Code, '操作成功' AS Msg
                                  ");
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Name",model.Name),
                        new SqlParameter("@InnerCode",model.InnerCode),
                        new SqlParameter("@DeveloperId",model.DeveloperId),
                        new SqlParameter("@PremisesId",model.PremisesId),
                        new SqlParameter("@BuildingId",model.BuildingId),
                        new SqlParameter("@UnitId",model.UnitId),
                        new SqlParameter("@Floor",model.Floor),
                        new SqlParameter("@RId",model.RId),
                        new SqlParameter("@BuildingType",model.BuildingType),
                        new SqlParameter("@Orientation",model.Orientation),
                        new SqlParameter("@PriceType",model.PriceType),
                        new SqlParameter("@TotalPrice",model.TotalPrice),
                        new SqlParameter("@SinglePrice",model.SinglePrice),
                        new SqlParameter("@DownPayment",model.DownPayment),
                        new SqlParameter("@SalesStatus",model.SalesStatus),
                        new SqlParameter("@BuildingArea",model.BuildingArea),
                        new SqlParameter("@Area",model.Area),
                        new SqlParameter("@Room",model.Room),
                        new SqlParameter("@Hall",model.Hall),
                        new SqlParameter("@Toilet",model.Toilet),
                        new SqlParameter("@Kitchen",model.Kitchen),
                        new SqlParameter("@CreateTime",model.CreateTime),
                        new SqlParameter("@UpdateTime",model.UpdateTime),
                        new SqlParameter("@IsDel",model.IsDel),
                         new SqlParameter("@IsRelease",model.IsRelease),
                          new SqlParameter("@CoordX",model.CoordX),
                           new SqlParameter("@CoordY",model.CoordY)
                    };

                    var result = dbEnt.ExecuteStoreQuery<ESqlResult>(strSql.ToString(), parameters).ToList();
                    if (0 < result.Count)
                    {
                        return Convert.ToInt32(result[0].Code);
                    }
                    return 0;
                }
            }
            catch (System.Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", model, e);
                return 0;
            }
        }
        #endregion

        #region 根据房源id获取城市id
        /// <summary>
        /// 根据房源id获取城市id
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public int GetCityIdByHouseId(int hid)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    //var query = (from p in kyj.Premises
                    //             join h in kyj.Houses on p.Id equals h.PremisesId
                    //             where h.Id == hid // 发送消息时已经将其标记为删除，因此查询不到数据 && h.IsDel == false
                    //             select p).FirstOrDefault();
                    //if (query != null)
                    //    return query.CityId;
                    string sql = string.Format(@"SELECT  CityId
                                    FROM    dbo.Premises p
                                            INNER JOIN dbo.House h ON p.Id = h.PremisesId
                                    WHERE   h.Id = {0}", hid);
                    return kyj.ExecuteStoreQuery<int>(sql).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马欢", hid, ex);
                return 0;
            }
        }
        #endregion

        #region 检查房源名称重复

        /// <summary>
        /// 检查房源名称重复
        /// author: liyuzhao
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public int GetHouseNameCountByIdAndName(House house)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                    SELECT  COUNT(1) AS Result
                                    FROM    House (NOLOCK)
                                    WHERE   Name = '{0}'
                                            AND BuildingId = {1}
                                            AND IsDel = 0
                                            AND UnitId = {2}", house.Name, house.BuildingId, house.UnitId);

                    var list = kyj.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 1;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", house, ex);
                return 1;
            }
        }

        /// <summary>
        /// 检查房源名称重复(更新信息)
        /// author: liyuzhao
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public int GetHouseNameCountByIdAndName_Update(House house)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                    SELECT  COUNT(1) AS Result
                                    FROM    House (NOLOCK)
                                    WHERE   Name = '{0}'
                                            AND BuildingId = {1}
                                            AND Id <> {2}
                                            AND IsDel = 0
                                            AND UnitId = {3}", house.Name, house.BuildingId, house.Id, house.UnitId);

                    var list = kyj.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 1;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", house, ex);
                return 1;
            }
        }

        #endregion

        #region 房源详情

        /// <summary>
        /// 房源详情
        /// author: liyuzhao
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public PVE_NH_House_Detail GetHouseDetailByHouseId(int houseId)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  b.Name AS PremisesName, c.Name + c.NameType AS BuildingName, a.Name AS HouseName, d.Name AS UnitName, a.[Floor],
        a.Room, a.Hall, a.Toilet, a.Kitchen, a.BuildingType, a.Orientation, a.PriceType, a.TotalPrice, a.SinglePrice,
        a.DownPayment, a.SalesStatus, a.BuildingArea, a.Area, a.RId, a.CoordX, a.CoordY
FROM    House (NOLOCK) AS a
        LEFT JOIN Premises (NOLOCK) AS b ON b.Id = a.PremisesId
        LEFT JOIN Building (NOLOCK) AS c ON c.Id = a.BuildingId
        LEFT JOIN Unit (NOLOCK) AS d ON d.Num = a.UnitId
                                        AND d.BuildingId = a.BuildingId
WHERE   a.Id = {0}
        AND a.IsDel = 0", houseId);

                    var list = kyj.ExecuteStoreQuery<PVE_NH_House_Detail>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return list[0];
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("houseId:{0}", houseId), ex);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 根据删除的户型图编号重置房源表->户型图关联
        /// </summary>
        /// <param name="id"></param>
        public void ResetHouseRIdByDelPicId(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                        UPDATE  House
                        SET     RId = -1
                        WHERE   RId = {0}", id);
                    db.ExecuteStoreCommand(sql);
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id:{0}", id), ex);
            }
        }

        /// <summary>
        /// 根据楼栋编号获取房源编号集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<int> GetHouseIdListByBuildingId(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                        SELECT  Id
                        FROM    House (NOLOCK)
                        WHERE   BuildingId = {0}", id);
                    var list = db.ExecuteStoreQuery<int>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id:{0}", id), ex);

                return null;
            }
        }
    }
}