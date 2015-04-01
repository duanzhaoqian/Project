using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using TXModel.AdminPVM;
using TXOrm;

namespace TXDal.HouseData
{
    public class PremiseImgMapDal
    {
        /// <summary>
        /// 获取楼盘图片分页信息
        /// </summary>
        /// <param name="cityid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public List<PremiseImgMap> GetPermitImgList(int cityid, int pageindex, int pagesize)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                       SELECT * FROM (
                                       SELECT  ROW_NUMBER() OVER ( ORDER BY CreateDate DESC ) AS RowID,* FROM  PremiseImgMap WHERE cityid= {0}
                                    ) temp  
			    WHERE   temp.RowID BETWEEN {1} AND {2} ",
                       cityid,
                        ((pageindex - 1) * pagesize) + 1,
                        pageindex * pagesize
                   );

                    #region 执行操作

                    ObjectResult<PremiseImgMap> query = houseDb.ExecuteStoreQuery<PremiseImgMap>(sql);
                    List<PremiseImgMap> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马欢", string.Format("({0},{1},{2})", new object[] { cityid, pageindex, pagesize }), ex);
                throw;
            }
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DelPermitImgMap(PremiseImgMap model)
        {
            string sql = string.Format("UPDATE dbo.PremiseImgMap SET ImgIsDel=1 ,adminid={0},adminName='{1}',EditDate=GETDATE() WHERE ID={2}", model.adminid, model.adminName, model.ID);
            using (var houseDb = new kyj_NewHouseDBEntities())
            {
                return houseDb.ExecuteStoreCommand(sql, null);
            }
        }
        /// <summary>
        /// 获取列表总数
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public int GetAllPermitImgCount(int cityid)
        {
            string sql =
                string.Format(
                    "SELECT count(*) FROM  PremiseImgMap WHERE cityid= {0}",
                    cityid);
            using (var houseDb = new kyj_NewHouseDBEntities())
            {
                var query = houseDb.ExecuteStoreQuery<int>(sql).ToList();
                if (query.Any())
                    return Convert.ToInt32(query.First());
            }
            return 0;
        }
        public List<PVM_NH_Premises> GetAllPremisesList()
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
                       SELECT * FROM (
                                       SELECT 
                                         premises.Id,
				                         premises.Name,
				                         premises.PropertyType,
				                         premises.Developer,
				                         premises.ProvinceID,
				                         premises.ProvinceName,
				                         premises.CityId,
				                         premises.CityName,
				                         premises.SalesStatus  
				                         FROM Premises as premises (NOLOCK)                    
				                         WHERE premises.IsDel=0  and  pageUrl IS NOT NULL

                                    ) HOSE  ";


                    #region 执行操作

                    ObjectResult<PVM_NH_Premises> query = houseDb.ExecuteStoreQuery<PVM_NH_Premises>(sql);
                    List<PVM_NH_Premises> ls = query.ToList();

                    #endregion

                    return ls;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
