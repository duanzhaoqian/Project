using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Generalizes;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.RentalGoodses;

namespace KYJ.ZS.DAL.Generalizes
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-28
    /// 描述：管理商品排序
    /// </summary>
    public class GeneralizeManageDal
    {
        /// <summary>
        /// 根据分页条件获取当前页的数据
        /// </summary>
        /// <param name="pms">分页属性对象</param>
        /// <returns></returns>
        public PageData<Generalize> GetPageData(PagePmsDal pmsdal, GeneralizeSearchEntity searchEntity)
        {
            try
            {
                pmsdal.TableList = "Generalizes";
                pmsdal.ColList = "generalize_id as GeneralizeId,generalizetype_id as GeneralizeTypeId,generalizelocation_id as GeneralizeLocationId,category_id as Category,generalize_name as Name,generalize_remark as Remark,generalize_begintime as BeginTime,generalize_endtime as EndTime,generalize_createtime as CreateTime,generalize_state as State,generalize_aduitstate as AduitState,generalize_aduitremark as AduitRemark,generalize_applytime as ApplyTime,generalize_aduittime as AduitTime,generalize_updatetime as UpdateTime,generalize_isdel as Isdel,admin_id as AdminId,admin_name as AdminName";
                pmsdal.StrWhere = "generalize_isdel =0 and generalize_state<>0 and generalize_aduitstate<>0";
                #region 拼接搜索条件
                if (!string.IsNullOrEmpty(searchEntity.NameSearch))
                {
                    pmsdal.StrWhere += " and generalize_name like '%" + searchEntity.NameSearch.Trim() + "%'";
                }
                else if (searchEntity.BeginTime != null && searchEntity.EndTime == null)
                {
                    pmsdal.StrWhere += " and generalize_endtime>='" + searchEntity.BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "'";
                }
                else if (searchEntity.BeginTime == null && searchEntity.EndTime != null)
                {
                    pmsdal.StrWhere += " and generalize_begintime<='" + searchEntity.EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                }
                else
                    if (searchEntity.BeginTime != null && searchEntity.EndTime != null)
                    {
                        pmsdal.StrWhere += " and generalize_endtime>='" + searchEntity.BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "' and generalize_begintime<='" + searchEntity.EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "'";
                    }
                switch (searchEntity.State)
                {
                    case 1: pmsdal.StrWhere += " and generalize_aduitstate = 1";
                        pmsdal.SortColnum = "generalize_updatetime desc";
                        break;
                    case 2: pmsdal.SortColnum = "generalize_updatetime desc";
                        pmsdal.StrWhere += " and generalize_aduitstate=2 or generalize_aduitstate=3 or generalize_aduitstate=4";
                        break;
                    case 3: pmsdal.StrWhere += " and generalize_aduitstate = 5 and generalize_begintime>'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'";
                        pmsdal.SortColnum = "generalize_updatetime desc";
                        break;
                    case 4: pmsdal.StrWhere += " and generalize_aduitstate = 5 and generalize_begintime<'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' and generalize_endtime >'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' and generalize_state = 1";
                        pmsdal.SortColnum = "generalize_updatetime desc";
                        break;
                    case 5: pmsdal.StrWhere += " and generalize_aduitstate = 5 and generalize_endtime<'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' or (generalize_state = 2 and generalize_isdel = 0 and generalize_state<>0 and generalize_aduitstate<>0) ";
                        pmsdal.SortColnum = "generalize_updatetime desc";
                        break;
                }
                #endregion
                return KYJ_ZushouRDB.GetPages<Generalize>(pmsdal);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "商品排序列表", ex);
                return null;
            } 

        }
        /// <summary>
        /// 删除排序
        /// </summary>
        /// <param name="generalize"></param>
        /// <returns></returns>
        public int DeleteGeneralize(Generalize generalize)
        {
            try
            {
                #region Sql
                string sqlStr = "update  Generalizes set generalize_updatetime = @updateTime,generalize_isdel =@isdel,admin_id =@adminId,admin_name =@adminName where generalize_id =@generalizeId";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@updateTime", DateTime.Now);
                sqlParam.AddParam("@isdel", 1);
                sqlParam.AddParam("@adminId", generalize.AdminId);
                sqlParam.AddParam("@adminName", generalize.AdminName);
                sqlParam.AddParam("@generalizeId", generalize.GeneralizeId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);

            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "删除商品排序", ex);
                return 0;
            }

         
        }
        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="generalize"></param>
        /// <returns></returns>
        public int DownLine(Generalize generalize)
        {
            try
            {
                #region Sql
                string sqlStr = "update Generalizes set generalize_state = 2,generalize_updatetime =@updateTime,admin_id =@adminId,admin_name =@adminName where generalize_id =@generalizeId and generalize_isdel =0";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@updateTime", DateTime.Now);
                sqlParam.AddParam("@adminId", generalize.AdminId);
                sqlParam.AddParam("@adminName", generalize.AdminName);
                sqlParam.AddParam("@generalizeId", generalize.GeneralizeId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "商品排序强制下线", ex);
                return 0;
               
            }
          
        }
        /// <summary>
        /// 根据排序Id获取商品排序信息
        /// </summary>
        /// <param name="generalizeId">排序Id</param>
        /// <returns></returns>
        public Generalize GetGeneralizeById(int generalizeId)
        {
            try
            {
                Generalize result = null;
                #region SQL
                string sqlStr = "select generalize_id as GeneralizeId,generalizetype_id as GeneralizeTypeId,generalizelocation_id as GeneralizeLocationId,category_id as Category,generalize_name as Name,generalize_remark as Remark,generalize_begintime as BeginTime,generalize_endtime as EndTime,generalize_createtime as CreateTime,generalize_state as State,generalize_aduitstate as AduitState,generalize_applytime as ApplyTime,generalize_aduittime as AduitTime,generalize_aduitremark as AduitRemark,generalize_updatetime as UpdateTime,generalize_isdel as Isdel,admin_id as AdminId,admin_name as AdminName from Generalizes with(nolock) where generalize_isdel = 0 and generalize_id =@id";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", generalizeId);
                #endregion
                #region 执行
                DataTable dt = KYJ_ZushouRDB.GetTable(sqlStr, sqlParam);
                #endregion
                #region 处理
                if (dt != null)
                {
                    result = DataHelper<Generalize>.GetEntity(dt.Rows[0]);
                }
                #endregion
                return result;

            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "获取商品排序信息", ex);
                return null;
            }
           
        }
        /// <summary>
        /// 获取位置信息
        /// </summary>
        /// <param name="locationId">位置ID</param>
        /// <returns></returns>
        public Manager_GeneralizeLocationEntity GetLoactionInfo(int locationId)
        {
            try
            {
                #region SQL

                string sqlStr = "SELECT g.generalizelocation_id as GeographyCode," +
                                "g.generalizetype_id as GeneralizeTypeId," +
                                "g.generalizelocation_num as Number," +
                                "t.generalizetype_name as GeneralizeTypeName," +
                                "g.category_id as CategoryId," +
                                "c.category_name as CategoryName," +
                                "g.generalizelocation_name as GeographyName " +
                                "from GeneralizeType as t right join GeneralizeLocations as g on g.generalizetype_id = t.generalizetype_id " +
                                "left join Categories as c on g.category_id = c.category_id" +
                                "  where generalizelocation_id =@id";

                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", locationId);
                #endregion
                DataTable dt = KYJ_ZushouRDB.GetTable(sqlStr, sqlParam);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<Manager_GeneralizeLocationEntity>.GetEntity(dt.Rows[0]);
           
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "商品排序信息-获取位置信息", ex);
                return null;
            }
         
        }
        /// <summary>
        /// 获取商品的ID和序号
        /// </summary>
        /// <param name="generalizeId"></param>
        /// <returns></returns>
        public List<Manager_RentalGoodsAduitEntity> GetRentalInfoByGeneralizeId(int generalizeId)
        {
            try
            {
                #region SQL
                string sqlStr = "select generalizegoods_id as GeneralizeGoodsId,rental_id as GoodsId,generalizegoods_sort as Sort from GeneralizeGoodses with(nolock) where generalize_id =@id order by generalizegoods_sort ";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", generalizeId);
                #endregion
                return DataHelper<Manager_RentalGoodsAduitEntity>.GetEntityList(KYJ_ZushouRDB.GetTable(sqlStr, sqlParam));
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "商品排序信息-获取排序商品的ID和序号", ex);
                return null;
            }
           
        }
        /// <summary>
        /// 根据商户ID获取商户名字
        /// </summary>
        /// <param name="p">商户ID</param>
        /// <returns></returns>
        public string GetMerchantName(int MerchantId)
        {
            try
            {
                #region SQL
                string sqlStr = "select merchant_name from Merchants where Merchant_id  =@merchantId";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@merchantId", MerchantId);
                #endregion
                DataTable dt = KYJ_ZushouRDB.GetTable(sqlStr, sqlParam);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "商品排序信息-根据商户ID获取商户名字", ex);
                return null;
                
            }
          
        }

        /// <summary>
        /// 根据位置信息返回位置
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public List<Manager_GeneralizeLocationEntity> GetLoactionId(Manager_GeneralizeLocationEntity location)
        {
            try
            {
                #region SQL
                string sqlStr = "select generalizelocation_id as GeographyCode, generalizetype_id as GeneralizeTypeId,layout_id as LayoutId,category_id as CategoryId,generalizelocation_name as GeographyName,generalizelocation_num as Number  from GeneralizeLocations with(nolock) where generalizetype_id =@typeId";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@typeId", location.GeneralizeTypeId);
                if (location.CategoryId != null)
                {
                    sqlStr += " and category_id=@categoryID";
                    sqlParam.AddParam("categoryId", location.CategoryId);
                }

                #endregion
                #region 执行
                DataTable dt = KYJ_ZushouRDB.GetTable(sqlStr, sqlParam);
                #endregion

                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<Manager_GeneralizeLocationEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "商品排序信息-根据位置信息返回位置", ex);
                return null;
            }
            
        }

        /// <summary>
        /// 获取推广类型
        /// </summary>
        /// <returns></returns>
        public List<GeneralizeTypes> GetGeneralizetypesData()
        {
            try
            {
                #region SQL
                string sql = "select generalizetype_id as GeneralizeTypeID,generalizetype_name as GeneralizeTypeName,generalizetype_iscatetory as GeneralizeIsCategory,generalizetype_sort as GeneralizeSort from GeneralizeType nolock";
                #endregion
                DataTable dt = KYJ_ZushouRDB.GetTable(sql);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                List<GeneralizeTypes> list = DataHelper<GeneralizeTypes>.GetEntityList(dt);
                return list;
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "商品排序信息-获取推广类型", ex);
                return null;
               
            }
          
        }
        /// <summary>
        /// 添加商品排序
        /// </summary>
        /// <param name="generalize"></param>
        /// <returns></returns>
        public int PublishGeneralize(Generalize generalize)
        {
            try
            {
                #region SQL
                string sql = string.Empty;
                sql = "INSERT INTO Generalizes([generalizetype_id]"
                       + ",[generalizelocation_id]"
                       + ",[category_id]"
                       + ",[generalize_begintime]"
                       + ",[generalize_endtime]"
                      + ",[generalize_state]"
                      + ",[generalize_createtime]"
                      + ",[generalize_updatetime]"
                      + ",[generalize_isdel]"
                      + ",[admin_id]"
                      + ",[admin_name]"
                      + ",[generalize_name]"
                      + ",[generalize_remark]"
                      + ",[generalize_aduitstate])"
                 + "VALUES"
                       + "(@generalizeTypeId"
                       + ",@generalizeLocationId"
                       + ",@categoryId"
                       + ",@generalizeBegintime"
                       + ",@generalizeEndtime"
                       + ",@generalizeState"
                       + ",@generalizeCreatetime"
                       + ",@generalizeUpdatetime"
                       + ",@generalizeIsdel"
                       + ",@generalizeAdminid"
                       + ",@generalizeAdminname"
                      + ",@generalizeName"
                      + ",@generalizeRemark"
                      + ",@generalizeAduitstate)";
                #endregion

                #region 参数赋值
                var param = new SqlParam();
                param.AddParam("generalizeTypeId", generalize.GeneralizeTypeId);
                param.AddParam("generalizeLocationId", generalize.GeneralizeLocationId);
                param.AddParam("categoryId", generalize.CategoryId);
                param.AddParam("generalizeBegintime", generalize.BeginTime);
                param.AddParam("generalizeEndtime", generalize.EndTime);
                param.AddParam("generalizeState", generalize.State);
                param.AddParam("generalizeCreatetime", generalize.CreateTime);
                param.AddParam("generalizeUpdatetime", generalize.UpdateTime);
                param.AddParam("generalizeIsdel", generalize.Isdel);
                param.AddParam("generalizeAdminid", generalize.AdminId);
                param.AddParam("generalizeAdminname", generalize.AdminName);
                param.AddParam("generalizeName", generalize.Name);
                param.AddParam("generalizeRemark", generalize.Remark);
                param.AddParam("generalizeAduitstate", generalize.AduitState);
                #endregion
                return KYJ_ZushouWDB.SqlExecuteRuturnId(sql, param);
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "商品排序信息-添加商品排序", ex);
                return 0;
            }
           

        }
        /// <summary>
        /// 排序商品搜索
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="rentalSearch">搜索条件</param>
        /// <returns></returns>
        public PageData<Manager_RentalGoodsAduitEntity> GetGeneralizeGoods(PagePmsDal pagePms, GeneralizeRentalGoodsSearchEntity rentalSearch)
        {
            try
            {
                pagePms.ColList = " r.rental_id as GoodsId,r.rental_title as GoodsName,r.rental_monthprice as MonthPrice,m.merchant_name as MerchantName,r.rental_code as GoodsCode,r.rental_brandname as Brand ";
                pagePms.TableList = " RentalGoodses as r inner join Merchants as m on r.merchant_id = m.merchant_id ";
                pagePms.SortColnum = " r.rental_title ";
                pagePms.StrWhere = " r.rental_isdel = 'false' ";
                #region 拼接搜索条件
                if (!string.IsNullOrEmpty(rentalSearch.MerchantName))
                {
                    pagePms.StrWhere += " and m.merchant_name like '%" + rentalSearch.MerchantName + "%'";
                }
                if (!string.IsNullOrEmpty(rentalSearch.GoodsCode))
                {
                    pagePms.StrWhere += " and r.rental_code like '%" + rentalSearch.GoodsCode + "%'";
                }
                if (!string.IsNullOrEmpty(rentalSearch.GoodsName))
                {
                    pagePms.StrWhere += " and r.rental_title like '%" + rentalSearch.GoodsName + "%'";
                }
                #endregion
                return KYJ_ZushouRDB.GetPages<Manager_RentalGoodsAduitEntity>(pagePms);
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "商品排序信息-排序商品搜索", ex);
                return null;
            }
            
        }
        /// <summary>
        /// 删除排序列表中的商品
        /// </summary>
        /// <param name="GeneralizeId"></param>
        /// <returns></returns>
        public int DeleteGeneralizeGoods(int GeneralizeId)
        {
            try
            {
                #region SQL
                string sqlStr = "delete from GeneralizeGoodses where generalize_id =@id ";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", GeneralizeId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "商品排序信息-删除排序商品", ex);
                return 0;
            }
           
        }
        /// <summary>
        /// 更新商品排序
        /// </summary>
        /// <param name="generalize">商品排序信息</param>
        /// <returns>此条记录的Id</returns>
        public int UpdateGeneralize(Generalize generalize)
        {
            try
            {
                #region SQL
                string sqlStr = "update Generalizes set generalize_name =@generalizeName,generalize_remark =@remark,generalize_state=@state,generalize_aduitstate=@aduitstate,generalize_begintime=@beginTime,generalize_endtime=@endTime,generalize_updatetime=@updateTime,admin_id=@adminId,admin_name =@adminName where generalize_id=@id";
                #endregion
                #region 参数赋值
                SqlParam sqlParem = new SqlParam();
                sqlParem.AddParam("@generalizeName", generalize.Name);
                sqlParem.AddParam("@remark", generalize.Remark);
                sqlParem.AddParam("@beginTime", generalize.BeginTime);
                sqlParem.AddParam("@endTime", generalize.EndTime);
                sqlParem.AddParam("@updateTime", generalize.UpdateTime);
                sqlParem.AddParam("@adminId", generalize.AdminId);
                sqlParem.AddParam("@adminName", generalize.AdminName);
                sqlParem.AddParam("@id",generalize.GeneralizeId);
                sqlParem.AddParam("@state",generalize.State);
                sqlParem.AddParam("@aduitstate",generalize.AduitState);
                #endregion
                return KYJ_ZushouWDB.SqlExecuteRuturnId(sqlStr, sqlParem);
            }
            catch (Exception ex)
            {
                Log4net.RecordLog.RecordException("menggd", "商品排序信息-更新商品排序", ex);
                return 0;
            }
           
        }
        /// <summary>
        /// 添加排序商品
        /// </summary>
        /// <param name="rentalGoods">排序商品信息</param>
        /// <returns></returns>
        public int AddGeneralizeGoods(Manager_RentalGoodsAduitEntity rentalGoods,int generalizeId)
        {
            try
            {
                #region SQL
                string sqlStr = "insert into GeneralizeGoodses (generalize_id,rental_id,generalizegoods_sort) values (@generalizeId,@rentalId,@Sort)";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@generalizeId", generalizeId);
                sqlParam.AddParam("@rentalId", rentalGoods.GoodsId);
                sqlParam.AddParam("@Sort", rentalGoods.Sort);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "商品排序信息-添加排序商品", ex);
                return 0;
            }
           
        }
        /// <summary>
        /// 提交核审
        /// </summary>
        /// <param name="generalizeId">排序ID</param>
        /// <returns></returns>
        public int ApplyAduit(Generalize generalize)
        {
            try
            {
                #region SQL
                string sqlStr = "update Generalizes set generalize_aduitstate =@aduitstate,generalize_applytime =@applyTime,generalize_updatetime =@dateTime,admin_id = @adminId,admin_name =@adminName where generalize_id =@generalizeId and generalize_isdel =0";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@aduitstate", 2);
                sqlParam.AddParam("@applyTime", DateTime.Now);
                sqlParam.AddParam("@dateTime", DateTime.Now);
                sqlParam.AddParam("@adminId", generalize.AdminId);
                sqlParam.AddParam("@adminName", generalize.AdminName);
                sqlParam.AddParam("@generalizeId", generalize.GeneralizeId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {
              Log4net.RecordLog.RecordException("mengggd","商品排序-提交审核",ex);
                return 0;
            }
         
        }
        /// <summary>
        /// 审核商品
        /// </summary>
        /// <param name="generalize"></param>
        /// <returns></returns>
        public int CheckGeneralize(Generalize generalize)
        {
            try
            {
                #region SQL
                string sqlStr = "update Generalizes set generalize_aduitstate=@aduitState,generalize_aduittime =@aduitTime,generalize_aduitremark=@aduitRemark,generalize_updatetime =@updateTime,admin_id =@adminId,admin_name =@adminName where generalize_id=@id";
                #endregion
                #region 参数赋值
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@aduitState", generalize.AduitState);
                sqlParam.AddParam("@aduitTime", DateTime.Now);
                sqlParam.AddParam("@aduitRemark", generalize.AduitRemark);
                sqlParam.AddParam("@updateTime", DateTime.Now);
                sqlParam.AddParam("@adminId", generalize.AdminId);
                sqlParam.AddParam("@adminName", generalize.AdminName);
                sqlParam.AddParam("@id", generalize.GeneralizeId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {
                
                
                Log4net.RecordLog.RecordException("menggd","商品排序—核审商品",ex);
                return 0;
            }
          
        }
    }
}
