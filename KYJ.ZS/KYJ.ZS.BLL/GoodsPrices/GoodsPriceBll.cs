using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.GoodsPrices;

namespace KYJ.ZS.BLL.GoodsPrices
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/23 9:45:05
    /// 描述：商品价格业务逻辑 
    /// </summary>
    public class GoodsPriceBll
    {
        GoodsPriceDal dal = new GoodsPriceDal();
        /// <summary>
        /// 根据ID返回信息
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public List<GoodsPrice> GetList(int id)
        {
            return dal.GetList(id);
        }

        /// <summary>
        /// 插入记录返回执行条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(GoodsPrice model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 根据实体更新表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(GoodsPrice model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据ID，直接删除数据。数据库删除，谨慎操作。
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public bool DeleteFormDateBase(int id)
        {
            return dal.DeleteFormDateBase(id);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：201-4-23
        /// 描述：获取租售商品的价格Model
        /// </summary>
        /// <param name="goods_id">商品Id</param>
        /// <param name="goodscolor_id">商品颜色Id</param>
        /// <param name="goodsnorm_id">商品规格Id</param>
        /// <returns>返回租售商品价格Model</returns>
        public GoodsPrice GetModel(int goods_id, int goodscolor_id, int goodsnorm_id)
        {
            return dal.GetModel(goods_id,goodscolor_id,goodsnorm_id);
        }
    }
}
