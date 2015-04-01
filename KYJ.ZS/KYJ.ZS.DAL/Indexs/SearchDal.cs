using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace KYJ.ZS.DAL.Indexs
{
    using KYJ.ZS.DAL.DB;

    public  class SearchDal
    {
        /// <summary>
        /// 返回品牌数据
        /// </summary>
        /// <param name="brand_id"></param>
        /// <returns></returns>
        public DataTable GetBrands(string brand_id)
        {
            string sql = "select * from Brands where brand_isdel=0 " + brand_id;
            if (!string.IsNullOrEmpty(brand_id))
                sql += " and brand_id=" + brand_id;
            return KYJ_ZushouRDB.GetTable(sql);
        }
        /// <summary>
        /// 返回品牌分类
        /// </summary>
        /// <param name="brand_id"></param>
        /// <returns></returns>
        public DataTable GetCategoryBrands(string  brand_id)
        {
            string sql = "select * from CategoryBrands where brand_id=" + brand_id;
            return KYJ_ZushouRDB.GetTable(sql);
        }
        /// <summary>
        /// 返回属性数据
        /// </summary>
        /// <param name="attr_id"></param>
        /// <returns></returns>
        public DataTable GetAttributes(string attr_id)
        {
            string sql = "select * from Attributes where attr_isdel=0  ";
            if (!string.IsNullOrEmpty(attr_id))
                sql += " and attr_isdel=" + attr_id;
            return KYJ_ZushouRDB.GetTable(sql);
        }
        /// <summary>
        /// 返回属性分类关系
        /// </summary>
        /// <param name="attr_id"></param>
        /// <returns></returns>
        public DataTable GetCategoryAttributes(string  attr_id)
        {
            string sql = "select * from CategoryAttributes where attr_id=" + attr_id;
            return KYJ_ZushouRDB.GetTable(sql);
        }
        /// <summary>
        /// 返回所有分类 
        /// </summary>
        /// <param name="attr_id"></param>
        /// <returns></returns>
        public DataTable GetCategories()
        {
            string sql = "select * from Categories where category_isdel=0  ";
            
            return KYJ_ZushouRDB.GetTable(sql);
        }

        /// <summary>
        /// 返回所有出售商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetSaleGoodses(string id)
        {
            string sql = "Select * from SaleGoodses Where sale_isdel=0";
            if (!string.IsNullOrEmpty(id))
            {
                sql += " and sale_id=" + id;
            }
            return KYJ_ZushouRDB.GetTable(sql);
        }

        /// <summary>
        /// 返回所有出租商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRentalGoodses(string id)
        {
            string sql = "Select * from RentalGoodses where rental_isdel=0";
            if (!string.IsNullOrEmpty(id))
            {
                sql += " and rental_id=" + id;
            }
            return KYJ_ZushouRDB.GetTable(sql);
        }

    }
}
