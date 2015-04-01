using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.BLL.RentalGoodses;
using System.Web.Mvc;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.BLL.Categories;
using KYJ.ZS.Manager.Controllers.ActionFilters;
using KYJ.ZS.Models.Logs;
using KYJ.ZS.BLL.Logs;

namespace KYJ.ZS.Manager.Controllers
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-23
    /// Desc：出租商品管理控制器
    /// </summary>
    public class RentalGoodsController : BaseController
    {
        #region 成员及变量
        private readonly RentalGoodsBll bll = new RentalGoodsBll();
        #endregion

        /// <summary>
        /// 商品管理-商品列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Manage(int? pageIndex)
        {
            IList<RentalGoodsIndexEntity> goodsList = ParamsFactory(pageIndex, RentalGoodsAreaEnum.Manage);

            return View(goodsList);
        }

        /// <summary>
        /// 商品管理-违规商品
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViolationGoods(int? pageIndex)
        {
            string area = Request.QueryString["hdn_area"];
            RentalGoodsAreaEnum goodsAreaEnum = string.IsNullOrEmpty(area) ? RentalGoodsAreaEnum.Pending : (RentalGoodsAreaEnum)Auxiliary.ToInt32(area, 1);
            IList<RentalGoodsIndexEntity> goodsList = ParamsFactory(pageIndex, goodsAreaEnum);
            ViewData["area"] = goodsAreaEnum;

            return View(goodsList);
        }

        /// <summary>
        /// 商品审核
        /// </summary>
        /// <param name="modelMark">审核模块来源（1：管理商品，2：违规商品）</param>
        /// <param name="rentalGoodsId">商品ID</param>
        /// <param name="rentalGoodsName">商品名称</param>
        /// <param name="AuditOpinion">审核意见</param>
        /// <param name="isValidate">是否通过审核</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public string AuditRentalGoods(int modelMark, int rentalGoodsId, string rentalGoodsName, string AuditOpinion, bool isValidate)
        {
            if (!isValidate && string.IsNullOrEmpty(AuditOpinion))
                return "false||请填写违规原因！";

            string result = bll.Audited(rentalGoodsId, AuditOpinion, isValidate).ToString();
            if (result.ToUpper().IndexOf("TRUE") >= 0)
            {
                // 日志记录
                LogCreateEntity logEntity = new LogCreateEntity() { };
                logEntity.AdminId = _ServiceContext.CurrentUser.UserID;
                logEntity.Name = _ServiceContext.CurrentUser.LoginName;
                logEntity.RealName = _ServiceContext.CurrentUser.RealName;
                logEntity.ModuleId = modelMark == 1 ? (int)ManagerNavigation.SHANGPINLIEBIAO : (int)ManagerNavigation.WEIGUISHANGPIN;
                logEntity.ModuleName = modelMark == 1 ? "管理商品" : "违规商品";
                logEntity.Remark = isValidate ? "解除商品" + rentalGoodsName + "的违规信息" : "将商品" + rentalGoodsName + "违规下架";
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
        private IList<RentalGoodsIndexEntity> ParamsFactory(int? pageIndex, RentalGoodsAreaEnum goodsAreaEnum)
        {
            pageIndex = pageIndex.HasValue ? pageIndex : 1;
            int pageSize = 10;
            int totalRecord; //记录总条数
            int totalPage; //总页数

            string title = Request.QueryString["txt_title"]; //企业名称
            int firstCategoryId = Auxiliary.ToInt32(Request.QueryString["sel_firstCategory"], 0); //商品类别(一级类目)
            int secondCategoryId = Auxiliary.ToInt32(Request.QueryString["sel_secondCategory"], 0); //商品类别(二级类目)
            int thirdCategoryId = Auxiliary.ToInt32(Request.QueryString["sel_thirdCategory"], 0); //商品类别(三级类目)
            string merchantName = Request.QueryString["txt_merchantName"]; //商户名称
            string code = Request.QueryString["txt_code"]; //商家编号

            IList<RentalGoodsIndexEntity> goodsList = bll.GetRentalGoodsList(goodsAreaEnum, title, firstCategoryId, secondCategoryId, thirdCategoryId, merchantName, code, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            // 后一页无数据情况
            if ((goodsList == null || goodsList.Count <= 0) && pageIndex > 1)
            {
                pageIndex -= 1;
                goodsList = bll.GetRentalGoodsList(goodsAreaEnum, title, firstCategoryId, secondCategoryId, thirdCategoryId, merchantName, code, pageIndex.Value, pageSize, out totalRecord, out totalPage);
            }
            ViewData["merchantTitle"] = title;
            ViewData["merchantName"] = merchantName;
            ViewData["code"] = code;
            // 类目
            ViewData["firstCategory"] = firstCategoryId;
            ViewData["secondCategory"] = secondCategoryId;
            ViewData["thirdCategory"] = thirdCategoryId;
            // 分页数据
            ViewData["totalItemCount"] = totalRecord;
            ViewData["pageSize"] = pageSize;
            ViewData["pageIndex"] = pageIndex;

            return goodsList;
        }

        #endregion
    }
}
