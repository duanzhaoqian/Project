
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXDal.NHouseActivies.Yaohao
{
    /// <summary>
    /// 摇号 (网站管理平台)
    /// </summary>
    public partial class YaohaoDal : BaseDal_Admin
    {
        #region 分页获取摇号活动列表
        /// <summary>
        /// 分页获取摇号活动列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">尺寸</param>
        /// <returns></returns>
        public List<PVM_NH_YaoHao> GetPageList_BySearch_Admin(PVS_NH_YaoHao search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
//                    string sql = string.Format(@"  SELECT * FROM (
//                SELECT  ROW_NUMBER() OVER ( ORDER BY activ.CreateTime DESC ) AS RowID,
//                activ.Id,
//				activ.DeveloperId,
//				activ.Name,
//				activ.Type,
//				activ.UserCount,
//				activ.HouseCount,
//				activ.BidPrice,
//				activ.AddPrice,
//				activ.MaxPrice,
//				activ.BeginTime,
//				activ.EndTime,
//				activ.Bond,
//				activ.ActivitieState,
//				activ.UpdateTime,
//				activ.CreateTime,
//				activ.IsDel,
//                i.Periods,
//                i.BuildingIds,
//                i.ActivitieLocation,
//                i.NotarialOffice,
//                activ.SignupBeginTime,
//                activ.SignupEndTime,
//                i.ChooseHouseTime,
//                i.Introduction,
//                i.Notice,
//                i.Process,
//				ah.PremisesId,
//				ah.BuildingId,
//				ah.UnitId,
//				ah.HouseId,
//				ah.Discount,
//				p.Name PremisesName,
//				p.ProvinceID,
//				p.ProvinceName,
//				p.CityId,
//				p.CityName,
//				d.UserName,
//				d.CompanyName,
//				d.UserMobile,
//                d.Type AS CompanyType
//                FROM dbo.Activities activ
//                JOIN dbo.ActivitiesYaoHaoInfos i ON activ.Id=i.ActivitiesId
//                JOIN dbo.ActivitiesHouse ah ON activ.Id = ah.ActivitiesId
//                JOIN dbo.Premises p ON ah.PremisesId=p.Id
//                JOIN dbo.Developer_Identity d ON activ.DeveloperId=d.DeveloperId
//                WHERE  activ.IsDel=0 AND ah.IsDel=0 AND p.IsDel=0 AND activ.Type=1 {0}
//                ) HOSE 
//                WHERE   HOSE.RowID BETWEEN {1} AND {2}",
//                                                new object[]{
//                                                GetWhere_BySearch_Admin(search),
//                                                ((pageIndex - 1)*pageSize) + 1,
//                                                pageIndex*pageSize
//                                                });

                    string sql = string.Format(@"  SELECT * FROM (
                SELECT  ROW_NUMBER() OVER ( ORDER BY activ.CreateTime DESC ) AS RowID,
                activ.Id,
				activ.DeveloperId,
				activ.Name,
				activ.Type,
				activ.UserCount,
				activ.HouseCount,
				activ.BidPrice,
				activ.AddPrice,
				activ.MaxPrice,
				activ.BeginTime,
				activ.EndTime,
				activ.Bond,
				activ.ActivitieState,
				activ.UpdateTime,
				activ.CreateTime,
				activ.IsDel,
                i.Periods,
                i.BuildingIds,
                i.ActivitieLocation,
                i.NotarialOffice,
                activ.SignupBeginTime,
                activ.SignupEndTime,
                i.ChooseHouseTime,
                i.Introduction,
                i.Notice,
                i.Process,
				a.PremisesId,
				p.Name PremisesName,
				p.ProvinceID,
				p.ProvinceName,
				p.CityId,
				p.CityName,
				d.UserName,
				d.CompanyName,
				d.UserMobile,
                d.Type AS CompanyType
                FROM dbo.Activities activ
                JOIN dbo.ActivitiesYaoHaoInfos i ON activ.Id=i.ActivitiesId
                JOIN ActivitiesYaoHaoApply as a on activ.Id=a.ActivitiesId
                JOIN dbo.Premises p ON a.PremisesId=p.Id
                JOIN dbo.Developer_Identity d ON activ.DeveloperId=d.DeveloperId
                WHERE  activ.IsDel=0 AND p.IsDel=0 AND activ.Type=1  AND activ.ActivitieState!=4 {0}
                ) HOSE 
                WHERE   HOSE.RowID BETWEEN {1} AND {2}",
                                               new object[]{
                                                GetWhere_BySearch_Admin(search),
                                                ((pageIndex - 1)*pageSize) + 1,
                                                pageIndex*pageSize
                                                });

                    var list = kyj.ExecuteStoreQuery<PVM_NH_YaoHao>(sql).ToList();
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

        #region 分页获取摇号活动列表记录数
        /// <summary>
        /// 分页获取摇号活动列表记录数
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_YaoHao search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
//                    string sql = string.Format(@"  
//                SELECT  COUNT(1) AS Result
//                FROM dbo.Activities activ
//                JOIN dbo.ActivitiesYaoHaoInfos i ON activ.Id=i.ActivitiesId
//                JOIN dbo.ActivitiesHouse ah ON activ.Id = ah.ActivitiesId
//                JOIN dbo.Premises p ON ah.PremisesId=p.Id
//                JOIN dbo.Developer_Identity d ON activ.DeveloperId=d.DeveloperId
//                WHERE  activ.IsDel=0 AND ah.IsDel=0 AND p.IsDel=0 AND activ.Type=1 {0}",
//                                                new object[]{
//                                                GetWhere_BySearch_Admin(search)
//                                                });

                    string sql = string.Format(@"  
                SELECT  COUNT(1) AS Result
                FROM dbo.Activities activ
                JOIN dbo.ActivitiesYaoHaoInfos i ON activ.Id=i.ActivitiesId
                left JOIN ActivitiesYaoHaoApply as a on activ.Id=a.ActivitiesId
                left JOIN dbo.Premises p ON a.PremisesId=p.Id
                left JOIN dbo.Developer_Identity d ON activ.DeveloperId=d.DeveloperId
                WHERE  activ.IsDel=0 AND p.IsDel=0 AND activ.Type=1  AND activ.ActivitieState!=4 {0}",
                                                new object[]{
                                                GetWhere_BySearch_Admin(search)
                                                });

                    var list = kyj.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();
                    if (list.Count>0)
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
        public string GetWhere_BySearch_Admin(PVS_NH_YaoHao search)
        {
            StringBuilder s = new StringBuilder();
            if (search.ProvinceID > 0)
            {
                s.AppendFormat(" AND p.ProvinceID={0}",search.ProvinceID);
            }
            if (search.CityId > 0)
            {
                s.AppendFormat(" AND p.CityId={0}", search.CityId);
            }
           
            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                s.AppendFormat(" AND activ.Name LIKE '{0}%'", search.Name);
            }
            //时间
            //活动状态
            s.Append(" AND activ.ActivitieState!=4");
            if (search.ActivitieState > -1)
            {
                s.AppendFormat(" AND activ.ActivitieState={0}", search.ActivitieState);
            }
            return s.ToString();
        }
        #endregion

        #region 更新摇号活动标记状态
        /// <summary>
        /// 更新摇号活动标记状态
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">状态0 待处理 1 已通过 2 未通过 3未联系 4 已联系 5 已通过并开启报名入口6 已结束</param>
        /// <returns></returns>
        public ESqlResult UpdateYaoHaoState_Admin(int id, int state)
        {
            try
            {
                using (var kyj=new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"DECLARE @t_id INT  
                                               SELECT  @t_id = Id
                                               FROM    ActivitiesYaoHaoApply (NOLOCK)
                                               WHERE   Id = {0} AND IsDel=0
                                               IF ( @t_id IS NULL ) 
                                               BEGIN
                                                    SELECT  '0' AS Code, '记录不存在' AS Msg
                                                    RETURN
                                               END
                                               UPDATE  ActivitiesYaoHaoApply
                                               SET     State = {1},
                                               UpdateTime=GETDATE()
                                               WHERE   Id = @t_id
                                               IF ( @@ERROR > 0 ) 
                                                  BEGIN
                                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                                    RETURN
                                                  END
                                               SELECT  '1' AS Code, '操作成功' AS Msg", id,state);
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("活动编号{0}-状态{1}", id, state), e);
                throw;
            }
        }

        #endregion

        #region 分页获取摇号活动列表 活动审批列表
        /// <summary>
        /// 分页获取摇号活动列表 活动审批列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">尺寸</param>
        /// <returns></returns>
        public List<PVM_NH_YaoHaoApply> GetYaoHaoApplyPageList_BySearch_Admin(PVS_NH_YaoHao search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"  
SELECT * FROM (
                SELECT  ROW_NUMBER() OVER ( ORDER BY app.CreateTime DESC ) AS RowID,
                app.Id,
                app.IsAllPremises,
                app.BuildingIds,
                app.STATE,
                app.UpdateTime,
                app.CreateTime,
                app.IsDel,
                p.Name PremisesName,
				p.ProvinceID,
				p.ProvinceName,
				p.CityId,
				p.CityName,
				d.UserName,
				d.CompanyName,
				d.UserMobile,
                d.Type AS CompanyType
                FROM 
                ActivitiesYaoHaoApply app
                JOIN dbo.Premises p ON app.PremisesId=p.Id
                JOIN dbo.Developer_Identity d ON app.DeveloperId=d.DeveloperId
                WHERE app.IsDel=0 AND d.IsDel=0 AND p.IsDel=0 {0}
                ) YaoHaoApply 
                WHERE   YaoHaoApply.RowID BETWEEN {1} AND {2}",
                                                new object[]{
                                                GetYaoHaoApplyWhere_BySearch_Admin(search),
                                                ((pageIndex - 1)*pageSize) + 1,
                                                pageIndex*pageSize
                                                });

                    var list = kyj.ExecuteStoreQuery<PVM_NH_YaoHaoApply>(sql).ToList();
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

        #region 分页获取摇号活动列表 活动审批列表
        /// <summary>
        /// 分页获取摇号活动列表 活动审批列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <returns></returns>
        public int GetYaoHaoApplyPageCount_BySearch_Admin(PVS_NH_YaoHao search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                SELECT  COUNT(1) AS Result
                FROM 
                ActivitiesYaoHaoApply app
                JOIN dbo.Premises p ON app.PremisesId=p.Id
                JOIN dbo.Developer_Identity d ON app.DeveloperId=d.DeveloperId
                WHERE app.IsDel=0 AND d.IsDel=0 AND p.IsDel=0 {0}  ",
                                                new object[]{
                                                GetYaoHaoApplyWhere_BySearch_Admin(search)});

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
        public string GetYaoHaoApplyWhere_BySearch_Admin(PVS_NH_YaoHao search)
        {
            StringBuilder s = new StringBuilder();
            if (search.ProvinceID > 0)
            {
                s.AppendFormat(" AND p.ProvinceID={0}", search.ProvinceID);
            }
            if (search.CityId > 0)
            {
                s.AppendFormat(" AND p.CityId={0}", search.CityId);
            }
            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                s.AppendFormat(" AND d.CompanyName LIKE '{0}%'", search.Name);
            }
            //时间
            if (!string.IsNullOrWhiteSpace(search.BeginTime))
            {
                s.AppendFormat(" AND app.CreateTime>='{0}'", Convert.ToDateTime(search.BeginTime));
            }
            if (!string.IsNullOrWhiteSpace(search.EndTime))
            {
                s.AppendFormat(" AND app.CreateTime<='{0}'", Convert.ToDateTime(search.EndTime));
            }
            //标记状态
            if (search.State > -1)
            {
                s.AppendFormat(" AND app.State={0}", search.State);
            }
            else
            {
                //s.AppendFormat(" AND app.State!={0}", 5);
            }
            //公司类型
            if (search.CompanyType > 0)
            {
                s.AppendFormat(" AND d.Type={0}", search.CompanyType);
            }
            
            return s.ToString();
        }
        #endregion

        #region 根据编号获取摇号申请信息实体
        /// <summary>
        /// 根据编号获取摇号申请信息实体
        /// </summary>
        /// <param name="id">摇号编号</param>
        /// <returns>返回：摇号实体</returns>
        public ActivitiesYaoHaoApply GetActivitiesYaoHaoApply_ById_Admin(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.ActivitiesYaoHaoApplies.FirstOrDefault(it => it.Id == id && it.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("Id:{0}", new object[] { id }), ex);
                return null;
            }
        }
        #endregion

        #region 添加摇号活动 v0.1
        /// <summary>
        /// 添加摇号活动 v0.1
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddYaoHaoActivities_Admin_v01(PVM_NH_YaoHao model)
        {
            try
            {
                using (var kyj=new kyj_NewHouseDBEntities())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"
DECLARE @houseCount INT,@t_activities INT,@t_Periods INT,@t_BuildingIds NVARCHAR
SELECT @t_BuildingIds=@BuildingIds
--获取参与房源数
SELECT @houseCount=COUNT(1) FROM dbo.House WHERE BuildingId IN (@t_BuildingIds)
--获取期数
SELECT @t_Periods=(CASE WHEN MAX(i.Periods) IS NULL THEN 1 ELSE MAX(i.Periods)+1 END)  FROM dbo.Activities a
JOIN ActivitiesYaoHaoInfos i ON a.Id=i.ActivitiesId
WHERE a.IsDel=0 AND Type=1
--插入Activities数据
BEGIN TRY
BEGIN TRAN
INSERT INTO [dbo].[Activities]
           ([DeveloperId]
           ,[Name]
           ,[Type]
           ,[UserCount]
           ,[HouseCount]
           ,[BidPrice]
           ,[AddPrice]
           ,[MaxPrice]
           ,[BeginTime]
           ,[EndTime]
           ,[Bond]
           ,[ActivitieState]
           ,[UpdateTime]
           ,[CreateTime]
           ,[IsDel])
     VALUES
           (@DeveloperId,
            @Name,
            1,
            0,
            @houseCount,
            @BidPrice,
            @AddPrice,
            @MaxPrice,
            @BeginTime,
            @EndTime,
            @Bond,
            0,
            GETDATE(),
            GETDATE(),
            0)
SET @t_activities=@@IDENTITY
--更新ActivitiesYaoHaoApply 数据
UPDATE ActivitiesYaoHaoApply SET ActivitiesId=@t_activities,State=5 WHERE Id=@ActivitiesYaoHaoApplyId
--插入ActivitiesYaoHaoInfos数据
INSERT INTO [dbo].[ActivitiesYaoHaoInfos]
           ([ActivitiesId]
           ,[Periods]
           ,[BuildingIds]
           ,[ActivitieLocation]
           ,[NotarialOffice]
           ,[SignupBeginTime]
           ,[SignupEndTime]
           ,[ChooseHouseTime]
           ,[Introduction]
           ,[Notice]
           ,[Process])
     VALUES
           (@t_activities,
            @t_Periods,
            @BuildingIds,
            @ActivitieLocation,
            @NotarialOffice,
            @SignupBeginTime,
            @SignupEndTime,
            @ChooseHouseTime,
            @Introduction,
            @Notice,
            @Process)


COMMIT
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Code, '操作失败' AS Msg
END CATCH
IF ( @@ERROR > 0 ) 
    BEGIN
        SELECT  '0' AS Code, '操作失败' AS Msg
        RETURN
    END
SELECT  STR(@t_activities) AS Code, '操作成功' AS Msg
           ");
                    object[] parameters =
                    {
                        new SqlParameter("@DeveloperId",model.DeveloperId),
                        new SqlParameter("@Name",model.Name),
                        new SqlParameter("@BidPrice",model.BidPrice),
                        new SqlParameter("@AddPrice",model.AddPrice),
                        new SqlParameter("@MaxPrice",model.MaxPrice),
                        new SqlParameter("@BeginTime",model.BeginTime),
                        new SqlParameter("@EndTime",model.EndTime),
                        new SqlParameter("@Bond",model.Bond),
                        new SqlParameter("@ActivitiesYaoHaoApplyId",model.ActivitiesYaoHaoApplyId),

                        new SqlParameter("@BuildingIds",model.BuildingIds),
                        new SqlParameter("@ActivitieLocation",model.ActivitieLocation),
                        new SqlParameter("@NotarialOffice",model.NotarialOffice),
                        new SqlParameter("@SignupBeginTime",model.SignupBeginTime),
                        new SqlParameter("@SignupEndTime",model.SignupEndTime),
                        new SqlParameter("@ChooseHouseTime",model.ChooseHouseTime),
                        new SqlParameter("@Introduction",model.Introduction),
                        new SqlParameter("@Notice",model.Notice),
                        new SqlParameter("@Process",model.Process)
                    };
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql.ToString(), parameters).ToList();
                    if (0 < result.Count)
                    {
                        return Convert.ToInt32(result[0].Code);
                    }
                    return 0;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", model, e);
                return 0;
            }
        }

        #endregion 

        #region 添加摇号活动 v0.2
        /// <summary>
        /// 添加摇号活动 v0.2
        /// 修改  SignupBeginTime、SignupEndTime 2013年12月30日14:36:20
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ESqlResult AddYaoHaoActivities_Admin(PVM_NH_YaoHao model)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"
DECLARE @houseCount INT,@t_activities INT,@t_Periods INT,@t_BuildingIds NVARCHAR
--SELECT @t_BuildingIds=@BuildingIds
------------------验证申请是否生成活动记录----------
--SET @t_activities=@Id
SELECT @t_activities=activitiesid,@t_BuildingIds=BuildingIds FROM ActivitiesYaoHaoApply WHERE id=@ActivitiesYaoHaoApplyId AND IsDel=0
IF(@t_activities IS NULL)
    BEGIN
            SELECT  '0' AS Code, '没有对应的活动申请记录(Activities)，添加失败。' AS Msg
             RETURN
    END
