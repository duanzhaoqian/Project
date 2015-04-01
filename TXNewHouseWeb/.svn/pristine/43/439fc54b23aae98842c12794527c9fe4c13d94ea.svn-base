using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TXModel.Commons;
using TXOrm;

namespace TXDal.NHouseActivies.Discount
{
    /// <summary>
    /// 折扣
    /// </summary>
    public partial class DiscountDal
    {
        #region 通用方法

        /// <summary>
        /// 根据折扣编号更新折扣信息
        /// </summary>
        /// <param name="entity">折扣实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return 0;
        }

        /// <summary>
        /// 逻辑删除折扣信息
        /// </summary>
        /// <param name="id">折扣编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            return 0;
        }

        /// <summary>
        /// 根据编号获取折扣实体
        /// </summary>
        /// <param name="id">折扣编号</param>
        /// <returns>返回：折扣实体</returns>
        public Activity GetEntity_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Activities.FirstOrDefault(active => active.Id == id && active.Type == 2 && active.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("Id:{0}", new object[] {id}), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据活动编号获取楼盘实体
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <returns>返回楼栋实体，否则返回null</returns>
        public Premis GetPremisesEntity_ByActivitiesId(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    const string sql = @"
SELECT  *
FROM    Premises (NOLOCK) AS a
WHERE   Id = ( SELECT TOP 1
                        PremisesId
               FROM     ActivitiesHouse (NOLOCK) AS a1
               WHERE    a1.ActivitiesId = @ActivitiesId
                        AND a1.IsDel = 0
             )
        AND a.IsDel = 0";
                    var sqlParms = new object[]
                    {
                        new SqlParameter("ActivitiesId", id)
                    };

                    var result = db.ExecuteStoreQuery<Premis>(sql, sqlParms).ToList();

                    return (0 < result.Count ? result[0] : null);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("Id:{0}", new object[] {id}), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据编号集合获取实体列表
        /// </summary>
        /// <param name="ids">编号集合字符串(逗号分隔)</param>
        /// <returns>返回：列表</returns>
        public List<object> GetList_ByIds(string ids)
        {
            return null;
        }

        /// <summary>
        /// 根据搜素条件获取分页列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public Paging<object> GetPageList_BySearch(object search, int pageIndex, int pageSize)
        {
            return null;
        }

        #endregion
    }
}