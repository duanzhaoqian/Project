using TXOrm;
using System;
using System.Collections.Generic;
using System.Linq;
using TXModel.Commons;

namespace TXDal.HouseData
{
    /// <summary>
    /// 楼盘DAL类-开发商后台
    /// </summary>
    public partial class PremisesDal
    {

        #region 楼盘分页列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/12 15:37:17
        /// 功能描述：楼盘分页列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public Paging<Premis> GetPremisList(Paging<Premis> paging, int DeveloperId)
        {
            string sql = String.Empty;
            string sqlCount = String.Empty;

            #region 拼接分页SQL
            sql = @"SELECT * FROM (
            SELECT ROW_NUMBER() OVER(ORDER BY UpdateTime DESC) AS Row, * FROM Premises WHERE 1 = 1
            and IsDel=0 And DeveloperId={0} 
            ) AS tempTable
            WHERE Row BETWEEN {1} AND {2}";
            sql = String.Format(sql, DeveloperId, ((paging.PageIndex - 1) * paging.PageSize) + 1, paging.PageIndex * paging.PageSize);
            #endregion

            #region 拼接总记录SQL
            sqlCount = @"SELECT COUNT(1) FROM Premises WHERE 1 = 1 and IsDel=0  and DeveloperId={0}";
            sqlCount = String.Format(sqlCount, DeveloperId);
            #endregion

            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    var query = houseDB.ExecuteStoreQuery<Premis>(sql);
                    List<Premis> p = query.ToList();
                    paging.ResultData = p;
                    #endregion

                    #region 查询总记录数据
                    var queryCount = houseDB.ExecuteStoreQuery<int>(sqlCount);
                    paging.TotalCount = queryCount.FirstOrDefault<int>();
                    #endregion
                }
                return paging;

            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0},{1},{2}", DeveloperId), ex);
                throw;
            }
        }

        #endregion
    }
}