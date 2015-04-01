using System.Collections.Generic;
using TXDal.Activitys;
using TXModel.Web;
using System.Collections;

namespace TXBll.Activitys
{
    public class ActivitysBll
    {
        ActivitysDal _activitysDal = new ActivitysDal();

        #region 摇号活动列表
        /// <summary>
        /// 摇号活动列表
        /// author：sunlin
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<ActivityList> GetExerciseList(int pageindex, int pagesize, out int totalcount)
        {
            return _activitysDal.GetActivityList(pageindex, pagesize, out totalcount);
        }
        #endregion

        #region 本月摇号活动
        /// <summary>
        /// 本月摇号活动
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActivityList GetExercise()
        {
            return _activitysDal.GetActivity();
        }
        #endregion

        /// <summary>
        /// 跟据活动Id取摇号活动详情
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActivityList GetActivityById(int id)
        {
            return _activitysDal.GetActivityById(id);
        }

        #region 活动订单相关

        /// <summary>
        /// 提交前台活动订单
        /// author:maboxun
        /// </summary>
        /// <param name="order">活动订单</param>
        /// <returns></returns>
        public int AddActivityOrder(TXOrm.Order order)
        {
            return _activitysDal.AddActivityOrder(order);
        }

        /// <summary>
        /// 根据id获得活动订单
        /// author: maboxun
        /// </summary>
        /// <param name="orderID">订单id</param>
        /// <returns></returns>
        public TXOrm.Order GetActivityOrderByID(int orderID)
        {
            return _activitysDal.GetActivityOrderByID(orderID);
        }

        /// <summary>
        /// 获得用户参加某活动的订单
        /// author: maboxun
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="activitiesID"></param>
        /// <returns></returns>
        public List<TXOrm.Order> GetActivityOrderByUserID(int userID, int activitiesID)
        {
            return _activitysDal.GetActivityOrderByUserID(userID, activitiesID);
        }

        /// <summary>
        /// 修改订单状态
        /// author: maboxun
        /// 订单支付成功，修改订单状态，添加出价和用户参与信息到相关表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateActivityOrderSuc(int orderID)
        {
            return _activitysDal.UpdateActivityOrderSuc(orderID);
        }

        /// <summary>
        /// 修改秒杀活动订单状态
        /// author: maboxun
        /// 秒杀活动的订单支付成功，修改订单状态、添加出价、修改房源状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateMSOrderSuc(int orderID)
        {
            return _activitysDal.UpdateMSOrderSuc(orderID);
        }

        /// <summary>
        /// 修改订单中相关用户信息
        /// author: maboxun
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderUserInfo(TXOrm.Order order)
        {
            return _activitysDal.UpdateOrderUserInfo(order);
        }

        /// <summary>
        /// 第三方支付完成 要写入账户流水
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int AddUserAccountInfo(int orderID)
        {
            return _activitysDal.AddUserAccountInfo(orderID);
        }

        #endregion


        #region 秒杀相关

        /// <summary>
        /// 添加 唯一活动房源（添加成功失败状态视为秒杀状态）
        /// author: maboxun
        /// </summary>
        /// <returns></returns>
        public int AddUniqueActivitiesHouse(TXOrm.UniqueActivitiesHouse uaHouse)
        {
            return _activitysDal.AddUniqueActivitiesHouse(uaHouse);
        }

        /// <summary>
        /// 添加 活动定时服务
        /// author: maboxun
        /// </summary>
        /// <param name="activitiesTiming">活动定时服务</param>
        /// <param name="msOperatingTime">秒杀操作处理时间</param>
        /// <returns></returns>
        public int AddActivitiesTiming(TXOrm.ActivitiesTiming activitiesTiming, int msOperatingTime)
        {
            return _activitysDal.AddActivitiesTiming(activitiesTiming, msOperatingTime);
        }

        /// <summary>
        /// 获取该活动某个订单是否有定时服务
        /// author: maboxun
        /// </summary>
        /// <param name="activitiesTiming"></param>
        /// <returns></returns>
        public bool HasActivitiesTimingByOrder(TXOrm.ActivitiesTiming activitiesTiming)
        {
            return _activitysDal.HasActivitiesTimingByOrder(activitiesTiming);
        }

        /// <summary>
        /// 根据房源id 修改 房源定时服务
        /// author: maboxun
        /// </summary>
        /// <returns></returns>
        public int UpdateHouseTimingByHouseID(TXOrm.ActivitiesTiming activitiesTiming)
        {
            return _activitysDal.UpdateHouseTimingByHouseID(activitiesTiming);
        }

        #endregion

        #region 获取活动出价次数、参与人数

        /// <summary>
        /// 获取活动出价次数、参与人数
        /// author: maboxun
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList GetActivityBidNum(int id)
        {
            return _activitysDal.GetActivityBidNum(id);
        }
        #endregion
    }
}
