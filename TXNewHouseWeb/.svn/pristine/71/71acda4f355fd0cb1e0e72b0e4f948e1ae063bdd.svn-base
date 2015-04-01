using System.Collections.Generic;
using TXDal.WebSite;
using TXModel.AdminLogs;

namespace TXBll.WebSite
{
    public class AdminLogBll
    {
        private AdminLogDal _adminLogDal;

        /// <summary>
        /// 获取所有日志信息
        /// </summary>
        /// <returns>日志信息列表</returns>
        public List<Z_AdminLog> Z_GetAdminLogs()
        {
            _adminLogDal = new AdminLogDal();
            return _adminLogDal.Z_GetAdminLogs();
        }

        /// <summary>
        /// 分页获取日志信息
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <param name="pageindex">当前第几页</param>
        /// <param name="pagesize">每页显示的条数</param>
        /// <returns>日志信息列表</returns>
        public List<Z_AdminLog> Z_GetAdminLogsByPage(string keyWord, int pageindex, int pagesize)
        {
            _adminLogDal = new AdminLogDal();
            return _adminLogDal.Z_GetAdminLogsByPage(keyWord, pageindex, pagesize);
        }

        /// <summary>
        /// 根据关键词获取日志数量
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <returns>日志数量</returns>
        public int Z_GetAdminLogsByPageCount(string keyWord)
        {
            _adminLogDal = new AdminLogDal();
            return _adminLogDal.Z_GetAdminLogsByPageCount(keyWord);
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="operAdminLog">创建日志操作实体</param>
        public void Create(Z_OperAdminLog operAdminLog)
        {
            _adminLogDal = new AdminLogDal();
            _adminLogDal.Create(operAdminLog);
        }
    }
}