------------------验证申请楼栋中是否有参与其他活动的房源-------
SELECT @houseCount=COUNT(1) FROM ActivitiesHouse ah
JOIN Activities a  ON a.Id=ah.ActivitiesId
JOIN House h ON h.Id=ah.HouseId
WHERE ah.IsDel=0 AND a.IsDel=0 AND h.IsDel=0 AND ah.BuildingId IN (@t_BuildingIds)
IF(@houseCount >0)
    BEGIN
            SELECT  '0' AS Code, '楼栋中存在参与其它活动的房源，添加失败。' AS Msg
             RETURN
    END

----获取参与房源数----
SELECT @houseCount=COUNT(1) FROM dbo.House WHERE BuildingId IN (@t_BuildingIds)
----获取期数-----
SELECT @t_Periods=(CASE WHEN MAX(i.Periods) IS NULL THEN 1 ELSE MAX(i.Periods)+1 END)  FROM dbo.Activities a
JOIN ActivitiesYaoHaoInfos i ON a.Id=i.ActivitiesId
WHERE a.IsDel=0 AND Type=1
----更新Activities数据----
BEGIN TRY
BEGIN TRAN
UPDATE [dbo].[Activities]
   SET [DeveloperId] = @DeveloperId
      ,[Name] = @Name
      ,[Type] = 1
      ,[UserCount] = 0
      ,[HouseCount] = @houseCount
      ,[BidPrice] = @BidPrice
      ,[AddPrice] = @AddPrice
      ,[MaxPrice] = @MaxPrice
      ,[BeginTime] =  @BeginTime
      ,[EndTime] = @EndTime
      ,[Bond] = @Bond
      ,[ActivitieState] = 0
      ,[UpdateTime] = GETDATE()
      ,[SignupBeginTime]=@SignupBeginTime
      ,[SignupEndTime]=@SignupEndTime
      ,[IsDel] = 0
 WHERE Id=@t_activities
