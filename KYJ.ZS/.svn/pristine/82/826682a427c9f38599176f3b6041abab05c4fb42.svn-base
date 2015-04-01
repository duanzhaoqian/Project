using System.Collections.Generic;
using KYJ.ZS.DAL.Attributes;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.BLL.Attributes
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/28 14:33:25
    /// 描述：类目属性、属性值操作类
    /// </summary>
    public class AttributeBll
    {
        private AttributeDal dal = new AttributeDal();

        #region 属性规格管理---ningjd

        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectAttrs(int categoryId)
        {
            return dal.GetSelectAttrs(categoryId);
        }

        /// <summary>
        /// 添加属性，返回属性ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public string Create(int categoryId, string name)
        {
            int attrId = dal.Create(categoryId, name);
            if (attrId <= 0)
                return "false";
            return "true||" + attrId.ToString();
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public bool Edit(int id, string name)
        {
            return dal.Edit(id, name) > 0;
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="attrId">属性ID</param>
        /// <returns></returns>
        public bool DeleteAttr(int categoryId, int attrId)
        {
            return dal.DeleteAttr(categoryId, attrId) > 0;
        }

        #endregion
    }
}
