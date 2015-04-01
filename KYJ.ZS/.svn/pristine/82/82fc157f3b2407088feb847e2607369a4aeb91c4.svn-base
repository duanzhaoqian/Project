using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.DAL.Manager
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-21
    /// 描述：登录
    /// </summary>
   public class ManagerDal
    {
       /// <summary>
       /// 根据登录名获取管理员实体类
       /// </summary>
       /// <param name="adminName">登录名</param>
       /// <returns>管理员信息</returns>
       public Admins GetAdmin(string adminName)
       {
           Admins admin = null;
           #region SQL语句
           string sqlStr = @"select admin_id as AdminId,role_id as RoleId,admin_name as AdminName,admin_pwd as PassWord,admin_mobile as Mobile,admin_realname as RealName,admin_updatetime as UpdateTime,admin_lastlogintime as LastLoginTime,admin_isdel as Isdel,oper_id as OperId,oper_name as OperName 
                            from Admins with(nolock) where admin_name = @name and admin_isdel = 0";
           #endregion
           #region 参数赋值
           SqlParam sqlParam = new SqlParam();
           sqlParam.AddParam("@name",adminName);
           #endregion
           using(DataTable dataTable = KYJ_ZSPlatformRDB.GetTable(sqlStr,sqlParam))
           {
               if(dataTable!=null)
               {
                   admin = new Admins();
                  admin = DataHelper<Admins>.GetEntity(dataTable.Rows[0]);
               }
           }
           return admin;
       }

       /// <summary>
       /// 更新最后登录时间
       /// </summary>
       /// <param name="adminId">管理员ID</param>
       public int UpDatelastLoginTime(int adminId)
       {
       
            #region Sql
            string strSql = "update Admins set admin_lastlogintime=@t where admin_id =@id";
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@t", DateTime.Now);
            sqlParam.AddParam("@id", adminId);
            #endregion
            return KYJ_ZSPlatformWDB.SqlExecute(strSql, sqlParam);
        }
           
    }
}
