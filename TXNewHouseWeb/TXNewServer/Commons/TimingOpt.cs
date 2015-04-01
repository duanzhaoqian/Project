using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Sources;

namespace Commons
{
    public class TimingOpt
    {
        /// <summary>
        /// 秒杀定时服务操作
        /// </summary>
        public int TimingSeckill()
        {
            try
            {
                var _dal = new TimingDal();
                var timers = _dal.GetActivityTimingByTimingType(1);
                foreach (var item in timers)
                {
                    _dal.ExecuteActivityTimingSeckill(item.TimingID, item.HouseID, item.OperID);
                }
                return timers.Count;
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "秒杀定时服务操作:" + ex.Message, true);
                return 0;
            }
        }

        /// <summary>
        /// 金额返还业务
        /// </summary>
        /// <returns></returns>
        public int AmountTimingOperation()
        {
            try
            {
                var _dal = new TimingDal();
                var timers = _dal.GetAmountTimingByTimingType(1);
                foreach (var item in timers)
                {
                    _dal.ExecuteAmountTimingOperation(item);
                }
                return timers.Count;
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "秒杀定时服务操作:" + ex.Message, true);
                return 0;
            }
        }

        /// <summary>
        /// 活动结束判定定时服务
        /// </summary>
        /// <returns></returns>
        public int ActivitiesEndTimingOperation(int type)
        {
            try
            {
                var _dal = new ActivitiesDal();
                var activities = _dal.GetActivities(type, 2);
                foreach (var item in activities)
                {
                    _dal.ExecuteEndActivities(item.ActivityID, item.HouseID, item.TimingType);
                    _dal.SendMessage(item.DeveloperId, item.DeveloperName, item.ActivityName, item.HouseTitle);
                    TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", item.PremisesID, item.CityID);
                }
                return activities.Count;
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "秒杀定时服务操作:" + ex.Message, true);
                return 0;
            }
        }
        /// <summary>
        /// 活动开始判定 定时服务
        /// </summary>
        /// <returns></returns>
        public int ActivitiesBeginTimingOperation(int type)
        {
            try
            {
                var _dal = new ActivitiesDal();
                var activities = _dal.GetActivitiesIds(type, 1);
                if (activities != null && activities.Count > 0)
                {
                    _dal.ExecuteBeginActivities(string.Join(",", activities.ToArray()), 1);
                    return activities.Count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "秒杀定时服务操作:" + ex.Message, true);
                return 0;
            }
        }
    }
}
