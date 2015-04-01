using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.DAL.SaleGoodses
{
    using KYJ.ZS.Commons;
    using KYJ.ZS.DAL.DB;
    using KYJ.ZS.Models.DB;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/23 10:18:04
    /// 描述：出售商品扩展操作类
    /// </summary>
    public class SaleOtherDal
    {
        /// <summary>
        /// 根据ID返回信息
        /// </summary>
        /// <param name="id">出售商品id</param>
        /// <returns></returns>
        public SaleOther Get(int id)
        {
            try
            {
                string sql = @"SELECT  
                        [other_id] as Id
                       ,[sale_id] as SaleId
                       ,[other_desc] as Descs
                 FROM  [dbo].[SaleOthers] NOLOCK
                 WHERE sale_id=@sale_id";

                var param = new SqlParam();
                param.AddParam("@sale_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<SaleOther>.GetEntityList(dt)[0];
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
        public int Add(SaleOther model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("SaleOtherDal.Add,参数不可为空");
                string sql = @"INSERT INTO [dbo].[SaleOthers]
                       ([sale_id]
                       ,[other_desc])
                 VALUES
                       (@sale_id,
                        @other_desc)";

                var param = new SqlParam();
                param.AddParam("sale_id", model.SaleId);
                param.AddParam("other_desc", model.Descs);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return 0;
            }

        }

        /// <summary>
        /// 根据实体更新表，根据出售商品ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(SaleOther model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("SaleOtherDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[SaleOthers]
                                   SET [other_desc] = @other_desc
                                 WHERE sale_id = @sale_id";

                var param = new SqlParam();
                param.AddParam("sale_id", model.SaleId);
                param.AddParam("other_desc", model.Descs);

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
                const string sql = @"DELETE FROM [dbo].[SaleOthers]
                                 WHERE sale_id = @sale_id";

                var param = new SqlParam();
                param.AddParam("sale_id", id);

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
