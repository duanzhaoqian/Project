using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.DAL.Generalizes;
using KYJ.ZS.Models.Generalizes;

namespace KYJ.ZS.BLL.Generalizes
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-5-15
    /// 描述：推广
    /// </summary>
    public class GeneralizeBll
    {
        GeneralizeDal dal = new GeneralizeDal();
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：根据布局获取位置列表
        /// </summary>
        /// <param name="layout_id">布局Id</param>
        /// <returns>返回位置列表</returns>
        public List<Web_GeneralizeLocationEntity> Web_GetGeneralizeLocationEntity(int layout_id)
        {
            return dal.Web_GetGeneralizeLocationEntity(0, layout_id);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：根据布局和分类获取位置列表
        /// </summary>
        /// <param name="category_id">分类Id</param>
        /// <param name="layout_id">布局Id</param>
        /// <returns>获取位置列表</returns>
        public List<Web_GeneralizeLocationEntity> Web_GetGeneralizeLocationEntity(int category_id, int layout_id)
        {
            return dal.Web_GetGeneralizeLocationEntity(category_id, layout_id);
        }

        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：根据推广位置获取推广商品
        /// </summary>
        /// <param name="generalizelocation_id">推广位置id</param>
        /// <returns>返回推广商品列表</returns>
        public List<RentalGoodsListItemEntity> Web_GetGeneralizeGoodsEntity(int generalizelocation_id)
        {
            List<RentalGoodsListItemEntity> list = dal.Web_GetGeneralizeGoodsEntity(generalizelocation_id);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    RentalGoodsListItemEntity entity = list[i];
                    if (entity.GoodsName != null)
                    {
                        list[i].GoodsName = entity.GoodsName.Length <= 25 ? entity.GoodsName : entity.GoodsName.Substring(0, 25) + "...";
                    }
                }
            }
            else
            {
                list = new List<RentalGoodsListItemEntity>();
            }
            return list;
        }
    }
}
