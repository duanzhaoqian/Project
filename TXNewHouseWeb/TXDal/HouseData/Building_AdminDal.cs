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
    public partial class BuildingDal : BaseDal_Admin
    {
        /// <summary>
        /// 根据搜索条件获取楼盘列表信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Building> GetPageList_BySearch_Admin(PVS_NH_Building search, int pageIndex, int pageSize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  c.Id, c.Name, c.NameType, c.PremisesId, c.BuildingType, c.Renovation, c.PropertyType, c.[State]
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.CreateTime DESC ) AS rowid, a.Id, a.Name, a.NameType, a.PremisesId, b.BuildingType,
                    a.Renovation, a.PropertyType, a.[State]
          FROM      Building (NOLOCK) AS a
                    INNER JOIN Premises (NOLOCK) AS b ON b.Id = a.PremisesId
                                                         AND b.IsDel = 0
          WHERE     a.IsDel = 0
                    AND a.PremisesId = {0}
        ) AS c
WHERE   c.rowid BETWEEN {1} AND {2}";
                    sql = string.Format(sql, GetParms_PageList_BySearch_Admin(search, pageIndex, pageSize));

                    var list = houseDb.ExecuteStoreQuery<PVM_NH_Building>(sql).ToList();

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
        /// sql条件
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private object[] GetParms_PageList_BySearch_Admin(PVS_NH_Building search, int pageIndex = 0, int pageSize = 0)
        {
            var parms = new List<object>
            {
                search.PremisesId
            };

            if (0 < pageIndex && 0 < pageSize)
            {
                int startIndex = (pageIndex - 1)*pageSize + 1;
                int endIndex = pageIndex*pageSize;

                parms.Add(startIndex);
                parms.Add(endIndex);

            }

            return parms.ToArray();
        }

        /// <summary>
        /// 根据搜索条件获取楼盘列表信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Admin(PVS_NH_Building search)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  COUNT(1) AS Result
FROM    Building (NOLOCK) AS a
        INNER JOIN Premises (NOLOCK) AS b ON b.Id = a.PremisesId
                                             AND b.IsDel = 0
