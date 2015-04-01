using System.Data.SqlClient;
using System.Linq;
using TXOrm;

namespace TXDal.HouseData
{
    public partial class PremisesDal
    {
        /// <summary>
        /// 同区热门楼盘列表中的楼盘信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TXOrm.Premis Get_Premises_HotPremisesList(int id)
        {
            using (var db = new kyj_NewHouseDBEntities())
            {
                string sql = @"
SELECT  Id, ProvinceId, CityId, DId
FROM    Premises (NOLOCK)
WHERE   IsDel = 0
        AND Id = @Id";
                var parms = new object[]
                {
                    new SqlParameter("@Id", id)
                };

                var entity = db.ExecuteStoreQuery<TXOrm.Premis>(sql, parms).FirstOrDefault();

                return entity;
            }
        }
    }
}