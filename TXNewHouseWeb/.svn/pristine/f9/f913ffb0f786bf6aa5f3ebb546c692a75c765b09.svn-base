using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXModel.Ajax;
using System.Data.SqlClient;
using TXOrm;

namespace TXBll.Ajax
{
    public class StaticDataBll
    {
        #region 取楼栋静态数据

        /// <summary>
        /// 根据楼盘Id取楼栋信息
        /// author：sunlin
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<StaticData> GetBuildingByPremisesId(int Id) {
            
            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    List<StaticData> list = new List<StaticData>();
                    strSql.Append(@"SELECT [Id],[Name],[NameType],[PremisesId] FROM Building WHERE PremisesId=@Id");
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Id",Id),
                    };
                    var result = dbEnt.ExecuteStoreQuery<StaticData>(strSql.ToString(), parameters);
                    return result.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        #endregion

        #region 取厅室静态数据

        /// <summary>
        /// 根据楼盘Id取厅室信息
        /// author：sunlin
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<StaticData> GetUnitByPremisesId(int Id)
        {

            try
            {
                using (var dbEnt = new kyj_NewHouseDBEntities())
                {
                    var strSql = new StringBuilder();
                    List<StaticData> list = new List<StaticData>();
                    strSql.Append(@"SELECT Room FROM dbo.House WHERE PremisesId=@Id GROUP BY Room");
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Id",Id),
                    };
                    var result = dbEnt.ExecuteStoreQuery<StaticData>(strSql.ToString(), parameters);
                    return result.ToList();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
