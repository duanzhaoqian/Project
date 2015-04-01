using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.DAL.GoodsAttributes
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/23 9:10:40
    /// 描述：商品属性实体类
    /// </summary>
    public class GoodsAttributeDal
    {
        /// <summary>
        /// 根据ID返回信息
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public List<GoodsAttribute> GetList(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [goodsattr_id] as Id
                       ,[goods_id] as GoodsId
                       ,[cate_attr_id] as CategoryAttrId
                       ,[attrval_id] as AttrValId
                       ,[goodsattr_value] as GoodsAttrVal
                       ,[goodsattr_type] as GoodsAttrType
                 FROM  [dbo].[GoodsAttributes] NOLOCK
                 WHERE goods_id=@goods_id";

                var param = new SqlParam();
                param.AddParam("@goods_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                var list = new List<GoodsAttribute>();
                if (!Auxiliary.CheckDt(dt))
                    return list;

                return DataHelper<GoodsAttribute>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return null;
            }
        }

        /// <summary>
        /// 插入记录返回结果（返回结果默认执行条数，isReturnId=true返回记录ID）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(GoodsAttribute model, bool isReturnId = false)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsAttributeDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[GoodsAttributes]
                       ([goods_id]
                       ,[cate_attr_id]
                       ,[attrval_id]
                       ,[goodsattr_value]
                       ,[goodsattr_type])
                 VALUES
                       (@goods_id,
                        @cate_attr_id,
                        @attrval_id,
                        @goodsattr_value,
                        @goodsattr_type)";

                var param = new SqlParam();
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("cate_attr_id", model.CategoryAttrId);
                param.AddParam("attrval_id", model.AttrValId);
                param.AddParam("goodsattr_value", model.GoodsAttrVal);
                param.AddParam("goodsattr_type", model.GoodsAttrType);

                if (isReturnId)
                    return KYJ_ZushouWDB.SqlExecuteRuturnId(sql, param);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return 0;
            }

        }

        /// <summary>
        /// 根据实体更新表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(GoodsAttribute model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsAttributeDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[GoodsAttributes]
                                   SET [goods_id] = @goods_id
                                      ,[cate_attr_id] = @cate_attr_id
                                      ,[attrval_id] = @attrval_id
                                      ,[goodsattr_value] = @goodsattr_value
                                      ,[goodsattr_type] = @goodsattr_type
                                 WHERE goodsattr_id = @goodsattr_id";

                var param = new SqlParam();
                param.AddParam("goodsattr_id", model.Id);
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("cate_attr_id", model.CategoryAttrId);
                param.AddParam("attrval_id", model.AttrValId);
                param.AddParam("goodsattr_value", model.GoodsAttrVal);
                param.AddParam("goodsattr_type", model.GoodsAttrType);

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
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public bool DeleteFormDateBase(int id)
        {
            try
            {
                const string sql = @"DELETE FROM [dbo].[GoodsAttributes]
                                 WHERE goods_id = @goods_id";

                var param = new SqlParam();
                param.AddParam("goods_id", id);

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
