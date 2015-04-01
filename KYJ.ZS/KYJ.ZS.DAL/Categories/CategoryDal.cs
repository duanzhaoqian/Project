using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.DAL.Categories
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.Categories;
    using KYJ.ZS.Commons.Index;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014-04-17
    /// 描述：商品类目操作类
    /// </summary>
    public class CategoryDal
    {
        /// <summary>
        /// 根据类目ID得到一条类目记录
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category Get(int categoryId)
        {
            try
            {
                string sql = @"SELECT  
                       [category_id] as Id
                       ,[category_name] as Name
                       ,[category_pid] as PId
                       ,[category_level] as Level
                       ,[category_type] as Type
                       ,[category_sort] as Sort
                       ,[category_isdel] as IsDel
                 FROM  [dbo].[Categories] NOLOCK
                 WHERE category_id=@category_id";

                var param = new SqlParam();
                param.AddParam("category_id", categoryId);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                //var item = dt.Rows[0];

                //category.Id = Auxiliary.ToInt32(item["category_id"]);
                //category.Name = item["category_name"].ToString();
                //category.PId = Auxiliary.ToInt32(item["category_pid"]);
                //category.Level = Auxiliary.ToInt32(item["category_level"]);
                //category.Type = Auxiliary.ToInt32(item["category_type"]);
                //category.Sort = Auxiliary.ToInt32(item["category_sort"]);
                //category.IsDel = Auxiliary.ToBoolen(item["category_isdel"]);

                return DataHelper<Category>.GetEntity(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", categoryId, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据类目父ID返回子节点信息（默认未删除的）
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        public List<Category> GetCategoryByParentId(int parentId, bool isDel = false)
        {
            try
            {
                string sql = @"SELECT  
                       [category_id] as Id
                       ,[category_name] as Name
                       ,[category_pid] as PId
                       ,[category_level] as Level
                       ,[category_type] as Type
                       ,[category_sort] as Sort
                       ,[category_isdel] as IsDel
                 FROM  [dbo].[Categories] NOLOCK
                 WHERE category_pid=@category_pid AND category_isdel=@category_isdel";

                var param = new SqlParam();
                param.AddParam("@category_pid", parentId);
                param.AddParam("@category_isdel", isDel);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                var categoryList = new List<Category>();
                if (!Auxiliary.CheckDt(dt))
                    return categoryList;

                var category = new Category();

                foreach (DataRow item in dt.Rows)
                {
                    //category.Id = Auxiliary.ToInt32(item["category_id"]);
                    //category.Name = item["category_name"].ToString();
                    //category.PId = Auxiliary.ToInt32(item["category_pid"]);
                    //category.Level = Auxiliary.ToInt32(item["category_level"]);
                    //category.Type = Auxiliary.ToInt32(item["category_type"]);
                    //category.Sort = Auxiliary.ToInt32(item["category_sort"]);
                    //category.IsDel = Auxiliary.ToBoolen(item["category_isdel"]);
                    category = DataHelper<Category>.GetEntity(item);

                    categoryList.Add(category);
                }

                return categoryList;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", parentId, ex);
                return null;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-03-5-13
        /// 描述：根据类目父ID返回子节点信息（默认未删除的）
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        public List<Category> Web_GetNavigationCategoryByParentId(int parentId, bool isDel = false)
        {
            try
            {
                string sql = @"SELECT  
                       [category_id] as Id
                       ,[category_name] as Name
                       ,[category_pid] as PId
                       ,[category_level] as Level
                 FROM  [dbo].[Categories] NOLOCK
                 WHERE category_isshow=1 and category_pid=@category_pid AND category_isdel=@category_isdel";

                var param = new SqlParam();
                param.AddParam("@category_pid", parentId);
                param.AddParam("@category_isdel", isDel);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                var categoryList = new List<Category>();

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Category>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "分类,父ID返回子节点信息", ex);
                return null;
            }
        }

        /// <summary>
        /// 根据类目级别返回子节点信息（默认未删除的）
        /// </summary>
        /// <param name="level">类目级别</param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        public List<Category> GetCategoryByLevel(int level, bool isDel = false)
        {
            try
            {
                string sql = @"SELECT  
                       [category_id] as Id
                       ,[category_name] as Name
                       ,[category_pid] as PId
                       ,[category_level] as Level
                       ,[category_type] as Type
                       ,[category_sort] as Sort
                       ,[category_isdel] as IsDel
                 FROM  [dbo].[Categories] NOLOCK
                 WHERE category_level=@category_level AND category_isdel=@category_isdel";

                var param = new SqlParam();
                param.AddParam("@category_level", level);
                param.AddParam("@category_isdel", isDel);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Category>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", level, ex);
                return null;
            }
        }

        
        /// <summary>
        /// 插入记录返回执行条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Category model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[Categories]
                       ([category_name]
                       ,[category_pid]
                       ,[category_level]
                       ,[category_type]
                       ,[category_sort])
                 VALUES
                       (@category_name,
                        @category_pid,
                        @category_level,
                        @category_type,
                        @category_sort)";

                var param = new SqlParam();
                param.AddParam("category_name", model.Name);
                param.AddParam("category_pid", model.PId);
                param.AddParam("category_level", model.Level);
                param.AddParam("category_type", model.Type);
                param.AddParam("category_sort", model.Sort);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return 0;
            }
            
        }

        /// <summary>
        /// 插入记录返回ID值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddReturnId(Category model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryDal.AddReturnId,参数不可为空");
                string sql = @"INSERT INTO [dbo].[Categories]
                       ([category_name]
                       ,[category_pid]
                       ,[category_level]
                       ,[category_type]
                       ,[category_sort])
                 VALUES
                       (@category_name,
                        @category_pid,
                        @category_level,
                        @category_type,
                        @category_sort)
                 SELECT SCOPE_IDENTITY() AS Id";

                var param = new SqlParam();
                param.AddParam("category_name", model.Name);
                param.AddParam("category_pid", model.PId);
                param.AddParam("category_level", model.Level);
                param.AddParam("category_type", model.Type);
                param.AddParam("category_sort", model.Sort);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return 0;
            }

        }

        /// <summary>
        /// 插入记录返回实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Category AddReturnEntity(Category model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryDal.AddReturnEntity,参数不可为空");
                string sql = @"INSERT INTO [dbo].[Categories]
                       ([category_name]
                       ,[category_pid]
                       ,[category_level]
                       ,[category_type]
                       ,[category_sort])
                 VALUES
                       (@category_name,
                        @category_pid,
                        @category_level,
                        @category_type,
                        @category_sort)
                 SELECT SCOPE_IDENTITY() AS Id";

                var param = new SqlParam();
                param.AddParam("category_name", model.Name);
                param.AddParam("category_pid", model.PId);
                param.AddParam("category_level", model.Level);
                param.AddParam("category_type", model.Type);
                param.AddParam("category_sort", model.Sort);

                model.Id = KYJ_ZushouWDB.SqlExecute(sql, param);

                return model;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return null;
            }

        }

        /// <summary>
        /// 根据类目实体中类目ID更新类目表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Category model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[Categories]
                                   SET [category_name] = @category_name
                                      ,[category_pid] = @category_pid
                                      ,[category_level] = @category_level
                                      ,[category_type] = @category_type
                                      ,[category_sort] = @category_sort
                                      ,[category_isdel] = @category_isdel
                                 WHERE category_id = @category_id";

                var param = new SqlParam();
                param.AddParam("category_id", model.Id);
                param.AddParam("category_name", model.Name);
                param.AddParam("category_pid", model.PId);
                param.AddParam("category_level", model.Level);
                param.AddParam("category_type", model.Type);
                param.AddParam("category_sort", model.Sort);
                param.AddParam("category_isdel", model.IsDel);
                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }

        /// <summary>
        /// 根据类目ID，直接删除类目。数据库删除，谨慎操作。
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool DeleteFormDateBase(int categoryId)
        {
            try
            {
                const string sql = @"DELETE FROM [dbo].[Categories]
                                 WHERE category_id = @category_id";

                var param = new SqlParam();
                param.AddParam("category_id", categoryId);


                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", categoryId, ex);
                return false;
            }
        }

        public bool Delete(int categoryId)
        {
            try
            {
                const string sql = @"UPDATE [dbo].[Categories]
                                   SET [category_isdel] = 1
                                 WHERE category_id = @category_id";

                var param = new SqlParam();
                param.AddParam("category_id", categoryId);

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", categoryId, ex);
                return false;
            }
        }

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-8
        /// 描述：获取所有分类
        /// </summary>
        /// <returns></returns>
        public Relusts<CatResult> GetCategoryList()
        {
            try
            {
                string sql = @"SELECT  
                       [category_id] as Id
                       ,[category_name] as Name
                       ,[category_pid] as PId
                       ,[category_level] as Level
                       ,[category_type] as Type
                       ,[category_sort] as Sort
                       ,[category_isdel] as IsDel
                 FROM  [dbo].[Categories] with(nolock)
                 WHERE category_isdel=@category_isdel AND [category_isshow]=@category_isshow";


                var param = new SqlParam();
                param.AddParam("@category_isdel", false);
                param.AddParam("@category_isshow", true);

                var dtCategories = KYJ_ZushouRDB.GetTable(sql, param);

                Relusts<CatResult> result = new Relusts<CatResult>();

                result.List = DataHelper<CatResult>.GetEntityList(dtCategories);

                return result;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wwang", ex.Message, ex);
                return null;
            }


        }

        #region 商品分类管理---ningjd

        #region 添加分类
        /// <summary>
        /// 分类名称校验
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public int CheckName(string name)
        {
            try
            {
                string sql = @"SELECT  COUNT(1)
                 FROM  [dbo].[Categories] with(nolock)
                 WHERE category_name=@category_name and category_isdel='false'";

                var param = new SqlParam();
                param.AddParam("@category_name", name);

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", name, ex);
                return 1;
            }
        }

        /// <summary>
        /// 添加分类，返回分类ID
        /// </summary>
        /// <param name="pId">父类目ID</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="level">节点深度</param>
        /// <returns></returns>
        public int Create(int pId, string categoryName, int level)
        {
            try
            {
                string sql = @"INSERT INTO  [dbo].[Categories]
                                    ([category_name]
                                    ,[category_pid]
                                    ,[category_level]
                                    ,[category_type]
                                    ,[category_sort])
                             VALUES
                                    (@category_name,
                                    @category_pid,
                                    @category_level,
                                    2,0)
                            SELECT @@IDENTITY";

                var param = new SqlParam();
                param.AddParam("@category_name", categoryName);
                param.AddParam("@category_pid", pId);
                param.AddParam("@category_level", level);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", pId + "," + categoryName + "," + level, ex);
                return 0;
            }
        }

        #endregion

        #region 修改分类

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            try
            {
                string sql = @"SELECT  category_name
                 FROM  [dbo].[Categories] with(nolock)
                 WHERE category_id=@category_id and category_isdel='false'";

                var param = new SqlParam();
                param.AddParam("@category_id", id);

                return KYJ_ZushouRDB.GetFirst(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return "";
            }
        }

        /// <summary>
        /// 修改分类名称
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public int EditName(int id, string name)
        {
            try
            {
                string sql = @"UPDATE   [dbo].[Categories]
                                SET     category_name=@category_name
                                WHERE category_id=@category_id";

                var param = new SqlParam();
                param.AddParam("@category_id", id);
                param.AddParam("@category_name", name);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return 0;
            }
        }

        #endregion

        #region 删除分类

        /// <summary>
        /// 删除校验
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public int DeleteValidate(int id)
        {
            try
            {
                var sql = @"declare @level int --节点深度
                        set @level=(select category_level from Categories with(nolock) where category_id=@category_id)
                        if(@level=2)
                        begin
	                        select 0;
                        end
                        else
                        begin
	                        select count(1) from Categories with(nolock) where category_pid=@category_id and category_isdel='false'
                        end";
                var param = new SqlParam();
                param.AddParam("@category_id", id);

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return 1;
            }
        }

        /// <summary>
        /// 删除分类校验是否有该分类的商品
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public int DeleteValidateGoods(int id)
        {
            try
            {
                var sql = @"select count(1) 
                            from RentalGoodses with(nolock) 
                            where rental_categoryid=@rental_categoryid and rental_isdel='false'";
                var param = new SqlParam();
                param.AddParam("@rental_categoryid", id);

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return 1;
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <returns></returns>
        public int DeleteCategory(int categoryId)
        {
            try
            {
                var sql = @"UPDATE [dbo].[Categories]
                        SET [category_isdel] = 'true'
                        WHERE category_id = @category_id";

                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", categoryId, ex);
                return 0;
            }
        }

        #endregion

        #endregion

     

    }
}
