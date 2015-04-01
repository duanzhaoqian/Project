using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons.Common;

namespace KYJ.ZS.BLL.BrowseAmounts
{
    public class BrowseAmountBll
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-19
        /// 描述：获取租商品的浏览量
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>浏览数</returns>
        public int Web_GetRentalGoodsBrowseAmount(int rental_id)
        {
            string key = "Z" + rental_id.ToString();
            int browseAmount = 0;
            browseAmount = RedisTool.GetValue<int>(key, FunctionType.ZuShouBrowseAmount, 0);
            browseAmount++;
            RedisTool.SetValue<int>(key, browseAmount, null, FunctionType.ZuShouBrowseAmount, 0);
            return browseAmount;
        }

        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-19
        /// 描述：获取售商品的浏览量
        /// </summary>
        /// <param name="sale_id">商品Id</param>
        /// <returns>浏览量</returns>
        public int Web_GetSaleGoodsBrowseAmount(int sale_id)
        {
            string key = "S" + sale_id.ToString();
            int browseAmount = 0;
            browseAmount = RedisTool.GetValue<int>(key, FunctionType.ZuShouBrowseAmount, 0);
            browseAmount++;
            RedisTool.SetValue<int>(key, browseAmount, null, FunctionType.ZuShouBrowseAmount, 0);
            return browseAmount;
        }
    }
}
