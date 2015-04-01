using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KYJ.ZS.Commons
{
    public class WebSiteUrlHelper
    {
        private static readonly WebSiteUrlHelper instance = new WebSiteUrlHelper();

        private static string BaseUrl;

        internal static WebSiteUrlHelper Instance
        {
            get
            {
                if (string.IsNullOrEmpty(BaseUrl))
                    BaseUrl = PubConstant.WebBaseUrl;
                return instance;
            }
        }

        private WebSiteUrlHelper()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal WebSiteUrlHelper SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }

        #region URL集装箱

        /// <summary>
        /// 商城首页地址
        /// </summary>
        public string Index
        {
            get { return BaseUrl; }
        }
        /// <summary>
        /// 商品三级页面地址
        /// </summary>
        public string RentalList
        {
            get { return BaseUrl + "/zu"; }
        }
        /// <summary>
        /// 商品二级页面地址
        /// </summary>
        public string Channel_Index
        {
            get { return BaseUrl + "/channel"; }
        }

        #region 作者：邓福伟 时间：2013-04-23 简介：租售详情
        /// <summary> 租售商品详情地址
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 描述：租售商品详情地址
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public string RentalGoods_Detail(int id)
        {
            return BaseUrl + "/rt-" + id + ".html";
        }
        /// <summary>租商品预览详情页地址
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 详情：租商品详预览情页
        /// </summary>
        public string RentalGoods_DetailPreview(int id)
        {
            return BaseUrl + "/rt-p-" + id + ".html";
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
        public string RentalGoods_AddCollections
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "addcollections", "rentalgoods"); }
        }
        /// <summary>获取商户的其它商品
        /// 作者：邓福伟
        /// 时间：2014-05-23
        /// 描述：获取商户的其它商品
        /// </summary>
        public string RentalGoods_OtherGoodsList(int id, int merchantid, int type)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "othergoodslist", "rentalgoods", new { id = id, merchantid = merchantid,type=type });
        }
        /// <summary>获取商铺的同类商品
        /// 作者：邓福伟
        /// 时间：2014-05-23
        /// 描述：获取商铺的同类商品
        /// </summary>
        public string RentalGoods_SimilarGoodsList(int id, int categoryid, int type)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "similargoodslist", "rentalgoods", new { id = id, categoryid = categoryid,type = type });
        }
        /// <summary>添加浏览历史
        /// 作者：邓福伟
        /// 时间：2014-05-28
        /// 描述：添加浏览历史
        /// </summary>
        public string RentalGoods_BrowseHistoryOperate(int id, string rental_guid, string monthprice)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "browsehistoryoperate", "rentalgoods", new { id = id, rental_guid = rental_guid, monthprice = monthprice });
        }
        #endregion
        #region 作者：邓福伟 时间：2013-04-29 简介：订单页
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-29
        /// 描述：订单页地址 
        /// </summary>
        public string Orders_Detail(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "detail", "orders", new { id = id });
        }
        /// <summary> 根据User_Id查找收货地址
        /// 作者:邓福伟
        /// 时间:2014-4-29
        /// 描述:根据User_Id查找收货地址
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>收货地址集合</returns>
        public string Orders_DeliveryAddressesList
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deliveryaddresseslist", "orders"); } 
        }
        /// <summary>根据收货地址Id获取收货详情
        /// 作者:邓福伟
        /// 时间:2014-5-9
        /// 描述:根据收货地址Id获取收货详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>收货地址详情</returns>
        public string Orders_DeliveryAddressesEntity
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deliveryaddressesentity", "orders"); }
        }
        /// <summary>订单提交，生成订单
        /// 作者：邓福伟
        /// 时间：2014-5-7
        /// 描述：订单提交，生成订单
        /// </summary>
        /// <returns></returns>
        public string Orders_SubmitOrder
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "submitorder", "orders"); }
        }
        /// <summary>收货地址删除或修改
        /// 作者:邓福伟
        /// 时间:2014-5-9
        /// 描述:收货地址删除或修改
        /// </summary>
        /// <returns>返回受影响的行数</returns>
        public string Orders_DeliveryAddressesAddEdit 
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deliveryaddressesaddedit", "orders"); }
        }
        #endregion
        #region 作者：邓福伟 时间：2013-04-30 简介：售详情
        /// <summary>售列表页地址
        /// 作者：邓福伟
        /// 时间：2014-04-30
        /// 描述：售列表页地址
        /// </summary>
        public string SaleGoods_Index
        {
            get { return BaseUrl + "/xianzhi/xz"; }
        }
        /// <summary> 售商品详情地址
        /// 作者:邓福伟
        /// 时间:2014-4-30
        /// 描述:售商品详情地址
        /// </summary>
        public string SaleGoods_Detail(int id)
        {
            return BaseUrl + "/sl-" + id + ".html";
        }
        /// <summary>售商品预览详情地址
        /// 作者：邓福伟
        /// 时间：2014-4-22
        /// 详情：售商品预览详情
        /// </summary>
        public string SaleGoods_DetailPreview(int id)
        {
            return BaseUrl + "/sl-p-" + id + ".html";
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-23
        /// 描述：获取用户的其它商品
        /// </summary>
        public string SaleGoods_OtherGoodsList(int id, int userid,int type)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "othergoodslist", "salegoods", new { id = id,userid=userid,type=type });
        }
        #endregion
        #region 作者：邓福伟 时间:2014-05-08 描述:地理位置
        /// <summary>地理位置
        /// 作者：邓福伟 时间:2014-05-08 描述:地理位置
        /// </summary>
        /// <param name="actionName">ActionName</param>
        public string GeographyLocation(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "common");
        }

        #endregion
        #endregion

        #region URL工具方法

        /// <summary>
        /// 获取Action的绝对URL
        /// </summary>
        /// <param name="actionName">action的名称</param>
        /// <param name="controllerName">controller的名称</param>
        /// <returns></returns>
        private string ActionUrlAbsolute(string actionName, string controllerName)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.Action(actionName, controllerName)).AbsoluteUri;
        }

        /// <summary>
        /// 获取Action的绝对URL
        /// </summary>
        /// <param name="actionName">action的名称</param>
        /// <param name="controllerName">controller的名称</param>
        /// <param name="routeValues">路由数据</param>
        /// <returns></returns>
        private string ActionUrlAbsolute(string actionName, string controllerName, object routeValues)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.Action(actionName, controllerName, routeValues)).AbsoluteUri;
        }

        /// <summary>
        /// 获取Route的绝对URL
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeValues">路由数据</param>
        /// <returns></returns>
        private string RouteUrlAbsolute(string routeName, object routeValues)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.RouteUrl(routeName, routeValues)).AbsoluteUri;
        }

        private Uri GetBaseUrl(UrlHelper url)
        {
            Uri contextUri = new Uri(url.RequestContext.HttpContext.Request.Url,
                url.RequestContext.HttpContext.Request.RawUrl);
            UriBuilder realmUri = new UriBuilder(contextUri)
            {
                Path = url.RequestContext.HttpContext.Request.ApplicationPath,
                Query = null,
                Fragment = null
            };
            return realmUri.Uri;
        }

        #endregion

    }
}
