using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Authority;
using KYJ.ZS.Models.Authority;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.RolePermission;

namespace KYJ.ZS.BLL.Authority
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-23
    /// 描述：角色权限的管理
    /// </summary>
    public class RoleModulesBll
    {
        RoleModulesDal dal = new RoleModulesDal();
        LoginFilterDal loginDal = new LoginFilterDal();
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        public List<RoleShowEntity> GetRoleInfo()
        {
            return dal.GetRoleInfo();
        }
        /// <summary>
        /// 获取所有模块的权限信息的集合
        /// </summary>
        /// <returns></returns>
        public List<Modules> GetAllMouleData()
        {
            return dal.GetAllModules();
        }
        /// <summary>
        /// 添加角色并返回角色的ID
        /// </summary>
        /// <param name="roleinfo"></param>
        /// <returns></returns>
        public int AddRoleInfo(Roles roleinfo)
        {  
           int roleId = dal.AddRoleInfo(roleinfo);
           return roleId;
 
        }
        /// <summary>
        /// 为角色添加权限
        /// </summary>
        /// <param name="moduleList">模块列表</param>
        /// <returns></returns>
        public bool AddRolePermission(List<RolePermissions> moduleList)
        {
            int result = 0;
            foreach (var item in moduleList)
            {
                result += dal.AddRolePermission(item);
            }
            if(result!=moduleList.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 根据角色ID获取所有权限集合
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleManagement> GetPermissionByRoleId(int roleId)
        {
           return loginDal.GetAuthority(roleId);
        }
        /// <summary>
        /// 根据角色Id取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Roles GetRoleInfoByRoleId(int roleId)
        {
            return dal.GetRoleInfoById(roleId);
        }
        public bool BatchUpadte(List<RolePermissions> list)
        {
            int result = 0;
            foreach (var item in list)
            {
                result +=dal.UpdatePermission(item);
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        

        /// <summary>
        /// Author：ningjd
        /// Time：2014-06-05
        /// Desc：添加、修改角色权限
        /// </summary>
        /// <returns></returns>
        public bool CreateOrUpdate(RolePermissionEntity entity)
        {
            return dal.CreateOrUpdate(entity) > 0;
        }

        /// <summary>
        /// 删除角色校验
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteValidate(int roleId)
        {
            return dal.DeleteValidate(roleId) <= 0;
        }

        /// <summary>
        /// Author：ningjd
        /// Time：2014-06-06
        /// Desc：删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool Delete(int roleId)
        {
            return dal.Delete(roleId) > 0;
        }
    }
}
