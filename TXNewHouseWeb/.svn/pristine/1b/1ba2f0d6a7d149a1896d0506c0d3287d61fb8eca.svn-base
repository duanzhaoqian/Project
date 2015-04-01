using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.Commons;
using TXOrm;

namespace TXDal.HouseData
{
    /// <summary>
    /// 楼栋
    /// </summary>
    public partial class BuildingDal
    {
        #region 通用方法

        /// <summary>
        /// 添加楼栋
        /// </summary>
        /// <param name="model">楼栋实体</param>
        /// <returns>返回：新添加楼栋编号</returns>
        public int Add(Building model)
        {
            int result = 0;
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("insert into Building(");
                strSql.Append("FamilyNum,TheFloor,Underloor,Ladder,Renovation,BuildingPosition,OpeningTime,OthersTime,State,PresaleId,DeveloperId,PermitPresale,PNum,UpdateTime,IsDel,PremisesId,Periods,PropertyType,CreateTime,Name");
                strSql.Append(") values (");
                strSql.Append("@FamilyNum,@TheFloor,@Underloor,@Ladder,@Renovation,@BuildingPosition,@OpeningTime,@OthersTime,@State,@PresaleId,@DeveloperId,@PermitPresale,@PNum,@UpdateTime,@IsDel,@PremisesId,@Periods,@PropertyType,@CreateTime,@Name");
                strSql.Append(") ");
                strSql.Append(";select @@IDENTITY");
                object[] parameters =
                {
                    new SqlParameter("@FamilyNum", model.FamilyNum),
                    new SqlParameter("@TheFloor", model.TheFloor),
                    new SqlParameter("@Underloor", model.Underloor),
                    new SqlParameter("@Ladder", model.Ladder),
                    new SqlParameter("@Renovation", model.Renovation),
                    new SqlParameter("@BuildingPosition", model.BuildingPosition),
                    new SqlParameter("@OpeningTime", model.OpeningTime),
                    new SqlParameter("@OthersTime", model.OthersTime),
                    new SqlParameter("@State", model.State),
                    new SqlParameter("@PresaleId", model.PresaleId),
                    new SqlParameter("@DeveloperId", model.DeveloperId),
                    new SqlParameter("@PermitPresale", model.PermitPresale),
                    new SqlParameter("@PNum", model.PNum),
                    new SqlParameter("@UpdateTime", model.UpdateTime),
                    new SqlParameter("@IsDel", model.IsDel),
                    new SqlParameter("@PremisesId", model.PremisesId),
                    new SqlParameter("@Periods", model.Periods),
                    new SqlParameter("@PropertyType", model.PropertyType),
                    new SqlParameter("@CreateTime", model.CreateTime),
                    new SqlParameter("@Name", model.Name)
                };

                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", model, ex);
                //throw;
            }
            return result;

        }

