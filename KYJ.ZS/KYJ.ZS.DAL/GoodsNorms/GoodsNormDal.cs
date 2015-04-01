using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.GoodsNorms
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-4-22
    /// 描述：商品规格值
    /// </summary>
    public class GoodsNormDal
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 描述：根据goodsId获取商品规格
        /// </summary>
        /// <param name="goodsId">商品Id</param>
        /// <returns>商品规格集合</returns>
        public List<GoodsNorm> GetGoodsNormList(int goodsId)
        {
            try
            {
                #region SQL
                var sql = @"SELECT [goodsnorm_id] as Id
                              ,[goods_id] as GoodsId
                              ,[norm_id] as NormId
                              ,[norm_name] as NormName
                              ,[norm_type] as NormType
                          FROM [dbo].[GoodsNorms] with(nolock)
                          WHERE goods_id=@goods_id ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@goods_id", goodsId, SqlDbType.Int, 4);
                #endregion
                #region 执行
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<GoodsNorm>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "商品规格集合", ex);
                return null;
            }
        }

        /// <summary>
        /// 作者：wangyu
        /// 时间：2014/4/23 9:10:40
        /// 描述：插入记录返回结果（返回结果默认执行条数，isReturnId=true返回记录ID）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isReturnId"></param>
        /// <returns></returns>
        public int Add(GoodsNorm model, bool isReturnId = false)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsNormDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[GoodsNorms]
                       ([goods_id]
                       ,[norm_id]
                       ,[norm_name]
                        ,[norm_type])
                 VALUES
                       (@goods_id,
                        @norm_id,
                        @norm_name,
                        @norm_type)";

                var param = new SqlParam();
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("norm_id", model.NormId);
                param.AddParam("norm_name", model.NormName);
                param.AddParam("norm_type", model.NormType);

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
        /// 作者：wangyu
        /// 时间：2014/4/23 9:10:40
        /// 描述：根据实体更新表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(GoodsNorm model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsNormDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[GoodsNorms]
                                   SET [goods_id] = @goods_id
                                      ,[norm_id] = @norm_id
                                      ,[norm_name] = @norm_name
                                        ,[norm_type] = @norm_type
                                 WHERE goodsnorm_id = @goodsnorm_id";

                var param = new SqlParam();
                param.AddParam("goodsnorm_id", model.Id);
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("norm_id", model.NormId);
                param.AddParam("norm_name", model.NormName);
                param.AddParam("norm_type", model.NormType);

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }

        /// <summary>
        /// 作者：wangyu
        /// 时间：2014/4/23 9:10:40
        /// 描述：根据ID，直接删除数据。数据库删除，谨慎操作。
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public bool DeleteFormDateBase(int id)
        {
            try
            {
                const string sql = @"DELETE FROM [dbo].[GoodsNorms]
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
