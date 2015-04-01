using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.Commons;
using TXOrm;
using System.Data;

namespace TXDal.HouseData
{
    /// <summary>
    /// 房源
    /// </summary>
    public partial class HouseDal
    {
        #region 通用方法

        /// <summary>
        /// 添加房源
        /// </summary>
        /// <param name="entity">房源实体</param>
        /// <returns>返回：新添加房源编号</returns>
        public int Add(House model, HouseTemplate ht)
        {
            int result = 0;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("BEGIN ");
                    strSql.Append("BEGIN TRY ");
                    strSql.Append("BEGIN TRAN ");

                    strSql.Append(@"INSERT INTO [House]
                                       ([Name]
                                       ,[InnerCode]
                                       ,[DeveloperId]
                                       ,[PremisesId]
                                       ,[BuildingId]
                                       ,[UnitId]
                                       ,[Floor]
                                       ,[RId]
                                       ,[PictureData]
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
                                       ,[CoordX]
                                       ,[CoordY]
                                       ,[CreateTime]
                                       ,[UpdateTime]
                                       ,[IsDel],[IsRelease])
                                 VALUES(
                                        @Name,
                                        @InnerCode,
                                        @DeveloperId,
                                        @PremisesId,
                                        @BuildingId,
                                        @UnitId,
                                        @Floor,
                                        @RId,
                                        @PictureData,
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
                                        @CoordX,
                                        @CoordY,
                                        @CreateTime,
                                        @UpdateTime,
                                        @IsDel,
                                        @IsRelease)");

                    strSql.Append(" select @id= SCOPE_IDENTITY();");

                    if (ht != null)
                    {
                        strSql.Append("insert into HouseTemplate(");
                        strSql.Append("DeveloperId,Title,Remark,Content,CreateTime,UpdateTime,IsDel");
                        strSql.Append(") values (");
                        strSql.Append("@DeveloperId,@Title,@Remark,@Content,GetDate(),GetDate(),0");
                        strSql.Append(") ");

                    }
                    else
                    {
                        ht = new HouseTemplate();
                        ht.Title = "";
                        ht.Remark = "";
                        ht.Content = "";
                    }

                    strSql.Append(" COMMIT ");
                    strSql.Append(" END TRY ");
                    strSql.Append(" BEGIN CATCH ");
                    strSql.Append(" ROLLBACK TRAN ");
                    strSql.Append(" END CATCH ");
                    strSql.Append(" END ");


                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@id", SqlDbType.Int),
                        #region 模版参数
                        new SqlParameter("@Title", ht.Title),
                        new SqlParameter("@Remark", ht.Remark),
                        new SqlParameter("@Content", ht.Content),
                        #endregion
                        
                        

                        new SqlParameter("@Name",model.Name),
                        new SqlParameter("@InnerCode",model.InnerCode),
                        new SqlParameter("@DeveloperId",model.DeveloperId),
                        new SqlParameter("@PremisesId",model.PremisesId),
                        new SqlParameter("@BuildingId",model.BuildingId),
                        new SqlParameter("@UnitId",model.UnitId),
                        new SqlParameter("@Floor",model.Floor),
                        new SqlParameter("@RId",model.RId),
                        new SqlParameter("@PictureData",model.PictureData),
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
                        new SqlParameter("@CoordX",model.CoordX),
                        new SqlParameter("@CoordY",model.CoordY),
                        new SqlParameter("@CreateTime",model.CreateTime),
                        new SqlParameter("@UpdateTime",model.UpdateTime),
                        new SqlParameter("@IsDel",model.IsDel),
                        new SqlParameter("@IsRelease",model.IsRelease)
                    };
                    parameters[0].Direction = ParameterDirection.Output;
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                    if (result > 0)
                    {
                        result = Convert.ToInt32(parameters[0].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", null, ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// 更新房源编号更新房源信息
        /// </summary>
        /// <param name="entity">房源实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return 0;
        }

        /// <summary>
        /// 逻辑删除房源信息
        /// </summary>
        /// <param name="id">房源编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    //var model = db.Houses.FirstOrDefault(house => house.Id == id && house.IsDel == false);
                    //if (model != null)
                    //{
                    //    model.IsDel = true;
                    //    model.UpdateTime = DateTime.Now;
                    //}
                    //return db.SaveChanges();
                    string sql = string.Format(@" UPDATE  dbo.House
                        SET     IsDel=1,
                                UpdateTime = GETDATE()
                        WHERE   Id = {0} and IsDel=0", id);
                    return db.ExecuteStoreCommand(sql);

                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马欢", string.Format("Id:{0}", new object[] { id }), e);
                return 0;
            }
        }

        /// <summary>
        /// 根据编号获取房源实体
        /// </summary>
        /// <param name="id">房源编号</param>
        /// <returns>返回：房源实体</returns>
        public House GetEntity_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"SELECT  *
                                    FROM    dbo.House
                                    WHERE   Id = {0}
                                            AND IsDel = 0", id);
                    return db.ExecuteStoreQuery<House>(sql).FirstOrDefault();
                    //return db.Houses.FirstOrDefault(house => house.Id == id && house.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马欢", string.Format("Id:{0}", new object[] { id }), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据编号获取房源实体
        /// </summary>
        /// <param name="id">房源编号</param>
        /// <returns>返回：房源实体</returns>
        public House GetHouseModel(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"SELECT  *
                                    FROM    dbo.House
                                    WHERE   Id = {0}", id);
                    return db.ExecuteStoreQuery<House>(sql).FirstOrDefault();
                    //return db.Houses.FirstOrDefault(house => house.Id == id);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马欢", string.Format("Id:{0}", new object[] { id }), ex);
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

        #endregion
    }
}