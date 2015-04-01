using TXDal.NHouseActivies.SecKill;
using TXOrm;

namespace TXBll.NHouseActivies.SecKill
{
    /// <summary>
    /// 秒杀
    /// </summary>
    public partial class SecKillBll
    {
        private readonly SecKillDal _secKillDal = new SecKillDal();

        #region 通用方法

        /// <summary>
        /// 根据编号获取秒杀实体
        /// </summary>
        /// <param name="id">秒杀编号</param>
        /// <returns>返回：秒杀实体</returns>
        public Activity GetEntity_ById(int id)
        {
            return (_secKillDal.GetEntity_ById(id) as Activity);
        }

        #endregion
    }
}