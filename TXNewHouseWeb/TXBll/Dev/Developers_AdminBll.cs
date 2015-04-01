using System.Collections.Generic;
using TXModel.AdminPVM;
using TXModel.Commons;

namespace TXBll.Dev
{
    /// <summary>
    /// 开发商 (网站管理平台)
    /// </summary>
    public partial class DevelopersBll
    {
        #region 根据搜索条件获取开发商表信息

        /// <summary>
        /// 根据搜索条件获取开发商表信息
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Developer> GetPageList_BySearch_Admin(PVS_NH_Developer search, int pageIndex, int pageSize)
        {
            var list = _developersDal.GetPageList_BySearch_Admin(search, pageIndex, pageSize);
            foreach (PVM_NH_Developer m in list)
            {
                m.StateStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Developer.Fc_ById.For_State(m.State);
                m.TypeStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Developer.Fc_ById.For_TypeState(m.Type);
                m.LockStateStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Developer.Fc_ById.For_LockState(m.LockState);
            }
            return list;
        }

        #endregion

        #region 根据搜索条件获取开发商表信息记录数

        /// <summary>
        /// 根据搜索条件获取开发商表信息记录数
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_Developer search)
        {
            return _developersDal.GetPageCount_BySearch_Admin(search);
        }

        #endregion

        #region 根据开发商id获取实体

        /// <summary>
        /// 根据开发商id获取实体
        /// author:huhangfei
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PVM_NH_Developer GetDevelopersById(int id)
        {
            var model = _developersDal.GetDevelopersById(id);
            model.TypeStr = TXCommons.Admins.AdminTypes.Db_NewHouse.Tb_Developer.Fc_ById.For_TypeState(model.Type);
            return model;
        }

        #endregion

        #region 根据开发商id修改开发商状态

        /// <summary>
        /// 根据开发商id修改开发商状态
        /// author:huhangfei
        /// </summary>
        /// <param name="id">开发商id</param>
        /// <param name="state">0未认证 1已通过 2未通过</param>
        /// <param name="refuse">拒绝原因</param>
        /// <param name="adminId"></param>
        /// <param name="adminName"></param>
        /// <returns></returns>
        public ESqlResult UpdateDevelopersStateById(int id, int state, string refuse, int adminId, string adminName)
        {
            return _developersDal.UpdateDevelopersStateById(id, state, refuse, adminId, adminName);
        }

        #endregion

        #region 根据关键字搜索开放商列表

        /// <summary>
        /// 根据关键字搜索开放商列表
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public List<PVM_NH_Developer> GetDevelopersByKeyWord(string key)
        {
            return _developersDal.GetDevelopersByKeyWord(key);
        }

        #endregion
    }
}