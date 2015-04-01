using System;
using System.Collections.Generic;
using TXOrm;

namespace TXBll.HouseData
{
    public partial class UnitBll
    {
        /// <summary>
        /// 根据楼栋编号获取单元列表(发布楼栋、编辑楼栋)
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetUnitList(int buildingId)
        {
            var unitList = _unitDal.GetUnitList(buildingId);
            var list = new List<KeyValuePair<string, string>>();
            foreach (var unit in unitList)
            {
                list.Add(new KeyValuePair<string, string>(Convert.ToString(unit.Id), unit.Name));
            }
            return list;
        }

        /// <summary>
        /// 根据楼栋编号获取单元列表
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<Unit> GetUnitListByBuildingId(int buildingId)
        {
            return _unitDal.GetUnitListByBuildingId(buildingId);
        }
    }
}