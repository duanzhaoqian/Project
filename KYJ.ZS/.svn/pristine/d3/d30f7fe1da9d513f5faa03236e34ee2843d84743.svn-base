using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Authority;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using System.Data;
using KYJ.ZS.Models.RolePermission;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.Authority
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-23
    /// 描述：角色权限的管理
    /// </summary>
    public class RoleModulesDal
    {
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        public List<RoleShowEntity> GetRoleInfo()
        {
            #region SQL语句
            string sqlStr = @"select role_id as Id
                                ,role_name as RoleName
                                ,RoleNum=(select count(1) from Admins(nolock) where role_id=t.role_id and admin_isdel='false')
                                from Roles(nolock) t
	                            where role_isdel='false' order by role_name";
            #endregion
            return DataHelper<RoleShowEntity>.GetEntityList(KYJ_ZSPlatformRDB.GetTable(sqlStr));
        }
        public List<Modules> GetAllModules()
        {
            #region SQL语句
            string sqlStr = "select module_id as ModuleId,module_name as ModuleName,module_remark as ModuleRemark,module_permission as Permission from Modules with(nolock) order by module_name";
            #endregion
            return DataHelper<Modules>.GetEntityList(KYJ_ZSPlatformRDB.GetTable(sqlStr));
        }
        /// <summary>
        /// 为角色添加权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int AddRolePermission(RolePermissions rolePermission)
        {
            #region Sql语句
            string sqlStr = "insert into RolePermission (role_id,module_id,rolepermission_permission,oper_id,oper_name) values (@roleid,@moduleid,@permission,@operid,@opername)";
            #endregion
            #region 参数赋值
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@roleid", rolePermission.RoleId);
            sqlParam.AddParam("@moduleid", rolePermission.ModuleId);
            sqlParam.AddParam("@permission", rolePermission.Permission);
            sqlParam.AddParam("@operid", rolePermission.OperId);
            sqlParam.AddParam("@opername", rolePermission.OperName);
            #endregion
            return KYJ_ZSPlatformWDB.SqlExecute(sqlStr, sqlParam);
        }
        /// <summary>
        /// 根据角色ID获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Roles GetRoleInfoById(int roleId)
        {
            #region SQL
            string sqlStr = "select role_id as RoleId,role_name as RoleName,role_createtime as CreateTime,role_updatetime as UpdateTime,role_isdel as Isdel, oper_id as OperId,oper_name as OperName from Roles where role_id =@id and role_isdel =0 ";
            #endregion
            #region 参数赋值
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", roleId);
            #endregion
            return DataHelper<Roles>.GetEntity(KYJ_ZSPlatformRDB.GetTable(sqlStr, sqlParam).Rows[0]);
        }
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roleinfo"></param>
        /// <returns></returns>
        public int AddRoleInfo(Roles roleinfo)
        {
            #region SQL语句
            string sqlStr = "insert into Roles (role_name,role_createtime,role_updatetime,role_isdel,oper_id,oper_name) values (@roleName,@createTime,@updateTime,@isdel,@operId,@operName)";
            #endregion
            #region 给参数赋值
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@roleName", roleinfo.RoleName, SqlDbType.NVarChar, 200);
            sqlParam.AddParam("@createTime", roleinfo.CreateTime);
            sqlParam.AddParam("@updateTime", roleinfo.UpdateTime);
            sqlParam.AddParam("@isdel", roleinfo.Isdel);
            sqlParam.AddParam("@operId", roleinfo.OperId);
            sqlParam.AddParam("@operName", roleinfo.OperName);
            #endregion
            return KYJ_ZSPlatformWDB.SqlExecuteRuturnId(sqlStr, sqlParam);
        }
        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="permissionId">权限对象</param>
        /// <returns></returns>
        public int UpdatePermission(RolePermissions permission)
        {
            #region SQL
            string sqlStr = "update RolePermissions set rolepermission_permission =@permission,oper_id =@operId,oper_name =@operName where rolepermission_id =@rolePermissionId";
            #endregion
            #region 参数赋值
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@permission", permission.Permission);
            sqlParam.AddParam("@operId", permission.OperId);
            sqlParam.AddParam("@operName", permission.OperName);
            sqlParam.AddParam("@rolePermissionId", permission.RolePermissionId);
            #endregion
            return KYJ_ZSPlatformWDB.SqlExecute(sqlStr, sqlParam);
        }

        /// <summary>
        /// Author：ningjd
        /// Time：2014-06-05
        /// Desc：添加、修改角色权限
        /// </summary>
        /// <returns></returns>
        public int CreateOrUpdate(RolePermissionEntity entity)
        {
            try
            {
                #region SQL及参数
                var sql = @"begin tran ";
                var param = new SqlParam();
                if (entity.Id.HasValue) //修改
                {
                    sql += @"declare @results int --执行结果，0失败，1成功
                            /*角色*/
                            update Roles set role_name=@role_name where role_id=@roleId
                            /*角色权限*/
                            delete from RolePermissions where role_id=@roleId ";
                    param.AddParam("@roleId", entity.Id.Value);
                }
                else //添加
                {
                    sql += @"declare @roleId int, --角色ID
			                @results int --执行结果，0失败，1成功
                            /*角色*/
                            insert into Roles(role_name,role_createtime,role_updatetime,oper_id,oper_name)
                            values(@role_name,@createtime,@createtime,@oper_id,@oper_name)
                            set @roleId=(select @@IDENTITY) ";
                    param.AddParam("@createtime", DateTime.Now);
                }
                if (entity.PermissionList != null && entity.PermissionList.Count > 0)
                {
                    foreach (var item in entity.PermissionList)
                    {
                        // 角色权限
                        sql += string.Format(@" insert into RolePermissions(role_id,module_id,rolepermission_permission,oper_id,oper_name)
                                    values(@roleId,{0},'{1}',@oper_id,@oper_name)", item.ModuleId, item.Permission);
                    }
                }
                sql += @"/*判断事务回滚或提交*/
	                        if @@error<>0 --判断有任何一条出现错误
	                        begin 
		                        rollback tran --开始执行事务的回滚
		                        set @results=0  
	                        end
	                        else
	                        begin
		                        commit tran --执行这个事务的操作
		                        set @results=1 
	                        end
                            /*返回执行结果*/
                            select  @results";

                param.AddParam("@role_name", entity.RoleName);
                param.AddParam("@oper_id", entity.OperId);
                param.AddParam("@oper_name", entity.OperName);
                #endregion
                return Auxiliary.ToInt32(KYJ_ZSPlatformWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<RolePermissionEntity>("ningjd", entity, ex);
                return 0;
            }
        }

        /// <summary>
        /// 删除角色校验
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int DeleteValidate(int roleId)
        {
            try
            {
                var sql = "select count(1) from Admins(nolock) where role_id=@role_id and admin_isdel='false'";
                var param = new SqlParam();
                param.AddParam("@role_id", roleId);
                return Auxiliary.ToInt32(KYJ_ZSPlatformRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", roleId, ex);
                return 1;
            }
        }

        /// <summary>
        /// Author：ningjd
        /// Time：2014-06-06
        /// Desc：删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int Delete(int roleId)
        {
            try
            {
                #region SQL及参数
                var sql = @"begin tran
                        declare @results int --执行结果，0失败，1成功
                        /*角色权限*/
                        delete from RolePermissions where role_id=@role_id
                        /*角色*/
                        update Roles set role_isdel='true' where role_id=@role_id
                        /*判断事务回滚或提交*/
	                    if @@error<>0 --判断有任何一条出现错误
	                    begin 
		                    rollback tran --开始执行事务的回滚
		                    set @results=0  
	                    end
	                    else
	                    begin
		                    commit tran --执行这个事务的操作
		                    set @results=1 
	                    end
                        /*返回执行结果*/
                        select  @results";
                var param = new SqlParam();
                param.AddParam("@role_id", roleId);
                #endregion
                return Auxiliary.ToInt32(KYJ_ZSPlatformWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", roleId, ex);
                return 0;
            }
        }
    }
}
