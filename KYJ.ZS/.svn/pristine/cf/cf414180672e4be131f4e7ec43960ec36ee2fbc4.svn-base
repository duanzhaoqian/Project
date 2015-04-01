using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Pages
{
    /// <summary>
    /// 时间:2014年4月18日
    /// 作者:maq
    /// 描述:分页类，用途dal向bll层返回分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T> where T : new()
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="dataList">当前页数据</param>
        public PageData(int pageSize, int pageCount, int recordCount, int pageIndex, List<T> dataList)
        {
            PageSize = pageSize;
            PageCount = pageCount;
            RecordCount = recordCount;
            PageIndex = pageIndex;
            DataList = dataList;
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pageCount">分页总数</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageIndex">当前页码</param>
        public PageData(int pageCount, int recordCount, int pageIndex,int pageSize)
            : this(pageSize, pageCount, recordCount, pageIndex, new List<T>())
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
        private List<T> _dataList =new List<T>();
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
