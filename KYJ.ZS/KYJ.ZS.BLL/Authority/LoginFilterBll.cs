using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.Authority;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Models.Authority;

namespace KYJ.ZS.BLL.Authority
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-21
    /// 描述：登录权限过滤
    /// </summary>
   public class LoginFilterBll
    {
       LoginFilterDal dal = new LoginFilterDal();
       /// <summary>
       /// 获取权限信息的集合
       /// </summary>
       /// <param name="roleId">角色ID</param>
       /// <returns>权限集合</returns>
       public List<RoleManagement> GetAuthority(int roleId)
       {
           List<int> listModule = new List<int>();
           List<RoleManagement> list = new List<RoleManagement>();
           list = dal.GetAuthority(roleId);
           for (int i = 0; i < list.Count; i++)
           {
               if (!list[i].Permission.Contains("4"))
               {
                   list.RemoveAt(i);
               }
           }
           return list;
       }
       /// <summary>
       /// 登出
       /// </summary>
       /// <param name="userGuid"></param>

       public void LoginOut(string userGuid)
       {
           var cookieName = Auxiliary.ConfigKey("cookie_name");
           CookieTool.RemoveCookie(cookieName);
           RedisTool.RemoveLoginUserInfo(userGuid, UserInfoType.MANAGERLOGIN);
       }
       /// <summary>
       /// 获取登录权限
       /// </summary>
       /// <param name="roleId"></param>
       /// <returns></returns>
       internal List<RoleManagerEntity> GetRoleManagerEntity(int roleId)
       {
           List<RoleManagement> listMaangement = GetAuthority(roleId);
           RoleManagerEntity role = null;
           List<RoleManagerEntity> listManagerEntity = new List<RoleManagerEntity>(listMaangement.Count);
           for (int i = 0; i < listMaangement.Count; i++)
           {
               role = new RoleManagerEntity();
               role.ModuleId = listMaangement[i].ModuleId;
               role.RoleId = listMaangement[i].RoleId;
               role.Permission = listMaangement[i].Permission;
               listManagerEntity.Add(role);
           }
           return listManagerEntity;
       }
    }
}
