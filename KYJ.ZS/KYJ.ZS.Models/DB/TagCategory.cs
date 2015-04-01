using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-4-29
    /// 描述：标签分类实体类
    /// </summary>
    public class TagCategory
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// 标签包含的分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 分类级别
        /// </summary>
        public int CategoryLevel { get; set; }

        /// <summary>
        /// 标签包含的分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
