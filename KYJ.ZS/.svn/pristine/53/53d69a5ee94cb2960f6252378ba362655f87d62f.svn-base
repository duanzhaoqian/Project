using System;
using System.Collections.Generic;

namespace KYJ.ZS.DAL.Attributes
{
    using System.Data;
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Models.View;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 9:12:26
    /// 描述：商品属性操作类
    /// </summary>
    public class AttributeDal
    {
        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">属性id</param>
        /// <returns></returns>
        public Attribute Get(int id)
        {
            try
            {
                string sql = @"SELECT  
                       [attr_id] as Id
                       ,[attr_name] as Name
                       ,[attr_sort] as Sort
                       ,[attr_isdel] as IsDel
                 FROM  [dbo].[Attributes] NOLOCK
                 WHERE attr_id=@attr_id";

                var param = new SqlParam();
                param.AddParam("attr_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Attribute>.GetEntity(dt.Rows[0]);
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
        public int Add(Attribute model)
        {            
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[Attributes]
                       ([attr_name]
                       ,[attr_sort])
                 VALUES
                       (@attr_name,
                        @attr_sort)";

                var param = new SqlParam();
                param.AddParam("attr_name", model.Name);
                param.AddParam("attr_sort", model.Sort);

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
        public int AddReturnId(Attribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeDal.AddReturnId,参数不可为空");
                string sql = @"INSERT INTO [dbo].[Attributes]
                       ([attr_name]
                       ,[attr_sort])
                 VALUES
                       (@attr_name,
                        @attr_sort)
                 SELECT SCOPE_IDENTITY() AS Id";

                var param = new SqlParam();
                param.AddParam("attr_name", model.Name);
                param.AddParam("attr_sort", model.Sort);

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
        public Attribute AddReturnEntity(Attribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeDal.AddReturnEntity,参数不可为空");

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
        public bool Update(Attribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("AttributeDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[Attributes]
                                   SET [attr_name] = @attr_name
                                      ,[attr_sort] = @attr_sort
                                      ,[attr_isdel] = @attr_isdel
                                 WHERE attr_id = @attr_id";

                var param = new SqlParam();
                param.AddParam("attr_id", model.Id);
                param.AddParam("attr_name", model.Name);
                param.AddParam("attr_sort", model.Sort);
                param.AddParam("attr_isdel", model.IsDel);
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
                const string sql = @"DELETE FROM [dbo].[Attributes]
                                 WHERE attr_id = @attr_id";

                var param = new SqlParam();
                param.AddParam("attr_id", id);


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
                const string sql = @"UPDATE [dbo].[Attributes]
                                   SET [attr_isdel] = 1
                                 WHERE attr_id = @attr_id";

                var param = new SqlParam();
                param.AddParam("attr_id", id);

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
        /// 获取属性列表
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <returns></returns>
        public IList<AttrCategorySelectEntity> GetSelectAttrs(int categoryId)
        {
            try
            {
                var sql = @"select t1.attr_id as Code,attr_name as Name 
                            from Attributes(NOLOCK) t1 left join CategoryAttributes(NOLOCK) t2 on t1.attr_id=t2.attr_id 
                            where attr_isdel='false' and cate_attr_isdel='false' and category_id=@category_id";
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

        #region 添加属性

        /// <summary>
        /// 添加属性，返回属性ID
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public int Create(int categoryId, string name)
        {
            try
            {
                string sql = @"begin tran
                            declare @attrId int --属性ID
                            /*属性*/
                            INSERT INTO  [dbo].[Attributes]
                                    ([attr_name]
                                    ,[attr_sort])
                            VALUES
                                    (@attr_name,0)
                            SET @attrId=(SELECT @@IDENTITY)
                            /*分类属性*/
                            INSERT INTO  [dbo].[CategoryAttributes]
                                    ([category_id]
                                    ,[attr_id])
                            VALUES
                                    (@category_id
                                    ,@attrId)
                            /*判断事务回滚或提交*/
	                        if @@error<>0 --判断有任何一条出现错误
	                        begin 
		                        rollback tran --开始执行事务的回滚
		                        select 0
	                        end
	                        else
	                        begin
		                        commit tran --执行这个事务的操作
		                        select @attrId
	                        end";

                var param = new SqlParam();
                param.AddParam("@attr_name", name);
                param.AddParam("@category_id", categoryId);

                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", categoryId + "," + name, ex);
                return 0;
            }
        }

        #endregion

        #region 修改属性

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="id">属性ID</param>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public int Edit(int id, string name)
        {
            try
            {
                string sql = @"UPDATE  [dbo].[Attributes]
                            SET     attr_name=@attr_name
                            WHERE   attr_id=@attr_id";

                var param = new SqlParam();
                param.AddParam("@attr_name", name);
                param.AddParam("@attr_id", id);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id + "," + name, ex);
                return 0;
            }
        }

        #endregion

        #region 删除属性

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="categoryId">类目ID</param>
        /// <param name="attrId">属性ID</param>
        /// <returns></returns>
        public int DeleteAttr(int categoryId, int attrId)
        {
            try
            {
                string sql = @"begin tran
                            /*属性*/
                            UPDATE  [dbo].[Attributes]
                            SET     attr_isdel='true'
                            WHERE   attr_id=@attr_id
                            /*分类属性*/
                            UPDATE  [dbo].[CategoryAttributes]
                            SET     cate_attr_isdel='true'
                            WHERE   attr_id=@attr_id and category_id=@category_id
                            /*判断事务回滚或提交*/
	                        if @@error<>0 --判断有任何一条出现错误
	                        begin 
		                        rollback tran --开始执行事务的回滚
		                        select 0
	                        end
	                        else
	                        begin
		                        commit tran --执行这个事务的操作
		                        select 1
	                        end";

                var param = new SqlParam();
                param.AddParam("@attr_id", attrId);
                param.AddParam("@category_id", categoryId);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", categoryId + "," + attrId, ex);
                return 0;
            }
        }

        #endregion

        #endregion
    }
}
