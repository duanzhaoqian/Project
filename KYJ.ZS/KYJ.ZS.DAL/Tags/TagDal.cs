using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Tags;
using System.Data;

namespace KYJ.ZS.DAL.Tags
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-4-29
    /// 描述：操作数据库Tags表
    /// </summary>
    public class TagDal
    {
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-4-29
        /// 描述：通过ID获取标签
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        public Tag GetTagById(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [tag_id] as Id
                       ,[category_id] as CategoryId
                       ,[category_name] as CategoryName
                       ,[tag_type] as Type 
                       ,[tag_name] as Name 
                       ,[tag_sort] as Sort
                       ,[tag_createtime] as CreateTime
                       ,[tag_updatetime] as UpdateTime
                       ,[tag_isdel] as IsDel
                 FROM  [dbo].[Tags] NOLOCK
                 WHERE tag_id=@tag_id";

                var param = new SqlParam();
                param.AddParam("@tag_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Tag>.GetEntityList(dt)[0];
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<int>("wwang", id, ex);
                return null;
            }
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
            try
            {
                string sql = @"SELECT  
                        [tag_id] as Id
                       ,[category_id] as CategoryId
                       ,[category_name] as CategoryName
                       ,[tag_type] as Type 
                       ,[tag_name] as Name 
                       ,[tag_sort] as Sort
                       ,[tag_createtime] as CreateTime
                       ,[tag_updatetime] as UpdateTime
                       ,[tag_isdel] as IsDel
                 FROM  [dbo].[Tags] NOLOCK
                 WHERE category_id=@category_id and tag_isdel='false'";

                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                List<Tag> list = new List<Tag>();

                if (!Auxiliary.CheckDt(dt))
                    return list;

                return DataHelper<Tag>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<int>("wwang", categoryId, ex);
                return null;
            }
 
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
            try
            {
                string sql = @"SELECT  
                        [tag_id] as Id
                       ,[category_id] as CategoryId
                       ,[category_name] as CategoryName
                       ,[tag_type] as Type 
                       ,[tag_name] as Name 
                       ,[tag_sort] as Sort
                       ,[tag_createtime] as CreateTime
                       ,[tag_updatetime] as UpdateTime
                       ,[tag_isdel] as IsDel
                 FROM  [dbo].[Tags] NOLOCK
                 WHERE tag_type=@tag_type and tag_isdel='false'";

                var param = new SqlParam();
                param.AddParam("@tag_type", type);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                List<Tag> list = new List<Tag>();

                if (!Auxiliary.CheckDt(dt))
                    return list;

                return DataHelper<Tag>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<int>("wwang", type, ex);
                return null;
            }

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
            try
            {
                var where = "tag_isdel='false' and tag_type=2";
                var orderBy = "tag_name";
                var columnList = "tag_id as Id,tag_name as Name,GoodsCount=(SELECT COUNT(1) FROM SaleGoodses(NOLOCK) WHERE charindex(',' + cast(tag_id as nvarchar(50)) + ',',',' + sale_tag + ',')>0)";
                var tableList = "Tags(NOLOCK)";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableList, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<TagEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalPage = 0;
                totalRecord = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 标签名称校验
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public int isValidateName(string name)
        {
            try
            {
                var sql = @"SELECT COUNT(1)
                        FROM    Tags(NOLOCK)
                        WHERE   tag_name=@tag_name";
                var param = new SqlParam();
                param.AddParam("@tag_name", name);

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", name, ex);
                return 1;
            }
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <returns></returns>
        public int Create(string name)
        {
            try
            {
                var sql = @"INSERT INTO Tags
                                    (category_id
                                    ,category_name
                                    ,tag_type
                                    ,tag_name
                                    ,tag_sort
                                    ,tag_createtime
                                    ,tag_updatetime)
                        VALUES      (0,'',2,@tag_name,0,@createtime,@createtime)";
                var param = new SqlParam();
                param.AddParam("@tag_name", name);
                param.AddParam("@createtime", DateTime.Now);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", name, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取标签的信息数
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public int GetCountByTag(int tagId)
        {
            try
            {
                var sql = "SELECT COUNT(1) FROM SaleGoodses(NOLOCK) WHERE charindex(',' + cast(@tag_id as nvarchar(50)) + ',',',' + sale_tag + ',')>0";
                var param = new SqlParam();
                param.AddParam("@tag_id", tagId);

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", tagId, ex);
                return 0;
            }
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public int Delete(int tagId)
        {
            try
            {
                var sql = "UPDATE Tags SET tag_isdel='true' WHERE tag_id=@tag_id";
                var param = new SqlParam();
                param.AddParam("@tag_id", tagId);
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", tagId, ex);
                return 0;
            }
        }

        #endregion

    }
}
