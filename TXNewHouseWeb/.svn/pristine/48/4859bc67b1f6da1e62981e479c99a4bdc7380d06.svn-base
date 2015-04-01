using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TXOrm;

namespace TXDal.HouseData
{
    /// <summary>
    ///  楼盘特色
    /// </summary>
    public partial class PremisesFeatureDal
    {
        /// <summary>
        /// 添加
        /// Author:tyh
        /// Time:2014-4-15
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int Add(string text)
        {
            int result = 0;
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    result = db.ExecuteStoreCommand("INSERT INTO dbo.PremisesFeature(Name, CreateTime, IsDel) VALUES (@Text, GETDATE(), 0)", new SqlParameter("@Text", text));
                }
            }
            catch (Exception e)
            {
                //Log4netService.RecordLog.RecordException("台永海", String.Concat("text:", text), e);
                throw;
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// Author:tyh
        /// Time:2014-4-15
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public int Update(int id, string text)
        {
            int result = 0;
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    result = db.ExecuteStoreCommand("UPDATE dbo.PremisesFeature SET Name = @Text WHERE Id = @Id", new SqlParameter[] { new SqlParameter("@Id", id), new SqlParameter("@Text", text) });
                }
            }
            catch (Exception e)
            {
                //Log4netService.RecordLog.RecordException("台永海", String.Format("id:{0},text:{1}", id, text), e);
                throw;
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// Author:tyh
        /// Time:2014-4-15
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            int result = 0;
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    result = db.ExecuteStoreCommand("UPDATE dbo.PremisesFeature SET IsDel = 1 WHERE Id = @Id", new SqlParameter("@Id", id));
                }
            }
            catch (Exception e)
            {
                //Log4netService.RecordLog.RecordException("台永海", String.Concat("id:", id), e);
                throw;
            }
            return result;
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/3 14:44:46
        /// 功能描述：获取楼盘特色列表
        /// </summary>
        /// <returns></returns>
        public List<PremisesFeature> GetList()
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    return dbEnt.PremisesFeatures.Where(t => t.IsDel == false).ToList();
                }
            }
            catch (Exception ex)
            {
                //Log4netService.RecordLog.RecordException("谢江", ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// 根据编号集获取信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<PremisesFeature> GetListByIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return null;
            }

            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    PremisesFeature (NOLOCK) AS a
WHERE   Id IN ( {0} )
        AND IsDel = 0", ids);

                    var list = db.ExecuteStoreQuery<PremisesFeature>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("ids:{0}", ids), ex);
                return null;
            }
        }
    }
}