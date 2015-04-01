using System.Collections.Generic;
using TXModel.AdminPVM;
using TXModel.Geography;

namespace TXBll.HouseData
{
    /// <summary>
    /// 预售许可证 (网站管理平台)
    /// </summary>
    public partial class PermitPresaleBll
    {
        /// <summary>
        /// 根据楼盘编号获取预售许可证列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPermitPresaleByPremisesId(int premisesId)
        {
            return _permitPresaleDal.GetPermitPresaleByPremisesId(premisesId);
        }

        /// <summary>
        /// 根据楼盘编号获取预售许可证列表(发布楼盘、编辑楼盘)
        /// author: liyuzhao
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<PVS_NH_Premises_SalePermit> GetPermitPresalesByPremisesId(int premisesId)
        {
            return _permitPresaleDal.GetPermitPresalesByPremisesId(premisesId);
        }

        /// <summary>
        /// 添加新预售许可证
        /// </summary>
        /// <param name="permitPresale"></param>
        /// <returns></returns>
        public bool AddNewPermitPresale(PVE_PermitPresale permitPresale)
        {
            return _permitPresaleDal.AddNewPermitPresale(permitPresale) > 0;
        }
    }
}