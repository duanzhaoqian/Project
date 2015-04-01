using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.RentalGoodses;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.BLL.FreightTemplates;
using KYJ.ZS.BLL.Collections;
using System.Text.RegularExpressions;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.BLL.Tags;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.Index;
using KYJ.ZS.BLL.Brands;
using KYJ.ZS.BLL.Categories;
using KYJ.ZS.Models.Categories;
using KYJ.ZS.BLL.Generalizes;
using KYJ.ZS.Web.Controllers.ActionFilters;
using KYJ.ZS.BLL.BrowseAmounts;
using KYJ.ZS.BLL.BrowseHistorys;
using KYJ.ZS.Models.BrowseHistorys;
using KYJ.ZS.Commons.PictureModular;
using KYJ.ZS.Commons.Common;

namespace KYJ.ZS.Web.Controllers
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-04-12
    /// 描述：租售商品
    /// </summary>
    public class RentalGoodsController : BaseController
    {
        
        
       
        GetRentalGoodsListBll getlistBll = new GetRentalGoodsListBll();
        BrandBll brandBll = new BrandBll();
        CategoryBll catBll = new CategoryBll();
        CategoryNormBll normBll = new CategoryNormBll();
       

        #region  邓福伟
        /// <summary>租商品详情页
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 详情：租商品详情页
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            #region 订单商品详情
            RentalGoodsBll bll = new RentalGoodsBll();
            Web_RentalGoodsEntity entity = bll.Web_GetRentalGoodsEntity(id);
            if (entity == null)
            {
                //详情页数据为空时，跳转到首页
                return Redirect(Url.WebSiteUrl().Index);
            }
            else
            { 
              //获取收藏数
               CollectionBll collectionBll = new CollectionBll();
              entity.CollectionCount = collectionBll.Web_CollectionCount(entity.Id);
              //扩展属性名称
              CategoryAttributeBll categoryAttributeBll = new CategoryAttributeBll();
              entity.OtherAttrName = categoryAttributeBll.GetCategoryAttrName(entity.CategoryId).ToJson();
              //扩展运费模版
              FreightTemplateBll freightTemplateBll = new FreightTemplateBll();
              entity.OtherFreightTemplate = freightTemplateBll.Web_GetFreightTemplateList(entity.FtempId).ToJson();
              //浏览量
              BrowseAmountBll browseAmountBll = new BrowseAmountBll();
              entity.BrowseAmount = browseAmountBll.Web_GetRentalGoodsBrowseAmount(entity.Id);
            }
            return View(entity);
            #endregion           
        }
        /// <summary>租商品预览详情页
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 详情：租商品详预览情页
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public ActionResult DetailPreview(int id)
        {
            #region 订单商品详情
            RentalGoodsBll bll = new RentalGoodsBll();
            Web_RentalGoodsEntity entity = bll.Web_GetRentalGoodsPreviewEntity(id);
            if (entity == null)
            {
                entity = new Web_RentalGoodsEntity();
            }
            else
            {
                //获取收藏数
                CollectionBll collectionBll = new CollectionBll();
                entity.CollectionCount = collectionBll.Web_CollectionCount(entity.Id);
                //扩展属性名称
                CategoryAttributeBll categoryAttributeBll = new CategoryAttributeBll();
                entity.OtherAttrName = categoryAttributeBll.GetCategoryAttrName(entity.CategoryId).ToJson();
                //扩展运费模版
                FreightTemplateBll freightTemplateBll = new FreightTemplateBll();
                entity.OtherFreightTemplate = freightTemplateBll.Web_GetFreightTemplateList(entity.FtempId).ToJson();
                //浏览量
                BrowseAmountBll browseAmountBll = new BrowseAmountBll();
                entity.BrowseAmount = browseAmountBll.Web_GetRentalGoodsBrowseAmount(entity.Id);
            }
            return View(entity);
            #endregion
        }

        /// <summary>获取商铺的其它商品
        /// 作者：邓福伟
        /// 时间：2014-05-23
        /// 描述：获取商铺的其它商品
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <param name="merchantid">商户Id</param>
        /// <param name="userid">用户类型1详情页2详情预览页</param>
        /// <returns></returns>
        public string OtherGoodsList(int id, int merchantid,int type)
        {
            RentalGoodsBll bll = new RentalGoodsBll();
            List<Web_RentalGoodsEntity> list =bll.Web_GetRentalGoodsListByMerchantId(id, merchantid);
            string strHtml = "";
            foreach (var entity in list)
            {
                //从Redis中获取商品图片
                var pic = GetPicture.GetFirstGoodsPicture(entity.RentalGuid, true, "SHOW");
                if (string.IsNullOrEmpty(pic))
                {
                    pic = PubConstant.StaticUrl + Url.Content("web/web/images/img/cont_rpic1.jpg");
                }
                else
                {
                    pic = pic + ".50_50.jpg";
                }
                //生成Html代码
                switch (type)
                {
                    case 1:
                        strHtml += "<li>" +
                                    "<a class=\"picimg\" href=\"" + Url.WebSiteUrl().RentalGoods_Detail(entity.Id) + "\">" +
                                    "<img src=\"\" data-original=\"" + pic + "\" ></a>" +
                                    "<div class=\"text\">" +
                                        "<p><a href=\"" + Url.WebSiteUrl().RentalGoods_Detail(entity.Id) + "\">" + entity.Title + "</a></p>" +
                                        "<div class=\"price\">" +
                                            "<span class=\"col333\">￥" + entity.MothPrice.ToDouble().ToString("F2") + "/月</span>" +
                                            //"<span class=\"ml10 col999\">浏览次数:" + entity.BrowseAmount + "</span></div>" +
                                    "</div>" +
                                "</li>";
                        break;
                    case 2:
                        strHtml += "<li>"+
                                    "<span class=\"picimg\"><img src=\"\" data-original=\"" + pic + "\" ></span>" +
                                    "<div class=\"text\">"+
                                        "<p>" + entity.Title + "</p>" +
                                        "<div class=\"price\">"+
                                            "<span class=\"col333\">￥" + entity.MothPrice.ToDouble().ToString("F2") + "/月</span>" +
                                            //"<span class=\"ml10 col999\">浏览次数:" + entity.BrowseAmount + "</span></div>" +
                                    "</div>"+
                                "</li>";
                        break;
                }
            }
            return strHtml;
        }
        /// <summary>获取商铺的同类商品
        /// 作者：邓福伟
        /// 时间：2014-05-23
        /// 描述：获取商铺的同类商品
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <param name="categoryid">分类Id</param>
        /// <param name="userid">用户类型1详情页2详情预览页</param>
        /// <returns></returns>
        public string SimilarGoodsList(int id, int categoryid,int type)
        {
            RentalGoodsBll bll = new RentalGoodsBll();
            List<Web_RentalGoodsEntity> list = bll.Web_GetRentalGoodsListByCategoryId(id, categoryid);
            string strHtml = "";
            foreach (var entity in list)
            {
                //从Redis中获取商品图片
                var pic = GetPicture.GetFirstGoodsPicture(entity.RentalGuid, true, "SHOW");
                if (string.IsNullOrEmpty(pic))
                {
                    pic = PubConstant.StaticUrl + Url.Content("web/web/images/img/cont_rpic1.jpg");
                }
                else
                {
                    pic = pic + ".50_50.jpg";
                }
                //生成Html代码
                switch (type)
                {
                    case 1:
                        strHtml += "<li>" +
                                      "<a class=\"picimg\" href=\"" + Url.WebSiteUrl().RentalGoods_Detail(entity.Id) + "\">" +
                                      "<img src=\"\" data-original=\"" + pic + "\" ></a>" +
                                      "<div class=\"text\">" +
                                          "<p><a href=\"" + Url.WebSiteUrl().RentalGoods_Detail(entity.Id) + "\">" + entity.Title + "</a></p>" +
                                          "<div class=\"price\">" +
                                              "<span class=\"col333\">￥" + entity.MothPrice.ToDouble().ToString("F2") + "/月</span>" +
                                              //"<span class=\"ml10 col999\">浏览次数:" + entity.BrowseAmount + "</span></div>" +
                                      "</div>" +
                                  "</li>";
                        break;
                    case 2:
                        strHtml += "<li>" +
                                    "<span class=\"picimg\"><img src=\"\" data-original=\"" + pic + "\" ></span>" +
                                    "<div class=\"text\">" +
                                        "<p>" + entity.Title + "</p>" +
                                        "<div class=\"price\">" +
                                            "<span class=\"col333\">￥" + entity.MothPrice.ToDouble().ToString("F2") + "/月</span>" +
                                            //"<span class=\"ml10 col999\">浏览次数:" + entity.BrowseAmount + "</span></div>" +
                                    "</div>" +
                                "</li>";
                        break;
                }
            }
            return strHtml;
        }


        /// <summary>添加收藏数据
        /// 作者：邓福伟
        /// 时间：2014-04-28
        /// 描述：添加收藏数据
        /// </summary>
        /// <param name="user_id">用户Id</param>
        /// <param name="goods_id">商品Id</param>
        /// <param name="url">收藏地址</param>
        /// <returns></returns>
        [HttpPost]
        [LoginChecked]
        public ActionResult AddCollections(FormCollection formCollection)
        {
            CollectionBll collectionBll = new CollectionBll();

            int user_id = _ServiceContext.CurrentUser.UserID;
            int goods_id = Auxiliary.ToInt32(formCollection["goods_id"]);
            int goods_type = Auxiliary.ToInt32(formCollection["goods_type"]);

            int intReuslts = collectionBll.Web_AddCollection(user_id, goods_id, goods_type);
            string strReuslts = string.Empty;
            switch (intReuslts)
            {
                case 0: strReuslts = "收藏失败！"; break;
                case 1: strReuslts = "收藏成功！"; break;
                case 2: strReuslts = "收藏已存在！"; break;
                case 3: strReuslts = "收藏商品错误！"; break;
                default: strReuslts = "收藏错误！"; break;
            }
            return Json(new { result = intReuslts, message= strReuslts }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>添加浏览历史
        /// 作者：邓福伟
        /// 时间：2014-05-28
        /// 描述：添加浏览历史
        /// </summary>
        /// <param name="id">商品id</param>
        /// <param name="rental_guid">商品guid</param>
        /// <param name="monthprice">商品月租金</param>
        /// <returns></returns>
        [LoginChecked]
        public string BrowseHistoryOperate(int id, string rental_guid, string monthprice)
        {
            if (_ServiceContext.CurrentUser != null)
            {
                BrowseHistoryBll browseHoistoryBll = new BrowseHistoryBll();
                BrowseHistoryEntity browseHoistoryEntity = new BrowseHistoryEntity();
                browseHoistoryEntity.GoodsId = id;
                browseHoistoryEntity.GoodsGuid = rental_guid;
                if ((monthprice != null ? monthprice.Length : 0) > 0)
                {
                    int indexNum = monthprice.IndexOf('-');
                    //存在最大值和最小值
                    if (indexNum > 0)
                    {
                        browseHoistoryEntity.GoodsPrice = monthprice.Substring(0, indexNum).ToDouble();
                    }
                    else
                    {
                        browseHoistoryEntity.GoodsPrice = monthprice.ToDouble();
                    }
                }
                browseHoistoryBll.BrowseHistoryOperate(_ServiceContext.CurrentUser._Guid, browseHoistoryEntity);
            }
            return "浏览历史";
        }
        #endregion


        #region 王威
        /// <summary>
        /// 商品三级列表
        /// 作者：wwang
        /// 时间：2014-4-29
        /// 描述：出租商品三级列表页面
        /// </summary>
        /// <param name="condition">搜索条件集合</param>
        /// <returns></returns>
        public ActionResult List(string condition)
        {
            RentalGoodsListEntity model = new RentalGoodsListEntity();


            #region 搜索数据处理

            bool iscurrentpage = Regex.IsMatch(condition, @"-i\d{1,}");//是否是页格式
            var url = Request.RawUrl.Substring(1);
            var key = Regex.Match(url, @"-w_\w+").ToString();//关键字
            if (!iscurrentpage)
            if (key != "") 
            {
                condition = condition.Replace(key, "") + "-i1" + key; 
                url = url.Replace(key, "") + "-i1" + key; 
            }
            else 
            {
                condition = condition + "-i1";
                url = url + "-i1";
            }//把key加到url最后

            #endregion

            try
            {
                IndexGoodsConditionInfo info = IndexCondition.GetSearchCondiction(condition);

                int firstlyCatId = Convert.ToInt32(info.FirstlyCatId);

                int secondaryCatId = Convert.ToInt32(info.SecondaryCatId);

                int thirdlyCatId = Convert.ToInt32(info.ThirdlyCatId);

                //获取当前标签
                //Tag tag = tagbll.GetTagById(tagid);
                ////获取标签所属分类的标签集合
                //List<Tag> tagList = tagbll.GetTagsByCategoryId(tag.CategoryId);

                //绑定分类菜单
                RentalGoodsCatMenuEntity catMenuModel = new RentalGoodsCatMenuEntity();
                catMenuModel.CatMenuItems = new List<CatMenuItem>();
                var firstlyCategory = catBll.GetCategoryList().List.First((a) => a.Id == firstlyCatId);
                var secondaryCategory = catBll.GetCategoryList().List.Where((a) => a.Pid == firstlyCatId);


                foreach (var f in secondaryCategory)
                {

                    CatMenuItem item = new CatMenuItem();
                    item.Categories = new List<CatResult>();
                    item.Channel = f;
                    var thirdlyCategory = catBll.GetCategoryList().List.Where((a) => a.Pid == f.Id);
                    foreach (var s in thirdlyCategory)
                    {
                        item.Categories.Add(s);
                        if (s.Id == thirdlyCatId)
                        {
                            ViewBag.ThirdlyName = s.Name;
                        }

                    }
                    catMenuModel.CatMenuItems.Add(item);
                }

                model.CatMenu = catMenuModel;


                PagedList<RentalGoodsListItemEntity> list = getlistBll.RentalGoodsListResutl(condition, "");

                model.ItemList = list;


                //绑定搜索菜单
                RentalGoodsSearchMenuEntity searchMenu = new RentalGoodsSearchMenuEntity();
                searchMenu.Attrs = getlistBll.GetCateroryAttrsById(thirdlyCatId);
                searchMenu.BrandList = brandBll.GetBrandsByCatId(thirdlyCatId);
                searchMenu.CategoryNorms = normBll.GetCategoryNorm(thirdlyCatId);
                model.SearchMenu = searchMenu;

                //绑定分页
                model.PageSize = list.PageSize;
                model.TotalItemCount = list.TotalItemCount;
                model.CurrentPageIndex = list.CurrentPageIndex;
                ViewData["conPage"] = url;

                ViewBag.FirstlyCatId = firstlyCatId;
                ViewBag.SecondaryCatId = secondaryCatId;
                ViewBag.ThirdlyCatId = thirdlyCatId;

                ViewBag.Title = firstlyCategory.Name + "-" + catBll.GetCategoryList().List.First((a) => a.Id == secondaryCatId).Name + "-" + catBll.GetCategoryList().List.First((a) => a.Id == thirdlyCatId).Name;
            }
            catch (Exception ex)
            {
                return Redirect(Url.WebSiteUrl().Index);
               // KYJ.ZS.Log4net.RecordLog.RecordException<string>("wwang", condition, ex);
               
            }
         
            return View(model);
        }

        /// <summary>
        /// 商品二级列表
        /// 作者：wwang
        /// 时间：2014-4-29
        /// 描述：出租商品二级列表页面
        /// </summary>
        /// <param name="condition">条件集合</param>
        /// <returns></returns>
        public ActionResult Channel(string condition)
        {

            IndexGoodsConditionInfo info = IndexCondition.GetSearchCondiction(condition);

            RentalGoodsChannelEntity model = new RentalGoodsChannelEntity();

            try
            {


                int firstlyCatId = Convert.ToInt32(info.FirstlyCatId);

                int secondaryCatId = Convert.ToInt32(info.SecondaryCatId);

                int thirdlyCatId = Convert.ToInt32(info.ThirdlyCatId);


                RentalGoodsCatMenuEntity catMenuModel = new RentalGoodsCatMenuEntity();
                catMenuModel.CatMenuItems = new List<CatMenuItem>();

                string catcondition = info.FirstlyCatId + "-" + info.SecondaryCatId;

                var firstlyCategory = catBll.GetCategoryList().List.First((a) => a.Id == firstlyCatId);

                var secondaryCategory = catBll.GetCategoryList().List.Where((a) => a.Pid == firstlyCatId);

                foreach (var f in secondaryCategory)
                {
                    CatMenuItem item = new CatMenuItem();
                    item.Categories = new List<CatResult>();
                    item.Channel = f;
                    var thirdlyCategory = catBll.GetCategoryList().List.Where((a) => a.Pid == f.Id);

                    foreach (var s in thirdlyCategory)
                    {
                        item.Categories.Add(s);
                    }
                    catMenuModel.CatMenuItems.Add(item);
                }
                ViewBag.FirstlyCatId = firstlyCatId;
                ViewBag.SecondaryCatId = secondaryCatId;
                ViewBag.ThirdlyCatId = thirdlyCatId;

                model.CatMenu = catMenuModel;

                ViewBag.Title = firstlyCategory.Name + "-" + catBll.GetCategoryList().List.First((a) => a.Id == secondaryCatId).Name;
            }
            catch (Exception ex)
            {
                return Redirect(Url.WebSiteUrl().Index);
                //返回404页面
                //KYJ.ZS.Log4net.RecordLog.RecordException<string>("wwang", condition, ex);
            }
            return View(model);
        }
        #endregion
    }
}
