using TXOrm;
using System.Collections.Generic;
using System.Linq;
using System;
using TXModel.Commons;
using TXModel.Web;
using System.Data.SqlClient;

namespace TXDal.HouseData
{
    public partial class BuildingDal
    {

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/12 17:08:17
        /// 功能描述：楼栋分页列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="DeveloperId"></param>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public Paging<PremisesBuilding> GetBuildingList(Paging<PremisesBuilding> paging, int DeveloperId, int PremisesId)
        {
            string sql = string.Empty;
            string sqlCount = String.Empty;

            #region 拼接分页SQL
            sql = @"SELECT * FROM (
            SELECT ROW_NUMBER() OVER(ORDER BY Id DESC) AS Row, [Id]
      ,[DeveloperId]
      ,[PremisesId]
      ,[Periods]
      ,[PropertyType]
      ,[CreateTime]
      ,[PictureData]
      ,[Name]
      ,[NameType]
      ,[FamilyNum]
      ,[TheFloor]
      ,[Underloor]
      ,[Ladder]
      ,[Renovation]
      ,[BuildingPosition]
      ,[OpeningTime]
      ,[OthersTime]
      ,[State]
      ,BuildingType=(select BuildingType from Premises where Premises.Id=PremisesId)
      ,[PermitPresale]
      ,[PNum]
      ,[UpdateTime]
      ,[IsDel]  FROM Building WHERE 1 = 1 and IsDel=0 {0}
            And DeveloperId={1} 
            ) AS tempTable
            WHERE Row BETWEEN {2} AND {3}";
            if (PremisesId != 0)
            {
                sql = String.Format(sql, String.Format(" AND PremisesId ={0}", PremisesId), DeveloperId, ((paging.PageIndex - 1) * paging.PageSize) + 1, paging.PageIndex * paging.PageSize);
            }
            else
            {
                sql = String.Format(sql, "", DeveloperId, ((paging.PageIndex - 1) * paging.PageSize) + 1, paging.PageIndex * paging.PageSize);
            }

            #endregion

            #region 拼接总记录SQL
            sqlCount = @"SELECT COUNT(1) FROM Building WHERE 1 = 1 and IsDel=0 {0} and DeveloperId={1} ";
            if (PremisesId != 0)
            {
                sqlCount = String.Format(String.Format(sqlCount, " AND PremisesId ={0}", DeveloperId), PremisesId);
            }
            else
            {
                sqlCount = String.Format(sqlCount, "", DeveloperId);
            }
            sqlCount = String.Format(sqlCount, DeveloperId);
            #endregion

            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    var query = db.ExecuteStoreQuery<PremisesBuilding>(sql);
                    List<PremisesBuilding> p = query.ToList();
                    paging.ResultData = p;
                    #endregion

                    #region 查询总记录数据
                    var queryCount = db.ExecuteStoreQuery<int>(sqlCount);
                    paging.TotalCount = queryCount.FirstOrDefault<int>();
                    #endregion
                }


            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("DeveloperId:{0},PremisesId:{1}", DeveloperId, PremisesId), ex);
                //throw;
            }
            return paging;
        }


        #region 楼栋名称在同楼盘名称下不能相重
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/27 15:32:16
        /// 功能描述：楼栋名称在同楼盘名称下不能相重，房源名称（房号）在同楼栋下不能相重，如相重则提示：“楼栋名称已存在”、
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="NameType"></param>
        /// <returns></returns>
        public int CheckBuilding(string Name, string NameType, int PremisesId)
        {
            int i = 0;
            try
            {

                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = "select count(1) as result from Building WITH(NoLock) where PremisesId=@PremisesId and [Name]=@Name and NameType=@NameType and IsDel=0";
                    SqlParameter[] param = new SqlParameter[]{
                            new SqlParameter("@PremisesId",PremisesId),
                            new SqlParameter("@Name",Name),
                            new SqlParameter("@NameType",NameType)
                    };
                    i = dbEnt.ExecuteStoreQuery<int>(sql, param).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("Name:{0},NameType:{1}", Name, NameType), ex);
                //throw;
            }
            return i;
        }
        #endregion


        #region 获取沙盘标号列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/21 14:18:14
        /// 功能描述：获取沙盘标号列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public List<int> GetPNumList(int PremisesId)
        {
            List<int> list = null;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = "select Pnum from Building where PremisesId=@PremisesId and Isdel=0";
                    SqlParameter[] parma ={
                       new SqlParameter("@PremisesId",PremisesId)
                    };
                    list = dbEnt.ExecuteStoreQuery<int>(sql, parma).ToList();

                }

            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("PremisesId:{0}", PremisesId), ex);
                //throw;
            }
            return list;
        }
        #endregion
    }
}