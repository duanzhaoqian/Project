using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-4-29
    /// 描述：标签实体类
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 分类名
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 标签类型：租，售
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
