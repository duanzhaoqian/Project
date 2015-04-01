using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Categories
{
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014-04-17
    /// 描述：类目实体类
    /// </summary>
    class SelectCatregoryEntity
    {
    }
    public class CategoryAttributeEntity
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-19
        /// 描述：分类属性Id
        /// </summary>
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
        /// 属性名称
        /// </summary>
        public string AttributeName { get; set; }
    }
    /// <summary>
    /// 类目、属性、属性值实体
    /// </summary>
    public class CategoryAttributeValue
    {
        public int CategoryId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public int AttributeValId { get; set; }
        public string AttributeValName { get; set; }
    }

    public class UpdateCatregoryEntity
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int? PId { get; set; }

        /// <summary>
        /// 节点深度
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 分类类型0 未知，1 租 ，2 售
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDel { get; set; }
    }

    public class UpdateCategoryAttribute
    {
        public int? Id { get; set; }

        /// <summary>
        /// 类目ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// 属性ID
        /// </summary>
        public int? AttributeId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDel { get; set; } 
    }

    public class UpdateCategoryColor
    {
        public int? Id { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// 颜色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 颜色编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDel { get; set; }
    }

    public class UpdateCategoryNorm
    {
        public int? Id { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDel { get; set; } 
    }

    /// <summary>
    /// Author:ningjd
    /// Date:2014-05-26
    /// Desc:类目下拉列表Entity
    /// </summary>
    public class CategorySelectEntity
    {
        /// <summary>
        /// 类目ID
        /// </summary>
        public int GeographyCode { get; set; }

        /// <summary>
        /// 类目名称
        /// </summary>
        public string GeographyName { get; set; }
    }
}
