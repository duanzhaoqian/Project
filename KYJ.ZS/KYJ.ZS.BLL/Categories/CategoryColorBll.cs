using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.BLL.Categories
{
    using KYJ.ZS.DAL.Categories;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Models.View;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/28 16:42:45
    /// 描述：类目颜色关系操作类
    /// </summary>
    public class CategoryColorBll
    {
        private CategoryColorDal dal = new CategoryColorDal();

        /// <summary>
        /// 根据类目Id，得到此类目包含的颜色
        /// </summary>
        /// <returns></returns>
        public List<CategoryColor> GetCategoryColor(int categoryId)
        {
            return new CategoryColorDal().GetCategoryColor(categoryId);
        }

        #region 属性规格管理---ningjd

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool IsShow(int categoryId)
        {
            return dal.IsShow(categoryId);
        }

        /// <summary>
        /// 更改显示状态
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool EditShow(int categoryId, bool isShow)
        {
            return dal.EditShow(categoryId, isShow) > 0;
        }

        /// <summary>
        /// 获取分类颜色列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectColors(int categoryId)
        {
            return dal.GetSelectColors(categoryId);
        }

        /// <summary>
        /// 添加分类颜色，返回分类颜色ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="name">分类颜色名称</param>
        /// <returns></returns>
        public string Create(int categoryId, string name, bool colorIsShow)
        {
            int colorId = dal.Create(categoryId, name, colorIsShow);
            if (colorId <= 0)
                return "false";
            return "true||" + colorId.ToString();
        }

        /// <summary>
        /// 修改分类颜色
        /// </summary>
        /// <param name="id">分类颜色ID</param>
        /// <param name="name">分类颜色名称</param>
        /// <returns></returns>
        public bool Edit(int id, string name)
        {
            return dal.Edit(id, name) > 0;
        }

        /// <summary>
        /// 删除分类颜色
        /// </summary>
        /// <param name="id">分类颜色ID</param>
        /// <returns></returns>
        public bool DeleteColor(int id)
        {
            return dal.DeleteColor(id) > 0;
        }

        #endregion
    }
}
