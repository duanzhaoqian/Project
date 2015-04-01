using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.BLL.Categories
{
    using KYJ.ZS.BLL.Attributes;
    using KYJ.ZS.DAL.Categories;
    using KYJ.ZS.Models.Categories;
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/28 14:40:22
    /// 描述：类目属性关系操作类
    /// </summary>
    public class CategoryAttributeBll
    {
        private readonly CategoryAttributeDal dal;
        public CategoryAttributeBll()
        {
            if (dal == null) dal = new CategoryAttributeDal();
        }
        /// <summary>
        /// 根据类目Id，得到此类目包含的属性
        /// </summary>
        /// <returns></returns>
        public List<CategoryAttribute> GetCategoryAttr(int categoryId)
        {
           return dal.GetCategoryAttr(categoryId);
        }

        /// <summary>
        /// 根据类目Id，得到此类目包含的属性及属性name
        /// </summary>
        /// <returns></returns>
        public List<CategoryAttributeEntity> GetCategoryAttrName(int categoryId)
        {
            return dal.GetCategoryAttrName(categoryId);
        }

        /// <summary>
        /// 根据类目Id，得到此类目包含的属性及属性值
        /// </summary>
        /// <returns></returns>
        public List<CategoryAttributeValue> GetCategoryAttrValue(int categoryId)
        {
            return  dal.GetCategoryAttributeValue(categoryId);
        }
    }
}
