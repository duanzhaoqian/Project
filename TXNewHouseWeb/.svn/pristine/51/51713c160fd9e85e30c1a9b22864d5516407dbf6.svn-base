using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXDal.NHouseActivies.Advertise;

namespace TXBll.NHouseActivies.Advertise
{
    public class AdvertiseBll
    {

        #region Local

        private readonly AdvertiseDal _advertiseDal = new AdvertiseDal();

        #endregion

        #region Method

        #region 根据ID获取广告

        /// <summary>
        /// 根据ID获取广告
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns>返回：广告实体 or null</returns>
        public TXOrm.Advertise GetAdvertise_ById(int id)
        {
            return _advertiseDal.GetAdvertise_ById(id);
        }
        #endregion

        #region 添加广告

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="model">对象实体</param>
        /// <returns>新增ID or 0</returns>
        public int AddAdvertise(TXOrm.Advertise model)
        {
            return _advertiseDal.AddAdvertise(model);
        }
        #endregion

        #region 根据ID标识删除广告

        /// <summary>
        /// 根据ID标识删除广告
        /// </summary>
        /// <param name="ids">集合</param>
        /// <returns>受影响行数or 错误0</returns>
        public int DeleteAdvertise_ById(int id)
        {
            return _advertiseDal.DeleteAdvertise_ById(id);
        }
        #endregion

        #region 更新广告

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="?"></param>
        /// <returns>受影响行数 or 错误0</returns>
        public int UpdateAdvertise(TXOrm.Advertise model)
        {
            return _advertiseDal.UpdateAdvertise(model);
        }
        #endregion

        #endregion
    }
}
