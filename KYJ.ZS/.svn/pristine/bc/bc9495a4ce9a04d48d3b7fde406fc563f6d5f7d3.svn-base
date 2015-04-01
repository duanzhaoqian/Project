using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Authority;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.Authority;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Admin;

namespace KYJ.ZS.BLL.Authority
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-22
    /// 描述：职员管理
    /// </summary>
    public class AdminsBll
    {
        //声明一个管理员信息的对象
        AdminsDal dal = new AdminsDal();
        /// <summary>
        /// 根据搜索条件获得当前页的管理员信息数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        ///  <param name="adminSearchEntity">搜索条件</param>
        /// <returns>当前页数据</returns>
        public IList<AdminsEntity> GetPageData(AdminSearchEntity adminSearchEntity, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetPageData(adminSearchEntity, index, pageSize, out totalRecord, out totalPage);
        }
        /// <summary>
        /// 获取下拉选框的内容
        /// </summary>
        /// <returns></returns>
        public List<string> GetRoleDropDownList()
        {
            return dal.GetRoleDropDownList();
        }
        /// <summary>
        /// 添加职员信息
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public bool AddAdmin(AdminCreateEntity admin)
        {
            return dal.AddAdmin(admin) > 0;
        }
        /// <summary>
        /// 账户名称校验
        /// </summary>
        /// <param name="oldName"></param>
        /// <returns></returns>
        public bool ValidateLoginName(string name)
        {
            return dal.ValidateLoginName(name) <= 0;
        }
        /// <summary>
        /// 根据职员ID获取职员信息
        /// </summary>
        /// <param name="adminId">职员ID</param>
        /// <returns></returns>
        public AdminsEntity GetAdminsEntity(int adminId)
        {
            return dal.GetAdminsEntity(adminId);
        }
        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public bool UpdateAdminById(Admins admin)
        {

            if (dal.UpdateAdminById(admin) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除职员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return dal.Delete(id) > 0;
        }

        /// <summary>
        /// 根据登录账号获取职员ID
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int GetAdminId(string loginName)
        {
            return dal.GetAdminId(loginName);
        }

        /// <summary>
        /// 根据登录账号获取职员真实姓名
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public string GetRealName(string loginName)
        {
            return dal.GetRealName(loginName);
        }
    }
}
