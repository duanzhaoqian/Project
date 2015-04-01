using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Authority;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Admin;

namespace KYJ.ZS.DAL.Authority
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-22
    /// 描述：职员管理
    /// </summary>
    public class AdminsDal
    {
        /// <summary>
        /// 获取职员信息
        /// </summary>
        /// <returns></returns>
        public IList<AdminsEntity> GetPageData(AdminSearchEntity adminSearchEntity, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                #region 给分页属性对象PagePmsDal赋值

                // 表名
                string tableName = "Admins(nolock) as a right join Roles(nolock) as r on a.role_id = r.role_id";
                // 查询条件
                string where = " a.admin_isdel ='false'";

                //排序
                string orderBy = " a.admin_Name";
                //列名
                string columnList = "a.admin_id as AdminId,a.admin_name as AdminName,a.admin_realname as RealName,r.role_name as RoleName,a.admin_mobile as Mobile,a.admin_lastlogintime as LastLoginTime,a.admin_pwd as PassWord,a.role_id as RoleId";

                //拼接查询条件
                if (!string.IsNullOrEmpty(adminSearchEntity.AdminName))
                {
                    where += " and a.admin_name like '%" + adminSearchEntity.AdminName + "%' ";
                }
                if (!string.IsNullOrEmpty(adminSearchEntity.RealName))
                {
                    where += " and a.admin_realname like '%" + adminSearchEntity.RealName + "%' ";
                }
                if (!string.IsNullOrEmpty(adminSearchEntity.Mobile))
                {
                    where += " and a.admin_mobile like '%" + adminSearchEntity.Mobile + "%' ";
                }
                if (adminSearchEntity.RoleId > 0)
                {
                    where += " and r.role_id =" + adminSearchEntity.RoleId;
                }
                #endregion
                DataTable dt = KYJ_ZSPlatformRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<AdminsEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalPage = 0;
                totalRecord = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }
        /// <summary>
        /// 获取角色下拉选框的内容
        /// </summary>
        /// <returns>下拉选框内容的集合</returns>
        public List<string> GetRoleDropDownList()
        {
            List<string> list = new List<string>();
            #region Sql语句
            string sqlStr = "select role_name from Roles with(nolock) where role_isdel = 0";
            #endregion
            return ConvertDB.GetStringList(KYJ_ZSPlatformRDB.GetTable(sqlStr));
        }

        /// <summary>
        /// 
        /// 添加职员信息
        /// </summary>
        /// <param name="admin">职员信息</param>
        /// <returns></returns>
        public int AddAdmin(AdminCreateEntity admin)
        {
            try
            {
                #region SQL及参数
                string sqlStr = "";
                SqlParam sqlParam = new SqlParam();
                if (admin.AdminId.HasValue)
                {
                    sqlStr = "update Admins set role_id=@role_id,admin_name=@admin_name,admin_pwd=@admin_pwd,admin_mobile=@admin_mobile,admin_realname=@admin_realname,admin_updatetime=@updatetime,oper_id=@oper_id,oper_name=@oper_name where admin_id=@admin_id";
                    sqlParam.AddParam("@admin_id", admin.AdminId.Value);
                }
                else
                {
                    sqlStr = "insert into Admins (role_id,admin_name,admin_pwd,admin_mobile,admin_realname,admin_createtime,admin_updatetime,admin_lastlogintime,oper_id,oper_name) values (@role_id,@admin_name,@admin_pwd,@admin_mobile,@admin_realname,@updatetime,@updatetime,@updatetime,@oper_id,@oper_name)";
                }
                sqlParam.AddParam("@role_id", admin.RoleId);
                sqlParam.AddParam("@admin_name", admin.AdminName);
                sqlParam.AddParam("@admin_pwd", admin.PassWord);
                sqlParam.AddParam("@admin_mobile", admin.Mobile);
                sqlParam.AddParam("@admin_realname", admin.RealName);
                sqlParam.AddParam("@updatetime", DateTime.Now);
                sqlParam.AddParam("@oper_id", admin.OperId);
                sqlParam.AddParam("@oper_name", admin.OperName);
                #endregion
                return KYJ_ZSPlatformWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<AdminCreateEntity>("ningjd", admin, ex);
                return 0;
            }
        }
        /// <summary>
        /// 账户名称校验
        /// </summary>
        /// <param name="oldName"></param>
        /// <returns></returns>
        public int ValidateLoginName(string name)
        {
            try
            {
                var sql = "select count(1) from Admins(nolock) where admin_name=@admin_name and admin_isdel='false'";
                var param = new SqlParam();
                param.AddParam("@admin_name", name);
                return Auxiliary.ToInt32(KYJ_ZSPlatformRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", name, ex);
                return 1;
            }
        }
        /// <summary>
        /// 根据职员ID获取职员信息
        /// </summary>
        /// <param name="adminId">管理员ID</param>
        /// <returns></returns>
        public AdminsEntity GetAdminsEntity(int adminId)
        {
            #region SQL语句
            string sqlStr = "select a.admin_id as AdminId,r.role_name as RoleName,r.role_id as RoleId,a.admin_name as AdminName,a.admin_pwd as PassWord,a.admin_mobile as Mobile,a.admin_realname as RealName,a.admin_createtime as CreateTime,a.admin_updatetime as UpdateTime,a.admin_lastlogintime as LastLoginTime,a.admin_isdel as Isdel,a.oper_id as OperId,a.oper_name as OperName from Admins as a inner join Roles as r on a.role_id = r.role_id where a.admin_id =@id and a.admin_isdel =0";
            #endregion
            #region 参数赋值
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", adminId);
            #endregion
            AdminsEntity adminsEntity = null;
            using (DataTable dt = KYJ_ZSPlatformRDB.GetTable(sqlStr, sqlParam))
            {
                if (dt != null)
                {
                    adminsEntity = new AdminsEntity();
                    adminsEntity = DataHelper<AdminsEntity>.GetEntity(dt.Rows[0]);
                }
            }
            return adminsEntity;
        }
        /// <summary>
        /// 根据管理员ID去修改管理员信息
        /// </summary>
        /// <param name="admin">管理员信息</param>
        /// <returns>是否修改成功</returns>
        public int UpdateAdminById(Admins admin)
        {
            #region SQL语句
            string sqlStr = "update Admins set admin_name =@adminName,admin_pwd =@pwd,admin_realname =@realName,admin_mobile =@mobile,role_id =@roleId,admin_updatetime =@updateTime,oper_id =@operID,oper_name =@operName where admin_id =@amdinId";
            #endregion
            #region 参数赋值
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@adminName", admin.AdminName, SqlDbType.NVarChar, 50);
            sqlParam.AddParam("@pwd", admin.PassWord, SqlDbType.NVarChar, 20);
            sqlParam.AddParam("@realName", admin.RealName, SqlDbType.NVarChar, 20);
            sqlParam.AddParam("@mobile", admin.Mobile, SqlDbType.NVarChar, 50);
            sqlParam.AddParam("@roleId", admin.RoleId);
            sqlParam.AddParam("@updateTime", admin.UpdateTime);
            sqlParam.AddParam("@operId", admin.OperId);
            sqlParam.AddParam("@operName", admin.OperName);
            sqlParam.AddParam("@adminId", admin.AdminId);
            #endregion
            return KYJ_ZSPlatformWDB.SqlExecute(sqlStr, sqlParam);
        }

        /// <summary>
        /// 删除职员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            try
            {
                var sql = "update Admins set admin_isdel='true' where admin_id=@admin_id";
                var param = new SqlParam();
                param.AddParam("@admin_id", id);
                return KYJ_ZSPlatformWDB.SqlExecute(sql, param);
            }
            catch(Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return 0;
            }
        }

        /// <summary>
        /// 根据登录账号获取职员ID
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int GetAdminId(string loginName)
        {
            try
            {
                var sql = "select admin_id from Admins(nolock) where admin_name=@admin_name";
                var param = new SqlParam();
                param.AddParam("@admin_name", loginName);
                return Auxiliary.ToInt32(KYJ_ZSPlatformRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", loginName, ex);
                return 0;
            }
        }

        /// <summary>
        /// 根据登录账号获取职员真实姓名
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public string GetRealName(string loginName)
        {
            try
            {
                var sql = "select admin_realname from Admins(nolock) where admin_name=@admin_name";
                var param = new SqlParam();
                param.AddParam("@admin_name", loginName);
                return KYJ_ZSPlatformRDB.GetFirst(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", loginName, ex);
                return "";
            }
        }
    }
}
