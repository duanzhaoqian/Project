using System;
using System.Data.SqlClient;
using System.Linq;
using TXDal.NHouseActivies.APrice;
using TXOrm;
using System.Collections.Generic;
using TXModel.NHouseActivies.Discount;
using System.Text;
using System.Data.Objects;
using TXModel.Commons;
using TXModel;
using TXModel.NHouseActivies.Common;

namespace TXDal.NHouseActivies
{
    /// <summary>
    /// 活动公用类
    /// </summary>
    public class ActiviesUtilsDal : BaseDal_Admin
    {

        /// <summary>
        /// 根据楼盘编号获取正在参与活动的房源数量
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">楼盘编号</param>
        /// <returns>如果执行错误返回： -1</returns>
        public int GetLivingHouseCountByPremisesId(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    const string sql = @"
SELECT  COUNT(HouseId) AS Result
FROM    ActivitiesHouse (NOLOCK) AS a
        INNER JOIN Activities (NOLOCK) AS b ON b.Id = a.ActivitiesId
                                               AND GETDATE() < b.EndTime
WHERE   a.PremisesId = @PremisesId
        AND a.IsDel = 0";

                    var parms = new object[]
                    {
                        new SqlParameter("PremisesId", id)
                    };

                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql, parms).ToList();
                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id: {0}", id), ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据楼栋编号获取正在参与活动的房源数量
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">楼栋编号</param>
        /// <returns>如果执行错误返回： -1</returns>
        public int GetLivingHouseCountByBuildingId(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    const string sql = @"
SELECT  COUNT(HouseId) AS Result
FROM    ActivitiesHouse (NOLOCK) AS a
        INNER JOIN Activities (NOLOCK) AS b ON b.Id = a.ActivitiesId
                                               AND GETDATE() < b.EndTime
WHERE   a.BuildingId = @BuildingId
        AND a.IsDel = 0";

                    var parms = new object[]
                    {
                        new SqlParameter("BuildingId", id)
                    };

                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql, parms).ToList();
                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id: {0}", id), ex);
                return -1;
            }
        }

        /// <summary>
        /// 查询楼盘
        /// author: 李刚
        /// </summary>
        /// <param name="PremisesId">楼盘Id</param>
        /// <returns></returns>
        public List<PremiseModel> GetPremise(int developerId, int salesStatus)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @" select distinct p.Id,p.Name,p.DeveloperId,p.CityId
                                    from   Premises(nolock) as p
                                    inner  join House(nolock) as h on p.DeveloperId =h.DeveloperId and p.Id=h.PremisesId and h.IsDel=0
                                    where p.IsDel=0 and p.DeveloperId =@DeveloperId  ";
                    if (salesStatus != -1)
                    {
                        sql += " and p.SalesStatus=@SalesStatus ";
                    }
                    sql += @"   and h.Id not in (select ah.HouseId from  ActivitiesHouse as ah
                                    inner join Activities as act  on act.Id=ah.ActivitiesId and act.IsDel=0 and act.EndTime>getdate()
                                    where ah.IsDel=0 )";

                    var parameter = new SqlParameter[] { 
                        new SqlParameter(){ParameterName = "DeveloperId", Value = developerId },
                        new SqlParameter(){ParameterName = "SalesStatus", Value = salesStatus }
                    };
                    ObjectResult<PremiseModel> query = db.ExecuteStoreQuery<PremiseModel>(sql, parameter);
                    return query.ToList();

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("developerId: {0}", developerId), ex);
                return null;
            }
        }
        /// <summary>
        /// 查询楼盘
        /// author: 李刚
        /// </summary>
        /// <param name="PremisesId">楼盘Id</param>
        /// <returns></returns>
        public List<PremiseModel> GetPremise(int developerId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"select distinct Id,Name,DeveloperId,CityId
                                   from   Premises(nolock) 
                                   where  IsDel=0 and DeveloperId=@DeveloperId";

                    var parameter = new SqlParameter() { ParameterName = "DeveloperId", Value = developerId };

                    ObjectResult<PremiseModel> query = db.ExecuteStoreQuery<PremiseModel>(sql, parameter);
                    return query.ToList();

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("developerId: {0}", developerId), ex);
                return null;
            }
        }
        /// <summary>
        /// 查询楼栋
        /// author: 李刚
        /// </summary>
        /// <param name="PremisesId">楼盘Id</param>
        /// <returns></returns>
        public List<BuildingModel> GetBuilding(int PremisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"select distinct Id,Name,NameType
                                   from   Building(nolock)
                                   where  IsDel=0 and PremisesId=@PremisesId ";
                    var parameter = new SqlParameter() { ParameterName = "PremisesId", Value = PremisesId };

                    ObjectResult<BuildingModel> query = db.ExecuteStoreQuery<BuildingModel>(sql, parameter);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("PremisesId: {0}", PremisesId), ex);
                return null;
            }
        }

        /// <summary>
        /// 查新单元
        /// author: 李刚
        /// </summary>
        /// <param name="PremisesId">楼盘Id</param>
        /// <param name="BuildingId">楼栋Id</param>
        /// <returns></returns>
        public List<UnitModel> GetUnit(int PremisesId, int BuildingId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {

                    string sql = @"select distinct Num as Id,Name 
                                   from   Unit(nolock)
                                   where  IsDel=0 and PremisesId=@PremisesId and BuildingId=@BuildingId ";

                    var parameter = new SqlParameter[] { 
                        new SqlParameter(){ParameterName = "PremisesId", Value = PremisesId },
                        new SqlParameter(){ParameterName = "BuildingId", Value = BuildingId }
                    };

                    ObjectResult<UnitModel> query = db.ExecuteStoreQuery<UnitModel>(sql, parameter);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("PremisesId: {0},BuildingId:{1}", PremisesId, BuildingId), ex);
                return null;
            }
        }

        /// <summary>
        /// 查询楼层
        /// author: 李刚
        /// </summary>
        /// <param name="PremisesId">楼盘Id</param>
        /// <param name="BuildingId">楼栋Id</param>
        /// <param name="UnitId">单元Id</param>
        /// <returns></returns>
        public List<int> GetFloor(int PremisesId, int BuildingId, int UnitId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"select distinct Floor from House(nolock) 
                                   where IsDel=0 and PremisesId=@PremisesId and BuildingId=@BuildingId and UnitId=@UnitId ";

                    var parameter = new SqlParameter[] { 
                        new SqlParameter(){ParameterName = "PremisesId", Value = PremisesId },
                        new SqlParameter(){ParameterName = "BuildingId", Value = BuildingId },
                        new SqlParameter(){ParameterName = "UnitId", Value = UnitId }
                    };

                    ObjectResult<int> query = db.ExecuteStoreQuery<int>(sql, parameter);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("PremisesId: {0},BuildingId:{1},UnitId:{2}", PremisesId, BuildingId, UnitId), ex);
                return null;
            }
        }
        /// <summary>
        /// 查询楼层
        /// author: 李刚
        /// </summary>
        /// <param name="PremisesId">楼盘Id</param>
        /// <param name="BuildingId">楼栋Id</param>
        /// <param name="UnitId">单元Id</param>
        /// <returns></returns>
        public List<int> GetFloor(int PremisesId, int BuildingId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"select distinct Floor from House(nolock) 
                                   where IsDel=0 and PremisesId=@PremisesId and BuildingId=@BuildingId ";

                    var parameter = new SqlParameter[] { 
                        new SqlParameter(){ParameterName = "PremisesId", Value = PremisesId },
                        new SqlParameter(){ParameterName = "BuildingId", Value = BuildingId }
                    };

                    ObjectResult<int> query = db.ExecuteStoreQuery<int>(sql, parameter);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("PremisesId: {0},BuildingId:{1}", PremisesId, BuildingId), ex);
                return null;
            }
        }
        /// <summary>
        /// 查询房源
        /// 李刚
        /// </summary>
        /// <param name="developerId">开发商ID</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <param name="floor">楼层Id</param>
        /// <param name="status">销售状态</param>
        /// <param name="paging">分页类</param>
        /// <returns></returns>
        public Paging<HouseInfoModel> GetHouse(int developerId, int premisesId, int buildingId, int unitId, int floor, int status, Paging<HouseInfoModel> paging)
        {
            try
            {
                using (kyj_NewHouseDBEntities db = new kyj_NewHouseDBEntities())
                {
                    #region Sql语句
                    StringBuilder str = new StringBuilder();

                    str.Append("select * from ( select row_number() over (order by h.UpdateTime desc) as RowNum,h.Id,h.DeveloperId,h.PremisesId,h.BuildingId,h.UnitId,");
                    str.Append("h.Name as HouseNo,h.Floor,h.Room,h.Hall,h.Toilet,h.Kitchen,h.BuildingArea,h.SinglePrice,h.TotalPrice,h.CreateTime,h.SalesStatus,b.Name as BuildName,b.NameType ");
                    str.Append("from House (NOLOCK) as h  ");
                    str.Append("inner join Building(nolock) as b on b.PremisesId=h.PremisesId  and b.Id = h.BuildingId and b.IsDel=0 ");
                    str.Append("where h.IsDel=0 and h.Id  not in (");
                    str.Append("select ah.HouseId from  ActivitiesHouse(nolock) as ah inner join Activities(nolock) as act  on act.Id=ah.ActivitiesId and act.IsDel=0 and act.EndTime>getdate() where  ah.IsDel=0 ) ");
                    #endregion

                    #region Sql参数
                    List<SqlParameter> list = new List<SqlParameter>();

                    list.Add(new SqlParameter() { ParameterName = "StartNum", Value = (paging.PageIndex - 1) * paging.PageSize });
                    list.Add(new SqlParameter() { ParameterName = "EndNum", Value = paging.PageIndex * paging.PageSize });

                    if (developerId != -1)
                    {
                        str.Append("and h.DeveloperId=@DeveloperId ");
                        list.Add(new SqlParameter() { ParameterName = "DeveloperId", Value = developerId });
                    }
                    if (premisesId != -1)
                    {
                        str.Append("and h.PremisesId=@PremisesId ");
                        list.Add(new SqlParameter() { ParameterName = "PremisesId", Value = premisesId });
                    }
                    if (buildingId != -1)
                    {
                        str.Append("and h.BuildingId=@BuildingId ");
                        list.Add(new SqlParameter() { ParameterName = "BuildingId", Value = buildingId });
                    }
                    if (unitId != -1)
                    {
                        str.Append("and h.UnitId=@UnitId ");
                        list.Add(new SqlParameter() { ParameterName = "UnitId", Value = unitId });
                    }
                    if (floor != -100)
                    {
                        str.Append("and h.Floor=@Floor ");
                        list.Add(new SqlParameter() { ParameterName = "Floor", Value = floor });
                    }
                    if (status != -1)
                    {
                        str.Append("and h.SalesStatus=@SalesStatus ");
                        list.Add(new SqlParameter() { ParameterName = "SalesStatus", Value = status });
                    }


                    str.Append(" ) as T  where  T.RowNum>@StartNum and T.RowNum<=@EndNum ");
                    #endregion

                    ObjectResult<HouseInfoModel> query = db.ExecuteStoreQuery<HouseInfoModel>(str.ToString(), list.ToArray());

                    paging.ResultData = query.ToList();
                    paging.TotalCount = this.GetHouseCount(developerId, premisesId, buildingId, unitId, floor, status, paging);

                    return paging;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("developerId: {0},premisesId:{1},buildingId:{2},unitId:{3},floor:{4},status:{5},PageSize:{6},PageIndex:{7}", developerId, premisesId, buildingId, unitId, floor, status, paging.PageSize, paging.PageIndex), ex);
                return null;
            }
        }

        /// <summary>
        /// 查询房源数量
        /// 李刚
        /// </summary>
        /// <param name="developerId">开发商ID</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <param name="floor">楼层Id</param>
        /// <param name="status">销售状态</param>
        /// <param name="paging">分页类</param>
        /// <returns></returns>
        private int GetHouseCount(int developerId, int premisesId, int buildingId, int unitId, int floor, int status, Paging<HouseInfoModel> paging)
        {
            try
            {
                using (kyj_NewHouseDBEntities db = new kyj_NewHouseDBEntities())
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("select count(House.Id) ");
                    str.Append("from House (NOLOCK) ");
                    str.Append("where IsDel=0 and  not exists (select HouseId from  ActivitiesHouse (NOLOCK) where  HouseId=House.Id)  ");

                    List<SqlParameter> list = new List<SqlParameter>();
                    if (developerId != -1)
                    {
                        str.Append("and DeveloperId=@DeveloperId ");
                        list.Add(new SqlParameter() { ParameterName = "DeveloperId", Value = developerId });
                    }
                    if (premisesId != -1)
                    {
                        str.Append("and PremisesId=@PremisesId ");
                        list.Add(new SqlParameter() { ParameterName = "PremisesId", Value = premisesId });
                    }
                    if (buildingId != -1)
                    {
                        str.Append("and BuildingId=@BuildingId ");
                        list.Add(new SqlParameter() { ParameterName = "BuildingId", Value = buildingId });
                    }
                    if (unitId != -1)
                    {
                        str.Append("and UnitId=@UnitId ");
                        list.Add(new SqlParameter() { ParameterName = "UnitId", Value = unitId });
                    }
                    if (floor != -100)
                    {
                        str.Append("and Floor=@Floor ");
                        list.Add(new SqlParameter() { ParameterName = "Floor", Value = floor });
                    }
                    if (status != -1)
                    {
                        str.Append("and SalesStatus=@SalesStatus ");
                        list.Add(new SqlParameter() { ParameterName = "SalesStatus", Value = status });
                    }
                    ObjectResult<int> query = null;
                    if (list.Count > 0)
                    {
                        query = db.ExecuteStoreQuery<int>(str.ToString(), list.ToArray());
                    }
                    else
                    {
                        query = db.ExecuteStoreQuery<int>(str.ToString());
                    }

                    return query.ToList()[0];
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("developerId: {0},premisesId:{1},buildingId:{2},unitId:{3},floor:{4},status:{5},PageSize:{6},PageIndex:{7}", developerId, premisesId, buildingId, unitId, floor, status, paging.PageSize, paging.PageIndex), ex);
                return 0;
            }
        }

        /// <summary>
        /// 查询活动
        /// 李刚
        /// </summary>
        /// <param name="paging">分页类</param>
        /// <param name="type">活动类型</param>
        /// <returns></returns>
        public Paging<CT_Activity> GetActivityPageList(Paging<CT_Activity> paging, int type)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var query = from activity in db.Activities
                                join h in db.ActivitiesHouses on activity.Id equals h.ActivitiesId into ah
                                from activityHouse in ah.DefaultIfEmpty()
                                where !activity.IsDel && activity.Type == type
                                select new
                                {
                                    activity.Id,
                                    activity.Name,
                                    activity.HouseCount,
                                    activity.BeginTime,
                                    activity.EndTime,
                                    activity.Bond,
                                    activity.ActivitieState,
                                    activity.CreateTime,
                                    activityHouse.Discount
                                };
                    paging.TotalCount = query.Count();

                    paging.ResultData = query.OrderByDescending(it => it.CreateTime)
                                 .Skip((paging.PageIndex - 1) * paging.PageSize)
                                 .Take(paging.PageSize).ToList()
                                 .ConvertAll(it => new CT_Activity
                                 {
                                     Id = it.Id,
                                     Name = it.Name,
                                     BeginTime = it.BeginTime,
                                     EndTime = it.EndTime,
                                     HouseCount = it.HouseCount,
                                     CreateTime = it.CreateTime,
                                     Bond = it.Bond,
                                     ActivitieState = it.ActivitieState,
                                     MinDiscount = it.Discount
                                 });
                }
                return paging;
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("type:{0},PageSize:{1},PageIndex:{2}", type, paging.PageSize, paging.PageIndex), ex);
                return null;
            }
        }

        /// <summary>
        /// 查询活动
        /// 李刚
        /// </summary>
        /// <param name="paging">分页类</param>
        /// <param name="type">活动类型</param>
        /// <returns></returns>
        public Paging<CT_Activity> GetActivityPageList2(Paging<CT_Activity> paging, int type)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var activity = db.Activities;
                    var activityHouse = db.ActivitiesHouses;

                    var query = activity.Where(a => a.IsDel == false && a.Type == type).Select(a => new CT_Activity()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        BeginTime = a.BeginTime,
                        EndTime = a.EndTime,
                        HouseCount = a.HouseCount,
                        CreateTime = a.CreateTime,
                        Bond = a.Bond,
                        ActivitieState = a.ActivitieState,
                        ListDiscount = activityHouse.Where(ah => ah.ActivitiesId == a.Id && ah.IsDel == false).Select(ah => ah.Discount)
                    });

                    paging.TotalCount = query.Count();
                    paging.ResultData = query.OrderByDescending(it => it.CreateTime)
                                 .Skip((paging.PageIndex - 1) * paging.PageSize)
                                 .Take(paging.PageSize).ToList()
                                 .ConvertAll(it => new CT_Activity
                                 {
                                     Id = it.Id,
                                     Name = it.Name,
                                     BeginTime = it.BeginTime,
                                     EndTime = it.EndTime,
                                     HouseCount = it.HouseCount,
                                     CreateTime = it.CreateTime,
                                     Bond = it.Bond,
                                     ActivitieState = it.ActivitieState,
                                     ListDiscount = it.ListDiscount
                                 });
                }
                return paging;
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("type:{0},PageSize:{1},PageIndex:{2}", type, paging.PageSize, paging.PageIndex), ex);
                return null;
            }
        }


        /// <summary>
        /// 查询活动房源列表
        /// 李刚
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <param name="floor">楼层</param>
        /// <param name="activState">状态</param>
        /// <param name="type">活动类型</param>
        /// <param name="paging">分页</param>
        /// <returns></returns>
        public Paging<ActivityHouse> GetActivityHouseList(int developerId, int premisesId, int buildingId, int unitId, int floor, int activState, int type, Paging<ActivityHouse> paging)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    #region Sql语句
                    StringBuilder str = new StringBuilder();
                    str.Append("select * from ( ");
                    str.Append("select h.Id,ac.Type,row_number() over (order by ah.UpdateTime desc) as RowNum,");
                    str.Append("h.DeveloperId,h.PremisesId,h.BuildingId,h.UnitId,h.Floor,h.TotalPrice,h.SinglePrice,h.Name as HouseNo,");
                    str.Append("h.IsDel,ac.Name,ac.BidPrice,ac.AddPrice,ac.MaxPrice,ac.BeginTime,ac.EndTime,ah.CreateTime,");
                    str.Append("ac.Bond,ac.ActivitieState,b.Name as BuildName,b.NameType ");
                    str.Append("from House(nolock) as h  ");
                    str.Append("inner join Building(nolock) as b on b.PremisesId=h.PremisesId  and b.Id = h.BuildingId and b.IsDel=0 ");
                    str.Append("inner join ActivitiesHouse(nolock) as ah on ah.HouseId = h.Id  and ah.IsDel=0 ");
                    str.Append("inner join Activities(nolock) as ac on ac.Id = ah.ActivitiesId and ac.IsDel=0 ");
                    str.Append("where ac.Type=@Type and h.IsDel=0 and h.DeveloperId=@DeveloperId ");

                    #endregion

                    #region Sql参数
                    List<SqlParameter> list = new List<SqlParameter>();
                    list.Add(new SqlParameter() { ParameterName = "Type", Value = type });
                    list.Add(new SqlParameter() { ParameterName = "DeveloperId", Value = developerId });

                    if (premisesId != -1)
                    {
                        str.Append("and h.PremisesId=@PremisesId ");
                        list.Add(new SqlParameter() { ParameterName = "PremisesId", Value = premisesId });
                    }
                    if (buildingId != -1)
                    {
                        str.Append("and h.BuildingId=@BuildingId ");
                        list.Add(new SqlParameter() { ParameterName = "BuildingId", Value = buildingId });
                    }
                    if (unitId != -1)
                    {
                        str.Append("and h.UnitId=@UnitId ");
                        list.Add(new SqlParameter() { ParameterName = "UnitId", Value = unitId });
                    }
                    if (floor != -100)
                    {
                        str.Append("and h.Floor=@Floor ");
                        list.Add(new SqlParameter() { ParameterName = "Floor", Value = floor });
                    }
                    if (activState != -1)
                    {
                        str.Append("and ac.ActivitieState=@ActivitieState ");
                        list.Add(new SqlParameter() { ParameterName = "ActivitieState", Value = activState });
                    }

                    str.Append(" ) as T  where T.RowNum>@StartNum and T.RowNum<=@EndNum ");

                    list.Add(new SqlParameter() { ParameterName = "StartNum", Value = (paging.PageIndex - 1) * paging.PageSize });
                    list.Add(new SqlParameter() { ParameterName = "EndNum", Value = paging.PageIndex * paging.PageSize });

                    #endregion

                    ObjectResult<ActivityHouse> query = db.ExecuteStoreQuery<ActivityHouse>(str.ToString(), list.ToArray());
                    paging.TotalCount = this.GetHouseCount(developerId, premisesId, buildingId, unitId, floor, activState, type, paging);
                    paging.ResultData = query.ToList();


                    return paging;
                }

            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("developerId: {0},premisesId:{1},buildingId:{2},unitId:{3},floor:{4},activState:{5},PageSize:{6},PageIndex:{7}", developerId, premisesId, buildingId, unitId, floor, activState, paging.PageSize, paging.PageIndex), ex);
                return null;
            }
        }
        /// <summary>
        /// 查询活动房源数量
        /// 李刚
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <param name="unitId">单元Id</param>
        /// <param name="floor">楼层</param>
        /// <param name="activState">状态</param>
        /// <param name="type">活动类型</param>
        /// <param name="paging">分页</param>
        /// <returns></returns>
        private int GetHouseCount(int developerId, int premisesId, int buildingId, int unitId, int floor, int activState, int type, Paging<ActivityHouse> paging)
        {
            try
            {
                using (kyj_NewHouseDBEntities db = new kyj_NewHouseDBEntities())
                {
                    StringBuilder str = new StringBuilder();

                    str.Append("select   count(h.Id) ");
                    str.Append("from House(nolock) as h ");
                    str.Append("inner join ActivitiesHouse(nolock) as ah on ah.HouseId = h.Id and ah.IsDel=0 ");
                    str.Append("inner join Activities(nolock) as ac on ac.Id = ah.ActivitiesId and ac.IsDel=0 ");
                    str.Append("where ac.Type=@Type and h.IsDel=0 and h.DeveloperId=@DeveloperId ");

                    List<SqlParameter> list = new List<SqlParameter>();
                    list.Add(new SqlParameter() { ParameterName = "Type", Value = type });
                    list.Add(new SqlParameter() { ParameterName = "DeveloperId", Value = developerId });

                    if (premisesId != -1)
                    {
                        str.Append("and h.PremisesId=@PremisesId ");
                        list.Add(new SqlParameter() { ParameterName = "PremisesId", Value = premisesId });
                    }
                    if (buildingId != -1)
                    {
                        str.Append("and h.BuildingId=@BuildingId ");
                        list.Add(new SqlParameter() { ParameterName = "BuildingId", Value = buildingId });
                    }
                    if (unitId != -1)
                    {
                        str.Append("and h.UnitId=@UnitId ");
                        list.Add(new SqlParameter() { ParameterName = "UnitId", Value = unitId });
                    }
                    if (floor != -100)
                    {
                        str.Append("and h.Floor=@Floor ");
                        list.Add(new SqlParameter() { ParameterName = "Floor", Value = floor });
                    }
                    if (activState != -1)
                    {
                        str.Append("and ac.ActivitieState=@ActivitieState ");
                        list.Add(new SqlParameter() { ParameterName = "ActivitieState", Value = activState });
                    }
                    ObjectResult<int> query = null;
                    if (list.Count > 0)
                    {
                        query = db.ExecuteStoreQuery<int>(str.ToString(), list.ToArray());
                    }
                    else
                    {
                        query = db.ExecuteStoreQuery<int>(str.ToString());
                    }

                    return query.ToList()[0];
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李刚", string.Format("developerId: {0},premisesId:{1},buildingId:{2},unitId:{3},floor:{4},activState:{5},PageSize:{6},PageIndex:{7}", developerId, premisesId, buildingId, unitId, floor, activState, paging.PageSize, paging.PageIndex), ex);
                return 0;
            }
        }

        /// <summary>
        /// 创建活动
        /// 李刚
        /// </summary>
        /// <param name="activName">活动名称</param>
        /// <param name="bidPrice">起拍价格</param>
        /// <param name="addPrice">加价幅度</param>
        /// <param name="maxPrice">最大幅度</param>
        /// <param name="bond">活动保证金金额</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="ids">房源Id集合，以逗号分隔</param>
        public int CreateActivity(int cityId, int developerId, int type, DateTime? bStart, DateTime? bEnd, string activName, decimal bidPrice, decimal addPrice, decimal maxPrice, decimal bond, DateTime start, DateTime end, String ids, int activState)
        {
            int result = 0;
            using (kyj_NewHouseDBEntities db = new kyj_NewHouseDBEntities())
            {
                try
                {

                    #region SQL语句

                    StringBuilder sql = new StringBuilder();

                    #region 表Activities

                    sql.Append("begin try ");
                    sql.Append("begin tran ");

                    sql.Append("begin ");
                    sql.Append("insert into [Activities](");
                    sql.Append("[DeveloperId],[Name],[Type],[UserCount],[HouseCount],");
                    sql.Append("[BidPrice],[AddPrice],[MaxPrice],[SignupBeginTime],[SignupEndTime],");
                    sql.Append("[BeginTime],[EndTime],[Bond],[ActivitieState],[UpdateTime],[CreateTime],[IsDel]");
                    sql.Append(")values(");
                    sql.Append("@developerId,@Name,@Type,0,0,@bidPrice,@addPrice,@maxPrice,@SignupBeginTime,@SignupEndTime,@start,@end,@bond,@ActivitieState,getdate(),getdate(),0");
                    sql.Append(");");
                    #endregion

                    #region 表ActivitiesHouse

                    sql.AppendFormat("INSERT INTO [ActivitiesHouse] ");
                    sql.AppendFormat("([ActivitiesId],[CityId],[DeveloperId],[PremisesId],[BuildingId],[UnitId],[HouseId],[Discount],[UpdateTime],[CreateTime],[IsDel])");
                    sql.AppendFormat("select @@IDENTITY,{0},DeveloperId,PremisesId,BuildingId,UnitId,Id,0,getdate(),getdate(),0 from House where House.Id in ({1}) ", cityId, ids);


                    sql.Append("commit  ");
                    sql.Append("end ");
                    sql.Append("end try ");

                    sql.Append("begin catch ");
                    sql.Append("rollback tran ");
                    sql.Append("end catch  ");

                    #endregion

                    #endregion

                    #region SQL参数
                    List<SqlParameter> list = new List<SqlParameter>();
                    list.Add(new SqlParameter() { ParameterName = "developerId", Value = developerId });
                    list.Add(new SqlParameter() { ParameterName = "Name", Value = activName });
                    list.Add(new SqlParameter() { ParameterName = "Type", Value = type });
                    list.Add(new SqlParameter() { ParameterName = "bidPrice", Value = bidPrice });
                    list.Add(new SqlParameter() { ParameterName = "addPrice", Value = addPrice });
                    list.Add(new SqlParameter() { ParameterName = "maxPrice", Value = maxPrice });
                    list.Add(new SqlParameter() { ParameterName = "SignupBeginTime", Value = bStart ?? (object)DBNull.Value });
                    list.Add(new SqlParameter() { ParameterName = "SignupEndTime", Value = bEnd ?? (object)DBNull.Value });
                    list.Add(new SqlParameter() { ParameterName = "start", Value = start });
                    list.Add(new SqlParameter() { ParameterName = "end", Value = end });
                    list.Add(new SqlParameter() { ParameterName = "bond", Value = bond });
                    list.Add(new SqlParameter() { ParameterName = "ActivitieState", Value = activState });
                    #endregion

                    result = db.ExecuteStoreCommand(sql.ToString(), list.ToArray());
                }
                catch (Exception ex)
                {
                    Log4netService.RecordLog.RecordException("李刚", string.Format("developerId:{0},activName:{1},bidPrice:{2},addPrice:{3},maxPrice:{4},bond:{5},start:{6},end:{7},ids:{7}", developerId, activName, bidPrice, addPrice, maxPrice, bond, start, end, ids), ex);
                    throw;
                }
                return result;
            }
        }

        /// <summary>
        /// 根据活动编号获取活动房源信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<ActivitiesHouse> GetActivitiesHousesByActivityId(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  *
FROM    ActivitiesHouse (NOLOCK)
WHERE   ActivitiesId = {0}
        AND IsDel = 0";
                    sql = string.Format(sql, activityId);
                    var list = db.ExecuteStoreQuery<ActivitiesHouse>(sql).ToList();

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
        /// 获得单个活动详情
        /// author: maboxun
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public TXOrm.Activity GetActivityById(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var activity = db.Activities;
                    return activity.Where(a => a.Id == activityId && a.IsDel == false).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马波讯", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据活动编号获取活动房源信息
        /// author: maboxun
        /// </summary>
        /// <param name="activityId">活动编号</param>
        /// <returns></returns>
        public List<CT_ActivitiesHouse> GetActivitiesHousesById(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql= @"select 
                                h.Name as Name,p.Name as Premis,b.Name+b.NameType as Building,u.Name as Unit,
                                h.Floor,h.Room,h.Hall,h.Kitchen,h.Toilet,h.Area as BuildingArea,h.TotalPrice,h.SinglePrice,h.SalesStatus,ah.Discount
                                from ActivitiesHouse as ah
                                left join House as h on ah.HouseId=h.Id
                                left join Premises as p on ah.PremisesId=p.Id
                                left join Building as b on ah.BuildingId=b.Id
                                left join Unit as u on ah.UnitId=u.Num and ah.BuildingId=u.BuildingId
                                where ah.ActivitiesId={0}";

                    sql = string.Format(sql, activityId);
                    var list = db.ExecuteStoreQuery<CT_ActivitiesHouse>(sql).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马波讯", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }

        /// <summary>
        /// 获取阶梯团购折扣
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<TXOrm.Social> GetSocialByActivityId(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Socials.Where(s => s.ActivitiesId == activityId && s.IsDel == false).OrderByDescending(s=>s.Discount).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马波讯", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }
    }
}