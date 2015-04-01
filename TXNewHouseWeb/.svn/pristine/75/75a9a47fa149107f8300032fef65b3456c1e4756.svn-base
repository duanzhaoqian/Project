using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Collections.Generic;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXModel.Geography;
using TXOrm;


namespace TXDal.HouseData
{
    /// <summary>
    /// 楼盘DAL类-管理后台
    /// </summary>
    public partial class PremisesDal : BaseDal_Admin
    {
        #region 楼盘信息列表

        /// <summary>
        /// 楼盘信息列表
        /// Author:wangzhikun
        /// </summary>
        /// <param name="premises">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="devidisnull"> 开发商id是否为空</param>
        /// <returns></returns>
        public List<PVM_NH_Premises> GetPremisesListByPages(PVS_NH_Premises premises, int pageIndex, int pageSize, bool devidisnull)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
                       SELECT * FROM (
                                       SELECT  ROW_NUMBER() OVER ( ORDER BY premises.CreateTime DESC ) AS RowID,
                                         premises.Id,
				                         premises.Name,
				                         premises.PropertyType,
				                         premises.Developer,
				                         premises.ProvinceID,
				                         premises.ProvinceName,
				                         premises.CityId,
				                         premises.CityName,
				                         premises.SalesStatus                   

				                         FROM Premises as premises (NOLOCK)                    
				                         WHERE premises.IsDel=0  ";
                    if (devidisnull)
                    {
                        sql += " and ISNULL(DeveloperId,0)=0 ";
                    }
                    sql += string.Format(@"{0}
                                                            ) HOSE  
			                            WHERE   HOSE.RowID BETWEEN {1} AND {2} ", new object[]
                                            {
                                                GetSearchSqlMess(premises),
                                                ((pageIndex - 1)*pageSize) + 1,
                                                pageIndex*pageSize
                                            });
                    #region 执行操作

