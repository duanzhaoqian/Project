using System;
using System.Linq;
using TXModel.Admins;
using TXOrm;

namespace TXDal.WebSite
{
    public class AdminDal
    {
        /// <summary>
        /// 查询管理员信息
        /// </summary>
        /// <param name="loginname">管理员登录名</param>
        /// <returns>管理员信息实体</returns>
        public Z_Admin Z_GetAdminByLoginName(string loginname)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {

                    var query = webEntity.Admins.Join(webEntity.Roles, admin => admin.RoleId, role => role.Id, (admin, role)
                        => new Z_Admin
                        {
                            Id = admin.Id,
                            LoginName = admin.LoginName,
                            Name = admin.Name,
                            Pwd = admin.Pwd,
                            RoleId = admin.RoleId,
                            Rate = admin.Rate,
                            RoleRate = role.RATE,
                            CreateTime = admin.CreatTime,
                            UpdateTime = admin.UpdateTime,
                            CityID = admin.CityId ?? -1,
                            IsDel = admin.IsDel
                        }).FirstOrDefault(adminUser => adminUser.LoginName == loginname && adminUser.IsDel == false);

                    if (null == query)
                    {
                        return new Z_Admin
                        {
                            Rates_Num = string.Empty
                        };
                    }

                    if (!string.IsNullOrWhiteSpace(query.RoleRate) && !string.IsNullOrWhiteSpace(query.Rate))
                        query.Rates_Num = query.RoleRate + ("," + query.Rate);
                    else if (!string.IsNullOrWhiteSpace(query.RoleRate) && string.IsNullOrWhiteSpace(query.Rate))
                        query.Rates_Num = query.RoleRate;
                    else if (string.IsNullOrWhiteSpace(query.RoleRate) && !string.IsNullOrWhiteSpace(query.Rate))
                        query.Rates_Num = query.Rate;
                    else
                        query.Rates_Num = string.Empty;
                    return query;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("LoginName:{0}", new object[] {loginname}), ex);
                return null;
            }
        }
    }
}