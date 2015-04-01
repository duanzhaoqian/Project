using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Common
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-22
    /// 描述：导航栏模型
    /// </summary>
    public class NavigationEntity
    {
        /// <summary>
        /// 导航栏类型：0租 1售
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 导航栏最后位置内容
        /// </summary>
        public string Content { get; set; }
    }
}
