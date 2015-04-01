using TXDal.WebSite;
using TXModel.Admins;

namespace TXBll.WebSite
{
    public class AdminBll
    {
        private AdminDal _adminDal;

        /// <summary>
        /// 查询管理员信息
        /// </summary>
        /// <param name="loginname">管理员登录名</param>
        /// <returns>管理员信息实体</returns>
        public Z_Admin Z_GetAdminByLoginName(string loginname)
        {
            _adminDal = new AdminDal();
            return _adminDal.Z_GetAdminByLoginName(loginname);
        }
    }
}