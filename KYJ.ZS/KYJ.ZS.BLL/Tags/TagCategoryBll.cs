using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Tags;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.BLL.Tags
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-4-30
    /// 描述：标签下的分类操作
    /// </summary>
    public class TagCategoryBll
    {
        private TagCategoryDal _dal = new TagCategoryDal();

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-4-30
        /// 描述：通过ID获取标签分类
        /// </summary>
        /// <param name="id">标签分类ID</param>
        /// <returns></returns>
        public TagCategory GetTagCategoryById(int id)
        {
            return _dal.GetTagCategoryById(id);
        }

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-4-30
        /// 描述：通过标签ID获取标签下面分类集合
        /// </summary>
        /// <param name="tagid">标签ID</param>
        /// <returns></returns>
        public List<TagCategory> GetTagCategoriesByTagId(int tagid)
        {
            return _dal.GetTagCategoriesByTagId(tagid);
        }
    }
}
