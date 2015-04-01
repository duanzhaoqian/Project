using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.DAL.FreightCosts
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：操作运费计算(数据库表FreightCosts)，包括查询、添加、修改
    /// </summary>
    public class FreightCostDal
    {
        /// <summary>
        /// 添加运费计算
        /// </summary>
        /// <param name="freightCost">运费计算</param>
        /// <returns></returns>
        public int Create(FreightCost freightCost)
        {
            #region SQL
            var sql = @"INSERT INTO [dbo].[FreightCosts]
                               ([ftemp_id]
                               ,[fcost_type]
                               ,[fcost_cityids]
                               ,[fcost_firstime]
                               ,[fcost_firstprice]
                               ,[fcost_continuetime]
                               ,[fcost_continueprice])
                         VALUES
                               (@ftemp_id
                               ,@fcost_type
                               ,@fcost_cityids
                               ,@fcost_firstime
                               ,@fcost_firstprice
                               ,@fcost_continuetime
                               ,@fcost_continueprice)";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@ftemp_id", freightCost.TempId);
            param.AddParam("@fcost_type", freightCost.Type);
            param.AddParam("@fcost_cityids", freightCost.CityIds);
            param.AddParam("@fcost_firstime", freightCost.FirstIme);
            param.AddParam("@fcost_firstprice", freightCost.FirstPrice);
            param.AddParam("@fcost_continuetime", freightCost.ContinuetIme);
            param.AddParam("@fcost_continueprice", freightCost.ContinuePrice);

            #endregion

            #region 执行

            return KYJ_ZushouWDB.SqlExecute(sql, param);

            #endregion
        }

        /// <summary>
        /// 修改运费计算
        /// </summary>
        /// <param name="freightCost">运费计算</param>
        /// <returns></returns>
        public int Update(FreightCost freightCost)
        {
            #region SQL
            var sql = @"UPDATE  [dbo].[FreightCosts]
                        SET    [fcost_type] = @fcost_type
                               ,[fcost_cityids] = @fcost_cityids
                               ,[fcost_firstime] = @fcost_firstime
                               ,[fcost_firstprice] = @fcost_firstprice
                               ,[fcost_continuetime] = @fcost_continuetime
                               ,[fcost_continueprice] = @fcost_continueprice
                        WHERE  [fcost_id] = @fcost_id";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@fcost_id", freightCost.Id);
            param.AddParam("@fcost_type", freightCost.Type);
            param.AddParam("@fcost_cityids", freightCost.CityIds);
            param.AddParam("@fcost_firstime", freightCost.FirstIme);
            param.AddParam("@fcost_firstprice", freightCost.FirstPrice);
            param.AddParam("@fcost_continuetime", freightCost.ContinuetIme);
            param.AddParam("@fcost_continueprice", freightCost.ContinuePrice);

            #endregion

            #region 执行

            return KYJ_ZushouWDB.SqlExecute(sql, param);

            #endregion
        }

        /// <summary>
        /// 删除运费计算（按运费计算ID）
        /// </summary>
        /// <param name="freightCostID">运费计算ID</param>
        /// <returns></returns>
        public int Delete(int freightCostID)
        {
            var sql = @"DELETE FROM  [dbo].[FreightCosts]
                      WHERE        fcost_id = @fcost_id";

            var param = new SqlParam();
            param.AddParam("@fcost_id", freightCostID);

            return KYJ_ZushouWDB.SqlExecute(sql, param);
        }

        /// <summary>
        /// 删除运费计算（按运费模板ID）
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public int DeleteByTemplateID(int freightTemplateID)
        {
            var sql = @"DELETE FROM  [dbo].[FreightCosts]
                      WHERE        ftemp_id = @ftemp_id";

            var param = new SqlParam();
            param.AddParam("@ftemp_id", freightTemplateID);

            return KYJ_ZushouWDB.SqlExecute(sql, param);
        }

        /// <summary>
        /// 获取运费计算
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <param name="cityID">运送城市ID</param>
        /// <returns></returns>
        public FreightCost GetFreightCost(int freightTemplateID, int cityID)
        {
            #region SQL
            var sql = @"SELECT 
                               [fcost_id] as Id
                               ,[ftemp_id] as TempId
                               ,[fcost_type] as Type
                               ,[fcost_cityids] as CityIds
                               ,[fcost_firstime] as FirstIme
                               ,[fcost_firstprice] as FirstPrice
                               ,[fcost_continuetime] as ContinuetIme
                               ,[fcost_continueprice] as ContinuePrice
                               ,[fcost_isdel] as IsDel
                         FROM  [dbo].[FreightCosts](NOLOCK)
                         WHERE [ftemp_id] = @ftemp_id and [fcost_cityids] like '%,'+ @cityID +',%' and [fcost_isdel] = 'false' ";
            #endregion
            var param = new SqlParam();
            param.AddParam("@ftemp_id", freightTemplateID);
            param.AddParam("@cityID", cityID);

            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);

            if (Auxiliary.CheckDt(dt))
                return DataHelper<FreightCost>.GetEntity(dt.Rows[0]);

            #region 查询默认全国运费计算
            sql = @"SELECT 
                            [fcost_id] as Id
                            ,[ftemp_id] as TempId
                            ,[fcost_type] as Type
                            ,[fcost_cityids] as CityIds
                            ,[fcost_firstime] as FirstIme
                            ,[fcost_firstprice] as FirstPrice
                            ,[fcost_continuetime] as ContinuetIme
                            ,[fcost_continueprice] as ContinuePrice
                            ,[fcost_isdel] as IsDel
                    FROM    [dbo].[FreightCosts](NOLOCK)
                    WHERE   [ftemp_id] = @ftemp_id and [fcost_cityids] = '-1' and [fcost_isdel] = 'false' ";

            param = new SqlParam();
            param.AddParam("@ftemp_id", freightTemplateID);

            dt = KYJ_ZushouRDB.GetTable(sql, param);
            return DataHelper<FreightCost>.GetEntity(dt.Rows[0]);
            #endregion
        }
    }
}
