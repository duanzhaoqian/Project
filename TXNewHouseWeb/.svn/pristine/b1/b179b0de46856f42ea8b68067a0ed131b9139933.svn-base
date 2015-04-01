using System.Collections.Generic;
using TXModel.Commons;
using TXModel.Web;
namespace TXBll.HouseData
{
    /// <summary>
    /// 楼盘 开发商管理平台
    /// </summary>
    public partial class BuildingBll
    {
        private readonly TXDal.HouseData.BuildingDal dal = new TXDal.HouseData.BuildingDal();
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/12 17:08:17
        /// 功能描述：楼栋分页列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="DeveloperId"></param>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public Paging<PremisesBuilding> GetBuildingList(Paging<PremisesBuilding> paging, int DeveloperId, int PremisesId)
        {
            return dal.GetBuildingList(paging, DeveloperId, PremisesId);
        }

        #region 楼栋名称在同楼盘名称下不能相重
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/27 15:32:16
        /// 功能描述：楼栋名称在同楼盘名称下不能相重，房源名称（房号）在同楼栋下不能相重，如相重则提示：“楼栋名称已存在”、
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="NameType"></param>
        /// <returns></returns>
        public int CheckBuilding(string Name, string NameType, int PremisesId)
        {
            return dal.CheckBuilding(Name, NameType, PremisesId);
        }
        #endregion

        #region 获取沙盘标号列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/21 14:18:14
        /// 功能描述：获取沙盘标号列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public List<int> GetPNumList(int PremisesId)
        {
            return dal.GetPNumList(PremisesId);
        }
        #endregion
    }
}