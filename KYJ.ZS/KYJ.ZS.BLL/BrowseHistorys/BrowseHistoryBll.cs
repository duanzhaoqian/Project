using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.BrowseHistorys;
using KYJ.ZS.Commons.Common;

namespace KYJ.ZS.BLL.BrowseHistorys
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-5-27
    /// 描述：浏览历史操作
    /// </summary>
    public class BrowseHistoryBll
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-28
        /// 描述：对浏览历史的操作
        /// </summary>
        /// <param name="userguid"></param>
        /// <param name="entity"></param>
        public void BrowseHistoryOperate(string userguid, BrowseHistoryEntity entity)
        {
            List<BrowseHistoryEntity> list = GetBrowseHistoryList(userguid);
            if (list == null)//浏览历史为空时
            {
                AddBrowseHistory(userguid, entity);
            }
            else //浏览历史不为空
            {
                //删除相同的浏览历史
                foreach (var bhEntity in list)
                {
                    if (bhEntity.GoodsId == entity.GoodsId)
                    {
                        RedisTool.RemoveItemFromList<BrowseHistoryEntity>(userguid, bhEntity, FunctionType.ZuShouBrowseHistory, 0);
                    }
                }
                //添加最新的浏览历史
                AddBrowseHistory(userguid, entity);
            }
        }
        /// <summary>
        /// 作者:邓福伟
        /// 时间：2014-05-28
        /// 描述：返回浏览历史的前多少条记录
        /// </summary>
        /// <param name="userguid">用户Guid</param>
        /// <param name="topnum">前几条</param>
        /// <returns></returns>
        public List<BrowseHistoryEntity> GetBrowseHistoryTopList(string userguid,int topnum)
        {
            if (GetBrowseHistoryList(userguid) != null)
            {
                return GetBrowseHistoryList(userguid).Take(topnum).ToList();
            }
            return null;
        }

        /// <summary>
        /// 根据用户Guid获取全部的浏览历史
        /// </summary>
        /// <param name="UserGuid">用户Guid</param>
        /// <returns></returns>
        private List<BrowseHistoryEntity> GetBrowseHistoryList(string userguid)
        {
            List<BrowseHistoryEntity> list = RedisTool.GetAllItemsFromList<List<BrowseHistoryEntity>>(userguid, FunctionType.ZuShouBrowseHistory, 0);
           return list;
        }
        /// <summary>
        /// 添加浏览历史
        /// </summary>
        /// <returns></returns>
        private bool AddBrowseHistory(string userguid, BrowseHistoryEntity entity)
        {
            return RedisTool.AddItemToList<BrowseHistoryEntity>(entity, userguid, DateTime.Now.AddDays(30), FunctionType.ZuShouBrowseHistory, 0);
        }
    }
}