WHERE   a.IsDel = 0
        AND a.PremisesId = {0}";
                    sql = string.Format(sql, GetParms_PageList_BySearch_Admin(search));

                    var list = houseDb.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();
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

        /// <summary>
        /// 添加新楼栋
        /// author: liyuzhao
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int AddNewBuilding(PVE_NH_Building building)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sqlparmsList = new List<object>();
                    var sql = new StringBuilder();
                    sql.Append(@"
BEGIN TRY
    BEGIN TRAN
    DECLARE @buildingid INT ,
        @developerid INT
    
    SELECT  @developerid = DeveloperId
    FROM    Premises (NOLOCK)
    WHERE   Id = @PremisesId
    
    INSERT  INTO Building ( DeveloperId, PremisesId, Periods, PropertyType, CreateTime, Name, NameType, FamilyNum, PictureData,
                            TheFloor, Underloor, Ladder, Renovation, BuildingPosition, OpeningTime, OthersTime, [State],
                            PresaleId, PermitPresale, PNum, UpdateTime, IsDel )
    VALUES  ( @developerid, @PremisesId, @Periods, @PropertyType, GETDATE(), @Name, @NameType, @FamilyNum, @PictureData, @TheFloor,
              @Underloor, @Ladder, @Renovation, @BuildingPosition, @OpeningTime, @OthersTime, @State, @PresaleId,
              @PermitPresale, @PNum, GETDATE(), 0 )
    IF @@ERROR > 0 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Building表插入错误' AS Msg
            RETURN
        END
    SET @buildingid = @@IDENTITY");

                    // 需要添加 @UnitName 字段 将 var sqlparms = new object[] 修改为list形式可以add
                    for (int i = 0; i < building.UnitCount; i++)
                    {
                        sql.AppendFormat(@"

    INSERT  INTO Unit ( DeveloperId, PremisesId, BuildingId, Num, Name, CreateTime, UpdateTime, IsDel )
    VALUES  ( @developerid, @PremisesId, @buildingid, @Num{0}, @Name{0}, GETDATE(), GETDATE(), 0 )

    IF @@ERROR > 0 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Unit表插入错误' AS Msg
            RETURN
        END", i);
                        sqlparmsList.Add(new SqlParameter("Num" + i, building.UnitNameList[i].Key));
                        sqlparmsList.Add(new SqlParameter("Name" + i, building.UnitNameList[i].Value));
                    }

                    sql.Append(@"
COMMIT TRAN
    
    SELECT  STR(@buildingid) AS Code, '操作成功' AS Msg
END TRY
BEGIN  CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作错误' AS Msg
END CATCH");

                    sqlparmsList.Add(new SqlParameter("PremisesId", building.PremisesId));
                    sqlparmsList.Add(new SqlParameter("Name", building.Name));
                    sqlparmsList.Add(new SqlParameter("NameType", building.NameType));
                    sqlparmsList.Add(new SqlParameter("Periods", building.Periods));
                    sqlparmsList.Add(new SqlParameter("PropertyType", building.PropertyType));
                    sqlparmsList.Add(new SqlParameter("FamilyNum", building.FamilyNum));
                    sqlparmsList.Add(new SqlParameter("PictureData", building.PictureData));
                    sqlparmsList.Add(new SqlParameter("TheFloor", building.TheFloor));
                    sqlparmsList.Add(new SqlParameter("Underloor", building.Underloor));
                    sqlparmsList.Add(new SqlParameter("Ladder", building.Ladder));
                    sqlparmsList.Add(new SqlParameter("Renovation", building.Renovation));
                    sqlparmsList.Add(new SqlParameter("BuildingPosition", building.BuildingPosition));
                    sqlparmsList.Add(new SqlParameter("OpeningTime", building.OpeningTime));
                    sqlparmsList.Add(new SqlParameter("OthersTime", building.OthersTime));
                    sqlparmsList.Add(new SqlParameter("State", building.State));
                    sqlparmsList.Add(new SqlParameter("PresaleId", building.PresaleId));
                    sqlparmsList.Add(new SqlParameter("PermitPresale", building.PermitPresale));
                    sqlparmsList.Add(new SqlParameter("PNum", building.PNum));

                    var result = db.ExecuteStoreQuery<ESqlResult>(sql.ToString(), sqlparmsList.ToArray()).ToList();
                    if (0 < result.Count)
                    {
                        return Convert.ToInt32(result[0].Code);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", building, ex);
                return 0;
            }
        }

        /// <summary>
        /// 更新楼栋信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int UpdateNewBuilding(PVE_NH_Building building)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sqlparmsList = new List<object>();
                    var sql = new StringBuilder();
                    sql.AppendFormat(@"
BEGIN TRY
    BEGIN TRAN
    DECLARE @buildingid INT ,
        @developerid INT ,
        @PremisesId INT
        
    SET @buildingid = {0}
    
    SELECT  @PremisesId = PremisesId, @developerid = DeveloperId
    FROM    Building (NOLOCK)
    WHERE   Id = @buildingid

	-- 更新Building
    UPDATE  Building
    SET     Name = @Name, NameType = @NameType, Periods = @Periods, PropertyType = @PropertyType, OpeningTime = @OpeningTime,
            OthersTime = @OthersTime, FamilyNum = @FamilyNum, TheFloor = @TheFloor, Underloor = @Underloor, Ladder = @Ladder,
            Renovation = @Renovation, BuildingPosition = @BuildingPosition, [State] = @State, PresaleId = @PresaleId,
            PermitPresale = @PermitPresale, PNum = @PNum
    WHERE   Id = @buildingid
    
    IF ( @@ERROR > 0 ) 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Building表更新错误' AS Msg
            RETURN
        END
        
	-- 删除旧Unit
    DELETE  FROM Unit
    WHERE   BuildingId = @buildingid
    
    IF ( @@ERROR > 0 ) 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Unit表删除错误' AS Msg
            RETURN
        END", building.Id);

                    // 添加单元名称列表
                    for (int i = 0; i < building.UnitCount; i++)
                    {
                        sql.AppendFormat(@"

    INSERT  INTO Unit ( DeveloperId, PremisesId, BuildingId, Num, Name, CreateTime, UpdateTime, IsDel )
    VALUES  ( @developerid, @PremisesId, @buildingid, @Num{0}, @Name{0}, GETDATE(), GETDATE(), 0 )

    IF @@ERROR > 0 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Unit表插入错误' AS Msg
            RETURN
        END", i);
                        sqlparmsList.Add(new SqlParameter("Num" + i, building.UnitNameList[i].Key));
                        sqlparmsList.Add(new SqlParameter("Name" + i, building.UnitNameList[i].Value));
                    }

                    sql.Append(@"
COMMIT TRAN
    
    SELECT  STR(@buildingid) AS Code, '操作成功' AS Msg
END TRY
BEGIN  CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作错误' AS Msg
END CATCH");
                    sqlparmsList.Add(new SqlParameter("Name", building.Name));
                    sqlparmsList.Add(new SqlParameter("NameType", building.NameType));
                    sqlparmsList.Add(new SqlParameter("Periods", building.Periods));
                    sqlparmsList.Add(new SqlParameter("PropertyType", building.PropertyType));
                    sqlparmsList.Add(new SqlParameter("FamilyNum", building.FamilyNum));
                    sqlparmsList.Add(new SqlParameter("TheFloor", building.TheFloor));
                    sqlparmsList.Add(new SqlParameter("Underloor", building.Underloor));
                    sqlparmsList.Add(new SqlParameter("Ladder", building.Ladder));
                    sqlparmsList.Add(new SqlParameter("Renovation", building.Renovation));
                    sqlparmsList.Add(new SqlParameter("BuildingPosition", building.BuildingPosition));
                    sqlparmsList.Add(new SqlParameter("OpeningTime", building.OpeningTime));
                    sqlparmsList.Add(new SqlParameter("OthersTime", building.OthersTime));
                    sqlparmsList.Add(new SqlParameter("State", building.State));
                    sqlparmsList.Add(new SqlParameter("PresaleId", building.PresaleId));
                    sqlparmsList.Add(new SqlParameter("PermitPresale", building.PermitPresale));
                    sqlparmsList.Add(new SqlParameter("PNum", building.PNum));

                    var result = db.ExecuteStoreQuery<ESqlResult>(sql.ToString(), sqlparmsList.ToArray()).ToList();
                    if (0 < result.Count)
                    {
                        return Convert.ToInt32(result[0].Code);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", building, ex);
                return 0;
            }
        }

        /// <summary>
        /// 更新楼栋信息
        /// author: liyuzhao
        /// </summary>
        /// <param name="building"></param>
        /// <param name="updateHouseState">更新房源状态目标值</param>
        /// <returns></returns>
        public int UpdateNewBuildingAndUpdateHouseState(PVE_NH_Building building, int updateHouseState)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sqlparmsList = new List<object>();
                    var sql = new StringBuilder();
                    sql.AppendFormat(@"
BEGIN TRY
    BEGIN TRAN
    DECLARE @buildingid INT ,
        @developerid INT ,
        @PremisesId INT ,
        @updateHouseState INT
        
    SET @buildingid = {0}
    SET @updateHouseState = {1}
    
    SELECT  @PremisesId = PremisesId, @developerid = DeveloperId
    FROM    Building (NOLOCK)
    WHERE   Id = @buildingid

	-- 更新Building
    UPDATE  Building
    SET     Name = @Name, NameType = @NameType, Periods = @Periods, PropertyType = @PropertyType, OpeningTime = @OpeningTime,
            OthersTime = @OthersTime, FamilyNum = @FamilyNum, TheFloor = @TheFloor, Underloor = @Underloor, Ladder = @Ladder,
            Renovation = @Renovation, BuildingPosition = @BuildingPosition, [State] = @State, PresaleId = @PresaleId,
            PermitPresale = @PermitPresale, PNum = @PNum
    WHERE   Id = @buildingid
    
    IF ( @@ERROR > 0 ) 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Building表更新错误' AS Msg
            RETURN
        END
        
	-- 删除旧Unit
    DELETE  FROM Unit
    WHERE   BuildingId = @buildingid
    
    IF ( @@ERROR > 0 ) 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Unit表删除错误' AS Msg
            RETURN
        END

    -- 更新房源状态
    IF ( @updateHouseState = 9 OR @updateHouseState = 0 OR @updateHouseState = 2 ) 
        BEGIN
            UPDATE  House
            SET     SalesStatus = @updateHouseState
            WHERE   IsDel = 0
                    AND BuildingId = @buildingid
                    AND SalesStatus NOT IN ( 1, 3, 4, 5, 6, 7, 8 )
        END
    ELSE 
        BEGIN
            UPDATE  House
            SET     SalesStatus = @updateHouseState
            WHERE   IsDel = 0
                    AND BuildingId = @buildingid 
        END
      
    IF ( @@ERROR > 0 ) 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'House表状态更新错误' AS Msg
            RETURN
        END", building.Id, updateHouseState);

                    // 添加单元名称列表
                    for (int i = 0; i < building.UnitCount; i++)
                    {
                        sql.AppendFormat(@"

    INSERT  INTO Unit ( DeveloperId, PremisesId, BuildingId, Num, Name, CreateTime, UpdateTime, IsDel )
    VALUES  ( @developerid, @PremisesId, @buildingid, @Num{0}, @Name{0}, GETDATE(), GETDATE(), 0 )

    IF @@ERROR > 0 
        BEGIN
            ROLLBACK TRAN
            SELECT  '0' AS Code, 'Unit表插入错误' AS Msg
            RETURN
        END", i);
                        sqlparmsList.Add(new SqlParameter("Num" + i, building.UnitNameList[i].Key));
                        sqlparmsList.Add(new SqlParameter("Name" + i, building.UnitNameList[i].Value));
                    }

                    sql.Append(@"
COMMIT TRAN
    
    SELECT  STR(@buildingid) AS Code, '操作成功' AS Msg
END TRY
BEGIN  CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作错误' AS Msg
END CATCH");
                    sqlparmsList.Add(new SqlParameter("Name", building.Name));
                    sqlparmsList.Add(new SqlParameter("NameType", building.NameType));
                    sqlparmsList.Add(new SqlParameter("Periods", building.Periods));
                    sqlparmsList.Add(new SqlParameter("PropertyType", building.PropertyType));
                    sqlparmsList.Add(new SqlParameter("FamilyNum", building.FamilyNum));
                    sqlparmsList.Add(new SqlParameter("TheFloor", building.TheFloor));
                    sqlparmsList.Add(new SqlParameter("Underloor", building.Underloor));
                    sqlparmsList.Add(new SqlParameter("Ladder", building.Ladder));
                    sqlparmsList.Add(new SqlParameter("Renovation", building.Renovation));
                    sqlparmsList.Add(new SqlParameter("BuildingPosition", building.BuildingPosition));
                    sqlparmsList.Add(new SqlParameter("OpeningTime", building.OpeningTime));
                    sqlparmsList.Add(new SqlParameter("OthersTime", building.OthersTime));
                    sqlparmsList.Add(new SqlParameter("State", building.State));
                    sqlparmsList.Add(new SqlParameter("PresaleId", building.PresaleId));
                    sqlparmsList.Add(new SqlParameter("PermitPresale", building.PermitPresale));
                    sqlparmsList.Add(new SqlParameter("PNum", building.PNum));

                    var result = db.ExecuteStoreQuery<ESqlResult>(sql.ToString(), sqlparmsList.ToArray()).ToList();
                    if (0 < result.Count)
                    {
                        return Convert.ToInt32(result[0].Code);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", building, ex);
                return 0;
            }
        }

        /// <summary>
        /// 删除楼栋
        /// author: liyuzhao
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public int DelNewBuilding(int buildingId)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
BEGIN TRY
    BEGIN TRAN
    UPDATE  Building
    SET     IsDel = 1
    WHERE   Id = @Id

    UPDATE  House
    SET     IsDel = 1
    WHERE   BuildingId = @Id
    COMMIT
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH";
                    var sqlparms = new object[]
                    {
                        new SqlParameter("Id", buildingId)
                    };

                    var count = houseDb.ExecuteStoreCommand(sql, sqlparms);

                    return count;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("buidlingId: {0}", buildingId), ex);
                return 0;
            }
        }

        /// <summary>
        /// 根据楼盘编号获取楼栋编号
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<Building> GetBuildingListByPremisesId(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var query = db.Buildings.Where(it => it.IsDel == false);

                    if (0 < premisesId)
                    {
                        query = query.Where(it => it.PremisesId == premisesId);
                    }

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("PremisesId: {0}", premisesId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据楼栋编号获取单元列表
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<Unit> GetUnitsByBuildingId(int buildingId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  *
FROM    Unit (NOLOCK) AS a
WHERE   a.IsDel = 0
        AND BuildingId = {0}";
                    sql = string.Format(sql, buildingId);

                    var list = db.ExecuteStoreQuery<Unit>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("buildingId: {0}", buildingId), ex);
                return null;
            }
        }

        /// <summary>
        /// 根基编号和名称查询在同一楼盘下同名楼栋数量
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int GetBuildingNameCountByIdAndName(PVE_NH_Building building)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  COUNT(1) AS Result
FROM    Building (NOLOCK)
WHERE   PremisesId = {0}
        AND Name = '{1}'
        AND IsDel = 0";
                    sql = string.Format(sql, building.PremisesId, building.Name);

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
                Log4netService.RecordLog.RecordException("李雨钊", building, ex);
                return 1;
            }
        }

        /// <summary>
        /// 根基编号和名称查询在同一楼盘下同名楼栋数量(更新时使用)
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public int GetBuildingNameCountByIdAndName_Update(PVE_NH_Building building)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT  COUNT(1) AS Result
FROM    Building (NOLOCK)
WHERE   PremisesId = {0}
        AND Name = '{1}'
        AND Id <> {2}
        AND IsDel = 0";
                    sql = string.Format(sql, building.PremisesId, building.Name, building.Id);

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
                Log4netService.RecordLog.RecordException("李雨钊", building, ex);
                return 1;
            }
        }
    }
}