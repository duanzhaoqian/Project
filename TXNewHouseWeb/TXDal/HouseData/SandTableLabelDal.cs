using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;
using System.Data.SqlClient;

namespace TXDal.HouseData
{
    public partial class SandTableLabelDal
    {
        #region 根据楼盘Id 和开发商ID 获取 楼盘标注列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/11 10:24:10
        /// 功能描述：根据楼盘Id 和开发商ID 获取 楼盘标注列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public List<SandTableLabel> GetSandList(int PremisesId, int DeveloperId)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    return dbEnt.SandTableLabels.Where(t => t.PremisesId == PremisesId && t.DeveloperId == DeveloperId).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("PremisesId={0},DeveloperId={1}", PremisesId, DeveloperId), ex);
                throw;
            }

        }
        #endregion

        #region 根据楼盘Id 和开发商ID 删除楼盘沙盘图标记
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/10 15:28:09
        /// 功能描述：根据楼盘Id 和开发商ID 删除楼盘沙盘图标记
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public int DelSandTableLabelByDeveloperIdAndPremisesId(int PremisesId, int DeveloperId)
        {
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    string sql = "delete from SandTableLabel where PremisesId=@PremisesId and DeveloperId=@DeveloperId";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@PremisesId",PremisesId),
                        new SqlParameter("@DeveloperId",DeveloperId)
                    };
                    return dbEnt.ExecuteStoreCommand(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("PremisesId={0},DeveloperId={1}", PremisesId, DeveloperId), ex);
                throw;
            }

        }
        #endregion

        #region 根据楼盘ID获取沙盘标记列表
        /// <summary>
        /// 根据楼盘ID获取沙盘标记列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<SandTableLabel> GetSandBoxListByPremisesId(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
SELECT  *
FROM    SandTableLabel (NOLOCK)
WHERE   PremisesId = {0}", premisesId);

                    var list = db.ExecuteStoreQuery<SandTableLabel>(sql).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("PremisesId={0}", premisesId), ex);
                throw;
            }
        }

        #endregion
    }
}
