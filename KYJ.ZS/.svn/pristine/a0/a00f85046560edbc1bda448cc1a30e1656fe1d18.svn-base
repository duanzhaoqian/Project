using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Adverts;
using KYJ.ZS.DAL.Adverts;

namespace KYJ.ZS.BLL.Adverts
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-5-15
    /// 描述：广告
    /// </summary>
    public class AdvertBll
    {
        AdvertDal dal = new AdvertDal();
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：广告位置列表
        /// </summary>
        /// <param name="layout_id">布局id</param>
        /// <returns>返回广告位置列表</returns>
        public List<Web_AdvertLocationEntity> Web_GetAdvertLocationEntity(int layout_id)
        {
            return dal.Web_GetAdvertLocationEntity(0, layout_id);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：广告位置列表
        /// </summary>
        /// <param name="category_id">分类Id</param>
        /// <param name="layout_id">布局id</param>
        /// <returns>返回广告位置列表</returns>
        public List<Web_AdvertLocationEntity> Web_GetAdvertLocationEntity(int category_id, int layout_id)
        {
            return dal.Web_GetAdvertLocationEntity(category_id, layout_id);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：根据广告位置获取广告
        /// </summary>
        /// <param name="generalizelocation_id">广告位置id</param>
        /// <returns>返回位置广告列表</returns>
        public List<Web_AdvertEntity> Web_GetAdvertEntity(int advertlocation_id)
        {
            return dal.Web_GetAdvertEntity(advertlocation_id);
        }
    }
}
