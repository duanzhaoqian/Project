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
    /// 时间：2014/4/22 12:58:04
    /// 描述：类目颜色操作类
    /// </summary>
    public class CategoryColorDal
    {
        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public CategoryColor Get(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [color_id] as Id
                       ,[category_id] as CategoryId
                       ,[color_name] as Name
                       ,[color_code] as Code
                       ,[color_sort] as Sort
                       ,[color_createtime] as CreateTime
                       ,[color_updatetime] as UpdateTime
                       ,[color_isdel] as IsDel
                 FROM  [dbo].[CategoryColors] NOLOCK
                 WHERE color_id=@color_id";

                var param = new SqlParam();
                param.AddParam("color_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryColor>.GetEntity(dt.Rows[0]);
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
        public List<CategoryColor> GetCategoryColor(int id ,bool isDel = false)
        {
            try
            {
                string sql = @"SELECT  
                        [color_id] as Id
                       ,[category_id] as CategoryId
                       ,[color_name] as Name
                       ,[color_code] as Code
                       ,[color_sort] as Sort
                       ,[color_createtime] as CreateTime
                       ,[color_updatetime] as UpdateTime
                       ,[color_isdel] as IsDel
                 FROM  [dbo].[CategoryColors] NOLOCK
                 WHERE category_id=@category_id  AND color_isdel=@color_isdel AND color_isShow=1";

                var param = new SqlParam();
                param.AddParam("category_id", id);
                param.AddParam("color_isdel", isDel);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryColor>.GetEntityList(dt);
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
        public int Add(CategoryColor model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryColorDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[CategoryColors]
                       ([category_id]
                       ,[color_name]
                       ,[color_code]
                       ,[color_sort]
                       ,[color_createtime]
                       ,[color_updatetime])
                 VALUES
                       (@category_id,
                        @color_name,
                        @color_code,
                        @color_sort,
                        @color_createtime,
                        @color_updatetime)";

                var param = new SqlParam();
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("color_name", model.Name);
                param.AddParam("color_code", model.Code);
                param.AddParam("color_sort", model.Sort);
                param.AddParam("color_createtime", model.CreateTime);
                param.AddParam("color_updatetime", model.UpdateTime);

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
        public int AddReturnId(CategoryColor model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryColorDal.AddReturnId,参数不可为空");
                string sql = @"INSERT INTO [dbo].[CategoryColors]
                       ([category_id]
                       ,[color_name]
                       ,[color_code]
                       ,[color_sort]
                       ,[color_createtime]
                       ,[color_updatetime])
                 VALUES
                       (@category_id,
                        @color_name,
                        @color_code,
                        @color_sort,
                        @color_createtime,
                        @color_updatetime)
                 SELECT SCOPE_IDENTITY() AS Id";

                var param = new SqlParam();
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("color_name", model.Name);
                param.AddParam("color_code", model.Code);
                param.AddParam("color_sort", model.Sort);
                param.AddParam("color_createtime", model.CreateTime);
                param.AddParam("color_updatetime", model.UpdateTime);

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
        public CategoryColor AddReturnEntity(CategoryColor model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryColorDal.AddReturnEntity,参数不可为空");

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
        public bool Update(CategoryColor model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryColorDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[CategoryColors]
                                   SET [category_id] = @category_id
                                      ,[color_name] = @color_name
                                      ,[color_code] = @color_code
                                      ,[color_sort] = @color_sort
                                      ,[color_createtime] = @color_createtime
                                      ,[color_updatetime] = @color_updatetime
                                      ,[color_isdel] = @color_isdel
                                 WHERE color_id = @color_id";

                var param = new SqlParam();
                param.AddParam("color_id", model.Id);
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("color_name", model.Name);
                param.AddParam("color_code", model.Code);
                param.AddParam("color_sort", model.Sort);
                param.AddParam("color_createtime", model.CreateTime);
                param.AddParam("color_updatetime", model.UpdateTime);
                param.AddParam("color_isdel", model.IsDel);
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
                const string sql = @"DELETE FROM [dbo].[CategoryColors]
                                 WHERE color_id = @color_id";

                var param = new SqlParam();
                param.AddParam("color_id", id);


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
                const string sql = @"UPDATE [dbo].[CategoryColors]
                                   SET [color_isdel] = 1
                                 WHERE color_id = @color_id";

                var param = new SqlParam();
                param.AddParam("color_id", id);

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
                var sql = "select top 1 color_isshow from CategoryColors(nolock) where category_id=@category_id";
                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);
                return Auxiliary.ToBoolen(KYJ_ZushouRDB.GetFirst(sql, param));
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
                var sql = "update CategoryColors set color_isshow=@color_isshow,color_updatetime=@updatetime where category_id=@category_id";
                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);
                param.AddParam("@color_isshow", isShow);
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
        /// 获取分类颜色列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectColors(int categoryId)
        {
            try
            {
                var sql = @"select color_id as Code,color_name as Name 
                            from CategoryColors(NOLOCK)
                            where color_isdel='false' and category_id=@category_id";
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

        #region 添加分类颜色

        /// <summary>
        /// 添加分类颜色，返回分类颜色ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="name">分类颜色名称</param>
        /// <returns></returns>
        public int Create(int categoryId, string name, bool colorIsShow)
        {
            try
            {
                string sql = @"INSERT INTO  [dbo].[CategoryColors]
                                    ([category_id]
                                    ,[color_name]
                                    ,[color_code]
                                    ,[color_sort]
                                    ,[color_createtime]
                                    ,[color_updatetime]
                                    ,[color_isshow])
                            VALUES
                                    (@category_id
                                    ,@color_name
                                    ,'',0
                                    ,@createtime
                                    ,@createtime
                                    ,@color_isshow)
                            SELECT @@IDENTITY";

                var param = new SqlParam();
                param.AddParam("@color_name", name);
                param.AddParam("@category_id", categoryId);
                param.AddParam("@createtime", DateTime.Now);
                param.AddParam("@color_isshow", colorIsShow);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", categoryId + "," + name, ex);
                return 0;
            }
        }

        #endregion

        #region 修改分类颜色

        /// <summary>
        /// 修改分类颜色
        /// </summary>
        /// <param name="id">分类颜色ID</param>
        /// <param name="name">分类颜色名称</param>
        /// <returns></returns>
        public int Edit(int id, string name)
        {
            try
            {
                string sql = @"UPDATE  [dbo].[CategoryColors]
                            SET     color_name=@color_name
                                    ,color_updatetime=@updatetime
                            WHERE   color_id=@color_id";

                var param = new SqlParam();
                param.AddParam("@color_name", name);
                param.AddParam("@color_id", id);
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

        #region 删除分类颜色

        /// <summary>
        /// 删除分类颜色
        /// </summary>
        /// <param name="id">分类颜色ID</param>
        /// <returns></returns>
        public int DeleteColor(int id)
        {
            try
            {
                string sql = @"UPDATE  [dbo].[CategoryColors]
                            SET     color_isdel='true'
                            WHERE   color_id=@color_id";

                var param = new SqlParam();
                param.AddParam("@color_id", id);

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
