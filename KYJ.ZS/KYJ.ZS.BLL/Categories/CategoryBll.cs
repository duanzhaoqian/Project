using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.BLL.Categories
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.Attributes;
    using KYJ.ZS.DAL.Categories;
    using KYJ.ZS.Models.Categories;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Commons.Index;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014-04-17
    /// 描述：商品类目操作类
    /// </summary>       
    public class CategoryBll
    {
        private readonly CategoryDal dal;
        public CategoryBll()
        {
            if (dal == null) dal = new CategoryDal();
        }

        /// <summary>
        /// 得到类目根节点集合
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCateGoryList()
        {
            return dal.GetCategoryByLevel(0);
        }

        /// <summary>
        /// 获取类目子一级节点
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns></returns>
        public List<Category> GetCateGoryList(int parentId)
        {
            return dal.GetCategoryByParentId(parentId);
        }

        /// <summary>
        /// 在指定节点下增加一个子类目
        /// </summary>
        /// <param name="parentId">父节点ID</param>
        /// <param name="parentLevel">父节点的级别</param>
        /// <param name="childNode">要增加的类目实体</param>
        /// <returns></returns>
        public bool AddCategory(int parentId, int parentLevel, Category childNode)
        {
            if (childNode == null) return false;
            childNode.PId = parentId;
            childNode.Level += parentLevel;

            return dal.Add(childNode) > 0;
        }

        public bool AddCategory(int parentId, Category childNode)
        {
            var category = dal.Get(parentId);

            return AddCategory(parentId, category.Level += 1, childNode);
        }

        public bool AddCategory(int parentId, string chileNodeName)
        {
            var childNode = new Category() { Name = chileNodeName };

            return AddCategory(parentId, childNode);
        }

        /// <summary>
        /// 修改指定ID的类目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool UpdateCategory(int id, UpdateCatregoryEntity node)
        {
            if (node == null) return false;
            var category = dal.Get(id);

            node.CopyValueTo<Category>(category);

            return dal.Update(category);
        }

        /// <summary>
        /// 根据UpdateCatregoryEntity修改值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool UpdateCategory(UpdateCatregoryEntity node)
        {
            if (node == null) return false;

            return node.Id != null && this.UpdateCategory(node.Id.Value, node);
        }
        /// <summary>
        /// 根据类目ID删除类目，isReal默认false(标识删除)，true为物理删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isReal"></param>
        /// <returns></returns>
        public bool DeleteCategory(int id, bool isReal = false)
        {
            return isReal ? this.dal.DeleteFormDateBase(id) : this.dal.Delete(id);
        }

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-8
        /// 描述：获取分类集合（未被删除）
        /// </summary>
        /// <returns></returns>
        public Relusts<CatResult> GetCategoryList()
        {
            return dal.GetCategoryList();
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-13
        /// 描述：根据类目父ID返回子节点信息（默认未删除的）
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        public List<Category> Web_GetNavigationCategoryByParentId(int parentId)
        {
            return dal.Web_GetNavigationCategoryByParentId(parentId);
        }

        #region 类目下拉列表相关---ningjd

        /// <summary>
        /// 一级类目列表
        /// </summary>
        /// <returns></returns>
        public IList<CategorySelectEntity> GetCategorySelectList()
        {
            IList<Category> list = dal.GetCategoryByLevel(0);
            if (list == null || list.Count <= 0)
                return new List<CategorySelectEntity>();
            IList<CategorySelectEntity> result = new List<CategorySelectEntity>();
            foreach (var category in list)
            {
                CategorySelectEntity entity = new CategorySelectEntity();
                entity.GeographyCode = category.Id;
                entity.GeographyName = category.Name;
                result.Add(entity);
            }
            return result;
        }

        /// <summary>
        /// 获取字节点类目列表
        /// </summary>
        /// <param name="parentId">父节点ID</param>
        /// <returns></returns>
        public IList<CategorySelectEntity> GetCategorySelectList(int parentId)
        {
            IList<Category> list = dal.GetCategoryByParentId(parentId);
            if (list == null || list.Count <= 0)
                return new List<CategorySelectEntity>();
            IList<CategorySelectEntity> result = new List<CategorySelectEntity>();
            foreach (var category in list)
            {
                CategorySelectEntity entity = new CategorySelectEntity();
                entity.GeographyCode = category.Id;
                entity.GeographyName = category.Name;
                result.Add(entity);
            }
            return result;
        }

        #endregion



        #region 商品分类管理---ningjd

        /// <summary>
        /// 分类名称校验
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public bool CheckName(string name)
        {
            return dal.CheckName(name) <= 0;
        }

        /// <summary>
        /// 添加分类，返回结果及分类ID
        /// </summary>
        /// <param name="pId">父类目ID</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="level">节点深度</param>
        /// <returns></returns>
        public string Create(int pId, string categoryName, int level)
        {
            int id = dal.Create(pId, categoryName, level);
            return (id > 0).ToString() + "||" + id.ToString();
        }

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            return dal.GetName(id);
        }

        /// <summary>
        /// 修改分类名称
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public bool EditName(int id, string name)
        {
            return dal.EditName(id, name) > 0;
        }

        /// <summary>
        /// 删除校验
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public bool DeleteValidate(int id)
        {
            return dal.DeleteValidate(id) <= 0;
        }

        /// <summary>
        /// 删除分类校验是否有该分类的商品
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public bool DeleteValidateGoods(int id)
        {
            return dal.DeleteValidateGoods(id) <= 0;
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <returns></returns>
        public bool DeleteCategory(int categoryId)
        {
            return dal.DeleteCategory(categoryId) > 0;
        }

        #endregion
    }
}
