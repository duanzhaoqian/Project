using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Pages
{
    /// <summary>
    /// 时间:2014年4月18日
    /// 作者:maq
    /// 描述:分页参数类 
    /// </summary>
    public class PagePms
    {
        private int _pageIndex;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
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

        private string _strWhere;
        /// <summary>
        /// 查询条件
        /// </summary>
        public string StrWhere
        {
            get { return _strWhere; }
            set { _strWhere = value; }
        }

        private string _sortColnum;
        /// <summary>
        /// 排序列
        /// </summary>
        public string SortColnum
        {
            get { return _sortColnum; }
            set { _sortColnum = value; }
        }
    }
}
