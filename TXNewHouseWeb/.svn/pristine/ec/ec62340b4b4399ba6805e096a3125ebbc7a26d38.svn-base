
using TXModel.AdminPVM;

namespace TXBll.Dev
{
    /// <summary>
    /// 开发商账号 (网站管理平台)
    /// </summary>
    public partial class Developer_AccountBll
    {

        #region 开发商账号状态锁定(解锁)操作
        /// <summary>
        /// 开发商账号状态锁定(解锁)操作
        /// Author:wangzhikun
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">状态</param>
        public void Locked(int id, int state)
        {
            _developerAccountDal.Locked(id, state);
        }
        #endregion
        #region 开发商帐号推荐（取消推荐）操作
        /// <summary>
        /// 开发商帐号推荐（取消推荐）操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restate"></param>
        public void Recommend(int id, int restate)
        {
            _developerAccountDal.Recommend(id, restate);
        }

        #endregion
        #region 修改开发商资料

        /// <summary>
        /// 修改开发商资料
        /// </summary>
        /// <param name="operDev">实体</param>
        /// <param name="tabType">tab选项卡(1基本资料 2头像  3密码 4身份认证)</param> 
        public bool Update(PVM_NH_Developer operDev, int tabType)
        {
            return _developerAccountDal.Update(operDev, tabType);
        }
        #endregion

        #region 修改开发商身份认证信息
        /// <summary>
        /// 修改开发商身份认证信息
        /// </summary>
        /// <param name="operDev">实体</param>
        public bool UpdateIdentity(PVM_NH_Developer operDev)
        {
            return _developerAccountDal.UpdateIdentity(operDev);
        }
        #endregion

        #region 根据开发商id获取实体
        /// <summary>
        /// 根据开发商id获取实体
        /// author:wangzhikun
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TXOrm.Developer GetDevelopersById(int id)
        {
            return _developerAccountDal.GetDevelopersById(id);
        }
        #endregion

    }
}