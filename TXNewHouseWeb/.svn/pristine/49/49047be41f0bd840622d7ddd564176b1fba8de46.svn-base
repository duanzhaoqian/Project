using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;

namespace TXDal.HouseData
{
    /// <summary>
    /// 单元(网站管理平台)
    /// </summary>
    public partial class UnitDal
    {
        /// <summary>
        /// 根据楼栋编号获取单元列表
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<Unit> GetUnitListByBuildingId(int buildingId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var query = db.Units.Where(it => it.IsDel == false && it.BuildingId == buildingId);

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("buildingId:{0}", buildingId), ex);
                return null;
            }
        }
    }
}