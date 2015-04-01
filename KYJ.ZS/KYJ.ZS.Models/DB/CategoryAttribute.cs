using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014-04-17
    /// 描述：分类属性关系实体类
    /// </summary>
    public class CategoryAttribute
    {
        public int Id { get; set; }

        /// <summary>
        /// 类目ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 属性ID
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; } 
    }
}
