using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.GoodsColors
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-4-22
    /// 描述：商品颜色
    /// </summary>
    public class GoodsColorDal
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 描述：根据goodsId获取商品颜色
        /// </summary>
        /// <param name="goodsId">商品Id</param>
        /// <returns>商品颜色集合</returns>
        public List<GoodsColor> GetGoodsColorList(int goodsId)
        {
            try
            {
                #region SQL
                var sql = @"SELECT [goodscolor_id] as Id
                              ,[goods_id] as GoodsId
                              ,[color_id] as ColorId
                              ,[color_name] as ColorName
                              ,[color_code] as ColorCode
                              ,[color_type] as ColorType
                        FROM [dbo].[GoodsColors] with(nolock)
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
                return DataHelper<GoodsColor>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "商品颜色集合", ex);
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
        public int Add(GoodsColor model, bool isReturnId = false)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsColorDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[GoodsColors]
                       ([goods_id]
                       ,[color_id]
                       ,[color_name]
                       ,[color_code]
                        ,[color_type])
                 VALUES
                       (@goods_id,
                        @color_id,
                        @color_name,
                        @color_code,
                        @color_type)";

                var param = new SqlParam();
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("color_id", model.ColorId);
                param.AddParam("color_name", model.ColorName);
                param.AddParam("color_code", model.ColorCode);
                param.AddParam("color_type", model.ColorType);

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
        public bool Update(GoodsColor model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("GoodsColorDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[GoodsColors]
                                   SET [goods_id] = @goods_id
                                      ,[color_id] = @color_id
                                      ,[color_name] = @color_name
                                      ,[color_code] = @color_code
                                        ,[color_type] = @color_type
                                 WHERE goodscolor_id = @goodscolor_id";

                var param = new SqlParam();
                param.AddParam("goodscolor_id", model.Id);
                param.AddParam("goods_id", model.GoodsId);
                param.AddParam("color_id", model.ColorId);
                param.AddParam("color_name", model.ColorName);
                param.AddParam("color_code", model.ColorCode);
                param.AddParam("color_type", model.ColorType);

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
                const string sql = @"DELETE FROM [dbo].[GoodsColors]
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
