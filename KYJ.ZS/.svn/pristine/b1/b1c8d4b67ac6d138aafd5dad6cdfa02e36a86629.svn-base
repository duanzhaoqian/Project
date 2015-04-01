using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.User.Controllers
{
    using System.Web.Mvc;

    using KYJ.ZS.BLL.SaleGoodses;
    using KYJ.ZS.Commons;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Models.SaleGoodses;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/25 13:18:03
    /// 描述：
    /// </summary>
    public class SaleGoodsController : BaseController
    {
        private readonly SaleGoodsBll bll;
        public SaleGoodsController()
        {
            if (bll == null) bll = new SaleGoodsBll();
        }

        public ActionResult PublishSaleGoods()
        {
            try
            {
                int userId = _ServiceContext.CurrentUser.UserID;
                int papersStatus = bll.GetPapersstatusByUserId(userId);
                //认证状态：0未知  1未认证  2认证中  3认证未通过  4认证通过
                if (papersStatus == 4)
                {
                    //通过认证
                    ViewData["Province"] = new KYJ.ZS.BLL.Geographies.GeographyBll().GetProvinces();
                    ViewData["Tags"] = new KYJ.ZS.BLL.Tags.TagBll().GetsTagsByType(2);//标签类型：0 未知，1 租，2 售
                    return View();
                }
                else
                {
                    string url = Url.UserSiteUrl().Index;
                    return Content("<script >alert('亲，认证未通过，不能发布信息^_^');window.location='" + url + "';</script >", "text/html");
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "发布商品（售）", ex);
                return View("Error");
            }
        }

        /// <summary>
        /// 发布出售的商品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePublishSaleGoods(string guid, FormCollection collection)
        {
            try
            {
                var model = new SaleGoodsEntity();

                model.SaleGoods = new SaleGoods
                                      {
                                          UserId = this._ServiceContext.CurrentUser.UserID,
                                          ProvinceId = this.Request["ProvinceId"].ToInt(),
                                          ProvinceName = this.Request["ProvinceName"].ToString(),
                                          CityId = this.Request["CityId"].ToInt(),
                                          CityName = this.Request["CityName"].ToString(),
                                          DistrictId = this.Request["DistrictId"].ToInt(),
                                          DistrictName = this.Request["DistrictName"].ToString(),
                                          Title = this.Request["Title"].ToString(),
                                          Degree = this.Request["Degree"].ToInt(),
                                          Price = this.Request["Price"].ToDouble(),
                                          Status = 1,
                                          State = 2,
                                          CreateTime = this.Request["CreateTime"].ToDateTime(DateTime.Now),
                                          UpdateTime = DateTime.Now,
                                          Guid = guid.IsNullOrEmpty() ? Guid.Empty.ToString() : guid,
                                          AdminName = string.Empty,
                                          Contact = this.Request["Contact"].ToString(),
                                          ContactPhone = this.Request["ContactPhone"].ToString(),
                                          IsBargain = this.Request["IsBargain"].IsNullOrEmpty() ? false : true,
                                          Tag = this.Request["Tag"].ToString(),
                                      };
                model.SaleOther = new SaleOther { Descs = collection["Desc"].ToString() };

                if (!TryValidateModel(model.SaleGoods) || !TryValidateModel(model.SaleOther))
                {
                    return Json(new { result = false, message = "数据未通过验证" });
                }

                if (!bll.AddSaleGoods(model))
                    return Json(new { result = false, message = "操作失败" });

                return Json(new { result = true, message = "操作成功" });
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "发布商品保存（售）", ex);
                return Json(new { result = false, message = "发生错误" });
            }

        }

        public ActionResult UpdateSaleGoods()
        {
            try
            {
                var id = Request["Id"].ToInt();

                ViewData["Tags"] = new KYJ.ZS.BLL.Tags.TagBll().GetsTagsByType(2);//标签类型：0 未知，1 租，2 售

                var saleGoods = bll.GetSaleGoods(id);

                if (saleGoods.SaleGoods.UserId != _ServiceContext.CurrentUser.UserID)
                    return View("Error");

                return View(saleGoods);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "修改商品（售）", ex);
                return View("Error");
            }
        }
        /// <summary>
        /// 修改出售的商品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveUpdateSaleGoods(FormCollection collection)
        {
            try
            {
                var id = Request["Id"].ToInt();

                if (id < 1) return Json(new { result = false, message = "参数错误" });

                var saleGoods = bll.GetSaleGoods(id);

                if (saleGoods.SaleGoods.UserId != _ServiceContext.CurrentUser.UserID)
                    return Json(new { result = false, message = "操作失败，访问权限" });

                saleGoods.SaleGoods.ProvinceId = Request["ProvinceId"].ToInt();
                saleGoods.SaleGoods.ProvinceName = Request["ProvinceName"].ToString();
                saleGoods.SaleGoods.CityId = Request["CityId"].ToInt();
                saleGoods.SaleGoods.CityName = Request["CityName"].ToString();
                saleGoods.SaleGoods.DistrictId = Request["DistrictId"].ToInt();
                saleGoods.SaleGoods.DistrictName = Request["DistrictName"].ToString();
                saleGoods.SaleGoods.Title = Request["Title"].ToString();
                saleGoods.SaleGoods.Degree = Request["Degree"].ToInt();
                saleGoods.SaleGoods.Price = Request["Price"].ToDouble();
                saleGoods.SaleGoods.Guid = Request["Guid"].ToGuid().ToString();
                if (saleGoods.SaleGoods.State == 3)
                {
                    saleGoods.SaleGoods.State = 1;
                }

                saleGoods.SaleGoods.UpdateTime = Request["UpdateTime"].ToDateTime(DateTime.Now);
                saleGoods.SaleGoods.AdminName = string.Empty;
                saleGoods.SaleGoods.Contact = this.Request["Contact"].ToString();
                saleGoods.SaleGoods.ContactPhone = this.Request["ContactPhone"].ToString();
                saleGoods.SaleGoods.IsBargain = this.Request["IsBargain"].IsNullOrEmpty() ? false : true;
                saleGoods.SaleGoods.Tag = this.Request["Tag"].ToString();

                saleGoods.SaleOther.Descs = collection["Desc"].ToString();


                if (!TryValidateModel(saleGoods.SaleGoods) || !TryValidateModel(saleGoods.SaleOther))
                {
                    return Json(new { result = false, message = "数据未通过验证" });
                }

                if (!bll.UpdateSaleGoods(saleGoods))
                    return Json(new { result = false, message = "操作失败" });

                return Json(new { result = true, message = "操作成功" });
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "修改商品保存（售）", ex);
                return Json(new { result = false, message = "发生错误" });
            }

        }
    }
}
