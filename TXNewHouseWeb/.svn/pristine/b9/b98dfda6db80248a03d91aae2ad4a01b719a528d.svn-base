using System;
using System.Collections.Generic;
using TXCommons.Admins;
using TXDal.NHouseActivies.TuanGou;
using TXModel.AdminPVM;

namespace TXBll.NHouseActivies.TuanGou
{
    public class TuanGou_AdminBll
    {
        private readonly TuanGou_AdminDal _tuangouDal = new TuanGou_AdminDal();

        #region 阶梯团购活动信息列表

        /// <summary>
        ///     阶梯团购活动信息列表
        /// </summary>
        /// <param name="search">搜索实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_LadderBuy> GetLadderBuyListByPages(PVS_NH_LadderBuy search, int pageIndex, int pageSize)
        {
            List<PVM_NH_LadderBuy> list = _tuangouDal.GetLadderBuyListByPages(search, pageIndex, pageSize);
            if (null != list && 0 < list.Count)
            {
                foreach (PVM_NH_LadderBuy item in list)
                {
                    item.BeginTimeString = string.Format("{0:yyyy-MM-dd}", item.BeginTime);
                    item.EndTimeString = string.Format("{0:yyyy-MM-dd}", item.EndTime);


                    item.ActivitieStateString = AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(item.ActivitieState); //item.ActivitieState != 2 ? AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(item.BeginTime, item.EndTime, DateTime.Now) : AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(item.ActivitieState);
                    item.RestTimeString = GetRestTime(item.EndTime - DateTime.Now);
                }
            }
            return list;
        }

        #endregion

        #region 阶梯团购活动信息总记录数

        /// <summary>
        ///     阶梯团购活动信息总记录数
        ///     Author:wangzhikun
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetLadderBuyListByPageCounts(PVS_NH_LadderBuy search)
        {
            return _tuangouDal.GetLadderBuyListByPageCounts(search);
        }

        #endregion

        #region 获取剩余时间

        /// <summary>
        ///     获取剩余时间
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private string GetRestTime(TimeSpan timeSpan)
        {
            string str = string.Empty;
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

        #region 获取排号购房报名用户列表

        /// <summary>
        ///     获取排号购房报名用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<PVM_NH_RegistrationUser> GetUsersListByPages(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            var list = _tuangouDal.GetUsersListByPages(search, pageIndex, pageSize);
            return list;
        }

        #endregion

        #region 获取排号购房报名用户信息总记录数

        /// <summary>
        ///     获取排号购房报名用户信息总记录数
        /// </summary>
        /// <param name="search">实体</param>
        /// <returns></returns>
        public int GetUsersListByPageCounts(PVS_NH_RegistrationUser search)
        {
            return _tuangouDal.GetUsersListByPageCounts(search);
        }

        #endregion

        #region 导出报名用户列表(排号购房)

        /// <summary>
        ///     导出报名用户列表
        /// </summary>
        /// <returns></returns>
        public List<PVM_NH_RegistrationUser> GetOutPutRegistUsersByList(int id)
        {
            return _tuangouDal.GetOutPutRegistUsersByList(id);
        }

        #endregion

        #region 阶梯团购用户报名

