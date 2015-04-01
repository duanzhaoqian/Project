namespace KYJ.ZS.Models.DB
{
    using System;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014-04-17
    /// 描述：分类规格实体类
    /// </summary>
    public class CategoryNorm
    {
        public int Id { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 规格名称
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