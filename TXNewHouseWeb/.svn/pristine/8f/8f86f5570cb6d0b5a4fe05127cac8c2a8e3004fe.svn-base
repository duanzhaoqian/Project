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
    /// 广告管理(网站管理平台)
    /// </summary>
    public class Advertise_AdminDal : BaseDal_Admin
    {
        #region 查询广告列表

        /// <summary>
        /// 查询广告列表
        /// author:wangdk
        /// </summary>
        /// <param name="cityID">城市</param>
        /// <param name="position">位置1热点新盘推荐;2最新楼盘推荐;3.精品楼盘;4.即将开盘;</param>
        /// <returns>广告集合or null</returns>
        public List<TXOrm.Advertise> GetAdvertiseList_Admin(int cityID, int position)
        {
            List<TXOrm.Advertise> advertiseList = null;
            try
            {
                using (var newHouseDB = new kyj_NewHouseDBEntities())
                {

                    StringBuilder sql = new StringBuilder(@"SELECT Id,CityId,CityName,Position,PremisesId,PremisesName,SequenceNum,BeginTime,EndTime ,CreateTime,IsDel,AdminID,AdminName
                                                            FROM Advertise (NOLOCK) where IsDel=0");
                    List<SqlParameter> paras = new List<SqlParameter>();
                    if (cityID > 0)
                    {
                        sql.Append(" and CityId=@CityId");
                        paras.Add(new SqlParameter("@CityId", cityID));
                    }

                    if (position > 0)
                    {
                        sql.Append(" and Position=@Position");
                        paras.Add(new SqlParameter("@Position", position));
                    }
                    sql.Append(" order by SequenceNum");

                    var query = newHouseDB.ExecuteStoreQuery<TXOrm.Advertise>(sql.ToString(), paras.ToArray());
                    advertiseList = query.ToList();
                    return advertiseList;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("王东坤", string.Format("查询广告列表"), e);
                return null;
            }
        }

        #endregion

        #region 批量添加广告

        /// <summary>
        /// 批量添加广告
        /// author:wangdk
        /// </summary>
        /// <param name="adList">广告集合</param>
        /// <returns>success:1,error:0</returns>
        public int AddAdvertiseList(List<TXOrm.Advertise> adList)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    #region 批量sql

                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"BEGIN TRY; BEGIN TRAN;");

                    string tempSql = @"INSERT INTO Advertise
                                                     (CityId ,CityName ,Position,PremisesId ,PremisesName,SequenceNum,BeginTime,EndTime ,CreateTime ,IsDel,AdminID,AdminName)
                                                     VALUES
                                                    ({0},'{1}',{2},{3},'{4}',{5},'{6}','{7}','{8}',{9},{10},'{11}');
                                                    ";
                    foreach (var ad in adList)
                    {
                        sql.Append(string.Format(tempSql, ad.CityId, ad.CityName, ad.Position, ad.PremisesId, ad.PremisesName, ad.SequenceNum, ad.BeginTime, ad.EndTime, ad.CreateTime, ad.IsDel == false ? 0 : 1, ad.AdminID, ad.AdminName));
                    }

                    sql.Append(@"COMMIT TRAN
                                 SELECT  '1' AS Code, '操作成功' AS Msg
                                 END TRY
                                BEGIN CATCH
                                    ROLLBACK TRAN
                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                END CATCH");

                    #endregion

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql.ToString()).ToList();

                    if (list.Count > 0)
                        return Convert.ToInt32(list[0].Code);
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王东坤", string.Format("批量添加广告"), ex);
                return 0;
            }
        }

        #endregion

        #region 删除并重新排序号

        /// <summary>
        /// 根据ID删除并重新排序号
        /// author:wangdk
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns>success:1 error:0</returns>
        public int DeleteAdvertise_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"BEGIN TRY;BEGIN TRAN;");
                    sql.Append(@"declare @position int;                                       
                                 declare @currentSeq int;  
                                 declare @totalSeq int;
                                 select @currentSeq=SequenceNum,@position=Position from Advertise (NOLOCK) where id=@ID and IsDel=0;
                                 select @totalSeq=count(*) from Advertise (NOLOCK) where Position=@position and IsDel=0;                                   
                                 if(@currentSeq<@totalSeq+1)
                                 begin
                                   UPDATE Advertise SET IsDel =1 WHERE ID=@ID and IsDel=0;
                                   update Advertise set SequenceNum=SequenceNum-1 where Position=@position and SequenceNum>@currentSeq and IsDel=0
                                 end;");
                    sql.Append(@" COMMIT TRAN
                                    SELECT  '1' AS Code, '操作成功' AS Msg
                                END TRY
                                BEGIN CATCH
                                    ROLLBACK TRAN
                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                END CATCH");
                    SqlParameter[] parameters = { new SqlParameter("@ID", id) };
                    var ret = db.ExecuteStoreQuery<ESqlResult>(sql.ToString(), parameters).ToList();

                    if (ret.Count > 0)
                        return Convert.ToInt32(ret[0].Code);
                    else
                        return 0;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("王东坤", string.Format("删除并重新排序号,广告id({0})", id), e);
                return 0;
            }
        }

        #endregion

        #region 批量更新广告

        /// <summary>
        ///批量更新广告
        /// author:wangdk
        /// </summary>
        /// <param name="">广告集合</param>
        /// <returns>success:1 error:0</returns>
        public int UpdateAdvertiseList(List<TXOrm.Advertise> adList)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    #region 批量sql

                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"BEGIN TRY; BEGIN TRAN;");

                    string tempSql = @"UPDATE Advertise  SET CityId={0}
                                        ,CityName ='{1}'
                                        ,Position = {2}
                                        ,PremisesId = {3}
                                        ,PremisesName ='{4}'
                                        ,SequenceNum = {5}
                                        ,BeginTime = '{6}'
                                        ,EndTime = '{7}'                                       
                                        ,IsDel = {8}
                                        ,AdminID = {9}
                                        ,AdminName = '{10}'
                           WHERE Id={11};";
                    foreach (var ad in adList)
                    {
                        sql.Append(string.Format(tempSql, ad.CityId, ad.CityName, ad.Position, ad.PremisesId, ad.PremisesName, ad.SequenceNum, ad.BeginTime, ad.EndTime, ad.IsDel == false ? 0 : 1, ad.AdminID, ad.AdminName, ad.Id));
                    }

                    sql.Append(@"COMMIT TRAN
                                 SELECT  '1' AS Code, '操作成功' AS Msg
                                 END TRY
                                BEGIN CATCH
                                    ROLLBACK TRAN
                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                END CATCH");

                    #endregion

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql.ToString()).ToList();

                    if (list.Count > 0)
                        return Convert.ToInt32(list[0].Code);
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王东坤", string.Format("批量更新广告"), ex);
                return 0;
            }
        }

        #endregion

        #region 根据ID是否存在判断更新与添加操作

        /// <summary>
        /// 根据ID是否存在判断更新与添加操作
        /// </summary>
        /// <param name="adList">广告集合</param>
        /// <returns>success:1 error:0</returns>
        public int AddOrUpdateAdvertise(List<TXOrm.Advertise> adList)
        {
            try
            {
                List<TXOrm.Advertise> addAdList = adList.Where(x => x.Id == 0).ToList();   //添加
                List<TXOrm.Advertise> updateAdList = adList.Where(x => x.Id > 0).ToList(); //更新              

                using (var db = new kyj_NewHouseDBEntities())
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"BEGIN TRY; BEGIN TRAN;");

                    #region 添加
                    if (addAdList != null && addAdList.Count > 0)
                    {

                        string tempAddSql = @"INSERT INTO Advertise
                                                     (CityId ,CityName ,Position,PremisesId ,PremisesName,SequenceNum,BeginTime,EndTime ,CreateTime ,IsDel,AdminID,AdminName)
                                                     VALUES
                                                    ({0},'{1}',{2},{3},'{4}',{5},'{6}','{7}','{8}',{9},{10},'{11}');
                                                    ";
                        foreach (var ad in addAdList)
                        {
                            sql.Append(string.Format(tempAddSql, ad.CityId, ad.CityName, ad.Position, ad.PremisesId, ad.PremisesName, ad.SequenceNum, ad.BeginTime, ad.EndTime, ad.CreateTime, ad.IsDel == false ? 0 : 1, ad.AdminID, ad.AdminName));
                        }
                    }
                    #endregion

                    #region 更新
                    if (updateAdList != null && updateAdList.Count > 0)
                    {
                        string tempUpdateSql = @"UPDATE Advertise  SET CityId={0}
                                        ,CityName ='{1}'
                                        ,Position = {2}
                                        ,PremisesId = {3}
                                        ,PremisesName ='{4}'
                                        ,SequenceNum = {5}
                                        ,BeginTime = '{6}'
                                        ,EndTime = '{7}'                                       
                                        ,IsDel = {8}
                                        ,AdminID = {9}
                                        ,AdminName = '{10}'
                           WHERE Id={11};";
                        foreach (var ad in updateAdList)
                        {
                            sql.Append(string.Format(tempUpdateSql, ad.CityId, ad.CityName, ad.Position, ad.PremisesId, ad.PremisesName, ad.SequenceNum, ad.BeginTime, ad.EndTime, ad.IsDel == false ? 0 : 1, ad.AdminID, ad.AdminName, ad.Id));
                        }
                    }
                    #endregion

                    sql.Append(@"COMMIT TRAN
                                 SELECT  '1' AS Code, '操作成功' AS Msg
                                 END TRY
                                BEGIN CATCH
                                    ROLLBACK TRAN
                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                END CATCH");

                    var list = db.ExecuteStoreQuery<ESqlResult>(sql.ToString()).ToList();

                    if (list.Count > 0)
                        return Convert.ToInt32(list[0].Code);
                    else
                        return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("王东坤", string.Format("根据ID是否存在判断更新与添加操作"), ex);
                return 0;
            }
        }

        #endregion
    }
}
