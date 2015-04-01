using System;
using KYJ.ZS.DAL.FreightCosts;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.BLL.FreightCosts
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-21
    /// Desc：操作运费计算(数据库表FreightCosts)，包括查询、添加、修改
    /// </summary>
    public class FreightCostBll
    {
        private FreightCostDal dal = new FreightCostDal();

        /// <summary>
        /// 添加运费计算
        /// </summary>
        /// <param name="freightCost">运费计算</param>
        /// <returns></returns>
        public bool Create(FreightCost freightCost)
        {
            return dal.Create(freightCost) > 0;
        }

        /// <summary>
        /// 修改运费计算
        /// </summary>
        /// <param name="freightCost">运费计算</param>
        /// <returns></returns>
        public bool Update(FreightCost freightCost)
        {
            return dal.Update(freightCost) > 0;
        }

        /// <summary>
        /// 删除运费计算（按运费计算ID）
        /// </summary>
        /// <param name="freightCostID">运费计算ID</param>
        /// <returns></returns>
        public bool Delete(int freightCostID)
        {
            return dal.Delete(freightCostID) > 0;
        }

        /// <summary>
        /// 删除运费计算（按运费模板ID）
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public bool DeleteByTemplateID(int freightTemplateID)
        {
            return dal.DeleteByTemplateID(freightTemplateID) > 0;
        }

        /// <summary>
        /// 获取运费
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <param name="cityId">运送城市ID</param>
        /// <param name="total">计算总量</param>
        /// <returns></returns>
        public decimal GetFreightCost(int freightTemplateID, int cityId, decimal total)
        {
            FreightCost freightCost = dal.GetFreightCost(freightTemplateID, cityId);

            return total > freightCost.FirstIme ?
                freightCost.FirstPrice + Math.Ceiling((total - freightCost.FirstIme) / freightCost.ContinuetIme) * freightCost.ContinuePrice
                : freightCost.FirstPrice;

        }
    }
}
