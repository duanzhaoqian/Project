using System;
using System.Collections.Generic;
using TXModel.AdminPVM;
using TXModel.Commons;

namespace TXBll.NHouseActivies.Biding
{
    /// <summary>
    /// 竞价活动管理(网站管理平台)
    /// </summary>
    public partial class BidingBll
    {
        #region 分页获取竞价活动列表

        /// <summary>
        /// 分页获取竞价活动列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">尺寸</param>
        /// <returns></returns>
        public List<PVM_NH_Biding> GetPageList_BySearch_Admin(PVS_NH_Biding search, int pageIndex, int pageSize)
        {
            var list = _bidingDal.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            var datenow = DateTime.Now;
            foreach (PVM_NH_Biding m in list)
            {
                m.BeginTimeStr = m.BeginTime.ToString("yyyy-MM-dd");
                m.EndTimeStr = m.EndTime.ToString("yyyy-MM-dd");

                m.ActivitieStateStr =
                    TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(m.ActivitieState);
                //时间判断 0 未开始 1 进行中 2 已结束
                m.TimeFlag = m.BeginTime > datenow
                    ? 0
                    : m.BeginTime <= datenow && m.EndTime >= datenow ? 1 : m.EndTime < datenow ? 2 : -1;
                if (m.TimeFlag == 1)
                    m.SurplusTimeString = GetRestTime(m.EndTime, datenow);
            }
            return list;
        }

        #endregion

        #region 分页获取竞价活动列表

        /// <summary>
        /// 分页获取竞价活动列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_Biding search)
        {
            return _bidingDal.GetPageCount_BySearch_Admin(search);
        }

        #endregion

        #region 获取剩余时间

        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <returns></returns>
        private string GetRestTime(DateTime begin, DateTime end)
        {
            TimeSpan timeSpan = (begin - end);
            var str = string.Empty;
            if (0 < timeSpan.Days)
            {
                str += timeSpan.Days + "天";
            }
            if (0 < timeSpan.Hours)
            {
                str += timeSpan.Hours + "小时";
            }
            if (0 < timeSpan.Minutes)
            {
                str += timeSpan.Minutes + "分";
            }
            if (0 < timeSpan.Seconds)
            {
                str += timeSpan.Seconds + "秒";
            }
            if (string.IsNullOrEmpty(str))
            {
                str = "0";
            }
            return str;
        }

        #endregion

        #region 分页获取竞价用户列表

        public List<PVM_NH_BidingUsers> GetUserPageList_BySearch_Admin(PVS_NH_BidingUsers search, int pageIndex, int pageSize)
        {
            return _bidingDal.GetUserPageList_BySearch_Admin(search, pageIndex, pageSize);
        }

        #endregion

        #region 获取竞价用户记录数

        public int GetUserPageCount_BySearch_Admin(PVS_NH_BidingUsers search)
        {
            return _bidingDal.GetUserPageCount_BySearch_Admin(search);
        }

        #endregion

        #region 根据活动id获取活动相关信息

        /// <summary>
        /// 根据活动id获取活动相关信息
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public PVE_NH_Biding GetBidingActiviesInfo(int id)
        {
            return _bidingDal.GetBidingActiviesInfo(id);
        }

        #endregion

        #region 获取竞价 参与房源列表

        #region 获取竞价 参与房源列表

        /// <summary>
        /// 获取竞价 参与房源列表
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Biding_House> GetBidingHousePageList_BySearch_Admin(PVS_NH_Biding_House search, int pageIndex, int pageSize)
        {
            var list = _bidingDal.GetBidingHousePageList_BySearch_Admin2(search, pageIndex, pageSize);
            foreach (PVM_NH_Biding_House m in list)
            {
                m.SalesStatusStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(m.SalesStatus);
            }
            return list;
        }

        #endregion

        #region 获取竞价 参与房源列表记录数

        /// <summary>
        /// 获取竞价 参与房源列表记录数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetBidingHousePageCount_BySearch_Admin(PVS_NH_Biding_House search)
        {
            return _bidingDal.GetBidingHousePageCount_BySearch_Admin2(search);
        }

        #endregion

        #endregion

        #region 修改竞价活动

        /// <summary>
        /// 修改竞价活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ESqlResult UpdateBiding_Admin(PVM_NH_Biding model)
        {
            return _bidingDal.UpdateBiding_Admin(model);
        }

        #endregion

        #region 导出竞价用户列表

        /// <summary>
        /// 导出竞价用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<PVM_NH_BidingUsers> GetAllUserPageList_BySearch_Admin(PVS_NH_BidingUsers search)
        {
            return _bidingDal.GetAllUserPageList_BySearch_Admin(search);
        }

        #endregion

        #region 删除竞价活动

        /// <summary>
        /// 删除竞价活动
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ESqlResult DeleteBiding_Admin(int aid)
        {
            return _bidingDal.DeleteBiding_Admin(aid);
        }

        #endregion

        #region 参与用户中标操作

        /// <summary>
        /// 参与用户中标操作
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="status">0未成交1已成交2已中标</param>
        /// <returns></returns>
        public bool UpdateBidingStatus_Admin(int bid, int status)
        {
            switch (status)
            {
                case 0:
                    return _bidingDal.UpdateBidingState_Admin_CancelZhongBiao(bid);
                case 2:
                    return _bidingDal.UpdateBidingState_Admin_ZhongBiao(bid);
                default:
                    return false;
            }
        }

        #endregion
    }
}