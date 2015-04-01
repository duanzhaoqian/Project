using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.DAL.Categories
{
    using System.Data;

    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.Categories;
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 9:47:50
    /// 描述：类目属性关系操作类
    /// </summary>
    public class CategoryAttributeDal
    {
        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">类目属性id</param>
        /// <returns></returns>
        public CategoryAttribute Get(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [cate_attr_id] as Id
                       ,[category_id] as CategoryId
                       ,[attr_id] as AttributeId
                       ,[cate_attr_isdel] as IsDel
                 FROM  [dbo].[CategoryAttributes] NOLOCK
                 WHERE cate_attr_id=@cate_attr_id";

                var param = new SqlParam();
                param.AddParam("cate_attr_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryAttribute>.GetEntity(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据ID返回信息（默认未删除的）
        /// </summary>
        /// <param name="id">类目id</param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        public List<CategoryAttribute> GetCategoryAttr(int id, bool isDel = false)
        {
            try
            {
                string sql = @"SELECT  
                        [cate_attr_id] as Id
                       ,[category_id] as CategoryId
                       ,[attr_id] as AttributeId
                       ,[cate_attr_isdel] as IsDel
                 FROM  [dbo].[CategoryAttributes] NOLOCK
                 WHERE category_id=@category_id AND cate_attr_isdel=@cate_attr_isdel";

                var param = new SqlParam();
                param.AddParam("@category_id", id);
                param.AddParam("@cate_attr_isdel", isDel);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryAttribute>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据ID返回信息（默认未删除的）
        /// 修改人：邓福伟 时间：2014-5-19
        /// </summary>
        /// <param name="categoryId">类目id</param>
        /// <returns></returns>
        public List<CategoryAttributeEntity> GetCategoryAttrName(int categoryId)
        {
            try
            {
                string sql = @"SELECT ca.[cate_attr_id] as Id   
                                  ,ca.[category_id] as CategoryId
                                  ,ca.[attr_id] as AttributeId
                                  ,a.[attr_name] as AttributeName
                                  
                              FROM [dbo].[CategoryAttributes] as ca WITH (NOLOCK)
                              Left join [dbo].[Attributes] as a WITH (NOLOCK) on ca.[attr_id] = a.attr_id
                              
                              where ca.[category_id]=@category_id and ca.[cate_attr_isdel] =0 and a.[attr_isdel] = 0";

                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryAttributeEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", categoryId, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据类目ID返回类目、属性、属性值信息
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<CategoryAttributeValue> GetCategoryAttributeValue(int categoryId)
        {
            try
            {
                string sql = @"SELECT ca.[category_id] as CategoryId
                                  ,ca.[attr_id] as AttributeId
                                  ,a.[attr_name] as AttributeName
                                  ,av.[attrval_id] as AttributeValId
                                  ,av.[attrval_value] as AttributeValName
                              FROM [dbo].[CategoryAttributes] as ca WITH (NOLOCK)
                              Left join [dbo].[Attributes] as a WITH (NOLOCK) on ca.[attr_id] = a.attr_id
                              Left join [dbo].[AttributeValues] as av WITH (NOLOCK) on ca.[cate_attr_id] = av.[cate_attr_id]
                              where ca.[category_id]=@category_id and ca.[cate_attr_isdel] =0 and a.[attr_isdel] = 0 and av.[attrval_isdel] = 0";

                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<CategoryAttributeValue>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", categoryId, ex);
                return null;
            }
        }
        /// <summary>
        /// 插入记录返回执行条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(CategoryAttribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryAttributeDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[CategoryAttributes]
                       ([category_id]
                       ,[attr_id])
                 VALUES
                       (@category_id,
                        @attr_id)";

                var param = new SqlParam();
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("attr_id", model.AttributeId);

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
        public int AddReturnId(CategoryAttribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryAttributeDal.AddReturnId,参数不可为空");
                string sql = @"INSERT INTO [dbo].[CategoryAttributes]
                       ([category_id]
                       ,[attr_id])
                 VALUES
                       (@category_id,
                        @attr_id)
                 SELECT SCOPE_IDENTITY() AS Id";

                var param = new SqlParam();
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("attr_id", model.AttributeId);

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
        public CategoryAttribute AddReturnEntity(CategoryAttribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryAttributeDal.AddReturnEntity,参数不可为空");

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
        public bool Update(CategoryAttribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("CategoryAttributeDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[CategoryAttributes]
                                   SET [category_id] = @category_id
                                      ,[attr_id] = @attr_id
                                      ,[cate_attr_isdel] = @cate_attr_isdel
                                 WHERE cate_attr_id = @cate_attr_id";

                var param = new SqlParam();
                param.AddParam("cate_attr_id", model.Id);
                param.AddParam("category_id", model.CategoryId);
                param.AddParam("attr_id", model.AttributeId);
                param.AddParam("cate_attr_isdel", model.IsDel);

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
        /// <param name="id">类目属性id</param>
        /// <returns></returns>
        public bool DeleteFormDateBase(int id)
        {
            try
            {
                const string sql = @"DELETE FROM [dbo].[CategoryAttributes]
                                 WHERE cate_attr_id = @cate_attr_id";

                var param = new SqlParam();
                param.AddParam("cate_attr_id", id);


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
                const string sql = @"UPDATE [dbo].[CategoryAttributes]
                                   SET [cate_attr_isdel] = 1
                                 WHERE cate_attr_id = @cate_attr_id";

                var param = new SqlParam();
                param.AddParam("cate_attr_id", id);

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return false;
            }
        }
    }
}
