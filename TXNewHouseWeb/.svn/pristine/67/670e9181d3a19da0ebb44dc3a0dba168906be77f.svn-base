
using System.Collections.Generic;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXBll.NHouseActivies.Yaohao
{
    /// <summary>
    /// 摇号 (网站管理平台)
    /// </summary>
    public partial class YaohaoBll
    {
        /// <summary>
        /// 分页获取摇号活动列表
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">尺寸</param>
        /// <returns></returns>
        public List<PVM_NH_YaoHao> GetPageList_BySearch_Admin(PVS_NH_YaoHao search, int pageIndex, int pageSize)
        {
            var list = _yaohaoDal.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            foreach (PVM_NH_YaoHao m in list)
            {
                if(m.State!=0)
                m.StateStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_MarkStates(m.State);
                m.CompanyTypeStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Developer.Fc_ById.For_TypeState(m.CompanyType);
                m.ActivitieStateStr =
                    TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_ActivitieStates(m.ActivitieState);
            }
            return list;
        }

        /// <summary>
        /// 分页获取摇号活动列表记录数
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_YaoHao search)
        {
            return _yaohaoDal.GetPageCount_BySearch_Admin(search);
        }

         #region 更新摇号活动标记状态

        /// <summary>
        /// 更新摇号活动标记状态
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">状态0 待处理 1 已通过 2 未通过 3未联系 4 已联系 5 已通过并开启报名入口</param>
        /// <returns></returns>
        public ESqlResult UpdateYaoHaoState_Admin(int id, int state)
        {
            return _yaohaoDal.UpdateYaoHaoState_Admin(id,state);
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
        public List<PVM_NH_YaoHaoApply> GetYaoHaoApplyPageList_BySearch_Admin(PVS_NH_YaoHao search, int pageIndex,int pageSize)
        {
            var list=_yaohaoDal.GetYaoHaoApplyPageList_BySearch_Admin(search,pageIndex,pageSize);
            foreach (PVM_NH_YaoHaoApply m in list)
            {
                m.StateStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Activities.Fc_ById.For_MarkStates(m.State);
                m.CompanyTypeStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Developer.Fc_ById.For_TypeState(m.CompanyType);
            }
            return list;
        }
        #endregion

        #region 分页获取摇号活动列表 活动审批列表Count

        /// <summary>
        /// 分页获取摇号活动列表 活动审批列表Count
        /// </summary>
        /// <param name="search">搜素实体</param>
        /// <returns></returns>
        public int GetYaoHaoApplyPageCount_BySearch_Admin(PVS_NH_YaoHao search)
        {
            return _yaohaoDal.GetYaoHaoApplyPageCount_BySearch_Admin(search);
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
            return _yaohaoDal.GetActivitiesYaoHaoApply_ById_Admin(id);
        }
        #endregion

        #region 添加摇号活动
        /// <summary>
        /// 添加摇号活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ESqlResult AddYaoHaoActivities_Admin(PVM_NH_YaoHao model)
        {
            return _yaohaoDal.AddYaoHaoActivities_Admin(model);
        }
        #endregion

        #region 获取PVE_NH_YaoHao

        /// <summary>
        /// 获取PVE_NH_YaoHao
        /// </summary>
        /// <returns></returns>
        public PVE_NH_YaoHao GetYaoHaoInfo(int id)
        {
            return _yaohaoDal.GetYaoHaoInfo(id);
        }
        #endregion

        #region 修改摇号活动 基本信息

        /// <summary>
        /// 修改摇号活动 基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ESqlResult UpdateYaoHaoInfo_Admin(PVE_NH_YaoHao model)
        {
            return _yaohaoDal.UpdateYaoHaoInfo_Admin(model);
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
        public List<PVM_NH_YaoHaoUsers> GetYaoHaoApplyUsersPageList_BySearch_Admin(PVS_NH_YaoHaoUsers search,int pageIndex, int pageSize)
        {
            return _yaohaoDal.GetYaoHaoApplyUsersPageList_BySearch_Admin(search,pageIndex,pageSize);
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
            return _yaohaoDal.GetYaoHaoApplyUsersPageCount_BySearch_Admin(search);
        }
        #endregion
    }
}