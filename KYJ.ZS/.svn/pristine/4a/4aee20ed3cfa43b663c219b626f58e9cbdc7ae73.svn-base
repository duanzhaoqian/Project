using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.SaleGoodses;
using KYJ.ZS.BLL.SaleGoodses;
using KYJ.ZS.BLL.Tags;
using KYJ.ZS.Models.DB;
using System.Text.RegularExpressions;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.BLL.BrowseAmounts;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.PictureModular;
using KYJ.ZS.Commons.Common;

namespace KYJ.ZS.Web.Controllers
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间：2014-04-30
    /// 描述：售详情
    /// </summary>
    public class SaleGoodsController : BaseController
    {
        SaleGoodsBll bll = new SaleGoodsBll();
        TagBll tagBll = new TagBll();

        #region 邓福伟
        /// <summary>售商品详情页
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 详情：售商品详情页
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            //售的商品Entity
            Web_SaleGoodsEntity entity = bll.Web_GetSaleGoodsEntity(id);
            if (entity == null)
            {
                //详情页数据为空时，跳转到首页
                return Redirect(Url.WebSiteUrl().Index);
            }
            else
            {
                //获取用户的全部闲置物品数
                entity.UserSaleGoodsCount = bll.Web_GetSaleGoodsCount(entity.UserId);
                //商品浏览量
                BrowseAmountBll browseAmountBll = new BrowseAmountBll();
                entity.BrowseAmount = browseAmountBll.Web_GetSaleGoodsBrowseAmount(entity.Id);
                //判断用户是否登录和电话号码是否为空
                if (_ServiceContext.CurrentUser == null && entity.ContactPhone != null)
                {
                    //电话号码加型号*
                    entity.ContactPhone = entity.ContactPhone.Length > 4 ? entity.ContactPhone.Substring(0, entity.ContactPhone.Length - 4) + "****" : "****";
                }
            }
            return View(entity);
        }
        /// <summary>售商品预览详情页
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 详情：售商品预览详情页
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public ActionResult DetailPreview(int id)
        {
            #region 售的商品详情
            //售的商品Entity
            Web_SaleGoodsEntity entity = bll.Web_GetSaleGoodsPreviewEntity(id);
            if (entity == null)
            {
                //详情页数据为空时，跳转到首页
                entity = new Web_SaleGoodsEntity();
            }
            else
            {
                //获取用户的全部闲置物品数
                entity.UserSaleGoodsCount = bll.Web_GetSaleGoodsCount(entity.UserId);
                //商品浏览量
                BrowseAmountBll browseAmountBll = new BrowseAmountBll();
                entity.BrowseAmount = browseAmountBll.Web_GetSaleGoodsBrowseAmount(entity.Id);
                //判断用户是否登录和电话号码是否为空
                if (entity.ContactPhone != null)
                {
                    //电话号码加型号*
                    entity.ContactPhone = entity.ContactPhone.Length > 4 ? entity.ContactPhone.Substring(0, entity.ContactPhone.Length - 4) + "****" : "****";
                }
            }
            return View(entity);
            #endregion
        }
        /// <summary>获取用户的其它商品
        /// 作者：邓福伟
        /// 时间：2014-05-23
        /// 描述：获取用户的其它商品
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <param name="userid">用户Id</param>
        /// <param name="userid">用户类型1详情页2详情预览页</param>
        /// <returns></returns>
        public string OtherGoodsList(int id,int userid,int type)
        {
            List<Web_SaleGoodsEntity> list = bll.Web_GetSaleGoodsListByUserId(id, userid);
            string strHtml = "";
            foreach (var entity in list)
            {
                
                #region 从Redis中获取商品图片
                var pic = GetPicture.GetFirstGoodsPicture(entity.SaleGuid, true, "FREE");
                if (string.IsNullOrEmpty(pic))
                {
                    pic = PubConstant.StaticUrl + Url.Content("web/web/images/img/cont_rpic1.jpg");
                }
                else
                {
                    pic = pic + ".50_50.jpg";
                }
                #endregion
                #region 生成Html代码
                switch (type)
                {
                    case 1: //详情页
                        strHtml += "<li>" +
                                    "<a class=\"picimg\" href=\"" + Url.WebSiteUrl().SaleGoods_Detail(entity.Id) + "\">" +
                                    "<img src=\"\" data-original=\"" + pic + "\" ></a>" +
                                    "<div class=\"text\">" +
                                        "<p><a href=\"" + Url.WebSiteUrl().SaleGoods_Detail(entity.Id) + "\">" + entity.Title + "</a></p>" +
                                        "<div class=\"price\">" +
                                            "<span class=\"col333\">￥" + entity.Price.ToString("F2") + "</span>" +
                                            //"<span class=\"ml10 col999\">浏览次数:" + entity.BrowseAmount.ToString() + "</span></div>" +
                                    "</div>" +
                                 "</li>";
                        break;
                    case 2: //详情预览页
                        strHtml += "<li>" +
                                    "<span class=\"picimg\">" +
                                    "<img src=\"\" data-original=\"" + pic + "\" ></span>" +
                                    "<div class=\"text\">" +
                                        "<p>" + entity.Title + "</p>" +
                                        "<div class=\"price\">" +
                                            "<span class=\"col333\">￥" + entity.Price.ToString("F2") + "</span>" +
                                            //"<span class=\"ml10 col999\">浏览次数:" + entity.BrowseAmount.ToString() + "</span></div>" +
                                    "</div>" +
                                "</li>";
                        break;
                }
                 #endregion
            }
            return strHtml;
        }
        #endregion

        #region 王威
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-5
        /// 描述：闲置物品列表页
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult List(string condition="")
        {
            SaleGoodsListEntity model = new SaleGoodsListEntity();

            try
            {
                bool iscurrentpage = Regex.IsMatch(condition, @"-i\d{1,}");//是否是页格式
                var url = Request.RawUrl.Substring(1);
                var key = Regex.Match(condition, @"-w_\w+").ToString();//关键字

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


             

                List<Tag> tags = tagBll.GetsTagsByType(2);
                SaleGoodsCatMenuEntity catMenu = new SaleGoodsCatMenuEntity();
                catMenu.Tags = tags;

                PagedList<Web_SaleGoodsEntity> list = bll.GetSaleGoodsList(condition, "");


                model.ItemList = list;
                model.TotalItemCount = list.TotalItemCount;
                model.PageSize = list.PageSize;
                catMenu.TotalCount = list.TotalItemCount;
                model.CurrentPageIndex = list.CurrentPageIndex;
                model.CatMenu = catMenu;

                ViewData["conPage"] = url;
             
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<string>("wwang", condition, ex);
            }
            return View(model);
        }
        #endregion
    }
}
