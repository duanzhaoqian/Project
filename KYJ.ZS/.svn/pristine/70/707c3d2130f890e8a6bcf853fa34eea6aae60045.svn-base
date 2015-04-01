#define isUse
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Adverts;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;

namespace KYJ.ZS.DAL.Adverts
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-28
    /// 描述：广告排序
    /// </summary>
    public class AdvertManageDal
    {
        /// <summary>
        /// 根据分页条件获取当前页的数据
        /// </summary>
        /// <param name="pms">分页属性对象</param>
        /// <returns></returns>
        public PageData<Advert> GetPageData(PagePmsDal pmsdal, AdvertsSearchEntity searchEntity)
        {
            pmsdal.TableList = "Adverts";
            pmsdal.ColList = "advert_id as AdvertId,advert_aduitremark as AduitRemark,adverttype_id as AdvertTypeId,advertlocation_id as AdvertLocationId,category_id as Category,advert_name as Name,advert_remark as Remark,advert_begintime as BeginTime,advert_endtime as EndTime,advert_createtime as CreateTime,advert_state as State,advert_aduitstate as AduitState,advert_applytime as ApplyTime,advert_aduittime as AduitTime,advert_updatetime as UpdateTime,advert_isdel as Isdel,advert_adminid as AdminId,advert_adminname as AdminName";
            pmsdal.StrWhere = "advert_isdel =0 and advert_state<>0 and advert_aduitstate<>0";
            #region 拼接搜索条件
            if (!string.IsNullOrEmpty(searchEntity.NameSearch))
            {
                pmsdal.StrWhere += " and advert_name like '%" + searchEntity.NameSearch + "%'";
            }
            else if (searchEntity.BeginTime != null && searchEntity.EndTime == null)
            {
                pmsdal.StrWhere += " and advert_endtime>='" + searchEntity.BeginTime + "'";
            }
            else if (searchEntity.BeginTime == null && searchEntity.EndTime != null)
            {
                pmsdal.StrWhere += " and advert_begintime<='" + searchEntity.EndTime + "'";
            }
            else
                if (searchEntity.BeginTime != null && searchEntity.EndTime != null)
                {
                    pmsdal.StrWhere += " and advert_endtime>='" + searchEntity.BeginTime + "' and advert_begintime<='" + searchEntity.EndTime + "'";
                }
            switch (searchEntity.State)
            {
                case 1: pmsdal.StrWhere += " and advert_aduitstate = 1";
                    pmsdal.SortColnum = "advert_createtime desc";
                    break;
                case 2: pmsdal.SortColnum = "advert_applytime desc";
                    pmsdal.StrWhere += " and (advert_aduitstate=2 or advert_aduitstate=3 or advert_aduitstate=4) ";
                    break;
                case 3: pmsdal.StrWhere += " and advert_aduitstate = 5 and advert_begintime>'" + DateTime.Now + "'";
                    pmsdal.SortColnum = "advert_aduittime desc";
                    break;
                case 4: pmsdal.StrWhere += " and advert_aduitstate = 5 and advert_begintime<'" + DateTime.Now + "' and advert_endtime >'" + DateTime.Now + "' and advert_state = 1";
                    pmsdal.SortColnum = "advert_begintime desc";
                    break;
                case 5: pmsdal.StrWhere += " and advert_aduitstate = 5 and advert_endtime<'" + DateTime.Now + "' or (advert_state = 2 and advert_isdel =0 and advert_state<>0 and advert_aduitstate<>0)";
                    pmsdal.SortColnum = "advert_name";
                    break;
                default:
                    break;
            }
            #endregion
            return KYJ_ZushouRDB.GetPages<Advert>(pmsdal);

        }
        /// <summary>
        /// 提交核审
        /// </summary>
        /// <param name="AdvertId">排序ID</param>
        /// <returns></returns>
        public int ApplyAduit(Advert advert)
        {
            try
            {
                #region SQL
                string sqlStr = "update Adverts set advert_aduitstate =@aduitstate,advert_applytime =@applyTime,advert_updatetime =@dateTime,advert_adminid = @adminId,advert_adminname =@adminName where advert_id =@AdvertId and advert_isdel =0";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@aduitstate", advert.AduitState);
                sqlParam.AddParam("@applyTime", DateTime.Now);
                sqlParam.AddParam("@dateTime", DateTime.Now);
                sqlParam.AddParam("@adminId", advert.AdminId);
                sqlParam.AddParam("@adminName", advert.AdminName);
                sqlParam.AddParam("@advertId", advert.AdvertId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", advert, e);
                return 0;
            }
        }
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public int DeleteAdvert(Advert advert)
        {
            try
            {
                #region Sql
                string sqlStr = "update  Adverts set advert_updatetime = @updateTime,advert_isdel =@isdel,advert_adminid =@adminId,advert_adminname =@adminName where advert_id =@AdvertId";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@updateTime", DateTime.Now);
                sqlParam.AddParam("@isdel", 1);
                sqlParam.AddParam("@adminId", advert.AdminId);
                sqlParam.AddParam("@adminName", advert.AdminName);
                sqlParam.AddParam("@AdvertId", advert.AdvertId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", advert, e);
                return 0;
            }
        }
        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public int DownLine(Advert advert)
        {
            try
            {
                #region Sql
                string sqlStr = "update Adverts set advert_state = 2,advert_aduitstate = 1,advert_updatetime =@updateTime,advert_adminid =@adminId,advert_adminname =@adminName where advert_id =@AdvertId and advert_isdel =0";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@updateTime", DateTime.Now);
                sqlParam.AddParam("@adminId", advert.AdminId);
                sqlParam.AddParam("@adminName", advert.AdminName);
                sqlParam.AddParam("@AdvertId", advert.AdvertId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException<Advert>("cheny", advert, e);
                return 0;
            }
        }
        /// <summary>
        /// 根据广告Id获取广告信息
        /// </summary>
        /// <param name="advertId">排序Id</param>
        /// <returns></returns>
        public Advert GetAdvertById(int advertId)
        {
            try
            {
                Advert result = null;
                #region SQL
                string sqlStr = "select advert_id as AdvertId,advert_sort as Sort,adverttype_id as AdvertTypeId,advertlocation_id as AdvertLocationId,advert_url as Address,category_id as Category,advert_guid as Guid,advert_name as Name,advert_remark as Remark,advert_begintime as BeginTime,advert_endtime as EndTime,advert_createtime as CreateTime,advert_state as State,advert_aduitstate as AduitState,advert_applytime as ApplyTime,advert_aduittime as AduitTime,advert_aduitremark as AduitRemark,advert_updatetime as UpdateTime,advert_isdel as Isdel,advert_adminid as AdminId,advert_adminname as AdminName from Adverts with(nolock) where advert_isdel = 0 and advert_id=@advert_id ";
                #endregion

                #region 参数
                var param = new SqlParam();
                param.AddParam("@advert_id", advertId);
                #endregion

                #region 执行
                DataTable dt = KYJ_ZushouRDB.GetTable(sqlStr, param);
                #endregion

                #region 处理
                if (dt != null)
                {
                    result = DataHelper<Advert>.GetEntity(dt.Rows[0]);
                }
                #endregion
                return result;
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", advertId, e);
                return null;
            }
        }
        /// <summary>
        /// 获取位置信息
        /// </summary>
        /// <param name="locationId">位置ID</param>
        /// <returns></returns>
        public Manager_AdvertLocationEntity GetLoactionInfo(int locationId)
        {
            try
            {
                #region SQL
                //查询CategoryId 
                string sql = "select g.category_id from AdvertLocations as g inner join Categories as c on g.category_id = c.category_id where advertlocation_id = " + locationId;
                string cateId = KYJ_ZushouWDB.GetFirst(sql);
                string sqlStr = string.Empty;
                if (Auxiliary.ToInt32(cateId) <= 0)
                {
                    sqlStr = "select g.advertlocation_id as AdvertId,g.adverttype_id as AdvertTypeId,t.adverttype_name as AdvertTypeName,g.category_id as CategoryId,g.advertlocation_name as LocationName from AdvertLocations as g inner join AdvertTypes as t on g.adverttype_id = t.adverttype_id  where advertlocation_id =@id";
                }
                else
                {
                    sqlStr = "select g.advertlocation_id as AdvertId,g.adverttype_id as AdvertTypeId,t.adverttype_name as AdvertTypeName,g.category_id as CategoryId,c.category_name as CategoryName,g.advertlocation_name as LocationName from AdvertLocations as g inner join AdvertTypes as t on g.adverttype_id = t.adverttype_id inner join Categories as c on g.category_id = c.category_id where advertlocation_id =@id";
                }
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", locationId);
                #endregion
                DataTable dt = KYJ_ZushouRDB.GetTable(sqlStr, sqlParam);//.Rows[0];
                if (dt.Rows.Count <= 0)
                {
                    return null;
                }
                return DataHelper<Manager_AdvertLocationEntity>.GetEntity(dt.Rows[0]);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", locationId.ToString(), e);
                return null;
            }
        }

        /// <summary>
        /// 根据位置信息返回位置Id
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public int GetLoactionId(Manager_AdvertLocationEntity location)
        {
            try
            {
                #region SQL
                string sqlStr = "select advertlocation_id from AdvertLocations with(nolock) where adverttype_id =@typeId and category_id =@categoryId and advertlocation_name =@locationName";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@typeId", location.AdvertTypeId);
                sqlParam.AddParam("@categoryId", location.CategoryId);
                sqlParam.AddParam("@locationName", location.LocationName);
                #endregion
                #region 执行
                DataTable dt = KYJ_ZushouRDB.GetTable(sqlStr, sqlParam);
                #endregion
                return Auxiliary.ToInt32(dt.Rows[0][0]);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", location, e);
                return 0;
            }
        }



        /// <summary>
        /// 删除排序列表中的广告
        /// </summary>
        /// <param name="AdvertId"></param>
        /// <returns></returns>
        public int DeleteAdvertGoods(int advertId)
        {
            try
            {
                #region SQL
                string sqlStr = "delete from AdvertGoodses where advert_id =@id ";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", advertId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", advertId, e);
                return 0;
            }
        }

#if !isUse
        /// <summary>
        /// 申请审核(停用)
        /// </summary>
        /// <param name="advertId"></param>
        /// <returns></returns>
        public int ApplyAdvert(Advert advert)
        {
        #region SQL
            string sqlStr = "update Adverts set advert_applytime =@datetime,advert_updatetime =@updateTime,advert_adminid=@adminId,advert_adminname=@adminName where advert_id=@id";
        #endregion
        #region 参数赋值
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@datetime", DateTime.Now);
            sqlParam.AddParam("@updateTime", DateTime.Now);
            sqlParam.AddParam("@adminId", advert.AdminId);
            sqlParam.AddParam("@adminName", advert.AdminName);
            sqlParam.AddParam("@id", advert.AdvertId);
        #endregion
            return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
        } 
#endif
        /// <summary>
        /// 审核广告
        /// </summary>
        /// <param name="Advert"></param>
        /// <returns></returns>
        public int CheckAdvert(Advert advert)
        {
            try
            {
                #region SQL
                string sqlStr = "update Adverts set advert_aduitstate=@aduitState,advert_aduittime =@aduitTime,advert_aduitremark=@aduitRemark,advert_updatetime =@updateTime,advert_adminid =@adminId,advert_adminname =@adminName where advert_id=@id";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@aduitState", advert.AduitState);
                sqlParam.AddParam("@aduitTime", DateTime.Now);
                sqlParam.AddParam("@aduitRemark", advert.AduitRemark);
                sqlParam.AddParam("@updateTime", DateTime.Now);
                sqlParam.AddParam("@adminId", advert.AdminId);
                sqlParam.AddParam("@adminName", advert.AdminName);
                sqlParam.AddParam("@id", advert.AdvertId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException<Advert>("cheny", advert, e);
                return 0;
            }
        }
        #region 发布广告 +int PublishAdvert(Advert advert)
        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public int PublishAdvert(Advert advert)
        {
            try
            {
                #region SQL
                string sql = string.Empty;
                sql = "INSERT INTO Adverts([adverttype_id]"
                       + ",[advertlocation_id]"
                       + ",[category_id]"
                       + ",[advert_begintime]"
                       + ",[advert_endtime]"
                      + ",[advert_price]"
                      + ",[advert_state]"
                      + ",[advert_sort]"
                      + ",[advert_weight]"
                      + ",[advert_createtime]"
                      + ",[advert_updatetime]"
                      + ",[advert_isdel]"
                      + ",[advert_adminid]"
                      + ",[advert_adminname]"
                      + ",[advert_guid]"
                      + ",[advert_name]"
                      + ",[advert_remark]"
                      + ",[advert_merchantname]"
                      + ",[advert_url]"
                      + ",[advert_aduitstate])"
                 + "VALUES"
                       + "(@advertTypeId"
                       + ",@advertLocationId"
                       + ",@categoryId"
                       + ",@advertBegintime"
                       + ",@advertEndtime"
                       + ",@advertPrice"
                       + ",@advertState"
                       + ",@advertSort"
                       + ",@advertWeight"
                       + ",@advertCreatetime"
                       + ",@advertUpdatetime"
                       + ",@advertIsdel"
                       + ",@advertAdminid"
                       + ",@advertAdminname"
                       + ",@advertGuid"
                      + ",@advertName"
                      + ",@advertRemark"
                      + ",@advertMerchantname"
                      + ",@advertUrl"
                      + ",@advertAduitstate)";
                #endregion

                #region 参数赋值
                var param = new SqlParam();
                param.AddParam("advertTypeId", advert.AdvertTypeId);
                param.AddParam("advertLocationId", advert.AdvertLocationId);
                param.AddParam("categoryId", advert.CategoryId);
                param.AddParam("advertBegintime", advert.BeginTime);
                param.AddParam("advertEndtime", advert.EndTime);
                param.AddParam("advertPrice", advert.Price);
                param.AddParam("advertState", advert.State);
                param.AddParam("advertSort", advert.Sort);
                param.AddParam("advertWeight", advert.Weight);
                param.AddParam("advertCreatetime", advert.CreateTime);
                param.AddParam("advertUpdatetime", advert.UpdateTime);
                param.AddParam("advertIsdel", advert.Isdel);
                param.AddParam("advertAdminid", advert.AdminId);
                param.AddParam("advertAdminname", advert.AdminName);
                param.AddParam("advertGuid", advert.Guid);
                param.AddParam("advertName", advert.Name);
                param.AddParam("advertRemark", advert.Remark);
                param.AddParam("advertMerchantname", advert.MerchantName);
                param.AddParam("advertUrl", advert.Address);
                param.AddParam("advertAduitstate", advert.AduitState);
                #endregion

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException<Advert>("cheny", advert, e);
                return 0;
            }
        }
        #endregion

        #region 修改广告 +int ModifyAdvert(Advert advert)
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public int ModifyAdvert(Advert advert)
        {
            try
            {
                #region SQL
                string sql = string.Empty;
                sql = "UPDATE [Adverts]"
       + "SET [advert_begintime] = @advertBegintime"
          + ",[advert_endtime] = @advertEndtime"
         + " ,[advert_price] = @advertPrice"
         + " ,[advert_state] = @advertState"
         + " ,[advert_sort] = @advertSort"
         + " ,[advert_weight] = @advertWeight"
         + " ,[advert_updatetime] = @advertUpdatetime"
         + " ,[advert_isdel] = @advertIsdel"
         + " ,[advert_adminid] = @advertAdminid"
         + " ,[advert_adminname] = @advertAdminname"
          + ",[advert_guid] =@advertGuid"
         + " ,[advert_name] = @advertName"
         + " ,[advert_remark] = @advertRemark"
          + ",[advert_merchantname] = @advertMerchantname"
         + " ,[advert_url] = @advertUrl"
         + " ,[advert_aduitstate] =@advertAduitstate "
         + " WHERE advert_id=@advertId";
                #endregion

                #region 参数赋值
                var param = new SqlParam();
                param.AddParam("advertBegintime", advert.BeginTime);
                param.AddParam("advertEndtime", advert.EndTime);
                param.AddParam("advertPrice", advert.Price);
                param.AddParam("advertState", advert.State);
                param.AddParam("advertSort", advert.Sort);
                param.AddParam("advertWeight", advert.Weight);
                param.AddParam("advertUpdatetime", advert.UpdateTime);
                param.AddParam("advertIsdel", advert.Isdel);
                param.AddParam("advertAdminid", advert.AdminId);
                param.AddParam("advertAdminname", advert.AdminName);
                param.AddParam("advertGuid", advert.Guid);
                param.AddParam("advertName", advert.Name);
                param.AddParam("advertRemark", advert.Remark);
                param.AddParam("advertMerchantname", advert.MerchantName);
                param.AddParam("advertUrl", advert.Address);
                param.AddParam("advertAduitstate", advert.AduitState);
                param.AddParam("advertId", advert.AdvertId);
                #endregion

                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException<Advert>("cheny", advert, e);
                return 0;
            }
        }
        #endregion

        #region 获取页面数据 +List<AdvertTypes> GetPageData()
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <returns></returns>
        public List<AdvertTypes> GetAdvertTypesData()
        {
            try
            {
                string sql = "select adverttype_id as AdvertTypeID,adverttype_name as AdvertTypeName,adverttype_iscatetory as AdvertTypeIsCategory,adverttype_sort AdvertTypeSort from AdvertTypes nolock";
                DataTable dt = KYJ_ZushouRDB.GetTable(sql);
                List<AdvertTypes> list = DataHelper<AdvertTypes>.GetEntityList(dt);
                return list;
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", null, e);
                return null;
            }
        }
        #endregion

        #region 根据分类ID 查找其父节点名称 +string GetCategoryName(int categoryId)
        /// <summary>
        /// 根据分类ID 查找其父节点名称  
        /// <para>eg：家具 - 客厅家具</para>
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <returns>父分类名称</returns>
        public string GetCategoryName(int categoryId)
        {
            try
            {
                string sql = "SELECT category_name from Categories where category_id =(SELECT category_pid FROM Categories nolock where category_id= @categoryId)";
                var param = new SqlParam();
                param.AddParam("categoryId", categoryId);
                return KYJ_ZushouWDB.GetFirst(sql, param);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", categoryId.ToString(), e);
                return string.Empty;
            }
        }
        #endregion

        #region 根据页面类型和分类查找广告位置信息 +List<AdvertLocations> GetLocationData(int advertTpyeID, int? categoryID)
        /// <summary>
        /// 根据页面类型和分类查找广告位置信息
        /// </summary>
        /// <param name="advertTpyeID">页面类型ID</param>
        /// <param name="categoryID">分类ID</param>
        /// <returns></returns>
        public List<AdvertLocations> GetLocationData(int advertTypeID, int? categoryID)
        {
            try
            {
                string sql = "SELECT adverttype_id as AdvertTypeId,category_id as CategoryId, advertlocation_id as GeographyCode ,advertlocation_name as GeographyName ,advertlocation_num as AdvertLocationNum FROM AdvertLocations  where adverttype_id=@typeID ";
                var param = new SqlParam();
                if (categoryID != null)
                {
                    sql += " and category_id=@categoryID";
                    param.AddParam("categoryId", categoryID);
                }
                param.AddParam("typeID", advertTypeID);

                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                List<AdvertLocations> list = DataHelper<AdvertLocations>.GetEntityList(dt);
                return list;
            }
            catch (Exception e)
            {
                string errParam = string.Format("({0},{1})", advertTypeID, categoryID);
                Log4net.RecordLog.RecordException("cheny", errParam, e);
                return null;
            }
        }
        #endregion

        #region 获取允许发布广告数量（图片） +int GetMaxNum(int advertTypeID, int? categoryID, int? locationID)
        /// <summary>
        /// 获取允许发布广告数量（图片）
        /// </summary>
        /// <param name="locationID">广告位置ID</param>
        /// <returns>允许发布广告数量</returns>
        public string GetMaxNum(int? locationID)
        {
            try
            {
                //select advertlocation_num MaxNum FROM AdvertLocations where advertlocation_id=1 AND adverttype_id=1 AND category_id=0
                string sql = "select advertlocation_num MaxNum FROM AdvertLocations where advertlocation_id=@locationID ";
                var param = new SqlParam();
                param.AddParam("locationID", locationID);
                return KYJ_ZushouWDB.GetFirst(sql, param);
            }
            catch (Exception e)
            {
                string errParam = string.Format("({0})", locationID);
                Log4net.RecordLog.RecordException("cheny", errParam, e);
                return "0";
            }
        }
        #endregion

        #region 获取图片尺寸 +AdvertSize GetAdvertSize(int? locationId)
        /// <summary>
        /// 获取图片尺寸
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public AdvertSize GetAdvertSize(int? locationId)
        {
            try
            {
                string sql = "SELECT advertlocation_width as AdvertWidth,advertlocation_height as AdvertHeight from AdvertLocations where advertlocation_id=@locationID";
                var param = new SqlParam();
                param.AddParam("locationID", locationId);
                DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<AdvertSize>.GetEntity(dt.Rows[0]);
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", locationId.ToString(), e);
                return null;
            }
        } 
        #endregion

    }
}
