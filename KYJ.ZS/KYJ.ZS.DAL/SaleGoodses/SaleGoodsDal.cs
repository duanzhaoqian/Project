using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.DAL.SaleGoodses
{
    using System.Transactions;

    using KYJ.ZS.Commons;
    using KYJ.ZS.Models.SaleGoodses;
    using KYJ.ZS.Commons.Index;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 15:12:26
    /// 描述：出售商品操作类
    /// </summary>
    public class SaleGoodsDal
    {
        /// <summary>
        /// 根据ID得到记录
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public SaleGoods Get(int id)
        {
            try
            {
                string sql = @"SELECT [sale_id] as Id
                          ,[user_id] as UserId
                          ,[sale_provinceid] as ProvinceId
                          ,[sale_provincename] as ProvinceName
                          ,[sale_cityid] as CityId
                          ,[sale_cityname] as CityName
                          ,[sale_districtid] as DistrictId
                          ,[sale_districtname] as DistrictName
                          ,[sale_title] as Title
                          ,[sale_degree] as Degree
                          ,[sale_price] as Price
                          ,[sale_guid] as Guid
                          ,[sale_status] as Status
                          ,[sale_state] as State
                          ,[sale_createtime] as CreateTime
                          ,[sale_updatetime] as UpdateTime
                          ,[sale_isdel] as IsDel
                          ,[sale_shelfreason] as ShelfReason
                          ,[admin_id] as AdminId
                          ,[admin_name] as AdminName
                            ,[sale_contact] as Contact
                            ,[sale_contactphone] as ContactPhone
                            ,[sale_isbargain] as IsBargain
                            ,[sale_tag] as Tag
                 FROM  [dbo].[SaleGoodses] NOLOCK
                 WHERE sale_id=@sale_id";

                var param = new SqlParam();
                param.AddParam("sale_id", id);

                var dt = KYJ_ZushouRDB.GetTable(sql, param);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<SaleGoods>.GetEntity(dt.Rows[0]);
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
        public int Add(SaleGoods model,bool isReturnId = false)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("SaleGoodsDal.Add,参数不可为空");

                string sql = @"INSERT INTO [dbo].[SaleGoodses]
                       ([user_id]
                      ,[sale_provinceid]
                      ,[sale_provincename]
                      ,[sale_cityid]
                      ,[sale_cityname]
                      ,[sale_districtid]
                      ,[sale_districtname]
                      ,[sale_title]
                      ,[sale_degree]
                      ,[sale_price]
                      ,[sale_guid]
                      ,[sale_status]
                      ,[sale_state]
                      ,[sale_createtime]
                      ,[sale_updatetime]
                      ,[sale_isdel]
                      ,[admin_id]
                      ,[admin_name]
                        ,[sale_contact]
                        ,[sale_contactphone]
                        ,[sale_isbargain]
                        ,[sale_tag])
                 VALUES
                     (@user_id
                      ,@sale_provinceid
                      ,@sale_provincename
                      ,@sale_cityid
                      ,@sale_cityname
                      ,@sale_districtid
                      ,@sale_districtname
                      ,@sale_title
                      ,@sale_degree
                      ,@sale_price
                      ,@sale_guid
                      ,@sale_status
                      ,@sale_state
                      ,@sale_createtime
                      ,@sale_updatetime
                      ,@sale_isdel
                      ,@admin_id
                      ,@admin_name
                        ,@sale_contact
                        ,@sale_contactphone
                        ,@sale_isbargain
                        ,@sale_tag)";

                var param = new SqlParam();
                param.AddParam("user_id", model.UserId);
                param.AddParam("sale_provinceid", model.ProvinceId);
                param.AddParam("sale_provincename", model.ProvinceName);
                param.AddParam("sale_cityid", model.CityId);
                param.AddParam("sale_cityname", model.CityName);
                param.AddParam("sale_districtid", model.DistrictId);
                param.AddParam("sale_districtname", model.DistrictName);
                param.AddParam("sale_title", model.Title);
                param.AddParam("sale_degree", model.Degree);
                param.AddParam("sale_price", model.Price);
                param.AddParam("sale_guid", model.Guid);
                param.AddParam("sale_status", model.Status);
                param.AddParam("sale_state", model.State);
                param.AddParam("sale_createtime", model.CreateTime);
                param.AddParam("sale_updatetime", model.UpdateTime);
                param.AddParam("sale_isdel", model.IsDel);
                param.AddParam("admin_id", model.AdminId);
                param.AddParam("admin_name", model.AdminName);
                param.AddParam("sale_contact", model.Contact);
                param.AddParam("sale_contactphone", model.ContactPhone);
                param.AddParam("sale_isbargain", model.IsBargain);
                param.AddParam("sale_tag", model.Tag);

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
        public SaleGoods AddReturnEntity(SaleGoods model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("SaleGoodsDal.AddReturnEntity,参数不可为空");

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
        public bool Update(SaleGoods model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException("SaleGoodsDal.Update,参数不可为空");
                const string sql = @"UPDATE [dbo].[SaleGoodses]
                                   SET [user_id] = @user_id
                                      ,[sale_provinceid] = @sale_provinceid
                                      ,[sale_provincename] = @sale_provincename
                                      ,[sale_cityid] = @sale_cityid
                                      ,[sale_cityname] = @sale_cityname
                                      ,[sale_districtid] = @sale_districtid
                                      ,[sale_districtname] = @sale_districtname
                                      ,[sale_title] = @sale_title
                                      ,[sale_degree] = @sale_degree
                                      ,[sale_price] = @sale_price
                                      ,[sale_guid] = @sale_guid
                                      ,[sale_status] = @sale_status
                                      ,[sale_state] = @sale_state
                                      ,[sale_createtime] = @sale_createtime
                                      ,[sale_updatetime] = @sale_updatetime
                                      ,[sale_isdel] = @sale_isdel
                                      ,[admin_id] = @admin_id
                                      ,[admin_name] = @admin_name
                                        ,[sale_contact] = @sale_contact
                                        ,[sale_contactphone] = @sale_contactphone
                                        ,[sale_isbargain] = @sale_isbargain
                                        ,[sale_tag] = @sale_tag
                                 WHERE sale_id = @sale_id";

                var param = new SqlParam();
                param.AddParam("sale_id", model.Id);
                param.AddParam("user_id", model.UserId);
                param.AddParam("sale_provinceid", model.ProvinceId);
                param.AddParam("sale_provincename", model.ProvinceName);
                param.AddParam("sale_cityid", model.CityId);
                param.AddParam("sale_cityname", model.CityName);
                param.AddParam("sale_districtid", model.DistrictId);
                param.AddParam("sale_districtname", model.DistrictName);
                param.AddParam("sale_title", model.Title);
                param.AddParam("sale_degree", model.Degree);
                param.AddParam("sale_price", model.Price);
                param.AddParam("sale_guid", model.Guid);
                param.AddParam("sale_status", model.Status);
                param.AddParam("sale_state", model.State);
                param.AddParam("sale_createtime", model.CreateTime);
                param.AddParam("sale_updatetime", model.UpdateTime);
                param.AddParam("sale_isdel", model.IsDel);
                param.AddParam("admin_id", model.AdminId);
                param.AddParam("admin_name", model.AdminName);
                param.AddParam("sale_contact", model.Contact);
                param.AddParam("sale_contactphone", model.ContactPhone);
                param.AddParam("sale_isbargain", model.IsBargain);
                param.AddParam("sale_tag", model.Tag);

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
                const string sql = @"DELETE FROM [dbo].[SaleGoodses]
                                 WHERE sale_id = @sale_id";

                var param = new SqlParam();
                param.AddParam("sale_id", id);

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
                const string sql = @"UPDATE [dbo].[SaleGoodses]
                                   SET [sale_isdel] = 1
                                 WHERE sale_id = @sale_id";

                var param = new SqlParam();
                param.AddParam("sale_id", id);

                return KYJ_ZushouWDB.SqlExecute(sql, param) > 0;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", id, ex);
                return false;
            }
        }

        /// <summary>
        /// 增加出售商品，同时加扩展表
        /// </summary>
        /// <returns></returns>
        public bool AddSaleGoods(SaleGoodsEntity model)
        {
            try
            {
                if (model == null) return false;

                #region .net事务
                //using (TransactionScope ts = new TransactionScope())
                //{
                //int sid = Add(model.SaleGoods, true);//出售表加记录
                //model.SaleOther.SaleId = sid;
                //model.SaleOther.Descs = model.SaleOther.Descs;

                //new SaleOtherDal().Add(model.SaleOther);//出售扩展表加记录
                //ts.Complete();
                //} 
                #endregion

                #region SQL
                var sql = @"begin tran
			            DECLARE @saleId int, --商品ID
                                @results int --执行结果，0失败，1成功

                        /*出售商品表*/
                        INSERT INTO [dbo].[SaleGoodses]
                       ([user_id]
                      ,[sale_provinceid]
                      ,[sale_provincename]
                      ,[sale_cityid]
                      ,[sale_cityname]
                      ,[sale_districtid]
                      ,[sale_districtname]
                      ,[sale_title]
                      ,[sale_degree]
                      ,[sale_price]
                      ,[sale_guid]
                      ,[sale_status]
                      ,[sale_state]
                      ,[sale_createtime]
                      ,[sale_updatetime]
                      ,[sale_isdel]
                      ,[admin_id]
                      ,[admin_name]
                        ,[sale_contact]
                        ,[sale_contactphone]
                        ,[sale_isbargain]
                        ,[sale_tag])
                 VALUES
                     (@user_id
                      ,@sale_provinceid
                      ,@sale_provincename
                      ,@sale_cityid
                      ,@sale_cityname
                      ,@sale_districtid
                      ,@sale_districtname
                      ,@sale_title
                      ,@sale_degree
                      ,@sale_price
                      ,@sale_guid
                      ,@sale_status
                      ,@sale_state
                      ,@sale_createtime
                      ,@sale_updatetime
                      ,@sale_isdel
                      ,@admin_id
                      ,@admin_name
                        ,@sale_contact
                        ,@sale_contactphone
                        ,@sale_isbargain
                        ,@sale_tag)
                        SELECT @saleId=SCOPE_IDENTITY()

                        /*出售商品扩展表*/
                        INSERT INTO [dbo].[SaleOthers]
                       ([sale_id]
                       ,[other_desc])
                 VALUES
                       (@saleId,
                        @other_desc)

                        /*判断事务回滚或提交*/
	                    if @@error<>0 --判断有任何一条出现错误
	                    begin 
		                    rollback tran --开始执行事务的回滚
		                    set @results=0  
	                    end
	                    else
	                    begin
		                    commit tran --执行这个事务的操作
		                    set @results=1 
	                    end
                        /*返回执行结果*/
                        select  @results";
                #endregion

                #region 参数

                var param = new SqlParam();
                param.AddParam("user_id", model.SaleGoods.UserId);
                param.AddParam("sale_provinceid", model.SaleGoods.ProvinceId);
                param.AddParam("sale_provincename", model.SaleGoods.ProvinceName);
                param.AddParam("sale_cityid", model.SaleGoods.CityId);
                param.AddParam("sale_cityname", model.SaleGoods.CityName);
                param.AddParam("sale_districtid", model.SaleGoods.DistrictId);
                param.AddParam("sale_districtname", model.SaleGoods.DistrictName);
                param.AddParam("sale_title", model.SaleGoods.Title);
                param.AddParam("sale_degree", model.SaleGoods.Degree);
                param.AddParam("sale_price", model.SaleGoods.Price);
                param.AddParam("sale_guid", model.SaleGoods.Guid);
                param.AddParam("sale_status", model.SaleGoods.Status);
                param.AddParam("sale_state", model.SaleGoods.State);
                param.AddParam("sale_createtime", model.SaleGoods.CreateTime);
                param.AddParam("sale_updatetime", model.SaleGoods.UpdateTime);
                param.AddParam("sale_isdel", model.SaleGoods.IsDel);
                param.AddParam("admin_id", model.SaleGoods.AdminId);
                param.AddParam("admin_name", model.SaleGoods.AdminName);
                param.AddParam("sale_contact", model.SaleGoods.Contact);
                param.AddParam("sale_contactphone", model.SaleGoods.ContactPhone);
                param.AddParam("sale_isbargain", model.SaleGoods.IsBargain);
                param.AddParam("sale_tag", model.SaleGoods.Tag);

                param.AddParam("other_desc", model.SaleOther.Descs);
                #endregion

                #region 执行

                return KYJ_ZushouWDB.GetFirst(sql, param).ToInt() > 0;

                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }

        /// <summary>
        /// 更新出售商品，同时更新扩展表
        /// </summary>
        /// <returns></returns>
        public bool UpdateSaleGoods(SaleGoodsEntity model)
        {
            try
            {
                if (model == null) return false;

                #region .net事务
                //using (TransactionScope ts = new TransactionScope())
                //{
                //Update(model.SaleGoods);//出售表修改记录

                //model.SaleOther.Descs = model.SaleOther.Descs;

                //new SaleOtherDal().Update(model.SaleOther);//出售扩展表修改记录
                //ts.Complete();
                //} 
                #endregion

                #region SQL
                var sql = @"begin tran
			            DECLARE @results int --执行结果，0失败，1成功

                        /*出售商品表*/
                        UPDATE [dbo].[SaleGoodses]
                                   SET [user_id] = @user_id
                                      ,[sale_provinceid] = @sale_provinceid
                                      ,[sale_provincename] = @sale_provincename
                                      ,[sale_cityid] = @sale_cityid
                                      ,[sale_cityname] = @sale_cityname
                                      ,[sale_districtid] = @sale_districtid
                                      ,[sale_districtname] = @sale_districtname
                                      ,[sale_title] = @sale_title
                                      ,[sale_degree] = @sale_degree
                                      ,[sale_price] = @sale_price
                                      ,[sale_guid] = @sale_guid
                                      ,[sale_status] = @sale_status
                                      ,[sale_state] = @sale_state
                                      ,[sale_createtime] = @sale_createtime
                                      ,[sale_updatetime] = @sale_updatetime
                                      ,[sale_isdel] = @sale_isdel
                                      ,[admin_id] = @admin_id
                                      ,[admin_name] = @admin_name
                                        ,[sale_contact] = @sale_contact
                                        ,[sale_contactphone] = @sale_contactphone
                                        ,[sale_isbargain] = @sale_isbargain
                                        ,[sale_tag] = @sale_tag
                                 WHERE sale_id = @sale_id

                        /*出售商品扩展表*/
                        UPDATE [dbo].[SaleOthers]
                                   SET [other_desc] = @other_desc
                                 WHERE sale_id = @sale_id

                        /*判断事务回滚或提交*/
	                    if @@error<>0 --判断有任何一条出现错误
	                    begin 
		                    rollback tran --开始执行事务的回滚
		                    set @results=0  
	                    end
	                    else
	                    begin
		                    commit tran --执行这个事务的操作
		                    set @results=1 
	                    end
                        /*返回执行结果*/
                        select  @results";
                #endregion

                #region 参数

                var param = new SqlParam();
                param.AddParam("sale_id", model.SaleGoods.Id);
                param.AddParam("user_id", model.SaleGoods.UserId);
                param.AddParam("sale_provinceid", model.SaleGoods.ProvinceId);
                param.AddParam("sale_provincename", model.SaleGoods.ProvinceName);
                param.AddParam("sale_cityid", model.SaleGoods.CityId);
                param.AddParam("sale_cityname", model.SaleGoods.CityName);
                param.AddParam("sale_districtid", model.SaleGoods.DistrictId);
                param.AddParam("sale_districtname", model.SaleGoods.DistrictName);
                param.AddParam("sale_title", model.SaleGoods.Title);
                param.AddParam("sale_degree", model.SaleGoods.Degree);
                param.AddParam("sale_price", model.SaleGoods.Price);
                param.AddParam("sale_guid", model.SaleGoods.Guid);
                param.AddParam("sale_status", model.SaleGoods.Status);
                param.AddParam("sale_state", model.SaleGoods.State);
                param.AddParam("sale_createtime", model.SaleGoods.CreateTime);
                param.AddParam("sale_updatetime", model.SaleGoods.UpdateTime);
                param.AddParam("sale_isdel", model.SaleGoods.IsDel);
                param.AddParam("admin_id", model.SaleGoods.AdminId);
                param.AddParam("admin_name", model.SaleGoods.AdminName);
                param.AddParam("sale_contact", model.SaleGoods.Contact);
                param.AddParam("sale_contactphone", model.SaleGoods.ContactPhone);
                param.AddParam("sale_isbargain", model.SaleGoods.IsBargain);
                param.AddParam("sale_tag", model.SaleGoods.Tag);

                param.AddParam("other_desc", model.SaleOther.Descs);
                #endregion

                #region 执行

                return KYJ_ZushouWDB.GetFirst(sql, param).ToInt() > 0;

                #endregion
                
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", model, ex);
                return false;
            }
        }
        #region 邓福伟
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-30
        /// 描述：Web前端售商品详情页,售商品详情Model
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_SaleGoodsEntity Web_GetSaleGoodsEntity(int sale_id)
        {
            try
            {
                #region sql
                var sql = @"
                        SELECT ag.[sale_id] as Id --售商品Id
                              ,ag.[sale_guid]  as SaleGuid --售商品Guid
                              ,ag.[sale_cityname]+'-'+ag.[sale_districtname] as Location  --商品所在地
                              ,ag.[sale_title]  as Title --商品标题
                              ,ag.[sale_degree] as Degree --成色
                              ,ag.[sale_price]  as Price --价格
                              ,ag.[sale_createtime] as CreateTime --创建时间
                              ,ag.[sale_updatetime] as UpdateTime --修改时间
                              ,ag.[sale_contact]  as Contact --联系人
                              ,ag.[sale_contactphone] as ContactPhone --联系人电话
                              ,ag.[sale_isbargain]  as IsBargain  --是否还价

                              ,ag.[user_id] as UserId --用户Id
                              ,lu.[user_guid] as UserGuid --用户Guid
                              ,lu.[user_nickname] as NickName  --用户昵称
                              ,luo.[other_papersstatus] as PapersStatus --认证状态

                              ,so.other_desc as OtherDesc --商品描述
                        FROM [dbo].[SaleGoodses] ag with(nolock)
                        left join dbo.LocalUsers lu with(nolock) on ag.[user_id]=lu.[user_id]
                        left join dbo.LocalUserOthers luo with(nolock) on luo.[user_id]=lu.[user_id]
                        left join dbo.SaleOthers so with(nolock) on ag.sale_id=so.sale_id
                        where ag.sale_id=@sale_id and ag.sale_isdel=0 and sale_status=1 and sale_state=2";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@sale_id", sale_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_SaleGoodsEntity>.GetEntity(dt.Rows[0]);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "售商品详情Model", ex);
                return null;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-30
        /// 描述：Web前端售商品详情页,售商品详情Model预览页
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_SaleGoodsEntity Web_GetSaleGoodsPreviewEntity(int sale_id)
        {
            try
            {
                #region sql
                var sql = @"
                        SELECT ag.[sale_id] as Id --售商品Id
                              ,ag.[sale_guid]  as SaleGuid --售商品Guid
                              ,ag.[sale_cityname]+'-'+ag.[sale_districtname] as Location  --商品所在地
                              ,ag.[sale_title]  as Title --商品标题
                              ,ag.[sale_degree] as Degree --成色
                              ,ag.[sale_price]  as Price --价格
                              ,ag.[sale_createtime] as CreateTime --创建时间
                              ,ag.[sale_updatetime] as UpdateTime --修改时间
                              ,ag.[sale_contact]  as Contact --联系人
                              ,ag.[sale_contactphone] as ContactPhone --联系人电话
                              ,ag.[sale_isbargain]  as IsBargain  --是否还价

                              ,ag.[user_id] as UserId --用户Id
                              ,lu.[user_guid] as UserGuid --用户Guid
                              ,lu.[user_nickname] as NickName  --用户昵称
                              ,luo.[other_papersstatus] as PapersStatus --认证状态

                              ,so.other_desc as OtherDesc --商品描述
                        FROM [dbo].[SaleGoodses] ag with(nolock)
                        left join dbo.LocalUsers lu with(nolock) on ag.[user_id]=lu.[user_id]
                        left join dbo.LocalUserOthers luo with(nolock) on luo.[user_id]=lu.[user_id]
                        left join dbo.SaleOthers so with(nolock) on ag.sale_id=so.sale_id
                        where ag.sale_id=@sale_id ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@sale_id", sale_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_SaleGoodsEntity>.GetEntity(dt.Rows[0]);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "售商品详情Model预览页", ex);
                return null;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-17
        /// 描述：返回用户的商品数
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int Web_GetSaleGoodsCount(int userId)
        {
            try
            {
                #region sql
                var sql = @"select count(1) as num from dbo.SaleGoodses 
                                 where [user_id]=@user_id and 
                                       sale_isdel=0 and 
                                       sale_status=1 and 
                                       sale_state=2";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userId);
                #endregion
                #region 返回
                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "用户的商品数", ex);
                return -0;
            }
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-21
        /// 描述：返回用户的商品列表
        /// </summary>
        /// <param name="sale_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Web_SaleGoodsEntity> Web_GetSaleGoodsListByUserId(int sale_id, int user_id)
        {
            try
            {
                #region sql
                var sql = @"select top 10 [sale_id] as Id --售商品Id
                              ,[sale_guid]  as SaleGuid --售商品Guid
                              ,[sale_title]  as Title --商品标题
                              ,[sale_price]  as Price --价格
                        from [dbo].[SaleGoodses] ag with(nolock)
                        where [user_id]=@user_id and sale_id<>@sale_id and sale_isdel=0 and sale_status=1 and sale_state=2
                        order by sale_updatetime desc";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@sale_id", sale_id, SqlDbType.Int, 4);
                param.AddParam("@user_id", user_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return null;

                return DataHelper<Web_SaleGoodsEntity>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "用户的商品列表", ex);
                return null;
            }

        }
        #endregion
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-6
        /// 描述：通过搜索索引从Lucene获取二手商品集合
        /// </summary>
        /// <param name="conditions">条件索引</param>
        /// <param name="getdatapagename"></param>
        /// <returns></returns>
        public List<Web_SaleGoodsEntity> GetSaleGoodsList(string conditions, string getdatapagename,out int totalcount)
        {
            SaleGoodsConditionInfo info = SaleGoodsCondition.GetSearchCondiction(conditions);

            return GetList(info, out totalcount);
        }

        public List<Web_SaleGoodsEntity> GetList(SaleGoodsConditionInfo info, out int totalcount)
        {
            try
            {
                #region 参数
                PagePmsDal pagePmsDal = new PagePmsDal();
                int index = Convert.ToInt32(info.PageIndex);
                int pagesize = 10;
                string where = " ag.sale_isdel=0 and ag.sale_status=1 and  ag.sale_state=2";
                string orderby = " sale_updatetime DESC";
                string tablelist = @"[dbo].[SaleGoodses] ag with(nolock) left join dbo.LocalUsers lu with(nolock) on ag.[user_id]=lu.[user_id] left join dbo.SaleOthers so with(nolock) on ag.sale_id=so.sale_id";
                string collist = @"ag.[sale_id] as Id ,ag.[sale_guid]  as SaleGuid,[sale_cityname]+'-'+[sale_districtname] as Location,ag.[sale_title]  as Title,ag.[sale_degree] as Degree,ag.[sale_price]  as Price,ag.[sale_createtime] as CreateTime,ag.[sale_updatetime] as UpdateTime,ag.[sale_contact]  as Contact,ag.[sale_contactphone] as ContactPhone,ag.[sale_isbargain]  as IsBargain,ag.[user_id] as UserId,lu.[user_guid] as UserGuid,lu.[user_nickname] as NickName,so.other_desc as OtherDesc";
                int totalpage = 0;
                #endregion

                #region 拼接查询参数

                if (info.Isbargain)
                    where += " and ag.[sale_isbargain] ='0'";
                if (info.IsNew)
                    where += " and ag.[sale_degree]='10'";
                if (!string.IsNullOrEmpty(info.TagId))
                    where += " and charindex('," + info.TagId + ",',','+ag.[sale_tag]+',')>0";
                switch (info.Sort)
                {
                    case "0":
                        orderby = " sale_updatetime DESC";
                        break;
                    case "1":
                        orderby = " ag.[sale_price]";
                        break;
                    case "2":
                        orderby = " ag.[sale_price] DESC";
                        break;
                }
                #endregion
                var dt = KYJ_ZushouRDB.GetPages(index, where, orderby, collist, tablelist, pagesize, true, out totalcount, out totalpage);

                if (!Auxiliary.CheckDt(dt))
                    return new List<Web_SaleGoodsEntity>();

                return DataHelper<Web_SaleGoodsEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalcount = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException<SaleGoodsConditionInfo>("wwang", info, ex);
                return null;
            }
        }

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-29
        /// 描述：获取闲置物品总条数
        /// </summary>
        /// <returns></returns>
        public int GetSaleGoodsTotalCount()
        {
            try
            {
                var sql = @"select count(1) from SaleGoodses where sale_isdel=0 and sale_status=1 and  sale_state=2";

                return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql));
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wwang", ex.Message, ex);
                return 0;
            }
 
        }
        /// <summary>
        /// Author:baiyan
        /// Date:2014-05-14
        /// Desc:用户后台首页已发布信息个数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetSaleGoodsCountByUserId(int userId)
        {
            var sql = @"select count(1) as num from dbo.SaleGoodses where [user_id]=@user_id and sale_isdel='false'";
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
        }

        #region 商品管理（审核）-----ningjd

        /// <summary>
        /// Author:ningjd
        /// Date:2014-05-22
        /// Desc:商品管理
        /// </summary>
        /// <param name="areaEnum">商品所在区域Enum</param>
        /// <param name="userLoginName">用户账户</param>
        /// <param name="title">信息名称</param>
        /// <param name="number">信息编号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<SaleGoodsIndexEntity> GetManageList(SaleGoodsAreaEnum areaEnum, string userLoginName, string title, string number, DateTime? beginTime, DateTime? endTime, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            try
            {
                // 表名
                string tableName = "SaleOthers(NOLOCK) t1 right join SaleGoodses(NOLOCK) t2 on t1.sale_id=t2.sale_id left join LocalUsers(NOLOCK) t3 on t2.user_id=t3.user_id";
                // 查询条件
                string where = " sale_isdel = 'false' and sale_status<>0 and t2.sale_state=" + (int)areaEnum;
                // 用户账户过滤
                if (!string.IsNullOrEmpty(userLoginName))
                    where += " and t3.user_loginname like '%" + userLoginName.Trim() + "%'";
                // 商品名称过滤
                if (!string.IsNullOrEmpty(title))
                    where += " and t2.sale_title like '%" + title.Trim() + "%'";
                // 信息编号过滤
                if (!string.IsNullOrEmpty(number))
                    where += " and t2.sale_id like '%" + number.Trim() + "%'";
                // 发布时间过滤
                if (beginTime.HasValue)
                    where += " and t2.sale_createtime>='" + beginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "'";
                if (endTime.HasValue)
                    where += " and t2.sale_createtime<='" + endTime.Value.ToString("yyyy-MM-dd 23:59:59") + "'";

                // 排序
                string orderBy = " t2.sale_updatetime desc";
                // 列名
                string columnList = "t2.sale_id as Id,t3.user_loginname as UserLoginName,t2.sale_title as Title,t2.sale_price as Price,t2.sale_createtime as CreateTime,t2.sale_shelfreason as ShelfReason";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;

                IList<SaleGoodsIndexEntity> result = new List<SaleGoodsIndexEntity>();
                return DataHelper<SaleGoodsIndexEntity>.GetEntityList(dt);
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
        public int Audited(int goodsId, string shelfReason,bool isAudited)
        {
            try
            {
                var sql = @"UPDATE  [dbo].[SaleGoodses]
                        SET     [sale_status] = @sale_status
                                ,[sale_state] = @sale_state
                                ,[sale_shelfreason] = @sale_shelfreason
                                ,[sale_updatetime] = @updateTime"
                            + (isAudited ? "" : ",[sale_shelftime] = @updateTime")
                            + " WHERE   sale_id = @sale_id";
                var param = new SqlParam();
                param.AddParam("@sale_id", goodsId);
                param.AddParam("@sale_status", isAudited ? 1 : 2);
                param.AddParam("@sale_state", isAudited ? 2 : 3);
                param.AddParam("@sale_shelfreason", shelfReason);
                param.AddParam("@updateTime", DateTime.Now);

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", goodsId + "," + shelfReason + "," + isAudited, ex);
                return 0;
            }
        }

        #endregion

        #region cheny 20140618

        #region 根据用户Id获取用户的认证状态 +int GetPapersstatusByUserId(int userId)
        /// <summary>
        /// 根据用户Id获取用户的认证状态
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns>返回 认证状态值</returns>
        public int GetPapersstatusByUserId(int userId)
        {
            var sql = @"SELECT other_papersstatus from LocalUserOthers where [user_id]=@user_id";
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            return Auxiliary.ToInt32(KYJ_ZushouRDB.GetFirst(sql, param));
        } 
        #endregion
            
        #endregion
    }
}
