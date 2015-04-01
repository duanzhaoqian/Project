using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.DAL.RentalGoodses
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/23 10:13:41
    /// 描述：出租商品扩展操作类
    /// </summary>
    public class RentalOtherDal
    {
        /// <summary>
        /// 根据ID返回信息
        /// </summary>
        /// <param name="id">出租商品id</param>
        /// <returns></returns>
        public RentalOther Get(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [other_id] as Id
                       ,[rental_id] as RentalId
                       ,[other_desc] as Descs
                       ,[other_attrs] as Attrs
                       ,[other_colors] as Colors
                       ,[other_norms] as Norms
                        ,[other_prices] as Prices
                 FROM  [dbo].[RentalOthers] NOLOCK
                 WHERE rental_id=@rental_id";

                var param = new SqlParam();
                param.AddParam("@rental_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<RentalOther>.GetEntityList(dt)[0];
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
        public int Add(RentalOther model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("RentalOtherDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[RentalOthers]
                       ([rental_id]
                       ,[other_desc]
                       ,[other_attrs]
                       ,[other_colors]
                       ,[other_norms]
                        ,[other_prices])
                 VALUES
                       (@rental_id,
                        @other_desc,
                        @other_attrs,
                        @other_colors,
                        @other_norms,
                         @other_prices)";

                var param = new SqlParam();
                param.AddParam("rental_id", model.RentalId);
                param.AddParam("other_desc", model.Descs);
                param.AddParam("other_attrs", model.Attrs);
                param.AddParam("other_colors", model.Colors);
                param.AddParam("other_norms", model.Norms);
                param.AddParam("other_prices", model.Prices);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return 0;
            }

        }

        /// <summary>
        /// 根据出售商品表Id，更新实体表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(RentalOther model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("RentalOtherDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[RentalOthers]
                                   SET [rental_id] = @rental_id
                                      ,[other_desc] = @other_desc
                                      ,[other_attrs] = @other_attrs
                                      ,[other_colors] = @other_colors
                                      ,[other_norms] = @other_norms
                                        ,[other_prices] = @other_prices
                                 WHERE rental_id = @rental_id";

                var param = new SqlParam();
                param.AddParam("rental_id", model.RentalId);
                param.AddParam("other_desc", model.Descs);
                param.AddParam("other_attrs", model.Attrs);
                param.AddParam("other_colors", model.Colors);
                param.AddParam("other_norms", model.Norms);
                param.AddParam("other_prices", model.Prices);

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
                const string sql = @"DELETE FROM [dbo].[RentalOthers]
                                 WHERE rental_id = @rental_id";

                var param = new SqlParam();
                param.AddParam("rental_id", id);

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
