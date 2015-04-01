using System;
using System.Linq;
using TXModel.Commons;
using TXOrm;

namespace TXDal.NHouseActivies.Biding
{
    /// <summary>
    /// 竞价活动管理
    /// </summary>
    public partial class BidingDal
    {
        #region 通用方法

        /// <summary>
        /// 根据编号获取竞价实体
        /// </summary>
        /// <param name="id">竞价编号</param>
        /// <returns>返回：竞价实体</returns>
        public Activity GetEntity_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Activities.FirstOrDefault(it => it.Id == id && it.Type == 5 && it.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("Id:{0}", new object[] { id }), ex);
                return null;
            }
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
            return null;
        }

        #endregion
    }
}