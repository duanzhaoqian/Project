using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Tags;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Tags;

namespace KYJ.ZS.BLL.Tags
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-4-30
    /// 描述：标签操作
    /// </summary>
    public class TagBll
    {
        private TagDal _dal = new TagDal();

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-4-29
        /// 描述：通过ID获取标签
        /// </summary>
        /// <param name="id">标签ID</param>
        public Tag GetTagById(int id)
        {
            return _dal.GetTagById(id);
        }
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-4-29
        /// 描述：通过分类ID获取分类下所有标签集合
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <returns></returns>
        public List<Tag> GetTagsByCategoryId(int categoryId)
        {
            return _dal.GetTagsByCategoryId(categoryId);
        }
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-9
        /// 通过标签类型获取标签集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Tag> GetsTagsByType(int type)
        {
            return _dal.GetsTagsByType(type);
        }

        #region 信息标签管理---ningjd

        /// <summary>
        /// Author：ningjd
        /// Time：2014-05-27
        /// Desc：获取信息标签管理列表
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<TagEntity> GetTagsManageList(int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return _dal.GetTagsManageList(index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 标签名称校验
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public bool isValidateName(string name)
        {
            return _dal.isValidateName(name) <= 0;
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public bool Create(string name)
        {
            return _dal.Create(name) > 0;
        }

        /// <summary>
        /// 删除标签校验
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public bool isValidateDelete(int tagId)
        {
            return _dal.GetCountByTag(tagId) <= 0;
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public bool Delete(int tagId)
        {
            return _dal.Delete(tagId) > 0;
        }

        #endregion
    }
}
