using System.Collections.Generic;
using TXDal.HouseData;
using TXModel.Commons;
using TXOrm;

namespace TXBll.HouseData
{
    /// <summary>
    /// 房源
    /// </summary>
    public partial class HouseBll
    {
        private readonly HouseDal _houseDal = new HouseDal();

        #region 通用方法

        /// <summary>
        /// 添加房源
        /// </summary>
        /// <param name="entity">房源实体</param>
        /// <returns>返回：新添加房源编号</returns>
        public int Add(House entity, HouseTemplate ht)
        {
            return _houseDal.Add(entity, ht);
        }

        /// <summary>
        /// 更新房源编号更新房源信息
        /// </summary>
        /// <param name="entity">房源实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return _houseDal.Update_ById(entity);
        }

        /// <summary>
        /// 逻辑删除房源信息
        /// </summary>
        /// <param name="id">房源编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            return _houseDal.Del_ById(id);
        }

        /// <summary>
        /// 根据编号获取房源实体
        /// </summary>
        /// <param name="id">房源编号</param>
        /// <returns>返回：房源实体</returns>
        public House GetEntity_ById(int id)
        {
            return _houseDal.GetEntity_ById(id);
        }


        /// <summary>
        /// 根据编号获取房源实体
        /// </summary>
        /// <param name="id">房源编号</param>
        /// <returns>返回：房源实体</returns>
        public House GetHouseModel(int id)
        {
            return _houseDal.GetHouseModel(id);
        }

        /// <summary>
        /// 根据编号集合获取实体列表
        /// </summary>
        /// <param name="ids">编号集合字符串(逗号分隔)</param>
        /// <returns>返回：列表</returns>
        public List<object> GetList_ByIds(string ids)
        {
            return _houseDal.GetList_ByIds(ids);
        }

        /// <summary>
        /// 根据搜素条件获取分页列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public Paging<object> GetPageList_BySearch(object search, int pageIndex, int pageSize)
        {
            var paging = _houseDal.GetPageList_BySearch(search, pageIndex, pageSize);
            return paging;
        }

        #endregion
    }
}