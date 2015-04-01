using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TXOrm;
using TXModel.Commons;

namespace TXDal.HouseData
{
    /// <summary>
    /// 房源模版
    /// </summary>
    public partial class HouseTemplateDal
    {
        #region  添加模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/16 16:32:45
        /// 功能描述：添加模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(HouseTemplate model)
        {
            int result = 0;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into HouseTemplate(");
                    strSql.Append("DeveloperId,Title,Content,CreateTime,UpdateTime,IsDel");
                    strSql.Append(") values (");
                    strSql.Append("@DeveloperId,@Title,@Content,@CreateTime,@UpdateTime,@IsDel");
                    strSql.Append(") ");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
			            new SqlParameter("@DeveloperId", model.DeveloperId) ,            
                        new SqlParameter("@Title", model.Title) ,            
                        new SqlParameter("@Content", model.Content) ,            
                        new SqlParameter("@CreateTime", model.CreateTime) ,            
                        new SqlParameter("@UpdateTime", model.UpdateTime) ,            
                        new SqlParameter("@IsDel", model.IsDel)             
              
                        };
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);


                }
            }
            catch (Exception ex)
            {
                //Log4netService.RecordLog.RecordException("谢江", ex.ToString());
                throw ex;
            }
            return result;
        }
        #endregion

        #region 修改模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/27 15:02:53
        /// 功能描述：修改模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int update(HouseTemplate model)
        {
            int result = 0;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update HouseTemplate set ");
                    strSql.Append(" Title=@Title , ");
                    strSql.Append(" Content = @Content , ");
                    strSql.Append(" UpdateTime = @UpdateTime ");
                    strSql.Append(" where Id=@Id and DeveloperId = @DeveloperId   ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@Id", model.Id) , 
                        new SqlParameter("@Title", model.Title) , 
			            new SqlParameter("@DeveloperId", model.DeveloperId) ,            
                        new SqlParameter("@Content", model.Content) ,            
                        new SqlParameter("@UpdateTime", model.UpdateTime)         
                    };
                    result = dbEnt.ExecuteStoreCommand(strSql.ToString(), parameters);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        #region 房源模版列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/16 16:33:27
        /// 功能描述：房源模版列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public Paging<HouseTemplate> GetHouseTemplateList(Paging<HouseTemplate> paging, int DeveloperId)
        {
            string sql = String.Empty;
            string sqlCount = String.Empty;

            #region 拼接分页SQL
            sql = @"SELECT * FROM (
            SELECT ROW_NUMBER() OVER(ORDER BY Id DESC) AS Row, * FROM HouseTemplate WHERE 1 = 1 and IsDel=0 
            And DeveloperId={0} 
            ) AS tempTable
            WHERE Row BETWEEN {1} AND {2}";
            sql = String.Format(sql, DeveloperId, ((paging.PageIndex - 1) * paging.PageSize) + 1, paging.PageIndex * paging.PageSize);
            #endregion

            #region 拼接总记录SQL
            sqlCount = @"SELECT COUNT(1) FROM HouseTemplate WHERE 1 = 1 and IsDel=0  and DeveloperId={0}";
            sqlCount = String.Format(sqlCount, DeveloperId);
            #endregion

            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    var query = houseDB.ExecuteStoreQuery<HouseTemplate>(sql);
                    List<HouseTemplate> p = query.ToList();
                    paging.ResultData = p;
                    #endregion

                    #region 查询总记录数据
                    var queryCount = houseDB.ExecuteStoreQuery<int>(sqlCount);
                    paging.TotalCount = queryCount.FirstOrDefault<int>();
                    #endregion
                }


            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0},{1},{2}", DeveloperId), ex);
                throw;
            }
            return paging;

        }
        #endregion

        #region 根据开发商Id 查询模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/18 13:54:35
        /// 功能描述：根据开发商Id 查询模版
        /// </summary>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public List<HouseTemplate> GetHouseTemplateByDeveloperId(int DeveloperId)
        {
            List<HouseTemplate> list = null;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"SELECT  *
                                                FROM    dbo.HouseTemplate
                                                WHERE   IsDel = 0
                                                        AND DeveloperId = {0}", DeveloperId);
                    list = dbEnt.ExecuteStoreQuery<HouseTemplate>(sql).ToList();
                    //list = dbEnt.HouseTemplates.Where(t => t.IsDel == false && t.DeveloperId == DeveloperId).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        #endregion

        #region 根据Id 查询模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/18 16:46:02
        /// 功能描述：根据Id 查询模版
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public HouseTemplate GetHouseTemplateById(int Id)
        {
            HouseTemplate model = null;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"SELECT  *
                                                FROM    dbo.HouseTemplate
                                                WHERE   IsDel = 0
                                                        AND ID = {0}", Id);
                    model = dbEnt.ExecuteStoreQuery<HouseTemplate>(sql).FirstOrDefault();
                    //model = dbEnt.HouseTemplates.Where(t => t.Id == Id && t.IsDel == false).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }
        #endregion

        #region 删除单个房源模版
        ///作者：谢江
        ///时间：2014/1/20 17:14:06
        ///功能描述：删除单个房源模版
        public int DelHouseTemplateByID(int Id)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"UPDATE  dbo.HouseTemplate
                                    SET     IsDel = 1 ,
                                            UpdateTime = GETDATE()
                                    WHERE   Id = {0}", Id);
                    kyj.ExecuteStoreCommand(sql);
                    //var query = kyj.HouseTemplates.FirstOrDefault(it => it.Id == Id);
                    //if (query != null)
                    //{
                    //    query.IsDel = true;
                    //    query.UpdateTime = DateTime.Now;
                    //    return kyj.SaveChanges();
                    //}
                    return 0;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("马欢", Id, e);
                return 0;
            }
        }
        #endregion
    }
}
