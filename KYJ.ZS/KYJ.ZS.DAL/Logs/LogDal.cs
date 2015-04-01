using System;
using System.Collections.Generic;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Logs;

namespace KYJ.ZS.DAL.Logs
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-19
    /// Desc：操作日志(数据库表Logs)
    /// </summary>
    public class LogDal
    {
        /// <summary>
        /// 平台管理中心日志显示
        /// </summary>
        /// <param name="adminId">当前账户ID</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<LogIndexEntity> GetLogList(int adminId, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                // 表名
                string tableName = "Logs(NOLOCK) t1 join Admins(NOLOCK) t2 on t1.admin_id=t2.admin_id join Roles(NOLOCK) as t3 on t2.role_id=t3.role_id";
                // 查询条件
                string where = " t1.log_isdel='false' and t2.admin_id=" + adminId;

                //排序
                string orderBy = " t1.log_createtime desc";
                //列名
                string columnList = "t1.log_createtime as CreateTime,t2.admin_name as LoginName,t2.admin_realname as RealName" +
                    ",t3.role_name as RoleName,t1.module_name as ModuleName,t1.log_remark as Remark";

                DataTable dt = KYJ_ZSPlatformRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<LogIndexEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalPage = 0;
                totalRecord = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", adminId + "," + index + "," + pageSize + "," + totalRecord + "," + totalPage, ex);
                return null;
            }
        }

        /// <summary>
        /// 日志查询
        /// </summary>
        /// <param name="entity">搜索条件Entity</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<LogIndexEntity> GetLogList(LogSearchEntity entity, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                // 表名
                string tableName = "Logs(NOLOCK) t1 join Admins(NOLOCK) t2 on t1.admin_id=t2.admin_id join Roles(NOLOCK) as t3 on t2.role_id=t3.role_id";
                // 查询条件
                string where = " t1.log_isdel='false'";
                // 登录账号
                if (!string.IsNullOrEmpty(entity.LoginName))
                    where += " and t2.admin_name like '%" + entity.LoginName + "%'";
                // 职员姓名
                if (!string.IsNullOrEmpty(entity.StaffName))
                    where += " and t2.admin_realname like '%" + entity.StaffName + "%'";
                // 权限角色
                if (entity.RoleId > 0)
                    where += " and t2.role_id=" + entity.RoleId;
                // 模块
                if (!string.IsNullOrEmpty(entity.ModuleIds))
                    where += " and t1.module_id in(" + entity.ModuleIds + ")";

                //排序
                string orderBy = " t1.log_createtime desc";
                //列名
                string columnList = "t1.log_createtime as CreateTime,t2.admin_name as LoginName,t2.admin_realname as RealName" +
                    ",t3.role_name as RoleName,t1.module_name as ModuleName,t1.log_remark as Remark";

                DataTable dt = KYJ_ZSPlatformRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<LogIndexEntity>.GetEntityList(dt);
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
        /// 添加日志
        /// </summary>
        /// <param name="entity">日志添加Entity</param>
        /// <returns></returns>
        public int CreateLog(LogCreateEntity entity)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[Logs]
                            ([admin_id]
                            ,[admin_name]
                            ,[admin_realname]
                            ,[module_id]
                            ,[module_name]
                            ,[log_remark]
                            ,[log_createtime])
                        VALUES
                            (@admin_id
                            ,@admin_name
                            ,@admin_realname
                            ,@module_id
                            ,@module_name
                            ,@log_remark
                            ,@log_createtime)";
                var param = new SqlParam();
                param.AddParam("@admin_id", entity.AdminId);
                param.AddParam("@admin_name", entity.Name);
                param.AddParam("@admin_realname", entity.RealName);
                param.AddParam("@module_id", entity.ModuleId);
                param.AddParam("@module_name", entity.ModuleName);
                param.AddParam("@log_remark", entity.Remark);
                param.AddParam("@log_createtime", DateTime.Now);

                return KYJ_ZSPlatformWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return 0;
            }
        }
    }
}
