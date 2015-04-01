using TXOrm;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TXModel.Commons;
using System.Data.SqlClient;
using TXModel.Web;
using System.Data.Common;

namespace TXDal.HouseData
{
    public partial class HouseDal
    {
        #region 房源列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/4 10:40:57
        /// 功能描述：房源分页列表(未删除房源)
        /// </summary>
        /// <param name="DeveloperId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<HouseAndBuilding> GetHouseList(Paging<HouseAndBuilding> paging, int DeveloperId, int pid, int bid, int uid, int fid, int sid, bool IsRelease, int order)
        {

            StringBuilder sql = new StringBuilder();
            StringBuilder sqlCount = new StringBuilder();


            #region 拼接分页SQL

            sql.AppendFormat(@"SELECT * FROM (
            SELECT ROW_NUMBER() OVER(ORDER BY house.UpdateTime {0}) AS Row, house.Id,house.[Name] as HouseName,
            BuildingName=(select [Name] from Building where id=house.BuildingId),

            NameType=(select NameType from Building where id=house.BuildingId),

Unit.[Name] as UnitName

,[Floor],Room,Hall,Toilet,Kitchen,Area,SinglePrice,TotalPrice,SalesStatus,house.UpdateTime   FROM House 
			inner join Unit on Unit.BuildingId=House.BuildingId  inner join Building   on Building.id=House.BuildingId  and num=unitid
            And house.DeveloperId=@DeveloperId ", order == 0 ? "ASC" : "DESC");

            #endregion

            #region 拼接总记录SQL
            sqlCount.Append(@"SELECT COUNT(1) FROM House inner join Unit on Unit.BuildingId=House.BuildingId  inner join Building   on Building.id=House.BuildingId  and num=unitid  and house.DeveloperId=@DeveloperId");
            #endregion

            #region  //条件1 只选择楼盘

            if (pid != 0 && bid == 0 && uid == 0 && fid == 0)
            {
                if (sid == -1)
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId ");
                }
                else
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and SalesStatus=@SalesStatus ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and SalesStatus=@SalesStatus ");
                }
            }
            #endregion

