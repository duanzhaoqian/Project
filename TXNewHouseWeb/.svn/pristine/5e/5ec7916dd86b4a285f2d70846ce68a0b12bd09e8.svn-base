using TXDal.NHouseActivies.Discount;
using TXOrm;

namespace TXBll.NHouseActivies.Discount
{
    /// <summary>
    /// 折扣
    /// </summary>
    public partial class DiscountBll
    {
        private readonly DiscountDal _discountDal = new DiscountDal();

        /// <summary>
        /// 根据活动编号获取楼盘实体
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <returns>返回楼栋实体，否则返回null</returns>
        public Premis GetPremisesEntity_ByActivitiesId(int id)
        {
            return _discountDal.GetPremisesEntity_ByActivitiesId(id);
        }

        #region 通用方法

        /// <summary>
        /// 根据编号获取折扣实体
        /// </summary>
        /// <param name="id">折扣编号</param>
        /// <returns>返回：折扣实体</returns>
        public Activity GetEntity_ById(int id)
        {
            return _discountDal.GetEntity_ById(id);
        }

        #endregion
    }
}