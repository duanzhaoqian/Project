using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.GoodsColors;


namespace KYJ.ZS.Bll.GoodsColors
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-4-22
    /// 描述：商品颜色
    /// </summary>
    public class GoodsColorBll
    {

        GoodsColorDal dal = new GoodsColorDal();
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 描述：根据goodsId获取商品颜色
        /// </summary>
        /// <param name="goodsId">商品Id</param>
        /// <returns>商品颜色集合</returns>
        public List<GoodsColor> GetGoodsColorList(int goodsId)
        {
            return dal.GetGoodsColorList(goodsId);
        }
    }
}