-----更新ActivitiesYaoHaoApply 数据------
UPDATE ActivitiesYaoHaoApply SET State=5 WHERE Id=@ActivitiesYaoHaoApplyId
-----插入ActivitiesYaoHaoInfos数据-------
INSERT INTO [dbo].[ActivitiesYaoHaoInfos]
           ([ActivitiesId]
           ,[Periods]
           ,[BuildingIds]
           ,[ActivitieLocation]
           ,[NotarialOffice]
           ,[ChooseHouseTime]
           ,[Introduction]
           ,[Notice]
           ,[Process])
     VALUES
           (@t_activities,
            @t_Periods,
            @BuildingIds,
            @ActivitieLocation,
            @NotarialOffice,
            @ChooseHouseTime,
            @Introduction,
            @Notice,
            @Process)
--------------------------------
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
SELECT  STR(@t_activities) AS Code, '操作成功' AS Msg
           ");
                    object[] parameters =
                    {
                        new SqlParameter("@Id",model.Id),
                        new SqlParameter("@DeveloperId",model.DeveloperId),
                        new SqlParameter("@Name",model.Name),
                        new SqlParameter("@SignupBeginTime",model.SignupBeginTime),
                        new SqlParameter("@SignupEndTime",model.SignupEndTime),
                        new SqlParameter("@BidPrice",model.BidPrice),
                        new SqlParameter("@AddPrice",model.AddPrice),
                        new SqlParameter("@MaxPrice",model.MaxPrice),
                        new SqlParameter("@BeginTime",model.BeginTime),
                        new SqlParameter("@EndTime",model.EndTime),
                        new SqlParameter("@Bond",model.Bond),
                        new SqlParameter("@ActivitiesYaoHaoApplyId",model.ActivitiesYaoHaoApplyId),

                        new SqlParameter("@BuildingIds",model.BuildingIds),
                        new SqlParameter("@ActivitieLocation",model.ActivitieLocation),
                        new SqlParameter("@NotarialOffice",model.NotarialOffice),
                        
                        new SqlParameter("@ChooseHouseTime",model.ChooseHouseTime),
                        new SqlParameter("@Introduction",model.Introduction),
                        new SqlParameter("@Notice",model.Notice),
                        new SqlParameter("@Process",model.Process)
                    };
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql.ToString(), parameters).ToList().FirstOrDefault();

                    return result;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", model, e);
                return new ESqlResult{Code="0",Msg="dal异常"};
            }
        }

        #endregion

        #region 获取PVE_NH_YaoHao
        /// <summary>
        /// 获取PVE_NH_YaoHao
        /// </summary>
        /// <returns></returns>
        public PVE_NH_YaoHao GetYaoHaoInfo(int id)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var query = from activ in kyj.Activities
                        join info in kyj.ActivitiesYaoHaoInfos on activ.Id equals info.ActivitiesId
                        where activ.IsDel==false &&　activ.Id==id
                        select new PVE_NH_YaoHao
                        {
                            Id=activ.Id,
                            Name=activ.Name,
                            BeginTime=activ.BeginTime,
                            EndTime=activ.EndTime,
                            ApplyBeginTime = activ.SignupBeginTime,
                            ApplyEndTime = activ.SignupEndTime,
                            Bond=activ.Bond,
                            NotarialOffice=info.NotarialOffice,
                            LectHouseTime=info.ChooseHouseTime,
                            ActivitiesIntroduction=info.Introduction,
                            ActivitiesNotice=info.Notice,
                            ActivitiesProcess=info.Process
                        };
                    return query.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("Id:{0}", new object[] { id }), e);
                return null;
            }
        }
        #endregion

        #region 修改摇号活动 基本信息

        /// <summary>
        /// 修改摇号活动 基本信息
        /// 修改 2013年12月30日14:36:20 SignupBeginTime、SignupEndTime
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ESqlResult UpdateYaoHaoInfo_Admin(PVE_NH_YaoHao model)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                               DECLARE @t_id INT  
                                               SELECT  @t_id = a.Id
                                               FROM    Activities (NOLOCK) a
                                               JOIN  ActivitiesYaoHaoInfos b ON a.Id=b.ActivitiesId
                                               WHERE   Id = @Id AND a.IsDel=0
                                               IF ( @t_id IS NULL ) 
                                               BEGIN
                                                    SELECT  '0' AS Code, '记录不存在' AS Msg
                                                    RETURN
                                               END
                                                BEGIN TRY
                                                BEGIN TRAN
                                                ---------------更新主表-------
                                                UPDATE dbo.Activities SET
                                                Name=@Name,
                                                Bond=@Bond,
                                                BeginTime=@BeginTime,
                                                EndTime=@EndTime,
                                                SignupBeginTime=@SignupBeginTime,
                                                SignupEndTime=@SignupEndTime,
                                                UpdateTime=GETDATE()
                                                WHERE Id=@t_id
                                                ---------------更新扩展表-------
                                                UPDATE dbo.ActivitiesYaoHaoInfos
                                                SET NotarialOffice=@NotarialOffice,                                                
                                                ChooseHouseTime=@ChooseHouseTime,
                                                Introduction=@Introduction,
                                                Notice=@Notice,
                                                Process=@Process
                                                WHERE ActivitiesId=@t_id
                                                ----------------end-------------
                                                COMMIT
                                                END TRY
                                                BEGIN CATCH
                                                    ROLLBACK TRAN
                                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                                END CATCH
                                               IF ( @@ERROR > 0 ) 
                                                  BEGIN
                                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                                    RETURN
                                                  END
                                               SELECT  '1' AS Code, '操作成功' AS Msg");
                    object[] parameters =
                    {
                        new SqlParameter("@Id",model.Id),
                        new SqlParameter("@Name",model.Name),
                        new SqlParameter("@Bond",model.Bond),
                        new SqlParameter("@BeginTime",model.BeginTime),
                        new SqlParameter("@EndTime",model.EndTime),
                        new SqlParameter("@NotarialOffice",model.NotarialOffice),
                        new SqlParameter("@SignupBeginTime",model.ApplyBeginTime),
                        new SqlParameter("@SignupEndTime",model.ApplyEndTime),
                        new SqlParameter("@ChooseHouseTime",model.LectHouseTime),
                        new SqlParameter("@Introduction",model.ActivitiesIntroduction),
                        new SqlParameter("@Notice",model.ActivitiesNotice),
                        new SqlParameter("@Process",model.ActivitiesProcess)
                    };
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql, parameters).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", model, e);
                throw;
            }
        }

        #endregion


        #region 分页获取摇号活动用户列表 
        /// <summary>
        /// 分页获取摇号活动用户列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">尺寸</param>
        /// <returns></returns>
        public List<PVM_NH_YaoHaoUsers> GetYaoHaoApplyUsersPageList_BySearch_Admin(PVS_NH_YaoHaoUsers search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"  
SELECT * FROM (
                SELECT  ROW_NUMBER() OVER ( ORDER BY au.CreateTime DESC ) AS RowID,
                Id,ActivitiesId,BidUserId as UserId,BidUserName as Name,BidUserPrice as Bond,InnerCode,
                BidUserMobile as Mobile,IDCard,BidUserNumber as Num,CreateTime,UpdateTime,IsDel
                FROM 
                BidPrice au
                WHERE au.IsDel=0 AND au.ActivitiesId={2}
                ) YaoHaoApply 
                WHERE   YaoHaoApply.RowID BETWEEN {0} AND {1}",
                                                new object[]{
                                                ((pageIndex - 1)*pageSize) + 1,
                                                pageIndex*pageSize,
                                                search.ActivitiesId
                                                });

                    var list = kyj.ExecuteStoreQuery<PVM_NH_YaoHaoUsers>(sql).ToList();
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

        #region 分页获取摇号活动用户列表记录数
        /// <summary>
        /// 分页获取摇号活动用户列表记录数
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <returns></returns>
        public int GetYaoHaoApplyUsersPageCount_BySearch_Admin(PVS_NH_YaoHaoUsers search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                SELECT  COUNT(1) AS Result    
                FROM 
                BidPrice au
                WHERE au.IsDel=0 AND au.ActivitiesId={0}  ",
                                                new object[]{
                                                search.ActivitiesId
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




    }
}