                    ObjectResult<PVM_NH_Premises> query = houseDb.ExecuteStoreQuery<PVM_NH_Premises>(sql);
                    List<PVM_NH_Premises> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0},{1},{2})", new object[] { premises, pageIndex, pageSize }), ex);
                throw;
            }
        }

        #endregion

        #region 楼盘信息总记录数

        /// <summary>
        /// 楼盘信息总记录数
        /// Author:wangzhikun
        /// </summary>
        /// <param name="premises">实体</param>
        /// <param name="devidisnull">开发商id是否为空</param>
        /// <returns></returns>
        public int GetPremisesListByPageCounts(PVS_NH_Premises premises, bool devidisnull)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
                            SELECT COUNT(1) AS Result
			                        FROM Premises as premises (NOLOCK)               
			                        WHERE premises.IsDel=0 ";
                    if (devidisnull)
                    {
                        sql += " and ISNULL(DeveloperId,0)=0 ";
                    }
                    sql += string.Format(@"{0}                                       
                                    ", new object[]
                    {
                        GetSearchSqlMess(premises)
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
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }

        #endregion

        #region 获取查询楼盘列表sqlwhere语句

        /// <summary>
        /// 获取查询楼盘列表sqlwhere语句
        /// </summary>
        /// <param name="premises">实体</param>
        /// <returns></returns>
        public string GetSearchSqlMess(PVS_NH_Premises premises)
        {

            var whereStr = new StringBuilder();

            #region 过滤数据

            //根据省份搜索
            if (premises.ProvinceID > 0)
            {
                whereStr.AppendFormat(" AND premises.ProvinceID={0}", premises.ProvinceID);
            }
            //根据城市搜索
            if (premises.CityID > 0)
            {
                whereStr.AppendFormat(" AND premises.CityID={0}", premises.CityID);
            }

            //根据销售状态搜索
            if (premises.SalesStatus != -1)
            {
                whereStr.AppendFormat(" AND premises.SalesStatus={0}", premises.SalesStatus);
            }

            //根据开发商名称模糊查询
            if (!string.IsNullOrWhiteSpace(premises.DeveloperName))
            {
                whereStr.AppendFormat(@"AND ( premises.Developer LIKE '{0}%')", premises.DeveloperName);
            }

            //根据楼盘名称模糊查询
            if (!string.IsNullOrWhiteSpace(premises.PremisesName))
            {
                whereStr.AppendFormat(@"AND ( premises.Name LIKE '{0}%')", premises.PremisesName);
            }


            #endregion

            return whereStr.ToString();
        }

        /// <summary>
        /// SQL执行返回首行首列(整型)
        /// Author:wangzhikun
        /// </summary>
        private class SqlResult_ScalarInt
        {
            public int? Result { set; get; }
        }

        #endregion

        #region 获取楼盘列表(下拉列表)

        /// <summary>
        /// 获取所有楼盘列表(下拉列表)
        /// author:liyuzhao
        /// </summary>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPremisesForSelectItems()
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Premises.Where(it => it.IsDel == false).OrderBy(it => it.CreateTime).Select(it => new Z_GeographyItem { GeographyCode = it.Id, GeographyName = it.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", null, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据省、市、开发商编号获取所有楼盘列表(下拉列表)
        /// author:liyuzhao
        /// </summary>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPremisesByProvinceOrCityOrDeveloperIdForSelectItems(int provinceId, int cityId, int developerId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    sql.Append(@"
SELECT  Id AS GeographyCode, Name AS GeographyName
FROM    Premises (NOLOCK)
WHERE   1 = 1");
                    if (0 < provinceId)
                    {
                        sql.AppendFormat(@"
        AND ProvinceID = {0}", provinceId);
                    }

                    if (0 < cityId)
                    {
                        sql.AppendFormat(@"
        AND CityId = {0}", cityId);
                    }

                    if (0 < developerId)
                    {
                        sql.AppendFormat(@"
        AND DeveloperId = {0}", developerId);
                    }

                    sql.Append(@"
ORDER BY CreateTime");

                    var list = db.ExecuteStoreQuery<Z_GeographyItem>(sql.ToString()).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", null, ex);
                return null;
            }
        }

        #endregion


        #region 获取实体

        /// <summary>
        /// 获取实体
        /// author: wangzhikun
        /// </summary>
        /// <param name="id">楼盘Id</param>
        /// <returns></returns>
        public PVM_NH_Premises GetPVM_PremisesById(int id)
        {
            using (var newhouseDb = new kyj_NewHouseDBEntities())
            {
                var query = newhouseDb.Premises.FirstOrDefault(it => it.Id == id && it.IsDel == false);
                var model = new PVM_NH_Premises();
                if (query != null)
                {
                    model.Id = query.Id;
                    model.DeveloperId = query.DeveloperId;
                    model.InnerCode = query.InnerCode;
                    model.ProvinceId = query.ProvinceId;
                    model.ProvinceName = query.ProvinceName;
                    model.CityId = query.CityId;
                    model.CityName = query.CityName;
                    model.DId = query.DId;
                    model.DName = query.DName;
                    model.BId = query.BId;
                    model.BName = query.BName;
                    model.TId = query.TId;
                    model.Name = query.Name;
                    model.ReferencePrice = query.ReferencePrice;
                    model.TelePhone = query.TelePhone;
                    model.SalesStatus = query.SalesStatus;
                    model.Ring = query.Ring;
                    model.PremisesAddress = query.PremisesAddress;
                    model.SalesAddress = query.salesAddress;
                    model.Developer = query.Developer;
                    model.Agent = query.Agent;
                    model.SalePermit = query.SalePermit;
                    model.PropertyRight = query.PropertyRight;
                    model.BuildingArea = query.BuildingArea;
                    model.Area = query.Area;
                    model.UserCount = query.UserCount;
                    model.Characteristic = query.Characteristic;
                    model.BuildingType = query.BuildingType;
                    model.PropertyType = query.PropertyType;
                    model.AreaRatio = query.AreaRatio;
                    model.RoomRate = query.RoomRate;
                    model.PropertyPrice = query.PropertyPrice;
                    model.ParkingLot = query.ParkingLot;
                    model.Renovation = query.Renovation;
                    model.PropertyCompany = query.PropertyCompany;
                    model.GreeningRate = query.GreeningRate;
                    model.TrafficIntroduction = query.TrafficIntroduction;
                    model.SupportingIntroduction = query.SupportingIntroduction;
                    model.PremisesIntroduction = query.PremisesIntroduction;
                    model.Lng = query.Lng;
                    model.Lat = query.Lat;
                    model.Phone400 = query.Phone400;
                    model.IsShow400 = query.IsShow400;
                    model.PageUrl = query.pageUrl;
                    return model;
                }
                return null;
            }
        }

        #endregion

        #region 根据城市Id获取楼盘名称

        /// <summary>
        ///  根据城市Id获取楼盘名称
        /// </summary>
        /// <param name="provinceId">省份Id</param> 
        /// <param name="cityId">城市Id</param> 
        /// <returns></returns>
        public List<string> GetPremiseNameById(int provinceId, int cityId)
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.Premises.Where(it => it.ProvinceId == provinceId && it.CityId == cityId && it.IsDel == false).Select(it => it.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("wangzhikun", string.Format("provinceId:{0}, cityId:{1}", provinceId, cityId), ex);
                return null;
            }
        }

        #endregion

        #region 获取指定城市的基本信息
        /// <summary>
        /// 获取指定城市的基本信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public Area Z_GetAreaByCityID(int cityId)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {

                    var query = (from area in webEntity.Areas where area.Id == cityId select area).FirstOrDefault();
                    return query;
                }
            }
            catch (Exception ex)
            {
                //Log4netService.RecordLog.RecordException("wnagzhikun", string.Format("cityId:{0}, tid:{1}", cityId, tid), ex);
                throw;
            }
        }
        #endregion

        #region 获取指定城市的地铁线列表

        /// <summary>
        /// 获取指定城市的地铁线 列表
        /// </summary>
        /// <param name="cityId">城市Id</param>
        /// <param name="tid">父id</param>
        /// <returns></returns>
        public List<Z_GeographyItem> Z_GetTrafficsByCityIDAndTId(int cityId, int tid)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {

                    var query = (from traffic in webEntity.Traffic where traffic.PId == tid && traffic.CityId == cityId && !traffic.IsDel select traffic).OrderBy(it => it.Sort);
                    return query.ToList().ConvertAll(it => new Z_GeographyItem { GeographyCode = it.TId, GeographyName = it.Name });
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("wnagzhikun", string.Format("cityId:{0}, tid:{1}", cityId, tid), ex);
                throw;
            }
        }

        #endregion

        #region 页面获取指定地铁线路下面的每一个站

        /// <summary>
        /// 页面获取指定地铁线路下面的每一个站
        /// </summary>
        /// <param name="tid">地铁线ID</param>
        /// <returns></returns>
        public List<Z_GeographyItem> Z_GetTrafficsByID(int tid)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    var query = (from traffic in webEntity.Traffic where traffic.PId == tid && !traffic.IsDel select new Z_GeographyItem { GeographyCode = traffic.TId, GeographyName = traffic.Name });
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("wangzhikun", string.Format("tid:{0}", tid), ex);
                throw;
            }
        }

        #endregion

        #region 根据省市，关键字获取楼盘信息
        /// <summary>
        /// 根据省市，关键字获取楼盘列表
        /// author:wangdk
        /// </summary>
        /// <param name="cityID">城市标识</param>
        /// <param name="keywords">关键字</param>
        /// <returns>楼盘ID,名称 or null</returns>
        public List<ESqlResult_SelectList> GetPremises_ByCityIDOrKeywords(int cityID, string keywords)
        {
            try
            {
                Dictionary<int, string> dicPremises = new Dictionary<int, string>();
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    sql.Append(@"SELECT ID,Name FROM Premises (NOLOCK) WHERE  IsDel=0");
                    List<SqlParameter> paras = new List<SqlParameter>();
                    if (cityID > 0)
                    {
                        sql.Append(" and CityId=@CityId");
                        paras.Add(new SqlParameter("@CityId", cityID));
                    }

                    if (!string.IsNullOrEmpty(keywords))
                    {
                        sql.Append(" and Name like @Name");
                        paras.Add(new SqlParameter("@Name", "%" + keywords + "%"));
                    }
                    sql.Append(" ORDER BY CreateTime");
                    var result = db.ExecuteStoreQuery<ESqlResult_SelectList>(sql.ToString(), paras.ToArray()).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王东坤", string.Format("根据省市，关键字获取楼盘信息:城市标识{0},关键字{1}", cityID, keywords), ex);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="subways"></param>
        /// <param name="permitpresales"></param>
        /// <param name="sandTables"></param>
        /// <returns></returns>
        public int AddNew(PVE_NH_Premises2 premises, List<Traffic> subways, List<SandTableLabel> sandTables)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    sql.Append(@"
BEGIN TRY
    BEGIN TRAN
    DECLARE @tPremisesId INT
    INSERT  INTO Premises ( DeveloperId, InnerCode, ProvinceId, ProvinceName, CityId, CityName, DId, DName, BId, BName,
                            TId, Name, ReferencePrice, TelePhone, SalesStatus, Ring, PremisesAddress, salesAddress,
                            Developer, Agent, SalePermit, PropertyRight, BuildingArea, Area, UserCount, Characteristic,
                            BuildingType, PropertyType, AreaRatio, RoomRate, PropertyPrice, ParkingLot, Renovation,
                            PropertyCompany, GreeningRate, TrafficIntroduction, SupportingIntroduction,
                            PremisesIntroduction, Lng, Lat, Phone400, IsShow400, CreateTime, UpdateTime, IsDel )
    VALUES  ( @DeveloperId, @InnerCode, @ProvinceId, @ProvinceName, @CityId, @CityName, @DId, @DName, @BId, @BName, @TId,
              @Name, @ReferencePrice, @TelePhone, @SalesStatus, @Ring, @PremisesAddress, @salesAddress, @Developer,
              @Agent, @SalePermit, @PropertyRight, @BuildingArea, @Area, @UserCount, @Characteristic, @BuildingType,
              @PropertyType, @AreaRatio, @RoomRate, @PropertyPrice, @ParkingLot, 0, @PropertyCompany,
              @GreeningRate, @TrafficIntroduction, @SupportingIntroduction, @PremisesIntroduction, @Lng, @Lat, '', 0, GETDATE(),
              GETDATE(), 0 )

    IF 0 < @@ERROR 
    BEGIN
        SELECT  '0' AS Code, '楼盘添加失败' AS Msg
        ROLLBACK TRAN
        RETURN
    END

    SET @tPremisesId = @@IDENTITY
    
    -- 地铁");
                    var parmsList = new List<object>
                    {
                        new SqlParameter("DeveloperId", premises.DeveloperId),
                        new SqlParameter("InnerCode", premises.InnerCode),
                        new SqlParameter("ProvinceId", premises.ProvinceId),
                        new SqlParameter("ProvinceName", premises.ProvinceName),
                        new SqlParameter("CityId", premises.CityId),
                        new SqlParameter("CityName", premises.CityName),
                        new SqlParameter("DId", premises.DId),
                        new SqlParameter("DName", premises.DName),
                        new SqlParameter("BId", premises.BId),
                        new SqlParameter("BName", premises.BName),
                        new SqlParameter("TId", premises.TId),

                        new SqlParameter("Name", premises.Name),
                        new SqlParameter("ReferencePrice", premises.ReferencePrice),
                        new SqlParameter("TelePhone", premises.TelePhone),
                        new SqlParameter("SalesStatus", premises.SalesStatus),
                        new SqlParameter("Ring", premises.Ring),
                        new SqlParameter("PremisesAddress", premises.PremisesAddress),
                        new SqlParameter("salesAddress", premises.SalesAddress),
                        new SqlParameter("Developer", premises.Developer),

                        new SqlParameter("Agent", premises.Agent),
                        new SqlParameter("SalePermit", string.Empty), // premises.SalePermit
                        new SqlParameter("PropertyRight", premises.PropertyRight),
                        new SqlParameter("BuildingArea", premises.BuildingArea),
                        new SqlParameter("Area", premises.Area),
                        new SqlParameter("UserCount", premises.UserCount),
                        new SqlParameter("Characteristic", premises.Characteristic),
                        new SqlParameter("BuildingType", premises.BuildingType),

                        new SqlParameter("PropertyType", premises.PropertyType),
                        new SqlParameter("AreaRatio", premises.AreaRatio),
                        new SqlParameter("RoomRate", premises.RoomRate),
                        new SqlParameter("PropertyPrice", premises.PropertyPrice),
                        new SqlParameter("ParkingLot", premises.ParkingLot),
                        //new SqlParameter("Renovation", 0), // premises.Renovation
                        new SqlParameter("PropertyCompany", premises.PropertyCompany),

                        new SqlParameter("GreeningRate", premises.GreeningRate),
                        new SqlParameter("TrafficIntroduction", premises.TrafficIntroduction),
                        new SqlParameter("SupportingIntroduction", premises.SupportingIntroduction),
                        new SqlParameter("PremisesIntroduction", premises.PremisesIntroduction),
                        new SqlParameter("Lng", premises.Lng),
                        new SqlParameter("Lat", premises.Lat)
                    };

                    #region 地铁

                    if (!string.IsNullOrEmpty(premises.TId))
                    {
                        for (int i = 0; i < subways.Count; i++)
                        {
                            sql.AppendFormat(@"
    INSERT  INTO PremisesSubway ( PId, Tid, TName, VId, CreateTime, UpdateTime, IsDel )
    VALUES  ( {0}, {1}, N'{2}', @tPremisesId, GETDATE(), GETDATE(), 0 )", subways[i].PId, subways[i].TId, subways[i].Name);
                        }

                        sql.AppendFormat(@"
    IF 0 < @@ERROR 
    BEGIN
        SELECT  '0' AS Code, '地铁添加失败' AS Msg
        ROLLBACK TRAN
        RETURN
    END");
                    }

                    #endregion


                    #region 预售许可证

                    if (null != premises.PermitPresales
                        && 0 < premises.PermitPresales.Count)
                    {
                        sql.Append(@"
    -- 预售许可证");
                        for (int i = 0; i < premises.PermitPresales.Count; i++)
                        {
                            sql.AppendFormat(@"
    INSERT  INTO PermitPresale ( PremisesId, Name, CreateTime, UpdateTime, IsDel )
    VALUES  ( @tPremisesId, N'{0}', GETDATE(), GETDATE(), 0 )", premises.PermitPresales[i].Name);
                        }

                        sql.AppendFormat(@"
    IF 0 < @@ERROR 
    BEGIN
        SELECT  '0' AS Code, '预售许可证添加失败' AS Msg
        ROLLBACK TRAN
        RETURN
    END");
                    }

                    #endregion

                    #region 沙盘图

                    if (null != sandTables
                        && 0 < sandTables.Count)
                    {
                        sql.Append(@"
    -- 沙盘图");
                        for (int i = 0; i < sandTables.Count; i++)
                        {
                            sql.AppendFormat(@"
    INSERT  INTO SandTableLabel ( DeveloperId, PremisesId, SandBox, Number, CoordX, CoordY, CreateTime )
    VALUES  ( @DeveloperId, @tPremisesId, N'{0}', {1}, {2}, {3}, GETDATE() )", sandTables[i].SandBox, sandTables[i].Number, sandTables[i].CoordX, sandTables[i].CoordY);
                        }

                        sql.AppendFormat(@"
    IF 0 < @@ERROR 
    BEGIN
        SELECT  '0' AS Code, '沙盘图添加失败' AS Msg
        ROLLBACK TRAN
        RETURN
    END");
                    }

                    #endregion

                    sql.Append(@"
COMMIT TRAN
    SELECT  CAST(@tPremisesId AS NVARCHAR(20)) AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    SELECT  '0' AS Code, '操作失败' AS Msg
    ROLLBACK TRAN
END CATCH");
                    var list = db.ExecuteStoreQuery<ESqlResult>(Convert.ToString(sql), parmsList.ToArray()).ToList();

                    if (list.Any())
                    {
                        return Convert.ToInt32(list[0].Code);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", premises, ex);
                return 0;
            }
        }

        /// <summary>
        /// 根据楼盘信息获取同地域同名数量
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public int GetNameCountByPremises(PVE_NH_Premises2 premises)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  COUNT(1) AS Result
FROM    Premises (NOLOCK)
WHERE   Name = '{0}'
        AND IsDel = 0
        AND DId = {1}", premises.Name, premises.DId);

                    if (0 < premises.Id)
                    {
                        sql += string.Format(@"
        AND Id <> {0}", premises.Id);
                    }

                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 1;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", premises, ex);
                return 1;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="subways"></param>
        /// <param name="sandTables"></param>
        /// <returns></returns>
        public int UpdatePremises(PVE_NH_Premises2 premises, List<Traffic> subways, List<SandTableLabel> sandTables)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    sql.Append(@"
BEGIN TRY
    BEGIN TRAN
    -- 更新 楼盘
    UPDATE  Premises
    SET     DeveloperId = @DeveloperId ,InnerCode = @InnerCode, ProvinceId = @ProvinceId, ProvinceName = @ProvinceName, CityId = @CityId,
            CityName = @CityName, DId = @DId, DName = @DName, BId = @BId, BName = @BName, TId = @TId, Name = @Name,
            ReferencePrice = @ReferencePrice, TelePhone = @TelePhone, SalesStatus = @SalesStatus, Ring = @Ring,
            PremisesAddress = @PremisesAddress, salesAddress = @salesAddress, Developer = @Developer, Agent = @Agent,
            SalePermit = @SalePermit, PropertyRight = @PropertyRight, BuildingArea = @BuildingArea, Area = @Area,
            UserCount = @UserCount, Characteristic = @Characteristic, BuildingType = @BuildingType,
            PropertyType = @PropertyType, AreaRatio = @AreaRatio, RoomRate = @RoomRate, PropertyPrice = @PropertyPrice,
            ParkingLot = @ParkingLot, PropertyCompany = @PropertyCompany,
            GreeningRate = @GreeningRate, TrafficIntroduction = @TrafficIntroduction,
            SupportingIntroduction = @SupportingIntroduction, PremisesIntroduction = @PremisesIntroduction, Lng = @Lng,
            Lat = @Lat, UpdateTime = GETDATE()
    WHERE   Id = @PremisesId
    ");
                    var parmsList = new List<object>
                    {
                        new SqlParameter("PremisesId", premises.Id),
                        new SqlParameter("DeveloperId", premises.DeveloperId),
                        new SqlParameter("InnerCode", premises.InnerCode),
                        new SqlParameter("ProvinceId", premises.ProvinceId),
                        new SqlParameter("ProvinceName", premises.ProvinceName),
                        new SqlParameter("CityId", premises.CityId),
                        new SqlParameter("CityName", premises.CityName),
                        new SqlParameter("DId", premises.DId),
                        new SqlParameter("DName", premises.DName),
                        new SqlParameter("BId", premises.BId),
                        new SqlParameter("BName", premises.BName),
                        new SqlParameter("TId", premises.TId),

                        new SqlParameter("Name", premises.Name),
                        new SqlParameter("ReferencePrice", premises.ReferencePrice),
                        new SqlParameter("TelePhone", premises.TelePhone),
                        new SqlParameter("SalesStatus", premises.SalesStatus),
                        new SqlParameter("Ring", premises.Ring),
                        new SqlParameter("PremisesAddress", premises.PremisesAddress),
                        new SqlParameter("salesAddress", premises.SalesAddress),
                        new SqlParameter("Developer", premises.Developer),

                        new SqlParameter("Agent", premises.Agent),
                        new SqlParameter("SalePermit", string.Empty), // premises.SalePermit
                        new SqlParameter("PropertyRight", premises.PropertyRight),
                        new SqlParameter("BuildingArea", premises.BuildingArea),
                        new SqlParameter("Area", premises.Area),
                        new SqlParameter("UserCount", premises.UserCount),
                        new SqlParameter("Characteristic", premises.Characteristic),
                        new SqlParameter("BuildingType", premises.BuildingType),

                        new SqlParameter("PropertyType", premises.PropertyType),
                        new SqlParameter("AreaRatio", premises.AreaRatio),
                        new SqlParameter("RoomRate", premises.RoomRate),
                        new SqlParameter("PropertyPrice", premises.PropertyPrice),
                        new SqlParameter("ParkingLot", premises.ParkingLot),
                        //new SqlParameter("Renovation", 0), // premises.Renovation
                        new SqlParameter("PropertyCompany", premises.PropertyCompany),

                        new SqlParameter("GreeningRate", premises.GreeningRate),
                        new SqlParameter("TrafficIntroduction", premises.TrafficIntroduction),
                        new SqlParameter("SupportingIntroduction", premises.SupportingIntroduction),
                        new SqlParameter("PremisesIntroduction", premises.PremisesIntroduction),
                        new SqlParameter("Lng", premises.Lng),
                        new SqlParameter("Lat", premises.Lat)
                    };

                    #region 地铁

                    sql.Append(@"
    -- 更新 地铁
    UPDATE  PremisesSubway
    SET     IsDel = 1
    WHERE   VId = @PremisesId");

                    if (!string.IsNullOrEmpty(premises.TId))
                    {
                        for (int i = 0; i < subways.Count; i++)
                        {
                            sql.AppendFormat(@"
    INSERT  INTO PremisesSubway ( PId, Tid, TName, VId, CreateTime, UpdateTime, IsDel )
    VALUES  ( {0}, {1}, N'{2}', @PremisesId, GETDATE(), GETDATE(), 0 )", subways[i].PId, subways[i].TId, subways[i].Name);
                        }
                    }

                    #endregion

                    #region 预售许可证

                    if (null != premises.PermitPresales
                        && 0 < premises.PermitPresales.Count)
                    {
                        for (int i = 0; i < premises.PermitPresales.Count; i++)
                        {
                            if (0 < premises.PermitPresales[i].Id)
                            {
                                // 修改原数据
                                sql.AppendFormat(@"
    UPDATE  PermitPresale
    SET     Name = '{0}', IsDel = {2}, UpdateTime = GETDATE()
    WHERE   Id = {1}", premises.PermitPresales[i].Name, premises.PermitPresales[i].Id, premises.PermitPresales[i].IsDel);
                            }
                            else
                            {
                                // 添加新数据
                                sql.AppendFormat(@"
    INSERT  INTO PermitPresale ( PremisesId, Name, CreateTime, UpdateTime, IsDel )
    VALUES  ( @PremisesId, N'{0}', GETDATE(), GETDATE(), 0 )", premises.PermitPresales[i].Name);
                            }
                        }
                    }
                    else
                    {
                        // 原数据全部被删除的情况
                        sql.Append(@"
    -- 更新 预售许可证
    UPDATE  PermitPresale
    SET     IsDel = 1, UpdateTime = GETDATE()
    WHERE   PremisesId = @PremisesId");
                    }

                    #endregion

                    #region 沙盘图

                    if (1 == premises.IsUpdateSandbox)
                    {
                        sql.AppendFormat(@"
    -- 更新 沙盘图
    DELETE  FROM SandTableLabel
    WHERE   PremisesId = @PremisesId
            AND DeveloperId = {0}", premises.DeveloperId);
                        if (null != sandTables
                            && 0 < sandTables.Count)
                        {
                            sql.Append(@"
    -- 沙盘图");
                            for (int i = 0; i < sandTables.Count; i++)
                            {
                                sql.AppendFormat(@"
    INSERT  INTO SandTableLabel ( DeveloperId, PremisesId, SandBox, Number, CoordX, CoordY, CreateTime )
    VALUES  ( @DeveloperId, @PremisesId, N'{0}', {1}, {2}, {3}, GETDATE() )", sandTables[i].SandBox, sandTables[i].Number, sandTables[i].CoordX, sandTables[i].CoordY);
                            }
                        }
                    }

                    #endregion

                    sql.Append(@"
COMMIT TRAN
    SELECT  '1' AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作失败' AS Msg
END CATCH");

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql.ToString(), parmsList.ToArray()).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Code);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", premises, ex);
                return 0;
            }
        }

        /// <summary>
        /// 更新（状态变更）
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="subways"></param>
        /// <param name="sandTables"></param>
        /// <param name="buildingState"></param>
        /// <param name="houseState"></param>
        /// <returns></returns>
        public int UpdatePremises_StateChanged(PVE_NH_Premises2 premises, List<Traffic> subways, List<SandTableLabel> sandTables, int buildingState, int houseState)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = new StringBuilder();
                    sql.Append(@"
BEGIN TRY
    BEGIN TRAN
    -- 更新 楼盘
    UPDATE  Premises
    SET     DeveloperId = @DeveloperId ,InnerCode = @InnerCode, ProvinceId = @ProvinceId, ProvinceName = @ProvinceName, CityId = @CityId,
            CityName = @CityName, DId = @DId, DName = @DName, BId = @BId, BName = @BName, TId = @TId, Name = @Name,
            ReferencePrice = @ReferencePrice, TelePhone = @TelePhone, SalesStatus = @SalesStatus, Ring = @Ring,
            PremisesAddress = @PremisesAddress, salesAddress = @salesAddress, Developer = @Developer, Agent = @Agent,
            SalePermit = @SalePermit, PropertyRight = @PropertyRight, BuildingArea = @BuildingArea, Area = @Area,
            UserCount = @UserCount, Characteristic = @Characteristic, BuildingType = @BuildingType,
            PropertyType = @PropertyType, AreaRatio = @AreaRatio, RoomRate = @RoomRate, PropertyPrice = @PropertyPrice,
            ParkingLot = @ParkingLot, PropertyCompany = @PropertyCompany,
            GreeningRate = @GreeningRate, TrafficIntroduction = @TrafficIntroduction,
            SupportingIntroduction = @SupportingIntroduction, PremisesIntroduction = @PremisesIntroduction, Lng = @Lng,
            Lat = @Lat, UpdateTime = GETDATE()
    WHERE   Id = @PremisesId
    ");
                    var parmsList = new List<object>
                    {
                        new SqlParameter("PremisesId", premises.Id),
                        new SqlParameter("DeveloperId", premises.DeveloperId),
                        new SqlParameter("InnerCode", premises.InnerCode),
                        new SqlParameter("ProvinceId", premises.ProvinceId),
                        new SqlParameter("ProvinceName", premises.ProvinceName),
                        new SqlParameter("CityId", premises.CityId),
                        new SqlParameter("CityName", premises.CityName),
                        new SqlParameter("DId", premises.DId),
                        new SqlParameter("DName", premises.DName),
                        new SqlParameter("BId", premises.BId),
                        new SqlParameter("BName", premises.BName),
                        new SqlParameter("TId", premises.TId),

                        new SqlParameter("Name", premises.Name),
                        new SqlParameter("ReferencePrice", premises.ReferencePrice),
                        new SqlParameter("TelePhone", premises.TelePhone),
                        new SqlParameter("SalesStatus", premises.SalesStatus),
                        new SqlParameter("Ring", premises.Ring),
                        new SqlParameter("PremisesAddress", premises.PremisesAddress),
                        new SqlParameter("salesAddress", premises.SalesAddress),
                        new SqlParameter("Developer", premises.Developer),

                        new SqlParameter("Agent", premises.Agent),
                        new SqlParameter("SalePermit", string.Empty), // premises.SalePermit
                        new SqlParameter("PropertyRight", premises.PropertyRight),
                        new SqlParameter("BuildingArea", premises.BuildingArea),
                        new SqlParameter("Area", premises.Area),
                        new SqlParameter("UserCount", premises.UserCount),
                        new SqlParameter("Characteristic", premises.Characteristic),
                        new SqlParameter("BuildingType", premises.BuildingType),

                        new SqlParameter("PropertyType", premises.PropertyType),
                        new SqlParameter("AreaRatio", premises.AreaRatio),
                        new SqlParameter("RoomRate", premises.RoomRate),
                        new SqlParameter("PropertyPrice", premises.PropertyPrice),
                        new SqlParameter("ParkingLot", premises.ParkingLot),
                        //new SqlParameter("Renovation", 0), // premises.Renovation
                        new SqlParameter("PropertyCompany", premises.PropertyCompany),

                        new SqlParameter("GreeningRate", premises.GreeningRate),
                        new SqlParameter("TrafficIntroduction", premises.TrafficIntroduction),
                        new SqlParameter("SupportingIntroduction", premises.SupportingIntroduction),
                        new SqlParameter("PremisesIntroduction", premises.PremisesIntroduction),
                        new SqlParameter("Lng", premises.Lng),
                        new SqlParameter("Lat", premises.Lat)
                    };

                    #region 更新楼栋、房源状态

                    sql.AppendFormat(@"
UPDATE  Building
SET     State = {0}, UpdateTime = GETDATE()
WHERE   PremisesId = @PremisesId
        AND IsDel = 0
        
UPDATE  House
SET     SalesStatus = {1}, UpdateTime = GETDATE()
WHERE   PremisesId = @PremisesId
        AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 )
        AND IsDel = 0", buildingState, houseState);

                    #endregion

                    #region 地铁

                    sql.Append(@"
    -- 更新 地铁
    UPDATE  PremisesSubway
    SET     IsDel = 1
    WHERE   VId = @PremisesId");

                    if (!string.IsNullOrEmpty(premises.TId))
                    {
                        for (int i = 0; i < subways.Count; i++)
                        {
                            sql.AppendFormat(@"
    INSERT  INTO PremisesSubway ( PId, Tid, TName, VId, CreateTime, UpdateTime, IsDel )
    VALUES  ( {0}, {1}, N'{2}', @PremisesId, GETDATE(), GETDATE(), 0 )", subways[i].PId, subways[i].TId, subways[i].Name);
                        }
                    }

                    #endregion

                    #region 预售许可证

                    if (null != premises.PermitPresales
                        && 0 < premises.PermitPresales.Count)
                    {
                        for (int i = 0; i < premises.PermitPresales.Count; i++)
                        {
                            if (0 < premises.PermitPresales[i].Id)
                            {
                                // 修改原数据
                                sql.AppendFormat(@"
    UPDATE  PermitPresale
    SET     Name = '{0}', IsDel = {2}, UpdateTime = GETDATE()
    WHERE   Id = {1}", premises.PermitPresales[i].Name, premises.PermitPresales[i].Id, premises.PermitPresales[i].IsDel);
                            }
                            else
                            {
                                // 添加新数据
                                sql.AppendFormat(@"
    INSERT  INTO PermitPresale ( PremisesId, Name, CreateTime, UpdateTime, IsDel )
    VALUES  ( @PremisesId, N'{0}', GETDATE(), GETDATE(), 0 )", premises.PermitPresales[i].Name);
                            }
                        }
                    }
                    else
                    {
                        // 原数据全部被删除的情况
                        sql.Append(@"
    -- 更新 预售许可证
    UPDATE  PermitPresale
    SET     IsDel = 1, UpdateTime = GETDATE()
    WHERE   PremisesId = @PremisesId");
                    }

                    #endregion

                    #region 沙盘图

                    sql.Append(@"
    -- 更新 沙盘图
    DELETE  FROM SandTableLabel
    WHERE   PremisesId = @PremisesId
            AND DeveloperId = 0
");
                    if (null != sandTables
                        && 0 < sandTables.Count)
                    {
                        sql.Append(@"
    -- 沙盘图");
                        for (int i = 0; i < sandTables.Count; i++)
                        {
                            sql.AppendFormat(@"
    INSERT  INTO SandTableLabel ( DeveloperId, PremisesId, SandBox, Number, CoordX, CoordY, CreateTime )
    VALUES  ( @DeveloperId, @PremisesId, N'{0}', {1}, {2}, {3}, GETDATE() )", sandTables[i].SandBox, sandTables[i].Number, sandTables[i].CoordX, sandTables[i].CoordY);
                        }
                    }

                    #endregion

                    sql.Append(@"
COMMIT TRAN
    SELECT  '1' AS Code, '操作成功' AS Msg
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作失败' AS Msg
END CATCH");

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql.ToString(), parmsList.ToArray()).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Code);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", premises, ex);
                return 0;
            }
        }

        /// <summary>
        /// 根据Id集获取地铁
        /// </summary>
        /// <param name="ids">逗号分隔</param>
        /// <returns></returns>
        public List<Traffic> GetSubwaysByIds(string ids)
        {
            try
            {
                using (var db = new kyj_WebDBEntities())
                {
                    var sql = string.Format(@"
SELECT  *
FROM    Traffic (NOLOCK)
WHERE   TId IN ( {0} )
        AND IsDel = 0", ids);

                    var list = db.ExecuteStoreQuery<Traffic>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("ids:{0}", ids), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据楼盘编号获取预售许可证列表
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<PVS_NH_Premises_SalePermit> GetSalePermitByPremisesId(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = string.Format(@"
SELECT  a.Id, a.Name, 0 AS IsDel
FROM    PermitPresale (NOLOCK) AS a
WHERE   PremisesId = {0}
        AND IsDel = 0", premisesId);

                    var list = db.ExecuteStoreQuery<PVS_NH_Premises_SalePermit>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("premisesId:{0}", premisesId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据楼盘编号获取地铁信息列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<PremisesSubway> GetSubwaysByPremisesId(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    PremisesSubway (NOLOCK) AS a
WHERE   VId = {0}
        AND IsDel = 0", premisesId);

                    var list = db.ExecuteStoreQuery<PremisesSubway>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("premisesId:{0}", premisesId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据图片关联Id获取楼盘信息(使用BuildingId承载CityId)
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public List<House> GetHousesByRId(int rid)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  DISTINCT
        a.Id , PremisesId, b.CityId AS BuildingId
FROM    House (NOLOCK) AS a
        LEFT JOIN Premises AS b ON b.Id = a.PremisesId
WHERE   RId = {0}", rid);

                    var list = db.ExecuteStoreQuery<House>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("rid:{0}", rid), ex);
                return null;
            }
        }

        /// <summary>
        /// 在编辑楼盘信息时清空当前楼盘下所有楼栋的沙盘图标记信息
        /// </summary>
        /// <param name="premisesId"></param>
        public void UpdateBuildingPNumForUpdateSandBox(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
UPDATE  Building
SET     PNum = 0
WHERE   PremisesId = {0}", premisesId);

                    db.ExecuteStoreCommand(sql);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("premisesId:{0}", premisesId), ex);
            }
        }

        /// <summary>
        /// 根据活动编号获取楼盘信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public Premis GetEntity_ByActvityId(int activityId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    Premises (NOLOCK)
WHERE   Id = ( SELECT TOP 1
                        PremisesId
               FROM     ActivitiesHouse (NOLOCK)
               WHERE    IsDel = 0
                        AND ActivitiesId = {0}
             )", activityId);

                    var entity = db.ExecuteStoreQuery<Premis>(sql).FirstOrDefault();

                    return entity;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("activityId:{0}", activityId), ex);
                return null;
            }
        }
        /// <summary>
        /// 关联没有开放商的楼盘
        /// </summary>
        /// <param name="develid"></param>
        /// <param name="premisesids"></param>
        /// <returns></returns>
        public int UpdatePremisesDevelID(int develid, string premisesids)
        {
            if (string.IsNullOrWhiteSpace(premisesids) || develid <= 0)
                return 0;
            string sql =
                string.Format("UPDATE dbo.Premises SET DeveloperId={0} WHERE ISNULL(DeveloperId,0)=0 AND id IN({1}) ",
                              develid, premisesids);
            using (var db = new kyj_NewHouseDBEntities())
            {
                return db.ExecuteStoreCommand(sql);
            }
        }
    }
}