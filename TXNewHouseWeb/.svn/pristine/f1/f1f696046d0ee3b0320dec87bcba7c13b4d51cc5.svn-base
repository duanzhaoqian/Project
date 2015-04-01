using System;
using System.Collections.Generic;
using TXModel.Commons;
using System.Linq;
using System.Text;
using TXOrm;
using System.Data.SqlClient;
using System.Data;

namespace TXDal.HouseData
{
    /// <summary>
    /// 楼盘DAL类
    /// </summary>
    public partial class PremisesDal
    {


        #region 通用方法

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">对象实体</param>
        /// <returns>新增ID</returns>
        public int Add(Premis model)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();

                    strSql.Append("insert into Premises(");
                    strSql.Append("BId,BName,Name,ReferencePrice,TelePhone,SalesStatus,Ring,PremisesAddress,salesAddress,Developer,DeveloperId,Agent,SalePermit,PropertyRight,BuildingArea,Area,UserCount,Characteristic,BuildingType,PropertyType,AreaRatio,InnerCode,RoomRate,PropertyPrice,ParkingLot,Renovation,PropertyCompany,GreeningRate,TrafficIntroduction,SupportingIntroduction,PremisesIntroduction,ProvinceId,CreateTime,UpdateTime,IsDel,ProvinceName,CityId,CityName,DId,DName");
                    strSql.Append(") values (");
                    strSql.Append("@BId,@BName,@Name,@ReferencePrice,@TelePhone,@SalesStatus,@Ring,@PremisesAddress,@salesAddress,@Developer,@DeveloperId,@Agent,@SalePermit,@PropertyRight,@BuildingArea,@Area,@UserCount,@Characteristic,@BuildingType,@PropertyType,@AreaRatio,@InnerCode,@RoomRate,@PropertyPrice,@ParkingLot,@Renovation,@PropertyCompany,@GreeningRate,@TrafficIntroduction,@SupportingIntroduction,@PremisesIntroduction,@ProvinceId,@CreateTime,@UpdateTime,@IsDel,@ProvinceName,@CityId,@CityName,@DId,@DName");
                    strSql.Append(") ");
                    strSql.Append(";select @@IDENTITY ");

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@BId", model.BId),
                        new SqlParameter("@BName", model.BName),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@ReferencePrice", model.ReferencePrice),
                        new SqlParameter("@TelePhone", model.TelePhone),
                        new SqlParameter("@SalesStatus", model.SalesStatus),
                        new SqlParameter("@Ring", model.Ring),
                        new SqlParameter("@PremisesAddress", model.PremisesAddress),
                        new SqlParameter("@salesAddress", model.salesAddress),
                        new SqlParameter("@Developer", model.Developer),
                        new SqlParameter("@DeveloperId", model.DeveloperId),
                        new SqlParameter("@Agent", model.Agent),
                        new SqlParameter("@SalePermit", model.SalePermit),
                        new SqlParameter("@PropertyRight", model.PropertyRight),
                        new SqlParameter("@BuildingArea", model.BuildingArea),
                        new SqlParameter("@Area", model.Area),
                        new SqlParameter("@UserCount", model.UserCount),
                        new SqlParameter("@Characteristic", model.Characteristic),
                        new SqlParameter("@BuildingType", model.BuildingType),
                        new SqlParameter("@PropertyType", model.PropertyType),
                        new SqlParameter("@AreaRatio", model.AreaRatio),
                        new SqlParameter("@InnerCode", model.InnerCode),
                        new SqlParameter("@RoomRate", model.RoomRate),
                        new SqlParameter("@PropertyPrice", model.PropertyPrice),
                        new SqlParameter("@ParkingLot", model.ParkingLot),
                        new SqlParameter("@Renovation", model.Renovation),
                        new SqlParameter("@PropertyCompany", model.PropertyCompany),
                        new SqlParameter("@GreeningRate", model.GreeningRate),
                        new SqlParameter("@TrafficIntroduction", model.TrafficIntroduction),
                        new SqlParameter("@SupportingIntroduction", model.SupportingIntroduction),
                        new SqlParameter("@PremisesIntroduction", model.PremisesIntroduction),
                        new SqlParameter("@ProvinceId", model.ProvinceId),
                        new SqlParameter("@CreateTime", model.CreateTime),
                        new SqlParameter("@UpdateTime", model.UpdateTime),
                        new SqlParameter("@IsDel", model.IsDel),
                        new SqlParameter("@ProvinceName", model.ProvinceName),
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),
                        new SqlParameter("@DId", model.DId),
                        new SqlParameter("@DName", model.DName)
                    };

                    int result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", null, ex);
                throw;
            }
        }




        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">对象Id</param>
        /// <returns>影响行数</returns>
        public int Del_ById(int id)
        {
            return 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">对象Id</param>
        /// <returns>影响行数</returns>
        public int Update_ById(int id)
        {
            return 0;
        }

        /// <summary>
        /// 获取分页集合
        /// </summary>
        /// <param name="paging">分页对象</param>
        /// <returns>结果集</returns>
        public Paging<object> GetPageList_BySearch(Paging<object> paging)
        {
            return null;
        }

        /// <summary>
        /// 获取实体
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">对象Id</param>
        /// <returns>结果集</returns>
        public Premis GetPremises_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Premises.FirstOrDefault(premises => premises.IsDel == false && premises.Id == id);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id={0}", id), ex);
                throw;
            }
        }
        /// <summary>
        /// 获取实体
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">对象Id</param>
        /// <returns>结果集</returns>
        public Premis GetPremises_ById2(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Premises.FirstOrDefault(premises => premises.Id == id);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("id={0}", id), ex);
                throw;
            }
        }
        #region 根据开发商Id获取楼盘
        /// <summary>
        /// 根据开发商Id获取楼盘
        /// author:汪敏
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <returns></returns>
        public List<Premis> GetPremisByDeveloperId(int developerId)
        {
            List<Premis> list = null;
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var premises = newhouseDb.Premises.Where(t => t.DeveloperId == developerId && t.IsDel == false).ToList();

                    if (premises.Count() > 0)
                    {
                        list = premises;
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("developerId:{0}", developerId), e);
                //throw;
            }
            return list;
        }

        #endregion

        #endregion

        #region 发布楼盘

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/27 19:01:45
        /// 功能描述：发布楼盘
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PermitPresale"></param>
        /// <returns></returns>
        public int Add(Premis model, List<Traffic> subways)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    strSql.Append("BEGIN ");
                    strSql.Append("BEGIN TRY ");
                    strSql.Append("BEGIN TRAN ");

                    strSql.Append("insert into Premises(");
                    strSql.Append("BId,TId,BName,Name,ReferencePrice,TelePhone,SalesStatus,Ring,PremisesAddress,salesAddress,Developer,DeveloperId,Agent,SalePermit,PropertyRight,BuildingArea,Area,UserCount,Characteristic,BuildingType,PropertyType,AreaRatio,InnerCode,RoomRate,PropertyPrice,ParkingLot,Renovation,PropertyCompany,GreeningRate,TrafficIntroduction,SupportingIntroduction,PremisesIntroduction,Lng,Lat,ProvinceId,CreateTime,UpdateTime,IsDel,ProvinceName,CityId,CityName,DId,DName,Phone400,IsShow400");
                    strSql.Append(") values (");
                    strSql.Append("@BId,@TId,@BName,@Name,@ReferencePrice,@TelePhone,@SalesStatus,@Ring,@PremisesAddress,@salesAddress,@Developer,@DeveloperId,@Agent,@SalePermit,@PropertyRight,@BuildingArea,@Area,@UserCount,@Characteristic,@BuildingType,@PropertyType,@AreaRatio,@InnerCode,@RoomRate,@PropertyPrice,@ParkingLot,@Renovation,@PropertyCompany,@GreeningRate,@TrafficIntroduction,@SupportingIntroduction,@PremisesIntroduction,@Lng,@Lat,@ProvinceId,@CreateTime,@UpdateTime,@IsDel,@ProvinceName,@CityId,@CityName,@DId,@DName,'',0");
                    strSql.Append(") ");

                    strSql.Append(" SELECT @id= SCOPE_IDENTITY()");

                    strSql.Append(" Declare @num int ");
                    strSql.Append(" set @num=(select @@IDENTITY) ");



                    //for (int i = 0; i < PermitPresale.Length; i++)
                    //{
                    //    strSql.Append("insert into PermitPresale(");
                    //    strSql.Append("PremisesId,Name,CreateTime,UpdateTime,IsDel");
                    //    strSql.Append(") values (");
                    //    strSql.Append("@num,'" + PermitPresale[i] + "',GETDATE(),GETDATE(),0");
                    //    strSql.Append(") ");
                    //}

                    #region 地铁

                    if (!string.IsNullOrEmpty(model.TId))
                    {
                        for (int i = 0; i < subways.Count; i++)
                        {
                            strSql.AppendFormat(@"
    INSERT  INTO PremisesSubway ( PId, Tid, TName, VId, CreateTime, UpdateTime, IsDel )
    VALUES  ( {0}, {1}, N'{2}', @num, GETDATE(), GETDATE(), 0 )", subways[i].PId, subways[i].TId, subways[i].Name);
                        }
                    }

                    #endregion

                    strSql.Append("COMMIT ");
                    strSql.Append("END TRY ");
                    strSql.Append("BEGIN CATCH ");
                    strSql.Append("ROLLBACK TRAN ");
                    strSql.Append("END CATCH ");
                    strSql.Append("END ");


                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@id", SqlDbType.Int),
                        new SqlParameter("@BId", model.BId),
                        new SqlParameter("@TId", model.TId),
                        new SqlParameter("@BName", model.BName),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@ReferencePrice", model.ReferencePrice),
                        new SqlParameter("@TelePhone", model.TelePhone),
                        new SqlParameter("@SalesStatus", model.SalesStatus),
                        new SqlParameter("@Ring", model.Ring),
                        new SqlParameter("@PremisesAddress", model.PremisesAddress),
                        new SqlParameter("@salesAddress", model.salesAddress),
                        new SqlParameter("@Developer", model.Developer),
                        new SqlParameter("@DeveloperId", model.DeveloperId),
                        new SqlParameter("@Agent", model.Agent),
                        new SqlParameter("@SalePermit", model.SalePermit),
                        new SqlParameter("@PropertyRight", model.PropertyRight),
                        new SqlParameter("@BuildingArea", model.BuildingArea),
                        new SqlParameter("@Area", model.Area),
                        new SqlParameter("@UserCount", model.UserCount),
                        new SqlParameter("@Characteristic", model.Characteristic),
                        new SqlParameter("@BuildingType", model.BuildingType),
                        new SqlParameter("@PropertyType", model.PropertyType),
                        new SqlParameter("@AreaRatio", model.AreaRatio),
                        new SqlParameter("@InnerCode", model.InnerCode),
                        new SqlParameter("@RoomRate", model.RoomRate),
                        new SqlParameter("@PropertyPrice", model.PropertyPrice),
                        new SqlParameter("@ParkingLot", model.ParkingLot),
                        new SqlParameter("@Renovation", model.Renovation),
                        new SqlParameter("@PropertyCompany", model.PropertyCompany),
                        new SqlParameter("@GreeningRate", model.GreeningRate),
                        new SqlParameter("@TrafficIntroduction", model.TrafficIntroduction),
                        new SqlParameter("@SupportingIntroduction", model.SupportingIntroduction),
                        new SqlParameter("@PremisesIntroduction", model.PremisesIntroduction),
                        new SqlParameter("@Lng", model.Lng),
                        new SqlParameter("@Lat", model.Lat),
                        new SqlParameter("@ProvinceId", model.ProvinceId),
                        new SqlParameter("@CreateTime", model.CreateTime),
                        new SqlParameter("@UpdateTime", model.UpdateTime),
                        new SqlParameter("@IsDel", model.IsDel),
                        new SqlParameter("@ProvinceName", model.ProvinceName),
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),
                        new SqlParameter("@DId", model.DId),
                        new SqlParameter("@DName", model.DName),
                        new SqlParameter("@return",ParameterDirection.ReturnValue),
                     
                    };

                    parameters[0].Direction = ParameterDirection.Output;
                    dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);

                    int result = Convert.ToInt32(parameters[0].Value);
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/27 19:01:45
        /// 功能描述：发布楼盘
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PermitPresale"></param>
        /// <returns></returns>
        public int Add(Premis model, List<SandTableLabel> list, List<Traffic> subways)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    strSql.Append("BEGIN ");
                    strSql.Append("BEGIN TRY ");
                    strSql.Append("BEGIN TRAN ");

                    strSql.Append("insert into Premises(");
                    strSql.Append("BId,TId,BName,Name,ReferencePrice,TelePhone,SalesStatus,Ring,PremisesAddress,salesAddress,Developer,DeveloperId,Agent,SalePermit,PropertyRight,BuildingArea,Area,UserCount,Characteristic,BuildingType,PropertyType,AreaRatio,InnerCode,RoomRate,PropertyPrice,ParkingLot,Renovation,PropertyCompany,GreeningRate,TrafficIntroduction,SupportingIntroduction,PremisesIntroduction,Lng,Lat,ProvinceId,CreateTime,UpdateTime,IsDel,ProvinceName,CityId,CityName,DId,DName,Phone400,IsShow400");
                    strSql.Append(") values (");
                    strSql.Append("@BId,@TId,@BName,@Name,@ReferencePrice,@TelePhone,@SalesStatus,@Ring,@PremisesAddress,@salesAddress,@Developer,@DeveloperId,@Agent,@SalePermit,@PropertyRight,@BuildingArea,@Area,@UserCount,@Characteristic,@BuildingType,@PropertyType,@AreaRatio,@InnerCode,@RoomRate,@PropertyPrice,@ParkingLot,@Renovation,@PropertyCompany,@GreeningRate,@TrafficIntroduction,@SupportingIntroduction,@PremisesIntroduction,@Lng,@Lat,@ProvinceId,@CreateTime,@UpdateTime,@IsDel,@ProvinceName,@CityId,@CityName,@DId,@DName,'',0");
                    strSql.Append(") ");

                    strSql.Append(" SELECT @id= SCOPE_IDENTITY()");

                    strSql.Append(" Declare @num int ");
                    strSql.Append(" set @num=(select @@IDENTITY) ");


                    //#region 预售许可证
                    //for (int i = 0; i < PermitPresale.Length; i++)
                    //{
                    //    strSql.Append("insert into PermitPresale(");
                    //    strSql.Append("PremisesId,Name,CreateTime,UpdateTime,IsDel");
                    //    strSql.Append(") values (");
                    //    strSql.Append("@num,'" + PermitPresale[i] + "',GETDATE(),GETDATE(),0");
                    //    strSql.Append(") ");
                    //}
                    //#endregion

                    #region 楼盘沙盘图
                    foreach (var item in list)
                    {
                        strSql.Append("insert into SandTableLabel(");
                        strSql.Append("DeveloperId,PremisesId,SandBox,Number,CoordX,CoordY,CreateTIme");
                        strSql.Append(") values (");
                        strSql.Append("@DeveloperId,@num,'" + item.SandBox + "'," + item.Number + "," + item.CoordX + "," + item.CoordY + ",GETDATE()");
                        strSql.Append(") ");
                    }
                    #endregion

                    #region 地铁

                    if (!string.IsNullOrEmpty(model.TId))
                    {
                        for (int i = 0; i < subways.Count; i++)
                        {
                            strSql.AppendFormat(@"
    INSERT  INTO PremisesSubway ( PId, Tid, TName, VId, CreateTime, UpdateTime, IsDel )
    VALUES  ( {0}, {1}, N'{2}', @num, GETDATE(), GETDATE(), 0 )", subways[i].PId, subways[i].TId, subways[i].Name);
                        }
                    }

                    #endregion


                    strSql.Append("COMMIT ");
                    strSql.Append("END TRY ");
                    strSql.Append("BEGIN CATCH ");
                    strSql.Append("ROLLBACK TRAN ");
                    strSql.Append("END CATCH ");
                    strSql.Append("END ");




                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@id", SqlDbType.Int),
                        new SqlParameter("@BId", model.BId),
                        new SqlParameter("@TId", model.TId),
                        new SqlParameter("@BName", model.BName),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@ReferencePrice", model.ReferencePrice),
                        new SqlParameter("@TelePhone", model.TelePhone),
                        new SqlParameter("@SalesStatus", model.SalesStatus),
                        new SqlParameter("@Ring", model.Ring),
                        new SqlParameter("@PremisesAddress", model.PremisesAddress),
                        new SqlParameter("@salesAddress", model.salesAddress),
                        new SqlParameter("@Developer", model.Developer),
                        new SqlParameter("@DeveloperId", model.DeveloperId),
                        new SqlParameter("@Agent", model.Agent),
                        new SqlParameter("@SalePermit", model.SalePermit),
                        new SqlParameter("@PropertyRight", model.PropertyRight),
                        new SqlParameter("@BuildingArea", model.BuildingArea),
                        new SqlParameter("@Area", model.Area),
                        new SqlParameter("@UserCount", model.UserCount),
                        new SqlParameter("@Characteristic", model.Characteristic),
                        new SqlParameter("@BuildingType", model.BuildingType),
                        new SqlParameter("@PropertyType", model.PropertyType),
                        new SqlParameter("@AreaRatio", model.AreaRatio),
                        new SqlParameter("@InnerCode", model.InnerCode),
                        new SqlParameter("@RoomRate", model.RoomRate),
                        new SqlParameter("@PropertyPrice", model.PropertyPrice),
                        new SqlParameter("@ParkingLot", model.ParkingLot),
                        new SqlParameter("@Renovation", model.Renovation),
                        new SqlParameter("@PropertyCompany", model.PropertyCompany),
                        new SqlParameter("@GreeningRate", model.GreeningRate),
                        new SqlParameter("@TrafficIntroduction", model.TrafficIntroduction),
                        new SqlParameter("@SupportingIntroduction", model.SupportingIntroduction),
                        new SqlParameter("@PremisesIntroduction", model.PremisesIntroduction),
                        new SqlParameter("@Lng", model.Lng),
                        new SqlParameter("@Lat", model.Lat),
                        new SqlParameter("@ProvinceId", model.ProvinceId),
                        new SqlParameter("@CreateTime", model.CreateTime),
                        new SqlParameter("@UpdateTime", model.UpdateTime),
                        new SqlParameter("@IsDel", model.IsDel),
                        new SqlParameter("@ProvinceName", model.ProvinceName),
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),
                        new SqlParameter("@DId", model.DId),
                        new SqlParameter("@DName", model.DName),
                        new SqlParameter("@return",ParameterDirection.ReturnValue),
                     
                    };

                    parameters[0].Direction = ParameterDirection.Output;
                    dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);

                    int result = Convert.ToInt32(parameters[0].Value);
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region 删除楼盘

        public int DelPremises(int pid)
        {
            using (var dbEnt = new kyj_NewHouseDBEntities())
            {
                string sql = @"UPDATE dbo.PermitPresale SET IsDel=1,UpdateTime=GETDATE() WHERE PremisesId=@id;
                                UPDATE dbo.Premises SET IsDel=1,UpdateTime=GETDATE() WHERE Id=@id";
                SqlParameter[] parameter =
                    {
                        new SqlParameter("@id",pid)
                    };
                return dbEnt.ExecuteStoreCommand(sql, parameter);
            }
        }

        #endregion
        #region 修改楼盘
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/3 16:08:54
        /// 功能描述：修改楼盘
        /// </summary>
        /// <param name="model"></param>
        /// <param name="permitPresale"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(Premis model, List<SandTableLabel> list, List<Traffic> subways)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    strSql.Append("BEGIN ");
                    strSql.Append("BEGIN TRY ");
                    strSql.Append("BEGIN TRAN ");

                    strSql.Append(" declare @OldSalesStatus int ");
                    strSql.Append(" Set @OldSalesStatus=(select SalesStatus from Premises WHERE Id=@Id and DeveloperId=@DeveloperId  ) ");


                    strSql.Append("UPDATE Premises SET ");
                    strSql.Append(
                        "ProvinceId=@ProvinceId,ProvinceName=@ProvinceName,CityId=@CityId,CityName=@CityName,DId=@DId,DName=@DName,BId=@BId,");
                    strSql.Append(
                        "BName=@BName,TId=@TId,Name=@Name,ReferencePrice=@ReferencePrice,TelePhone=@TelePhone,SalesStatus=@SalesStatus,Ring=@Ring,PremisesAddress=@PremisesAddress,salesAddress=@salesAddress,Developer=@Developer,");
                    strSql.Append(
                        "Agent=@Agent,SalePermit=@SalePermit,PropertyRight=@PropertyRight,BuildingArea=@BuildingArea,Area=@Area,UserCount=@UserCount,Characteristic=@Characteristic,BuildingType=@BuildingType,PropertyType=@PropertyType,AreaRatio=@AreaRatio,");
                    strSql.Append(
                        "RoomRate=@RoomRate,PropertyPrice=@PropertyPrice,ParkingLot=@ParkingLot,Renovation=@Renovation,PropertyCompany=@PropertyCompany,GreeningRate=@GreeningRate,TrafficIntroduction=@TrafficIntroduction,SupportingIntroduction=@SupportingIntroduction,PremisesIntroduction=@PremisesIntroduction,Lng=@Lng,");
                    strSql.Append("Lat=@Lat,UpdateTime=@UpdateTime");

                    strSql.Append(" WHERE Id=@Id and DeveloperId=@DeveloperId ");

                    //strSql.Append("Declare @num int ");
                    //strSql.Append("set @num=(select @@IDENTITY) ");

                    ////删除 预售许可证书！ 重新添加
                    //strSql.Append("DELETE FROM PermitPresale where PremisesId=@id ");

                    //for (int i = 0; i < permitPresale.Length; i++)
                    //{
                    //    strSql.Append("insert into PermitPresale(");
                    //    strSql.Append("PremisesId,Name,CreateTime,UpdateTime,IsDel");
                    //    strSql.Append(") values (");
                    //    strSql.Append("@id,'" + permitPresale[i] + "',GETDATE(),GETDATE(),0");
                    //    strSql.Append(") ");
                    //}

                    //删除 楼盘沙盘坐标！ 重新添加
                    strSql.Append("DELETE FROM SandTableLabel where PremisesId=@id and DeveloperId=@DeveloperId ");
                    foreach (var item in list)
                    {
                        strSql.Append("insert into SandTableLabel(");
                        strSql.Append("DeveloperId,PremisesId,SandBox,Number,CoordX,CoordY,CreateTIme");
                        strSql.Append(") values (");
                        strSql.Append("@DeveloperId,@id,'" + item.SandBox + "'," + item.Number + "," + item.CoordX + "," + item.CoordY + ",GETDATE()");
                        strSql.Append(") ");
                    }

                    //更新该楼盘下的所有楼栋 沙盘号关联信息
                    strSql.Append(" Update Building set PNum=0 where DeveloperId=@DeveloperId And PremisesId=@id ");


                    #region 地铁
                    //删除楼盘地铁关系表数据 重新添加！
                    strSql.Append("DELETE FROM PremisesSubway where VId=@id ");


                    if (!string.IsNullOrEmpty(model.TId))
                    {
                        for (int i = 0; i < subways.Count; i++)
                        {
                            strSql.AppendFormat(@"
    INSERT  INTO PremisesSubway ( PId, Tid, TName, VId, CreateTime, UpdateTime, IsDel )
    VALUES  ( {0}, {1}, N'{2}', @id, GETDATE(), GETDATE(), 0 )", subways[i].PId, subways[i].TId, subways[i].Name);
                        }
                    }

                    #endregion


                    #region 当楼盘或者楼栋修改为售罄状态
                    //当楼盘或者楼栋修改为售罄状态，
                    //则只是把在售、待售状态的房源改为售罄状态，
                    //其他状态（已认购、已签约、已备案、已办产权、被限制、开发商保留、拆迁安置）
                    //的房源不做改动。

                    strSql.Append(" IF(@SalesStatus=2 and @OldSalesStatus<>2 ) ");

                    strSql.Append(" BEGIN ");

                    strSql.Append(" Update House set SalesStatus=9 where IsDel=0 and PremisesId=@Id ");
                    //strSql.Append(" AND House.SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
                    //strSql.Append(@" And House.Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
                    //      JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
                    //      WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
                    //      GROUP BY h.Id )");

                    strSql.Append("Update Building set state=3 where state in (1,2) and IsDel=0 and PremisesId=@Id  ");

                    strSql.Append(" END ");
                    #endregion

                    #region 当楼盘或者楼栋修改为售罄在售
                    strSql.Append(" IF(@SalesStatus=1 and @OldSalesStatus<>1) ");
                    strSql.Append(" BEGIN ");

                    strSql.Append(" Update House set SalesStatus=2 where IsDel=0 and PremisesId=@Id ");
                    //                    strSql.Append(" AND House.SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
                    //                    strSql.Append(@" And House.Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
                    //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
                    //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
                    //                          GROUP BY h.Id )");

                    strSql.Append("Update Building set state=2 where state in (1,2,3) and IsDel=0 and PremisesId=@Id  ");

                    strSql.Append(" END ");
                    #endregion

                    #region 当楼盘修改状态修改待售
                    strSql.Append(" IF(@SalesStatus=0 and @OldSalesStatus<>0) ");
                    strSql.Append(" BEGIN ");

                    strSql.Append(" Update House set SalesStatus=0 where IsDel=0 and PremisesId=@Id ");
                    //                    strSql.Append(" AND House.SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
                    //                    strSql.Append(@" And House.Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
                    //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
                    //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
                    //                          GROUP BY h.Id )");

                    strSql.Append("Update Building set state=1 where state in (1,2,3) and IsDel=0 and PremisesId=@Id  ");

                    strSql.Append(" END ");

                    #endregion









                    strSql.Append("COMMIT TRAN ");
                    strSql.Append("END TRY ");
                    strSql.Append("BEGIN CATCH ");
                    strSql.Append("ROLLBACK TRAN ");
                    strSql.Append("END CATCH ");
                    strSql.Append("END ");



                    object[] parameters =
                    {
                        new SqlParameter("@Id", model.Id),

                        new SqlParameter("@DeveloperId", model.DeveloperId),
                        new SqlParameter("@InnerCode", model.InnerCode),
                        new SqlParameter("@ProvinceId", model.ProvinceId),
                        new SqlParameter("@ProvinceName", model.ProvinceName),
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),

                        new SqlParameter("@DId", model.DId),
                        new SqlParameter("@DName", model.DName),
                        new SqlParameter("@BId", model.BId),
                        new SqlParameter("@BName", model.BName),

                        new SqlParameter("@TId", model.TId),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@ReferencePrice", model.ReferencePrice),
                        new SqlParameter("@TelePhone", model.TelePhone),
                        new SqlParameter("@SalesStatus", model.SalesStatus),


                        new SqlParameter("@Ring", model.Ring),
                        new SqlParameter("@PremisesAddress", model.PremisesAddress),
                        new SqlParameter("@salesAddress", model.salesAddress),
                        new SqlParameter("@Developer", model.Developer),

                        new SqlParameter("@Agent", model.Agent),
                        new SqlParameter("@SalePermit", model.SalePermit),
                        new SqlParameter("@PropertyRight", model.PropertyRight),
                        new SqlParameter("@BuildingArea", model.BuildingArea),
                        new SqlParameter("@Area", model.Area),
                        new SqlParameter("@UserCount", model.UserCount),
                        new SqlParameter("@Characteristic", model.Characteristic),
                        new SqlParameter("@BuildingType", model.BuildingType),
                        new SqlParameter("@PropertyType", model.PropertyType),
                        new SqlParameter("@AreaRatio", model.AreaRatio),

                        new SqlParameter("@RoomRate", model.RoomRate),
                        new SqlParameter("@PropertyPrice", model.PropertyPrice),
                        new SqlParameter("@ParkingLot", model.ParkingLot),
                        new SqlParameter("@Renovation", model.Renovation),
                        new SqlParameter("@PropertyCompany", model.PropertyCompany),
                        new SqlParameter("@GreeningRate", model.GreeningRate),
                        new SqlParameter("@TrafficIntroduction", model.TrafficIntroduction),
                        new SqlParameter("@SupportingIntroduction", model.SupportingIntroduction),
                        new SqlParameter("@PremisesIntroduction", model.PremisesIntroduction),

                        new SqlParameter("@Lng", model.Lng),
                        new SqlParameter("@Lat", model.Lat),
                        new SqlParameter("@UpdateTime", model.UpdateTime)

                    };

                    int result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                    return result;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0}", model.Id));
                throw e;
            }
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/9 13:37:46
        /// 功能描述：修改楼盘
        /// </summary>
        /// <param name="model"></param>
        /// <param name="permitPresale"></param>
        /// <returns></returns>
        public int Update(Premis model, List<Traffic> subways)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    strSql.Append("BEGIN ");
                    strSql.Append("BEGIN TRY ");
                    strSql.Append("BEGIN TRAN ");


                    strSql.Append(" declare @OldSalesStatus int ");
                    strSql.Append(" Set @OldSalesStatus=(select SalesStatus from Premises WHERE Id=@Id and DeveloperId=@DeveloperId  ) ");


                    strSql.Append("UPDATE Premises SET ");
                    strSql.Append(
                        "ProvinceId=@ProvinceId,ProvinceName=@ProvinceName,CityId=@CityId,CityName=@CityName,DId=@DId,DName=@DName,BId=@BId,");
                    strSql.Append(
                        "BName=@BName,TId=@TId,Name=@Name,ReferencePrice=@ReferencePrice,TelePhone=@TelePhone,SalesStatus=@SalesStatus,Ring=@Ring,PremisesAddress=@PremisesAddress,salesAddress=@salesAddress,Developer=@Developer,");
                    strSql.Append(
                        "Agent=@Agent,SalePermit=@SalePermit,PropertyRight=@PropertyRight,BuildingArea=@BuildingArea,Area=@Area,UserCount=@UserCount,Characteristic=@Characteristic,BuildingType=@BuildingType,PropertyType=@PropertyType,AreaRatio=@AreaRatio,");
                    strSql.Append(
                        "RoomRate=@RoomRate,PropertyPrice=@PropertyPrice,ParkingLot=@ParkingLot,Renovation=@Renovation,PropertyCompany=@PropertyCompany,GreeningRate=@GreeningRate,TrafficIntroduction=@TrafficIntroduction,SupportingIntroduction=@SupportingIntroduction,PremisesIntroduction=@PremisesIntroduction,Lng=@Lng,");
                    strSql.Append("Lat=@Lat,UpdateTime=@UpdateTime");

                    strSql.Append(" WHERE Id=@Id  and DeveloperId=@DeveloperId ");

                    //strSql.Append("Declare @num int ");
                    //strSql.Append("set @num=(select @@IDENTITY) ");

                    //删除 预售许可证书！ 重新添加
                    //strSql.Append("DELETE FROM PermitPresale where PremisesId=@id ");

                    //for (int i = 0; i < permitPresale.Length; i++)
                    //{
                    //    strSql.Append("insert into PermitPresale(");
                    //    strSql.Append("PremisesId,Name,CreateTime,UpdateTime,IsDel");
                    //    strSql.Append(") values (");
                    //    strSql.Append("@id,'" + permitPresale[i] + "',GETDATE(),GETDATE(),0");
                    //    strSql.Append(") ");
                    //}

                    #region 地铁
                    //删除楼盘地铁关系表数据 重新添加！
                    strSql.Append("DELETE FROM PremisesSubway where VId=@id ");


                    if (!string.IsNullOrEmpty(model.TId))
                    {
                        for (int i = 0; i < subways.Count; i++)
                        {
                            strSql.AppendFormat(@"
    INSERT  INTO PremisesSubway ( PId, Tid, TName, VId, CreateTime, UpdateTime, IsDel )
    VALUES  ( {0}, {1}, N'{2}', @id, GETDATE(), GETDATE(), 0 )", subways[i].PId, subways[i].TId, subways[i].Name);
                        }
                    }

                    #endregion

                    //当楼盘或者楼栋修改为售罄状态，
                    //则只是把在售、待售状态的房源改为售罄状态，
                    //其他状态（已认购、已签约、已备案、已办产权、被限制、开发商保留、拆迁安置）
                    //的房源不做改动。


                    strSql.Append(" IF(@SalesStatus=2 and @OldSalesStatus<>2) ");
                    strSql.Append(" BEGIN ");

                    strSql.Append(" Update House set SalesStatus=9 where IsDel=0 and PremisesId=@Id ");
                    //                    strSql.Append(" AND House.SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
                    //                    strSql.Append(@" And House.Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
                    //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
                    //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
                    //                          GROUP BY h.Id )");

                    strSql.Append("Update Building set state=3 where state in (1,2) and IsDel=0 and PremisesId=@Id  ");
                    strSql.Append(" END ");

                    strSql.Append(" IF(@SalesStatus=1 and @OldSalesStatus<>1) ");
                    strSql.Append(" BEGIN ");

                    strSql.Append(" Update House set SalesStatus=2 where IsDel=0 and PremisesId=@Id ");
                    //                    strSql.Append(" AND House.SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
                    //                    strSql.Append(@" And House.Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
                    //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
                    //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
                    //                          GROUP BY h.Id )");

                    strSql.Append("Update Building set state=2 where state in (1,2,3) and IsDel=0 and PremisesId=@Id  ");

                    strSql.Append(" END ");

                    #region 当楼盘修改状态修改待售
                    strSql.Append(" IF(@SalesStatus=0 and @OldSalesStatus<>0) ");
                    strSql.Append(" BEGIN ");

                    strSql.Append(" Update House set SalesStatus=0 where IsDel=0 and PremisesId=@Id ");
                    //                    strSql.Append(" AND House.SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 ) ");
                    //                    strSql.Append(@" And House.Id  Not IN ( SELECT h.Id FROM dbo.House h JOIN dbo.ActivitiesHouse ah ON h.Id=ah.HouseId
                    //                          JOIN dbo.Activities a ON ah.ActivitiesId=a.Id
                    //                          WHERE h.IsDel=0 AND ah.IsDel=0 AND a.IsDel=0 AND a.ActivitieState!=2 AND a.EndTime>GETDATE()
                    //                          GROUP BY h.Id )");

                    strSql.Append("Update Building set state=1 where state in (1,2,3) and IsDel=0 and PremisesId=@Id  ");

                    strSql.Append(" END ");

                    #endregion



                    strSql.Append("COMMIT TRAN ");
                    strSql.Append("END TRY ");
                    strSql.Append("BEGIN CATCH ");
                    strSql.Append("ROLLBACK TRAN ");
                    strSql.Append("END CATCH ");
                    strSql.Append("END ");



                    object[] parameters =
                    {
                        new SqlParameter("@Id", model.Id),
                        new SqlParameter("@DeveloperId", model.DeveloperId),
                        new SqlParameter("@InnerCode", model.InnerCode),
                        new SqlParameter("@ProvinceId", model.ProvinceId),
                        new SqlParameter("@ProvinceName", model.ProvinceName),
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),

                        new SqlParameter("@DId", model.DId),
                        new SqlParameter("@DName", model.DName),
                        new SqlParameter("@BId", model.BId),
                        new SqlParameter("@BName", model.BName),

                        new SqlParameter("@TId", model.TId),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@ReferencePrice", model.ReferencePrice),
                        new SqlParameter("@TelePhone", model.TelePhone),
                        new SqlParameter("@SalesStatus", model.SalesStatus),


                        new SqlParameter("@Ring", model.Ring),
                        new SqlParameter("@PremisesAddress", model.PremisesAddress),
                        new SqlParameter("@salesAddress", model.salesAddress),
                        new SqlParameter("@Developer", model.Developer),

                        new SqlParameter("@Agent", model.Agent),
                        new SqlParameter("@SalePermit", model.SalePermit),
                        new SqlParameter("@PropertyRight", model.PropertyRight),
                        new SqlParameter("@BuildingArea", model.BuildingArea),
                        new SqlParameter("@Area", model.Area),
                        new SqlParameter("@UserCount", model.UserCount),
                        new SqlParameter("@Characteristic", model.Characteristic),
                        new SqlParameter("@BuildingType", model.BuildingType),
                        new SqlParameter("@PropertyType", model.PropertyType),
                        new SqlParameter("@AreaRatio", model.AreaRatio),

                        new SqlParameter("@RoomRate", model.RoomRate),
                        new SqlParameter("@PropertyPrice", model.PropertyPrice),
                        new SqlParameter("@ParkingLot", model.ParkingLot),
                        new SqlParameter("@Renovation", model.Renovation),
                        new SqlParameter("@PropertyCompany", model.PropertyCompany),
                        new SqlParameter("@GreeningRate", model.GreeningRate),
                        new SqlParameter("@TrafficIntroduction", model.TrafficIntroduction),
                        new SqlParameter("@SupportingIntroduction", model.SupportingIntroduction),
                        new SqlParameter("@PremisesIntroduction", model.PremisesIntroduction),

                        new SqlParameter("@Lng", model.Lng),
                        new SqlParameter("@Lat", model.Lat),
                        new SqlParameter("@UpdateTime", model.UpdateTime)

                    };

                    int result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                    return result;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0}", model.Id));
                throw e;
            }
        }

        /// <summary>
        /// 多表进行修改
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="permitPresale">参数</param>
        /// <returns></returns>
        public int ModifyPremise(Premis model, string[] permitPresale)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    strSql.Append("BEGIN ");
                    strSql.Append("BEGIN TRY ");
                    strSql.Append("BEGIN TRAN ");

                    strSql.Append("UPDATE Premises SET ");
                    strSql.Append(
                        "DeveloperId=@DeveloperId,InnerCode=@InnerCode,ProvinceId=@ProvinceId,ProvinceName=@ProvinceName,CityId=@CityId,CityName=@CityName,DId=@DId,DName=@DName,BId=@BId,");
                    strSql.Append(
                        "BName=@BName,TId=@TId,Name=@Name,ReferencePrice=@ReferencePrice,TelePhone=@TelePhone,SalesStatus=@SalesStatus,Ring=@Ring,PremisesAddress=@PremisesAddress,salesAddress=@salesAddress,Developer=@Developer,");
                    strSql.Append(
                        "Agent=@Agent,SalePermit=@SalePermit,PropertyRight=@PropertyRight,BuildingArea=@BuildingArea,Area=@Area,UserCount=@UserCount,Characteristic=@Characteristic,BuildingType=@BuildingType,PropertyType=@PropertyType,AreaRatio=@AreaRatio,");
                    strSql.Append(
                        "RoomRate=@RoomRate,PropertyPrice=@PropertyPrice,ParkingLot=@ParkingLot,Renovation=@Renovation,PropertyCompany=@PropertyCompany,GreeningRate=@GreeningRate,TrafficIntroduction=@TrafficIntroduction,SupportingIntroduction=@SupportingIntroduction,PremisesIntroduction=@PremisesIntroduction,Lng=@Lng,");
                    strSql.Append("Lat=@Lat,UpdateTime=@UpdateTime");

                    strSql.Append(" WHERE Id=@Id ");

                    strSql.Append("Declare @num int ");
                    strSql.Append("set @num=(select @@IDENTITY) ");

                    //strSql.Append("DELETE FROM PermitPresale");
                    //strSql.Append("SET Name='" + permitPresale[i] + "',UpdateTime=GETDATE()");
                    //strSql.Append("WHERE PremisesId=@num ");

                    strSql.Append("UPDATE Building ");
                    strSql.Append("SET State=CASE WHEN @SalesStatus=0 THEN 0 ELSE 2 END ");
                    strSql.Append("WHERE PremisesId=@num ");

                    strSql.Append("UPDATE House ");
                    strSql.Append("SET SalesStatus=CASE WHEN @SalesStatus=0 THEN 9 ELSE 9 END ");
                    strSql.Append("WHERE PremisesId=@num ");

                    strSql.Append("COMMIT TRAN ");
                    strSql.Append("END TRY ");
                    strSql.Append("BEGIN CATCH ");
                    strSql.Append("ROLLBACK TRAN ");
                    strSql.Append("END CATCH ");
                    strSql.Append("END ");



                    object[] parameters =
                    {
                        new SqlParameter("@Id", model.Id),
                        new SqlParameter("@DeveloperId", model.DeveloperId),
                        new SqlParameter("@InnerCode", model.InnerCode),
                        new SqlParameter("@ProvinceId", model.ProvinceId),
                        new SqlParameter("@ProvinceName", model.ProvinceName),
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),

                        new SqlParameter("@DId", model.DId),
                        new SqlParameter("@DName", model.DName),
                        new SqlParameter("@BId", model.BId),
                        new SqlParameter("@BName", model.BName),

                        new SqlParameter("@TId", model.TId),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@ReferencePrice", model.ReferencePrice),
                        new SqlParameter("@TelePhone", model.TelePhone),
                        new SqlParameter("@SalesStatus", model.SalesStatus),


                        new SqlParameter("@Ring", model.Ring),
                        new SqlParameter("@PremisesAddress", model.PremisesAddress),
                        new SqlParameter("@salesAddress", model.salesAddress),
                        new SqlParameter("@Developer", model.Developer),

                        new SqlParameter("@Agent", model.Agent),
                        new SqlParameter("@SalePermit", model.SalePermit),
                        new SqlParameter("@PropertyRight", model.PropertyRight),
                        new SqlParameter("@BuildingArea", model.BuildingArea),
                        new SqlParameter("@Area", model.Area),
                        new SqlParameter("@UserCount", model.UserCount),
                        new SqlParameter("@Characteristic", model.Characteristic),
                        new SqlParameter("@BuildingType", model.BuildingType),
                        new SqlParameter("@PropertyType", model.PropertyType),
                        new SqlParameter("@AreaRatio", model.AreaRatio),

                        new SqlParameter("@RoomRate", model.RoomRate),
                        new SqlParameter("@PropertyPrice", model.PropertyPrice),
                        new SqlParameter("@ParkingLot", model.ParkingLot),
                        new SqlParameter("@Renovation", model.Renovation),
                        new SqlParameter("@PropertyCompany", model.PropertyCompany),
                        new SqlParameter("@GreeningRate", model.GreeningRate),
                        new SqlParameter("@TrafficIntroduction", model.TrafficIntroduction),
                        new SqlParameter("@SupportingIntroduction", model.SupportingIntroduction),
                        new SqlParameter("@PremisesIntroduction", model.PremisesIntroduction),

                        new SqlParameter("@Lng", model.Lng),
                        new SqlParameter("@Lat", model.Lat),
                        new SqlParameter("@UpdateTime", model.UpdateTime)

                    };

                    int result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        ///// <summary>
        ///// 提交报名信息（排号，摇号）
        ///// Author:sunlin
        ///// </summary>
        ///// <param name="activitiesUser"></param>
        ///// <param name="houseId"></param>
        ///// <returns></returns>
        //public int SubOfferInfo(TXOrm.ActivitiesUser activitiesUser, int houseId)
        //{
        //    try
        //    {
        //        using (var db = new kyj_NewHouseDBEntities())
        //        {
        //            string sql = @"INSERT INTO [ActivitiesUser]([ActivitiesId],[DeveloperId],[UserId],[Name],[InnerCode],[Bond],[Mobile],[IDCard],[Sex],[Num],[CreateTime],[UpdateTime],[IsDel]) VALUES (@ActivitiesId,@DeveloperId,@UserId,@Name,@InnerCode,@Bond,@Mobile,@IDCard,@Sex,@Num,@CreateTime,@UpdateTime,@IsDel)";
        //            var sqlparms = new object[]
        //            {
        //                new SqlParameter("@ActivitiesId", activitiesUser.ActivitiesId),
        //                //new SqlParameter("@DeveloperId", activitiesUser.DeveloperId),
        //                new SqlParameter("@UserId", activitiesUser.UserId),
        //                new SqlParameter("@Name", activitiesUser.Name),
        //                //new SqlParameter("@InnerCode", activitiesUser.InnerCode),
        //                new SqlParameter("@Bond", activitiesUser.Bond),
        //                new SqlParameter("@Mobile", activitiesUser.Mobile),
        //                new SqlParameter("@IDCard", activitiesUser.IDCard),
        //                new SqlParameter("@Sex", activitiesUser.Sex),
        //                new SqlParameter("@Num", activitiesUser.Num),
        //                new SqlParameter("@CreateTime", activitiesUser.CreateTime),
        //                new SqlParameter("@UpdateTime", activitiesUser.UpdateTime),
        //                new SqlParameter("@IsDel", activitiesUser.IsDel)
        //            };

        //            string hsql = @"UPDATE [House] SET [SalesStatus] = 3 WHERE Id=@Id";
        //            var hsqlparms = new object[]
        //            {
        //                new SqlParameter("@Id", houseId)
        //            };

        //            var result = db.ExecuteStoreCommand(sql, sqlparms);
        //            var hresult = db.ExecuteStoreCommand(hsql, hsqlparms);
        //            if (0 < result && 0 < hresult)
        //            {
        //                return 1;
        //            }

        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log4netService.RecordLog.RecordException("sunlin", null, ex);
        //        return 0;
        //    }
        //}

        /// <summary>
        /// 提交报价信息(（打折，一口价，秒杀，阶梯团购）=改house，竞价，砍价)
        /// Author:sunlin
        /// </summary>
        /// <param name="bidPrice"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public int SubBidPriceInfo(TXOrm.BidPrice bidPrice)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"INSERT INTO [BidPrice](
                                            [ActivitiesId],[HouseId],[BidUserId],[BidUserName],[BidUserMobile],[BidUserPrice],[BidUserQQ],[BidUserMail],
                                            [BidCount],[Status],[BidType],[CreateTime],[UpdateTime],[IsDel]) 
                                   VALUES (@ActivitiesId,@HouseId,@BidUserId,@BidUserName,@BidUserMobile,@BidUserPrice,@BidUserQQ,@BidUserMail,
                                           (select count(0)+1 from [BidPrice] where [ActivitiesId]=@ActivitiesId and [BidUserId]=@BidUserId and [IsDel]=0),
                                           @Status, @BidType,getdate(),getdate(), @IsDel)";
                    var sqlparms = new object[]
                    {
                        new SqlParameter("@ActivitiesId", bidPrice.ActivitiesId),
                        new SqlParameter("@HouseId", bidPrice.HouseId),
                        new SqlParameter("@BidUserId", bidPrice.BidUserId),
                        new SqlParameter("@BidUserName", bidPrice.BidUserName),
                        new SqlParameter("@BidUserPrice", bidPrice.BidUserPrice),
                        new SqlParameter("@BidUserQQ", bidPrice.BidUserQQ),
                        new SqlParameter("@BidUserMobile", bidPrice.BidUserMobile),
                        new SqlParameter("@BidUserMail", bidPrice.BidUserMail),
                        //new SqlParameter("@BidCount", bidPrice.BidCount),
                        new SqlParameter("@Status", bidPrice.Status),
                        new SqlParameter("@IsDel",false)
                    };
                    var result = db.ExecuteStoreCommand(sql, sqlparms);
                    if (0 < result)
                    {
                        return 1;
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("sunlin", null, ex);
                return 0;
            }
        }

        /// <summary>
        /// 提交报价信息
        /// </summary>
        /// <param name="bidPrice">出价实体</param>
        /// <param name="isUpdateSalesStatus">是否修改房源状态</param>
        /// <param name="houseId">房源ID</param>
        /// <returns></returns>
        public int SubBidPriceInfo(TXOrm.BidPrice bidPrice, bool isUpdateSalesStatus, int houseId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"declare @BidCount int;
                                 select @BidCount=count(0)+1 from [BidPrice] where [ActivitiesId]=@ActivitiesId and [BidUserId]=@BidUserId and [IsDel]=0;
                                 INSERT INTO [BidPrice](
                                      [ActivitiesId],[HouseId],[BidUserId],[BidUserName],[BidRealName],[IDCard],[BidUserMobile],[BidUserPrice],
                                      [BidUserNumber],[BidUserQQ],[BidUserMail],[InnerCode],[BidCount],[Status],[CreateTime],[UpdateTime],[IsDel]) 
                                 VALUES (@ActivitiesId,@HouseId,@BidUserId,@BidUserName,@BidRealName,@IDCard,@BidUserMobile,@BidUserPrice,
                                       0,@BidUserQQ,@BidUserMail,@InnerCode,@BidCount,@Status,getdate(),getdate(), @IsDel);";
                    var sqlparms = new object[]
                    {
                        new SqlParameter("@ActivitiesId", bidPrice.ActivitiesId),
                        new SqlParameter("@HouseId", bidPrice.HouseId),
                        new SqlParameter("@BidUserId", bidPrice.BidUserId),
                        new SqlParameter("@BidUserName", bidPrice.BidUserName),
                        new SqlParameter("@IDCard", bidPrice.IDCard),
                        new SqlParameter("@BidUserPrice", bidPrice.BidUserPrice),
                        new SqlParameter("@BidUserNumber", bidPrice.BidUserNumber),
                        new SqlParameter("@BidUserQQ", bidPrice.BidUserQQ),
                        new SqlParameter("@BidUserMobile", bidPrice.BidUserMobile),
                        new SqlParameter("@BidUserMail", bidPrice.BidUserMail),
                        new SqlParameter("@InnerCode", bidPrice.InnerCode),
                        //new SqlParameter("@BidCount", bidPrice.BidCount),
                        new SqlParameter("@Status", bidPrice.Status),
                        new SqlParameter("@BidRealName", bidPrice.BidRealName),
                        new SqlParameter("@CreateTime", bidPrice.CreateTime),
                        new SqlParameter("@UpdateTime", bidPrice.UpdateTime),
                        new SqlParameter("@IsDel", bidPrice.IsDel)
                    };

                    //先判断是否需要修改房源状态
                    if (isUpdateSalesStatus)
                    {
                        //如果需要修改房源状态，则需要判定房源状态是否为 在出售 状态
                        string hsql = @"UPDATE [House] SET [SalesStatus] = 3 WHERE Id=@Id";
                        string ms = " and SalesStatus=2";
                        var hsqlparms = new object[]
                        {
                        new SqlParameter("@Id", houseId)
                        };
                        var result = 0;
                        //如果为出售状态，则插入出价记录，
                        var hresult = 0;//db.ExecuteStoreCommand(bidPrice.BidType == 3 ? hsql + ms : hsql, hsqlparms);
                        if (hresult > 0)
                        {
                            result = db.ExecuteStoreCommand(sql, sqlparms);
                        }
                        if (0 < result && 0 < hresult)
                        {
                            return 1;
                        }
                        //否则不插入出价记录返回0
                        return 0;
                    }
                    else//否则只插入出价记录，   
                    {
                        var result = db.ExecuteStoreCommand(sql, sqlparms);
                        if (0 < result)
                        {
                            return 1;
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("sunlin", null, ex);
                return 0;
            }
        }

        /// <summary>
        /// 返回户型图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetROOMTYPEImageUseCount(string id)
        {

            using (var newhouseDb = new kyj_NewHouseDBEntities())
            {
                int i = int.Parse(id);

                return newhouseDb.Houses.Count(r => r.RId == i && r.IsDel == false).ToString();

            }

        }
        /// <summary>
        /// 返回平面图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPlaneImageUseCount(string id)
        {

            using (var newhouseDb = new kyj_NewHouseDBEntities())
            {
                return newhouseDb.Buildings.Count(r => r.PictureData.Trim() == id.Trim() && r.IsDel == false).ToString();

            }

        }
        /// <summary>
        /// 跟新已使用户型图
        /// </summary>
        /// <param name="id"></param>
        public string UpdateROOMTYPEImageUse(string id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"update house set rid=-1 where  rid=" + id;

                    var result = db.ExecuteStoreCommand(sql);
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return "0";

            }
        }
        /// <summary>
        /// 更新已使用平面图
        /// </summary>
        /// <param name="id"></param>
        public string UpdatePlaneImageUse(string id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"update building set picturedata='' where  picturedata='" + id + "'";

                    var result = db.ExecuteStoreCommand(sql);
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return "0";

            }
        }


        #region 同地区的楼盘名称不可重复，如重复则提示“楼盘名称已存在”
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/11 15:37:45
        /// 功能描述：同地区的楼盘名称不可重复，如重复则提示“楼盘名称已存在
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Did"></param>
        /// <returns></returns>
        public int CheckPremises(string Name, int Did)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = "select count(1) as result from Premises  WITH (NoLock) where [Name]=@Name and Did=@Did and IsDel=0";
                    SqlParameter[] param = new SqlParameter[]{
                        new SqlParameter("@Name",Name),
                        new SqlParameter("@Did",Did)
                    };
                    return dbEnt.ExecuteStoreQuery<int>(sql, param).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("Name:{0},Did:{1}", Name, Did), ex);
                throw;
            }
        }
        #endregion


        #region 根据楼盘ID 查询楼盘状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/24 15:49:49
        /// 功能描述：根据楼盘ID 查询楼盘状态
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public int GetPremisesState(int PremisesId)
        {
            int i = -1;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = @"select SalesStatus  AS Result from Premises  WITH (NoLock) where  id=@PremisesId";
                    SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@PremisesId",PremisesId)

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
                Log4netService.RecordLog.RecordException("谢江", string.Format("PremisesId:{0}", PremisesId), ex);
                throw;
            }
            return i;
        }
        #endregion

        /// <summary>
        /// 根据楼盘id获取楼盘浏览量和排名
        /// Author:maboxun
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TXOrm.PremisesViewCount GetPremisesViewCount(int id)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    return newhouseDb.PremisesViewCounts.Where(p => p.PremisesId == id).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 取今日成交信息
        /// </summary>
        /// <param name="premisesId">楼盘ID</param>
        /// <returns></returns>
        public PremisStatisticsInfo GetPremisStatisticsInfo(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"select * from PremisStatisticsInfo where PremisId=@PremisId and CONVERT(varchar(12) , CreateTime, 111 )=CONVERT(varchar(12) ,GETDATE(), 111 )";
                    SqlParameter[] para = new SqlParameter[]{
                        new SqlParameter("@PremisId",premisesId)
                     };
                    return db.ExecuteStoreQuery<PremisStatisticsInfo>(sql, para).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("sunlin", null, ex);
                return null;
            }
        }

        /// <summary>
        /// 添加今日成交信息
        /// </summary>
        /// <param name="id"></param>
        public int SubBargainInfo(string premisesId, string SubscribedCount, string BargainCount)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"DECLARE @tem INT
                    SELECT @tem=Id from PremisStatisticsInfo where PremisId=@PremisesId and CONVERT(varchar(12) , CreateTime, 111 )=CONVERT(varchar(12) , GETDATE(), 111 )
                    IF(@tem IS NULL)
	                    INSERT INTO dbo.PremisStatisticsInfo
	                            ( PremisId ,
	                              InputTime ,
	                              SubscribedCount ,
	                              BargainCount ,
	                              UpdateTime ,
	                              CreateTime
	                            )
	                    VALUES  ( @PremisesId , -- PremisId - int
	                              GETDATE() , -- InputTime - datetime
	                              @SubscribedCount , -- SubscribedCount - int认购数
	                              @BargainCount, -- BargainCount - int成交数
	                              GETDATE() , -- UpdateTime - datetime
	                              GETDATE()  -- CreateTime - datetime
	                            )
                    ELSE
	                    UPDATE dbo.PremisStatisticsInfo SET SubscribedCount=@SubscribedCount,BargainCount=@BargainCount,updateTime=GETDATE() WHERE PremisId=@PremisesId";
                    SqlParameter[] para = new SqlParameter[]{
                        new SqlParameter("@PremisesId",premisesId),
                        new SqlParameter("@SubscribedCount",SubscribedCount),
                        new SqlParameter("@BargainCount",BargainCount),
                        new SqlParameter("@CreateTime",DateTime.Now.ToShortDateString())
                            
                     };
                    var result = db.ExecuteStoreCommand(sql, para);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return 0;

            }
        }
    }
}
