using TXDal.HouseData;
using TXOrm;
using System.Collections.Generic;

namespace TXBll.HouseData
{
    /// <summary>
    /// 预售许可证
    /// </summary>
    public partial class PermitPresaleBll
    {
        private readonly PermitPresaleDal _permitPresaleDal = new PermitPresaleDal();

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/27 17:22:36
        /// 功能描述：添加预售许可证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(PermitPresale model)
        {
            return _permitPresaleDal.Add(model);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/29 14:50:42
        /// 功能描述：根据PremisesId 获取列表
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public List<PermitPresale> GetList(int PremisesId)
        {
            return _permitPresaleDal.GetList(PremisesId);
        }

        /// <summary>
        /// 根据编号获取房源实体
        /// author: liyuzhao
        /// </summary>
        /// <param name="id">预售许可证编号</param>
        /// <returns>返回：预售许可证实体</returns>
        public PermitPresale GetEntity_ById(int id)
        {
            return _permitPresaleDal.GetEntity_ById(id);
        }
    }
}