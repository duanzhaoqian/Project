using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using KYJ.ZS.BLL.RentalGoodses;
using KYJ.ZS.BLL.TenancyTemplates;
using KYJ.ZS.Commons;
using KYJ.ZS.Merchant.Controllers.ActionFilters;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.Merchant.Controllers
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月4日15:19:02
    /// 描述：租期模板控制器
    /// </summary>
    public class TenancyTemplateController : BaseController
    {
        TenancyTemplateBll bll = new TenancyTemplateBll();
        /// <summary>
        /// 租期模板列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int? pageIndex)
        {
            if (pageIndex == null)
            {
                pageIndex = 1;
            }
            PageData<TenancyTemplate> list = bll.GetList(_ServiceContext.CurrentUser.UserID, (int)pageIndex);
            if (list.DataList.Count == 0 && pageIndex > 2)
            {
                return new RedirectResult(Url.MerchantSiteUrl().TenancyTemplate_Index + "?pageIndex=" + (pageIndex - 1));
            }
            return View(list);
        }
        #region 新增租期模板
        /// <summary>
        /// 新增租期模板视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        /// <summary>
        /// 新增租期模板
        /// </summary>
        /// <param name="tName">模板名称</param>
        /// <param name="tmonth">租期</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Insert(string tName, string tmonth)
        {
            BackMessge bm = new BackMessge();
            TenancyTemplate template = new TenancyTemplate();
            template.MerchantId = _ServiceContext.CurrentUser.UserID;
            template.TtempCreatetime = DateTime.Now;
            template.TtempTitle = tName;
            template.TtempMonths = tmonth;
            if (string.IsNullOrEmpty(template.TtempTitle) || string.IsNullOrEmpty(template.TtempMonths))
            {
                bm.Message = "请输入正确的数据";
            }
            else
            {
                template.TtempUpdatetime = DateTime.Now;
                bm.Success = bll.Insert(template);
                if (bm.Success)
                {
                    bm.Action = BackAction.Redirect;
                    bm.StrUrl = Url.MerchantSiteUrl().TenancyTemplate_Index;
                }
                else
                {
                    bm.Message = "添加失败，请重试！";
                }
            }
            return Json(bm);
        }

        #endregion
        #region 修改租期模板
        /// <summary>
        /// 修改租期模板视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpDate(int? tid)
        {
            //if (tid == null)
            //{
            //}
            TenancyTemplate template = bll.GetModel((int)tid,_ServiceContext.CurrentUser.UserID);
            //TenancyTemplateUpDateView templateUpDateView;
            //templateUpDateView = (TenancyTemplateUpDateView)template;
            return View(template);
        }
        /// <summary>
        /// 修改租期模板
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="tName"></param>
        /// <param name="tmonth"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpDate(int tid, string tName, string tmonth)
        {
            TenancyTemplate template = new TenancyTemplate();
            template.TtempId = tid;
            template.TtempTitle = tName;
            template.TtempMonths = tmonth;
            BackMessge bm = new BackMessge();
            bm.Success = bll.Update(template,_ServiceContext.CurrentUser.UserID);
            if (bm.Success)
            {
                bm.Action = BackAction.Redirect;
                bm.StrUrl = Url.MerchantSiteUrl().TenancyTemplate_Index;
            }
            else
            {
                bm.Message = "更新失败，请重试!";
            }
            return Json(bm);
        }
        #endregion

        /// <summary>
        /// 删除租期模板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Delete(int? tid, int? pageIndex)
        {
            BackMessge bm = new BackMessge();
            if (tid == null)
            {
                bm.Message = "参数错误！";
            }
            else
            {
                RentalGoodsBll rentalbll = new RentalGoodsBll();
                if (rentalbll.GetUsedTemplate((int)tid))
                {
                    bm.Message = "该租期模板被使用中，不能被删除！";
                }
                else
                {
                    bm.Success = bll.Delete((int)tid,_ServiceContext.CurrentUser.UserID);
                    if (bm.Success)
                    {
                        bm.Action = BackAction.Redirect;
                        bm.StrUrl = Url.MerchantSiteUrl().TenancyTemplate_Index+"?pageIndex="+pageIndex;
                    }
                }
            }
            return Json(bm);
        }
    }
}