        /// <summary>
        /// 分页信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_RegistrationUser> GetUsersListByPages_TuanGou(PVS_NH_RegistrationUser search, int pageIndex, int pageSize)
        {
            var list = _tuangouDal.GetUsersListByPages_TuanGou(search, pageIndex, pageSize);
            return list;
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetUsersListByPageCounts_TuanGou(PVS_NH_RegistrationUser search)
        {
            return _tuangouDal.GetUsersListByPageCounts_TuanGou(search);
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <returns></returns>
        public List<PVM_NH_RegistrationUser> GetOutPutRegistUsersByList_TuanGou(int id)
        {
            return _tuangouDal.GetOutPutRegistUsersByList_TuanGou(id);
        }

        #endregion

        #region 活动详情列表

        /// <summary>
        ///     活动详情列表
        /// </summary>
        /// <param name="activeId">活动Id</param>
        /// <returns></returns>
        public List<PVM_NH_LadderBuyDetail> GetLadderBuyByList(int activeId)
        {
            var pvm = new PVM_NH_LadderBuyDetail();
            List<PVM_NH_LadderBuyDetail> list = _tuangouDal.GetLadderBuyByList(activeId);
            foreach (PVM_NH_LadderBuyDetail item in list)
            {
                if (item.Id == activeId)
                {
                    pvm.BeginTimeString = string.Format("{0:yyyy-MM-dd}", item.BeginTime);
                    pvm.EndTimeString = string.Format("{0:yyyy-MM-dd}", item.EndTime);
                }
            }
            return list;
        }

        #endregion

        #region 列表总记录数

        /// <summary>
        ///     列表总记录数
        /// </summary>
        /// <param name="activeId">活动Id</param>
        /// <returns></returns>
        public int GetLadderBuyByPageCounts(int activeId)
        {
            return _tuangouDal.GetLadderBuyByPageCounts(activeId);
        }

        #endregion

        #region 删除活动

        /// <summary>
        ///     删除活动(团购、排号)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelTuanGou(int id)
        {
            _tuangouDal.DelTuanGou_Social(id);
            return _tuangouDal.DelTuanGou(id);
        }

        #endregion

        #region 排号购房房源

        /// <summary>
        ///     获取搜索记录
        ///     author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_Purchase_Edit2> GetPageList_BySearch_EditPurchaseHouses_Admin(PVS_NH_Purchase_Edit search, int pageIndex = 0, int pageSize = 0)
        {
            var list = _tuangouDal.GetPageList_BySearch_EditPurchaseHouses_Admin(search, pageIndex, pageSize);
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].FloorString = (0 < list[i].Floor ? string.Format("{0}层", list[i].Floor) : string.Format("地下{0}层", list[i].Floor));
                    list[i].BuildingName += list[i].BuildingNameType;
                    list[i].RoomTypeString = string.Format("{0}室{1}厅{2}卫{3}厨", list[i].Room, list[i].Hall, list[i].Toilet, list[i].Kitchen);
                    list[i].BuildingAreaString = string.Format("{0:0}", list[i].BuildingArea);
                    list[i].SinglePriceString = string.Format("{0:0}", list[i].SinglePrice);
                    list[i].TotalPriceString = string.Format("{0:0}", list[i].TotalPrice);
                    list[i].SalesStatusString = AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(list[i].SalesStatus);
                    list[i].CreateTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", list[i].CreateTime);
                }
            }
            return list;
        }

        /// <summary>
        ///     获取总记录数
        ///     author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_EditPurchaseHouses_Admin(PVS_NH_Purchase_Edit search)
        {
            return _tuangouDal.GetTotalCount_BySearch_EditPurchaseHouses_Admin(search);
        }

        #endregion

        #region 修改 排号购房

        /// <summary>
        /// 更新折扣活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePurchase_Admin(PVM_NH_Purchase model)
        {
            var count = _tuangouDal.UpdatePurchase_Admin_UpdateActivities(model);
            if (0 < count)
            {
                count = _tuangouDal.UpdatePurchase_Admin_UpdateActivityHouse(model);
                return 0 < count;
            }
            return false;
        }

        #endregion

        #region 修改团购活动

        /// <summary>
        /// 获取活动信息部分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PVM_NH_TuanGou_Social> GetTuanGouSocial(int id)
        {
            return _tuangouDal.GetTuanGouSocial(id);
        }

        public bool UpdateTuanGou_Admin(PVM_NH_TuanGou model)
        {
            var count = _tuangouDal.UpdateTuanGou_Admin_UpdateActivities(model);
            if (0 == count)
            {
                return false;
            }

            count = _tuangouDal.UpdateTuanGou_Admin_UpdateSocial(model);
            if (0 == count)
            {
                return false;
            }

            count = _tuangouDal.UpdateTuanGou_Admin_UpdateActivityHouse(model);
            if (0 == count)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 排号详情

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PVE_NH_Purchase_Detail GetPurchaseDetail(int id)
        {
            var model = _tuangouDal.GetPurchaseDetail(id);
            if (null == model)
            {
                return null;
            }

            var list = _tuangouDal.GetPurchaseDetail_Houses(id);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SinglePriceString = string.Format("{0:F0}", list[i].SinglePrice);
                list[i].CreateTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", list[i].CreateTime);
                list[i].SalesStatusString = AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(list[i].SalesStatus);
            }
            model.Houses = list;

            return model;
        }

        #endregion

        #region 团购详情

        /// <summary>
        /// 团购详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PVE_NH_Purchase_Detail GetTuanGouDetail(int id)
        {
            var model = _tuangouDal.GetPurchaseDetail(id);
            if (null == model)
            {
                return null;
            }

            var socials = _tuangouDal.GetTuanGouSocial(id);
            model.Socials = socials;

            var list = _tuangouDal.GetPurchaseDetail_Houses(id);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SinglePriceString = string.Format("{0:F0}", list[i].SinglePrice);
                list[i].CreateTimeString = string.Format("{0:yyyy-MM-dd HH:mm}", list[i].CreateTime);
                list[i].SalesStatusString = AdminTypes.Db_NewHouse.Tb_House.Fc_ById.For_SalesStatus(list[i].SalesStatus);
            }
            model.Houses = list;

            return model;
        }

        #endregion

    }
}