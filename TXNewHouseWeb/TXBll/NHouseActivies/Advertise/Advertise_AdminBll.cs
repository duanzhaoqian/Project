using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXDal.NHouseActivies.Advertise;

namespace TXBll.NHouseActivies.Advertise
{
    public class Advertise_AdminBll
    {
        #region Local

        private readonly Advertise_AdminDal _advertiseDal = new Advertise_AdminDal();

        #endregion

        #region Method

        #region 查询广告列表

        /// <summary>
        /// 查询广告列表
        /// author:wangdk
        /// </summary>
        /// <param name="cityID">城市</param>
        /// <param name="position">位置1热点新盘推荐;2最新楼盘推荐;3.精品楼盘;4.即将开盘;</param>
        /// <returns>广告集合or null</returns>
        public List<TXOrm.Advertise> GetAdvertiseList_Admin(int cityID, int position)
        {
            return _advertiseDal.GetAdvertiseList_Admin(cityID, position);
        }

        #endregion

        #region 批量新增广告

        /// <summary>
        /// 批量添加广告
        /// author:wangdk
        /// </summary>
        /// <param name="adList">广告集合</param>
        /// <returns>success:1,error:0</returns>
        public int AddAdvertiseList(List<TXOrm.Advertise> adList)
        {
            return _advertiseDal.AddAdvertiseList(adList);
        }

        #endregion

        #region 删除并重新排序号

        /// <summary>
        /// 根据ID删除并重新排序号
        /// author:wangdk
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns>success:1 error:0</returns>
        public int DeleteAdvertise_ById(int id)
        {
            return _advertiseDal.DeleteAdvertise_ById(id);
        }
        #endregion

        #region 批量更新广告

        /// <summary>
        ///批量更新广告
        /// author:wangdk
        /// </summary>
        /// <param name="">广告集合</param>
        /// <returns>success:1 error:0</returns>
        public int UpdateAdvertiseList(List<TXOrm.Advertise> adList)
        {
            return _advertiseDal.UpdateAdvertiseList(adList);
        }

        #endregion

        #region 根据ID是否存在判断更新与添加操作

        /// <summary>
        /// 根据ID是否存在判断更新与添加操作
        /// </summary>
        /// <param name="adList">广告集合</param>
        /// <returns>success:1 error:0</returns>
        public int AddOrUpdateAdvertise(List<TXOrm.Advertise> adList)
        {
            return _advertiseDal.AddOrUpdateAdvertise(adList);
        }

        #endregion

        #endregion
    }
}
