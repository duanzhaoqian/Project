using System.Collections.Generic;
using TXModel.AdminPVM;
using TXOrm;
using TXDal.HouseData;

namespace TXBll.HouseData
{
    public partial class SandTableLabelBll
    {
        private readonly SandTableLabelDal _sandTableLabelDal = new SandTableLabelDal();

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
            return _sandTableLabelDal.GetSandList(PremisesId, DeveloperId);
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
            return _sandTableLabelDal.DelSandTableLabelByDeveloperIdAndPremisesId(PremisesId, DeveloperId);
        }
        #endregion

        #region 获取沙盘图列表

        /// <summary>
        /// 获取沙盘图列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="pnum"></param>
        /// <returns></returns>
        public List<PVM_NH_SandBox_Selection> GetSandBoxSelectionsByPremisesId(int premisesId, int pnum)
        {
            var listSandBox = _sandTableLabelDal.GetSandBoxListByPremisesId(premisesId);

            var listBuilding = new BuildingDal().GetPNumsByPremisesId(premisesId);

            var list = new List<PVM_NH_SandBox_Selection>();

            for (int i = 0; i < listSandBox.Count; i++)
            {
                var sandbox = new PVM_NH_SandBox_Selection
                {
                    Id = listSandBox[i].Id,
                    Number = listSandBox[i].Number,
                    IsUsed = false
                };
                for (int j = 0; j < listBuilding.Count; j++)
                {
                    if (listSandBox[i].Id == listBuilding[j]
                        && pnum != listBuilding[j])
                    {
                        sandbox.IsUsed = true;
                    }
                }

                list.Add(sandbox);
            }

            return list;
        }

        #endregion
    }
}
