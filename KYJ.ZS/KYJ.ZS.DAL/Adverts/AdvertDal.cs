using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Adverts;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.Adverts
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-5-15
    /// 描述：广告
    /// </summary>
    public class AdvertDal
    {
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：广告位置列表
        /// </summary>
        /// <param name="category_id">分类Id</param>
        /// <param name="layout_id">布局id</param>
        /// <returns>返回广告位置列表</returns>
        public List<Web_AdvertLocationEntity> Web_GetAdvertLocationEntity(int category_id, int layout_id)
        {
            try
            {
                #region sql
                var sql = @" 
                        select advertlocation_id as Id,
		                       advertlocation_name as ALocationName,
		                       advertlocation_num as ALocationNum,
                                advertlocation_width as Width,
                                advertlocation_height as Height
                        from dbo.AdvertLocations with(nolock)  
	                    where category_id=@category_id  and --分类Id
                              layout_id=@layout_id    --布局Id
                        order by  advertlocation_sort
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

                return DataHelper<Web_AdvertLocationEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "广告位置列表数据异常", ex);
                return null;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-15
        /// 描述：根据广告位置获取广告
        /// </summary>
        /// <param name="generalizelocation_id">广告位置id</param>
        /// <returns>返回广告列表</returns>
        public List<Web_AdvertEntity> Web_GetAdvertEntity(int advertlocation_id)
        {
            try
            {
                #region sql
                var sql = @" 
                        select advert_id as Id, advert_name as AdvertName,advert_guid as AdvertGuid, advert_url as Url
                        from ( 
	                        select advert_id,
		                           advert_name,
		                           advert_guid,
		                           advert_url,
		                           row_number() over (partition by advert_sort order by advert_sort,advert_updatetime desc ) as id
	                        from dbo.Adverts with(nolock)
	                        where advert_state=1 and  --上架
		                          advert_aduitstate=5 and --审核通过
		                          advert_isdel=0 and --没有删除
		                          getdate() between advert_begintime and advert_endtime and --开始时间和结束时间范围内
		                          advertlocation_id=@advertlocation_id   --位置Id
	                   ) a
                        where id=1";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@advertlocation_id", advertlocation_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_AdvertEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "广告列表数据异常", ex);
                return null;
            }
        }
    }
}
