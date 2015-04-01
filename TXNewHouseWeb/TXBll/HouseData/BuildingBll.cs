using System.Collections.Generic;
using TXDal.HouseData;
using TXModel.Commons;
using TXOrm;

namespace TXBll.HouseData
{
    /// <summary>
    /// 楼栋
    /// </summary>
    public partial class BuildingBll
    {
        private readonly BuildingDal _buildingDal = new BuildingDal();

        #region 通用方法

        /// <summary>
        /// 添加楼栋
        /// </summary>
        /// <param name="entity">楼栋实体</param>
        /// <returns>返回：新添加楼栋编号</returns>
        public int Add(Building entity)
        {
            return _buildingDal.Add(entity);
        }

        /// <summary>
        /// 更新楼栋编号更新楼栋信息
        /// </summary>
        /// <param name="entity">楼栋实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return _buildingDal.Update_ById(entity);
        }

        /// <summary>
        /// 逻辑删除楼栋信息
        /// </summary>
        /// <param name="id">楼栋编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            return _buildingDal.Del_ById(id);
        }

        /// <summary>
        /// 根据编号获取楼栋实体
        /// </summary>
        /// <param name="id">楼栋编号</param>
        /// <returns>返回：楼栋实体</returns>
        public Building GetEntity_ById(int id)
        {
            return _buildingDal.GetEntity_ById(id);
        }

        /// <summary>
        /// 根据编号集合获取实体列表
        /// </summary>
        /// <param name="ids">编号集合字符串(逗号分隔)</param>
        /// <returns>返回：列表</returns>
        public List<object> GetList_ByIds(string ids)
        {
            return _buildingDal.GetList_ByIds(ids);
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
            var paging = _buildingDal.GetPageList_BySearch(search, pageIndex, pageSize);
            return paging;
        }

        #endregion

        /// <summary>
        /// 根据楼盘Id获取楼栋
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<Building> GetList_ByPremisId(int premisesId)
        {
            return _buildingDal.GetList_ByPremisId(premisesId);
        }

        /// <summary>
        /// 根据楼盘Id获取楼栋
        /// author: 汪敏
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <returns></returns>
        public List<Building> GetBuildingByPremisesId(int premisesId, int DeveloperId)
        {
            return _buildingDal.GetBuildingByPremisesId(premisesId, DeveloperId);
        }

        #region 发布楼栋

        /// <summary>
        /// 添加楼栋
        /// </summary>
        /// <param name="model">楼栋实体</param>
        /// <param name="unit"></param>
        /// <returns>返回：新添加楼栋编号</returns>
        public int Add(Building model, string[] unit)
        {
            return _buildingDal.Add(model, unit);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 13:35:39
        /// 功能描述：添加楼栋(手填预售许可证)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="unit"></param>
        /// <param name="PermitName">预售许可证名称</param>
        /// <returns></returns>
        public int Add(Building model, string[] unit, string PermitName)
        {
            return _buildingDal.Add(model, unit, PermitName);
        }

        #endregion

        #region 编辑楼栋

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Building model, string[] unit)
        {
            return dal.Update(model, unit);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/13 14:53:25
        /// 功能描述：更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool Update(Building model, string[] unit, string PermitName)
        {
            return dal.Update(model, unit, PermitName);
        }

        #endregion

        #region 根据楼栋编号 获取楼栋状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/7 16:06:27
        /// 功能描述：根据楼栋编号 获取楼栋状态
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public int GetBuildingState(int BuildingId)
        {
            return dal.GetBuildingState(BuildingId);
        }
        #endregion


        #region 获取楼层信息

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/3 10:00:05
        /// 功能描述：获取地上楼层信息
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public int GetTheFloor(int BuildingId)
        {
            return _buildingDal.GetTheFloor(BuildingId);
        }


        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/3 10:00:05
        /// 功能描述：获取地下楼层信息
        /// </summary>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public int GetUnderloor(int BuildingId)
        {
            return _buildingDal.GetUnderloor(BuildingId);
        }

        #endregion

    }
}