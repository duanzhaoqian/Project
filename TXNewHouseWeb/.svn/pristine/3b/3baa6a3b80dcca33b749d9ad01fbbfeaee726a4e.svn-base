using System.Collections.Generic;
using TXDal.Dev;
using TXModel.Commons;

namespace TXBll.Dev
{
    /// <summary>
    /// 开发商账户操作日志
    /// </summary>
    public partial class Developer_AccountLogBll
    {
        private readonly Developer_AccountLogDal _developerAccountLogDal = new Developer_AccountLogDal();

        #region 通用方法

        /// <summary>
        /// 添加开发商账户操作日志
        /// </summary>
        /// <param name="entity">开发商账户操作日志实体</param>
        /// <returns>返回：新添加开发商账户操作日志编号</returns>
        public int Add(object entity)
        {
            return _developerAccountLogDal.Add(entity);
        }

        /// <summary>
        /// 更新开发商账户操作日志编号更新开发商账户操作日志信息
        /// </summary>
        /// <param name="entity">开发商账户操作日志实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return _developerAccountLogDal.Update_ById(entity);
        }

        /// <summary>
        /// 逻辑删除开发商账户操作日志信息
        /// </summary>
        /// <param name="id">开发商账户操作日志编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            return _developerAccountLogDal.Del_ById(id);
        }

        /// <summary>
        /// 根据编号获取开发商账户操作日志实体
        /// </summary>
        /// <param name="id">开发商账户操作日志编号</param>
        /// <returns>返回：开发商账户操作日志实体</returns>
        public object GetEntity_ById(int id)
        {
            return _developerAccountLogDal.GetEntity_ById(id);
        }

        /// <summary>
        /// 根据编号集合获取实体列表
        /// </summary>
        /// <param name="ids">编号集合字符串(逗号分隔)</param>
        /// <returns>返回：列表</returns>
        public List<object> GetList_ByIds(string ids)
        {
            return _developerAccountLogDal.GetList_ByIds(ids);
        }

        /// <summary>
        /// 根据搜素条件获取分页列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public Paging<object> GetPageList_BySearch(object search, int pageIndex, int pageSize)
        {
            var paging = _developerAccountLogDal.GetPageList_BySearch(search, pageIndex, pageSize);
            return paging;
        }

        #endregion
    }
}