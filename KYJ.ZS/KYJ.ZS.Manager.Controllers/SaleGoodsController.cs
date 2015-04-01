using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.BLL.SaleGoodses;
using System.Web.Mvc;
using KYJ.ZS.Models.SaleGoodses;
using KYJ.ZS.Commons;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-29
    /// Desc：闲置商品管理控制器
    /// </summary>
    public class SaleGoodsController : BaseController
    {
        #region 成员及变量
        private readonly SaleGoodsBll bll = new SaleGoodsBll();
        #endregion

        /// <summary>
        /// 闲置物品信息管理-管理信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Manage(int? pageIndex)
        {
            IList<SaleGoodsIndexEntity> goodsList = ParamsFactory(pageIndex, SaleGoodsAreaEnum.Manage);
            return View(goodsList);
        }

        /// <summary>
        /// 闲置物品信息管理-违规信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViolationGoods(int? pageIndex)
        {
            string area = Request.QueryString["hdn_area"];
            SaleGoodsAreaEnum goodsAreaEnum = string.IsNullOrEmpty(area) ? SaleGoodsAreaEnum.Pending : (SaleGoodsAreaEnum)Auxiliary.ToInt32(area, 1);
            IList<SaleGoodsIndexEntity> goodsList = ParamsFactory(pageIndex, goodsAreaEnum);
            ViewData["area"] = goodsAreaEnum;
            return View(goodsList);
        }

        /// <summary>
        /// 商品审核
        /// </summary>
        /// <param name="modelMark">审核模块来源（1：管理信息，2：违规信息）</param>
        /// <param name="saleGoodsId">商品ID</param>
        /// <param name="saleGoodsName">商品名称</param>
        /// <param name="AuditOpinion">审核意见</param>
        /// <param name="isValidate">是否通过审核</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public string AuditRentalGoods(int modelMark, int saleGoodsId, string saleGoodsName, string AuditOpinion, bool isValidate)
        {
            if (!isValidate && string.IsNullOrEmpty(AuditOpinion))
                return "false||请填写违规原因！";
            string result = bll.Audited(saleGoodsId, AuditOpinion, isValidate).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = modelMark == 1 ? (int)ManagerNavigation.GUANLIXINXI : (int)ManagerNavigation.WEIGUIXINXI;
                logEntity.ModuleName = modelMark == 1 ? "管理信息" : "违规信息";
                logEntity.Remark = isValidate ? "解除商品" + saleGoodsName + "的违规信息" : "将商品" + saleGoodsName + "违规下架";
                new LogBll().CreateLog(logEntity);
            }

            return result;
        }



        #region 私有方法

        /// <summary>
        /// 返回商品管理Entity及页面所需ViewData
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="goodsAreaEnum">商品所在区域枚举</param>
        private IList<SaleGoodsIndexEntity> ParamsFactory(int? pageIndex, SaleGoodsAreaEnum goodsAreaEnum)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            string title = Request.QueryString["txt_title"]; //信息名称
            string userLoginName = Request.QueryString["txt_userLoginName"]; //用户账号
            string number = Request.QueryString["txt_number"]; //信息编号
            // 发布时间
            string beginTimeStr = Request.QueryString["txt_beginTime"];
            DateTime? beginTime = string.IsNullOrEmpty(beginTimeStr) ? null : (DateTime?)Auxiliary.ToDateTime(beginTimeStr, DateTime.Now.AddDays(-7));
            string endTimeStr = Request.QueryString["txt_endTime"];
            DateTime? endTime = string.IsNullOrEmpty(endTimeStr) ? null : (DateTime?)Auxiliary.ToDateTime(endTimeStr, DateTime.Now);

            IList<SaleGoodsIndexEntity> goodsList = bll.GetManageList(goodsAreaEnum, userLoginName, title, number, beginTime, endTime, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((goodsList == null || goodsList.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                goodsList = bll.GetManageList(goodsAreaEnum, userLoginName, title, number, beginTime, endTime, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            ViewData["goodsTitle"] = title;
            ViewData["userLoginName"] = userLoginName;
            ViewData["number"] = number;
            ViewData["beginTime"] = beginTime;
            ViewData["endTime"] = endTime;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return goodsList;
        }

        #endregion
    }
}
