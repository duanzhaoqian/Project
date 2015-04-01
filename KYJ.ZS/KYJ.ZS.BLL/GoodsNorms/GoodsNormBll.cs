using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.GoodsNorms;


namespace KYJ.ZS.Bll.GoodsNorms
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-4-22
    /// 描述：商品规格值
    /// </summary>
    public class GoodsNormBll
    {
        GoodsNormDal dal = new GoodsNormDal();
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 描述：根据goodsId获取商品规格
        /// </summary>
        /// <param name="goodsId">商品Id</param>
        /// <returns>商品规格集合</returns>
        public List<GoodsNorm> GetGoodsNormList(int goodsId)
        {
            return dal.GetGoodsNormList(goodsId);
        }
    }
}
