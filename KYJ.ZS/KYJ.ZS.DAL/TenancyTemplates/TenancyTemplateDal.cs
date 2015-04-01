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

namespace KYJ.ZS.DAL.TenancyTemplates
{
    /// <summary>
    /// 作者:maq
    /// 时间:2014年4月17日
    /// 描述:租期模板数据访问类
    /// </summary>
    public partial class TenancyTemplateDal
    {
        /// <summary>
        /// 新增数据，并返回，受影响的行数
        /// </summary>
        /// <param name="model">TenancyTemplate的实体类</param>
        /// <returns>返回受影响的行数</returns>
        public long Insert(TenancyTemplate model)
        {
            #region SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TenancyTemplates(");
            strSql.Append("merchant_id, ttemp_title, ttemp_months, ttemp_createtime, ttemp_updatetime");
            strSql.Append(") values (");
            strSql.Append(" @MerchantId,@TtempTitle,@TtempMonths,@TtempCreatetime,@TtempUpdatetime");
            strSql.Append(")");
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@MerchantId", model.MerchantId);
            sqlParam.AddParam("@TtempTitle", model.TtempTitle);
            sqlParam.AddParam("@TtempMonths", model.TtempMonths);
            sqlParam.AddParam("@TtempCreatetime", DateTime.Now);
            sqlParam.AddParam("@TtempUpdatetime", DateTime.Now);
            #endregion
            return KYJ_ZushouWDB.SqlExecute(strSql.ToString(), sqlParam);
        } 

        /// <summary>
        /// 软删除租期模板
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int Delete(int tid,int merId)
        {
            string strSql = "update TenancyTemplates set  ttemp_isdel=1 where ttemp_id=@ttemp_id and merchant_id=@mid";
            SqlParam sqlParam  =new SqlParam();
            sqlParam.AddParam("@ttemp_id",tid);
            sqlParam.AddParam("@mid",merId);
            return KYJ_ZushouWDB.SqlExecute(strSql, sqlParam);
        }

        /// <summary>
        /// 更新数据，并返回，受影响的行数
        /// </summary>
        /// <param name="model">TenancyTemplate的实体类</param>
        /// <returns>返回受影响的行数</returns>
        public int UpDate(TenancyTemplate model,int merId)
        {
            #region SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TenancyTemplates ");
            strSql.Append("set ttemp_title= @ttemp_title,ttemp_months= @ttemp_months,ttemp_updatetime= @ttemp_updatetime");
            strSql.Append(" where ttemp_id=@ttemp_id and merchant_id=@mid");
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@ttemp_id", model.TtempId);
            sqlParam.AddParam("@ttemp_title", model.TtempTitle);
            sqlParam.AddParam("@ttemp_months", model.TtempMonths);
            sqlParam.AddParam("@ttemp_updatetime", DateTime.Now);
            sqlParam.AddParam("@mid",merId);
            #endregion
            return KYJ_ZushouWDB.SqlExecute(strSql.ToString(), sqlParam);
        }

        /// <summary>
        /// 得到一个TenancyTemplate对象
        /// </summary>
        /// <param name="TtempId">要得到对象的主键</param>
        /// <returns>返回TenancyTemplate对象</returns>
        public TenancyTemplate GetModel(int TtempId,int merId)
        {
            #region SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ");
            strSql.Append("ttemp_id as TtempId,");
            strSql.Append("merchant_id as MerchantId ,");
            strSql.Append("ttemp_title as TtempTitle ,");
            strSql.Append("ttemp_months as TtempMonths ,");
            strSql.Append("ttemp_createtime as TtempCreatetime ,");
            strSql.Append("ttemp_updatetime as TtempUpdatetime ,");
            strSql.Append("ttemp_isdel as TtempIsdel");
            strSql.Append(" from TenancyTemplates with(nolock)");
            strSql.Append(" where ttemp_id=@TtempId and merchant_id=@mid");
            #endregion
            #region 参数
            SqlParam sqlParam = new SqlParam();
            sqlParam.AddParam("@TtempId", TtempId);
            sqlParam.AddParam("@mid",merId);
            #endregion
            TenancyTemplate model = null;
            using (DataTable table = KYJ_ZushouRDB.GetTable(strSql.ToString(), sqlParam))
            {
                if (table.Rows.Count > 0)
                {
                    model = DataHelper<TenancyTemplate>.GetEntityList(table)[0];
                }
            }
            return model;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public PageData<TenancyTemplate> GetList(PagePms pms)
        {
            #region 准备参数
            PagePmsDal pagePmsDal = new PagePmsDal();
            pagePmsDal.PageIndex = pms.PageIndex;
            pagePmsDal.PageSize = pms.PageSize;
            pagePmsDal.StrWhere = " ttemp_isdel=0 ";
            if (pms.StrWhere.Length > 0)
            {
                pagePmsDal.StrWhere += " and " + pms.StrWhere;
            }
            pagePmsDal.SortColnum = pms.SortColnum;
            pagePmsDal.TableList = " TenancyTemplates  WITH(NOLOCK) ";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("ttemp_id as TtempId,");
            strSql.Append("merchant_id as MerchantId ,");
            strSql.Append("ttemp_title as TtempTitle ,");
            strSql.Append("ttemp_months as TtempMonths ,");
            strSql.Append("ttemp_createtime as TtempCreatetime ,");
            strSql.Append("ttemp_updatetime as TtempUpdatetime ,");
            strSql.Append("ttemp_isdel as TtempIsdel ");
            #endregion
            pagePmsDal.ColList = strSql.ToString();
            return KYJ_ZushouRDB.GetPages<TenancyTemplate>(pagePmsDal);
        }
    }
}