            #region    //条件2 选择楼盘 楼栋
            else if (pid != 0 && bid != 0 && uid == 0 && fid == 0)
            {
                if (sid == -1)
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId ");

                }
                else
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and SalesStatus=@SalesStatus ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and SalesStatus=@SalesStatus ");
                }

            }
            #endregion

            #region//条件3 选择楼盘 楼栋 单元
            else if (pid != 0 && bid != 0 && uid != 0 && fid == 0)
            {
                if (sid == -1)
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId ");

                }
                else
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId  and SalesStatus=@SalesStatus ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId  and SalesStatus=@SalesStatus ");
                }
            }
            #endregion

            #region    //条件4 选择楼盘 楼栋 单元 楼层

            else if (pid != 0 && bid != 0 && uid != 0 && fid != 0)
            {
                if (sid == -1)
                {

                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor");

                }
                else
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor and SalesStatus=@SalesStatus ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor and SalesStatus=@SalesStatus ");
                }

            }
            #endregion

            #region  //条件5 选择楼盘 楼栋 楼层

            else if (pid != 0 && bid != 0 && uid == 0 && fid != 0)
            {
                if (sid == -1)
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor ");


                }
                else
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor and SalesStatus=@SalesStatus ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor and SalesStatus=@SalesStatus ");

                }
            }
            else
            {
                if (sid == -1)
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0   and Building.IsDel = 0  ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0  and Building.IsDel = 0  ");
                }
                else
                {
                    sql.Append(" and IsRelease=@IsRelease and house.IsDel=0 and SalesStatus=@SalesStatus  ");
                    sqlCount.Append(" and IsRelease=@IsRelease and house.IsDel=0 and SalesStatus=@SalesStatus ");
                }
            }
            #endregion

            sql.Append(string.Format(" ) AS tempTable WHERE Row BETWEEN {0} AND  {1}", ((paging.PageIndex - 1) * paging.PageSize) + 1, paging.PageIndex * paging.PageSize));

            SqlParameter[] parameters =
                    {
                        
                        new SqlParameter("@DeveloperId",DeveloperId),
                        new SqlParameter("@PremisesId",pid),
                        new SqlParameter("@BuildingId",bid),
                        new SqlParameter("@UnitId",uid),
                        new SqlParameter("@Floor", fid),
                        new SqlParameter("@SalesStatus", sid),
                        new SqlParameter("@IsRelease",IsRelease)
                       
                    };

            SqlParameter[] parameters1 =
                    {
                        
                        new SqlParameter("@DeveloperId",DeveloperId),
                        new SqlParameter("@PremisesId",pid),
                        new SqlParameter("@BuildingId",bid),
                        new SqlParameter("@UnitId",uid),
                        new SqlParameter("@Floor", fid),
                        new SqlParameter("@SalesStatus", sid),
                        new SqlParameter("@IsRelease",IsRelease)
                     
                    };

            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    var query = db.ExecuteStoreQuery<HouseAndBuilding>(sql.ToString(), parameters);
                    List<HouseAndBuilding> p = query.ToList();
                    paging.ResultData = p;
                    #endregion

                    #region 查询总记录数据
                    var queryCount = db.ExecuteStoreQuery<int>(sqlCount.ToString(), parameters1);
                    paging.TotalCount = queryCount.FirstOrDefault<int>();
                    #endregion
                }
                return paging;
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0},{1},{2}", DeveloperId, pid, uid), ex);
                throw;
            }




        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/4 10:40:57
        /// 功能描述：房源分页列表(已经删除房源)
        /// </summary>
        /// <param name="DeveloperId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<HouseAndBuilding> GetHouseList(Paging<HouseAndBuilding> paging, int DeveloperId, int pid, int bid, int uid, int fid, int sid)
        {

            StringBuilder sql = new StringBuilder();
            StringBuilder sqlCount = new StringBuilder();


            #region 拼接分页SQL

            sql.Append(@"SELECT * FROM (
            SELECT ROW_NUMBER() OVER(ORDER BY house.UpdateTime DESC) AS Row, house.Id,house.[Name] as HouseName,
            BuildingName=(select [Name] from Building where id=house.BuildingId),

            NameType=(select NameType from Building where id=house.BuildingId),

Unit.[Name] as UnitName

,[Floor],Room,Hall,Toilet,Kitchen,Area,SinglePrice,TotalPrice,SalesStatus,house.UpdateTime   FROM House 
			inner join Unit on Unit.BuildingId=House.BuildingId and num=unitid

            And house.DeveloperId=@DeveloperId ");

            #endregion

            #region 拼接总记录SQL
            sqlCount.Append(@"SELECT COUNT(1) FROM House inner join Unit on Unit.BuildingId=House.BuildingId and num=unitid and house.DeveloperId=@DeveloperId");
            #endregion

            #region  //条件1 只选择楼盘

            if (pid != 0 && bid == 0 && uid == 0 && fid == 0)
            {
                if (sid == -1)
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId ");
                }
                else
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and SalesStatus=@SalesStatus ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and SalesStatus=@SalesStatus ");
                }
            }
            #endregion

            #region    //条件2 选择楼盘 楼栋
            else if (pid != 0 && bid != 0 && uid == 0 && fid == 0)
            {
                if (sid == -1)
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and BuildingId=@BuildingId ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and BuildingId=@BuildingId ");

                }
                else
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and SalesStatus=@SalesStatus ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and SalesStatus=@SalesStatus ");
                }

            }
            #endregion

            #region//条件3 选择楼盘 楼栋 单元
            else if (pid != 0 && bid != 0 && uid != 0 && fid == 0)
            {
                if (sid == -1)
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId ");

                }
                else
                {
                    sql.Append("  and IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and SalesStatus=@SalesStatus  and UnitId=@UnitId   ");
                    sqlCount.Append("  and IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and SalesStatus=@SalesStatus  and UnitId=@UnitId   ");
                }
            }
            #endregion

            #region    //条件4 选择楼盘 楼栋 单元 楼层

            else if (pid != 0 && bid != 0 && uid != 0 && fid != 0)
            {
                if (sid == -1)
                {

                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor");

                }
                else
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor and SalesStatus=@SalesStatus ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId and UnitId=@UnitId and Floor=@Floor and SalesStatus=@SalesStatus ");
                }

            }
            #endregion

            #region  //条件5 选择楼盘 楼栋 楼层

            else if (pid != 0 && bid != 0 && uid == 0 && fid != 0)
            {
                if (sid == -1)
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor ");


                }
                else
                {
                    sql.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor and SalesStatus=@SalesStatus ");
                    sqlCount.Append("  and house.IsDel=1 and house.PremisesId=@PremisesId and house.BuildingId=@BuildingId  and Floor=@Floor and SalesStatus=@SalesStatus ");

                }
            }
            else
            {
                if (sid == -1)
                {
                    sql.Append("  and house.IsDel=1   ");
                    sqlCount.Append("  and house.IsDel=1  ");
                }
                else
                {
                    sql.Append("  and house.IsDel=1 and SalesStatus=@SalesStatus  ");
                    sqlCount.Append(" and house.IsDel=1 and SalesStatus=@SalesStatus ");
                }
            }
            #endregion

            sql.Append(string.Format(" ) AS tempTable WHERE Row BETWEEN {0} AND  {1}", ((paging.PageIndex - 1) * paging.PageSize) + 1, paging.PageIndex * paging.PageSize));

            SqlParameter[] parameters =
                    {
                        
                        new SqlParameter("@DeveloperId",DeveloperId),
                        new SqlParameter("@PremisesId",pid),
                        new SqlParameter("@BuildingId",bid),
                        new SqlParameter("@UnitId",uid),
                        new SqlParameter("@Floor", fid),
                        new SqlParameter("@SalesStatus", sid)
                       
                    };

            SqlParameter[] parameters1 =
                    {
                        
                        new SqlParameter("@DeveloperId",DeveloperId),
                        new SqlParameter("@PremisesId",pid),
                        new SqlParameter("@BuildingId",bid),
                        new SqlParameter("@UnitId",uid),
                        new SqlParameter("@Floor", fid),
                        new SqlParameter("@SalesStatus", sid)
                     
                    };

            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    var query = db.ExecuteStoreQuery<HouseAndBuilding>(sql.ToString(), parameters);
                    List<HouseAndBuilding> p = query.ToList();
                    paging.ResultData = p;
                    #endregion

                    #region 查询总记录数据
                    var queryCount = db.ExecuteStoreQuery<int>(sqlCount.ToString(), parameters1);
                    paging.TotalCount = queryCount.FirstOrDefault<int>();
                    #endregion
                }
                return paging;
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0},{1},{2}", DeveloperId, pid, uid), ex);
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
        public int Update(House entity)
        {//方法重复
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
                    //    query.IsRelease = entity.IsRelease;

                    //    query.CoordX = entity.CoordX;
                    //    query.CoordY = entity.CoordY;
                    //    query.PictureData = entity.PictureData;


                    //    query.UpdateTime = DateTime.Now;
                    //    return kyj.SaveChanges();

                    //}
                    //return 0;
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
                                PictureData=@PictureData,
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
                            new SqlParameter("@CoordY",entity.CoordY),
                            new SqlParameter("@PictureData",entity.PictureData)
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

        #region 根据多个Id 批量修改房源发布状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/15 15:36:51
        /// 功能描述：根据多个Id 批量修改房源发布状态
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int UpdateHouse_IsRelease(string Ids, int IsRelease, int DeveloperId)
        {
            int result = 0;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format("update House set IsRelease=@IsRelease,IsDel=0  where Id in ({0})  and DeveloperId=@DeveloperId", Ids);
                    DbParameter[] param = new SqlParameter[]{
                        new SqlParameter("@IsRelease",IsRelease),
                        new SqlParameter("@DeveloperId",DeveloperId),
                        new SqlParameter("@Ids",Ids)
                    };
                    result = houseDb.ExecuteStoreCommand(sql, param);

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("Ids:{0},IsRelease:{1},DeveloperId:{2}", Ids, IsRelease, DeveloperId), ex);
                throw;
            }

            return result;
        }
        #endregion

        #region 根据多个Id 批量修改房源销售状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/15 15:36:51
        /// 功能描述：根据多个Id 批量修改房源发布状态
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int UpdateHouse_SalesStatus(string Ids, int SalesStatus, int DeveloperId)
        {
            int result = 0;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"update House set SalesStatus=@SalesStatus where Id in (" + Ids + ") and IsDel=0 and DeveloperId=@DeveloperId";
                    DbParameter[] param = new SqlParameter[]{
                        new SqlParameter("@SalesStatus",SalesStatus),
                        new SqlParameter("@DeveloperId",DeveloperId),
                        new SqlParameter("@Ids",Ids)
                    };
                    result = houseDb.ExecuteStoreCommand(sql, param);

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("Ids:{0},SalesStatus:{1},DeveloperId:{2}", Ids, SalesStatus, DeveloperId), ex);
                throw;
            }

            return result;
        }
        #endregion

        #region 根据房源ID 获取房源CityId
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/17 13:14:46
        /// 功能描述：根据房源ID 获取房源CityId
        /// </summary>
        /// <param name="HouseId"></param>
        /// <returns></returns>
        public int GetCityIDByHouseID(int HouseId)
        {
            string sql = @"select CityID from Premises where Premises.ID=(
            select PremisesId from  House where House.ID=@HouseId)";
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@HouseId",HouseId)
            };

            try
            {
                using (var HouseDB = new kyj_NewHouseDBEntities())
                {
                    var CityId = HouseDB.ExecuteStoreQuery<int>(sql, parameters);
                    return CityId.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("房源id({0})]", HouseId), ex);
                return 0;
            }

        }
        #endregion

        #region 删除单个房源
        ///作者：谢江
        ///时间：2014/1/20 16:59:55
        ///功能描述：删除单个房源
        /// /// <returns>返回：0 失败 1 成功 2 正则参加活动</returns>
        public int DelHouseByID(int houseId)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    //var activcount = from ac in kyj.Activities
                    //                 join house in kyj.ActivitiesHouses on ac.Id equals house.ActivitiesId
                    //                 where ac.IsDel == false && house.IsDel == false && house.HouseId == houseId
                    //                 select house;
                    //if (activcount.Count() > 0)
                    //{
                    //    return 2;
                    //}
                    //var query = kyj.Houses.FirstOrDefault(it => it.Id == houseId && it.IsDel == false);
                    //if (query != null)
                    //{
                    //    query.IsDel = true;
                    //    query.UpdateTime = DateTime.Now;
                    //    return kyj.SaveChanges();

                    //}
                    //return 0;
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
                        SET     IsDel=1,
                                UpdateTime = GETDATE()
                        WHERE   Id = @ID and IsDel=0
                        SELECT  0
                    END";
                    SqlParameter[] sqlparameter = { new SqlParameter("@ID", houseId) };
                    return kyj.ExecuteStoreQuery<int>(sql, sqlparameter).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("马欢", houseId, e);
                return 0;
            }
        }
        #endregion



    }
}