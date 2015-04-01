using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.SaleGoodses;
using KYJ.ZS.DAL.DB;

namespace KYJ.ZS.DAL.SaleGoodses
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014.04.24
    /// 描述：用户对销售物品信息的管理、包括对商品的删，改，查
    /// </summary>
    public class SaleGoods_InformationManagementDal
    {
        /// <summary>
        /// 获取当前页的商品信息
        /// </summary>
        /// <param name="pagePms">分页的属性对象</param>
        /// <returns></returns>
        public PageData<SaleGoods_InformatioManagementEntity> GetPageData(PagePms pagePms, int userId)
        {
            try
            {
                #region 给分页属性对象PagePmsDal赋值
                PagePmsDal pagePmsDal = new PagePmsDal();
                pagePmsDal.TableList = "SaleGoodses(nolock)";
                pagePmsDal.ColList = "sale_id as Id, sale_contact as Contact,sale_contactphone as Contactphone,sale_tag as Tags, sale_provinceid as ProvinceId, sale_provincename as ProvinceName, sale_cityid as CityId, sale_cityname as CityName, sale_districtid as DistrictId, sale_districtname as DistrictName, sale_guid as Guid, sale_title as Title ,sale_degree as Degree ,sale_price as Price,sale_status as Status, sale_state as State, sale_createtime as CreateTime, sale_isdel as IsDel,sale_shelfreason as ShelfReason";
                pagePmsDal.PageIndex = pagePms.PageIndex;
                pagePmsDal.PageSize = pagePms.PageSize;
                pagePmsDal.SortColnum = "sale_createtime desc";
                pagePmsDal.StrWhere = "sale_isdel =0 and sale_state<>0 and user_id=" + userId;
                #endregion
                return KYJ_ZushouRDB.GetPages<SaleGoods_InformatioManagementEntity>(pagePmsDal);
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "闲置物品列表", ex);
                return null;
            }

        }
        /// <summary>
        /// 显示/隐藏商品(上架/下架)
        /// </summary>
        /// <param name="saleId">商品编号</param>
        /// <returns>受影响行数</returns>
        public int ShowGoods(int saleId, int actionFlag, int userId)
        {
            try
            {
                #region SQL
                string sqlStr = "Update SaleGoodses set sale_status=@status where sale_id = @saleid and user_id=@user_id";
                #endregion
                #region 设置参数
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@status", actionFlag);
                sqlParam.AddParam("@saleid", saleId);
                sqlParam.AddParam("@user_id", userId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("menggd", "闲置物品上下架", ex);
                return 0;
            }

        }
        /// <summary>
        /// 删除商品信息（标识删除）
        /// </summary>
        /// <param name="saleId">商品ID</param>
        /// <returns>受影响的行数</returns>
        public int DeleteGoods(int saleId, int userId)
        {
            try
            {
                #region SQL
                string sqlStr = "Update SaleGoodses set sale_isdel=@isdel where sale_id = @saleid and user_id=@userId";
                #endregion
                #region 设置参数
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@isdel", 1);
                sqlParam.AddParam("@saleid", saleId);
                sqlParam.AddParam("@userid", userId);
                #endregion
                return KYJ_ZushouWDB.SqlExecute(sqlStr, sqlParam);
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("mengggd", "删除闲置物品", ex);
                return 0;
            }

        }
        /// <summary>
        /// 获得标签名称
        /// </summary>
        /// <param name="tagid"></param>
        /// <returns></returns>
        public string GetTag(int tagid)
        {
            try
            {
                #region SQL
                string sqlStr = "select tag_name from Tags with(nolock) where tag_id =@id and tag_type =@type and tag_isdel =@isdel";
                #endregion
                #region 设置参数
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", tagid);
                sqlParam.AddParam("@type", 2);
                sqlParam.AddParam("@isdel", 0);
                #endregion
                return KYJ_ZushouRDB.GetTable(sqlStr, sqlParam).Rows[0][0].ToString();
            }
            catch (Exception ex)
            {

                Log4net.RecordLog.RecordException("mengggd","闲置物品-获取标签名",ex);
                return null;
            }

        }
    }
}
