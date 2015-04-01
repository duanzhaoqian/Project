using System.Collections.Generic;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using TXNewHouseServices.models;

namespace TXNewHouseServices.dal
{
    public class PremisesFeatureDal : BaseDal
    {
        /// <summary>
        /// 获取楼盘列表
        /// </summary>
        /// <returns></returns>
        public IList<PremisesFeatureEntity> GetPremisesList()
        {
            const string sql = @"
SELECT  Id, Name, IsDel
FROM    PremisesFeature (NOLOCK)";

            var ds = SqlHelper.ExecuteDataset(ConnStr_NewHouseDB, CommandType.Text, sql);

            if (null != ds && 0 < ds.Tables.Count)
            {
                var t = ds.Tables[0];
                var list = GetEntities<PremisesFeatureEntity>(t);
                return list;
            }

            return null;
        }
    }
}