using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.Models.Generalizes;

namespace KYJ.ZS.DAL.Generalizes
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-5-15
    /// 描述：推广
    /// </summary>
    public class GeneralizeDal
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-16
        /// 描述：获取推广位置列表
        /// </summary>
        /// <param name="category_id">分类Id</param>
        /// <param name="layout_id">布局Id</param>
        /// <returns>推广位置列表</returns>
        public List<Web_GeneralizeLocationEntity> Web_GetGeneralizeLocationEntity(int category_id, int layout_id)
        {
            try
            {
                #region sql
                var sql = @" 
                        select generalizelocation_id as Id,
		                       generalizelocation_name as GLocationName,
		                       generalizelocation_num as GLocationNum
                        from dbo.GeneralizeLocations with(nolock)  
	                   where category_id=@category_id  and --分类Id
                              layout_id=@layout_id    --布局Id
                        order by generalizelocation_sort
              ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@category_id", category_id, SqlDbType.Int, 4);
                param.AddParam("@layout_id", layout_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_GeneralizeLocationEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "推广位置列表", ex);
                return null;
            }
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
            try
            {
                #region sql
                var sql = @" 
                        select rg.rental_id as GoodsId,             --商品Id
                               rg.rental_title as GoodsName,       --商品标题
                               rg.rental_guid as Guid,   --商品GUId
                               rg.rental_price as Price, --市场价格
                               case when min(gp.goods_price) is null 
                                    then rg.rental_monthprice 
                                    else min(gp.goods_price)
                               end as MonthPrice             --商品月价格
                        from dbo.GeneralizeGoodses gg with(nolock) --推广商品表
                        right join (
                                    select top 1 g.generalize_Id
                                    from dbo.Generalizes g with(nolock)
	                                where g.generalize_state=1 and  --上架
		                                  g.generalize_aduitstate=5 and --审核通过
		                                  g.generalize_isdel=0 and --没有删除
		                                  getdate() between g.generalize_begintime and  g.generalize_endtime and --开始时间和结束时间范围内
		                                  g.generalizelocation_id=@generalizelocation_id  --位置Id
                                   order by g.generalize_updatetime desc
                        ) glj on gg.generalize_Id=glj.generalize_Id  --推广表
                        left join dbo.RentalGoodses rg with(nolock) on gg.rental_id=rg.rental_id   --商品表
                        left join dbo.GoodsPrices gp with(nolock) on rg.rental_id=gp.goods_id      --商品价格表
                        where rg.rental_id is not null and rg.[rental_isdel]=0 and rg.[rental_status]=1 and rg.[rental_state]=2 
                        group by rg.rental_id,rg.rental_title,rg.rental_guid,rg.rental_price,rg.rental_monthprice,gg.generalizegoods_sort
                        order by gg.generalizegoods_sort
              ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@generalizelocation_id", generalizelocation_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<RentalGoodsListItemEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "推广商品位置列表", ex);
                return null;
            }
        }
    }
}
