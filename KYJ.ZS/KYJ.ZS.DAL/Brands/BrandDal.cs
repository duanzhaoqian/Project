using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.Brands
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-7
    /// 描述：品牌操作类
    /// </summary>
    public class BrandDal
    {
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-7
        /// 描述：通过分类ID查找品牌集合
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <returns></returns>
        public List<Brand> GetBrandsByCatId(int categoryId, bool isDel = false)
        {
            try
            {

                string sql = @"SELECT b.[brand_id] as Id
                          ,b.[brand_name] as Name
                          ,b.[brand_guid] as Guid
                          ,b.[brand_state] as State
                          ,b.[brand_sort] as Sort
                          ,b.[brand_createtime] as CreateTime
                          ,b.[brand_updatetime] as UpdateTime
                          ,b.[brand_isbrandcenter] as IsBrandCenter
                          ,b.[brand_isdel] as IsDel
                          FROM [dbo].[Brands] b WITH (NOLOCK)
                          left join [dbo].[CategoryBrands] c WITH (NOLOCK)
                          on b.[brand_id]=c.[brand_id] Where c.[category_id]=@categoryid and b.[brand_state]=2 and b.[brand_isdel]=@isDel";

                var param = new SqlParam();
                param.AddParam("@categoryid", categoryId);
                param.AddParam("@isDel", isDel);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Brand>.GetEntityList(dt);


            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<int>("wwang", categoryId, ex);
                return null;
            }

           
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-13
        /// 描述：导航栏要显示的品牌
        /// </summary>
        /// <returns></returns>
        public List<Brand> Web_GetNavigationBrands()
        {
            try
            {
                string sql = @"SELECT b.[brand_id] as Id
                          ,b.[brand_name] as Name
                          FROM [dbo].[Brands] b WITH (NOLOCK)
                          Where  [brand_state]=2 and [brand_isdel]=0 and [brand_isnavigation]=1";

                var dt = KYJ_ZushouRDB.GetTable(sql);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Brand>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "导航栏要显示的品牌", ex);
                return null;
            }
        }
    }
}
