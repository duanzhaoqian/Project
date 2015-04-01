using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXOrm;

namespace TXDal.FinancialData
{
    public class BondDal : BaseDal_Admin
    {

        /// <summary>
        /// 获取保证金列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<PVM_NH_Bond> GetPageList_BySearch_Bond(PVS_NH_Bond search, int pageIndex, int pageSize)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = @"
SELECT  z.EndTime, z.ProvinceId, z.ProvinceName, z.CityId, z.CityName, z.HouseId, z.BidUserName, z.BidUserMobile, z.Bond,
        z.BondStatus
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY a.Id ASC ) AS rowid, d.EndTime, c.ProvinceId, c.ProvinceName, c.CityId,
                    c.CityName, a.HouseId, a.BidUserName, a.BidUserMobile, d.Bond, a.BondStatus
          FROM      [Order] (NOLOCK) AS a
                    INNER JOIN House (NOLOCK) AS b ON b.Id = a.HouseId
                    INNER JOIN Premises (NOLOCK) AS c ON c.Id = b.PremisesId
                    INNER JOIN Activities (NOLOCK) AS d ON d.Id = a.ActivitiesId
          WHERE     1 = 1
                    {3}
                    {2}
                    {1}
                    {0}
                    AND a.IsDel = 0
        ) AS z
WHERE   z.rowid BETWEEN {4} AND {5}";

                    sql = string.Format(sql, GetParms_BySearch_Bond(search, pageIndex, pageSize));

                    var list = db.ExecuteStoreQuery<PVM_NH_Bond>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取保证金列表总数据
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public int GetTotalCount_BySearch_Bond(PVS_NH_Bond search)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = @"
SELECT  COUNT(1) AS Result
FROM    [Order] (NOLOCK) AS a
        INNER JOIN House (NOLOCK) AS b ON b.Id = a.HouseId
        INNER JOIN Premises (NOLOCK) AS c ON c.Id = b.PremisesId
        INNER JOIN Activities (NOLOCK) AS d ON d.Id = a.ActivitiesId
WHERE   1 = 1
        {3}
        {2}
        {1}
        {0}
        AND a.IsDel = 0";

                    sql = string.Format(sql, GetParms_BySearch_Bond(search));

                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarInt>(sql).ToList();

                    if (list.Any())
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", search, ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取保证金搜索条件
        /// author: liyuzhao
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private object[] GetParms_BySearch_Bond(PVS_NH_Bond search, int pageIndex = 0, int pageSize = 0)
        {
            var time = new StringBuilder();

            if (!string.IsNullOrEmpty(search.BeginTime))
            {
                time.Append("AND (");
                time.AppendFormat("'{0}' < DATEADD(DAY, 7, d.EndTime)", search.BeginTime);
                if (!string.IsNullOrEmpty(search.EndTime))
                {
                    time.AppendFormat(" AND DATEADD(DAY, 7, d.EndTime) < '{0}')", search.EndTime);
                }
                else
                {
                    time.Append(")");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(search.EndTime))
                {
                    time.AppendFormat("AND (DATEADD(DAY, 7, d.EndTime) < '{0}')", search.EndTime);
                }
            }

            var list = new List<object>
            {
                (0 < search.ProvinceId ? string.Format("AND c.ProvinceId = {0}", search.ProvinceId) : string.Empty),
                (0 < search.CityId ? string.Format("AND c.CityId = {0}", search.CityId) : string.Empty),
                (!string.IsNullOrEmpty(search.KeyWord) ? string.Format("AND ( a.HouseId LIKE '%{0}%' OR a.BidUserId LIKE '%{0}%' OR a.BidUserMobile LIKE '%{0}%' )", search.KeyWord) : string.Empty),
                time
            };

            if (pageIndex > 0
                && pageSize > 0)
            {
                int startIndex = (pageIndex - 1)*pageSize + 1;
                int endIndex = pageIndex*pageSize;

                list.Add(startIndex);
                list.Add(endIndex);
            }

            return list.ToArray();
        }
    }
}