        /// <summary>
        /// 更新楼栋编号更新楼栋信息
        /// </summary>
        /// <param name="entity">楼栋实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return 0;
        }

        /// <summary>
        /// 逻辑删除楼栋信息
        /// </summary>
        /// <param name="id">楼栋编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            return 0;
        }

        /// <summary>
        /// 根据编号获取楼栋实体
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">楼栋编号</param>
        /// <returns>返回：楼栋实体</returns>
        public Building GetEntity_ById(int id)
        {
            try
            {
                string sql = @"SELECT  *
                                FROM    dbo.Building
                                WHERE   id = @id 
                                        AND IsDel = 0";
                SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@id", id)
                    };
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var building = db.ExecuteStoreQuery<Building>(sql, sqlparams);
                    return building.FirstOrDefault();
                    // return db.Buildings.FirstOrDefault(build => build.Id == id && build.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("Id:{0}", new object[] { id }), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据编号集合获取实体列表
        /// </summary>
        /// <param name="ids">编号集合字符串(逗号分隔)</param>
        /// <returns>返回：列表</returns>
        public List<object> GetList_ByIds(string ids)
        {
            return null;
        }

        /// <summary>
        /// 根据搜素条件获取分页列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public Paging<object> GetPageList_BySearch(object search, int pageIndex, int pageSize)
        {
            return null;
        }

        #region 根据楼盘Id获取楼栋

        /// <summary>
        /// 根据楼盘Id获取楼栋
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<Building> GetList_ByPremisId(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    Building (NOLOCK)
WHERE   IsDel = 0
        AND PremisesId = {0}", premisesId);

                    var list = db.ExecuteStoreQuery<Building>(sql).ToList();

                    return list;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("李雨钊", String.Format("premisesId:{0}", premisesId), e);
                throw;
            }
        }

        /// <summary>
        /// 根据楼盘Id获取楼栋
        /// author: 汪敏
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <returns></returns>
        public List<Building> GetBuildingByPremisesId(int premisesId, int DeveloperId)
        {
            List<Building> list = new List<Building>();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"SELECT  *
                                    FROM    dbo.Building
                                    WHERE   PremisesId ={0}
                                            AND IsDel = 0
                                            AND DeveloperId = {1}", premisesId, DeveloperId);
                    var buildings = newhouseDb.ExecuteStoreQuery<Building>(sql).ToList();
                    //var buildings = newhouseDb.Buildings.Where(t => t.PremisesId == premisesId && t.IsDel == false && t.DeveloperId == DeveloperId).ToList();


                    if (buildings.Count > 0)
                    {
                        foreach (var item in buildings)
                        {
                            var b = new Building();
                            b.Id = item.Id;
                            b.Name = item.Name + item.NameType;
                            b.NameType = item.NameType;
                            list.Add(b);
                        }
                        return list;
                    }
                    else
                    {
                        return null;
                    }


                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("premisesId:{0}", premisesId), e);
                throw;
            }


        }

        #endregion

        #endregion

        #region 发布楼栋

        /// <summary>
        /// 添加楼栋
        /// </summary>
        /// <param name="model">楼栋实体</param>
        /// <param name="unit"></param>
        /// <returns>返回：新添加楼栋编号</returns>
        public int Add(Building model, string[] unit)
        {
            int result = 0;
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("BEGIN ");
                strSql.Append("BEGIN TRY ");
                strSql.Append("BEGIN TRAN ");

                strSql.Append("insert into Building(");
                strSql.Append("FamilyNum,TheFloor,Underloor,Ladder,Renovation,BuildingPosition,OpeningTime,OthersTime,State,PresaleId,DeveloperId,PermitPresale,PNum,UpdateTime,IsDel,PremisesId,Periods,PropertyType,CreateTime,PictureData,Name,NameType");
                strSql.Append(") values (");
                strSql.Append("@FamilyNum,@TheFloor,@Underloor,@Ladder,@Renovation,@BuildingPosition,@OpeningTime,@OthersTime,@State,@PresaleId,@DeveloperId,@PermitPresale,@PNum,@UpdateTime,@IsDel,@PremisesId,@Periods,@PropertyType,@CreateTime,@PictureData,@Name,@NameType");
                strSql.Append(") ");

                strSql.Append("Declare @pid int ");
                strSql.Append("set @pid=(select @@IDENTITY) ");

                for (int i = 0; i < unit.Length; i++)
                {

                    strSql.Append("insert into Unit(");
                    strSql.Append("DeveloperId,PremisesId,BuildingId,Num,Name,CreateTime,UpdateTime,IsDel");
                    strSql.Append(") values (");
                    strSql.Append("@DeveloperId,@PremisesId,@pid," + (i + 1) + ",'" + unit[i] + "',GETDATE(),GETDATE(),0");
                    strSql.Append(") ");

                }

                strSql.Append("COMMIT ");
                strSql.Append("END TRY ");
                strSql.Append("BEGIN CATCH ");
                strSql.Append("ROLLBACK TRAN ");
                strSql.Append("END CATCH ");
                strSql.Append("END ");

                object[] parameters =
                {
                    new SqlParameter("@FamilyNum", model.FamilyNum),
                    new SqlParameter("@TheFloor", model.TheFloor),
                    new SqlParameter("@Underloor", model.Underloor),
                    new SqlParameter("@Ladder", model.Ladder),
                    new SqlParameter("@Renovation", model.Renovation),
                    new SqlParameter("@BuildingPosition", model.BuildingPosition),
                    new SqlParameter("@OpeningTime", model.OpeningTime),
                    new SqlParameter("@OthersTime", model.OthersTime),
                    new SqlParameter("@State", model.State),
                    new SqlParameter("@PresaleId", model.PresaleId),
                    new SqlParameter("@DeveloperId", model.DeveloperId),
                    new SqlParameter("@PermitPresale", model.PermitPresale),
                    new SqlParameter("@PNum", model.PNum),
                    new SqlParameter("@UpdateTime", model.UpdateTime),
                    new SqlParameter("@IsDel", model.IsDel),
                    new SqlParameter("@PremisesId", model.PremisesId),
                    new SqlParameter("@Periods", model.Periods),
                    new SqlParameter("@PropertyType", model.PropertyType),
                    new SqlParameter("@CreateTime", model.CreateTime),
                    new SqlParameter("@PictureData", model.PictureData),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@NameType", model.NameType)
                };


                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", model, ex);
                //throw;
            }
            return result;

        }



        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 13:35:39
        /// 功能描述：添加楼栋(手填预售许可证)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="unit"></param>
        /// <param name="PermitName">预售许可证名称</param>
        /// <returns></returns>
        public int Add(Building model, string[] unit, string PermitName)
        {
            int result;
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("BEGIN ");
                strSql.Append("BEGIN TRY ");
                strSql.Append("BEGIN TRAN ");

                strSql.Append("insert into PermitPresale(");
                strSql.Append("PremisesId,Name,CreateTime,UpdateTime,IsDel");
                strSql.Append(") values (");
                strSql.Append("@PremisesId,@PermitName,GETDATE(),GETDATE(),0");
                strSql.Append(") ");

                strSql.Append("Declare @PresId int ");
                strSql.Append("set @PresId=(select @@IDENTITY) ");


                strSql.Append("insert into Building(");
                strSql.Append("FamilyNum,TheFloor,Underloor,Ladder,Renovation,BuildingPosition,OpeningTime,OthersTime,State,PresaleId,DeveloperId,PermitPresale,PNum,UpdateTime,IsDel,PremisesId,Periods,PropertyType,CreateTime,PictureData,Name,NameType");
                strSql.Append(") values (");
                strSql.Append("@FamilyNum,@TheFloor,@Underloor,@Ladder,@Renovation,@BuildingPosition,@OpeningTime,@OthersTime,@State,@PresId,@DeveloperId,@PermitName,@PNum,@UpdateTime,@IsDel,@PremisesId,@Periods,@PropertyType,@CreateTime,@PictureData,@Name,@NameType");
                strSql.Append(") ");

                strSql.Append("Declare @pid int ");
                strSql.Append("set @pid=(select @@IDENTITY) ");

                for (int i = 0; i < unit.Length; i++)
                {

                    strSql.Append("insert into Unit(");
                    strSql.Append("DeveloperId,PremisesId,BuildingId,Num,Name,CreateTime,UpdateTime,IsDel");
                    strSql.Append(") values (");
                    strSql.Append("@DeveloperId,@PremisesId,@pid," + (i + 1) + ",'" + unit[i] + "',GETDATE(),GETDATE(),0");
                    strSql.Append(") ");

                }




                strSql.Append("COMMIT ");
                strSql.Append("END TRY ");
                strSql.Append("BEGIN CATCH ");
                strSql.Append("ROLLBACK TRAN ");
                strSql.Append("END CATCH ");
                strSql.Append("END ");

                object[] parameters =
                {
                    new SqlParameter("@FamilyNum", model.FamilyNum),
                    new SqlParameter("@TheFloor", model.TheFloor),
                    new SqlParameter("@Underloor", model.Underloor),
                    new SqlParameter("@Ladder", model.Ladder),
                    new SqlParameter("@Renovation", model.Renovation),
                    new SqlParameter("@BuildingPosition", model.BuildingPosition),
                    new SqlParameter("@OpeningTime", model.OpeningTime),
                    new SqlParameter("@OthersTime", model.OthersTime),
                    new SqlParameter("@State", model.State),
                    new SqlParameter("@DeveloperId", model.DeveloperId),
                    new SqlParameter("@PermitPresale", model.PermitPresale),
                    new SqlParameter("@PNum", model.PNum),
                    new SqlParameter("@UpdateTime", model.UpdateTime),
                    new SqlParameter("@IsDel", model.IsDel),
                    new SqlParameter("@PremisesId", model.PremisesId),
                    new SqlParameter("@Periods", model.Periods),
                    new SqlParameter("@PropertyType", model.PropertyType),
                    new SqlParameter("@CreateTime", model.CreateTime),
                    new SqlParameter("@PictureData", model.PictureData),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@NameType", model.NameType),
                    new SqlParameter("@PermitName", PermitName)


                };


                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", null, ex);
                throw;
            }
            return result;

        }


        #endregion

        #region 更新楼栋


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 14:53:25
        /// 功能描述：更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool Update(Building model, string[] unit)
        {
            int result;
            var strSql = new StringBuilder();
            strSql.Append("BEGIN ");
            strSql.Append("BEGIN TRY ");
            strSql.Append("BEGIN TRAN ");

            strSql.Append(" declare @OldState int ");
            strSql.Append(" Set @OldState=(select state from Building WHERE Id=@Id  ) ");


            strSql.Append("update Building set ");
            strSql.Append("PremisesId=@PremisesId,");
            strSql.Append("Periods=@Periods,");
            strSql.Append("PropertyType=@PropertyType,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("PictureData=@PictureData,");
            strSql.Append("Name=@Name,");
            strSql.Append("NameType=@NameType,");
            strSql.Append("FamilyNum=@FamilyNum,");
            strSql.Append("TheFloor=@TheFloor,");
            strSql.Append("Underloor=@Underloor,");
            strSql.Append("Ladder=@Ladder,");
            strSql.Append("Renovation=@Renovation,");
            strSql.Append("BuildingPosition=@BuildingPosition,");
            strSql.Append("OpeningTime=@OpeningTime,");
            strSql.Append("OthersTime=@OthersTime,");
            strSql.Append("State=@State,");
            strSql.Append("PresaleId=@PresaleId,");
            strSql.Append("PermitPresale=@PermitPresale,");
            strSql.Append("PNum=@PNum,");
            strSql.Append("UpdateTime=@UpdateTime ");
            strSql.Append(" where Id=@Id");

            strSql.Append("  DELETE  FROM Unit ");
            strSql.Append("  WHERE   BuildingId = @Id ");


            for (int i = 0; i < unit.Length; i++)
            {

                strSql.Append("insert into Unit(");
                strSql.Append("DeveloperId,PremisesId,BuildingId,Num,Name,CreateTime,UpdateTime,IsDel");
                strSql.Append(") values (");
                strSql.Append("@DeveloperId,@PremisesId,@Id," + (i + 1) + ",'" + unit[i] + "',GETDATE(),GETDATE(),0");
                strSql.Append(") ");

            }

            //当楼盘或者楼栋修改为售罄状态，
            //则只是把在售、待售状态的房源改为售罄状态，
            //其他状态（已认购、已签约、已备案、已办产权、被限制、开发商保留、拆迁安置）
            //的房源不做改动。
            strSql.Append(" IF(@state=3 and @OldState<>3) ");
            strSql.Append(" BEGIN ");
            strSql.Append(" Update House set SalesStatus=9 where IsDel=0 and BuildingId=@Id ");
            strSql.Append(" AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
            //            strSql.Append(@" And Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
            //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
            //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
            //                          GROUP BY h.Id ) ");
            strSql.Append(" END ");

            //状态改为在售
            strSql.Append(" IF(@state=2 and @OldState<>2) ");
            strSql.Append(" BEGIN ");
            strSql.Append(" Update House set SalesStatus=2 where IsDel=0 and BuildingId=@Id ");
            strSql.Append(" AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
            //            strSql.Append(@" And Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
            //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
            //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
            //                          GROUP BY h.Id ) ");
            strSql.Append(" END ");


            //状态改为待售
            strSql.Append(" IF(@state=1 and @OldState<>1) ");
            strSql.Append(" BEGIN ");
            strSql.Append(" Update House set SalesStatus=0 where IsDel=0 and BuildingId=@Id ");
            strSql.Append(" AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
            //            strSql.Append(@" And Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
            //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
            //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
            //                          GROUP BY h.Id ) ");
            strSql.Append(" END ");





            strSql.Append("COMMIT ");
            strSql.Append("END TRY ");
            strSql.Append("BEGIN CATCH ");
            strSql.Append("ROLLBACK TRAN ");
            strSql.Append("END CATCH ");
            strSql.Append("END ");


            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@FamilyNum", model.FamilyNum),
                new SqlParameter("@TheFloor", model.TheFloor),
                new SqlParameter("@Underloor", model.Underloor),
                new SqlParameter("@Ladder", model.Ladder),
                new SqlParameter("@Renovation", model.Renovation),
                new SqlParameter("@BuildingPosition", model.BuildingPosition),
                new SqlParameter("@OpeningTime", model.OpeningTime),
                new SqlParameter("@OthersTime", model.OthersTime),
                new SqlParameter("@State", model.State),
                new SqlParameter("@PresaleId", model.PresaleId),
                new SqlParameter("@DeveloperId", model.DeveloperId),
                new SqlParameter("@PermitPresale", model.PermitPresale),
                new SqlParameter("@PNum", model.PNum),
                new SqlParameter("@UpdateTime", model.UpdateTime),
                new SqlParameter("@PremisesId", model.PremisesId),
                new SqlParameter("@Periods", model.Periods),
                new SqlParameter("@PropertyType", model.PropertyType),
                new SqlParameter("@CreateTime", model.CreateTime),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@NameType", model.NameType),
                new SqlParameter("@PictureData", model.PictureData)
            };

            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                }
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", null, ex);
                throw;
            }

        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 14:53:25
        /// 功能描述：更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool Update(Building model, string[] unit, string PermitName)
        {
            int result;
            var strSql = new StringBuilder();
            strSql.Append("BEGIN ");
            strSql.Append("BEGIN TRY ");
            strSql.Append("BEGIN TRAN ");

            strSql.Append(" declare @OldState int ");
            strSql.Append(" Set @OldState=(select state from Building WHERE Id=@Id  ) ");


            strSql.Append("update PermitPresale set [Name]=@PermitName where Id=@PresaleId and PremisesId=@PremisesId ");

            strSql.Append(" update Building set ");
            strSql.Append("PremisesId=@PremisesId,");
            strSql.Append("Periods=@Periods,");
            strSql.Append("PropertyType=@PropertyType,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("PictureData=@PictureData,");
            strSql.Append("Name=@Name,");
            strSql.Append("NameType=@NameType,");
            strSql.Append("FamilyNum=@FamilyNum,");
            strSql.Append("TheFloor=@TheFloor,");
            strSql.Append("Underloor=@Underloor,");
            strSql.Append("Ladder=@Ladder,");
            strSql.Append("Renovation=@Renovation,");
            strSql.Append("BuildingPosition=@BuildingPosition,");
            strSql.Append("OpeningTime=@OpeningTime,");
            strSql.Append("OthersTime=@OthersTime,");
            strSql.Append("State=@State,");
            strSql.Append("PresaleId=@PresaleId,");
            strSql.Append("PermitPresale=@PermitPresale,");
            strSql.Append("PNum=@PNum,");
            strSql.Append("UpdateTime=@UpdateTime ");
            strSql.Append(" where Id=@Id");

            strSql.Append("  DELETE  FROM Unit ");
            strSql.Append("  WHERE   BuildingId = @Id ");


            for (int i = 0; i < unit.Length; i++)
            {

                strSql.Append("insert into Unit(");
                strSql.Append("DeveloperId,PremisesId,BuildingId,Num,Name,CreateTime,UpdateTime,IsDel");
                strSql.Append(") values (");
                strSql.Append("@DeveloperId,@PremisesId,@Id," + (i + 1) + ",'" + unit[i] + "',GETDATE(),GETDATE(),0");
                strSql.Append(") ");

            }

            //当楼盘或者楼栋修改为售罄状态，
            //则只是把在售、待售状态的房源改为售罄状态，
            //其他状态（已认购、已签约、已备案、已办产权、被限制、开发商保留、拆迁安置）
            //的房源不做改动。
            strSql.Append(" IF(@state=3 and @OldState<>3) ");
            strSql.Append(" BEGIN ");
            strSql.Append(" Update House set SalesStatus=9 where IsDel=0 and BuildingId=@Id ");
            strSql.Append(" AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
            //            strSql.Append(@" And Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
            //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
            //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
            //                          GROUP BY h.Id )");
            strSql.Append(" END ");

            //状态改为在售
            strSql.Append(" IF(@state=2 and @OldState<>2) ");
            strSql.Append(" BEGIN ");
            strSql.Append(" Update House set SalesStatus=2 where IsDel=0 and BuildingId=@Id ");
            strSql.Append(" AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
            //            strSql.Append(@" And Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
            //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
            //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
            //                          GROUP BY h.Id ) ");
            strSql.Append(" END ");


            //状态改为待售
            strSql.Append(" IF(@state=1 and @OldState<>1) ");
            strSql.Append(" BEGIN ");
            strSql.Append(" Update House set SalesStatus=0 where IsDel=0 and BuildingId=@Id ");
            strSql.Append(" AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
            //            strSql.Append(@" And Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
            //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
            //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
            //                          GROUP BY h.Id ) ");
            strSql.Append(" END ");






            strSql.Append("COMMIT ");
            strSql.Append("END TRY ");
            strSql.Append("BEGIN CATCH ");
            strSql.Append("ROLLBACK TRAN ");
            strSql.Append("END CATCH ");
            strSql.Append("END ");


            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", model.Id),
                new SqlParameter("@FamilyNum", model.FamilyNum),
                new SqlParameter("@TheFloor", model.TheFloor),
                new SqlParameter("@Underloor", model.Underloor),
                new SqlParameter("@Ladder", model.Ladder),
                new SqlParameter("@Renovation", model.Renovation),
                new SqlParameter("@BuildingPosition", model.BuildingPosition),
                new SqlParameter("@OpeningTime", model.OpeningTime),
                new SqlParameter("@OthersTime", model.OthersTime),
                new SqlParameter("@State", model.State),
                new SqlParameter("@PresaleId", model.PresaleId),
                new SqlParameter("@DeveloperId", model.DeveloperId),
                new SqlParameter("@PermitPresale", model.PermitPresale),
                new SqlParameter("@PNum", model.PNum),
                new SqlParameter("@UpdateTime", model.UpdateTime),
                new SqlParameter("@PremisesId", model.PremisesId),
                new SqlParameter("@Periods", model.Periods),
                new SqlParameter("@PropertyType", model.PropertyType),
                new SqlParameter("@CreateTime", model.CreateTime),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@NameType", model.NameType),
                new SqlParameter("@PictureData", model.PictureData),
                new SqlParameter("@PermitName", PermitName)
            };

            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                }
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", null, ex);
                throw;
            }

        }


        #endregion

        #region 获取楼层信息

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/3 10:00:05
        /// 功能描述：获取地上楼层信息
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public int GetTheFloor(int BuildingId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = "select TheFloor from Building where id=@id";
                    object[] param =
                    {
                        new SqlParameter("@id", BuildingId)
                    };
                    var theFloor = db.ExecuteStoreQuery<int>(sql, param).FirstOrDefault();

                    return theFloor;

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0}", BuildingId), ex);

                throw;
            }
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/3 10:00:05
        /// 功能描述：获取地下楼层信息
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public int GetUnderloor(int BuildingId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = "select Underloor from Building where id=@id";
                    object[] param =
                    {
                        new SqlParameter("@id", BuildingId)
                    };
                    var theFloor = db.ExecuteStoreQuery<int>(sql, param).FirstOrDefault();

                    return theFloor;

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0}", BuildingId), ex);

                throw;
            }
        }

        #endregion

        #region 根据楼盘编号获取楼栋沙盘图标记列表

        /// <summary>
        /// 根据楼盘编号获取楼栋沙盘图标记列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<int> GetPNumsByPremisesId(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                            SELECT  PNum
                                            FROM    Building (NOLOCK)
                                            WHERE   PremisesId = {0}
                                                    AND IsDel = 0", premisesId);

                    var list = db.ExecuteStoreQuery<int>(sql).ToList();

                    return list;

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("premisesId:{0}", premisesId), ex);
                throw;
            }
        }

        #endregion


        #region 根据楼栋Id 获取楼栋状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/25 16:13:04
        /// 功能描述：根据楼栋Id 获取楼栋状态
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public int GetBuildingState(int BuildingId)
        {
            int i = -1;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = @"select State  AS Result from Building  WITH (NoLock) where  id=@BuildingId";
                    SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@BuildingId",BuildingId)

                    };
                    var list = dbEnt.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql, param).ToList();
                    if (list.Count > 0)
                    {
                        i = Convert.ToInt32(list[0].Result);
                    }
                };

            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("BuildingId:{0}", BuildingId), ex);
                throw;
            }
            return i;
        }
        #endregion
    }
}