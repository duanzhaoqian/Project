using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Collections;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Collections;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons;

namespace KYJ.ZS.BLL.Collections
{
    public class CollectionBll
    {
        public CollectionDal _dal = null;
        public CollectionBll()
        {
            if (_dal == null)
            {
                _dal = new CollectionDal();
            }
        }

        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-25
        /// Desc:删除收藏方法
        /// </summary>
        /// <param name="collId">收藏表ID</param>
        /// <returns></returns>
        public int DelCollection(int userId, string collId)
        {
            return _dal.DelCollection(userId, collId);
        }
        /// <summary>
        /// 用户后台收藏列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public List<CollectionEntity> GetCollectionGoodsesPage(int userId, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return _dal.GetCollectionGoodsesPage(userId, index, pageSize, out totalRecord, out totalPage);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：添加收藏功能
        /// </summary>
        /// <param name="user_id">用户Id</param>
        /// <param name="goods_id">商品Id</param>
        /// <param name="goods_url">商品类型</param>
        /// <returns></returns>
        public int Web_AddCollection(int user_id, int goods_id, int goods_type)
        {
            return _dal.Web_AddCollection(user_id, goods_id, goods_type);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：商品收藏数
        /// </summary>
        /// <param name="goods_id">商品Id</param>
        /// <returns>收藏数量</returns>
        public int Web_CollectionCount(int goods_id)
        {
            int CollectionCount = _dal.Web_CollectionCount(goods_id);
            return CollectionCount;
        }
    }
}
