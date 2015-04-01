using System;
using System.Collections.Generic;
using TXCommons.Admins;
using TXDal.NHouseActivies.APrice;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXBll.NHouseActivies.APrice
{
    public class APrice_AdminBll
    {
        private readonly APrice_AdminDal _APriceDal = new APrice_AdminDal();

        #region 一口价活动信息列表

        /// <summary>
        /// 一口价活动信息列表
        /// Author:wangzhikun
        /// </summary>
        /// <param name="search">实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_Bargain> GetAPriceListByPages(PVS_NH_Bargain search, int pageIndex, int pageSize)
        {
            var list = _APriceDal.GetAPriceListByPages(search, pageIndex, pageSize);
            foreach (var item in list)
            {
                item.BeginTimeString = string.Format("{0:yyyy-MM-dd}", item.BeginTime);
                item.EndTimeString = string.Format("{0:yyyy-MM-dd}", item.EndTime);


                item.ActivitieStateString = item.ActivitieState != 2 ? AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(item.BeginTime, item.EndTime, DateTime.Now) : AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(item.ActivitieState);
                item.SurplusTimeString = GetRestTime(item.EndTime - DateTime.Now);
                //item.ActivitieStateString = AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(item.BeginTime, item.EndTime, DateTime.Now);
                item.ActivitieStateString = AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(item.ActivitieState);
            }
            return list;
        }

        #endregion

        #region 一口价活动信息总记录数

        /// <summary>
        /// 一口价活动信息总记录数
        /// Author:wangzhikun
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetAPriceByPageCounts(PVS_NH_Bargain search)
        {
            return _APriceDal.GetAPriceByPageCounts(search);
        }

        #endregion

        #region 获取一口价报名用户列表

        /// <summary>
        /// 获取一口价报名用户列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_BidingUsers> GetUsersListByPages(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            return _APriceDal.GetUsersListByPages(search, pageIndex, pageSize);
        }

        #endregion

        #region 获取一口价报名用户信息总记录数

        /// <summary>
        /// 获取一口价报名用户信息总记录数
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetUsersListByPageCounts(PVS_NH_RegistrationUser search)
        {
            return _APriceDal.GetUsersListByPageCounts(search);
        }

        #endregion

        #region 导出报名用户列表

        /// <summary>
        /// 导出报名用户列表
        /// </summary>
        /// <returns></returns>
        public List<PVM_NH_BidingUsers> GetOutPutRegistUsersByList(PVS_NH_RegistrationUser search)
        {
            return _APriceDal.GetOutPutRegistUsersByList(search);
        }

        #endregion

        #region 根据活动id获取活动相关信息

        /// <summary>
        /// 根据活动id获取活动相关信息
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public PVM_NH_BargainActive GetAPriceActiviesInfo(int id)
        {
            return _APriceDal.GetAPriceActiviesInfo(id);
        }

        #endregion

        #region 获取一口价活动参与房源列表

        /// <summary>
        /// 获取一口价活动参与房源列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Biding_House> GetAPriceHousePageList_BySearch_Admin(PVS_NH_Biding_House search, int pageIndex, int pageSize)
        {
            var list = _APriceDal.GetAPriceHousePageList_BySearch_Admin2(search, pageIndex, pageSize);
            foreach (PVM_NH_Biding_House m in list)
            {
                m.SalesStatusStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(m.SalesStatus);
            }
            return list;
        }

        #endregion

        #region 获取一口价活动参与房源列表记录数

        /// <summary>
        /// 获取一口价活动参与房源列表记录数
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetAPriceHousePageCount_BySearch_Admin(PVS_NH_Biding_House search)
        {
            return _APriceDal.GetAPriceHousePageCount_BySearch_Admin2(search);
        }

        #endregion

        #region 修改一口价活动

        /// <summary>
        /// 修改一口价活动
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public ESqlResult UpdateAPrice_Admin(PVM_NH_Biding model)
        {
            return _APriceDal.UpdateAPrice_Admin(model);
        }

        #endregion

        #region 删除一口价活动

        /// <summary>
        /// 删除一口价活动
        /// </summary>
        /// <param name="aid">活动Id</param>
        /// <returns></returns>
        public ESqlResult DeleteActive(int aid)
        {
            return _APriceDal.DeleteActive(aid);
        }

        #endregion

        #region 获取剩余时间

        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private string GetRestTime(TimeSpan timeSpan)
        {
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
            if (string.IsNullOrEmpty(str))
            {
                str = "0";
            }
            return str;
        }

        #endregion

        /// <summary>
        /// 根据编号获取一口价实体
        /// </summary>
        /// <param name="id">一口价编号</param>
        /// <returns>返回：一口价实体</returns>
        public Activity GetEntity_ById(int id)
        {
            return (_APriceDal.GetEntity_ById(id) as Activity);
        }
    }
}