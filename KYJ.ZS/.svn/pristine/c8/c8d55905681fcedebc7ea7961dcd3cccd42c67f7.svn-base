using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KYJ.ZS.BLL.FreightTemplates;
using KYJ.ZS.BLL.Geographies;
using KYJ.ZS.BLL.RentalGoodses;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.FreightTemplates;

namespace KYJ.ZS.Merchant.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-23
    /// Desc：运费模板控制器
    /// </summary>
    public class FreightTemplatesController : BaseController
    {
        #region 成员及变量
        private FreightTemplateBll bll = new FreightTemplateBll();
        private GeographyBll geographyBll = new GeographyBll();
        #endregion

        #region 运费模板首页加载
        /// <summary>
        /// 运费模板首页加载
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? pageIndex)
        {
            // 商户ID
            int merchantId = _ServiceContext.CurrentUser.UserID;
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            IList<FreightIndexEntity> freightTemplates = bll.GetFreightTemplateList(merchantId, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 用户删除数据后导致后一页无数据情况
            if ((freightTemplates == null || freightTemplates.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                freightTemplates = bll.GetFreightTemplateList(merchantId, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return View(freightTemplates);
        }
        #endregion

        #region 添加、修改运费模板

        /// <summary>
        /// 新增、修改运费模板
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(int? id)
        {
            FreightTemplateEntity model = null;
            if (id.HasValue)
            {
                int merchantId = _ServiceContext.CurrentUser.UserID;
                model = bll.GetFreightTemplate(merchantId, id.Value);
                if (model == null)
                    return RedirectToAction("index", "freighttemplates");
            }
            // 国家列表
            ViewData["CountryList"] = geographyBll.GetCountries();
            return View(model);
        }

        /// <summary>
        /// 新增、修改运费模板Post请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Create()
        {
            // 运费模板Json数据
            string freightJson = Request.Form["jsonData"];
            FreightJsonEntity freightEntity;
            try
            {
                freightEntity = Auxiliary.FromJsonTo<FreightJsonEntity>(freightJson);
                freightEntity.MerchantId = _ServiceContext.CurrentUser.UserID;
                if (!TryValidateModel(freightEntity))
                {
                    return "false";
                }
            }
            catch
            {
                return "false";
            }
            // 运费模板ID
            string tempID = freightEntity.Id;

            // 添加或修改模板名称的情况下进行重名校验
            if (string.IsNullOrEmpty(tempID) || (!string.IsNullOrEmpty(tempID) && freightEntity.Title != bll.GetName(Convert.ToInt32(tempID))))
            {
                // 校验模板名称是否可用（重名校验）
                if (!bll.CheckName(freightEntity.MerchantId, freightEntity.Title))
                    return "false||模板名称已存在！";
            }

            return bll.CreateOrUpdate(freightEntity).ToString().ToLower();
        }

        #endregion

        #region 删除运费模板

        /// <summary>
        /// 删除运费模板
        /// </summary>
        /// <param name="id">运费模板ID</param>
        /// <returns></returns>
        public string Delete(int id)
        {
            // 判断是否有商品正在使用该模板
            RentalGoodsBll goodsBll = new RentalGoodsBll();
            if (goodsBll.isUsing(id))
                return "false||部分商品正在使用该模板，删除失败！";

            return bll.Delete(id).ToString();
        }

        #endregion
    }
}
