using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.TenancyTemplates;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;

namespace KYJ.ZS.BLL.TenancyTemplates
{
    /// <summary>
    /// 作者;maq
    /// 时间:2014年4月24日
    /// 描述：租期业务处理类
    /// </summary>
    public class TenancyTemplateBll
    {
        TenancyTemplateDal dal = new TenancyTemplateDal();
        /// <summary>
        /// 添加租期模板
        /// </summary>
        /// <param name="template"></param> 
        /// <returns></returns>
        public Boolean Insert(TenancyTemplate template)
        {
            return dal.Insert(template) > 0;
        }

        /// <summary>
        /// 删除租期模板
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public Boolean Delete(int tid,int merId)
        {
            return dal.Delete(tid,merId) > 0;
        }

        /// <summary>
        /// 获取租期模板列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public PageData<TenancyTemplate> GetList(int merchantId, int pageIndex)
        {
            PagePms pms = new PagePms();
            pms.PageIndex = pageIndex;
            pms.PageSize = 10;
            pms.SortColnum = "ttemp_createtime desc";
            pms.StrWhere = "merchant_id = '" + merchantId + "'";
            return dal.GetList(pms);
        }
        /// <summary>
        /// 更新租期模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public Boolean Update(TenancyTemplate template,int merId)
        {
            return dal.UpDate(template, merId) > 0;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public TenancyTemplate GetModel(int tid,int merId)
        {
            return dal.GetModel(tid, merId);
        }
    }
}
