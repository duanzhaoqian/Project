using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXOrm;
using System.Data.SqlClient;
using TXModel.Commons;

namespace TXDal.NHouseActivies.Advertise
{
    /// <summary>
    /// 广告管理
    /// </summary>
    public partial class AdvertiseDal
    {

        #region 根据ID获取广告

        /// <summary>
        /// 根据ID获取广告
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns>返回：广告实体</returns>
        public TXOrm.Advertise GetAdvertise_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
SELECT Id,CityId,CityName,Position,PremisesId,PremisesName,SequenceNum,BeginTime,EndTime,CreateTime,IsDel ,AdminID,AdminName
  FROM Advertise (NOLOCK) where Id={0} and IsDel=0";
                    sql = string.Format(sql, id);
                    var entity = db.ExecuteStoreQuery<TXOrm.Advertise>(sql).FirstOrDefault();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王东坤", string.Format("根据ID获取广告Id:{0}", new object[] { id }), ex);
                return null;
            }
        }

        #endregion

        #region 添加广告

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="model">对象实体</param>
        /// <returns>新增ID or 错误返回0</returns>
        public int AddAdvertise(TXOrm.Advertise model)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder(@"INSERT INTO Advertise
                                                     (CityId ,CityName ,Position,PremisesId ,PremisesName,SequenceNum,BeginTime,EndTime ,CreateTime ,IsDel,AdminID,AdminName)
                                                     VALUES
                                                    (@CityId,@CityName,@Position,@PremisesId,@PremisesName,@SequenceNum,@BeginTime,@EndTime,@CreateTime,@IsDel,@AdminID,@AdminName);
                                                    select @@IDENTITY");

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),
                        new SqlParameter("@Position", model.Position),
                        new SqlParameter("@PremisesId", model.PremisesId),
                        new SqlParameter("@PremisesName", model.PremisesName),
                        new SqlParameter("@BeginTime", model.BeginTime),
                         new SqlParameter("@SequenceNum", model.SequenceNum),
                        new SqlParameter("@EndTime", model.EndTime),
                        new SqlParameter("@CreateTime", model.CreateTime),
                        new SqlParameter("@IsDel", model.IsDel),
                        new SqlParameter("@AdminID", model.AdminID),
                        new SqlParameter("@AdminName", model.AdminName),
                       
                    };
                    int result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                Log4netService.RecordLog.RecordException("王东坤", string.Format("添加广告"), ex);
                return 0;
            }
        }

        #endregion

        #region 根据ID删除广告

        /// <summary>
        /// 根据ID删除广告
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns>受影响行数or 错误0</returns>
        public int DeleteAdvertise_ById(int id)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = @"UPDATE Advertise SET IsDel =1 WHERE ID=@ID";
                    SqlParameter[] parameters = { new SqlParameter("@ID", id) };
                    var count = kyj.ExecuteStoreCommand(sql, parameters);
                    return count;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("王东坤", string.Format("删除广告id({0})", id), e);
                return 0;
            }
        }

        #endregion

        #region 更新广告

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="?"></param>
        /// <returns>受影响行数OR 错误0</returns>
        public int UpdateAdvertise(TXOrm.Advertise model)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder(@"UPDATE Advertise  SET CityId = @CityId
                                                                            ,CityName = @CityName
                                                                            ,Position = @Position
                                                                            ,PremisesId = @PremisesId
                                                                            ,PremisesName = @PremisesName
                                                                            ,SequenceNum = @SequenceNum
                                                                            ,BeginTime = @BeginTime
                                                                            ,EndTime= @EndTime                                                                           
                                                                            ,IsDel = @IsDel
                                                                            ,AdminID = @AdminID
                                                                            ,AdminName =@AdminName
                                                                WHERE ID=@ID");

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@CityId", model.CityId),
                        new SqlParameter("@CityName", model.CityName),
                        new SqlParameter("@Position", model.Position),
                        new SqlParameter("@PremisesId", model.PremisesId),
                        new SqlParameter("@PremisesName", model.PremisesName),
                        new SqlParameter("@SequenceNum", model.SequenceNum),
                        new SqlParameter("@BeginTime", model.BeginTime),
                        new SqlParameter("@EndTime", model.EndTime),
                        new SqlParameter("@IsDel", model.IsDel),
                        new SqlParameter("@AdminID", model.AdminID),
                        new SqlParameter("@AdminName", model.AdminName),
                        new SqlParameter("@ID", model.Id)
                       
                    };
                    int result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                Log4netService.RecordLog.RecordException("王东坤", string.Format("更新广告{0}", model.Id), ex);
                return 0;
            }
        }

        #endregion

    }
}
