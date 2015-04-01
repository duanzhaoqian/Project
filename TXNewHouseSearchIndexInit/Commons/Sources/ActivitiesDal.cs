using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Commons.Models;
using System.Data;

namespace Commons.Sources
{
    public class ActivitiesDal
    {
        private static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        public ActivitiesDal()
        {

        }

        #region 活动结束

        /// <summary>
        /// 获取所有活动时间过期的活动描述
        /// </summary>
        /// <param name="type">活动类型 1摇号 2限时折扣 3排号购房 4阶梯团购 5竞价 6砍价 7秒杀 8一口价</param>
        /// <param name="state">1 开始 2 结束</param>
        /// <returns></returns>
        public List<ActivityTiming> GetActivities(int type, int state)
        {
            var sql = @"SELECT  AH.Id ,
                                A.Id AS ActivityID ,
                                A.DeveloperId ,
                                D.RealName ,
                                A.Name AS ActivityName ,
                                AH.PremisesId ,
                                AH.HouseId AS HouseID ,
                                AH.CityId ,
                                P.Name AS PremisesName ,
                                B.Name AS BuildingName ,
                                B.NameType as BuildingTypeName,
                                U.Name AS UnitName ,
                                H.Name AS HouseTitle ,
                                A.[Type] ,
                                ActivitieState ,
                                A.EndTime
                        FROM    dbo.ActivitiesHouse (NOLOCK) AS AH
                                JOIN dbo.Premises (NOLOCK) AS P ON P.Id = AH.PremisesId
                                JOIN dbo.Building (NOLOCK) AS B ON B.Id = AH.BuildingId
                                JOIN dbo.Unit (NOLOCK) AS U ON U.Num = AH.UnitId
                                                                AND U.BuildingId = B.Id
                                                                AND U.PremisesId = P.Id 
                                JOIN dbo.Activities (NOLOCK) AS A ON AH.ActivitiesId = A.Id
                                JOIN dbo.Developer (NOLOCK) AS D ON D.Id = A.DeveloperId
                                JOIN dbo.House (NOLOCK) AS H ON H.Id = AH.HouseId
                        WHERE   P.IsDel = 0
                                AND B.IsDel = 0
                                AND U.IsDel = 0
                                AND A.[Type] = {0}
                                AND A.IsDel = 0
                                AND AH.IsDel = 0 {1}";

            var sqlstr = "";
            var time = "";
            if (type == 7)
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");
            }
            else
            {
                time = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            }
            switch (state)
            {
                case 1:
                    sqlstr = string.Format("AND A.BeginTime <= '{0}' AND A.ActivitieState = 0", time);
                    break;
                default:
                    sqlstr = string.Format("AND A.EndTime <= '{0}' AND A.ActivitieState = 1", time);
                    break;
            }
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, type, sqlstr));

            var list = new List<ActivityTiming>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectActivityTimingToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取活动定时服务信息：" + ex.Message, false);
                }
            }

            return list;
        }
        /// <summary>
        /// 获取指定活动类型的活动开始或结束的ID集合
        /// </summary>
        /// <param name="type">活动类型  1摇号 2限时折扣 3排号购房 4阶梯团购 5竞价 6砍价 7秒杀 8一口价</param>
        /// <param name="state">1 开始  2 结束</param>
        /// <returns></returns>
        public List<int> GetActivitiesIds(int type, int state)
        {
            var sql = @"SELECT  A.Id ,
                        FROM    dbo.Activities (NOLOCK) AS A
                        WHERE   A.[Type] = {0}
                                AND A.IsDel = 0 {1}";
            var sqlstr = "";
            var time = "";
            if (type == 7)
            {
                time = DateTime.Now.ToString("yyyy-MM-dd HH:00:00");
            }
            else
            {
                time = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            }
            switch (state)
            {
                case 1:
                    sqlstr = string.Format("AND A.BeginTime <= '{0}' AND A.ActivitieState = 0", time);
                    break;
                default:
                    sqlstr = string.Format("AND A.EndTime <= '{0}' AND A.ActivitieState = 1", time);
                    break;
            }
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, type, sqlstr));

            var list = new List<int>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    int id = item["Id"] == DBNull.Value ? 0 : Convert.ToInt32(item["Id"]);
                    list.Add(id);
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取活动定时服务信息：" + ex.Message, false);
                }
            }

            return list;

        }

        private ActivityTiming SelectActivityTimingToParticipates(DataRow row)
        {
            ActivityTiming getActivityTiming = new ActivityTiming();

            getActivityTiming.TimingID = row["Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]);

            getActivityTiming.DeveloperId = row["DeveloperId"] == DBNull.Value ? 0 : Convert.ToInt32(row["DeveloperId"]);

            getActivityTiming.DeveloperName = row["RealName"] == DBNull.Value ? string.Empty : Convert.ToString(row["RealName"]);

            getActivityTiming.ActivityID = row["ActivityID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ActivityID"]);

            getActivityTiming.ActivityName = row["ActivityName"] == DBNull.Value ? string.Empty : Convert.ToString(row["ActivityName"]);

            getActivityTiming.PremisesID = row["PremisesId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PremisesId"]);

            getActivityTiming.HouseID = row["HouseID"] == DBNull.Value ? 0 : Convert.ToInt32(row["HouseID"]);

            getActivityTiming.CityID = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CityId"]);

            var PremisesName = row["PremisesName"] == DBNull.Value ? string.Empty : Convert.ToString(row["PremisesName"]);

            var BuildingName = row["BuildingName"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingName"]);

            var UnitName = row["UnitName"] == DBNull.Value ? string.Empty : Convert.ToString(row["UnitName"]);

            var HouseTitle = row["HouseTitle"] == DBNull.Value ? string.Empty : Convert.ToString(row["HouseTitle"]);

            getActivityTiming.HouseTitle = PremisesName + " " + BuildingName + getActivityTiming.BuildingTypeName + " " + UnitName + " " + HouseTitle;//row["HouseTitle"] == DBNull.Value ? string.Empty : Convert.ToString(row["HouseTitle"]);

            getActivityTiming.TimingType = row["Type"] == DBNull.Value ? 0 : Convert.ToInt32(row["Type"]);

            getActivityTiming.Status = row["ActivitieState"] == DBNull.Value ? 0 : Convert.ToInt32(row["ActivitieState"]);

            getActivityTiming.EndTime = row["EndTime"] == DBNull.Value ? string.Empty : Convert.ToString(row["EndTime"]);
            getActivityTiming.BuildingTypeName = row["BuildingTypeName"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingTypeName"]);
            return getActivityTiming;
        }

        public int ExecuteEndActivities(int activityid, int houseid, int type)
        {
            var sql = @"BEGIN TRY
                        BEGIN TRAN
                        DECLARE @ActivityID INT ,
                            @HouseID INT ,
                            @Type INT
                        SET @ActivityID = {0}
                        SET @HouseID = {1}
                        SET @Type = {2}
	
                        UPDATE  dbo.Activities
                        SET     ActivitieState = 2
                        WHERE   Id = @ActivityID
	
                        IF @Type = 5 
                            BEGIN
                                DECLARE @MaxPrice MONEY
                                SET @MaxPrice = 0 
                                SELECT  @MaxPrice = A.BidUserPrice
                                FROM    ( SELECT TOP 1
                                                    *
                                          FROM      ( SELECT    BidUserPrice ,
                                                                COUNT(1) AS Number
                                                      FROM      dbo.BidPrice(NOLOCK)
                                                      WHERE     ActivitiesId = @ActivityID
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
                                                AND ActivitiesId = @ActivityID
                                                AND IsDel = 0
                                        UPDATE  dbo.House
                                        SET     SalesStatus = 3
                                        WHERE   Id = @HouseID
                                                AND IsDel = 0
                                    END                
                            END
                        IF @Type = 6 
                            BEGIN
                                DECLARE @MinPrice MONEY
                                SET @MinPrice = 0          
                                SELECT  @MinPrice = A.BidUserPrice
                                FROM    ( SELECT TOP 1
                                                    *
                                          FROM      ( SELECT    BidUserPrice ,
                                                                COUNT(1) AS Number
                                                      FROM      dbo.BidPrice
                                                      WHERE     ActivitiesId = @ActivityID
                                                      GROUP BY  BidUserPrice
                                                    ) AS BidPrice
                                          WHERE     BidPrice.Number = 1
                                          ORDER BY  BidPrice.BidUserPrice ASC
                                        ) AS A
                                IF @MinPrice <> 0 
                                    BEGIN
                                        UPDATE  dbo.BidPrice
                                        SET     [Status] = 1
                                        WHERE   BidUserPrice = @MinPrice
                                                AND ActivitiesId = @ActivityID
                                                AND IsDel = 0
                                        UPDATE  dbo.House
                                        SET     SalesStatus = 3
                                        WHERE   Id = @HouseID
                                                AND IsDel = 0
                                    END
                            END
   
                        INSERT  INTO dbo.TimingRecord
                                ( DealStatus, [Desc], DealTime )
                        VALUES  ( 0, -- DealStatus - int
                                  '{3}', -- Desc - nvarchar(500)
                                  GETDATE()  -- DealTime - datetime
                                  )
                        SELECT 1
                        COMMIT
                    END TRY
                    BEGIN CATCH
                        SELECT  0
                        ROLLBACK TRAN
                        INSERT  INTO dbo.TimingRecord
                                ( DealStatus, [Desc], DealTime )
                        VALUES  ( 1, -- DealStatus - int
                                  '{4}', -- Desc - nvarchar(500)
                                  GETDATE()  -- DealTime - datetime
                                  )
                    END CATCH ";

            var sucstr = string.Format("活动ID：{0}、房源ID：{1}、定时服务:处理结束。", activityid, houseid);

            var exstr = string.Format("活动ID：{0}、房源ID：{1}、定时服务:处理异常。", activityid, houseid);

            var result = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, string.Format(sql, activityid, houseid, type, sucstr, exstr));

            return result;
        }

        /// <summary>
        /// 批量修改活动已开始的状态
        /// </summary>
        /// <param name="activityids">活动IDs</param>
        /// <param name="state">活动状态 0未开始 1已开始 2已结束 3 摇号活动申请</param>
        /// <returns></returns>
        public int ExecuteBeginActivities(string activityids, int state)
        {
            var sql = @"UPDATE  dbo.Activities
                        SET     ActivitieState = {0}
                        WHERE   Id IN ({1}) 
                                AND IsDel = 0";
            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, string.Format(sql, state, activityids));
        }

        #endregion

        #region 系统邮件
        /// <summary>
        /// 发送系统站内信
        /// </summary>
        /// <param name="developerId">开发商ID</param>
        /// <param name="developername">开发商姓名</param>
        /// <param name="activityName">活动名称</param>
        /// <param name="houseTitle">房源名称</param>
        public void SendMessage(int developerId, string developername, string activityName, string houseTitle)
        {
            var sql = @"INSERT  INTO dbo.DeveloperMessage
                                ( SendUserId ,
                                  ReceiveUserId ,
                                  Title ,
                                  Content ,
                                  IsRead ,
                                  CreateTime ,
                                  IsDel
                                )
                        VALUES  ( 0 , -- SendUserId - int
                                  {0} , -- ReceiveUserId - int
                                  '营销活动已结束' , -- Title - nvarchar(100)
                                  '{1}' , -- Content - nvarchar(500)
                                  0 , -- IsRead - bit
                                  GETDATE() , -- CreateTime - datetime
                                  0  -- IsDel - bit
                                )";
            var context = string.Format("尊敬的{0}，您所创建的[{1}]+[{2}]已结束，请及时查看用户报名情况，并及时处理。", developername, houseTitle, activityName);

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, string.Format(sql, developerId, context));

        }

        #endregion

    }
}
