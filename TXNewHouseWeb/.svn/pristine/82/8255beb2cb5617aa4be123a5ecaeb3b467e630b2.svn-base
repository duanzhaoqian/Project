using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using TXNewHouseServices.models;

namespace TXNewHouseServices.dal
{
    public class PremisesViewCountDal : BaseDal
    {
        /// <summary>
        /// 获取楼盘列表
        /// </summary>
        /// <returns></returns>
        public IList<PremisesViewCountEntity> GetPremisesList()
        {
            const string sql = @"
SELECT  Id AS PremisesId, CityId, IsDel
FROM    Premises (NOLOCK)";

            var ds = SqlHelper.ExecuteDataset(ConnStr_NewHouseDB, CommandType.Text, sql);

            if (null != ds && 0 < ds.Tables.Count)
            {
                var t = ds.Tables[0];
                var list = GetEntities<PremisesViewCountEntity>(t);
                return list;
            }

            return null;
        }

        /// <summary>
        /// 添加排名结果
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public int AddRankResult(IList<PremisesViewCountEntity> premises)
        {
            var sql = new StringBuilder();
            sql.Append(@"
BEGIN TRY
    BEGIN TRAN");
            foreach (var premise in premises)
            {
                sql.AppendFormat(@"
    INSERT  INTO PremisesViewCount ( PremisesId, CityId, ViewCount, RankNum, RankTime )
    VALUES  ( {0}, {1}, {2}, {3}, GETDATE() )", premise.PremisesId, premise.CityId, premise.ViewCount, premise.RankNum); // CONVERT(VARCHAR(100), GETDATE(), 23)
            }
            sql.Append(@"
    COMMIT TRAN
    SELECT  '1' AS Result
END TRY
BEGIN CATCH
    ROLLBACK TRAN
    SELECT  '0' AS Result
END CATCH");

            var result = SqlHelper.ExecuteScalar(ConnStr_NewHouseDB, CommandType.Text, sql.ToString());

            if (null != result)
            {
                return Convert.ToInt32(result);
            }

            return 0;
        }
    }
}