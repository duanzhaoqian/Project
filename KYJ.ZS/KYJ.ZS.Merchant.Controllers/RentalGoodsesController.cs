using System.Web.Mvc;
using KYJ.ZS.BLL.RentalGoodses;
using KYJ.ZS.Commons.PictureModular;
using KYJ.ZS.Merchant.Controllers.ActionFilters;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.View;

namespace KYJ.ZS.Merchant.Controllers
{
    using System;
    using System.Collections.Generic;
    using KYJ.ZS.Commons;
    using KYJ.ZS.Models.RentalGoodses;

    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月28日09:13:03
    /// 描述：出租商品控制器
    /// </summary>
    public class RentalGoodsesController : BaseController
    {
        RentalGoodsBll bll = new RentalGoodsBll();
        [HttpGet]
        public ActionResult Index(RentalGoodsesQueryPms pms)
        {
            RentalGoodsIndexView<WareHouseGoodsEntity> viewModel = new RentalGoodsIndexView<WareHouseGoodsEntity>();
            if (pms.PageIndex == null)
            {
                pms.PageIndex = 1;
            }
            viewModel.PageData = bll.GetList(_ServiceContext.CurrentUser.UserID, pms);
            if (viewModel.PageData.DataList.Count == 0 && pms.PageIndex > 2)
            {
                pms.PageIndex = pms.PageIndex - 1;
                return new RedirectResult(Url.Action("Index", pms));
            }
            viewModel.QueryPms = pms;
            return View(viewModel);
        }
        /// <summary>
        /// 商品上架
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public ActionResult Shelves(string idList)
        {
            BackMessge bm = new BackMessge();
            string message;
            if (string.IsNullOrEmpty(idList))
            {
                bm.Message = "参数无效！";
            }
            else
            {
                bm.Success = bll.Shelf(idList, out message);
                bm.Message = message;
            }
            return Json(bm);
        }

