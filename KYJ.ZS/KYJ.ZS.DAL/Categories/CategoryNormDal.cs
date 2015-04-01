using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.DAL.Categories
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Models.View;
    using System.Data;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 13:24:29
    /// 描述：类目规格操作类
    /// </summary>
    public class CategoryNormDal
    {
        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public CategoryNorm Get(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [norm_id] as Id
                       ,[category_id] as CategoryId
                       ,[norm_name] as Name
                       ,[norm_sort] as Sort
                       ,[norm_createtime] as CreateTime
                       ,[norm_updatetime] as UpdateTime
                       ,[norm_isdel] as IsDel
                 FROM  [dbo].[CategoryNorms] NOLOCK
                 WHERE norm_id=@norm_id";

                var param = new SqlParam();
                param.AddParam("norm_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryNorm>.GetEntity(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">类目id</param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        public List<CategoryNorm> GetCategoryNorm(int id, bool isDel=false,bool isShow=true)
        {
            try
            {
                string sql = @"SELECT  
                        [norm_id] as Id
                       ,[category_id] as CategoryId
                       ,[norm_name] as Name
                       ,[norm_sort] as Sort
                       ,[norm_createtime] as CreateTime
                       ,[norm_updatetime] as UpdateTime
                       ,[norm_isdel] as IsDel
                 FROM  [dbo].[CategoryNorms] NOLOCK
                 WHERE category_id=@category_id and norm_isdel=@norm_isdel and norm_isshow=@norm_isshow";

                var param = new SqlParam();
                param.AddParam("category_id", id);
                param.AddParam("norm_isdel", isDel);
                param.AddParam("norm_isshow", isShow);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryNorm>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return null;
            }
        }

        /// <summary>
        /// 插入记录返回执行条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(CategoryNorm model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryNormDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[CategoryNorms]
                       ([category_id]
                       ,[norm_name]
                       ,[norm_sort]
                       ,[norm_createtime]
                       ,[norm_updatetime])
                 VALUES
                       (@category_id,
                        @norm_name,
                        @norm_sort,
                        @norm_createtime,
                        @norm_updatetime)";

                var param = new SqlParam();
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("norm_name", model.Name);
                param.AddParam("norm_sort", model.Sort);
                param.AddParam("norm_createtime", model.CreateTime);
                param.AddParam("norm_updatetime", model.UpdateTime);

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
        public int AddReturnId(CategoryNorm model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryNormDal.AddReturnId,参数不可为空");
                string sql = @"INSERT INTO [dbo].[CategoryNorms]
                       ([category_id]
                       ,[norm_name]
                       ,[norm_sort]
                       ,[norm_createtime]
                       ,[norm_updatetime])
                 VALUES
                       (@category_id,
                        @norm_name,
                        @norm_sort,
                        @norm_createtime,
                        @norm_updatetime)
                 SELECT SCOPE_IDENTITY() AS Id";

                var param = new SqlParam();
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("norm_name", model.Name);
                param.AddParam("norm_sort", model.Sort);
                param.AddParam("norm_createtime", model.CreateTime);
                param.AddParam("norm_updatetime", model.UpdateTime);

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
        public CategoryNorm AddReturnEntity(CategoryNorm model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryNormDal.AddReturnEntity,参数不可为空");

                var id = this.AddReturnId(model);

                return this.Get(id);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return null;
            }

        }

        /// <summary>
        /// 根据实体更新表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(CategoryNorm model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryNormDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[CategoryNorms]
                                   SET [category_id] = @category_id
                                      ,[norm_name] = @norm_name
                                      ,[norm_sort] = @norm_sort
                                      ,[norm_createtime] = @norm_createtime
                                      ,[norm_updatetime] = @norm_updatetime
                                      ,[norm_isdel] = @norm_isdel
                                 WHERE norm_id = @norm_id";

                var param = new SqlParam();
                param.AddParam("norm_id", model.Id);
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("norm_name", model.Name);
                param.AddParam("norm_sort", model.Sort);
                param.AddParam("norm_createtime", model.CreateTime);
                param.AddParam("norm_updatetime", model.UpdateTime);
                param.AddParam("norm_isdel", model.IsDel);
                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }

        /// <summary>
        /// 根据ID，直接删除数据。数据库删除，谨慎操作。
        /// </summary>
        /// <param name="id">类目id</param>
        /// <returns></returns>
        public bool DeleteFormDateBase(int id)
        {
            try
            {
                const string sql = @"DELETE FROM [dbo].[CategoryNorms]
                                 WHERE norm_id = @norm_id";

                var param = new SqlParam();
                param.AddParam("norm_id", id);


                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return false;
            }
        }

        /// <summary>
        /// 根据ID，删除数据(标识删除)。
        /// </summary>
        /// <param name="id">属性id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                const string sql = @"UPDATE [dbo].[CategoryNorms]
                                   SET [norm_isdel] = 1
                                 WHERE norm_id = @norm_id";

                var param = new SqlParam();
                param.AddParam("norm_id", id);

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return false;
            }
        }

        #region 属性规格管理---ningjd

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool IsShow(int categoryId)
        {
            try
            {
                var sql = "select top 1 norm_isshow from CategoryNorms(nolock) where category_id=@category_id";
                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);
                return Auxiliary.ToBoolen(KYJ_ZushouRDB.GetFirst(sql, param), false);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// 更改显示状态
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int EditShow(int categoryId, bool isShow)
        {
            try
            {
                var sql = "update CategoryNorms set norm_isshow=@norm_isshow,norm_updatetime=@updatetime where category_id=@category_id";
                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);
                param.AddParam("@norm_isshow", isShow);
                param.AddParam("@updatetime", DateTime.Now);
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取分类规格列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectNorms(int categoryId)
        {
            try
            {
                var sql = @"select norm_id as Code,norm_name as Name 
                            from CategoryNorms(NOLOCK)
                            where norm_isdel='false' and category_id=@category_id";
                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<AttrCategorySelectEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }

        #region 添加分类规格

        /// <summary>
        /// 添加分类规格，返回分类规格ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="name">分类规格名称</param>
        /// <returns></returns>
        public int Create(int categoryId, string name, bool normIsShow)
        {
            try
            {
                string sql = @"INSERT INTO  [dbo].[CategoryNorms]
                                    ([category_id]
                                    ,[norm_name]
                                    ,[norm_sort]
                                    ,[norm_createtime]
                                    ,[norm_updatetime]
                                    ,[norm_isshow])
                            VALUES
                                    (@category_id
                                    ,@norm_name
                                    ,0
                                    ,@createtime
                                    ,@createtime
                                    ,@norm_isshow)
                            SELECT @@IDENTITY";

                var param = new SqlParam();
                param.AddParam("@norm_name", name);
                param.AddParam("@category_id", categoryId);
                param.AddParam("@createtime", DateTime.Now);
                param.AddParam("@norm_isshow", normIsShow);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", categoryId + "," + name, ex);
                return 0;
            }
        }

        #endregion

        #region 修改分类规格

        /// <summary>
        /// 修改分类规格
        /// </summary>
        /// <param name="id">分类规格ID</param>
        /// <param name="name">分类规格名称</param>
        /// <returns></returns>
        public int Edit(int id, string name)
        {
            try
            {
                string sql = @"UPDATE  [dbo].[CategoryNorms]
                            SET     norm_name=@norm_name
                                    ,norm_updatetime=@updatetime
                            WHERE   norm_id=@norm_id";

                var param = new SqlParam();
                param.AddParam("@norm_name", name);
                param.AddParam("@norm_id", id);
                param.AddParam("@updatetime", DateTime.Now);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id + "," + name, ex);
                return 0;
            }
        }

        #endregion

        #region 删除分类规格

        /// <summary>
        /// 删除分类规格
        /// </summary>
        /// <param name="id">分类规格ID</param>
        /// <returns></returns>
        public int DeleteNorm(int id)
        {
            try
            {
                string sql = @"UPDATE  [dbo].[CategoryNorms]
                            SET     norm_isdel='true'
                            WHERE   norm_id=@norm_id";

                var param = new SqlParam();
                param.AddParam("@norm_id", id);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id, ex);
                return 0;
            }
        }

        #endregion

        #endregion
    }
}
