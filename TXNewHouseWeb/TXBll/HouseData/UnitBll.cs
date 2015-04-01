using System.Collections.Generic;
using TXDal.HouseData;
using TXOrm;

namespace TXBll.HouseData
{
    /// <summary>
    /// 单元Bll 类
    /// </summary>
    public partial class UnitBll
    {
        private readonly UnitDal _unitDal = new UnitDal();

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
            return _unitDal.GetUnitList(DeveloperId, PremisesId, BuildingId);
        }


        /// <summary>
        /// 根据楼栋id获取单元
        /// author:汪敏
        /// </summary>
        /// <param name="id">楼栋Id</param>
        /// <returns></returns>
        public List<TXOrm.Unit> GetUnitByBuildingId(int id)
        {
            return _unitDal.GetUnitByBuildingId(id);
        }

        /// <summary>
        /// 根据楼栋和单元编号获取单元信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public Unit GetUnit_ByBuildingIdAndNum(int buildingId, int num)
        {
            return _unitDal.GetUnit_ByBuildingIdAndNum(buildingId, num);
        }
    }
}