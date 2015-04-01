using System.Collections.Generic;
using KYJ.ZS.DAL.FreightTemplates;
using KYJ.ZS.Models.FreightTemplates;

namespace KYJ.ZS.BLL.FreightTemplates
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-18
    /// Desc：操作运费模板(数据库表FreightTemplates)，包括查询、添加、修改
    /// </summary>
    public class FreightTemplateBll
    {
        private FreightTemplateDal dal = new FreightTemplateDal();

        /// <summary>
        /// 校验模板名称(模板名称已存在返回false)
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="name">运费模板名称</param>
        /// <returns>模板名称已存在返回false</returns>
        public bool CheckName(int merchantId, string name)
        {
            return !(dal.CheckName(merchantId, name) > 0);
        }

        /// <summary>
        /// 获取模板原有名称
        /// </summary>
        /// <param name="ftempId">运费模板ID</param>
        /// <returns></returns>
        public string GetName(int ftempId)
        {
            return dal.GetName(ftempId);
        }

        /// <summary>
        /// 添加、修改运费模板
        /// </summary>
        /// <param name="freightTemplate">运费模板</param>
        /// <returns></returns>
        public bool CreateOrUpdate(FreightJsonEntity freightEntity)
        {
            return dal.CreateOrUpdate(freightEntity) == 1;
        }

        /// <summary>
        /// 获取运费模板(old)
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public FreightTemplateEntity GetFreightTemplate(int freightTemplateID)
        {
            return dal.GetFreightTemplate(freightTemplateID);
        }

        /// <summary>
        /// 获取运费模板(new)
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public FreightTemplateEntity GetFreightTemplate(int merchantId, int freightTemplateID)
        {
            return dal.GetFreightTemplate(merchantId, freightTemplateID);
        }

        /// <summary>
        /// 获取运费模板下拉列表
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns></returns>
        public IList<FreightSelectEntity> GetFreightTemplateList(int merchantId)
        {
            return dal.GetFreightTemplateList(merchantId);
        }

        /// <summary>
        /// 获取运费模板分页列表
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<FreightIndexEntity> GetFreightTemplateList(int merchantId, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetFreightTemplateList(merchantId, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 删除运费模板
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public bool Delete(int freightTemplateID)
        {
            return dal.Delete(freightTemplateID) > 0;
        }

        /// <summary>
        /// 是否包邮
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public bool IsFreeship(int freightTemplateID)
        {
            return dal.IsFreeship(freightTemplateID);
        }

        /// <summary>
        /// 获取计价方式(0 未知，1 按件数，2 按重量，3 按体积)
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns>0 未知，1 按件数，2 按重量，3 按体积</returns>
        public int GetPriceMode(int freightTemplateID)
        {
            return dal.GetPriceMode(freightTemplateID);
        }

        #region 邓福伟
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：Web前端租商品详情页,运费模版列表
        /// </summary>
        /// <param name="ftemp_id">运费模版Id</param>
        /// <returns>运费模版列表</returns>
        public List<Web_FreightTemplateEntity> Web_GetFreightTemplateList(int ftemp_id)
        {
           return dal.Web_GetFreightTemplateList(ftemp_id);
        } 
        #endregion
    }
}
