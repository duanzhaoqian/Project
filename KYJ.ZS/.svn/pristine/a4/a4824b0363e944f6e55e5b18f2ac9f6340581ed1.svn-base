using System;
using System.Collections.Generic;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Role;

namespace KYJ.ZS.DAL.Role
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-04
    /// Desc：操作角色表
    /// </summary>
    public class RoleDal
    {
        /// <summary>
        /// 获取模块下拉列表集合
        /// </summary>
        /// <returns></returns>
        public IList<RoleSelectEntity> GetSelectRoles()
        {
            try
            {
                var sql = "select role_id as RoleCode,role_name as RoleName from Roles(NOLOCK) where role_isdel='false'";
                DataTable dt = KYJ_ZSPlatformRDB.GetTable(sql);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<RoleSelectEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }
    }
}
