using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.DAL.Attributes
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Models.View;
    using System.Data;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 11:21:39
    /// 描述：属性值操作类
    /// </summary>
    public class AttributeValueDal
    {
        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">属性值id</param>
        /// <returns></returns>
        public Models.DB.AttributeValue Get(int id)
        {
            try
            {
                string sql = @"SELECT  
                       [attrval_id] as Id
                      ,[cate_attr_id] as CategoryAttributeId
                      ,[attrval_sort] as Sort
                      ,[attrval_value] as Value
                      ,[attrval_createtime] as CreateTime
                      ,[attrval_updatetime] as UpdateTime
                      ,[attrval_isdel] as IsDel
                  FROM [dbo].[AttributeValues] NOLOCK
                 WHERE attrval_id=@attrval_id";

                var param = new SqlParam();
                param.AddParam("attrval_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Models.DB.AttributeValue>.GetEntity(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据类目属性Id，得到此属性的属性值
        /// </summary>
        /// <param name="id">类目属性id</param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        public List<AttributeValue> GetAttrValue(int id, bool isDel = false)
        {
            try
            {
                string sql = @"SELECT  
                       [attrval_id] as Id
                      ,[cate_attr_id] as CategoryAttributeId
                      ,[attrval_sort] as Sort
                      ,[attrval_value] as Value
                      ,[attrval_createtime] as CreateTime
                      ,[attrval_updatetime] as UpdateTime
                      ,[attrval_isdel] as IsDel
                  FROM [dbo].[AttributeValues] NOLOCK
                 WHERE cate_attr_id=@cate_attr_id AND attrval_isdel=@attrval_isdel";

                var param = new SqlParam();
                param.AddParam("cate_attr_id", id);
                param.AddParam("@attrval_isdel", isDel);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                var list = new List<AttributeValue>();
                if (!Auxiliary.CheckDt(dt))
                    return list;

                return DataHelper<AttributeValue>.GetEntityList(dt);
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
        public int Add(Models.DB.AttributeValue model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeValueDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[AttributeValues]
                        ([cate_attr_id]
                         ,[attrval_sort]
                         ,[attrval_value]
                         ,[attrval_createtime]
                         ,[attrval_updatetime])
                 VALUES
                       (@cate_attr_id,
                        @attrval_sort,
                        @attrval_value,
                        @attrval_createtime,
                        @attrval_updatetime)";

                var param = new SqlParam();
                param.AddParam("cate_attr_id", model.CategoryAttributeId);
                param.AddParam("attrval_sort", model.Sort);
                param.AddParam("attrval_value", model.Value);
                param.AddParam("attrval_createtime", model.CreateTime);
                param.AddParam("attrval_updatetime", model.UpdateTime);
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
        public int AddReturnId(Models.DB.AttributeValue model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeValueDal.AddReturnId,参数不可为空");
                string sql = @"INSERT INTO [dbo].[AttributeValues]
                        ([cate_attr_id]
                         ,[attrval_sort]
                         ,[attrval_value]
                         ,[attrval_createtime]
                         ,[attrval_updatetime])
                 VALUES
                       (@cate_attr_id,
                        @attrval_sort,
                        @attrval_value,
                        @attrval_createtime,
                        @attrval_updatetime)
                SELECT SCOPE_IDENTITY() AS Id";

                var param = new SqlParam();
                param.AddParam("cate_attr_id", model.CategoryAttributeId);
                param.AddParam("attrval_sort", model.Sort);
                param.AddParam("attrval_value", model.Value);
                param.AddParam("attrval_createtime", model.CreateTime);
                param.AddParam("attrval_updatetime", model.UpdateTime);

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
        public Models.DB.AttributeValue AddReturnEntity(Models.DB.AttributeValue model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeValueDal.AddReturnEntity,参数不可为空");

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
        public bool Update(AttributeValue model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeValueDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[AttributeValues]
                                   SET [cate_attr_id] = @cate_attr_id
                                      ,[attrval_sort] = @attrval_sort
                                      ,[attrval_value] = @attrval_value
                                      ,[attrval_createtime] = @attrval_createtime
                                      ,[attrval_updatetime] = @attrval_updatetime
                                      ,[attrval_isdel] = @attrval_isdel
                                 WHERE attr_id = @attr_id";

                var param = new SqlParam();
                param.AddParam("attrval_id", model.Id);
                param.AddParam("cate_attr_id", model.CategoryAttributeId);
                param.AddParam("attrval_sort", model.Sort);
                param.AddParam("attrval_value", model.Value);
                param.AddParam("attrval_createtime", model.CreateTime);
                param.AddParam("attrval_updatetime", model.UpdateTime);
                param.AddParam("attrval_isdel", model.IsDel);
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
                const string sql = @"DELETE FROM [dbo].[AttributeValues]
                                 WHERE attrval_id = @attrval_id";

                var param = new SqlParam();
                param.AddParam("attrval_id", id);


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
                const string sql = @"UPDATE [dbo].[AttributeValues]
                                   SET [attrval_isdel] = 1
                                 WHERE attrval_id = @attrval_id";

                var param = new SqlParam();
                param.AddParam("attrval_id", id);

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
        /// 获取属性值列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="attrId">属性ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectAttrValues(int categoryId, int attrId)
        {
            try
            {
                var sql = @"select attrval_id as Code,attrval_value as Name 
                            from AttributeValues(NOLOCK)
                            where attrval_isdel='false' 
                            and cate_attr_id=
                            (select cate_attr_id from CategoryAttributes(NOLOCK) 
                            where category_id=@category_id and attr_id=@attr_id and cate_attr_isdel='false')";
                var param = new SqlParam();
                param.AddParam("@category_id", categoryId);
                param.AddParam("@attr_id", attrId);
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

        #region 添加属性值

        /// <summary>
        /// 添加属性值，返回属性值ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="attrId">属性ID</param>
        /// <param name="name">属性值名称</param>
        /// <returns></returns>
        public int Create(int categoryId, int attrId, string name)
        {
            try
            {
                string sql = @"declare @cate_attr_id int --分类属性ID
                            /*分类属性*/
                            SET @cate_attr_id=(SELECT cate_attr_id FROM CategoryAttributes(NOLOCK) 
                                        WHERE cate_attr_isdel='false' and category_id=@category_id and attr_id=@attr_id)
                            /*属性值*/
                            INSERT INTO  [dbo].[AttributeValues]
                                    ([cate_attr_id]
                                    ,[attrval_sort]
                                    ,[attrval_value]
                                    ,[attrval_createtime]
                                    ,[attrval_updatetime])
                            VALUES
                                    (@cate_attr_id
                                    ,0
                                    ,@attrval_value
                                    ,@createtime
                                    ,@createtime)
                            SELECT @@IDENTITY";

                var param = new SqlParam();
                param.AddParam("@attr_id", attrId);
                param.AddParam("@category_id", categoryId);
                param.AddParam("@attrval_value", name);
                param.AddParam("@createtime", DateTime.Now);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", categoryId + "," + name, ex);
                return 0;
            }
        }

        #endregion

        #region 修改属性值

        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="id">属性值ID</param>
        /// <param name="name">属性值名称</param>
        /// <returns></returns>
        public int Edit(int id, string name)
        {
            try
            {
                string sql = @"UPDATE  [dbo].[AttributeValues]
                            SET     attrval_value=@attrval_value
                                    ,attrval_updatetime=@updatetime
                            WHERE   attrval_id=@attrval_id";

                var param = new SqlParam();
                param.AddParam("@attrval_value", name);
                param.AddParam("@attrval_id", id);
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

        #region 删除属性值

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="id">属性值ID</param>
        /// <returns></returns>
        public int DeleteAttrValue(int id)
        {
            try
            {
                string sql = @"UPDATE  [dbo].[AttributeValues]
                            SET     attrval_isdel='true'
                            WHERE   attrval_id=@attrval_id";

                var param = new SqlParam();
                param.AddParam("@attrval_id", id);

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
