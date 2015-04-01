using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;
namespace KYJ.ZS.DAL.DeliveryAddresses
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-4-17
    /// Desc:操作数据库表DeliveryAddresses，用户后台添加、删除、修改、查询收货地址
    /// </summary>
    public class DeliveryAddressDal
    {
        #region 用户后台添加收货地址
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:用户后台添加收货地址
        /// </summary>
        /// <param name="address">收货地址实体</param>
        /// <returns></returns>
        public int AddDeliveryAddress(DeliveryAddress address)
        {
            #region SQL
            var sql = string.Empty;
            //判断用户当前添加的收货地址是否设置为默认收货地址
            if (address.IsDefault)
            {
                sql = @"BEGIN TRANSACTION
                            UPDATE [dbo].[DeliveryAddresses]
                            SET [delivery_isdefault] = 'false'
                            WHERE user_id=@user_id and delivery_isdefault='true'
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                            INSERT INTO [dbo].[DeliveryAddresses]
                           ([user_id]
                           ,[delivery_provinceid]
                           ,[delivery_provincename]
                           ,[delivery_cityid]
                           ,[delivery_cityname]
                           ,[delivery_districtid]
                           ,[delivery_districtname]
                           ,[delivery_address]
                           ,[delivery_code]
                           ,[delivery_realname]
                           ,[delivery_tel]
                           ,[delivery_restel]
                           ,[delivery_isdefault]
                           ,[delivery_createtime]
                           ,[delivery_updatetime])
                     VALUES
                           (@user_id
                           ,@delivery_provinceid
                           ,@delivery_provincename
                           ,@delivery_cityid
                           ,@delivery_cityname
                           ,@delivery_districtid
                           ,@delivery_districtname
                           ,@delivery_address
                           ,@delivery_code
                           ,@delivery_realname
                           ,@delivery_tel
                           ,@delivery_restel
                           ,@delivery_isdefault
                           ,@delivery_createtime
                           ,@delivery_updatetime) select @@identity
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                            COMMIT TRANSACTION";
            }
            else
            {
                sql = @"INSERT INTO [dbo].[DeliveryAddresses]
                           ([user_id]
                           ,[delivery_provinceid]
                           ,[delivery_provincename]
                           ,[delivery_cityid]
                           ,[delivery_cityname]
                           ,[delivery_districtid]
                           ,[delivery_districtname]
                           ,[delivery_address]
                           ,[delivery_code]
                           ,[delivery_realname]
                           ,[delivery_tel]
                           ,[delivery_restel]
                           ,[delivery_isdefault]
                           ,[delivery_createtime]
                           ,[delivery_updatetime])
                     VALUES
                           (@user_id
                           ,@delivery_provinceid
                           ,@delivery_provincename
                           ,@delivery_cityid
                           ,@delivery_cityname
                           ,@delivery_districtid
                           ,@delivery_districtname
                           ,@delivery_address
                           ,@delivery_code
                           ,@delivery_realname
                           ,@delivery_tel
                           ,@delivery_restel
                           ,@delivery_isdefault
                           ,@delivery_createtime
                           ,@delivery_updatetime) select @@identity";
            }
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", address.UserId);
            param.AddParam("@delivery_provinceid", address.ProvinceId);
            param.AddParam("@delivery_provincename", address.ProvinceName);
            param.AddParam("@delivery_cityid", address.CityId);
            param.AddParam("@delivery_cityname", address.CityName);
            param.AddParam("@delivery_districtid", address.DistrictId);
            param.AddParam("@delivery_districtname", address.DistrictName);
            param.AddParam("@delivery_address", address.Address);
            param.AddParam("@delivery_code", address.Code);
            param.AddParam("@delivery_realname", address.RealName);
            param.AddParam("@delivery_tel", address.Tel);
            param.AddParam("@delivery_restel", address.ResTel);
            param.AddParam("@delivery_isdefault", address.IsDefault);
            param.AddParam("@delivery_createtime", address.CreateTime);
            param.AddParam("@delivery_updatetime", address.UpdateTime);
            #endregion
            #region 执行
            try
            {
                return Auxiliary.ToInt32(KYJ_ZushouWDB.GetFirst(sql, param));
                // return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "用户后台添加收货地址", ex);
            }
            return 0;
            #endregion
        }
        #endregion
        #region 用户后台修改收货地址
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:用户后台修改收货地址
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int UpdateDeliveryAddress(DeliveryAddress address)
        {
            #region SQL
            var sql = string.Empty;
            //判断用户当前添加的收货地址是否设置为默认收货地址
            if (address.IsDefault)
            {
                sql = @"BEGIN TRANSACTION
                            UPDATE [dbo].[DeliveryAddresses]
                            SET [delivery_isdefault] = 'false'
                            WHERE user_id=@user_id and delivery_isdefault='true'
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                           UPDATE [dbo].[DeliveryAddresses]
                            SET [delivery_provinceid] = @delivery_provinceid
                          ,[delivery_provincename] = @delivery_provincename
                          ,[delivery_cityid] = @delivery_cityid
                          ,[delivery_cityname] = @delivery_cityname
                          ,[delivery_districtid] =@delivery_districtid
                          ,[delivery_districtname] = @delivery_districtname
                          ,[delivery_address] = @delivery_address
                          ,[delivery_code] = @delivery_code
                          ,[delivery_realname] = @delivery_realname
                          ,[delivery_tel] = @delivery_tel
                          ,[delivery_restel] = @delivery_restel
                          ,[delivery_isdefault] =@delivery_isdefault
                          ,[delivery_updatetime] = @delivery_updatetime
                            WHERE delivery_id=@delivery_id and user_id=@user_id
                            IF @@ERROR<>0
                            BEGIN
                                ROLLBACK TRANSACTION
                                SELECT  0
                            END
                            COMMIT TRANSACTION";
            }
            else
            {
                sql = @"UPDATE [dbo].[DeliveryAddresses]
                       SET [delivery_provinceid] = @delivery_provinceid
                          ,[delivery_provincename] = @delivery_provincename
                          ,[delivery_cityid] = @delivery_cityid
                          ,[delivery_cityname] = @delivery_cityname
                          ,[delivery_districtid] =@delivery_districtid
                          ,[delivery_districtname] = @delivery_districtname
                          ,[delivery_address] = @delivery_address
                          ,[delivery_code] = @delivery_code
                          ,[delivery_realname] = @delivery_realname
                          ,[delivery_tel] = @delivery_tel
                          ,[delivery_restel] = @delivery_restel
                          ,[delivery_isdefault] =@delivery_isdefault
                          ,[delivery_updatetime] = @delivery_updatetime
                     WHERE delivery_id=@delivery_id and user_id=@user_id";
            }
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", address.UserId);
            param.AddParam("@delivery_provinceid", address.ProvinceId);
            param.AddParam("@delivery_provincename", address.ProvinceName);
            param.AddParam("@delivery_cityid", address.CityId);
            param.AddParam("@delivery_cityname", address.CityName);
            param.AddParam("@delivery_districtid", address.DistrictId);
            param.AddParam("@delivery_districtname", address.DistrictName);
            param.AddParam("@delivery_address", address.Address);
            param.AddParam("@delivery_code", address.Code);
            param.AddParam("@delivery_realname", address.RealName);
            param.AddParam("@delivery_tel", address.Tel);
            param.AddParam("@delivery_restel", address.ResTel);
            param.AddParam("@delivery_isdefault", address.IsDefault);
            param.AddParam("@delivery_updatetime", address.UpdateTime);
            param.AddParam("@delivery_id", address.Id);
            #endregion
            #region 执行
            try
            {
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {

                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "用户后台修改收货地址", ex);
            }
            return 0;
            #endregion
        }
        #endregion
        #region 用户后台删除收货地址
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:用户后台删除收货地址
        /// </summary>
        /// <param name="deliveryAddressId">收货地址ID</param>
        /// <returns></returns>
        public int DelDeliveryAddress(int deliveryAddressId, int userId)
        {
            #region SQL
            //var sql = @"DELETE FROM [dbo].[DeliveryAddresses]
            //WHERE delivery_id=@delivery_id and [user_id]=@user_id";
            var sql = @"UPDATE [dbo].[DeliveryAddresses]
                      SET [delivery_isdel]='true' where delivery_id=@delivery_id and [user_id]=@user_id";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@delivery_id", deliveryAddressId);
            param.AddParam("@user_id", userId);
            #endregion
            #region 执行
            try
            {
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {

                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "用户后台删除收货地址", ex);
            }
            return 0;
            #endregion
        }
        #endregion
        #region 根据User_Id查找收货地址
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:根据User_Id查找收货地址
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns>收货地址集合</returns>
        public List<DeliveryAddress> DeliveryAddresses(int userId)
        {
            #region SQL
            var sql = @"SELECT [delivery_id]
                              ,[delivery_provincename]
                              ,[delivery_cityname]
                              ,[delivery_districtname]
                              ,[delivery_address]
                              ,[delivery_code]
                              ,[delivery_realname]
                              ,[delivery_tel]
                              ,[delivery_restel]
                              ,[delivery_isdefault]
                          FROM [dbo].[DeliveryAddresses] NOLOCK
                          WHERE user_id=@user_id and [delivery_isdel]='false' order by delivery_updatetime desc";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            #endregion
            #region 执行并返回值
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    List<DeliveryAddress> addressList = new List<DeliveryAddress>();
                    foreach (DataRow item in dt.Rows)
                    {
                        DeliveryAddress address = new DeliveryAddress();
                        address.Id = Auxiliary.ToInt32(item["delivery_id"]);
                        address.ProvinceName = item["delivery_provincename"].ToString();
                        address.CityName = item["delivery_cityname"].ToString();
                        address.DistrictName = item["delivery_districtname"].ToString();
                        address.Address = item["delivery_address"].ToString();
                        address.Code = item["delivery_code"].ToString();
                        address.RealName = item["delivery_realname"].ToString();
                        address.Tel = item["delivery_tel"].ToString();
                        address.IsDefault = Auxiliary.ToBoolen(item["delivery_isdefault"]);
                        addressList.Add(address);
                    }
                    return addressList;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "根据User_Id查找收货地址", ex);
            }
            return null;
            #endregion
        }
        #endregion
        #region 根据收货地址ID查找收货地址
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-24
        /// Desc:根据收货地址ID查找收货地址
        /// </summary>
        /// <param name="deliveryId">收货地址的ID</param>
        /// <returns></returns>
        public DeliveryAddress FindIdDeliveryAddresses(int deliveryId, int userId)
        {
            #region SQL
            var sql = @"SELECT [delivery_id]
                      ,[user_id]
                      ,[delivery_provinceid]
                      ,[delivery_provincename]
                      ,[delivery_cityid]
                      ,[delivery_cityname]
                      ,[delivery_districtid]
                      ,[delivery_districtname]
                      ,[delivery_address]
                      ,[delivery_code]
                      ,[delivery_realname]
                      ,[delivery_tel]
                      ,[delivery_restel]
                      ,[delivery_isdefault]
                  FROM [dbo].[DeliveryAddresses] NOLOCK WHERE [delivery_id]=@delivery_id and [user_id]=@user_id";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@delivery_id", deliveryId);
            param.AddParam("@user_id", userId);
            #endregion
            #region 执行并返回值
            try
            {
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (Auxiliary.CheckDt(dt))
                {
                    DeliveryAddress address = new DeliveryAddress();
                    var item = dt.Rows[0];
                    address.Id = Auxiliary.ToInt32(item["delivery_id"]);
                    address.UserId = Auxiliary.ToInt32(item["user_id"]);
                    address.ProvinceId = Auxiliary.ToInt32(item["delivery_provinceid"]);
                    address.ProvinceName = item["delivery_provincename"].ToString();
                    address.CityId = Auxiliary.ToInt32(item["delivery_cityid"]);
                    address.CityName = item["delivery_cityname"].ToString();
                    address.DistrictId = Auxiliary.ToInt32(item["delivery_districtid"]);
                    address.DistrictName = item["delivery_districtname"].ToString();
                    address.Address = item["delivery_address"].ToString();
                    address.Code = item["delivery_code"].ToString();
                    address.RealName = item["delivery_realname"].ToString();
                    address.Tel = item["delivery_tel"].ToString();
                    address.ResTel = item["delivery_restel"].ToString();
                    address.IsDefault = Auxiliary.ToBoolen(item["delivery_isdefault"]);
                    return address;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("baiyan", "根据收货地址ID查找收货地址", ex);
            }
            return null;
            #endregion
        }
        #endregion
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：订单页—收获地址列表
        /// </summary>
        /// <param name="user_id">用户Id</param>
        /// <returns></returns>
        public List<DeliveryAddress> Web_DeliVeryAddressList(int user_id)
        {
            try
            {
                #region sql
                var sql = @"
                        SELECT [delivery_id] as Id --收获地址id
                              ,[delivery_provinceid] as ProvinceId --省份Id
                              ,[delivery_provincename] as ProvinceName --省份名称
                              ,[delivery_cityid] as CityId  --城市Id
                              ,[delivery_cityname] as CityName --城市名称
                              ,[delivery_districtid] as DistrictId --区域Id
                              ,[delivery_districtname] as DistrictName --区域名称
                              ,[delivery_address] as Address --详细地址
                              ,[delivery_code] as Code --邮编
                              ,[delivery_realname] as RealName--收货人
                              ,[delivery_tel] as Tel   --电话
                              ,[delivery_restel] as ResTel --备用电话
                              ,[delivery_isdefault] as IsDefault --是否默认
                              ,[delivery_createtime] as CreateTime --创建时间
                              ,[delivery_updatetime] as UpdateTime --修改时间
                        FROM [dbo].[DeliveryAddresses] With(Nolock)
                        Where [user_id]=@user_id and [delivery_isdel]=0
                        Order by delivery_isdefault desc,delivery_id asc
                        ";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@user_id", user_id, SqlDbType.Int, 4);
                #endregion
                #region 执行
                var dt = KYJ_ZushouRDB.GetTable(sql, param);
                #endregion
                #region 返回
                if (!Auxiliary.CheckDt(dt))
                    return new List<DeliveryAddress>();

                return DataHelper<DeliveryAddress>.GetEntityList(dt);
                #endregion
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("dengfw", "订单页—收获地址列表", ex);
                return null;
            }

        }
    }
}
