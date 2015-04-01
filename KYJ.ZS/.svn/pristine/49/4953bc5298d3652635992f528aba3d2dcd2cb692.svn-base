using System.Collections.Generic;
using KYJ.ZS.DAL.Logs;
using KYJ.ZS.Models.Logs;

namespace KYJ.ZS.BLL.Logs
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-19
    /// Desc：操作日志
    /// </summary>
    public class LogBll
    {
        private LogDal dal = new LogDal();


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
            return dal.GetLogList(adminId, index, pageSize, out totalRecord, out totalPage);
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
            return dal.GetLogList(entity, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="entity">日志添加Entity</param>
        /// <returns></returns>
        public bool CreateLog(LogCreateEntity entity)
        {
            return dal.CreateLog(entity) > 0;
        }
    }
}
