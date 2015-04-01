using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXOrm;
using System.Data.SqlClient;

namespace TXDal.HouseData
{
    /// <summary>
    /// 预售许可证 DAL
    /// </summary>
    public partial class PermitPresaleDal
    {
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/27 17:22:36
        /// 功能描述：添加预售许可证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(PermitPresale model)
        {
            int result = 0;
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into PermitPresale(");
                    strSql.Append("PremisesId,Name,CreateTime,UpdateTime,IsDel");
                    strSql.Append(") values (");
                    strSql.Append("@PremisesId,@Name,@CreateTime,@UpdateTime,@IsDel");
                    strSql.Append(") ");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@PremisesId", model.PremisesId),
                        new SqlParameter("@Name", model.Name),
                        new SqlParameter("@CreateTime", model.CreateTime),
                        new SqlParameter("@UpdateTime", model.UpdateTime),
                        new SqlParameter("@IsDel", model.IsDel)
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


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/29 14:50:42
        /// 功能描述：根据PremisesId 获取列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public List<PermitPresale> GetList(int PremisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.PermitPresales.Where(it => it.PremisesId == PremisesId && it.IsDel == false).ToList();

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("PremisesId:{0}", new object[] {PremisesId}), ex);
                throw;
            }
        }

        /// <summary>
        /// 根据编号获取房源实体
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">预售许可证编号</param>
        /// <returns>返回：预售许可证实体</returns>
        public PermitPresale GetEntity_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.PermitPresales.FirstOrDefault(it => it.Id == id && it.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("Id:{0}", new object[] {id}), ex);
                return null;
            }
        }
    }
}