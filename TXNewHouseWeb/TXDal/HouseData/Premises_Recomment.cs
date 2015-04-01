using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TXCommons.Index;
using TXOrm;

namespace TXDal.HouseData
{
    /// <summary>
    /// 右侧推荐的热门，最新
    /// </summary>
    public class Premises_Recomment
    {
        public List<IndexRecList> GetHotList(int cityid, string districtId = null)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string sql = @"SELECT TOP 10  PremisesID ,
                            DName DistrictName ,
                            Name PremisesName,
                            ReferencePrice,
                            ISNULL(COUNT,0) AS ClickRate
                    FROM    PremisesClickStatistics (NOLOCK) click
                            INNER JOIN premises (NOLOCK) p ON click.premisesid = p.id
                                                              AND click.cityid = p.cityid
                                                              where p.isdel=0 and click.cityid=@cityid ";
            parameters.Add(new SqlParameter("@cityid", cityid));
            if (!string.IsNullOrWhiteSpace(districtId))
            {
                sql += " and p.Did=@Did";
                parameters.Add(new SqlParameter("@Did", districtId));
            }
            sql += " ORDER BY clickrate desc";
            using (var dbEnt = new kyj_NewHouseDBEntities())
            {
                var list = dbEnt.ExecuteStoreQuery<IndexRecList>(sql, parameters.ToArray()).ToList();
                if (list != null && list.Count > 0)
                    return list;
            }
            return new List<IndexRecList>();
        }
        public List<IndexRecList> GetNewList(int cityid, string districtId = null)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string sql = @"SELECT top 10 ID AS PremisesID ,
                            NAME AS PremisesName ,
                            DName AS DistrictName ,
                           ReferencePrice,
                           ISNULL(COUNT,0) AS ClickRate
                    FROM    premises (NOLOCK) p
                            LEFT JOIN PremisesClickStatistics (NOLOCK) click ON p.ID = click.PremisesID
                    WHERE p.isdel=0 and  p.cityid = @cityid ";
            parameters.Add(new SqlParameter("@cityid", cityid));
            if (!string.IsNullOrWhiteSpace(districtId))
            {
                sql += " and p.Did=@Did";
                parameters.Add(new SqlParameter("@Did", districtId));
            }
            sql += " ORDER BY p.id DESC";
            using (var dbEnt = new kyj_NewHouseDBEntities())
            {

                var list = dbEnt.ExecuteStoreQuery<IndexRecList>(sql, parameters.ToArray()).ToList();
                if (list != null && list.Count > 0)
                    return list;
            }
            return new List<IndexRecList>();
        }
    }
}
