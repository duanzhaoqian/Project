using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Brands;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.BLL.Brands
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-7
    /// 描述：品牌操作类
    /// </summary>
    public class BrandBll
    {
        private BrandDal _dal = new BrandDal();

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-7
        /// 描述：通过分类ID查找品牌集合
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <returns></returns>
        public List<Brand> GetBrandsByCatId(int categoryId)
        {
            return _dal.GetBrandsByCatId(categoryId);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-13
        /// 描述：导航栏要显示的品牌
        /// </summary>
        /// <returns></returns>
        public List<Brand> Web_GetNavigationBrands()
        {
            return _dal.Web_GetNavigationBrands();
        }
    }
}
