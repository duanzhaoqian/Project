using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.BLL.Attributes
{
    using KYJ.ZS.DAL.Attributes;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Models.View;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/28 14:49:14
    /// 描述：属性值操作类
    /// </summary>
    public class AttributeValueBll
    {
        private AttributeValueDal dal = new AttributeValueDal();

        /// <summary>
        /// 根据类目属性Id，得到此类目包含的属性值
        /// </summary>
        /// <returns></returns>
        public List<AttributeValue> GetAttrValue(int id)
        {
            return new AttributeValueDal().GetAttrValue(id);
        }

        #region 属性规格管理---ningjd

        /// <summary>
        /// 获取属性值列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="attrId">属性ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectAttrValues(int categoryId, int attrId)
        {
            return new AttributeValueDal().GetSelectAttrValues(categoryId, attrId);
        }

        /// <summary>
        /// 添加属性值，返回属性值ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="attrId">属性ID</param>
        /// <param name="name">属性值名称</param>
        /// <returns></returns>
        public string Create(int categoryId, int attrId, string name)
        {
            int attrValueId = dal.Create(categoryId, attrId, name);
            if (attrValueId <= 0)
                return "false";
            return "true||" + attrValueId.ToString();
        }

        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="id">属性值ID</param>
        /// <param name="name">属性值名称</param>
        /// <returns></returns>
        public bool Edit(int id, string name)
        {
            return dal.Edit(id, name) > 0;
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="id">属性值ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return dal.DeleteAttrValue(id) > 0;
        }

        #endregion
    }
}
