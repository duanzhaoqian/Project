using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.DAL.GoodsPrices
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.DB;
    using System.Data;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/23 9:45:05
    /// 描述：商品价格实体类
    /// </summary>
    public class GoodsPriceDal
    {
        /// <summary>
        /// 根据ID返回信息
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public List<GoodsPrice> GetList(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [goodsprice_id] as Id
                       ,[goods_id] as GoodsId
                       ,[goodscolor_id] as ColorId
                       ,[goodsnorm_id] as NormId
                       ,[goods_price] as Price
                       ,[goodsprice_num] as Number
                        ,[goodsprice_code] as Code
                        ,[goodsprice_barcode] as Barcode
                 FROM  [dbo].[GoodsPrices] NOLOCK
                 WHERE goods_id=@goods_id";

                var param = new SqlParam();
                param.AddParam("@goods_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                var list = new List<GoodsPrice>();
                if (!Auxiliary.CheckDt(dt))
                    return list;

                return DataHelper<GoodsPrice>.GetEntityList(dt);
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
        /// <param name="isReturnId"></param>
        /// <returns></returns>
        public int Add(GoodsPrice model, bool isReturnId = false)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsPriceDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[GoodsPrices]
                       ([goods_id]
                       ,[goodscolor_id]
                       ,[goodsnorm_id]
                       ,[goods_price]
                       ,[goodsprice_num]
                        ,[goodsprice_code]
                        ,[goodsprice_barcode])
                 VALUES
                       (@goods_id,
                        @goodscolor_id,
                        @goodsnorm_id,
                        @goods_price,
                        @goodsprice_num,
                        @goodsprice_code,
                        @goodsprice_barcode)";

                var param = new SqlParam();
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("goodscolor_id", model.ColorId);
                param.AddParam("goodsnorm_id", model.NormId);
                param.AddParam("goods_price", model.Price);
                param.AddParam("goodsprice_num", model.Number);
                param.AddParam("goodsprice_code", model.Code);
                param.AddParam("goodsprice_barcode", model.Barcode);

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
        public bool Update(GoodsPrice model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsPriceDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[GoodsPrices]
                                   SET [goods_id] = @goods_id
                                      ,[goodscolor_id] = @goodscolor_id
                                      ,[goodsnorm_id] = @goodsnorm_id
                                      ,[goods_price] = @goods_price
                                      ,[goodsprice_num] = @goodsprice_num
                                        ,[goodsprice_code] = @goodsprice_code
                                        ,[goodsprice_barcode] = @goodsprice_barcode
                                 WHERE goodsprice_id = @goodsprice_id";

                var param = new SqlParam();
                param.AddParam("goodsattr_id", model.Id);
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("goodscolor_id", model.ColorId);
                param.AddParam("goodsnorm_id", model.NormId);
                param.AddParam("goods_price", model.Price);
                param.AddParam("goodsprice_num", model.Number);
                param.AddParam("goodsprice_code", model.Code);
                param.AddParam("goodsprice_barcode", model.Barcode);

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
                const string sql = @"DELETE FROM [dbo].[GoodsPrices]
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
        /// <summary>
        /// 作者：邓福伟
        /// 时间：201-4-23
        /// 描述：获取租售商品的价格Model
        /// </summary>
        /// <param name="goods_id">商品Id</param>
        /// <param name="goodscolor_id">商品颜色Id</param>
        /// <param name="goodsnorm_id">商品规格Id</param>
        /// <returns>返回租售商品价格Model</returns>
        public GoodsPrice GetModel(int goods_id, int goodscolor_id,int goodsnorm_id)
        {
            try
            {
                #region Sql
                var sql = @"SELECT [goodsprice_id] as Id
                                ,[goods_id] as GoodsId
                                ,[goodscolor_id] as ColorId
                                ,[goodsnorm_id] as NormId
                                ,[goods_price] as Price
                                ,[goodsprice_num] as Number
                        FROM [dbo].[GoodsPrices] with(nolock)
                        Where goods_id=@goods_id 
                            and goodscolor_id=@goodscolor_id 
                            and goodsnorm_id=@goodsnorm_id";
                #endregion
                #region 参数
                SqlParam parm = new SqlParam();
                parm.AddParam("@goods_id", goods_id, SqlDbType.Int, 4);
                parm.AddParam("@goodscolor_id", goodscolor_id, SqlDbType.Int, 4);
                parm.AddParam("@goodsnorm_id", goodsnorm_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, parm);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<GoodsPrice>.GetEntity(dt.Rows[0]);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "租售商品的价格Model", ex);
                return null;
            }
        }
    }
}