        /// <summary>
        /// 获取商品图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsPic(string guid)
        {
            var pic = GetPicture.GetFirstGoodsPicture(guid, true, "SHOW");
            if (string.IsNullOrEmpty(pic))
            {
                pic = Url.MerchantSiteUrl().RentalGoodses_DefultPic;
            }
            else
            {
                pic = pic + ".60_60.jpg";
            }
            return Json(new {guid=guid,url=pic},JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public ActionResult ShelvesOff(string idList)
        {
            BackMessge bm = new BackMessge();
            string message;
            if (string.IsNullOrEmpty(idList))
            {
                bm.Message = "参数无效！";
            }
            else
            {
                bm.Success = bll.ShelfOff(idList, out message);
                bm.Message = message;
            }
            return Json(bm);
        }
        /// <summary>
        /// 商品软删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public ActionResult Delete(string idList)
        {
            BackMessge bm = new BackMessge();
            string message;
            if (string.IsNullOrEmpty(idList))
            {
                bm.Message = "参数无效！";
            }
            else
            {
                bm.Success = bll.DeleteList(idList, out message);
                bm.Message = message;
            }
            return Json(bm);
        }

        /// <summary>
        /// 违规商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IllegalGoods(RentalGoodsesQueryPms pms)
        {
            if (pms.PageIndex == null)
            {
                pms.PageIndex = 1;
            }
            RentalGoodsIndexView<RentalGoodsIllegalGoodsEntity> viewModel = new RentalGoodsIndexView<RentalGoodsIllegalGoodsEntity>();
            viewModel.PageData = bll.GetIiiegalGoods(_ServiceContext.CurrentUser.UserID, pms);
            viewModel.QueryPms = pms;
            return View(viewModel);
        }

        /// <summary>
        /// 作者：wangyu
        /// 时间：2014/4/29 9:47:50
        /// 描述：发布商品（租）
        /// </summary>
        [HttpGet]
        public ActionResult PublishGoods()
        {
            try
            {
                var categoryId = Request["id"].ToInt();
                var curentUserId = _ServiceContext.CurrentUser.UserID;

                if (categoryId < 1)
                    return null;
                var cab = new BLL.Categories.CategoryAttributeBll();

                ViewBag.CategoryId = categoryId;
                ViewData["CategoryAttr"] = cab.GetCategoryAttrName(categoryId);
                ViewData["CategoryAttrValue"] = cab.GetCategoryAttrValue(categoryId); ;

                ViewData["CategoryColor"] = new BLL.Categories.CategoryColorBll().GetCategoryColor(categoryId);
                ViewData["CategoryNorm"] = new BLL.Categories.CategoryNormBll().GetCategoryNorm(categoryId);

                ViewData["TenancyTemplate"] = new BLL.TenancyTemplates.TenancyTemplateBll().GetList(curentUserId, 1);
                ViewData["FreightTemplate"] = new BLL.FreightTemplates.FreightTemplateBll().GetFreightTemplateList(curentUserId);
                ViewData["CategoryBrands"] = new BLL.Brands.BrandBll().GetBrandsByCatId(categoryId);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "发布商品（租）", ex);
                return View("Error");
            }


            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePublishGoods(string guid, FormCollection collection)
        {
            try
            {
                var categoryId = Request["categoryId"].ToInt();
                //判断类目下是否存在属性、颜色、规格。存在为1
                var isHasAttr = Request["isHasAttr"].ToInt() > 0;
                var isHasColor = Request["isHasColor"].ToInt() > 0;
                var isHasNorm = Request["isHasNorm"].ToInt() > 0;

                string[] attrList = new string[] { }, colorList = new string[] { },
                        normList = new string[] { }, goodsPriceList = new string[] { };

                var reqAttr = Request["hideAttr"];
                var reqColor = Request["colorList"];
                var reqNorm = Request["normList"];
                var reqGoodsPrice = Request["hide_pricelist"];

                var reqTitle = Request["Title"];
                var reqNumber = Request["Number"].ToInt();
                var reqCode = Request["Code"];
                var reqBarcode = Request["Barcode"];
                var reqDesc = collection["Desc"];
                var reqPrice = Request["Price"].ToDouble();
                var reqMonthPrice = Request["MonthPrice"].ToDouble();
                var reqDeposit = Request["Deposit"].ToDouble();
                var reqLateFees = Request["LateFees"].ToDouble();
                var reqTtempId = Request["TtempId"].ToInt();
                var reqFtempId = Request["FtempId"].ToInt();
                var reqMode = Request["Mode"].ToInt();
                var reqValidity = Request["Validity"].ToInt();
                var reqWeight = Request["Weight"].ToDouble();
                var reqVolume = Request["Volume"].ToDouble();
                var reqTags = string.Empty;

                var reqDate = Request["DateTime_Date"];
                var reqHour = Request["DateTime_Hour"];
                var reqMinute = Request["DateTime_Minute"];

                DateTime? reqBeginTime = null, reqEndTime = null;
                int reqState = 2, reqStatus = 2;

                if (reqMode > 0)//储存方式：0 未知，1 立刻，2 放入仓库，3 自定义时间
                {
                    var now = DateTime.Now;
                    switch (reqMode)
                    {
                        case 1:
                            reqBeginTime = now;
                            reqStatus = 1;//显示状态：0 未知，1上架，2下架
                            reqState = 2;//审核状态：0未知，1待审核，2 通过，3未通过
                            break;
                        case 2:
                            reqStatus = 2;
                            reqState = 2;
                            break;
                        case 3:
                            if (!reqDate.IsNullOrEmpty())
                                reqBeginTime = DateTime.Parse(reqDate.ToDateTime(now).ToString("yyyy/MM/dd") + " " + reqHour + ":" + reqMinute);
                            break;
                    }
                }
                if (reqBeginTime.HasValue && reqValidity > 0)//有效期：0 未知，1（1周），2 (1个月)
                {
                    switch (reqValidity)
                    {
                        case 1:
                            reqEndTime = reqBeginTime.Value.AddDays(7);
                            break;
                        case 2:
                            reqEndTime = reqBeginTime.Value.AddMonths(1);
                            break;
                    }
                }

                //存在且不为空则以“，”号拆分到数组
                if (isHasAttr && !reqAttr.IsNullOrEmpty())
                    attrList = reqAttr.Split(',');
                if (isHasColor && !reqColor.IsNullOrEmpty())
                    colorList = reqColor.Split(',');
                if (isHasNorm && !reqNorm.IsNullOrEmpty())
                    normList = reqNorm.Split(',');
                if ((isHasColor && !reqColor.IsNullOrEmpty()) && (isHasNorm && !reqNorm.IsNullOrEmpty()))
                    goodsPriceList = reqGoodsPrice.Split(',');

                var brandId = 0;
                var brandName = string.Empty;
                if (!Request["brands"].IsNullOrEmpty())
                {
                    var brand = Request["brands"].Split(';');
                    brandId = brand[1].ToInt();
                    brandName = brand[2];
                }

                var rentalGoodsBll = new BLL.RentalGoodses.RentalGoodsBll();

                var entity = new RentalGoodsEntity();
                entity.RentalGoods = new RentalGoods()
                                         {
                                             MerchantId = _ServiceContext.CurrentUser.UserID,
                                             Title = reqTitle,
                                             Number = reqNumber,
                                             Code = reqCode,
                                             Barcode = reqBarcode,
                                             Price = reqPrice,
                                             MonthPrice = reqMonthPrice,
                                             Deposit = reqDeposit,
                                             LateFees = reqLateFees,
                                             TtempId = reqTtempId,
                                             FtempId = reqFtempId,
                                             Mode = reqMode,
                                             Validity = reqValidity,
                                             State = reqState,
                                             Status = reqStatus,
                                             BeginTime = reqBeginTime,
                                             EndTime = reqEndTime,
                                             CreateTime = DateTime.Now,
                                             UpdateTime = DateTime.Now,
                                             ProvinceName = string.Empty,
                                             CityName = string.Empty,
                                             DistrictName = string.Empty,
                                             Guid = guid,
                                             AdminName = string.Empty,
                                             Tags = reqTags,
                                             BrandId = brandId,
                                             BrandName = brandName,
                                             CategoryId = categoryId,
                                             Weight = reqWeight,
                                             Volume = reqVolume
                                         };

                if (entity.RentalGoods.FtempId > 0)
                {
                    var ft = new BLL.FreightTemplates.FreightTemplateBll().GetFreightTemplate(entity.RentalGoods.FtempId);
                    entity.RentalGoods.ProvinceId = ft.FreightTemplate.ProvinceId;
                    entity.RentalGoods.ProvinceName = ft.FreightTemplate.ProvinceName;
                    entity.RentalGoods.CityId = ft.FreightTemplate.CityId;
                    entity.RentalGoods.CityName = ft.FreightTemplate.CityName;
                    entity.RentalGoods.DistrictId = ft.FreightTemplate.DistrictId;
                    entity.RentalGoods.DistrictName = ft.FreightTemplate.DistrictName;
                }
                else
                {
                    entity.RentalGoods.ProvinceId = 0;
                    entity.RentalGoods.ProvinceName = string.Empty;
                    entity.RentalGoods.CityId = 0;
                    entity.RentalGoods.CityName = string.Empty;
                    entity.RentalGoods.DistrictId = 0;
                    entity.RentalGoods.DistrictName = string.Empty;
                }

                entity.RentalOther = new RentalOther()
                                         {
                                             Descs = reqDesc,
                                             Attrs = string.Empty,
                                             Colors = string.Empty,
                                             Norms = string.Empty,
                                             Prices = string.Empty
                                         };

                if (attrList.Length > 0)
                {
                    entity.GoodsAttribute = new List<GoodsAttribute>();

                    foreach (var item in attrList)
                    {
                        var it = item.Split(';');
                        var goodsAttr = new GoodsAttribute
                                            {
                                                GoodsAttrType = 1,
                                                AttrValId = it[1].ToInt(),
                                                GoodsAttrVal = it[2].ToString(),
                                                CategoryAttrId = it[3].ToInt()
                                            };
                        entity.GoodsAttribute.Add(goodsAttr);

                    }
                }

                if (colorList.Length > 0)
                {
                    entity.GoodsColor = new List<GoodsColor>();

                    foreach (var item in colorList)
                    {
                        var it = item.Split(';');
                        var goodsColor = new GoodsColor
                                             {
                                                 ColorId = it[0].ToInt(),
                                                 ColorName = it[1].ToString(),
                                                 ColorCode = it[2].ToString(),
                                                 ColorType = 1
                                             };

                        entity.GoodsColor.Add(goodsColor);

                    }
                }

                if (normList.Length > 0)
                {
                    entity.GoodsNorm = new List<GoodsNorm>();

                    foreach (var item in normList)
                    {
                        var it = item.Split(';');
                        var goodsNorm = new GoodsNorm
                                            {
                                                NormId = it[0].ToInt(),
                                                NormName = it[1].ToString(),
                                                NormType = 1
                                            };

                        entity.GoodsNorm.Add(goodsNorm);
                    }
                }

                if (goodsPriceList.Length > 0)
                {
                    entity.GoodsPrice = new List<GoodsPrice>();

                    foreach (var item in goodsPriceList)
                    {
                        var it = item.Split(';');
                        var goodsPrice = new GoodsPrice
                                             {
                                                 ColorId = it[0].ToInt(),
                                                 NormId = it[2].ToInt(),
                                                 Price = it[4].ToDouble(),
                                                 Number = it[5].ToInt(),
                                                 Code = it[6].ToString(),
                                                 Barcode = it[7].ToString()
                                             };

                        entity.GoodsPrice.Add(goodsPrice);
                    }
                }

                if (!TryValidateModel(entity.RentalGoods) || !TryValidateModel(entity.RentalOther))
                {
                    return Json(new { result = false, message = "数据未通过验证" });
                }

                if (!rentalGoodsBll.PublishGoods(entity))
                    return Json(new { result = false, message = "操作失败" });

                return Json(new { result = true, message = "操作成功" });
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "发布商品保存（租）", ex);
                return Json(new { result = false, message = "发生错误" });

            }

        }

        /// <summary>
        /// 作者：wangyu
        /// 时间：2014/5/6 9:47:50
        /// 描述：修改商品（租）
        /// </summary>
        [HttpGet]
        public ActionResult UpdateGoods(int id)
        {
            RentalGoodsEntity model = null;
            try
            {
                var goodsId = Request["id"].ToInt();
                var curentUserId = _ServiceContext.CurrentUser.UserID;

                if (goodsId < 1)
                    return null;
                var cab = new BLL.Categories.CategoryAttributeBll();

                model = bll.GetRentalGoods(goodsId);
                if (model.RentalGoods.MerchantId != curentUserId)
                    return View("Error");

                var categoryId = model.RentalGoods.CategoryId;

                ViewData["CategoryAttr"] = cab.GetCategoryAttrName(categoryId);
                ViewData["CategoryAttrValue"] = cab.GetCategoryAttrValue(categoryId); ;

                ViewData["CategoryColor"] = new BLL.Categories.CategoryColorBll().GetCategoryColor(categoryId);
                ViewData["CategoryNorm"] = new BLL.Categories.CategoryNormBll().GetCategoryNorm(categoryId);

                ViewData["TenancyTemplate"] = new BLL.TenancyTemplates.TenancyTemplateBll().GetList(curentUserId, 1);
                ViewData["FreightTemplate"] = new BLL.FreightTemplates.FreightTemplateBll().GetFreightTemplateList(curentUserId);
                ViewData["CategoryBrands"] = new BLL.Brands.BrandBll().GetBrandsByCatId(categoryId);
                
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "修改商品（租）", ex);
                return View("Error");
            }
            return View(model);
            
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveUpdateGoods(FormCollection collection)
        {
            try
            {
                var id = Request["rentalGoodsId"].ToInt();
                //判断类目下是否存在属性、颜色、规格。存在为1
                var isHasAttr = Request["isHasAttr"].ToInt() > 0;
                var isHasColor = Request["isHasColor"].ToInt() > 0;
                var isHasNorm = Request["isHasNorm"].ToInt() > 0;

                string[] attrList = new string[] { }, colorList = new string[] { },
                        normList = new string[] { }, goodsPriceList = new string[] { };

                var reqAttr = Request["hideAttr"];
                var reqColor = Request["colorList"];
                var reqNorm = Request["normList"];
                var reqGoodsPrice = Request["hide_pricelist"];

                var reqTitle = Request["Title"];
                var reqNumber = Request["Number"].ToInt();
                var reqCode = Request["Code"];
                var reqBarcode = Request["Barcode"];
                var reqDesc = collection["Desc"];
                var reqPrice = Request["Price"].ToDouble();
                var reqMonthPrice = Request["MonthPrice"].ToDouble();
                var reqDeposit = Request["Deposit"].ToDouble();
                var reqLateFees = Request["LateFees"].ToDouble();
                var reqTtempId = Request["TtempId"].ToInt();
                var reqFtempId = Request["FtempId"].ToInt();
                var reqMode = Request["Mode"].ToInt();
                var reqValidity = Request["Validity"].ToInt();
                var reqTags = string.Empty;
                var reqWeight = Request["Weight"].ToDouble();
                var reqVolume = Request["Volume"].ToDouble();
                var reqDate = Request["DateTime_Date"];
                var reqHour = Request["DateTime_Hour"];
                var reqMinute = Request["DateTime_Minute"];

                DateTime? reqBeginTime = null, reqEndTime = null;
                int reqState = 2, reqStatus = 2;

                if (reqMode > 0)//储存方式：0 未知，1 立刻，2 放入仓库，3 自定义时间
                {
                    var now = DateTime.Now;
                    switch (reqMode)
                    {
                        case 1:
                            reqBeginTime = now;
                            reqStatus = 1;//显示状态：0 未知，1上架，2下架
                            reqState = 2;//审核状态：0未知，1待审核，2 通过，3未通过
                            break;
                        case 2:
                            reqStatus = 2;
                            reqState = 2;
                            break;
                        case 3:
                            reqStatus = 2;
                            reqState = 2;
                            if (!reqDate.IsNullOrEmpty())
                                reqBeginTime = DateTime.Parse(reqDate.ToDateTime(now).ToString("yyyy/MM/dd") + " " + reqHour + ":" + reqMinute);
                            break;
                    }
                }
                if (reqBeginTime.HasValue && reqValidity > 0)//有效期：0 未知，1（1周），2 (1个月)
                {
                    switch (reqValidity)
                    {
                        case 1:
                            reqEndTime = reqBeginTime.Value.AddDays(7);
                            break;
                        case 2:
                            reqEndTime = reqBeginTime.Value.AddMonths(1);
                            break;
                    }
                }

                //存在且不为空则以“，”号拆分到数组
                if (isHasAttr && !reqAttr.IsNullOrEmpty())
                    attrList = reqAttr.Split(',');
                if (isHasColor && !reqColor.IsNullOrEmpty())
                    colorList = reqColor.Split(',');
                if (isHasNorm && !reqNorm.IsNullOrEmpty())
                    normList = reqNorm.Split(',');
                if ((isHasColor && !reqColor.IsNullOrEmpty()) && (isHasNorm && !reqNorm.IsNullOrEmpty()))
                    goodsPriceList = reqGoodsPrice.Split(',');

                var brandId = 0;
                var brandName = string.Empty;
                if (!Request["brands"].IsNullOrEmpty())
                {
                    var brand = Request["brands"].Split(';');
                    brandId = brand[1].ToInt();
                    brandName = brand[2];
                }

                var rentalGoodsBll = new BLL.RentalGoodses.RentalGoodsBll();

                var entity = rentalGoodsBll.GetRentalGoods(id);

                if (entity.RentalGoods.MerchantId != _ServiceContext.CurrentUser.UserID)
                    return Json(new { result = false, message = "操作失败，访问权限" });

                entity.RentalGoods.MerchantId = _ServiceContext.CurrentUser.UserID;
                entity.RentalGoods.Title = reqTitle;
                entity.RentalGoods.Number = reqNumber;
                entity.RentalGoods.Code = reqCode;
                entity.RentalGoods.Barcode = reqBarcode;
                entity.RentalGoods.Price = reqPrice;
                entity.RentalGoods.MonthPrice = reqMonthPrice;
                entity.RentalGoods.Deposit = reqDeposit;
                entity.RentalGoods.LateFees = reqLateFees;
                entity.RentalGoods.TtempId = reqTtempId;
                entity.RentalGoods.FtempId = reqFtempId;
                entity.RentalGoods.Mode = reqMode;
                entity.RentalGoods.Validity = reqValidity;
                entity.RentalGoods.BeginTime = reqBeginTime;
                entity.RentalGoods.EndTime = reqEndTime;
                entity.RentalGoods.UpdateTime = DateTime.Now;
                entity.RentalGoods.AdminName = string.Empty;
                entity.RentalGoods.Tags = reqTags;
                entity.RentalGoods.BrandId = brandId;
                entity.RentalGoods.BrandName = brandName;
                entity.RentalGoods.Status = reqStatus;
                entity.RentalGoods.Weight = reqWeight;
                entity.RentalGoods.Volume = reqVolume;
                if (entity.RentalGoods.State == 3 || entity.RentalGoods.State == 1)
                {
                    entity.RentalGoods.State = 1;
                    entity.RentalGoods.Status = 2;
                }
                else
                {
                    entity.RentalGoods.State = reqState;
                }

                entity.RentalOther.Descs = reqDesc;
                entity.RentalOther.Attrs = string.Empty;
                entity.RentalOther.Colors = string.Empty;
                entity.RentalOther.Norms = string.Empty;
                entity.RentalOther.Prices = string.Empty;

                if (entity.RentalGoods.FtempId > 0)
                {
                    var ft = new BLL.FreightTemplates.FreightTemplateBll().GetFreightTemplate(entity.RentalGoods.FtempId);
                    entity.RentalGoods.ProvinceId = ft.FreightTemplate.ProvinceId;
                    entity.RentalGoods.ProvinceName = ft.FreightTemplate.ProvinceName;
                    entity.RentalGoods.CityId = ft.FreightTemplate.CityId;
                    entity.RentalGoods.CityName = ft.FreightTemplate.CityName;
                    entity.RentalGoods.DistrictId = ft.FreightTemplate.DistrictId;
                    entity.RentalGoods.DistrictName = ft.FreightTemplate.DistrictName;
                }
                else
                {
                    entity.RentalGoods.ProvinceId = 0;
                    entity.RentalGoods.ProvinceName = string.Empty;
                    entity.RentalGoods.CityId = 0;
                    entity.RentalGoods.CityName = string.Empty;
                    entity.RentalGoods.DistrictId = 0;
                    entity.RentalGoods.DistrictName = string.Empty;
                }

                if (attrList.Length > 0)
                {
                    entity.GoodsAttribute = new List<GoodsAttribute>();

                    foreach (var item in attrList)
                    {
                        var it = item.Split(';');
                        var goodsAttr = new GoodsAttribute
                        {
                            GoodsAttrType = 1,
                            AttrValId = it[1].ToInt(),
                            GoodsAttrVal = it[2].ToString(),
                            CategoryAttrId = it[3].ToInt()
                        };
                        entity.GoodsAttribute.Add(goodsAttr);

                    }
                }

                if (colorList.Length > 0)
                {
                    entity.GoodsColor = new List<GoodsColor>();

                    foreach (var item in colorList)
                    {
                        var it = item.Split(';');
                        var goodsColor = new GoodsColor
                                             {
                                                 ColorId = it[0].ToInt(),
                                                 ColorName = it[1].ToString(),
                                                 ColorCode = it[2].ToString(),
                                                 ColorType = 1,
                                                 Id = it[3].ToInt()
                                             };

                        entity.GoodsColor.Add(goodsColor);

                    }
                }
                else
                {
                    entity.GoodsColor = null;
                }

                if (normList.Length > 0)
                {
                    entity.GoodsNorm = new List<GoodsNorm>();

                    foreach (var item in normList)
                    {
                        var it = item.Split(';');
                        var goodsNorm = new GoodsNorm
                                            {
                                                NormId = it[0].ToInt(),
                                                NormName = it[1].ToString(),
                                                NormType = 1,
                                                Id = it[2].ToInt()
                                            };

                        entity.GoodsNorm.Add(goodsNorm);
                    }
                }
                else
                {
                    entity.GoodsNorm = null;
                }

                if (goodsPriceList.Length > 0)
                {
                    entity.GoodsPrice = new List<GoodsPrice>();

                    foreach (var item in goodsPriceList)
                    {
                        var it = item.Split(';');
                        var goodsPrice = new GoodsPrice
                                             {
                                                 ColorId = it[0].ToInt(),
                                                 NormId = it[2].ToInt(),
                                                 Price = it[4].ToDouble(),
                                                 Number = it[5].ToInt(),
                                                 Code = it[6].ToString(),
                                                 Barcode = it[7].ToString()
                                             };

                        entity.GoodsPrice.Add(goodsPrice);
                    }
                }
                else
                {
                    entity.GoodsPrice = null;
                }

                if (!TryValidateModel(entity.RentalGoods) || !TryValidateModel(entity.RentalOther))
                {
                    return Json(new { result = false, message = "数据未通过验证" });
                }

                if (!rentalGoodsBll.UpdateGoods(entity))
                    return Json(new { result = false, message = "操作失败" });

                return Json(new { result = true, message = "操作成功" });
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("wangyu", "修改商品保存（租）", ex);
                return Json(new { result = false, message = "发生错误" });
            }

        }

    }
}
