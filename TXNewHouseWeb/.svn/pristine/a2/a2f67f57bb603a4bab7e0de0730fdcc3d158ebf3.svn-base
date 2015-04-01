using System;
using System.Collections.Generic;
using System.Linq;
using TXModel.AdminLogs;
using TXOrm;

namespace TXDal.WebSite
{
    public class AdminLogDal
    {
        /// <summary>
        /// 获取所有日志信息
        /// </summary>
        /// <returns>日志信息列表</returns>
        public List<Z_AdminLog> Z_GetAdminLogs()
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    var query = (from log in webEntity.AdminLogs
                        join admin in webEntity.Admins on log.AdminId equals admin.Id into a
                        from admins in a.DefaultIfEmpty()
                        select new {log, admins.Name}).ToList().ConvertAll(it => new Z_AdminLog
                        {
                            OperAdminName = Convert.ToString(it.Name),
                            OperTime = it.log.CreateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                            OperDes = Convert.ToString(it.log.Desc),
                            OperIP = Convert.ToString(it.log.IpAddress)
                        });
                    return query;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", null, ex);
                return null;
            }
        }

        /// <summary>
        /// 分页获取日志信息
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">每页显示的条数</param>
        /// <returns>日志信息列表</returns>
        public List<Z_AdminLog> Z_GetAdminLogsByPage(string keyWord, int pageIndex, int pageSize)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    var query = from log in webEntity.AdminLogs
                        join admin in webEntity.Admins on log.AdminId equals admin.Id into a
                        from admins in a.DefaultIfEmpty()
                        select new {log, admins.Name};
                    if (!String.IsNullOrWhiteSpace(keyWord))
                    {
                        query = query.Where(it => it.log.Desc.Contains(keyWord));
                    }
                    return query.OrderByDescending(it => it.log.CreateTime).Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList().ConvertAll(
                        it => new Z_AdminLog
                        {
                            OperAdminName = Convert.ToString(it.Name),
                            OperTime = it.log.CreateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                            OperDes = Convert.ToString(it.log.Desc),
                            OperIP = Convert.ToString(it.log.IpAddress)
                        });
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("keyWord:{0} pageIndex:{1} pageSize:{2}", keyWord, pageIndex, pageSize), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据关键词获取日志数量
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <returns>日志数量</returns>
        public int Z_GetAdminLogsByPageCount(string keyWord)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    if (!string.IsNullOrWhiteSpace(keyWord))
                    {
                        return webEntity.AdminLogs.Count(p => p.Desc.Contains(keyWord));
                    }
                    return webEntity.AdminLogs.Count();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("keyWord:{0}", keyWord), ex);
                return 0;
            }
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="operAdminLog">创建日志操作实体</param>
        public void Create(Z_OperAdminLog operAdminLog)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    var log = new AdminLog
                    {
                        Type = Convert.ToByte(operAdminLog.Type),
                        Desc = operAdminLog.OperDes,
                        CreateTime = DateTime.Now,
                        AdminId = operAdminLog.AdminID,
                        IpAddress = operAdminLog.OperIP
                    };
                    webEntity.AdminLogs.AddObject(log);
                    webEntity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", operAdminLog, ex);
            }
        }
    }
}