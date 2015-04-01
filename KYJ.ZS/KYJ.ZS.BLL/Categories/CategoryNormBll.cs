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
    /// 时间：2014/4/28 16:45:30
    /// 描述：类目规格关系操作类
    /// </summary>
    public class CategoryNormBll
    {
        private CategoryNormDal dal = new CategoryNormDal();

        /// <summary>
        /// 根据类目Id，得到此类目包含的颜色
        /// </summary>
        /// <returns></returns>
        public List<CategoryNorm> GetCategoryNorm(int categoryId)
        {
            return new CategoryNormDal().GetCategoryNorm(categoryId);
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
        /// 获取分类规格列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectNorms(int categoryId)
        {
            return dal.GetSelectNorms(categoryId);
        }

        /// <summary>
        /// 添加分类规格，返回分类规格ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="name">分类规格名称</param>
        /// <returns></returns>
        public string Create(int categoryId, string name, bool normIsShow)
        {
            int normId = dal.Create(categoryId, name, normIsShow);
            if (normId <= 0)
                return "false";
            return "true||" + normId.ToString();
        }

        /// <summary>
        /// 修改分类规格
        /// </summary>
        /// <param name="id">分类规格ID</param>
        /// <param name="name">分类规格名称</param>
        /// <returns></returns>
        public bool Edit(int id, string name)
        {
            return dal.Edit(id, name) > 0;
        }

        /// <summary>
        /// 删除分类规格
        /// </summary>
        /// <param name="id">分类规格ID</param>
        /// <returns></returns>
        public bool DeleteNorm(int id)
        {
            return dal.DeleteNorm(id) > 0;
        }

        #endregion
    }
}
