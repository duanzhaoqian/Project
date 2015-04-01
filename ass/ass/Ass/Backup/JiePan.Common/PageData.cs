using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiePan.Common
{
    public class PageData<T>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public PageData()
        {
        }
        private int _pageSize;
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        private int _pageIndex;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 当前页数据
        /// </summary>
        public int IndexCount
        {
            get { return DataList.Count; }
        }

        private int _recordCount;
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
        private int _pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        private List<T> _dataList = new List<T>();
        /// <summary>
        /// 当前页数据
        /// </summary>
        public List<T> DataList
        {
            get { return _dataList; }
            set { _dataList = value; }
        }
    }
}
