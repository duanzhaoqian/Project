using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.DAL.RentalGoodses
{
    using System.Transactions;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 15:12:26
    /// 描述：出租商品操作类
    /// </summary>
    public partial class RentalGoodsDal
    {
        #region 邓福伟
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-22
        /// 描述：Web前端租商品详情页,租商品详情Model
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_RentalGoodsEntity Web_GetRentalGoodsEntity(int rental_id)
        {
            try
            {
                #region sql
                var sql = @"/*获取租商品价格、数目、商品编码*/
                        declare @GoodsPriceId int,  --商品价格Id
                                @GoodsPrice nvarchar(100), --商品价格
                                @GoodsPriceNum int, --商品数量
                                @GoodsPriceCode nvarchar(20) --商品编码

                        select  @GoodsPriceId=min(goodsprice_id) --商品价格Id
                               ,@GoodsPrice=(case when min(goods_price)=max(goods_price)
			                        then cast(min(goods_price) as nvarchar(20))
			                        else cast(min(goods_price) as nvarchar(20))+'-'+cast(max(goods_price) as nvarchar(20)) 
			                        end) --商品价格
                               ,@GoodsPriceNum=sum(goodsprice_num)  --商品数量
       
                        FROM [dbo].[GoodsPrices] with(nolock)
                        Where goods_id=@goods_id
                        Group BY goods_id

                        select @GoodsPriceCode=goodsprice_code from [dbo].[GoodsPrices] with(nolock) where goodsprice_id=@GoodsPriceId

                        /*商品详情页数据，不包括租期模版*/
                        SELECT rg.[rental_id] as Id --商品Id
                              ,rg.[merchant_id] as MerchantId   --商户Id
                              ,rg.[rental_guid] as RentalGuid   --商品GUId
                              ,rg.[rental_cityname]+'-'+rg.[rental_districtname] as Location --商品所在地
                              ,rg.[rental_title] as Title       --商品名称
                              ,rg.[rental_price] as Price       --商品原价格
                              ,rg.[rental_latefees] as LateFees --商品带纳金
                              ,rg.[rental_deposit] as Deposit   --商品押金
                              ,rg.[ftemp_id] as FtempId         --运费模板Id
                              ,rg.[rental_begintime] as BeginTime --开始时间
                              ,rg.[rental_endtime] as EndTime     --结束时间
                              ,rg.[rental_weight] as Weight       --重量
                              ,rg.[rental_volume] as Volume      --体积
                              ,rg.[rental_categoryid] as CategoryId --分类Id
                              ,rg.[rental_brandid] as BrandId      --品牌Id
                              ,rg.[rental_brandname] as BrandName  --品牌名称

 
                              ,mh.merchant_guid as MerchantGuid   --商铺GUId
                              ,mh.merchant_name as MerchantName   --商铺名称

                              ,case when @GoodsPrice Is null 
                               then cast(rg.[rental_monthprice] as nvarchar(20)) 
                               else @GoodsPrice 
                               end as MothPrice  --商品月价格
                              ,case when @GoodsPriceNum Is null 
                               then rg.[rental_number] 
                               else @GoodsPriceNum 
                               end as Number     --商品总数量
                              ,case when @GoodsPriceCode Is null 
                               then cast(rg.[rental_code] as nvarchar(20)) 
                               else @GoodsPriceCode 
                               end as Code       --商家编码


                              ,tt.[ttemp_months] as TempMonths    --租期模版

                              ,ro.other_attrs as OtherAttrs       --属性扩展
                              ,ro.other_desc as OtherDesc         --描述扩展
                              ,ro.other_colors as OtherColors     --颜色扩展
                              ,ro.other_norms as OtherNorms       --规格扩展
                              ,ro.other_prices as OtherPrices     --价格扩展
                        FROM [dbo].[RentalGoodses] as rg with(nolock)                               ----租商品表                                ----租商品价格表
                        Left Join dbo.TenancyTemplates tt with(nolock) on rg.ttemp_id=tt.ttemp_id   ----租期表
                        Left Join dbo.RentalOthers ro with(nolock) on rg.rental_id=ro.rental_id     ----租商品扩展表
                        Left Join dbo.Merchants mh with(nolock) on rg.merchant_id=mh.merchant_id
                        Where rg.[rental_id]=@rental_id and rg.[rental_isdel]=0 and rg.[rental_status]=1 and rg.[rental_state]=2";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@goods_id", rental_id, SqlDbType.Int, 4);
                param.AddParam("@rental_id", rental_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_RentalGoodsEntity>.GetEntity(dt.Rows[0]);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "租商品详情Model", ex);
                return null;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-22
        /// 描述：Web前端租商品详情页,租商品详情Model预览页
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_RentalGoodsEntity Web_GetRentalGoodsPreviewEntity(int rental_id)
        {
            try
            {
                #region sql
                var sql = @"/*获取租商品价格、数目、商品编码*/
                        declare @GoodsPriceId int,  --商品价格Id
                                @GoodsPrice nvarchar(100), --商品价格
                                @GoodsPriceNum int, --商品数量
                                @GoodsPriceCode nvarchar(20) --商品编码

                        select  @GoodsPriceId=min(goodsprice_id) --商品价格Id
                               ,@GoodsPrice=(case when min(goods_price)=max(goods_price)
			                        then cast(min(goods_price) as nvarchar(20))
			                        else cast(min(goods_price) as nvarchar(20))+'-'+cast(max(goods_price) as nvarchar(20)) 
			                        end) --商品价格
                               ,@GoodsPriceNum=sum(goodsprice_num)  --商品数量
       
                        FROM [dbo].[GoodsPrices] with(nolock)
                        Where goods_id=@goods_id
                        Group BY goods_id

                        select @GoodsPriceCode=goodsprice_code from [dbo].[GoodsPrices] with(nolock) where goodsprice_id=@GoodsPriceId

                        /*商品详情页数据，不包括租期模版*/
                        SELECT rg.[rental_id] as Id --商品Id
                              ,rg.[merchant_id] as MerchantId   --商户Id
                              ,rg.[rental_guid] as RentalGuid   --商品GUId
                              ,rg.[rental_cityname]+'-'+rg.[rental_districtname] as Location --商品所在地
                              ,rg.[rental_title] as Title       --商品名称
                              ,rg.[rental_price] as Price       --商品原价格
                              ,rg.[rental_latefees] as LateFees --商品带纳金
                              ,rg.[rental_deposit] as Deposit   --商品押金
                              ,rg.[ftemp_id] as FtempId         --运费模板Id
                              ,rg.[rental_begintime] as BeginTime --开始时间
                              ,rg.[rental_endtime] as EndTime     --结束时间
                              ,rg.[rental_weight] as Weight       --重量
                              ,rg.[rental_volume] as Volume      --体积
                              ,rg.[rental_categoryid] as CategoryId --分类Id
                              ,rg.[rental_brandid] as BrandId      --品牌Id
                              ,rg.[rental_brandname] as BrandName  --品牌名称

 
                              ,mh.merchant_guid as MerchantGuid   --商铺GUId
                              ,mh.merchant_name as MerchantName   --商铺名称

                              ,case when @GoodsPrice Is null 
                               then cast(rg.[rental_monthprice] as nvarchar(20)) 
                               else @GoodsPrice 
                               end as MothPrice  --商品月价格
                              ,case when @GoodsPriceNum Is null 
                               then rg.[rental_number] 
                               else @GoodsPriceNum 
                               end as Number     --商品总数量
                              ,case when @GoodsPriceCode Is null 
                               then cast(rg.[rental_code] as nvarchar(20)) 
                               else @GoodsPriceCode 
                               end as Code       --商家编码


                              ,tt.[ttemp_months] as TempMonths    --租期模版

                              ,ro.other_attrs as OtherAttrs       --属性扩展
                              ,ro.other_desc as OtherDesc         --描述扩展
                              ,ro.other_colors as OtherColors     --颜色扩展
                              ,ro.other_norms as OtherNorms       --规格扩展
                              ,ro.other_prices as OtherPrices     --价格扩展
                        FROM [dbo].[RentalGoodses] as rg with(nolock)                               ----租商品表                                ----租商品价格表
                        Left Join dbo.TenancyTemplates tt with(nolock) on rg.ttemp_id=tt.ttemp_id   ----租期表
                        Left Join dbo.RentalOthers ro with(nolock) on rg.rental_id=ro.rental_id     ----租商品扩展表
                        Left Join dbo.Merchants mh with(nolock) on rg.merchant_id=mh.merchant_id
                        Where rg.[rental_id]=@rental_id ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@goods_id", rental_id, SqlDbType.Int, 4);
                param.AddParam("@rental_id", rental_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_RentalGoodsEntity>.GetEntity(dt.Rows[0]);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "租商品详情Model预览页", ex);
                return null;
            }
        }
        /// <summary>
        /// 作者: 邓福伟
        /// 时间：2014-04-22
        /// 描述: 根据商铺Id随机获取10个商品
        /// </summary>
        /// <param name="merchant_id">商铺Id</param>
        /// <returns></returns>
        public List<Web_RentalGoodsEntity> Web_GetRentalGoodsListByMerchantId(int rental_id, int merchant_id)
        {
            try
            {
                #region sql
                var sql = @" 
                        select top 10 rg.rental_id as Id,             --商品Id
                               rg.rental_title as Title,       --商品标题
                               rg.rental_guid as RentalGuid,   --商品GUId
                               rg.rental_price as Price, --市场价格
                               case when min(gp.goods_price) is null 
                                    then rg.rental_monthprice 
                                    else min(gp.goods_price)
                               end as MothPrice             --商品月价格
                        from  dbo.RentalGoodses rg with(nolock) 
                        left join dbo.GoodsPrices gp with(nolock) on rg.rental_id=gp.goods_id 
                        where rg.[merchant_id]=@merchant_id and rg.[rental_id]<>@rental_id and rg.[rental_isdel]=0 and rg.[rental_status]=1 and rg.[rental_state]=2
                        group by merchant_id,rg.rental_id,rg.rental_title,rg.rental_guid,rg.rental_price,rg.rental_monthprice,rg.rental_updatetime
                        order by rg.rental_updatetime desc";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@rental_id", rental_id, SqlDbType.Int, 4);
                param.AddParam("@merchant_id", merchant_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_RentalGoodsEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "根据商铺Id随机获取10个商品", ex);
                return null;
            }
        }
        /// <summary>
        /// 作者: 邓福伟
        /// 时间：2014-04-22
        /// 描述: 根据分类随机获取10个商品
        /// </summary>
        /// <param name="merchant_id">分类Id</param>
        /// <returns></returns>
        public List<Web_RentalGoodsEntity> Web_GetRentalGoodsListByCategoryId(int rental_id, int rental_categoryid)
        {
            try
            {
                #region sql
                var sql = @" 
                        select top 10 rg.rental_id as Id,             --商品Id
                               rg.rental_title as Title,       --商品标题
                               rg.rental_guid as RentalGuid,   --商品GUId
                               rg.rental_price as Price, --市场价格
                               case when min(gp.goods_price) is null 
                                    then rg.rental_monthprice 
                                    else min(gp.goods_price)
                               end as MothPrice             --商品月价格
                        from  dbo.RentalGoodses rg with(nolock) 
                        left join dbo.GoodsPrices gp with(nolock) on rg.rental_id=gp.goods_id 
                        where rg.[rental_categoryid]=@rental_categoryid and rg.[rental_id]<>@rental_id and rg.[rental_isdel]=0 and rg.[rental_status]=1 and rg.[rental_state]=2
                        group by rental_categoryid,rg.rental_id,rg.rental_title,rg.rental_guid,rg.rental_price,rg.rental_monthprice,rg.rental_updatetime
                        order by rg.rental_updatetime desc";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@rental_id", rental_id, SqlDbType.Int, 4);
                param.AddParam("@rental_categoryid", rental_categoryid, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_RentalGoodsEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "根据分类随机获取10个商品", ex);
                return null;
            }

        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-6-12
        /// 描述：根据商品Id取商品的价格扩展
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品价格扩展</returns>
        public string Web_GetRentalOtherPrice(int rental_id)
        {
            try
            {
                #region sql
                var sql = @" 
                        Select other_prices From dbo.RentalOthers with(nolock)
                        Where rental_id=@rental_id";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@rental_id", rental_id, SqlDbType.Int, 4);
                #endregion
                #region 返回
                return KYJ_ZushouRDB.GetFirst(sql, param);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "根据商品Id取商品的价格扩展", ex);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public RentalGoods Get(int id)
        {
            try
            {
                string sql = @"SELECT [rental_id] as Id
                          ,[merchant_id] as MerchantId
                          ,[rental_provinceid] as ProvinceId
                          ,[rental_provincename] as ProvinceName
                          ,[rental_cityid] as CityId
                          ,[rental_cityname] as CityName
                          ,[rental_districtid] as DistrictId
                          ,[rental_districtname] as DistrictName
                          ,[rental_title] as Title
                          ,[rental_number] as Number
                          ,[rental_rentnum] as RentNum
                          ,[rental_salesvolume] as SalesVolume
                          ,[rental_code] as Code
                          ,[rental_barcode] as Barcode
                          ,[rental_price] as Price
                          ,[rental_monthprice] as MonthPrice
                          ,[rental_deposit] as Deposit
                          ,[rental_latefees] as LateFees
                          ,[ttemp_id] as TtempId
                          ,[ftemp_id] as FtempId
                          ,[rental_mode] as Mode
                          ,[rental_validity] as Validity
                          ,[rental_begintime] as BeginTime
                          ,[rental_endtime] as EndTime
                          ,[rental_guid] as Guid
                          ,[rental_status] as Status
                          ,[rental_state] as State
                          ,[rental_createtime] as CreateTime
                          ,[rental_updatetime] as UpdateTime
                          ,[rental_isdel] as IsDel
                          ,[admin_id] as AdminId
                          ,[admin_name] as AdminName
                            ,[rental_tags] as Tags
                            ,[rental_weight] as Weight
                            ,[rental_volume] as Volume
                            ,[rental_brandid] as BrandId
                            ,[rental_brandname] as BrandName
                            ,[rental_categoryid] as CategoryId
                 FROM  [dbo].[RentalGoodses] NOLOCK
                 WHERE rental_id=@rental_id";

                var param = new SqlParam();
                param.AddParam("rental_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<RentalGoods>.GetEntity(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return null;
            }
        }

        /// <summary>
        /// 插入记录返回结果（返回结果默认执行条数，isReturnId=true返回记录ID）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isReturnId">是否返回ID，默认返回执行条数</param>
        /// <returns></returns>
        public int Add(RentalGoods model, bool isReturnId = false)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("RentalGoodsDal.Add,参数不可为空");

                string sql = @"INSERT INTO [dbo].[RentalGoodses]
                       ([merchant_id]
                      ,[rental_provinceid]
                      ,[rental_provincename]
                      ,[rental_cityid]
                      ,[rental_cityname]
                      ,[rental_districtid]
                      ,[rental_districtname]
                      ,[rental_title]
                      ,[rental_number]
                      ,[rental_rentnum]
                      ,[rental_salesvolume]
                      ,[rental_code]
                      ,[rental_barcode]
                      ,[rental_price]
                      ,[rental_monthprice]
                      ,[rental_deposit]
                      ,[rental_latefees]
                      ,[ttemp_id]
                      ,[ftemp_id]
                      ,[rental_mode]
                      ,[rental_validity]
                      ,[rental_begintime]
                      ,[rental_endtime]
                      ,[rental_guid]
                      ,[rental_status]
                      ,[rental_state]
                      ,[rental_createtime]
                      ,[rental_updatetime]
                      ,[rental_isdel]
                      ,[admin_id]
                      ,[admin_name]
                      ,[rental_tags]
                      ,[rental_weight]
                      ,[rental_volume]
                      ,[rental_brandid]
                      ,[rental_brandname]
                        ,[rental_categoryid])
                 VALUES
                     (@merchant_id
                      ,@rental_provinceid
                      ,@rental_provincename
                      ,@rental_cityid
                      ,@rental_cityname
                      ,@rental_districtid
                      ,@rental_districtname
                      ,@rental_title
                      ,@rental_number
                      ,@rental_rentnum
                      ,@rental_salesvolume
                      ,@rental_code
                      ,@rental_barcode
                      ,@rental_price
                      ,@rental_monthprice
                      ,@rental_deposit
                      ,@rental_latefees
                      ,@ttemp_id
                      ,@ftemp_id
                      ,@rental_mode
                      ,@rental_validity
                      ,@rental_begintime
                      ,@rental_endtime
                      ,@rental_guid
                      ,@rental_status
                      ,@rental_state
                      ,@rental_createtime
                      ,@rental_updatetime
                      ,@rental_isdel
                      ,@admin_id
                      ,@admin_name
                      ,@rental_tags
                      ,@rental_weight
                      ,@rental_volume
                      ,@rental_brandid
                      ,@rental_brandname
                        ,@rental_categoryid)";

                var param = new SqlParam();
                param.AddParam("merchant_id", model.MerchantId);
                param.AddParam("rental_provinceid", model.ProvinceId);
                param.AddParam("rental_provincename", model.ProvinceName);
                param.AddParam("rental_cityid", model.CityId);
                param.AddParam("rental_cityname", model.CityName);
                param.AddParam("rental_districtid", model.DistrictId);
                param.AddParam("rental_districtname", model.DistrictName);
                param.AddParam("rental_title", model.Title);
                param.AddParam("rental_number", model.Number);
                param.AddParam("rental_rentnum", model.RentNum);
                param.AddParam("rental_salesvolume", model.SalesVolume);
                param.AddParam("rental_code", model.Code);
                param.AddParam("rental_barcode", model.Barcode);
                param.AddParam("rental_price", model.Price);
                param.AddParam("rental_monthprice", model.MonthPrice);
                param.AddParam("rental_deposit", model.Deposit);
                param.AddParam("rental_latefees", model.LateFees);
                param.AddParam("ttemp_id", model.TtempId);
                param.AddParam("ftemp_id", model.FtempId);
                param.AddParam("rental_mode", model.Mode);
                param.AddParam("rental_validity", model.Validity);
                if (model.BeginTime == null)
                    param.AddParam("rental_begintime", DBNull.Value);
                else
                    param.AddParam("rental_begintime", model.BeginTime);
                if (model.EndTime == null)
                    param.AddParam("rental_endtime", DBNull.Value);
                else
                    param.AddParam("rental_endtime", model.EndTime);
                param.AddParam("rental_guid", model.Guid);
                param.AddParam("rental_status", model.Status);
                param.AddParam("rental_state", model.State);
                param.AddParam("rental_createtime", model.CreateTime);
                param.AddParam("rental_updatetime", model.UpdateTime);
                param.AddParam("rental_isdel", model.IsDel);
                param.AddParam("admin_id", model.AdminId);
                param.AddParam("admin_name", model.AdminName);

                param.AddParam("rental_tags", model.Tags);
                param.AddParam("rental_weight", model.Weight);
                param.AddParam("rental_volume", model.Volume);
                param.AddParam("rental_brandid", model.BrandId);
                param.AddParam("rental_brandname", model.BrandName);
                param.AddParam("rental_categoryid", model.CategoryId);

                if (isReturnId)
                    return KYJ_ZushouWDB.SqlExecuteRuturnId(sql, param);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return 0;
            }

        }


        /// <summary>
        /// 插入记录返回实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RentalGoods AddReturnEntity(RentalGoods model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("RentalGoodsDal.AddReturnEntity,参数不可为空");

                var id = this.Add(model, true);

                return this.Get(id);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return null;
            }

        }

        /// <summary>
        /// 根据实体更新表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(RentalGoods model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("RentalGoodsDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[RentalGoodses]
                                   SET [merchant_id] = @merchant_id
                                      ,[rental_provinceid] = @rental_provinceid
                                      ,[rental_provincename] = @rental_provincename
                                      ,[rental_cityid] = @rental_cityid
                                      ,[rental_cityname] = @rental_cityname
                                      ,[rental_districtid] = @rental_districtid
                                      ,[rental_districtname] = @rental_districtname
                                      ,[rental_title] = @rental_title
                                      ,[rental_number] = @rental_number
                                      ,[rental_rentnum] = @rental_rentnum
                                      ,[rental_salesvolume] = @rental_salesvolume
                                      ,[rental_code] = @rental_code
                                      ,[rental_barcode] = @rental_barcode
                                      ,[rental_price] = @rental_price
                                      ,[rental_monthprice] = @rental_monthprice
                                      ,[rental_deposit] = @rental_deposit
                                      ,[rental_latefees] = @rental_latefees
                                      ,[ttemp_id] = @ttemp_id
                                      ,[ftemp_id] = @ftemp_id
                                      ,[rental_mode] = @rental_mode
                                      ,[rental_validity] = @rental_validity
                                      ,[rental_begintime] = @rental_begintime
                                      ,[rental_endtime] = @rental_endtime
                                      ,[rental_guid] = @rental_guid
                                      ,[rental_status] = @rental_status
                                      ,[rental_state] = @rental_state
                                      ,[rental_createtime] = @rental_createtime
                                      ,[rental_updatetime] = @rental_updatetime
                                      ,[rental_isdel] = @rental_isdel
                                      ,[admin_id] = @admin_id
                                      ,[admin_name] = @admin_name
                                        ,[rental_tags] = @rental_tags
                                        ,[rental_weight] = @rental_weight
                                        ,[rental_volume] = @rental_volume
                                        ,[rental_brandid] = @rental_brandid
                                        ,[rental_brandname] = @rental_brandname
                                        ,[rental_categoryid] = @rental_categoryid
                                 WHERE rental_id = @rental_id";

                var param = new SqlParam();
                param.AddParam("rental_id", model.Id);
                param.AddParam("merchant_id", model.MerchantId);
                param.AddParam("rental_provinceid", model.ProvinceId);
                param.AddParam("rental_provincename", model.ProvinceName);
                param.AddParam("rental_cityid", model.CityId);
                param.AddParam("rental_cityname", model.CityName);
                param.AddParam("rental_districtid", model.DistrictId);
                param.AddParam("rental_districtname", model.DistrictName);
                param.AddParam("rental_title", model.Title);
                param.AddParam("rental_number", model.Number);
                param.AddParam("rental_rentnum", model.RentNum);
                param.AddParam("rental_salesvolume", model.Number);
                param.AddParam("rental_code", model.Code);
                param.AddParam("rental_barcode", model.Barcode);
                param.AddParam("rental_price", model.Price);
                param.AddParam("rental_monthprice", model.MonthPrice);
                param.AddParam("rental_deposit", model.Deposit);
                param.AddParam("rental_latefees", model.LateFees);
                param.AddParam("ttemp_id", model.TtempId);
                param.AddParam("ftemp_id", model.FtempId);
                param.AddParam("rental_mode", model.Mode);
                param.AddParam("rental_validity", model.Validity);
                if (model.BeginTime == null)
                    param.AddParam("rental_begintime", DBNull.Value);
                else
                    param.AddParam("rental_begintime", model.BeginTime);
                if (model.EndTime == null)
                    param.AddParam("rental_endtime", DBNull.Value);
                else
                    param.AddParam("rental_endtime", model.EndTime);
                param.AddParam("rental_guid", model.Guid);
                param.AddParam("rental_status", model.Status);
                param.AddParam("rental_state", model.State);
                param.AddParam("rental_createtime", model.CreateTime);
                param.AddParam("rental_updatetime", model.UpdateTime);
                param.AddParam("rental_isdel", model.IsDel);
                param.AddParam("admin_id", model.AdminId);
                param.AddParam("admin_name", model.AdminName);
                param.AddParam("rental_tags", model.Tags);
                param.AddParam("rental_weight", model.Weight);
                param.AddParam("rental_volume", model.Volume);
                param.AddParam("rental_brandid", model.BrandId);
                param.AddParam("rental_brandname", model.BrandName);
                param.AddParam("rental_categoryid", model.CategoryId);

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }

        /// <summary>
        /// 根据ID，直接删除数据。数据库删除，谨慎操作。
        /// </summary>
        /// <param name="id">类目id</param>
        /// <returns></returns>
        public bool DeleteFormDateBase(int id)
        {
            try
            {
                const string sql = @"DELETE FROM [dbo].[RentalGoodses]
                                 WHERE rental_id = @rental_id";

                var param = new SqlParam();
                param.AddParam("rental_id", id);


                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return false;
            }
        }

        /// <summary>
        /// 根据ID，删除数据(标识删除)。
        /// </summary>
        /// <param name="id">属性id</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                const string sql = @"UPDATE [dbo].[RentalGoodses]
                                   SET [rental_isdel] = 1
                                 WHERE rental_id = @rental_id";

                var param = new SqlParam();
                param.AddParam("rental_id", id);

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
               KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return false;
            }
        }

        /// <summary>
        /// 增加商品（租），
        /// 同时加商品扩展表,商品属性表，商品颜色，商品规格，goodsPrice
        /// </summary>
        /// <returns></returns>
        public bool AddRentalGoods(RentalGoodsEntity model)
        {
            try
            {
                if (model == null) return false;

                //using (TransactionScope ts = new TransactionScope())
                //{
                int sid = Add(model.RentalGoods, true);//出租表加记录


                if (model.GoodsAttribute != null && model.GoodsAttribute.Count > 0)
                {
                    foreach (var item in model.GoodsAttribute)
                    {
                        item.GoodsId = sid;
                        item.Id = new GoodsAttributes.GoodsAttributeDal().Add(item, true);
                    }
                    model.RentalOther.Attrs = model.GoodsAttribute.ToJson().Compress();
                }

                Dictionary<int, int> dicColor = new Dictionary<int, int>(), dicNorm = new Dictionary<int, int>();
                if (model.GoodsColor != null && model.GoodsColor.Count > 0)
                {
                    foreach (var item in model.GoodsColor)
                    {
                        item.GoodsId = sid;
                        item.Id = new GoodsColors.GoodsColorDal().Add(item, true);
                        dicColor.Add(item.ColorId, item.Id);
                    }
                    model.RentalOther.Colors = model.GoodsColor.ToJson().Compress();
                }

                if (model.GoodsNorm != null && model.GoodsNorm.Count > 0)
                {
                    foreach (var item in model.GoodsNorm)
                    {
                        item.GoodsId = sid;
                        item.Id = new GoodsNorms.GoodsNormDal().Add(item, true);
                        dicNorm.Add(item.NormId, item.Id);
                    }
                    model.RentalOther.Norms = model.GoodsNorm.ToJson().Compress();
                }

                if (model.GoodsPrice != null && model.GoodsPrice.Count > 0)
                {
                    foreach (var item in model.GoodsPrice)
                    {
                        item.GoodsId = sid;
                        item.ColorId = dicColor.First(o => o.Key == item.ColorId).Value;
                        item.NormId = dicNorm.First(o => o.Key == item.NormId).Value;
                        item.Id = new GoodsPrices.GoodsPriceDal().Add(item, true);
                    }
                    model.RentalOther.Prices = model.GoodsPrice.ToJson().Compress();
                }

                model.RentalOther.RentalId = sid;
                new RentalOtherDal().Add(model.RentalOther);//出租扩展表加记录

                //ts.Complete();
                //}

                return true;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }

        /// <summary>
        /// 更新商品(租)，
        /// 同时更新商品扩展表,商品属性表，商品颜色，商品规格，goodsPrice
        /// </summary>
        /// <returns></returns>
        public bool UpdateRentalGoods(RentalGoodsEntity model)
        {
            try
            {
                if (model == null) return false;

                //using (TransactionScope ts = new TransactionScope())
                //{
                Update(model.RentalGoods);//出租表修改记录
                var sid = model.RentalGoods.Id;

                var attrDal = new GoodsAttributes.GoodsAttributeDal();
                attrDal.DeleteFormDateBase(sid);

                if (model.GoodsAttribute != null && model.GoodsAttribute.Count > 0)
                {
                    foreach (var item in model.GoodsAttribute)
                    {
                        item.GoodsId = sid;
                        item.Id = attrDal.Add(item, true);
                    }
                    model.RentalOther.Attrs = model.GoodsAttribute.ToJson().Compress();
                }

                Dictionary<int, int> dicColor = new Dictionary<int, int>(), dicNorm = new Dictionary<int, int>();

                var colorDal = new GoodsColors.GoodsColorDal();
                colorDal.DeleteFormDateBase(sid);

                if (model.GoodsColor != null && model.GoodsColor.Count > 0)
                {
                    foreach (var item in model.GoodsColor)
                    {
                        int id = item.Id;
                        item.GoodsId = sid;
                        item.Id = colorDal.Add(item, true);
                        dicColor.Add(id, item.Id);
                    }
                    model.RentalOther.Colors = model.GoodsColor.ToJson().Compress();
                }

                var normDal = new GoodsNorms.GoodsNormDal();
                normDal.DeleteFormDateBase(sid);

                if (model.GoodsNorm != null && model.GoodsNorm.Count > 0)
                {
                    foreach (var item in model.GoodsNorm)
                    {
                        int id = item.Id;
                        item.GoodsId = sid;
                        item.Id = normDal.Add(item, true);
                        dicNorm.Add(id, item.Id);
                    }
                    model.RentalOther.Norms = model.GoodsNorm.ToJson().Compress();
                }

                var priceDal = new GoodsPrices.GoodsPriceDal();
                priceDal.DeleteFormDateBase(sid);

                if (model.GoodsPrice != null && model.GoodsPrice.Count > 0)
                {
                    foreach (var item in model.GoodsPrice)
                    {
                        item.GoodsId = sid;
                        item.ColorId = dicColor.First(o => o.Key == item.ColorId).Value;
                        item.NormId = dicNorm.First(o => o.Key == item.NormId).Value;
                        item.Id = priceDal.Add(item, true);
                    }
                    model.RentalOther.Prices = model.GoodsPrice.ToJson().Compress();
                }

                new RentalOtherDal().Update(model.RentalOther);//出租扩展表修改记录

                //ts.Complete();
                //}

                return true;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }

        /// <summary>
        /// 作者:maq
        /// 时间：2014年4月25日09:49:19
        /// 描述：商品列表
        /// </summary>
        /// <param name="pms">参数</param>
        /// <returns></returns>
        public PageData<WareHouseGoodsEntity> GetList(int merchantId, PagePms pms, RentalGoodsesQueryPms queryPms)
        {
            #region 参数
            PagePmsDal pagePmsDal = new PagePmsDal();
            pagePmsDal.PageIndex = (int)queryPms.PageIndex;
            pagePmsDal.PageSize = pms.PageSize;

            pagePmsDal.SortColnum = pms.SortColnum;
            pagePmsDal.TableList = " RentalGoodses with(nolock) ";
            pagePmsDal.ColList = @"[rental_id] as Id,[rental_title] as Title,[rental_number] as Stock,[rental_status] as SetStatus,[rental_rentnum] as RentNum,[rental_salesvolume] as SalesVolume,[rental_monthprice] as MonthPrice,[rental_guid] as Guid,[rental_begintime] as CreateTime";
            #endregion

            #region 拼接查询参数
            pagePmsDal.StrWhere = " rental_isdel =0 and rental_state=2  ";
            pagePmsDal.StrWhere += " and merchant_id = '" + merchantId + "'";
            pagePmsDal.StrWhere += " and rental_rentnum between " + queryPms.RentalNumMin + " and " + queryPms.RentalNumMax;
            pagePmsDal.StrWhere += " and rental_monthprice between " + queryPms.MonthPriceMin + " and " + queryPms.MonthPriceMax;
            if (queryPms.MerchantNumber.Length > 0)
            {
                pagePmsDal.StrWhere += " and rental_code like '%" + queryPms.MerchantNumber + "%'";
            }
            if (queryPms.GoodsName.Length > 0)
            {
                pagePmsDal.StrWhere += " and rental_title like '%" + queryPms.GoodsName + "%'";
            }
            pagePmsDal.StrWhere += " ";
            #endregion
            return KYJ_ZushouRDB.GetPages<WareHouseGoodsEntity>(pagePmsDal);
        }
        /// <summary>
        /// 作者：maq
        /// 时间：2014年4月25日14:13:51
        /// 描述：商品下架
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int ShelfOff(int id)
        {
            string strSql = @" update RentalGoodses set rental_status=2 where rental_id =@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", id);
            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam);
        }

        public int ShelfOff(int[] arrId)
        {
            string strSql = @"update RentalGoodses set rental_status=2 where rental_id in(" + BuildeSql(arrId) + ")";
            return KYJ_ZushouWDB.SqlExecute(strSql, BuilderParam(arrId));
        }

        #region 私有方法 构建sql语句和参数
        /// <summary>
        /// 构建sql语句
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        private string BuildeSql(int[] idList)
        {
            string strSql = "";
            for (int i = 0; i < idList.Length; i++)
            {
                strSql += "@id" + i.ToString() + ",";
            }
            strSql = strSql.Substring(0, strSql.Length - 1);
            return strSql;
        }
        /// <summary>
        /// 构建参数
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        private SqlParam BuilderParam(int[] idList)
        {
            SqlParam sqlParam = new SqlParam();
            for (int i = 0; i < idList.Length; i++)
            {
                sqlParam.AddParam("@id" + i.ToString(), idList[i]);
            }
            return sqlParam;
        }
        #endregion

        /// <summary>
        /// 作者：maq
        /// 时间：2014年4月25日14:14:34
        /// 描述：商品上架
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int Shelf(int id)
        {
            string strSql = @" update RentalGoodses set rental_status=1 where rental_id =@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", id);
            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam);
        }
      

        /// <summary>
        /// Author：ningjd
        /// Time：2014-04-29
        /// Desc：判断运费模板是否被商品使用
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public bool isUsing(int freightTemplateID)
        {
            try
            {
                var sql = @"SELECT  COUNT(1)
                        FROM    [dbo].[RentalGoodses]
                        WHERE   ftemp_id=@ftemp_id";
                var param = new SqlParam();
                param.AddParam("@ftemp_id", freightTemplateID);

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param)) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", freightTemplateID, ex);
                return true;
            }
        }
        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月6日14:50:28
        /// 描述：获取各个状态的订单数量
        /// </summary>
        /// <param name="merId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetRentalGoodsStateNum(int merId)
        {
            string strSql = "SELECT COUNT(1) AS num,rental_status  FROM dbo.RentalGoodses with(nolock) where merchant_id=@id GROUP BY  rental_status  ";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", merId);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql, sqlParam))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        dic.Add(Convert.ToInt32(row["rental_status"]), Convert.ToInt32(row["num"]));
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月6日15:05:07
        /// 描述：获取违规商品
        /// </summary>
        /// <param name="merId"></param>
        /// <returns></returns>
        public PageData<RentalGoodsIllegalGoodsEntity> GetIiiegalGoods(int merchantId, PagePms pms, RentalGoodsesQueryPms queryPms)
        {
            #region 参数
            PagePmsDal pagePmsDal = new PagePmsDal();
            pagePmsDal.PageIndex = (int)queryPms.PageIndex;
            pagePmsDal.PageSize = pms.PageSize;
            pagePmsDal.SortColnum = pms.SortColnum;
            pagePmsDal.TableList = " RentalGoodses with(nolock)";
            pagePmsDal.ColList = @"[rental_id] as Id,[rental_title] as Title,[rental_state] as State,[rental_monthprice] as RentMoney,[rental_guid] as Guid,[rental_begintime] as CreateTime,rental_shelfreason as Reason,rental_shelftime as Time";
            #endregion
            #region 拼接查询参数
            pagePmsDal.StrWhere = " rental_isdel ='false' and rental_state in(1,3)";
            pagePmsDal.StrWhere += " and merchant_id = '" + merchantId + "'";
            pagePmsDal.StrWhere += " and rental_rentnum between " + queryPms.RentalNumMin + " and " + queryPms.RentalNumMax;
            pagePmsDal.StrWhere += " and rental_monthprice between " + queryPms.MonthPriceMin + " and " + queryPms.MonthPriceMax;
            if (queryPms.MerchantNumber.Length > 0)
            {
                pagePmsDal.StrWhere += " and rental_code like '%" + queryPms.MerchantNumber + "%' ";
            }
            if (queryPms.GoodsName.Length > 0)
            {
                pagePmsDal.StrWhere += " and rental_title like '%" + queryPms.GoodsName + "%'";
            }
            pagePmsDal.StrWhere += " ";
            #endregion
            return KYJ_ZushouRDB.GetPages<RentalGoodsIllegalGoodsEntity>(pagePmsDal);
        }

        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月19日13:28:57
        /// 描述：获取使用的租期模板数量
        /// </summary>
        /// <returns></returns>
        public string GetUsedTemplate(int templateId)
        {
            string strSql = "SELECT COUNT(1) FROM dbo.RentalGoodses whit(nolock) WHERE  ttemp_id=@id";
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@id", templateId);
            return KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
        }

        #region 商品管理（审核）-----ningjd

        /// <summary>
        /// Author:ningjd
        /// Date:2014-05-22
        /// Desc:商品管理
        /// </summary>
        /// <param name="areaEnum">商品显示区域</param>
        /// <param name="title">商品名称</param>
        /// <param name="firstCategoryId">一级类目</param>
        /// <param name="secondCategoryId">二级类目</param>
        /// <param name="thirdCategoryId">三级类目</param>
        /// <param name="merchantName">商户名称</param>
        /// <param name="code">商家编号</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<RentalGoodsIndexEntity> GetRentalGoodsList(RentalGoodsAreaEnum areaEnum, string title, int firstCategoryId, int secondCategoryId, int thirdCategoryId, string merchantName, string code, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                // 表名
                string tableName = "Categories(NOLOCK) t1 right join RentalGoodses(NOLOCK) t2 on t1.category_id=t2.rental_categoryid left join Merchants(NOLOCK) t3 on t2.merchant_id=t3.merchant_id";
                // 查询条件
                string where = " rental_isdel = 'false' and rental_status<>0 and t2.rental_state=" + (int)areaEnum;
                // 商品名称过滤
                if (!string.IsNullOrEmpty(title))
                    where += " and t2.rental_title like '%" + title.Trim() + "%'";
                #region 商品类别过滤
                if (thirdCategoryId > 0) //三级类目确定
                {
                    where += " and t2.rental_categoryid =" + thirdCategoryId;
                }
                else if (secondCategoryId > 0) //二级类目确定
                {
                    where += " and t2.rental_categoryid in(select category_id from Categories(NOLOCK) where category_pid=" + secondCategoryId + ")";
                }
                else if (firstCategoryId > 0) //一级类目确定
                {
                    where += " and t2.rental_categoryid in(select category_id from Categories(NOLOCK) where category_pid in(select category_id from Categories(NOLOCK) where category_pid=" + firstCategoryId + "))";
                }
                #endregion
                // 商户名称过滤
                if (!string.IsNullOrEmpty(merchantName))
                    where += " and t3.merchant_name like '%" + merchantName.Trim() + "%'";
                // 商家编号过滤
                if (!string.IsNullOrEmpty(code))
                    where += " and t2.rental_code like '%" + code.Trim() + "%'";
                // 排序
                string orderBy = " t2.rental_updatetime desc";
                // 列名
                string columnList = "t2.rental_id as Id,t2.rental_code as Code,t2.rental_title as Title,t1.category_name as CategoryName,t3.merchant_name as MerchantName,t2.rental_monthprice as MonthPrice,t2.rental_status as Status,t2.rental_shelfreason as ShelfReason";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<RentalGoodsIndexEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalPage = 0;
                totalRecord = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Author:ningjd
        /// Date:2014-05-21
        /// Desc:商品审核
        /// </summary>
        /// <param name="goodsId">商品ID</param>
        /// <param name="shelfReason">违规原因</param>
        /// <param name="isAudited">是否通过审核</param>
        /// <returns></returns>
        public int Audited(int goodsId, string shelfReason, bool isAudited)
        {
            try
            {
                var sql = "";
                var param = new SqlParam();
                if (isAudited)
                {
                    sql = @"DECLARE @isAdded int
                        SET     @isAdded=(
                                    SELECT COUNT(1) FROM(
                                    SELECT rental_begintime AS beginTime,rental_endtime AS endTime from [dbo].[RentalGoodses] WHERE rental_id=@rental_id)
                                    t WHERE @auditedTime>=t.beginTime AND @auditedTime<=t.endtime)
                        UPDATE  [dbo].[RentalGoodses]
                        SET     [rental_status] = CASE WHEN @isAdded>0 THEN 1 ELSE [rental_status] END
                                ,[rental_state] = @rental_state
                                ,[rental_shelfreason] = @rental_shelfreason
                                ,[rental_updatetime] = @auditedTime
                        WHERE   rental_id = @rental_id";
                }
                else
                {
                    sql = @"UPDATE  [dbo].[RentalGoodses]
                        SET     [rental_status] = 2
                                ,[rental_state] = @rental_state
                                ,[rental_shelfreason] = @rental_shelfreason
                                ,[rental_shelftime] = @auditedTime
                                ,[rental_updatetime] = @auditedTime
                        WHERE   rental_id = @rental_id";
                }
                param.AddParam("@auditedTime", DateTime.Now);
                param.AddParam("@rental_id", goodsId);
                param.AddParam("@rental_state", isAudited ? 2 : 3);
                param.AddParam("@rental_shelfreason", shelfReason);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", goodsId + "," + shelfReason + "," + isAudited, ex);
                return 0;
            }
        }

        #endregion
    }
}
