using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.Tags
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-4-30
    /// 描述：操作数据库TagCategories表
    /// </summary>
    public class TagCategoryDal
    {
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-4-30
        /// 描述：通过ID获取标签分类
        /// </summary>
        /// <param name="id">标签分类ID</param>
        /// <returns></returns>
        public TagCategory GetTagCategoryById(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [tagcategory_id] as Id
                       ,[tag_id] as TagId
                       ,[category_id] as CategoryId
                       ,[category_level] as CategoryLevel
                       ,[tagcategory_name] as Name 
                       ,[tagcategory_isshow] as IsShow 
                       ,[tagcategory_sort] as Sort
                       ,[tagcategory_createtime] as CreateTime
                       ,[tagcategory_updatetime] as UpdateTime
                       ,[tagcategory_isdel] as IsDel
                 FROM  [dbo].[TagCategories] NOLOCK
                 WHERE tagcategory_id=@tagcategory_id";

                var param = new SqlParam();
                param.AddParam("@tagcategory_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<TagCategory>.GetEntityList(dt)[0];
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<int>("wwang", id, ex);
                return null;
            }
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
            try
            {
                string sql = @"SELECT  
                        [tagcategory_id] as Id
                       ,[tag_id] as TagId
                       ,[category_id] as CategoryId
                       ,[category_level] as CategoryLevel
                       ,[tagcategory_name] as Name 
                       ,[tagcategory_isshow] as IsShow 
                       ,[tagcategory_sort] as Sort
                       ,[tagcategory_createtime] as CreateTime
                       ,[tagcategory_updatetime] as UpdateTime
                       ,[tagcategory_isdel] as IsDel
                 FROM  [dbo].[TagCategories] NOLOCK
                 WHERE tag_id=@tag_id";

                var param = new SqlParam();
                param.AddParam("@tag_id", tagid);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                var list = new List<TagCategory>();

                if (!Auxiliary.CheckDt(dt))
                    return list;

                return DataHelper<TagCategory>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<int>("wwang", tagid, ex);
                return null;
            }
        }
    }
}
