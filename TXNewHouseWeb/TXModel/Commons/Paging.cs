using System.Collections.Generic;

namespace TXModel.Commons
{
    /// <summary>
    /// 分页操作类
    /// </summary>
    /// <typeparam name="T">结果集类型</typeparam>
    public class Paging<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Paging()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页显示记录数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="resultData">结果集</param>
        public Paging(int pageIndex, int pageSize, int totalCount, IList<T> resultData)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            ResultData = resultData;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页显示记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 结果集
        /// </summary>
        public IList<T> ResultData { get; set; }
    }
}