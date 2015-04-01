using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;

namespace KYJ.ZS.DAL.Authority
{
    //作者：孟国栋
    //时间：2014-05-19
    //描述：登录的权限过滤
    /// <summary>
    /// 登录的权限过滤
    /// </summary>
   public class LoginFilterDal
    {
       /// <summary>
       /// 获取角色下的所有权限集合
       /// </summary>
       /// <param name="roleId">角色Id</param>
       /// <returns>模块权限的集合</returns>
       public List<RoleManagement> GetAuthority(int roleId)
       {
           #region SQL语句
           string sqlStr = "select rolepermission_id as Id,role_id as RoleId,module_id as ModuleId,rolepermission_permission as Permission,oper_id as OperId,oper_name as OperName from RolePermissions with(nolock) where role_id =@roleId";
           #endregion
           #region 参数赋值
           SqlParam sqlParam = new SqlParam();
           sqlParam.AddParam("@roleId",roleId);
           #endregion
           return DataHelper<RoleManagement>.GetEntityList(KYJ_ZSPlatformRDB.GetTable(sqlStr, sqlParam));
       }
    }
}
