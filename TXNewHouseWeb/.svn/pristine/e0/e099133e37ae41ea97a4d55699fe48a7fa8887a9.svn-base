using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Models;
using System.Data;
using System.Configuration;

namespace Commons.Sources
{
    public class TimingDal
    {

        private static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        private static string NewHouseDBName = ConfigurationManager.AppSettings["NewHouseDBName"].ToString();

        private static string UserDBName = ConfigurationManager.AppSettings["UserDBName"].ToString();

        #region 活动

        /// <summary>
        /// 获取所有满足条件的定时信息
        /// </summary>
        /// <param name="type">服务类型：1 秒杀</param>
        /// <returns></returns>
        public List<ActivityTiming> GetActivityTimingByTimingType(int type)
        {
            var sql = @"SELECT  Id ,
                                ActivityID ,
                                HouseId ,
                                OperId ,
                                [Type] ,
                                [Status] ,
                                EndTime
                        FROM    dbo.ActivitiesTiming (NOLOCK)
                        WHERE   [Type]={0} AND EndTime <= GETDATE()
                        ORDER BY EndTime ASC";

            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, type));

            var list = new List<ActivityTiming>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectActivityTimingToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取房源定时服务信息：" + ex.Message, false);
                }
            }

            return list;
        }

        private ActivityTiming SelectActivityTimingToParticipates(DataRow row)
        {
            ActivityTiming getActivityTiming = new ActivityTiming();

            getActivityTiming.TimingID = row["Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]);

            getActivityTiming.ActivityID = row["ActivityID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ActivityID"]);

            getActivityTiming.HouseID = row["HouseId"] == DBNull.Value ? 0 : Convert.ToInt32(row["HouseId"]);

            getActivityTiming.OperID = row["OperId"] == DBNull.Value ? string.Empty : Convert.ToString(row["OperId"]);

            getActivityTiming.TimingType = row["Type"] == DBNull.Value ? 0 : Convert.ToInt32(row["Type"]);

            getActivityTiming.Status = row["Status"] == DBNull.Value ? 0 : Convert.ToInt32(row["Status"]);

            getActivityTiming.EndTime = row["EndTime"] == DBNull.Value ? string.Empty : Convert.ToString(row["EndTime"]);

            return getActivityTiming;
        }

        /// <summary>
        /// 执行秒杀处理逻辑
        /// </summary>
        /// <param name="ids">定时服务ID</param>
        /// <param name="houseIds">房源ID</param>
        /// <param name="orderIds">订单ID</param>
        /// <returns></returns>
        public int ExecuteActivityTimingSeckill(int id, int houseId, string orderId)
        {
            var sql = @"BEGIN TRY
                            BEGIN TRAN
                            DECLARE @BondStatus INT ,
                                @OrderID INT
                            SET @OrderID = {0}
                            SET @BondStatus = 0
                            SELECT TOP 1
                                    @BondStatus = BondStatus
                            FROM    dbo.[Order](NOLOCK)
                            WHERE   Id = @OrderID
                                    AND IsDel = 'false'
                            IF @BondStatus = 0 
                                BEGIN
                                    DELETE  dbo.UniqueActivitiesHouse
                                    WHERE   HouseId = {1}
                                    UPDATE  dbo.[Order]
                                    SET     [Status] = 3
                                    WHERE   Id = @OrderID
                                END
  
                            DELETE  dbo.ActivitiesTiming
                            WHERE   Id = {2}
                            INSERT  INTO dbo.TimingRecord
                                    ( DealStatus, [Desc], DealTime )
                            VALUES  ( 0, -- DealStatus - int
                                      '{3}', -- Desc - nvarchar(500)
                                      GETDATE()  -- DealTime - datetime
                                      )
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
                        END CATCH 
                        ";

            if (string.IsNullOrEmpty(orderId))
                orderId = "0";
            var sucstr = string.Format("房源：{0}、订单：{1}、定时服务:{2}处理结束。", houseId, orderId, id);

            var exstr = string.Format("房源：{0}、订单：{1}、定时服务:{2}处理异常。", houseId, orderId, id);

            var result = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, string.Format(sql, orderId, houseId, id, sucstr, exstr));

            return result;
        }

        #endregion

        #region 金额

        /// <summary>
        /// 获取所有满足条件的定时信息
        /// </summary>
        /// <param name="type">服务类型：1 保证金</param>
        /// <returns></returns>
        public List<AmountTiming> GetAmountTimingByTimingType(int type)
        {
            var sql = @"SELECT  [Id] ,
                                [ActivityID] ,
                                [OperId] ,
		                        [UserId],
                                [Type] ,
                                [AmountRecord],
		                        [OperPrice],
                                [EndTime]
                        FROM    dbo.AmountTiming (NOLOCK)
                        WHERE   [Type] = {0}
                                AND EndTime <= GETDATE()
                        ORDER BY EndTime ASC";

            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, type));

            var list = new List<AmountTiming>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectAmountTimingToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取房源定时服务信息：" + ex.Message, false);
                }
            }

            return list;
        }

        private AmountTiming SelectAmountTimingToParticipates(DataRow row)
        {
            AmountTiming getAmountTiming = new AmountTiming();

            getAmountTiming.TimingID = row["Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]);

            getAmountTiming.ActivityID = row["ActivityID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ActivityID"]);

            getAmountTiming.OperID = row["OperId"] == DBNull.Value ? string.Empty : Convert.ToString(row["OperId"]);

            getAmountTiming.UserID = row["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserId"]);

            getAmountTiming.TimingType = row["Type"] == DBNull.Value ? 0 : Convert.ToInt32(row["Type"]);

            getAmountTiming.AmountRecord = row["AmountRecord"] == DBNull.Value ? string.Empty : Convert.ToString(row["AmountRecord"]);

            getAmountTiming.OperPrice = row["OperPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(row["OperPrice"]);

            getAmountTiming.EndTime = row["EndTime"] == DBNull.Value ? string.Empty : Convert.ToString(row["EndTime"]);

            return getAmountTiming;
        }

        /// <summary>
        /// 执行秒杀处理逻辑
        /// </summary>
        /// <param name="ids">定时服务ID</param>
        /// <param name="houseIds">房源ID</param>
        /// <param name="orderIds">订单ID</param>
        /// <returns></returns>
        public int ExecuteAmountTimingOperation(AmountTiming timing)
        {
            var sql = @"BEGIN TRY
                        BEGIN TRAN
                        DECLARE @UserID INT ,
                            @OperPrice MONEY ,
                            @TimingID INT ,
                            @OrderID INT ,
                            @AmountRecord NVARCHAR(500)
                        SET @UserID = {0}  
                        SET @OperPrice = {1}
                        SET @TimingID = {2}
                        SET @AmountRecord = '{3}'
                        --修改用户金额
                        UPDATE  {4}.dbo.U_Account
                        SET     Price = Price + @OperPrice
                        WHERE   UserId = @UserID
	                    --添加金额日志
                        INSERT  INTO {5}.dbo.U_AccountLog
                                ( UserId ,
                                  UserType ,
                                  [Type] ,
                                  LogType ,
                                  Price ,
                                  [Desc] ,
                                  Createtime
	                            )
                        VALUES  ( @UserID , -- UserId - int
                                  0 , -- UserType - int
                                  1 , -- Type - tinyint
                                  9 , -- LogType - int
                                  @OperPrice , -- Price - money
                                  @AmountRecord , -- Desc - nvarchar(50)
                                  GETDATE()  -- Createtime - datetime
	                            )
	                    --修改订单表状态
                        UPDATE  {6}.dbo.[Order]
                        SET     BondStatus = 2
                        WHERE   Id = @OrderID
	                    --删除定时服务记录
                        DELETE  {7}.dbo.AmountTiming
                        WHERE   Id = @TimingID

                        INSERT  INTO {8}.dbo.TimingRecord
                                ( DealStatus, [Desc], DealTime )
                        VALUES  ( 0, -- DealStatus - int
                                  '{9}', -- Desc - nvarchar(500)
                                  GETDATE()  -- DealTime - datetime
                                  )
                        SELECT  1
                        COMMIT TRAN
                    END TRY
                    BEGIN CATCH
                        SELECT  0
                        ROLLBACK TRAN
                        INSERT  INTO kyj_NewHouseDB.dbo.TimingRecord
                                ( DealStatus, [Desc], DealTime )
                        VALUES  ( 1, -- DealStatus - int
                                  '{10}', -- Desc - nvarchar(500)
                                  GETDATE()  -- DealTime - datetime
                                  )
                    END CATCH 
                        ";

            if (string.IsNullOrEmpty(timing.OperID))
                timing.OperID = "0";
            var sucstr = string.Format("活动：{0}金额返还、订单：{1}、定时服务:{2}处理结束。", timing.ActivityID, timing.OperID, timing.TimingID);

            var exstr = string.Format("活动：{0}金额返还、订单：{1}、定时服务:{2}处理异常。", timing.ActivityID, timing.OperID, timing.TimingID);

            var result = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, string.Format(sql, timing.UserID, timing.OperPrice, timing.TimingID, timing.AmountRecord,
                UserDBName, UserDBName, NewHouseDBName, NewHouseDBName, NewHouseDBName, sucstr, exstr));

            return result;
        }
        #endregion
    }
}
