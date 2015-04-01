using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014-04-17
    /// 描述：商品类目实体类
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 节点深度,根节点为0
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 分类类型0 未知，1 租 ，2 售
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
