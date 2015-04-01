using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.Index
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-14
    /// 描述：闲置物品条件对象
    /// </summary>
    public class SaleGoodsConditionInfo
    {
        private int _pageSize = 10;

        public int PageIndex { get; set; }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }


        /// <summary>
        /// 售价排序 0 时间 1 升序 2 降序
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// 是否讲价
        /// </summary>
        public bool Isbargain { get; set; }

        /// <summary>
        /// 是否全新
        /// </summary>
        public bool IsNew { get; set; }
    }
}
