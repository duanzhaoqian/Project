using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;

namespace TXDal.HouseData
{
    public partial class UnitDal
    {
        /// <summary>
        /// 根据楼栋编号获取单元列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<Unit> GetUnitList(int buildingId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var list = db.Units.Where(it => it.BuildingId == buildingId).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("buildingId:{0}", buildingId), ex);
                return null;
            }
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/2 10:21:34
        /// 功能描述：根据楼盘楼栋开发商ID 查询单元列表
        /// </summary>
        /// <param name="DeveloperId">开发商id</param>
        /// <param name="PremisesId">楼盘id</param>
        /// <param name="BuildingId">楼栋id</param>
        /// <returns></returns>
        public List<Unit> GetUnitList(int DeveloperId, int PremisesId, int BuildingId)
        {
            List<Unit> list = null;
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    list = db.Units.Where(t => t.DeveloperId == DeveloperId && t.PremisesId == PremisesId && t.BuildingId == BuildingId).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("谢江", string.Format("{0},{1},{2}", DeveloperId, PremisesId, BuildingId), ex);
                throw;
            }
            return list;
        }


        # region 根据楼栋id获取单元
        /// <summary>
        /// 根据楼栋id获取单元
        /// author:汪敏
        /// </summary>
        /// <param name="id">楼栋Id</param>
        /// <returns></returns>
        public List<TXOrm.Unit> GetUnitByBuildingId(int id)
        {
            List<TXOrm.Unit> list = new List<Unit>();
            try
            {
                using (var entity = new TXOrm.kyj_NewHouseDBEntities())
                {
                    var query = (from unit in entity.Units
                                 where unit.BuildingId == id
                                 select unit);
                    if (query.Count() > 0)
                    {
                        list = query.ToList();
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("id:{0}", id), e);
                //throw;
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 根据楼栋和单元编号获取单元信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public Unit GetUnit_ByBuildingIdAndNum(int buildingId, int num)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Units.FirstOrDefault(t => t.BuildingId == buildingId && t.Num == num && t.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("buildingId={0}, num={1}", buildingId, num), ex);
                throw;
            }
        }
    }
}