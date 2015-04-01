using TXDal.NHouseActivies.Biding;
using TXOrm;
using TXModel.Commons;

namespace TXBll.NHouseActivies.Biding
{
    /// <summary>
    /// 竞价活动管理
    /// </summary>
    public partial class BidingBll
    {
        private readonly BidingDal _bidingDal = new BidingDal();

        #region 通用方法

        /// <summary>
        /// 根据编号获取竞价实体
        /// </summary>
        /// <param name="id">竞价编号</param>
        /// <returns>返回：竞价实体</returns>
        public Activity GetEntity_ById(int id)
        {
            return _bidingDal.GetEntity_ById(id);
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
            return _bidingDal.GetPageList_BySearch(search, pageIndex, pageSize);
        }

        #endregion
    }
}